using AuthServer.Entities;
using AuthServer.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.EntityFrameworkCore
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options)
            : base(options)
        { }

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserUserGroup> UserUserGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroupRole> UserGroupRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<APIAction> APIActions { get; set; }
        public DbSet<RoleAPIAction> RoleAPIActions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageAction> PageActions { get; set; }
        public DbSet<RolePageAction> RolePageActions { get; set; }
        public DbSet<RolePage> RolePages { get; set; }
        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entity.ClrType))
                {
                    var parameter = Expression.Parameter(entity.ClrType);
                    var expressionIsDeleted = Expression.Equal(
                        Expression.MakeMemberAccess(parameter, entity.ClrType.GetProperty("IsDeleted")),
                        Expression.Constant(false));

                    builder.Entity(entity.ClrType).HasQueryFilter(Expression.Lambda(expressionIsDeleted, parameter));
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entry.Entity.GetType()))
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            MarkAsDeleted((ISoftDelete)entry.Entity);
                            break;
                        case EntityState.Added:
                            MarkAsNotDeleted((ISoftDelete)entry.Entity);
                            break;
                        default:
                            break;
                    }
                }
                if (entry.Entity is AuditEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            RecordModifiedInfo((AuditEntity)entry.Entity);
                            break;
                        case EntityState.Modified:
                            RecordModifiedInfo((AuditEntity)entry.Entity);
                            break;
                        case EntityState.Added:
                            RecordCreatedInfo((AuditEntity)entry.Entity);
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //TODO:CurrentUserId
        protected Guid CurrentUserId = Guid.Empty;

        private void RecordCreatedInfo<T>(T entity) where T : AuditEntity
        {
            entity.CreatedTime = DateTime.UtcNow;
            entity.CreatedBy = CurrentUserId;
        }

        private void RecordModifiedInfo<T>(T entity) where T : AuditEntity
        {
            entity.ModifiedTime = DateTime.UtcNow;
            entity.ModifiedBy = CurrentUserId;
        }

        private void MarkAsDeleted<T>(T entity) where T : ISoftDelete
        {
            entity.IsDeleted = true;
        }

        private void MarkAsNotDeleted<T>(T entity) where T : ISoftDelete
        {
            entity.IsDeleted = false;
        }
    }
}
