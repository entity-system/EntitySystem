using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Options;
using EntitySystem.Client.Components.Data.Header.Origin;
using EntitySystem.Client.Components.Data.Header.Text.Factory;
using EntitySystem.Client.Components.Data.Header.Text.Parameters;
using EntitySystem.Client.Components.Data.Header.Text.Property;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Components.Data.Input.Text.Parameters;
using EntitySystem.Client.Components.Data.Input.Text.Property;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Types;

public class DataPropertyText<TEntity> : DataProperty<TEntity, string>, IDataHeaderTextProperty<TEntity>, IDataInputTextProperty<TEntity>
    where TEntity : IEntity
{
    public DataPropertyText(IDataSource<TEntity> source, Expression<Func<TEntity, string>> property) : base(source, property)
    {
    }

    protected override IRenderer BuildHeader(IDataHeaderOptions option, IDataHeaderOrigin<TEntity> origin)
    {
        var parameters = new DataHeaderTextParameters<TEntity>(this, option, origin);

        var factory = RegistrationProvider.GetRegistration<IDataHeaderTextFactory>();

        return factory.Build(parameters);
    }

    public override IRenderer BuildInput(TEntity entity, IDataInputOptions options)
    {
        var parameters = new DataInputTextParameters<TEntity>(RegistrationProvider, this, entity, options);

        return BuildInput(parameters);
    }
}