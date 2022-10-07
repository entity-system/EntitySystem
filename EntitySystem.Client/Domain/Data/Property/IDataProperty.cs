using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Components.Data.Entity.Dialog.Property;
using EntitySystem.Client.Components.Data.Record.Info.Property;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Domain.Data.List;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Shared.Domain;

/*using EntitySystem.Client.Components.DataHeaders.Builder;
using EntitySystem.Client.Components.DataHeaders.Parameters;
using EntitySystem.Client.Components.DataInputs.Builder;
using EntitySystem.Client.Components.DataInputs.Parameters;*/

namespace EntitySystem.Client.Domain.Data.Property;

public interface IDataProperty<TEntity, TValue> : IDataProperty<TEntity>
    where TEntity : IEntity
{
}

public interface IDataProperty<TEntity> : IDataProperty
    where TEntity : IEntity
{
    IDataProperty<TMiddle> Compose<TMiddle>(IDataSource<TMiddle> source, Expression<Func<TMiddle, TEntity>> expression) where TMiddle : IEntity;

    IDataValue GetValue<TKey>(DataObject<TKey, TEntity> @object) where TKey : IEntity;

    Task OnAfterSaveOrUpdateAsync(TEntity entity);

    string ToString(TEntity entity);

    /*IDataInputRenderer BuildInput(TEntity entity, IDataInputOptions option);*/

    IDataRecordInfoProperty<TEntity> GetRecordInfoProperty();

    IDataEntityDialogProperty<TEntity> GetEntityDialogProperty();
}

public interface IDataProperty : IFeatured
{
    string Name { get; set; }

    Type EntityType { get; }

    Type ValueType { get; }

    int JoinDeep { get; set; }

    int TargetDeep{ get; set; }

    void Reset();

    IDataEntityDialogProperty GetDialogProperty();

    /*IDataHeaderBuilder BuildHeader(IDataHeaderOptions option);*/
    IDataRecordListProperty GetRecordListProperty();
    bool IsInRelationWith(Type type);

    IDataSource GetDataSource();
}