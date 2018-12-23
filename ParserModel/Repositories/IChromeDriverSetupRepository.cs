using OpenQA.Selenium.Chrome;

namespace ParserModel.Repositories
{
   public interface IChromeDriverSetupRepository
   {
        ChromeOptions SetupChromeDriver();
   }
}
