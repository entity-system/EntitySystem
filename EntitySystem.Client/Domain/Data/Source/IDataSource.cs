using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntitySystem.Client.Abstract.Domain.Feature;
using EntitySystem.Client.Abstract.Providers;
using EntitySystem.Client.Components.Data.Header.Source;
using EntitySystem.Client.Components.Data.Output.Source;
using EntitySystem.Client.Components.Data.Record.Info.Join;
using EntitySystem.Client.Components.Data.Record.Info.Source;
using EntitySystem.Client.Components.Data.Record.List.Condition;
using EntitySystem.Client.Components.Data.Record.List.Order;
using EntitySystem.Client.Components.Data.Record.List.Property;
using EntitySystem.Client.Components.Data.Table.Options;
using EntitySystem.Client.Domain.Data.Condition;
using EntitySystem.Client.Domain.Data.Order;
using EntitySystem.Client.Domain.Data.Pair;
using EntitySystem.Client.Domain.Data.Property;
using EntitySystem.Shared.Domain;
using EntitySystem.Shared.Query;
using Microsoft.AspNetCore.Components;

/*using EntitySystem.Client.Components.DataTables.Builder.DataTableInfo;
using EntitySystem.Client.Components.DataTables.Parameters;*/

namespace EntitySystem.Client.Domain.Data.Source;

public interface IDataSource<TEntity> : IDataSource
    where TEntity : IEntity
{
    List<TEntity> List { get; }

    List<IDataOrder<TEntity>> Orders { get; set; }

    List<DataCondition<TEntity>> Conditions { get; set; }

    Task DeleteAsync(params TEntity[] entities);

    IDataRecordInfoSource<TEntity> GetRecordInfoSource();

    IDataHeaderSource<TEntity> GetHeaderSource();

    IDataOutputSource<TEntity> GetOutputSource();

    IEnumerable<IDataRecordInfoJoin<TEntity>> GetRecordInfoJoins();
}

public interface IDataSource : /*IEnumerable<IDataSource>,*/ IFeatured
{
    IRegistrationProvider RegistrationProvider { get; }

    IDataSource Cloned { get; }

    /*int Limit { get; set; }

    int Offset { get; set; }

    int JoinDeep { get; }*/

    Guid Guid { get; }

    string Name { get; }

    RenderFragment Render(IDataTableOptions options);

    void FillQueryNested(Query query);

    Task ListNestedAsync(Query query);

    IEnumerable<IDataRecordListProperty> GetRecordListProperties();

    IEnumerable<IDataPair> GetPairs();

    IEnumerable<IDataRecordListOrder> GetRecordListOrdersNested();

    IEnumerable<IDataRecordListCondition> GetRecordListConditionsNested();

    IEnumerable<IDataProperty> GetAllProperties();
}

