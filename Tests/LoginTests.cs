using NUnit.Framework;
using SeleniumTests.Pages; // Import thư mục Pages

namespace SeleniumTests.Tests;

// Kế thừa BaseTest để tự động chạy Setup() và TearDown()
public class LoginTests : BaseTest
{
    [Test]
    public void Test_ValidLogin_ShouldSucceed()
    {
        // 1. Arrange: Khởi tạo trang Login
        var loginPage = new LoginPage(driver);

        // 2. Act: Thực hiện các hành vi
        loginPage.GoTo();
        loginPage.LoginAs("tomsmith", "SuperSecretPassword!");

        // 3. Assert: Kiểm tra kết quả
        var actualMessage = loginPage.GetFlashMessage();
        Assert.That(actualMessage, Does.Contain("You logged into a secure area!"));
    }

    [Test]
    public void Test_InvalidLogin_ShouldFail()
    {
        var loginPage = new LoginPage(driver);

        loginPage.GoTo();
        loginPage.LoginAs("wronguser", "wrongpassword");

        // Tận dụng lại hàm GetFlashMessage để test case thất bại
        var actualMessage = loginPage.GetFlashMessage();
        Assert.That(actualMessage, Does.Contain("Your username is invalid!"));
    }
}