using NUnit.Framework;
using SeleniumTests.Pages; // Import thư mục Pages

namespace SeleniumTests.Tests;

// Kế thừa BaseTest để tự động chạy Setup() và TearDown()
public class DynamicTests : BaseTest
{
    [Test]
    public void Test_ValidMessage_Implicit()
    {
        var dynamicPage = new DynamicPage(driver);

        // 2. Act: Thực hiện các hành vi
        dynamicPage.GoTo();
        dynamicPage.Start();

        // 3. Assert: Kiểm tra kết quả
        var actualMessage = dynamicPage.GetMessage();
        Assert.That(actualMessage, Does.Contain("Hello World!"));
    }
}