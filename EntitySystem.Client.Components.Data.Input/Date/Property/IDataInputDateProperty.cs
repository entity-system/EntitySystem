using System;
using EntitySystem.Client.Components.Data.Input.Property;

namespace EntitySystem.Client.Components.Data.Input.Date.Property;

public interface IDataInputDateProperty<in TEntity> : IDataInputProperty<TEntity, DateTime?>
{
}