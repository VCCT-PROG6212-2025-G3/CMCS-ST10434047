using System.Security.Claims;
using System.Threading.Tasks;
using CMCS.Controllers;
using CMCS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class HomeControllerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
    private readonly Mock<ILogger<HomeController>> _mockLogger;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        // Mocking UserManager
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        // Mocking SignInManager
        _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
            _mockUserManager.Object,
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            null, null, null, null);

        // Mocking ILogger
        _mockLogger = new Mock<ILogger<HomeController>>();

        // Create the controller with the mocked dependencies
        _controller = new HomeController(_mockLogger.Object, _mockSignInManager.Object, _mockUserManager.Object);
    }

    [Fact]
    public async Task Index_UserIsAdmin_RedirectsToAdminDashboard()
    {
        // Arrange
        var user = new ApplicationUser { UserName = "admin@test.com", Id = "1" };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new System.Security.Claims.Claim[]
        {
            new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName),
        }));

        // Set up the controller's User property
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        // Configure mocks to simulate a signed-in Admin user
        _mockSignInManager.Setup(s => s.IsSignedIn(claimsPrincipal)).Returns(true);
        _mockUserManager.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(user);
        _mockUserManager.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);

        // Act
        var result = await _controller.Index();

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal("AdminDashboard", redirectToActionResult.ControllerName);
    }

    [Fact]
    public async Task Index_UserIsLecturer_RedirectsToLecturerApp()
    {
        // Arrange
        var user = new ApplicationUser { UserName = "lecturer@test.com", Id = "2" };
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new System.Security.Claims.Claim[]
        {
            new System.Security.Claims.Claim(ClaimTypes.Name, user.UserName),
        }));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        // Configure mocks to simulate a signed-in Lecturer
        _mockSignInManager.Setup(s => s.IsSignedIn(claimsPrincipal)).Returns(true);
        _mockUserManager.Setup(u => u.GetUserAsync(claimsPrincipal)).ReturnsAsync(user);
        // Important to set up the other roles as false to test the if-else logic
        _mockUserManager.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);
        _mockUserManager.Setup(u => u.IsInRoleAsync(user, "ProgramCoordinator")).ReturnsAsync(false);
        _mockUserManager.Setup(u => u.IsInRoleAsync(user, "Lecturer")).ReturnsAsync(true);

        // Act
        var result = await _controller.Index();

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal("LecturerApp", redirectToActionResult.ControllerName);
    }
}