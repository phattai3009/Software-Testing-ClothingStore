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
public class TCDNTCTest {
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
  public void tCDNTC() {
    driver.Navigate().GoToUrl("http://localhost:27660/");
    driver.Manage().Window.Size = new System.Drawing.Size(1382, 744);
    driver.FindElement(By.CssSelector(".header__top__links:nth-child(1) > a")).Click();
    driver.FindElement(By.LinkText("If you don\'t have an account, please click here to register!")).Click();
    driver.FindElement(By.CssSelector("form:nth-child(1)")).Click();
    driver.FindElement(By.Name("UserName")).SendKeys("son12");
    driver.FindElement(By.Name("Pass")).SendKeys("son123456");
    driver.FindElement(By.Name("RePass")).SendKeys("son123456");
    driver.FindElement(By.Name("Ten")).SendKeys("Từ Huệ Sơn");
    driver.FindElement(By.Name("NgaySinh")).SendKeys("05/02/2001");
    //driver.FindElement(By.Name("NgaySinh")).SendKeys("05022001");
    driver.FindElement(By.Name("Email")).SendKeys("son@gmail.com");
    driver.FindElement(By.Name("SDT")).SendKeys("0362417182");
    driver.FindElement(By.Name("DiaChi")).SendKeys("BC");
    driver.FindElement(By.CssSelector(".btnSignUp")).Click();
    Assert.That(driver.FindElement(By.CssSelector(".fw-normal")).Text, Is.EqualTo("Sign In"));
  }
    public void tCDNTC(string pUsername, string pPw, string pRpw, string pHoTen, string pNgSinh, string pEmail, string pSDT, string pDiaChi)
    {
        driver.Navigate().GoToUrl("http://localhost:27660/");
        driver.Manage().Window.Size = new System.Drawing.Size(1382, 744);
        driver.FindElement(By.CssSelector(".header__top__links:nth-child(1) > a")).Click();
        driver.FindElement(By.LinkText("If you don\'t have an account, please click here to register!")).Click();
        driver.FindElement(By.CssSelector("form:nth-child(1)")).Click();
        driver.FindElement(By.Name("UserName")).SendKeys(pUsername);
        driver.FindElement(By.Name("Pass")).SendKeys(pPw);
        driver.FindElement(By.Name("RePass")).SendKeys(pRpw);
        driver.FindElement(By.Name("Ten")).SendKeys(pHoTen);
        driver.FindElement(By.Name("NgaySinh")).SendKeys(pNgSinh);
        driver.FindElement(By.Name("Email")).SendKeys(pEmail);
        driver.FindElement(By.Name("SDT")).SendKeys(pSDT);
        driver.FindElement(By.Name("DiaChi")).SendKeys(pDiaChi);
        driver.FindElement(By.CssSelector(".btnSignUp")).Click();
        Thread.Sleep(4000);
        Assert.That(driver.FindElement(By.CssSelector(".fw-normal")).Text, Is.EqualTo("Sign In"));
    }
        
}
