using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
namespace Scrapping
{
    internal class TweeterScrapper
    {
        private static IWebDriver driver;
        private static HashSet<string> scrapedTweets = new HashSet<string>();
        public static void ScrappingTwitterData()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            driver = new ChromeDriver(@"C:\Users\admin\source\repos\Scrapping\Scrapping\chromedriver-win64\chromedriver.exe", options);
            driver.Navigate().GoToUrl("https://twitter.com/JunaidBabarian");
            Thread.Sleep(1000);
            bool stopScrolling = false;
            using (var writer = new StreamWriter(@"C:\Users\admin\source\repos\Scrapping\Scrapping\TWEETDATASCRAPING.csv", true))
            {
                while (!stopScrolling)
                {
                    var tweetElements = driver.FindElements(By.XPath("//*[@id='react-root']/div/div//section//article"));

                    foreach (var element in tweetElements)
                    {


                        var tweetDate = element.FindElement(By.CssSelector("time")).GetAttribute("datetime");
                        var tweetText = element.FindElement(By.CssSelector("div[lang]")).Text;
                        var likes = element.FindElement(By.CssSelector("div[data-testid='like']")).Text;
                        var tweetKey = $"{tweetDate}-{tweetText}";
                        if (!scrapedTweets.Contains(tweetKey))
                        {
                            Console.WriteLine("--------Request start ------");
                            writer.WriteLine($"Tweet Text:       {tweetText}");
                            writer.WriteLine($"Tweet Date:       {tweetDate}");
                            writer.WriteLine($"Tweet Likes:      {likes}");
                            writer.WriteLine();
                            writer.WriteLine();
                            writer.WriteLine();
                            scrapedTweets.Add(tweetKey);
                            Console.WriteLine("---------Request Complete------");
                        }
                    }
                    if (IsEndOfTweets())
                    {
                        Console.WriteLine("Reached the end of tweets. Stopping scrolling.");
                        stopScrolling = true;
                        driver.Quit();
                    }
                    Console.WriteLine("------------Scrolling down...------");
                    ScrollDownByPixels(2000);
                    Console.WriteLine("------------Waiting for some seconds...------");
                    Thread.Sleep(1000);
                }
            }
        }
        static void ScrollDownByPixels(int pixelsToScroll)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"window.scrollBy(0, {pixelsToScroll});");
        }
        static bool IsEndOfTweets()
        {
            var noNewTweetsElement = driver.FindElements(By.XPath("//div[@aria-label='Timeline: No new Tweets']"));
            bool isEndOfTweets = noNewTweetsElement.Count > 0;
            if (isEndOfTweets)
            {
                Console.WriteLine("Reached the end of tweets. Stopping scrolling.");
            }
            return isEndOfTweets;
        }
    }
}
