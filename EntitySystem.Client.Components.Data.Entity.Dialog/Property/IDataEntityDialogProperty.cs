using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Input.Options;

namespace EntitySystem.Client.Components.Data.Entity.Dialog.Property;

public interface IDataEntityDialogProperty<in TEntity> : IDataEntityDialogProperty
{
    IRenderer BuildInput(TEntity entity, IDataInputOptions option);
}

public interface IDataEntityDialogProperty : IFeatured
{
}