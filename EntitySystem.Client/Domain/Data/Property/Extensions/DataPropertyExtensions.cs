using System;
using System.Linq.Expressions;
using EntitySystem.Client.Domain.Data.Property.Extensions.Settings;
using EntitySystem.Client.Domain.Data.Property.Factory;
using EntitySystem.Client.Domain.Data.Property.Types;
using EntitySystem.Client.Domain.Data.Source;
using EntitySystem.Client.Services;
using EntitySystem.Shared.Domain;

namespace EntitySystem.Client.Domain.Data.Property.Extensions;

public static class DataPropertyExtensions
{
    public static DataSource<TEntity, TService> Identifier<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, long>> selector, Action<DataPropertyLong<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyLong<TEntity>(source, selector);

        property
            .ReadOnlyInput()
            .HideInAddDialog()
            .HideByDeepMaximum();

        options?.Invoke(property);

        return Property(source, property, name);
    }

    public static DataSource<TEntity, TService> Property<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, Guid>> selector, Action<DataPropertyGuid<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyGuid<TEntity>(source, selector);

        options?.Invoke(property);

        return Property(source, property, name);
    }

    public static DataSource<TEntity, TService> Property<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, string>> selector, Action<DataPropertyText<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyText<TEntity>(source, selector);

        options?.Invoke(property);

        return Property(source, property, name);
    }

    public static DataSource<TEntity, TService> Property<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, long>> selector, Action<DataPropertyLong<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyLong<TEntity>(source, selector);

        options?.Invoke(property);

        return Property(source, property, name);
    }

    public static DataSource<TEntity, TService> Property<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, int>> selector, Action<DataPropertyInteger<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyInteger<TEntity>(source, selector);

        options?.Invoke(property);

        return Property(source, property, name);
    }

    public static DataSource<TEntity, TService> Property<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, decimal>> selector, Action<DataPropertyDecimal<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyDecimal<TEntity>(source, selector);

        options?.Invoke(property);

        return Property(source, property, name);
    }

    public static DataSource<TEntity, TService> Property<TEntity, TService>(this DataSource<TEntity, TService> source, string name, Expression<Func<TEntity, DateTime?>> selector, Action<DataPropertyDate<TEntity>> options = null)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        var property = new DataPropertyDate<TEntity>(source, selector);

        options?.Invoke(property);

        return Property(source, property, name);
    }

    /*public static DataSource<FileReference, FileReferenceService> Size(this DataSource<FileReference, FileReferenceService> source, string name, Action<DataPropertyOptions<long>> options = null)
    {
        //var property = new DataPropertyLong<FileReference>(source, f => f.Size);

        return null;//return Property(source, property, name, o => o.ReadOnly().Format(l => l.ToNiceBytes(), s => s.SizeFromNiceBytes()).Other(option));
    }

    public static DataSource<FileReference, FileReferenceService> Upload(this DataSource<FileReference, FileReferenceService> source, string name, Action<DataPropertyOptions<FileReference>> options = null)
    {
        //var file = new DataPropertyUpload(source);

        return null;//return Property(source, file, name, o => o.HideInTable().Other(option));
    }*/

    public static DataSource<TEntity, TService> Property<TEntity, TService, TValue>(DataSource<TEntity, TService> source, DataProperty<TEntity, TValue> property, string name)
        where TEntity : IEntity, new()
        where TService : IEntityService<TEntity>
    {
        property.Name = name;

        var factory = source.RegistrationProvider.GetRegistration<IDataPropertyFactory>();

        factory.Process(property);

        source.AddProperty(property);

        return source;
    }
}