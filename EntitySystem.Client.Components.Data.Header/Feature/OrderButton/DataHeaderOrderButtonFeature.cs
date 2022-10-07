using System.Collections.Generic;
using System.Linq;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Property;

namespace EntitySystem.Client.Components.Data.Header.Feature.OrderButton;

public class DataHeaderOrderButtonFeature : IDataHeaderOrderButtonFeature
{
    public IEnumerable<IRenderer> Build<TProperty, TEntity, TValue>(BaseDataHeader<TProperty, TEntity, TValue> header) 
        where TProperty : IDataHeaderProperty<TEntity, TValue>
    {
        var variants = new[]
        {
            new { Descending = false, Text = "Order by ascending", Priority = 10 },
            new { Descending = true, Text = "Order by descending", Priority = 20 }
        };

        return variants.Select(v =>
            new Renderer<DataHeaderOrderButtonParameters<TProperty, TEntity, TValue>, DataHeaderOrderButton<TProperty, TEntity, TValue>>(
                new DataHeaderOrderButtonParameters<TProperty, TEntity, TValue>(header, v.Descending, v.Text), v.Priority));
    }
}