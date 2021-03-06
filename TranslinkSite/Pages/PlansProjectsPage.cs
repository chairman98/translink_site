﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TranslinkSite.Pages
{
    public class PlansProjectsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        private static readonly By PlansProjectsLink = By.XPath("(//a[contains(text(),'Plans & Projects')])[1]"); //desktop
        private static readonly By PlansProjectsMobileTab = By.XPath("(//a[contains(text(),'Plans & Projects')])[2]"); //mobile view
        private static readonly By BurnMounGondolaLink = By.CssSelector("a[title=\"Link to 'Burnaby Mountain Gondola' page on this site\"]");

        //methods
        public PlansProjectsPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        
        public void ClickPlansProjectsLink()
        {
            if (driver.FindElement(HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HamburgerMenuButton).Click();
                driver.FindElement(PlansProjectsMobileTab).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(HamburgerMenuButton).Click(); // close ham menu
                return;
            }
            
            else
            {
                driver.FindElement(PlansProjectsLink).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }

        public void ClickBurnGondoLink()
        {
            //driver.FindElement(BurnMounGondolaLink).Click();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(BurnMounGondolaLink));
        }
    }
}
