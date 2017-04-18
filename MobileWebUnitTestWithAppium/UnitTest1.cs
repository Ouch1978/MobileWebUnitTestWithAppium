using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium;

namespace MobileWebUnitTestWithAppium
{
    [TestClass]
    public class UnitTest1
    {
        //宣告 Appium Driver，並指定使用 Android Element
        AppiumDriver<AndroidElement> _driver;

        [TestMethod]
        public void TestSearchOuchMvpProfile()
        {
            //定義相容性
            DesiredCapabilities desiredCapabilities = new DesiredCapabilities();

            //指定平台為安卓
            desiredCapabilities.SetCapability( MobileCapabilityType.PlatformName , MobilePlatform.Android );

            //指定使用的平台版本為 4.4.2
            desiredCapabilities.SetCapability( MobileCapabilityType.PlatformVersion , "4.4.2" );

            //指定裝置名稱，裝置名稱可以透過在 Tools -> Android Adb Command Prompt... 中輸入 adb devices -l 取得
            desiredCapabilities.SetCapability( MobileCapabilityType.DeviceName , "generic_x86" );

            //指定瀏覽器名稱為 Browser(也可以使用 Chrome，指定為 Browser 則會使用預設的瀏覽器)
            desiredCapabilities.SetCapability( MobileCapabilityType.BrowserName , "Browser" );

            //指定不開啟任何 App(有的範例並沒有這行，但是我如果把這行拿掉的話會出錯)
            desiredCapabilities.SetCapability( MobileCapabilityType.App , null );

            //建立 AppiumDriver 的 Instance ，並指定 Appium Server 的路徑
            _driver = new AndroidDriver<AndroidElement>( new Uri( "http://127.0.0.1:4723/wd/hub" ) , desiredCapabilities );

            //指定瀏覽器開啟網址
            _driver.Navigate().GoToUrl( "https://mvp.microsoft.com/zh-tw/" );

            //找出關鍵字搜尋框
            AndroidElement searchBox = _driver.FindElementByName( "kw" );

            //在搜尋框中輸入文字
            searchBox.SendKeys( "Ouch Liu" );

            //按下 Enter
            searchBox.SendKeys( Keys.Enter );

            //找出內文為指定文字的超連結
            AndroidElement profileLink = _driver.FindElementByLinkText( "Ouch Liu" );

            //按下超連結
            profileLink.Click();

            //取出 class 為 title 物件中的文字
            string titleText = _driver.FindElementByClassName( "title" ).Text;

            //定義預期值
            string expectedTitle = "Ouch Liu (劉耀群)";

            //比對實際值與預期值是否相同
            Assert.AreEqual( expectedTitle , titleText );
        }
    }
}
