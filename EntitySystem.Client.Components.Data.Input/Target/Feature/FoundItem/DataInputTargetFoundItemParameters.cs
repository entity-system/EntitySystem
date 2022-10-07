using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;

namespace EntitySystem.Client.Components.Data.Input.Target.Feature.FoundItem;

public class DataInputTargetFoundItemParameters<TParent, TChild> : Featured, IParameters
{
    public BaseDataInputTarget<TParent, TChild> Target { get; }

    public TChild Entity { get; }

    public DataInputTargetFoundItemParameters(BaseDataInputTarget<TParent, TChild> target, TChild entity)
    {
        Target = target;
        Entity = entity;
    }
}