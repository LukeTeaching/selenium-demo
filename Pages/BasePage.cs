using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

public class BasePage
{
    protected IWebDriver driver;
    protected WebDriverWait wait;

    // Constructor
    public BasePage(IWebDriver driver)
    {
        this.driver = driver;
        // Khởi tạo WebDriverWait dùng chung cho mọi Page, timeout 15s
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    // --- CÁC HÀM WRAPPER TÍCH HỢP SẴN EXPLICIT WAIT ---

    protected void ClickElement(By locator)
    {
        // Đợi đến khi click được thì mới click
        IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        element.Click();
    }

    protected void EnterText(By locator, string text)
    {
        // Đợi đến khi hiển thị thì mới xóa chữ cũ và nhập chữ mới
        IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
        element.Clear();
        element.SendKeys(text);
    }
    
    // Hàm đợi một Element biến mất (vd: Loading icon)
    protected void WaitForElementToDisappear(By locator)
    {
        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
    }
    
    // Hàm đợi một Element hiển thị
    protected IWebElement WaitForElementToVisible(By locator)
    {
        return wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }
}