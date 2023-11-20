Twitter Scraper
Overview
This C# Twitter scraper is designed to collect data from a specified Twitter profile. It utilizes the Selenium WebDriver with Chrome to automate interactions with the Twitter website, extracting information about tweets, including text, date, and likes. The scraped data is saved to a CSV file, ensuring uniqueness.

Features
Selenium WebDriver: Automates browser interactions for web scraping.
Notification Disable: Disables browser notifications for an uninterrupted scraping experience.
Continuous Scrolling: Dynamically scrolls down to load more tweets until reaching the end.
Duplicate Avoidance: Uses a HashSet to prevent the addition of duplicate tweets to the output.
Prerequisites
ChromeDriver: Download the ChromeDriver executable from here. Ensure the correct path is set in the code.
How to Use
Clone or download the repository.
Download the ChromeDriver executable and update the path in the code.
Build and run the application.
