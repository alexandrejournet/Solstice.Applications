﻿using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using Solstice.Domain.Models;
using Solstice.Infrastructure.Specifications;

namespace Solstice.Applications.Core;

/// <summary>
/// This interface describes an abstraction for a data manipulation service on a given type of CoreModel.
/// It includes methods for CRUD operations, queries for specific entities,
/// counting entities, starting transactions, and more.
/// </summary>
/// <typeparam name="T">The type of the class to be processed by the service.</typeparam>
public interface ICoreService<T> where T : class
{
    #region Create, Update, Delete

    /// <summary>
    /// Add entity to repository
    /// </summary>
    /// <param name="entity">The entity object</param>
    /// <returns></returns>
    Task AddAsync(T entity);
    Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
    Task AddAndSaveAsync(T entity);
    Task AddAndSaveAsync<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Add range of entities to repository
    /// </summary>
    /// <param name="entities">The entities list</param>
    /// <returns></returns>
    Task AddRangeAsync(ICollection<T> entities);
    Task AddRangeAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class;
    Task AddRangeAndSaveAsync(ICollection<T> entities);
    Task AddRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class;

    /// <summary>
    /// Remove entity from repository
    /// </summary>
    /// <param name="entity">The entity object</param>
    /// <returns></returns>
    void Remove(T entity);
    void Remove<TEntity>(TEntity entity) where TEntity : class;
    Task RemoveAndSaveAsync(T entity);
    Task RemoveAndSaveAsync<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Remove range of entities from repository
    /// </summary>
    /// <param name="entities">The entities list</param>
    /// <returns></returns>
    void RemoveRange(ICollection<T> entities);
    void RemoveRange<TEntity>(ICollection<TEntity> entities) where TEntity : class;
    Task RemoveRangeAndSaveAsync(ICollection<T> entities);
    Task RemoveRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class;
    /// <summary>
    /// Save changes in repository
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();
    /// <summary>
    /// Update entity in repository
    /// </summary>
    /// <param name="entity">The entity object</param>
    /// <returns></returns>
    void Update(T entity);
    void Update<TEntity>(TEntity entity) where TEntity : class;
    Task UpdateAndSaveAsync(T entity);
    Task UpdateAndSaveAsync<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Update range of entities in repository
    /// </summary>
    /// <param name="entities">The entities list</param>
    /// <returns></returns>
    void UpdateRange(ICollection<T> entities);
    void UpdateRange<TEntity>(ICollection<TEntity> entities) where TEntity : class;
    Task UpdateRangeAndSaveAsync(ICollection<T> entities);
    Task UpdateRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class;

    #endregion Create, Update, Delete

    #region Actions

    /// <summary>
    /// Checks if any entity in the repository matches the provided expression
    /// </summary>
    /// <param name="where">The expression that describes the condition to match</param>
    /// <returns>True if any entity matches the condition, False otherwise</returns>
    Task<bool> AnyAsyncBy(Expression<Func<T, bool>> where);
    Task<bool> AnyAsyncBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
    Task<bool> AnyAsyncBy<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;
    /// <summary>
    /// Counts all entities in repository
    /// </summary>
    /// <returns>Total count of all entities</returns>
    Task<decimal> CountAllAsync();

    /// <summary>
    /// Counts the total entities that match the provided expression
    /// </summary>
    /// <param name="where">The expression that describes the condition to match</param>
    /// <returns>Total matched entities count</returns>
    Task<decimal> CountAllAsyncBy(Expression<Func<T, bool>> where);
    Task<decimal> CountAllAsyncBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
    Task<T> FindAsync(int id);
    Task<TEntity> FindAsync<TEntity>(int id) where TEntity : class;

    #endregion Actions

    #region Get By

    /// <summary>
    /// Retrieves an entity that matches the specified condition from the repository.
    /// </summary>
    /// <param name="where">An expression representing a condition to be matched by entities in the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the first matching entity or null if no entity matches the condition.</returns>
    Task<T?> GetBy(Expression<Func<T, bool>> where);

    /// <summary>
    /// Retrieves an entity from the repository that meets the criteria specified by the given Solstice specification.
    /// </summary>
    /// <param name="coreSpecifications">The specifications that an entity must meet to be retrieved from the repository.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is the first entity that satisfies the specified
    /// Solstice specifications. If no entity satisfies the specifications, the task result is null.
    /// </returns>
    Task<T?> GetBy(ICoreSpecifications<T> coreSpecifications);

    Task<TEntity?> GetBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
    Task<TEntity?> GetBy<TEntity>(ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;
    Task<TEntity?> GetBy<TEntity>(string query, ICollection<DbParameter> parameters, ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;

    #endregion Get By

    #region Get all

    /// <summary>
    /// Retrieves a collection of entities from the repository that satisfy the specified condition asynchronously.
    /// </summary>
    /// <param name="where">An expression representing a condition to be matched by entities in the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities satisfying the condition
    /// or an empty collection if there are no matches.</returns>
    Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where);

    /// <summary>
    /// Retrieves a collection of all entities from the repository asynchronously.
    /// </summary>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities satisfying The core specifications or an empty collection if no matches.</returns>
    Task<ICollection<T>> GetAllAsync(ICoreSpecifications<T>? coreSpecifications);

    /// <summary>
    /// Retrieves a collection of all entities from the repository asyncronously based on a provided SQL-like query and specifications.
    /// </summary>
    /// <param name="query">A SQL-like query that retrieves entities from the repository.</param>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task results contains a collection od entities satisfying the query and The core specifications or an empty collection if no matches.</returns>
    Task<ICollection<T>> GetAllAsync(string query, ICoreSpecifications<T>? coreSpecifications);

    /// <summary>
    /// Retrieves a collection of all entities from the repository asyncronously based on a provided SQL-like query, parameters and specifications.
    /// </summary>
    /// <param name="query">A SQL-like query that retrieves entities from the repository.</param>
    /// <param name="parameters">A collection of database parameters used in the query</param>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task results contains a collection od entities satisfying the query, parameters and The core specifications or an empty collection if no matches.</returns>
    Task<ICollection<T>> GetAllAsync(string query, ICollection<DbParameter> parameters,
        ICoreSpecifications<T>? coreSpecifications);

    Task<ICollection<TEntity>> GetAllAsync<TEntity>(ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;
    Task<ICollection<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
    Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query) where TEntity : class;
    Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query, ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;
    Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class;
    Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query, ICollection<DbParameter> parameters, ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;

    #endregion Get all

    #region Queryable

    /// <summary>
    /// Retrieves all entities from the repository asynchronously.
    /// </summary>
    /// <returns>A queryable collection of all entities in the repository.</returns>
    IQueryable<T> GetAllQueryable();

    /// <summary>
    /// Retrieves entities that match the specified condition from the repository asynchronously.
    /// </summary>
    /// <param name="where">An expression representing a condition to be matched by entities in the repository.</param>
    /// <returns>A queryable collection of entities matching the condition.</returns>
    IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> where);

    /// <summary>
    /// Retrieves entities that match the specified Solstice specifications from the repository asynchronously.
    /// </summary>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A queryable collection of entities that satisfy The core specifications.</returns>
    IQueryable<T> GetAllQueryable(ICoreSpecifications<T>? coreSpecifications);

    /// <summary>
    /// Retrieves entities from the repository based on a provided SQL-like query and specifications asynchronously.
    /// </summary>
    /// <param name="query">A SQL-like query that retrieves entities from the repository.</param>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A queryable collection of entities satisfying the query and The core specifications.</returns>
    IQueryable<T> GetAllQueryable(string query, ICoreSpecifications<T>? coreSpecifications);

    /// <summary>
    /// Retrieves entities from the repository based on a provided SQL-like query, parameters and specifications asynchronously.
    /// </summary>
    /// <param name="query">A SQL-like query that retrieves entities from the repository.</param>
    /// <param name="parameters">A collection of database parameters used in the query</param>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A queryable collection of entities satisfying the query, parameters and The core specifications.</returns>
    IQueryable<T> GetAllQueryable(string query, ICollection<DbParameter> parameters,
        ICoreSpecifications<T>? coreSpecifications);

    #endregion Queryable

    #region Pageable

    /// <summary>
    /// Retrieves a paginated list of entities from the repository that conforms to the specified page.
    /// </summary>
    /// <typeparam name="T">The type of entities to be retrieved from the repository.</typeparam>
    /// <param name="page">The page number and size of the entities to be retrieved from the repository.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is a collection of entities that meet The core specifications,
    /// paginated based on the given page object. If no entities meet the specifications, the task result is an empty collection.
    /// </returns>
    Task<Paged<T>> GetPagedResult(Page page);
    Task<Paged<T>> GetPagedResult(Page page, ICoreSpecifications<T> coreSpecifications);
    Task<Paged<T>> GetPagedResult(Page page, string query, ICollection<DbParameter> parameters);
    Task<Paged<T>> GetPagedResult(Page page, string query, ICollection<DbParameter> parameters, ICoreSpecifications<T> coreSpecifications);
    Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page) where TEntity : class;
    Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page, ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;
    Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page, string query, ICollection<DbParameter> parameters) where TEntity : class;
    Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page, string query, ICollection<DbParameter> parameters, ICoreSpecifications<TEntity> coreSpecifications) where TEntity : class;
    /// <summary>
    /// Retrieves a collection of entities from the repository that satisfy the specified condition asyncronously, and do paging on them.
    /// </summary>
    /// <param name="page">The page number and size of the entities to be retrieved from the repository.</param>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities satisfying the condition.
    /// or an empty collection if no matches, paged by the given information in Page object.</returns>
    Task<ICollection<T>> PageAllAsync(Page page, ICoreSpecifications<T>? coreSpecifications);

    /// <summary>
    /// Retrieves a queryable collection of entities from the repository that satisfy the specified condition asyncronously, and do paging on them.
    /// </summary>
    /// <param name="page">The page number and size of the entities to be retrieved from the repository.</param>
    /// <param name="coreSpecifications">The specifications that entities must meet to be retrieved from the repository.</param>
    /// <returns>A queryable collection of all entities in the repository that fulfill the condition, paged by the given information in page object.</returns>
    IQueryable<T> PageAllQueryable(Page page, ICoreSpecifications<T>? coreSpecifications);

    #endregion Pageable

    #region Transactions

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is an IDbContextTransaction object
    /// which encapsulates all information about the transaction.
    /// </returns>
    Task<IDbContextTransaction> BeginTransactionAsync();

    #endregion Transactions

    #region Others

    /// <summary>
    /// Retrieves a collection of entities from a specified IQueryable.
    /// </summary>
    /// <param name="query">An IQueryable that retrieves entities from the repository.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities that satisfy the query or an empty collection if no matches.</returns>
    Task<ICollection<TEntity>> GetAllByQueryable<TEntity>(IQueryable<TEntity> query);

    #endregion Others
}