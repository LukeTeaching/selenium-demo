using OpenQA.Selenium;

namespace SeleniumTests.Pages;

public class LoginPage
{
    private readonly IWebDriver _driver;

    // 1. Constructor: Nhận driver từ lớp Test truyền vào
    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    // 2. Locators: Định vị các phần tử UI (Để private để đóng gói tính năng)
    private readonly By _usernameInput = By.Id("username");
    private readonly By _passwordInput = By.Id("password");
    private readonly By _loginButton = By.CssSelector("button[type='submit']");
    private readonly By _flashMessage = By.Id("flash");

    // 3. Methods: Các hành vi mà user có thể làm trên trang này
    public void GoTo()
    {
        _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
    }

    // Gom thao tác đăng nhập thành một hàm tiện ích
    public void LoginAs(string username, string password)
    {
        _driver.FindElement(_usernameInput).SendKeys(username);
        _driver.FindElement(_passwordInput).SendKeys(password);
        _driver.FindElement(_loginButton).Click();
    }

    // Hàm lấy thông báo để phục vụ cho việc Assert ở tầng Test
    public string GetFlashMessage()
    {
        return _driver.FindElement(_flashMessage).Text;
    }
}