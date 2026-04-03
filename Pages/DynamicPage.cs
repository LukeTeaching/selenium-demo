using OpenQA.Selenium;

namespace SeleniumTests.Pages;

public class DynamicPage
{
    private readonly IWebDriver _driver;

    // 1. Constructor: Nhận driver từ lớp Test truyền vào
    public DynamicPage(IWebDriver driver)
    {
        _driver = driver;
    }

    // 2. Locators: Định vị các phần tử UI (Để private để đóng gói tính năng)    
    private readonly By _button = By.XPath("//button[text()='Start']");
    private readonly By _message = By.Id("finish");

    // 3. Methods: Các hành vi mà user có thể làm trên trang này
    public void GoTo()
    {
        _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
    }

    public void Start()
    {
        _driver.FindElement(_button).Click();
    }

    // Hàm lấy thông báo để phục vụ cho việc Assert ở tầng Test
    public string GetMessage()
    {
        return _driver.FindElement(_message).GetAttribute("textContent").Trim();
    }
}