using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using ParserModel.Models;

namespace ParserModel.Repositories
{
   public  interface IParseUtilitiesRepository
   {
       void ParseToDate(string dayAndMonth, string time, out DateTime dateStart, out DateTime dateEnd);
       int GetValueFromDateString(string obj);
       Dictionary<string, int> GetTime(string obj);
       List<Club> ItemClubs(ChromeDriver driver, int divCount);
   }
}
