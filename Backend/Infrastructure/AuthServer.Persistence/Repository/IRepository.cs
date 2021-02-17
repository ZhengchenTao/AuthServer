using AuthServer.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Repository
{
    public interface IRepository
    {
        Task<IRepository> BeginTrans();
        Task<int> CommitTrans();
        Task RollbackTrans();

        Task<int> Add<T>(T entity) where T : BaseEntity;
        Task<int> Add<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task<int> Update<T>(T entity) where T : BaseEntity;
        Task<int> Update<T>(IEnumerable<T> entities) where T : BaseEntity;
        Task<int> Remove<T>(Guid id) where T : BaseEntity;
        Task<int> Remove<T>(IEnumerable<Guid> ids) where T : BaseEntity;
        Task<int> Remove<T>(T entity) where T : BaseEntity;
        Task<int> Remove<T>(IEnumerable<T> entities) where T : BaseEntity;

        Task<T> FindEntity<T>(Guid id) where T : BaseEntity;
        Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : BaseEntity;
        Task<IEnumerable<T>> FindList<T>() where T : BaseEntity;
        Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : BaseEntity;

        IQueryable<T> GetQueryable<T>() where T : BaseEntity;
        IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> condition) where T : BaseEntity;
    }
}
