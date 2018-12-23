using OpenQA.Selenium.Chrome;

namespace ParserModel.Repositories
{
   public interface IChromeDriverSetupRepository
   {
        ChromeDriver SetupChromeDriver();
   }
}
