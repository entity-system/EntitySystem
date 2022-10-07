using EntitySystem.Client.Abstract.Domain.Feature;
using Microsoft.AspNetCore.Components;

namespace EntitySystem.Client.Abstract.Domain.Renderer;

public interface IRenderer
{
    long Priority { get; }

    string Text { get; }

    Features Features { get; }

    RenderFragment Render();
}