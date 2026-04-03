using OpenQA.Selenium;

namespace SeleniumTests.Pages;

public class DynamicPageV2 : BasePage
{
    public DynamicPageV2(IWebDriver driver) : base(driver) { }

    private readonly By _button = By.XPath("//button[text()='Start']");
    private readonly By _message = By.Id("finish");

    // 3. Methods: Các hành vi mà user có thể làm trên trang này
    public void GoTo()
    {
        this.driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
    }

    public void Start()
    {
        ClickElement(_button);
    }

    // Hàm lấy thông báo để phục vụ cho việc Assert ở tầng Test
    public string GetMessage()
    {
        return WaitForElementToVisible(_message).Text.Trim();
    }
}