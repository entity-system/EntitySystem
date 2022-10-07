using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Integer.Parameters;
using EntitySystem.Client.Components.Data.Input.Integer.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Types;

public class DataPropertyInteger<TEntity> : DataProperty<TEntity, int>, IDataInputIntegerProperty<TEntity>
    where TEntity : IEntity
{
    public DataPropertyInteger(IDataSource<TEntity> source, Expression<Func<TEntity, int>> property) : base(source, property)
    {
    }

    public override IRenderer BuildInput(TEntity entity, IDataInputOptions options)
    {
        var parameters = new DataInputIntegerParameters<TEntity>(RegistrationProvider, this, entity, options);

        return BuildInput(parameters);
    }
}