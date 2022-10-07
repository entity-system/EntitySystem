using System.Collections.Generic;
using EntitySystem.Client.Components.Data.Entity.Dialog.Source;
using EntitySystem.Client.Components.Data.Record.List.Condition;
using EntitySystem.Client.Components.Data.Record.List.Order;
using EntitySystem.Client.Components.Data.Record.List.Property;

namespace EntitySystem.Client.Components.Data.Record.List.Source;

public interface IDataRecordListSource<TKey> : IEntityDialogSource<TKey>
{
    int TargetDeep { get; }

    int JoinDeep { get; }

    int Limit { get; set; }

    int Offset { get; set; }

    int MasterCount { get; set; }

    IEnumerable<IDataRecordListOrder> GetRecordListOrdersNested();

    IEnumerable<IDataRecordListCondition> GetRecordListConditionsNested();

    IEnumerable<IDataRecordListProperty> GetRecordListProperties();
}