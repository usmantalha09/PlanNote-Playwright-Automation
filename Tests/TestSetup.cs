using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanNotePlaywrite.Utils;

namespace PlanNotePlaywrite.Tests
{
    [SetUpFixture]
    public class TestSetup : Base
    {
        public static ExtentReports extent;

        [OneTimeSetUp]
        public void SetupReports()
        {
            DeleteImagesFromFolder();
            Extent = new ExtentReports();
            string reportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Plan_notes_Playwright", "PlanNotePlaywrite", "report", "extent-report.html");
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            Extent.AttachReporter(htmlReporter);
        }

        [OneTimeTearDown]
        public void TeardownReports()
        {
            Extent.Flush();
        }
    }
}
