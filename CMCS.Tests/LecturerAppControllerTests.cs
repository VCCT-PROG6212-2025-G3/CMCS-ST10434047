using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CMCS.Controllers;
using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class LecturerAppControllerTests
{
    [Fact]
    public async Task NewClaim_Post_ValidModel_AddsClaimToDatabase()
    {
        // Arrange
        // 1. Mock the IDataContext
        var mockClaimsDbSet = new Mock<DbSet<CMCS.Models.Claim>>();
        var mockContext = new Mock<IDataContext>();
        mockContext.Setup(c => c.Claims).Returns(mockClaimsDbSet.Object);

        // 2. Mock UserManager
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        // 3. Mock IWebHostEnvironment
        var mockHostEnvironment = new Mock<IWebHostEnvironment>();
        mockHostEnvironment.Setup(m => m.WebRootPath).Returns("fake/path");

        // 4. Create the controller with mocks
        var controller = new LecturerAppController(
            mockContext.Object,
            mockUserManager.Object,
            mockHostEnvironment.Object
        );

        // 5. Simulate a logged-in user
        var user = new ApplicationUser { Id = "test-user-id", FirstName = "Test", LastName = "User" };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] { new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id) }));
        controller.ControllerContext.HttpContext = new DefaultHttpContext { User = claimsPrincipal };
        mockUserManager.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(user);

        // 6. Create the input model for the new claim
        var claimInput = new ClaimInputModel
        {
            HoursWorked = 10,
            HourlyRate = 50,
            Description = "Test claim description for unit test."
        };

        // Act
        var result = await controller.NewClaim(claimInput);

        // Assert
        // Verify that the Add method was called on the Claims DbSet exactly once
        mockClaimsDbSet.Verify(db => db.Add(It.IsAny<CMCS.Models.Claim>()), Times.Once());

        // Verify that SaveChangesAsync was called on the context exactly once
        mockContext.Verify(c => c.SaveChangesAsync(default(CancellationToken)), Times.Once());

        // Ensure the controller redirects to the Claims list after success
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Claims", redirectToActionResult.ActionName);
    }
}