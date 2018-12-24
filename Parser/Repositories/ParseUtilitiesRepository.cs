using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ParserModel.Repositories;
using static System.Int32;
using static ParserModel.Models.DictionaryMonths;
using ParserModel.Entities;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Threading;

namespace Parser.Repositories
{
   public class ParseUtilitiesRepository : IParseUtilitiesRepository
    {
        public void ParseToDate(string dayAndMonth, string time, out DateTime dateStart, out DateTime dateEnd)
        {
            string day;
            string month;
            int monthInt;
            dateStart = default(DateTime);
            dateEnd = default(DateTime);
            var yearNow = DateTime.Now.Year;
            var monthNow = DateTime.Now.Month;
            if (time.Contains('–'))
            {
                var splittedDate = (time.Split('–')
                    .Select(s => s.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)));
                var enumerable = splittedDate as string[][] ?? splittedDate.ToArray();


                day = enumerable.ElementAt(0)[0];
                monthInt = GetValueFromDateString(enumerable[0][1].ToUpper());
                var year = enumerable.ElementAt(0).Length == 3 ? enumerable.ElementAt(0)[2] :
                    (monthInt < monthNow) ? (yearNow + 1).ToString() : yearNow.ToString();
                dateStart = new DateTime(Parse(year), monthInt, Parse(day));
                day = enumerable.ElementAt(1)[0];
                monthInt = GetValueFromDateString(enumerable.ElementAt(1)[1].ToUpper());
                year = enumerable.ElementAt(1).Length == 3 ? enumerable.ElementAt(0)[2] :
                    (monthInt < monthNow) ? (yearNow + 1).ToString() : yearNow.ToString();
                dateEnd = new DateTime(Parse(year), monthInt, Parse(day));
                return;
            }


            var splitted = dayAndMonth.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            day = splitted[1];
            month = splitted[0];
            monthInt = GetValueFromDateString(month);


            var hours = GetTime(time)["hour"];

            var minutes = GetTime(time)["minutes"];
            if (monthInt < monthNow)
            {
                yearNow = yearNow + 1;
            }


            var resultDate = new DateTime(yearNow, monthInt, Parse(day), hours, minutes, 0, DateTimeKind.Local);
            dateStart = resultDate;
            dateEnd = new DateTime(yearNow, monthInt, Parse(day));
        }
        public int GetValueFromDateString(string obj)
        {
         
            var thisMonth = 0;
            foreach (var monthDict in Months)
            {
                if (obj.Contains(monthDict.Key))
                {
                    thisMonth = monthDict.Value;
                }
            }

            return thisMonth;
        }
        public Dictionary<string, int> GetTime( string obj)
        {
            var trimmed = obj.Substring(obj.IndexOfAny("0123456789".ToCharArray()));
            var trimEnd = trimmed.Substring(trimmed.IndexOfAny("0123456789".ToCharArray()), 5);
            var hour = trimEnd.Substring(0, trimEnd.IndexOf(":", StringComparison.Ordinal));
            var minutes = trimEnd.Substring(trimEnd.IndexOf(":", StringComparison.Ordinal) + 1);

            //int[] timeArray = {Parse(hour), Parse(minutes)};
            var timeDictionary = new Dictionary<string, int>
            {
                {"hour", Parse(hour)},
                {"minutes", Parse(minutes)}
            };
            return timeDictionary;
        }

        public List<Events> ItemClubs(ChromeDriver driver, int divCount)
        {
            var sw = new Stopwatch();
            var sw2 = new Stopwatch();;
            var objects = new List<Events>();
            var xpathStringPart1 = "//*[@id='upcoming_events_card']/div/div[";
            var xPathStringPart2 = "/table/tbody/tr";
            var strB = new List<string>();
            //divCount += 1;
            var elapse = 0L;

            for (var i = 2; ;i++)
            {
                //if (i == 0 || i == 1)
                //{
                //    continue;
                //}
                //if (divCount > 10 && divCount % 2 == 0)
                //{
                //    //Thread.Sleep(20);
                //    //divCount = driver.FindElements(By.XPath($"//*[@id='upcoming_events_card']/div/div/table")).Count +
                //    //           2;
                //}

                string tableRow;
             
                try
                {
                    sw.Restart();
                    //var element = driver
                    //    .FindElement(By.XPath($"{xpathStringPart1}{i}]{xPathStringPart2}"));

                    var element = driver
                        .FindElement(By.CssSelector($"#upcoming_events_card > div:nth-child(1) > div:nth-child({i}) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1)"));
                    //# upcoming_events_card > div:nth-child(1) > div:nth-child(7) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1)
                    tableRow = element.Text;
          
                   if (i > 10 && i % 2 == 0)
                   {
                       driver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                   }
               
                    sw.Stop();
                   elapse += sw.ElapsedMilliseconds;
                }
                catch (NoSuchElementException)
                {
                    break;
                }
               
                var tableRowSplit = tableRow.Split("\r\n");
                var dateEvent = tableRowSplit[0] + " " + tableRowSplit[1];
                var titleEvent = tableRowSplit[2];
                var info = tableRowSplit[3].Split("·");
                var timeEvent = info[0];
                var guestsEvent = info[1].Substring(info[1].IndexOfAny("0123456789".ToCharArray()));
                var localizationEvent = tableRowSplit[4];
            

                ParseToDate(dateEvent, timeEvent, out DateTime dateStart, out DateTime dateEnd);
                objects.Add(new Events
                {
                    DateStart = dateStart,
                    DateEnd = dateEnd,
                    Title = titleEvent,
                    Guests = Parse(new string(guestsEvent.Where(char.IsDigit).ToArray())),
                    Localization = localizationEvent
                });
             
            }
       
                
            Console.WriteLine(strB);
       
            Console.WriteLine(elapse);

            return objects;
        }


    }
}
