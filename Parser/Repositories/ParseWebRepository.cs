using System;
using System.Collections.Generic;
using System.Text;
using ParserModel.Repositories;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using  Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;

namespace Parser.Repositories
{
   public class ParseWebRepository : IParseWebRepository
   {
       private readonly IParseUtilitiesRepository _parseUtilitiesRepository;
       public ParseWebRepository(IParseUtilitiesRepository parseUtilitiesRepository)
        {
            _parseUtilitiesRepository = parseUtilitiesRepository;
        }

        public void ParseWeb(ChromeDriver driver, string path, ref List<JToken> listJson)
        {
           
            Console.WriteLine(path);
            //var x = false;
            var errorIterate = 0;
            var time = 1;
            int divCount;
            var list = new List<int>();
            while (true)
            {
                //Console.WriteLine(time);
                try
                {
                    Console.WriteLine(time);

                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(time);
                    var upcomingEventsIsLoaded =
                        driver.FindElement(By.XPath(" //*[@id='upcoming_events_card']/div/div[2]")).Displayed;

                    if (!upcomingEventsIsLoaded) continue;
                    //var result = default(System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>);
                    var iterator = 2;

                    try
                    {
                        while (true)
                        {
                            var trySource =
                                driver.FindElement(By.XPath($"//*[@id='upcoming_events_card']/div/div[{iterator}]"));
                            if (iterator % 2 == 0)
                            {
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
                            }

                            driver.ExecuteScript("arguments[0].scrollIntoView(true);", trySource);

                            iterator++;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        errorIterate++;
                        list.Add(iterator);
                        Console.WriteLine(list.Count);
                        foreach (var xex in list)
                        {
                            Console.WriteLine("listJson=" + xex);
                        }

                        //Console.WriteLine("error= "+errorIterate + "iterator= " + iterator);
                        if ((errorIterate < 3 && list.Count <= 3) || list[list.Count - 1] != iterator ||
                            list[list.Count - 2] != iterator) continue;
                        divCount = iterator;
                        break;
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine(ex);
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine(ex);

                    time += 1;
                }
            }

            var sw = new Stopwatch();

            if (divCount > 0)
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                };
                JObject jsonClubs = new JObject();
                sw.Start();
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                };
                jsonClubs[$"Club{path}"] = JToken.FromObject(_parseUtilitiesRepository.ItemClubs(driver, divCount));
                sw.Stop();
                File.WriteAllText($@"\Users\JANEK\Desktop\Strona\helloworld{path}Lista.txt", jsonClubs.ToString());

                listJson.Add(jsonClubs);

                Console.WriteLine("ParsowanieDanychCzas= " + sw.ElapsedMilliseconds);
            }
        }



    }
}
