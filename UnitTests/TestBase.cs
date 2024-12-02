using Microsoft.EntityFrameworkCore;

namespace Application.UnitTest;

public abstract class TestBase
{
    protected Mock<DbSet<TEntity>> CreateMockDbSet<TEntity>(List<TEntity> data) where TEntity : class
    {
        var queryable = data.AsQueryable();
        var mockDbSet = new Mock<DbSet<TEntity>>();
        mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        return mockDbSet;
    }
}