using AuthServer.Entities.BaseEntities;
using AuthServer.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Repository
{
    public class Repository : IRepository
    {
        /// <summary>
        /// DbContext
        /// </summary>
        protected readonly ServerDbContext _dbContext;
        /// <summary>
        /// 事务对象
        /// </summary>
        protected IDbContextTransaction dbContextTransaction;

        public Repository(ServerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region 事务
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public async Task<IRepository> BeginTrans()
        {
            dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
            return this;
        }

        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitTrans()
        {
            try
            {
                int returnValue = await _dbContext.SaveChangesAsync();
                if (dbContextTransaction != null)
                {
                    await dbContextTransaction.CommitAsync();
                }
                return returnValue;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public async Task RollbackTrans()
        {
            await dbContextTransaction.RollbackAsync();
            await dbContextTransaction.DisposeAsync();
        }
        #endregion

        #region 实体操作
        public async Task<int> Add<T>(T entity) where T : BaseEntity
        {
            _dbContext.Add(entity);
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Add<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _dbContext.AddRange(entities);
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Update(entity);
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Update<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _dbContext.UpdateRange(entities);
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Remove<T>(Guid id) where T : BaseEntity
        {
            _dbContext.Remove(_dbContext.Set<T>().Find(id));
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Remove<T>(IEnumerable<Guid> ids) where T : BaseEntity
        {
            _dbContext.Remove(_dbContext.Set<T>().Where(x => ids.Contains(x.Id)));
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Remove<T>(T entity) where T : BaseEntity
        {
            _dbContext.Remove(entity);
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }

        public async Task<int> Remove<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            _dbContext.RemoveRange(entities);
            return dbContextTransaction == null ? await CommitTrans() : 0;
        }
        #endregion

        #region 查询
        public async Task<T> FindEntity<T>(Guid id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> FindEntity<T>(Expression<Func<T, bool>> condition) where T : BaseEntity
        {
            return await _dbContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindList<T>(Expression<Func<T, bool>> condition) where T : BaseEntity
        {
            return await _dbContext.Set<T>().Where(condition).ToListAsync();
        }

        public IQueryable<T> GetQueryable<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> condition) where T : BaseEntity
        {
            return _dbContext.Set<T>().AsQueryable().Where(condition);
        }
        #endregion
    }
}
