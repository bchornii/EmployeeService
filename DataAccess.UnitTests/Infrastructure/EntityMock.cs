using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Moq;

namespace DataAccess.UnitTests.Infrastructure
{
    public class EntityMock
    {
        public static Mock<DbSet<TEntity>> GetEntityMock<TEntity>(IQueryable<TEntity> data) where TEntity : class
        {
            var mockSetEmployee = new Mock<DbSet<TEntity>>();
            mockSetEmployee.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(e => e.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));
            mockSetEmployee.As<IQueryable<TEntity>>()
                .Setup(e => e.Provider)
                .Returns(new TestDbAsyncQueryProvider<TEntity>(data.Provider));

            mockSetEmployee.As<IQueryable<TEntity>>().Setup(e => e.Expression).Returns(data.Expression);
            mockSetEmployee.As<IQueryable<TEntity>>().Setup(e => e.ElementType).Returns(data.ElementType);
            mockSetEmployee.As<IQueryable<TEntity>>().Setup(e => e.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSetEmployee;
        }
    }
}
