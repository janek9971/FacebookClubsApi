using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserModel.Repositories;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
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
            var sw = new Stopwatch();
            
            List<JToken> listJson = new List<JToken>();
            var listJsonString = new StringBuilder();
            var driver = _chromeDriverSetupRepository.SetupChromeDriver();
            var elapsed = new List<long>();

            //var js = String.Format("window.scrollTo({0}, {1})", 0, 1000);
            //driver.ExecuteScript(js);
            sw.Restart();
            driver.Navigate().GoToUrl("https://www.facebook.com/pg/klubremont/events/");
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(1);
          
            goToNewTabAndLoad(driver, "https://www.facebook.com/pg/klub.stodola/events/");
            goToNewTabAndLoad(driver, "https://www.facebook.com/pg/klubhydrozagadka/events/");


         
            //driver.SwitchTo().Window(driver.WindowHandles.First());
            foreach (string defwindow in driver.WindowHandles)
            {
          var title = driver.SwitchTo().Window(defwindow).Title.Split(" ")[1];
        
                _parseWebRepository.ParseWeb(driver, title, ref listJson);
               
            }
            sw.Stop();
            elapsed.Add(sw.ElapsedMilliseconds);

            Console.WriteLine(elapsed);
            driver.Close();
            driver.Quit();
         
     

            //sw.Start();
            //_parseWebRepository.ParseWeb(driver, "remont", ref listJson);
            //sw.Stop();
            //var time1 = sw.ElapsedMilliseconds;

            //sw.Reset();
            //driver.SwitchTo().Window(driver.WindowHandles.Last());

            ////driver.Navigate().GoToUrl("https://www.facebook.com/pg/klub.stodola/events/");

            //sw.Start();
            //_parseWebRepository.ParseWeb(driver, "stodola", ref listJson);
            //sw.Stop();
            //var time2 = sw.ElapsedMilliseconds;

            //Console.WriteLine($"Czas1= {time1}    Czas2= {time2}");

            foreach (var item in listJson)
            {
                listJsonString.Append(item);
            }

            return listJsonString.ToString();
        }

        private void goToNewTabAndLoad(ChromeDriver driver, string url)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl(url);
            var js = $"window.scrollTo({0}, {1500})";
            driver.ExecuteScript(js);
            //Task.Delay(100).Wait();

        }





    }
}
