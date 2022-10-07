using System;
using EntitySystem.Client.Abstract.Domain.Registrations;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Date.Property;

namespace EntitySystem.Client.Components.Data.Input.Date.Factory;

public interface IDataInputDateFactory : IRegistrable
{
    IRenderer Build<TEntity>(BaseDataInput<IDataInputDateProperty<TEntity>, TEntity, DateTime?> input);
}