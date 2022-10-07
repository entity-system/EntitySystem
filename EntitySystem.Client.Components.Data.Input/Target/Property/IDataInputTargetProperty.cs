using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Client.Components.Data.Input.Property;
using EntitySystem.Client.Components.Data.Input.Target.Source;

namespace EntitySystem.Client.Components.Data.Input.Target.Property;

public interface IDataInputTargetProperty<TParent, TChild> : IDataInputProperty<TParent, TChild>
{
    IDataInputTargetSource<TChild> GetTargetChildSource();

    Expression<Func<TParent, TChild>> Expression { get; }
        
    Expression<Func<TChild, string>> ChildNameExpression { get; set; }

    Func<TChild, string> ChildNameGetter { get; set; }

    Task<TChild> CreateChildAsync(TParent parent);

    Task EditChildAsync(TParent parent);
}