using Xunit;
using Moq;
using CMCS.Controllers;
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class CoordinatorAppControllerTests
{
    // Helper method to create a mock DbSet that supports FindAsync
    private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> sourceList) where T : class
    {
        var queryable = sourceList.AsQueryable();
        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        // This part is crucial for mocking FindAsync
        dbSet.Setup(d => d.FindAsync(It.IsAny<object[]>()))
             .Returns<object[]>(ids => {
                 var id = (int)ids[0];
                 // This logic assumes the entity has an 'Id' property of type int.
                 var entity = sourceList.FirstOrDefault(e => {
                     var prop = e.GetType().GetProperty("Id");
                     if (prop == null) return false;
                     var value = prop.GetValue(e);
                     return value is int && (int)value == id;
                 });
                 return new ValueTask<T>(entity);
             });

        return dbSet;
    }

    [Fact]
    public async Task UpdateStatus_ApprovesPendingClaim_AndSavesChanges()
    {
        // ARRANGE
        // 1. Create a fake claim that is pending
        var claims = new List<Claim>
        {
            new Claim { Id = 1, Status = ClaimStatus.Pending, Description = "Test Claim" }
        };

        var mockClaimsDbSet = CreateMockDbSet(claims);

        var mockContext = new Mock<IDataContext>();
        mockContext.Setup(c => c.Claims).Returns(mockClaimsDbSet.Object);

        var controller = new CoordinatorAppController(mockContext.Object);

        // ACT
        var result = await controller.UpdateStatus(1, ClaimStatus.Approved);

        // ASSERT
        // Verify the claim's status was updated in our fake list
        var updatedClaim = claims.First(c => c.Id == 1);
        Assert.Equal(ClaimStatus.Approved, updatedClaim.Status);

        // Verify that SaveChangesAsync was called exactly once
        mockContext.Verify(c => c.SaveChangesAsync(default(CancellationToken)), Times.Once());

        // Verify it redirects back to the Index page
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }
}

