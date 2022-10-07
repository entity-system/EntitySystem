using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Date.Parameters;
using EntitySystem.Client.Components.Data.Input.Date.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Types;

public class DataPropertyDate<TEntity> : DataProperty<TEntity, DateTime?>, IDataInputDateProperty<TEntity>
    where TEntity : IEntity
{
    public DataPropertyDate(IDataSource<TEntity> source, Expression<Func<TEntity, DateTime?>> property) : base(source, property)
    {
    }

    public override IRenderer BuildInput(TEntity entity, IDataInputOptions options)
    {
        var parameters = new DataInputDateParameters<TEntity>(RegistrationProvider, this, entity, options);

        return BuildInput(parameters);
    }
}