using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Decimal.Parameters;
using EntitySystem.Client.Components.Data.Input.Decimal.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Types;

public class DataPropertyDecimal<TEntity> : DataProperty<TEntity, decimal>, IDataInputDecimalProperty<TEntity>
    where TEntity : IEntity
{
    public DataPropertyDecimal(IDataSource<TEntity> source, Expression<Func<TEntity, decimal>> property) : base(source, property)
    {
    }

    public override IRenderer BuildInput(TEntity entity, IDataInputOptions options)
    {
        var parameters = new DataInputDecimalParameters<TEntity>(RegistrationProvider, this, entity, options);

        return BuildInput(parameters);
    }
}