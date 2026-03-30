using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests;

public class LoginTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        // Khởi tạo trình duyệt Chrome. 
        // Nhờ Selenium Manager, thư viện sẽ TỰ ĐỘNG tìm và tải ChromeDriver phù hợp.
        driver = new ChromeDriver();

        // Cấu hình phóng to cửa sổ
        driver.Manage().Window.Maximize();
    }

    [Test]
    public void Test_ValidLogin_ShouldSucceed()
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

    [TearDown]
    public void TearDown()
    {
        // LUÔN LUÔN dọn dẹp driver sau khi test xong để tránh treo process Chrome ngầm
        if (driver != null)
        {
            driver.Quit(); // Đóng toàn bộ cửa sổ và kết thúc session
            driver.Dispose(); // Giải phóng tài nguyên
        }
    }
}
