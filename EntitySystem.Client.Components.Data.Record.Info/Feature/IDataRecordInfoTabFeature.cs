using System.Collections.Generic;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;

namespace EntitySystem.Client.Components.Data.Record.Info.Feature;

public interface IDataRecordInfoTabFeature : IFeature, IRegistrable
{
    IEnumerable<IRenderer> BuildTab<TKey>(BaseDataRecordInfo<TKey> recordInfo);

    IEnumerable<IRenderer> BuildContent<TKey>(BaseDataRecordInfo<TKey> recordInfo);
}