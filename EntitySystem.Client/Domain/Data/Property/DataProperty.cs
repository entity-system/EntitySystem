using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;
using EntitySystem.Client.Components.Data.Header.Factory;
using EntitySystem.Client.Components.Data.Header.Options;
using EntitySystem.Client.Components.Data.Header.Origin;
using EntitySystem.Client.Components.Data.Header.Parameters;
using EntitySystem.Client.Components.Data.Header.Property;
using EntitySystem.Client.Components.Data.Header.Source;
using EntitySystem.Client.Components.Data.Input.Factory;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Parameters;
using EntitySystem.Client.Components.Data.Input.Property;
using EntitySystem.Client.Components.Data.Output.Property;
using EntitySystem.Client.Components.Data.Record.Info.Property;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Domain.Data.List;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Abstract.Extensions;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property;

public abstract class DataProperty<TEntity, TValue> : Featured,
    IDataProperty<TEntity, TValue>,
    IDataRecordInfoProperty<TEntity>,
    IDataRecordListProperty,
    IDataEntityDialogProperty<TEntity>,
    IDataHeaderProperty<TEntity, TValue>,
    IDataOutputProperty<TEntity, TValue>
    where TEntity : IEntity
{
    protected readonly IRegistrationProvider RegistrationProvider;

    public long Priority { get; set; } = DateTime.UtcNow.Ticks;

    public string Name { get; set; }

    public int TargetDeep { get; set; }

    public int JoinDeep { get; set; }

    public IDataSource<TEntity> Source { get; }

    public Expression<Func<TEntity, TValue>> Expression { get; }

    public Func<TEntity, TValue> Getter { get; }

    public Action<TEntity, TValue> Setter { get; }

    public virtual Type EntityType => typeof(TEntity);

    public virtual Type ValueType => typeof(TValue);

    protected DataProperty(IDataSource<TEntity> source, Expression<Func<TEntity, TValue>> property)
    {
        RegistrationProvider = source.RegistrationProvider;

        Source = source;

        Expression = property;

        Getter = property.Compile();

        Setter = property.CompileSetter();
    }

    public IDataProperty<T> Compose<T>(IDataSource<T> source, Expression<Func<T, TEntity>> expression)
        where T : IEntity
    {
        return new DataPropertyComposer<T, TEntity, TValue>(source, expression, this);
    }

    public virtual Task OnAfterSaveOrUpdateAsync(TEntity entity)
    {
        return Task.CompletedTask;
    }

    public IDataRecordInfoProperty<TEntity> GetRecordInfoProperty()
    {
        return this;
    }

    public IDataEntityDialogProperty<TEntity> GetEntityDialogProperty()
    {
        return this;
    }

    public IDataEntityDialogProperty GetDialogProperty()
    {
        return this;
    }

    public IDataRecordListProperty GetRecordListProperty()
    {
        return this;
    }

    public IDataValue GetValue<TKey>(DataObject<TKey, TEntity> @object) where TKey : IEntity
    {
        return new DataValue<TKey, TEntity, TValue>(@object, this);
    }

    public IRenderer BuildHeader(IDataHeaderOptions options)
    {
        return BuildHeader(Source, e => e, options);
    }

    public virtual IRenderer BuildHeader<TOrigin>(IDataSource<TOrigin> originSource, Expression<Func<TOrigin, TEntity>> originExpression, IDataHeaderOptions option)
        where TOrigin : IEntity
    {
        var origin = new DataHeaderOrigin<TOrigin, TEntity, TValue>(this, originSource.GetHeaderSource(), Source.GetHeaderSource(), originExpression);

        return BuildHeader(option, origin);
    }

    protected virtual IRenderer BuildHeader(IDataHeaderOptions option, IDataHeaderOrigin<TEntity> origin)
    {
        var parameters = CreateHeaderParameters(this, option, origin);

        var factory = RegistrationProvider.GetRegistration<IDataHeaderFactory>();

        return factory.Build(parameters);
    }

    private static DataHeaderParameters<TProperty, TEntity, TValue> CreateHeaderParameters<TProperty>(TProperty property, IDataHeaderOptions option, IDataHeaderOrigin<TEntity> origin)
        where TProperty : IDataHeaderProperty<TEntity, TValue>
    {
        return new DataHeaderParameters<TProperty, TEntity, TValue>(property, option, origin);
    }

    public IDataHeaderSource<TEntity> GetDataHeaderSource()
    {
        return Source.GetHeaderSource();
    }

    public IDataSource GetDataSource()
    {
        return Source;
    }

    public abstract IRenderer BuildInput(TEntity entity, IDataInputOptions option);

    protected IRenderer BuildInput<TProperty>(DataInputParameters<TProperty, TEntity, TValue> parameters)
        where TProperty : IDataInputProperty<TEntity, TValue>
    {
        var factory = RegistrationProvider.GetRegistration<IDataInputFactory>();

        return factory.Build(parameters);
    }

    public string ToString(TEntity entity)
    {
        var value = Getter(entity);

        return value?.ToString();
    }

    public virtual void Reset()
    {
    }

    public bool IsInRelationWith(Type type)
    {
        return EntityType == type || ValueType == type;
    }
}