namespace EntitySystem.Client.Components.Data.Record.List.Condition;

public interface IDataRecordListCondition
{
    long Priority { get; }

    string Name { get; }

    void UnAssign();
}