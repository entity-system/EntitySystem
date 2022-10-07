using System;
using System.Linq.Expressions;
using EntitySystem.Client.Abstract.Domain.Renderer;
using EntitySystem.Client.Components.Data.Header.Options;
using EntitySystem.Client.Components.Data.Input.Options;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Abstract.Extensions;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property;

public class DataPropertyComposer<TSource, TTarget, TValue> : DataProperty<TSource, TValue>
    where TTarget : IEntity
    where TSource : IEntity
{
    public Expression<Func<TSource, TTarget>> Intermediate { get; }

    public Func<TSource, TTarget> IntermediateCompiled { get; }

    public DataProperty<TTarget, TValue> Final { get; }

    public override Type EntityType { get; }

    public override Type ValueType { get; }

    public DataPropertyComposer(IDataSource<TSource> source, Expression<Func<TSource, TTarget>> intermediate, DataProperty<TTarget, TValue> final) : base(source, intermediate.Compose(final.Expression))
    {
        Intermediate = intermediate;

        IntermediateCompiled = intermediate.Compile();

        Final = final;

        Priority = final.Priority;

        Name = final.Name;

        JoinDeep = final.JoinDeep;

        TargetDeep = final.TargetDeep;

        EntityType = final.EntityType;

        ValueType = final.ValueType;

        final.Features.CopyFeaturesTo(this);
    }

    public override IRenderer BuildHeader<TOrigin>(IDataSource<TOrigin> originSource, Expression<Func<TOrigin, TSource>> originExpression, IDataHeaderOptions option)
    {
        var composed = originExpression.Compose(Intermediate);

        return Final.BuildHeader(originSource, composed, option);
    }

    public override IRenderer BuildInput(TSource source, IDataInputOptions option)
    {
        var target = IntermediateCompiled(source);

        return Final.BuildInput(target, option);
    }
}