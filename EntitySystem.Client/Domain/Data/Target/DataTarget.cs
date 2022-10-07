using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Target.Parameters;
using EntitySystem.Client.Components.Data.Input.Target.Property;
using EntitySystem.Client.Components.Data.Input.Target.Source;
using EntitySystem.Client.Components.Data.Record.Info.Property;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Domain.Data.Target.Feature.PropagatedEntity;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target;

public class DataTarget<TParent, TParentService, TChild, TChildService> : DataProperty<TParent, TChild>, IDataTarget<TParent, TChild>,
    IDataInputTargetProperty<TParent, TChild>
    where TParent : IEntity, new()
    where TParentService : IEntityService<TParent>
    where TChild : IEntity, new()
    where TChildService : IEntityService<TChild>
{
    private readonly IDictionary<IDataProperty, IDataProperty<TParent>> _composedTargetProperties;

    private readonly IDictionary<IDataProperty, IDataProperty<TParent>> _composedRawProperties;

    public DataSource<TParent, TParentService> ParentSource { get; set; }

    public DataSource<TChild, TChildService> ChildSource { get; set; }

    public Expression<Func<TChild, string>> ChildNameExpression { get; set; }

    public Func<TChild, string> ChildNameGetter { get; set; }

    public bool IsImplicitChild { get; set; }

    public DataTarget(DataSource<TParent, TParentService> parentSource, DataSource<TChild, TChildService> childSource, Expression<Func<TParent, TChild>> expression, Expression<Func<TChild, string>> childNameExpression) : base(parentSource, expression)
    {
        ParentSource = parentSource;

        ChildSource = childSource;

        Name = childSource.Name;

        ChildNameExpression = childNameExpression;

        ChildNameGetter = childNameExpression.Compile();

        _composedTargetProperties = new Dictionary<IDataProperty, IDataProperty<TParent>>(SelfComposeTargetProperties());

        _composedRawProperties = new Dictionary<IDataProperty, IDataProperty<TParent>>(SelfComposeRawProperties());
    }

    private IEnumerable<KeyValuePair<IDataProperty, IDataProperty<TParent>>> SelfComposeTargetProperties()
    {
        foreach (var property in ChildSource.Targets) yield return new KeyValuePair<IDataProperty, IDataProperty<TParent>>(property, property.Compose(Source, Expression));

        foreach (var (_, value) in ChildSource.Targets.SelectMany(t => t.ComposeTargetProperties())) yield return new KeyValuePair<IDataProperty, IDataProperty<TParent>>(value, value.Compose(Source, Expression));
    }

    private IEnumerable<KeyValuePair<IDataProperty, IDataProperty<TParent>>> SelfComposeRawProperties()
    {
        foreach (var property in ChildSource.Properties) yield return new KeyValuePair<IDataProperty, IDataProperty<TParent>>(property, property.Compose(Source, Expression));

        foreach (var (_, value) in ChildSource.Targets.SelectMany(t => t.ComposeRawProperties())) yield return new KeyValuePair<IDataProperty, IDataProperty<TParent>>(value, value.Compose(Source, Expression));
    }

    public IEnumerable<KeyValuePair<IDataProperty, IDataProperty<TParent>>> ComposeTargetProperties()
    {
        return _composedTargetProperties;
    }

    public IEnumerable<KeyValuePair<IDataProperty, IDataProperty<TParent>>> ComposeRawProperties()
    {
        return _composedRawProperties;
    }

    public IDataSource<TParent> GetParentEntitySource()
    {
        return ParentSource;
    }

    public IDataSource<TChild> GetChildEntitySource()
    {
        return ChildSource;
    }

    public IDataSource GetChildSource()
    {
        return ChildSource;
    }

    public IEnumerable<IDataRecordInfoProperty<TParent>> GetRecordInfoProperties()
    {
        return _composedRawProperties.Values.Select(p => p.GetRecordInfoProperty());
    }

    public IEnumerable<IDataRecordListProperty> GetRecordListProperties()
    {
        return GetObjectPropertiesNested().Select(p => p.GetRecordListProperty());
    }

    public IEnumerable<IDataProperty<TParent>> GetObjectPropertiesNested()
    {
        return _composedRawProperties.Values;
    }

    public IEnumerable<IDataEntityDialogProperty<TParent>> GetEntityDialogRawProperties()
    {
        if (!IsImplicitChild) yield break;

        var localProperties = ChildSource.Properties
            .Join(_composedRawProperties, p => p, c => c.Key, (_, c) => c.Value);

        foreach (var property in localProperties) yield return property.GetEntityDialogProperty();

        var remoteProperties = ChildSource.Targets
            .SelectMany(t => t.GetEntityDialogRawProperties()
            .Join(_composedRawProperties, p => p, c => c.Key.GetDialogProperty(), (_, c) => c.Value));

        foreach (var property in remoteProperties) yield return property.GetEntityDialogProperty();
    }

    public IEnumerable<IDataProperty<TParent>> GetEntityDialogTargetProperties()
    {
        if (!IsImplicitChild)
        {
            yield return this;

            yield break;
        }

        var properties = ChildSource.Targets
            .SelectMany(t => t.GetEntityDialogTargetProperties()
            .Join(_composedTargetProperties, p => p, c => c.Key, (_, c) => c.Value));

        foreach (var property in properties) yield return property;
    }

    public async Task OnBeforeSaveOrUpdateAsync(TParent parent)
    {
        var child = Getter(parent);

        foreach (var target in ChildSource.Targets) await target.OnBeforeSaveOrUpdateAsync(child);

        if (IsImplicitChild) await SaveImplicitChildAsync(parent, child);
    }

    private async Task SaveImplicitChildAsync(TParent parent, TChild child)
    {
        child = await ChildSource.SaveOrUpdateAsync(child);

        Setter(parent, child);

        IsImplicitChild = false;
    }

    public override IRenderer BuildInput(TParent source, IDataInputOptions option)
    {
        var parameters = new DataInputTargetParameters<TParent, TChild>(RegistrationProvider, this, source, option);

        return BuildInput(parameters);
    }

    public IDataInputTargetSource<TChild> GetTargetChildSource()
    {
        return ChildSource;
    }

    public async Task<TChild> CreateChildAsync(TParent parent)
    {
        IsImplicitChild = true;

        var child = await ChildSource.CreateEntityAsync();

        await OnAfterCreateChildAsync(parent, child);

        return child;
    }

    public async Task EditChildAsync(TParent parent)
    {
        IsImplicitChild = true;

        await Task.CompletedTask;
    }

    public async Task InitializeEntityAsync(TParent parent)
    {
        var child = Getter(parent);

        if (child == null) return;

        await ChildSource.InitializeEntityAsync(child);
    }

    public async Task OnAfterCreateChildAsync(TParent parent, TChild child)
    {
        foreach (var feature in Features.GetFeatures<IDataPropagatedEntityFeature>())
        {
            await feature.OnAfterCreateChildAsync(this, parent, child);
        }
    }

    public void ResetProperties()
    {
        Reset();

        foreach (var property in _composedTargetProperties.Values) property.Reset();

        ChildSource.ResetProperties();
    }

    public override void Reset()
    {
        IsImplicitChild = false;
    }

    public IEnumerable<IEntity> GetPairedEntities(IEntity entity)
    {
        if (entity is not TParent source) yield break;

        yield return Getter(source);
    }
}