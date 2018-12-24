using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ParserModel.Repositories;

namespace Parser.Repositories
{
    public class ChromeDriverSetupRepository : IChromeDriverSetupRepository
    {
        public ChromeDriver SetupChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
          //  chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("--no-proxy-server");
            chromeOptions.AddArguments("--proxy-server='direct://'");
            chromeOptions.AddArguments("--proxy-bypass-list=*");
            chromeOptions.AddArgument("blink-settings=imagesEnabled=false");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            //chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--lang=pl-PL");
            chromeOptions.AddArgument("--no-sandbox");

            var timespan = TimeSpan.FromMinutes(3);

            var driver =
                new ChromeDriver(
                    @"C:\Users\JANEK\source\repos\FacebookClubsApi\packages\Selenium.WebDriver.ChromeDriver.2.45.0\driver\win32",
                    chromeOptions, TimeSpan.FromSeconds(180));


            return driver;
        }
    }
}