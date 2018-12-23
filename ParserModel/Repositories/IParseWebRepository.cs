using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;

namespace ParserModel.Repositories
{
   public interface IParseWebRepository
    {
       void ParseWeb(ChromeDriver driver, string path, ref List<JToken> listJson);

    }
}
