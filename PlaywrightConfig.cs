using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite
{
    internal class PlaywrightConfig
    {
        public static async Task<IPlaywright> ConfigurePlaywrightAndLaunchBrowser()
        {
            // Enable logging
           // PlaywrightLogger.IsEnabled = true;

            // Create Playwright instance
            var playwright = await Playwright.CreateAsync();

            return playwright;
        }

        public static async Task<IBrowser> LaunchChromiumBrowser(IPlaywright playwright, string executablePath, bool headless)
        {
            // Launch Chromium browser
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                //ExecutablePath = executablePath,
                Headless = headless,
                Timeout = 60000,
                // Set viewport size to null for maximum size
                Args = new List<string> { "--start-maximized" }
            });            

            return browser;
        }
    }
}
