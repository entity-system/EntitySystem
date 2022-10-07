using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;
using EntitySystem.Client.Components.Data.Record.Info.Property;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Target;

public interface IDataTarget : IDataPair, IDataProperty
{
    IEnumerable<IDataRecordListProperty> GetRecordListProperties();
}

public interface IDataTarget<TParent> : IDataTarget, IDataProperty<TParent>
    where TParent : IEntity
{
    IEnumerable<KeyValuePair<IDataProperty, IDataProperty<TParent>>> ComposeTargetProperties();

    IEnumerable<KeyValuePair<IDataProperty, IDataProperty<TParent>>> ComposeRawProperties();

    IEnumerable<IDataRecordInfoProperty<TParent>> GetRecordInfoProperties();

    IEnumerable<IDataProperty<TParent>> GetObjectPropertiesNested();

    IEnumerable<IDataEntityDialogProperty<TParent>> GetEntityDialogRawProperties();

    IEnumerable<IDataProperty<TParent>> GetEntityDialogTargetProperties();

    Task OnBeforeSaveOrUpdateAsync(TParent source);

    void ResetProperties();

    Task InitializeEntityAsync(TParent parent);
}

public interface IDataTarget<TParent, TChild> : IDataTarget<TParent>, IDataProperty<TParent, TChild>, IDataPair<TParent, TChild>
    where TParent : IEntity
    where TChild : IEntity
{
}