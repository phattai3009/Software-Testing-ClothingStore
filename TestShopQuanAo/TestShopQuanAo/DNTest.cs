// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class DNTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  [SetUp]
  public void SetUp() {
        driver = new ChromeDriver(@"C:\Users\PhatTai\Desktop\Nhom07_KiemChungWebsiteBanQuanAo\Nhom07_KiemChungWebsiteBanQuanAo\TestShopQuanAo");
        js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<string, object>();
  }
  [TearDown]
  public void TearDown() {
    driver.Quit();
  }
  [Test]
  public void dN() {
    driver.Navigate().GoToUrl("http://localhost:27660/");
    driver.Manage().Window.Size = new System.Drawing.Size(1366, 728);
    driver.FindElement(By.CssSelector(".header__top__links:nth-child(1) > a")).Click();
    driver.FindElement(By.Name("username")).SendKeys("TK001");
    driver.FindElement(By.Name("pw")).SendKeys("123");
    driver.FindElement(By.Id("btnSignin")).Click();
    driver.FindElement(By.CssSelector(".header__top__links:nth-child(2) > a")).Click();
  }
}