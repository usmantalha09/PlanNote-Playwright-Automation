using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using PlanNotePlaywrite.Pages;
using PlanNotePlaywrite.Utils;
using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Tests
{
    public class LoginTest : Base
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IBrowserContext context;

        [SetUp]
        public async Task Setup()
        {
            playwright = await PlaywrightConfig.ConfigurePlaywrightAndLaunchBrowser();
            browser = await PlaywrightConfig.LaunchChromiumBrowser(playwright, chromiumExecutablePath, false);
            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport
            });
        }

        [TearDown]
        public async Task Teardown()
        {
            await browser.CloseAsync();
        }

        [Test]
        public async Task TestVerifyLoginWithValidCredentials()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1046";
                Test = Extent.CreateTest("Test Login 1046: Verify Login With Valid Credentials", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying login with valid credentials</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 3: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 4: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
            }
        }

        [Test]
        public async Task TestVerifyLoginWithInvalidCredentials()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1047";
                Test = Extent.CreateTest("Test Login 1047: Verify Login With Invalid Credentials", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying login with invalid credentials</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 3: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials("test.qat123@gmail.com", "Test@1234");
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 4: Verify that the dashboard page is not displaying");
                Assert.False(await loginPage.verifyDashboardOnLioinPageIsVisible());

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
            }
        }

        [Test]
        public async Task TestWhileUserEnterInvalidEmailVerifyForgotPasswordFlow()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1051";
                Test = Extent.CreateTest("Test Login 1051: Verify While User Enter Invalid Email Verify Forgot Password Flow", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying While User Enter Invalid Email Verify Forgot Password Flow</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 3: Click Forgot Password Link");
                await loginPage.clickOnForgotpasswordLink();

                Test.Log(Status.Info, "Step 4: Verify that the Restore Password Page is displaying");
                Assert.True(await loginPage.verifyRestorePasswordPageIsVisible());

                Test.Log(Status.Info, "Step 5: Enter Restore Password Email : test1234");
                await loginPage.enterRestorePasswordEmail("test1234");

                Test.Log(Status.Info, "Step 6: Verify that the Restore Password Page is displaying");
                Assert.True(await loginPage.verifyRestorePasswordPageIsVisible());

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
            }
        }

        [Test]
        public async Task TestRememberMeOptionOnLoginScreen()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1048";
                Test = Extent.CreateTest("Test Login 1048: Verify Remember Me Option On Login Screen", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying Remember Me Option On Login Screen</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 3: click remember me checkbox");
                await loginPage.clickOnRememberMeCheckbox();

                Test.Log(Status.Info, "Step 4: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 5: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
            }
        }

        public async Task TestVerifyThatAdminUserIsAbleToSetNoticeFromNameOfTheAccountsFromTheAccountsDetailPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1078";
                Test = Extent.CreateTest("Test Login 1078: Verify that admin user is able to set notice from name of the accounts from the accounts detail page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that admin user is able to set notice from name of the accounts from the accounts detail page</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 3: click remember me checkbox");
                await loginPage.clickOnRememberMeCheckbox();

                Test.Log(Status.Info, "Step 4: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 5: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
            }
        }
    }
}