﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.TestCases;

namespace TranslinkSite.Pages
{
    public class HomePageElements
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        //compass card elements
        private static readonly By CompassCardButton = By.LinkText("Visit compasscard.ca");
        // transit alerts
        private static readonly By TransitAlertsButton = By.LinkText("Sign up to receive transit alerts"); 

        public HomePageElements(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        }

        public void CompassCard()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Compass is your key!")), "Compass Article Title is Incorrect");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("TransLink's reloadable fare card that works everywhere on transit.")), "Compass Article Body is Incorrect"); 
            IWebElement CompassCardLink = driver.FindElement(HomePageElements.CompassCardButton);
            //user exception of element not clickable at point (x,y), tried Actions method doesn't work. 
            //Use the below method instead with reference to 
            //https://stackoverflow.com/questions/38923356/element-is-not-clickable-at-point-other-element-would-receive-the-click
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;  
            jse.ExecuteScript("arguments[0].click()", CompassCardLink);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            
            //Because clicking on link opens new tab, driver must switch windows
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Assert.IsTrue(driver.Url.Contains("compasscard"), "Compass Card is Not Displayed");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            
        }

        public void TransitAlerts()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Know before you go!")), "Alerts Article Title is Incorrect");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Create notifications for the transit services that matter most to you. Sign up to receive transit alerts via SMS or email.")), "Alerts Article Body is Incorrect");

            IWebElement SignUpForAlerts = driver.FindElement(HomePageElements.TransitAlertsButton);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", SignUpForAlerts);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Assert.IsTrue(driver.Url.Contains("Profile/Register"));
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Sign Up For Transit Alerts")), "Alerts SignUp Title Is Incorrect");

        }
    }
}
