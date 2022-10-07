using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature;

public interface IDataRecordInfoTagFeature : IFeature, IRegistrable
{
    IEnumerable<IRenderer> Build<TKey>(BaseDataRecordInfo<TKey> recordInfo);
}