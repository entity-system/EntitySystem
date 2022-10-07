using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Long.Parameters;
using EntitySystem.Client.Components.Data.Input.Long.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Types;

public class DataPropertyLong<TEntity> : DataProperty<TEntity, long>, IDataInputLongProperty<TEntity>
    where TEntity : IEntity
{
    public DataPropertyLong(IDataSource<TEntity> source, Expression<Func<TEntity, long>> property) : base(source, property)
    {
    }

    public override IRenderer BuildInput(TEntity entity, IDataInputOptions options)
    {
        var parameters = new DataInputLongParameters<TEntity>(RegistrationProvider, this, entity, options);

        return BuildInput(parameters);
    }
}