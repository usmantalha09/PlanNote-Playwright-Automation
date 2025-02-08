using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Utils
{
    public class Constants
    {
        public static readonly string BaseUrl = "https://app-qa.plannotice.com/login";

        public static readonly string email = "adnan.qat123@gmail.com";

        public static readonly string emailManager = "adnan.qat123456@gmail.com";

        public static readonly string password = "Test@12345";
        public static readonly string managerPassword = "Test@1234";

        public static ExtentReports Extent { get; set; }
        public static ExtentTest Test { get; set; }

        public readonly int retryCount = 2;

        public System.Random Random = new System.Random();

        public static string chromiumExecutablePath = "C:\\Users\\USER\\AppData\\Local\\ms-playwright\\chromium-1000\\chrome-win\\chrome.exe";

        // public string ReportPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plan_notes_Playwright", "PlanNotePlaywrite", "report", "extent-report.html");
        // public string ScreenshotPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plan_notes_Playwright", "PlanNotePlaywrite", "report", "image");
        // Get the project directory
        public static string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Combine with the relative path to the "Reports" folder
        public static string reportsFolderPath = Path.Combine(projectDirectory, "report");

        public string ReportPath = reportsFolderPath;

        // Combine with the relative path to the "Screenshots" folder
        public string ScreenshotPath = Path.Combine(reportsFolderPath, "image");

        // Combine with the relative path to the "resources" folder
        public static string resourcesFolderPath = Path.Combine(projectDirectory, "resources");

        // Combine with the relative path to the "resources/data" folder
        public string resourcesDataFolderPath = Path.Combine(resourcesFolderPath, "data");

        public static string fileName = "ADNAN Test Census File";

        public static string fileName2 = "ADNAN Test Census File - 2";

        public static string templateName = "Email Template";

        public static string fullPath = @"C:\Users\USER\Desktop\Plan_notes_Playwright\PlanNotePlaywrite\resources\data\"+ fileName + ".xlsx";

        public static string fullPath2 = @"C:\Users\USER\Desktop\Plan_notes_Playwright\PlanNotePlaywrite\resources\data\"+ fileName2 + ".xlsx";

        public static string fullPathHtmlTemplate = @"C:\Users\USER\Desktop\Plan_notes_Playwright\PlanNotePlaywrite\resources\data\" + templateName + ".html";

        public void SetupReports()
        {
            Extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(ReportPath);
            Extent.AttachReporter(htmlReporter);            
        }

        public void TeardownReports()
        {
            Extent.Flush();
        }

        public async Task HandleExceptionAsync(IPage page, Exception e)
        {
            string screenshotPath = Path.Combine(ScreenshotPath, $"screenshot{DateTime.Now.Ticks}.png");
            var screenshotOptions = new PageScreenshotOptions
            {
                Path = screenshotPath,
            };
            // page.ScreenshotAsync(screenshotOptions).Wait();
            byte[] screenshotBytes = await page.ScreenshotAsync();
            Test.Fail($"Test failed: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            Assert.True(false);
        }

    }
}
