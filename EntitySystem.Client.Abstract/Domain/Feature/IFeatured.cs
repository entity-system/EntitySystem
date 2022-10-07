namespace EntitySystem.Client.Abstract.Domain.Feature;

public interface IFeatured
{
    Features Features { get; }

    /*void AddFeature<TFeature>(TFeature feature)
        where TFeature : IFeature;

    void AddFeature<TFeature>()
        where TFeature : IFeature, new();

    void RemoveFeature<TFeature>()
        where TFeature : IFeature;

    TFeature GetFeature<TFeature>()
        where TFeature : IFeature;

    bool HasFeature<TFeature>()
        where TFeature : IFeature;

    IEnumerable<TFeature> GetFeatures<TFeature>()
        where TFeature : IFeature;*/
}