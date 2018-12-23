using System;
using System.Collections.Generic;
using System.Text;
using ParserModel.Repositories;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;

namespace Parser.Repositories
{
   public class ParserRootRepository : IParserRootRepository
   {
       private readonly IChromeDriverSetupRepository _chromeDriverSetupRepository;
       private readonly IParseWebRepository _parseWebRepository;

        public ParserRootRepository(IChromeDriverSetupRepository chromeDriverSetupRepository,
            IParseWebRepository parseWebRepository)
        {
            _chromeDriverSetupRepository = chromeDriverSetupRepository;
            _parseWebRepository = parseWebRepository;
        }

        public string Parse()
        {
            List<JToken> listJson = new List<JToken>();
            var listJsonString = new StringBuilder();
            using (var driver = new ChromeDriver(@"C:\Users\JANEK\source\repos\FacebookClubsApi\packages\Selenium.WebDriver.ChromeDriver.2.45.0\driver\win32", _chromeDriverSetupRepository.SetupChromeDriver(), TimeSpan.FromSeconds(180)))
            {
                driver.Navigate().GoToUrl("https://www.facebook.com/pg/klubhydrozagadka/events/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);

                _parseWebRepository.ParseWeb(driver, "hydro", ref listJson);

            }

            foreach (var item in listJson)
            {
                listJsonString.Append(item);
            }

            return listJsonString.ToString();
        }
    }
}
