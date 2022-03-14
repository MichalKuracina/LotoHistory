
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace LotoHistory
{
    public class Selenium
    {
        private EdgeOptions _options;
        private EdgeDriver _driver;
        private WebDriverWait _wait;
        private readonly int _sleep;

        public Selenium()
        {
            _options = new EdgeOptions();
            _options.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
            _driver = new EdgeDriver("C:\\IBOTS\\Selenium", _options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5))
                {
                    PollingInterval = TimeSpan.FromSeconds(1),
                };
            _driver.Manage().Window.Position = new System.Drawing.Point(2000,0);
            _driver.Manage().Window.Maximize();
            _sleep = 250;
        }

        public void goUrl()
        {
            _driver.Navigate().GoToUrl(@"https://www.tipos.sk/loterie/loto");
        }

        public string getValues()
        {
            string result = ";";
            IWebElement container;
            IReadOnlyCollection<IWebElement> listItems = new List<IWebElement>();
            string date = _driver.FindElement(By.Name("tiposDate")).GetAttribute("value");
            result = result + date + ";";
            string[] resultsId = { "results-1", "results-2" };
            foreach (string resultId in resultsId)
            {
                container = _driver.FindElement(By.Id(resultId));
                listItems = container.FindElements(By.TagName("li"));
                result = result + resultId + ";";
                foreach (IWebElement listItem in listItems)
                {
                    result = result + listItem.GetAttribute("data-value") + ";";
                    
                }
            }

            container = _driver.FindElement(By.Id("results-joker"));
            result = result + "results-joker" + ";";
            listItems = container.FindElements(By.TagName("li"));
            foreach (IWebElement listItem in listItems)
            {
                result = result + listItem.Text + ";";
            }

            return result;
        }

        public void openDatePicker ()
        {
            _driver.FindElement(By.Name("tiposDate")).Click();
            Thread.Sleep(_sleep);
        }

        public string clickOnDate(int tr, int td)
        {
            string dataEvent = "";
            try
            {
                dataEvent = _driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/table/tbody/tr[" + tr + "]/td[" + td + "]")).GetAttribute("data-event");
            }
            catch (Exception)
            {
                return null;
            }

            if (dataEvent != null)
            {
                _driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/table/tbody/tr[" + tr + "]/td[" + td + "]")).Click();
                Thread.Sleep(_sleep);
                string myValues = getValues();
                openDatePicker();
                return myValues;
            }
            return null;

        }

        public void clickPrevious()
        {
            _driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/div[1]/a[1]")).Click();
            Thread.Sleep(_sleep);
        }

    }
}
