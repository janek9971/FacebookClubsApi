using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ParserModel.Repositories;
using static System.Int32;
using static ParserModel.Models.DictionaryMonths;
using ParserModel.Models;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;

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
            day = splitted[0];
            month = splitted[1];
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

        public List<Club> ItemClubs(ChromeDriver driver, int divCount)
        {
            var sw = new Stopwatch();
            var objects = new List<Club>();
            var xpathStringPart1 = "//*[@id='upcoming_events_card']/div/div[";
            var xPathStringPart2 = "/table/tbody/tr/td[";
            for (var i = 0; i < divCount; i++)
            {
                if (i == 0 || i == 1)
                {
                    continue;
                }

                sw.Start();

                var dateEvent = driver.FindElement(By.XPath($"{xpathStringPart1}{i}]{xPathStringPart2}1]/span/span[2]"))
                                    .Text
                                + " "
                                + driver.FindElement(
                                    By.XPath($"{xpathStringPart1}{i}]{xPathStringPart2}1]/span/span[1]")).Text;

                var titleEvent = driver.FindElement(By.XPath($"{xpathStringPart1}{i}]{xPathStringPart2}2]/div/div[1]"))
                    .Text;

                var info = driver.FindElement(By.XPath($"{xpathStringPart1}{i}]{xPathStringPart2}2]/div/div[2]")).Text;
                var timeEvent = driver
                    .FindElement(By.XPath($"{xpathStringPart1}{i}]{xPathStringPart2}2]/div/div[2]/span[1]")).Text;
                var subStrTrimmedDateEvent = info.Replace(timeEvent, "");
                var guestsEvent =
                    subStrTrimmedDateEvent.Substring(subStrTrimmedDateEvent.IndexOfAny("0123456789".ToCharArray()));

                var localizationEvent = driver
                    .FindElement(By.XPath($"  {xpathStringPart1}{i}]{xPathStringPart2}3]/div/div[1]")).Text;
                sw.Stop();

                ParseToDate(dateEvent, timeEvent, out DateTime dateStart, out DateTime dateEnd);
                objects.Add(new Club
                {
                    DateStart = dateStart,
                    DateEnd = dateEnd,
                    Title = titleEvent,
                    Guests = new string(guestsEvent.Where(char.IsDigit).ToArray()),
                    Localization = localizationEvent
                });
                //Console.WriteLine(_driver.FindElement(By.XPath($"//*[@id='upcoming_events_card']/div/div[{i}]/table/tbody/tr/td[2]/div/div[2]/span[1]")).Text);

                //var x =   ParseToDate(dateEvent, timeEvent);
            }

            return objects;
        }


    }
}
