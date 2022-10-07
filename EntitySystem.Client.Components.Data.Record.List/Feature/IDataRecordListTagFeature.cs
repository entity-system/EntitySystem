using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.List.Feature;

public interface IDataRecordListTagFeature<TKey> : IFeature, IRegistrable
{
    public IEnumerable<IRenderer> Build(BaseDataRecordList<TKey> recordList);
}

public interface IDataRecordListTagFeature : IFeature, IRegistrable
{
    public IEnumerable<IRenderer> Build<TKey>(BaseDataRecordList<TKey> recordList);
}