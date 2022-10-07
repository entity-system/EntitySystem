using System;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Date.Property;

namespace EntitySystem.Client.Components.Data.Input.Date.Factory;

public class DataInputDateFactory : IDataInputDateFactory
{
    public IRenderer Build<TEntity>(BaseDataInput<IDataInputDateProperty<TEntity>, TEntity, DateTime?> input)
    {
        return new Renderer<BaseDataInput<IDataInputDateProperty<TEntity>, TEntity, DateTime?>, DataInputDate<TEntity>>(input);
    }
}