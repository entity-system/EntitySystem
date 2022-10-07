using EntitySystem.Client.Abstract.Components;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Domain.Parameters;
using Microsoft.AspNetCore.Components;

namespace EntitySystem.Client.Abstract.Domain.Renderer;

public class Renderer<TParameters, TComponent> : IRenderer
    where TParameters : IParameters
    where TComponent : BaseRendered<TParameters>
{
    private readonly TParameters _parameters;

    public long Priority { get; set; }

    public string Text { get; }

    public Features Features => _parameters.Features;

    public Renderer(TParameters parameters, long priority = 0, string text = null)
    {
        _parameters = parameters;

        Priority = priority;

        Text = text;
    }

    public RenderFragment Render()
    {
        return builder =>
        {
            builder.OpenComponent<TComponent>(1);

            builder.AddAttribute(2, nameof(BaseRendered<TParameters>.Parameters), _parameters);

            builder.CloseComponent();
        };
    }
}