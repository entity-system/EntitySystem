namespace EntitySystem.Client.Components.Data.Record.List.Order;

public interface IDataRecordListOrder
{
    long Priority { get; }

    string Name { get; }

    bool Descending { get; }

    void UnAssign();
}