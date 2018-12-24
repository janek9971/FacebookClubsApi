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
using OpenQA.Selenium.Interactions;

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
            int divCount=0;
            var list = new List<int>();
            var newSw = new Stopwatch();
            newSw.Start();
            var flag0 = true;
            while (flag0)
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
                    //try
                    //{
                    var js = String.Format("window.scrollTo({0}, {1})", 0, 1000);
                    driver.ExecuteScript(js);
                    while (true)
                        {
                            IWebElement trySource;
                   
                        try
                            {
                                 trySource =
                                    driver.FindElement(
                                        By.XPath($"//*[@id='upcoming_events_card']/div/div[{iterator}]"));
                            }
                            catch (NoSuchElementException)
                            {
                                driver.ExecuteScript(js);

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
                            flag0 = false;
                            break;
                                
                                //break;
                              
                            }

                            if (iterator % 2 == 0)
                            {
                                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
                            }


                            driver.ExecuteScript("arguments[0].scrollIntoView(true);", trySource);
                            //if (iterator > 5)
                            //{
                            //    divCount = iterator;
                            //    flag0 = false;
                            //    break;
                            //}
                        iterator++;
                        
                        }
                    //}
                    //catch (NoSuchElementException)
                    //{
                    //    errorIterate++;
                    //    list.Add(iterator);
                    //    Console.WriteLine(list.Count);
                    //    foreach (var xex in list)
                    //    {
                    //        Console.WriteLine("listJson=" + xex);
                    //    }

                    //    //Console.WriteLine("error= "+errorIterate + "iterator= " + iterator);
                    //    if ((errorIterate < 3 && list.Count <= 3) || list[list.Count - 1] != iterator ||
                    //        list[list.Count - 2] != iterator) continue;
                    //    divCount = iterator;
                    //    newSw.Stop();
                    //    var parseWebTime = newSw.ElapsedMilliseconds;
                    //    Console.WriteLine();
                    //    break;
                    //}
                    //catch (Exception)
                    //{
                    //    //Console.WriteLine(ex);
                    //}
                }
                catch (Exception)
                {
                    //Console.WriteLine(ex);

                    time += 1;
                }
            }
            newSw.Stop();
            var parseWebTime = newSw.ElapsedMilliseconds;
 
            var sw = new Stopwatch();

            if (divCount > 0)
            {
                sw.Start();
                //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                //{
                //    DateTimeZoneHandling = DateTimeZoneHandling.Local
                //};
                JObject jsonClubs = new JObject();
              
                //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                //{
                //    DateTimeZoneHandling = DateTimeZoneHandling.Local
                //};
                jsonClubs[$"Events{path}"] = JToken.FromObject(_parseUtilitiesRepository.ItemClubs(driver, divCount));
                sw.Stop();
                //File.WriteAllText($@"\Users\JANEK\Desktop\Strona\helloworld{path}Lista.txt", jsonClubs.ToString());

                listJson.Add(jsonClubs);

                Console.WriteLine("ParsowanieDanychCzas= " + sw.ElapsedMilliseconds);
            }
        }



    }
}
