using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests.Tests;

public class BaseTest
{
	// Để protected để các lớp con (Tests) có thể sử dụng biến driver này
	protected IWebDriver driver;

	[SetUp]
	public void Setup()
	{
		driver = new ChromeDriver();
		driver.Manage().Window.Maximize();
	}

	[TearDown]
	public void TearDown()
	{
		if (driver != null)
		{
			driver.Quit();
			driver.Dispose();
		}
	}
}