using Log.to.Text.File;
using LotoHistory;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

Console.WriteLine("Start scraping!");

int sleep = 250;
string results = "";
string lastResult = "";
int year = 2022;

var sessionLog = new SessionLog();
var browser = new Selenium();

browser.goUrl();
Thread.Sleep(sleep);

browser.openDatePicker();
Thread.Sleep(sleep);

while (year > 2000)
{
    for (int td = 1; td < 9; td++)
    {
        for (int tr = 1; tr < 7; tr++)
        {
            results = browser.clickOnDate(tr, td);
            if (results != null)
            {
                lastResult = results;
                Console.WriteLine(results);
                sessionLog.Write(results);
            }
        }
    }

    browser.clickPrevious();
    var regex = new Regex(@"(\d\d\d\d)");
    var match = regex.Match(lastResult);
    year = Convert.ToInt32(match.Groups[1].Value);
}

Console.WriteLine("End!");