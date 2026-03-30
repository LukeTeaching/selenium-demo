using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Utilities;

namespace SeleniumTests;

public class LoginTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        // Lấy tên của Test Case hiện tại đang chạy trong NUnit
        string testName = TestContext.CurrentContext.Test.Name;

        // Tạo một mục (node) mới trong file báo cáo HTML
        ReportManager.CreateTest(testName);
        ReportManager.CurrentTest.Log(Status.Info, "Bắt đầu khởi tạo Trình duyệt Chrome");

        // Khởi tạo trình duyệt Chrome. 
        // Nhờ Selenium Manager, thư viện sẽ TỰ ĐỘNG tìm và tải ChromeDriver phù hợp.
        driver = new ChromeDriver();

        // Cấu hình phóng to cửa sổ
        driver.Manage().Window.Maximize();
    }

    [Test]
    public void Test_ValidLogin_V1()
    {
        // 1. Điều hướng đến trang đăng nhập
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        // 2. Tương tác với các phần tử
        driver.FindElement(By.Id("username")).SendKeys("tomsmith");
        driver.FindElement(By.Name("password")).SendKeys("SuperSecretPassword!");
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        // 3. Xác minh kết quả (Assert)
        IWebElement flashMessage = driver.FindElement(By.Id("flash"));
        Assert.That(flashMessage.Text, Does.Contain("You logged into a secure area!"));
    }

    [Test]
    public void Test_ValidLogin_V2()
    {
        // 1. Điều hướng đến trang đăng nhập
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        // 2. Tương tác với các phần tử
        driver.FindElement(By.Id("username")).SendKeys("tomsmithasw");
        driver.FindElement(By.Name("password")).SendKeys("SuperSecretPassword!");
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        // 3. Xác minh kết quả (Assert)
        IWebElement flashMessage = driver.FindElement(By.Id("flash"));
        Assert.That(flashMessage.Text, Does.Contain("You logged into a secure area!"));
    }

    //[Test]
    //public void Test_InValidLogin_WithWrongPassword() { }

    //[Test]
    //public void Test_InValidLogin_WithWrongUserName() { }

    [TearDown]
    public void TearDown()
    {
        // 1. Lấy kết quả chạy test từ NUnit (Pass, Fail, hay Skip)
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var errorMessage = TestContext.CurrentContext.Result.Message;

        // 2. Ghi log tương ứng vào ExtentReports
        if (status == TestStatus.Failed)
        {
            ReportManager.CurrentTest.Log(Status.Fail, $"Test Thất Bại: {errorMessage}");
            try
            {
                // Ép kiểu IWebDriver sang ITakesScreenshot để sử dụng chức năng chụp ảnh
                ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
                Screenshot screenshot = screenshotDriver.GetScreenshot();

                // Lấy ảnh dưới dạng chuỗi Base64 (Cực kỳ hữu ích để nhúng thẳng vào file HTML)
                string base64Image = screenshot.AsBase64EncodedString;

                // Đính kèm ảnh vào báo cáo ExtentReports
                ReportManager.CurrentTest.AddScreenCaptureFromBase64String(base64Image, "Ảnh chụp màn hình lúc lỗi");
            }
            catch (Exception ex)
            {
                // Bắt lỗi dự phòng trường hợp trình duyệt crash không thể chụp ảnh
                ReportManager.CurrentTest.Log(Status.Warning, $"Không thể chụp màn hình. Lỗi: {ex.Message}");
            }
        }
        else if (status == TestStatus.Passed)
        {
            ReportManager.CurrentTest.Log(Status.Pass, "Test Thành Công!");
        }
        // LUÔN LUÔN dọn dẹp driver sau khi test xong để tránh treo process Chrome ngầm
        if (driver != null)
        {
            driver.Quit(); // Đóng toàn bộ cửa sổ và kết thúc session
            driver.Dispose(); // Giải phóng tài nguyên
        }
    }
}
