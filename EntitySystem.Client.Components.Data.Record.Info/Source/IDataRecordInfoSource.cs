using System.Collections.Generic;
using EntitySystem.Client.Components.Data.Entity.Dialog.Source;
using EntitySystem.Client.Components.Data.Record.Info.Join;
using EntitySystem.Client.Components.Data.Record.Info.Property;

namespace EntitySystem.Client.Components.Data.Record.Info.Source;

public interface IDataRecordInfoSource : IEntityDialogSource
{
}

public interface IDataRecordInfoSource<TEntity> : IDataRecordInfoSource, IEntityDialogSource<TEntity>
{
    IEnumerable<IDataRecordInfoProperty<TEntity>> GetRecordInfoProperties();

    IEnumerable<IDataRecordInfoJoin<TEntity>> GetRecordInfoJoins();
}