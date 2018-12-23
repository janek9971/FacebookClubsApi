using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using ParserModel.Repositories;

namespace Parser.Repositories
{
   public class ChromeDriverSetupRepository : IChromeDriverSetupRepository
    {
        public ChromeOptions SetupChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("--no-proxy-server");
            chromeOptions.AddArguments("--proxy-server='direct://'");
            chromeOptions.AddArguments("--proxy-bypass-list=*");
            chromeOptions.AddArgument("blink-settings=imagesEnabled=false");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--lang=pl-PL");
            chromeOptions.AddArgument("--no-sandbox");
            var timespan = TimeSpan.FromMinutes(3);


            return chromeOptions;
        }
    }
}
