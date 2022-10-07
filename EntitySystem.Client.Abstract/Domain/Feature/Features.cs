using System.Collections.Generic;
using System.Linq;

namespace EntitySystem.Client.Abstract.Domain.Feature;

    public class Features
    {
        private readonly List<IFeature> _collection = new();

        public void AddFeature<TFeature>(TFeature feature)
            where TFeature : IFeature
        {
            if (feature == null) return;

            _collection.Add(feature);
        }

        public void AddFeature<TFeature>() where TFeature : IFeature, new()
        {
            _collection.Add(new TFeature());
        }

        public void CopyFeaturesTo(Featured target)
        {
            target.Features._collection.AddRange(_collection);
        }

        public void RemoveFeature<TFeature>() where TFeature : IFeature
        {
            _collection.RemoveAll(f => f is TFeature);
        }

        public TFeature GetFeature<TFeature>()
            where TFeature : IFeature
        {
            return _collection.OfType<TFeature>().FirstOrDefault();
        }

        public bool HasFeature<TFeature>() where TFeature : IFeature
        {
            return _collection.OfType<TFeature>().Any();
        }

        public IEnumerable<TFeature> GetFeatures<TFeature>()
            where TFeature : IFeature
        {
            return _collection.OfType<TFeature>();
        }
    }

