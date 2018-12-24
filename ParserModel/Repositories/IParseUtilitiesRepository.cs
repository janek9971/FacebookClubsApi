using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using ParserModel.Entities;

namespace ParserModel.Repositories
{
   public  interface IParseUtilitiesRepository
   {
       void ParseToDate(string dayAndMonth, string time, out DateTime dateStart, out DateTime dateEnd);
       int GetValueFromDateString(string obj);
       Dictionary<string, int> GetTime(string obj);
       List<Events> ItemClubs(ChromeDriver driver, int divCount);
   }
}
