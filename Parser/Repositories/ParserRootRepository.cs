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
            var driver = _chromeDriverSetupRepository.SetupChromeDriver();
                driver.Navigate().GoToUrl("https://www.facebook.com/pg/klubhydrozagadka/events/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);

                _parseWebRepository.ParseWeb(driver, "hydro", ref listJson);

    

            foreach (var item in listJson)
            {
                listJsonString.Append(item);
            }

            return listJsonString.ToString();
        }
    }
}
