using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Registrations;

namespace EntitySystem.Client.Components.Data.Record.List.Feature;

public interface IDataRecordListStartFeature : IFeature, IRegistrable
{
    public Task OnParametersSetAsync<TEntity>(BaseDataRecordList<TEntity> dialog);
}