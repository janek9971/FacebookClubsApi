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
using System.Threading.Tasks;
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
            var iterator2 = 0;
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
                    var scrollIterate = 1200;
                    var lastEvents = driver.FindElement(By.XPath("//*[@id='past_events_card']/div/div[1]/div"));
                    
                    //*[@id="upcoming_events_card"]/div/div[8]   
                    driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);

                    //*[@id="upcoming_events_card"]/div/div[8]/span
                    //[0].getAttribute("aria-ValueText");
                    var x = lastEvents.Location.Y;
                    driver.ExecuteScript($"window.scrollTo({0}, {x-200})");
               
                    string xd = string.Empty;
                    while (true)
                    {
                        driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);
                        x = lastEvents.Location.Y;
                        driver.ExecuteScript($"window.scrollTo({0}, {x - 200})");
                        if (driver.FindElement(By.Id("upcoming_events_card")).FindElements(By.ClassName("_p6a")).Count == 0 && iterator2>3)
                        {
                            //xd = driver.FindElement(By.Id("upcoming_events_card")).FindElements(By.ClassName("_p6a"))
                            //    .Count.ToString();
                            break;
                        }
                        iterator2++;
                        if (iterator2 > 10)
                        {
                            break;
                        }
                        //*[@id="upcoming_events_card"]/div/div[8]/span

                        driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);
                        x = lastEvents.Location.Y;
                        driver.ExecuteScript($"window.scrollTo({0}, {x - 200})");
                        Task.Delay(75).Wait();



                    }

                    //if (driver.FindElement(By.ClassName("//_p6b img _55ym _55yq _55yo")).Displayed)
                    //{
                    //    Console.WriteLine(driver.FindElement(By.ClassName("//_p6b img _55ym _55yq _55yo")).Text);
                    //}
                    //while (driver.FindElement(By.ClassName("//_p6b img _55ym _55yq _55yo")).Displayed)
                    //{

                    //}

                    //driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);
                    //x = lastEvents.Location.Y;
                    //driver.ExecuteScript($"window.scrollTo({0}, {x - 200})");
                    //driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);
                    //Task.Delay(200).Wait();
                    //driver.ExecuteScript($"window.scrollTo({0}, {x - 200})");
                    //driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);
                    //Task.Delay(200).Wait();

                    //*[@id="upcoming_events_card"]
                    driver.ExecuteScript("arguments[0].scrollIntoView(true);", lastEvents);
                    x = lastEvents.Location.Y;
                    driver.ExecuteScript($"window.scrollTo({0}, {x - 200})");
                    driver.ExecuteScript($"window.scrollTo({0}, {x})");
                    Task.Delay(150).Wait();
                   driver.ExecuteScript($"window.scrollTo({0}, {x-100})");
                   Task.Delay(150).Wait();

                    divCount = driver.FindElements(By.XPath($"//*[@id='upcoming_events_card']/div/div/table")).Count+1;
                    //   divCount = driver.FindElements(By.XPath($"//*[@id='upcoming_events_card']/div/div")).Count;
                    Console.WriteLine(xd);
                    break;
                    //while (true)
                    //    {
                    //        IWebElement trySource;

                    //    try
                    //        {
                    //             trySource =
                    //                driver.FindElement(
                    //                    By.XPath($"//*[@id='upcoming_events_card']/div/div[{iterator}]"));
                    //        }
                    //        catch (NoSuchElementException)
                    //        {
                    //            driver.ExecuteScript(js);

                    //           errorIterate++;
                    //            list.Add(iterator);
                    //            Console.WriteLine(list.Count);
                    //            foreach (var xex in list)
                    //            {
                    //                Console.WriteLine("listJson=" + xex);
                    //            }



                    //            //Console.WriteLine("error= "+errorIterate + "iterator= " + iterator);
                    //            if ((errorIterate < 3 && list.Count <= 3) || list[list.Count - 1] != iterator ||
                    //                list[list.Count - 2] != iterator) continue;

                    //        divCount = iterator;
                    //        flag0 = false;
                    //        break;

                    //            //break;

                    //        }

                    //        if (iterator % 2 == 0)
                    //        {
                    //            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
                    //        }


                    //        driver.ExecuteScript("arguments[0].scrollIntoView(true);", trySource);
                    //        //if (iterator > 5)
                    //        //{
                    //        //    divCount = iterator;
                    //        //    flag0 = false;
                    //        //    break;
                    //        //}
                    //    iterator++;

                    //    }
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
