using AuthServer.Entities;
using AuthServer.EntityFrameworkCore;
using AuthServer.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AuthServer.EF.Test
{
    public class RepositoryTest
    {
        private readonly Repository repository;
        private readonly ServerDbContext serverDbContext;
        public RepositoryTest()
        {
            var dbOption = new DbContextOptionsBuilder<ServerDbContext>()
                .UseSqlServer("Server=tcp:mvpwork01,1433;Initial Catalog=AuthServer;Persist Security Info=False;User ID=authadmin;Password=Password1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30")
                .Options;
            serverDbContext = new ServerDbContext(dbOption);
            repository = new Repository(serverDbContext);
        }

        [Fact]
        public async Task AddTest()
        {
            Tenant tenant = new Tenant()
            {
                Name = "测试租户"
            };
            Assert.True(await repository.Add(tenant) > 0);

            Module module = new Module()
            {
                Name = "test",
                DisplayName = "测试模块"
            };
            Assert.True(await repository.Add(module) > 0);
        }

        [Fact]
        public async Task UpdaetTest()
        {
            var tenant = await repository.FindEntity<Tenant>(x => x.Name == "测试租户");
            tenant.Description = "测试1";
            Assert.True(await repository.Update(tenant) > 0);
        }

        [Fact]
        public async Task RemoveTest()
        {
            var tenant = await repository.FindEntity<Tenant>(x => x.Name == "测试租户");
            Assert.True(await repository.Remove(tenant) > 0);

            var module = await repository.FindEntity<Module>(x => x.Name == "测试模块");
            Assert.True(await repository.Remove(module) > 0);
        }

        [Fact]
        public void GetTest()
        {
            var tenants = repository.GetQueryable<Tenant>(x => x.Name == "测试租户");
            Assert.NotEmpty(tenants.ToList());
        }

    }
}
