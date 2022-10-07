using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Guid.Parameters;
using EntitySystem.Client.Components.Data.Input.Guid.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Types;

public class DataPropertyGuid<TEntity> : DataProperty<TEntity, Guid>, IDataInputGuidProperty<TEntity>
    where TEntity : IEntity
{
    public DataPropertyGuid(IDataSource<TEntity> source, Expression<Func<TEntity, Guid>> property) : base(source, property)
    {
    }

    public override IRenderer BuildInput(TEntity entity, IDataInputOptions option)
    {
        var parameters = new DataInputGuidParameters<TEntity>(RegistrationProvider, this, entity, option);

        return BuildInput(parameters);
    }
}