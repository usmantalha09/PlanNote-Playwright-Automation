using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlanNotePlaywrite.Pages;
using PlanNotePlaywrite.Utils;
using System;
using System.IO;
using System.Numerics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlanNotePlaywrite.Tests
{
    public class CampaignTest : Base
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
        public async Task TestVerifyThatAdminUserIsAbleToSetNoticeFromNameOfTheAccountsFromTheAccountsDetailPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1078";
                Test = Extent.CreateTest("Test Campaign 1078: Verify that admin user is able to set notice from name of the accounts from the accounts detail page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that admin user is able to set notice from name of the accounts from the accounts detail page</a>");

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

                Test.Log(Status.Info, "Step 5: click on accounts lift side menu option");
                await accountsPage.clickOnAccountsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerifyAccountsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on create an account button");
                await accountsPage.clickOnCreateAnAccountButton();

                Test.Log(Status.Info, "Step 8: Verify that the create an account popup title is displaying");
                Assert.True(await accountsPage.VerifyCreateAnAccountPopupTitleIsVisible());
                // steps for this test cases will be update.

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
        public async Task TestVerifyThatFirstClassCampaignShouldRunAndWorkSuccessfully()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1219";
                Test = Extent.CreateTest("Test Campaign 1219: Verify that first class campaign should run and work successfully", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that first class campaign should run and work successfully</a>");

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

                Test.Log(Status.Info, "Step 5: click on accounts lift side menu option");
                await accountsPage.clickOnAccountsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerifyAccountsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on create an account button");
                await accountsPage.clickOnCreateAnAccountButton();

                Test.Log(Status.Info, "Step 8: Verify that the create an account popup title is displaying");
                Assert.True(await accountsPage.VerifyCreateAnAccountPopupTitleIsVisible());
                // steps for this test cases will be update.

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
        public async Task TestVerifyThatEmailCampaignShouldManuallyRunSuccessfully()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var campaignPage = new CampaignPage(page);
            var campaignName = "Test Campaign: " + DateTime.Now.Ticks;
            var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1141";
                Test = Extent.CreateTest("Test Campaign 1141: Verify that email campaign should manually run successfully", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that email campaign should run successfully</a>");

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

                Test.Log(Status.Info, "Step 5: click on campaign lift side menu option");
                await campaignPage.clickOnCampaignsSiteMenu();

                Test.Log(Status.Info, "Step 6: Verify that the campaign page title is displaying");
                Assert.True(await campaignPage.verifyCampaignsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Click on create campaign button");
                await campaignPage.ClickOnCreateCampaignButton();

                Test.Log(Status.Info, "Step 8: Verify Create Campaign Modal is displaying");
                Assert.True(await campaignPage.VerifyCreateCampaignModalIsDisplaying());

                Test.Log(Status.Info, "Step 9: Enter campaign name");
                await campaignPage.enterCreateCampaignName(campaignName);

                Test.Log(Status.Info, "Step 10: Select Plan");
                await campaignPage.selectPlanDropdownWhitelistOption();

                Test.Log(Status.Info, "Step 11: Select template dropdown first Option");
                await campaignPage.selectTemplateAccountDropdownFirstOption();

                Test.Log(Status.Info, "Step 12: click on campaign save button");
                await campaignPage.clickOnCampaignSaveButton();

                Test.Log(Status.Info, "Step 13: Verify Configure Campaign page is displaying");
                Assert.True(await campaignPage.verifyConfigureCampaignPageIsDisplaying());

                Test.Log(Status.Info, "Status 15: Add Video URL");
                await campaignPage.addVideoURL(url);

                Test.Log(Status.Info, "Step 15: Select Thank you Email Template");
                await campaignPage.selectThankYouEmailTemplate();

                Test.Log(Status.Info, "Step 16: Add email step");
                await campaignPage.addEmailStep();

                Test.Log(Status.Info, "Step 17: Verify Add Campaign Step is displaying");
                Assert.True(await campaignPage.VerifyAddCampaignStepIsDisplaying());

                Test.Log(Status.Info, "Step 18: Select step type email from add campaign step");
                await campaignPage.selectStepTypeEmailFromAddCampaignStep();

                Test.Log(Status.Info, "Step 19: Click on add button to add step");
                await campaignPage.clickOnAddButton();

                Test.Log(Status.Info, "Step 20: Click on participant tab on campaign page");
                await campaignPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 21: Verify Select Campaign Participant Modal is displaying");
                Assert.True(await campaignPage.VerifySelectCampaignParticipantsModalIsDisplaying());

                Test.Log(Status.Info, "Step 22: Click on add Button");
                await campaignPage.clickOnAddButton();

                Test.Log(Status.Info, "Step 23: Choose Files");
                await campaignPage.ChooseFiles();
                
                Test.Log(Status.Info, "Step 24: Verify file has been attached");
                Assert.True(await campaignPage.VerifyFileHasBeenAttached());

                Test.Log(Status.Info, "Step 25: Verify Campaign Status updated from not ready to ready");
                Assert.True(await campaignPage.VerifyCampaignStatusUpdatedFromNotReadyToReady());

                Test.Log(Status.Info, "Step 26: Click on Start Now Campaign");
                await campaignPage.ClickOnStartNowCampaign();

                Test.Log(Status.Info, "Step 27: Stop the running test camapign");
                await campaignPage.StopTheRunningTestCampaign();

                Test.Log(Status.Info, "Step 28: Verify Campaign has been stopped");
                Assert.True(await campaignPage.VerifyCampaignStatusUpdatedFromRunningToStopped());

                Test.Log(Status.Info, "Step 29: Delete the stopped test campaign");
                await campaignPage.DeleteTheStoppedTestCampaign();
                
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
        public async Task TestVerifyThatAdminCampaignShouldRunSuccessfully()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var campaignPage = new CampaignPage(page);
            var campaignName = "Test Campaign: " + DateTime.Now.Ticks;
            var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1300";
                Test = Extent.CreateTest("Test Campaign 1300: Verify that Admin campaign should run successfully", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that admin campaign should run successfully</a>");

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

                Test.Log(Status.Info, "Step 5: click on campaign lift side menu option");
                await campaignPage.clickOnCampaignsSiteMenu();

                Test.Log(Status.Info, "Step 6: Verify that the campaign page title is displaying");
                Assert.True(await campaignPage.verifyCampaignsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Click on create campaign button");
                await campaignPage.ClickOnCreateCampaignButton();

                Test.Log(Status.Info, "Step 8: Verify Create Campaign Modal is displaying");
                Assert.True(await campaignPage.VerifyCreateCampaignModalIsDisplaying());

                Test.Log(Status.Info, "Step 9: Enter campaign name");
                await campaignPage.enterCreateCampaignName(campaignName);

                Test.Log(Status.Info, "Step 10: Select Plan");
                await campaignPage.selectPlanDropdownWhitelistOption();

                Test.Log(Status.Info, "Step 11: Select template dropdown first Option");
                await campaignPage.selectTemplateAccountDropdownFirstOption();

                Test.Log(Status.Info, "Step 12: click on campaign save button");
                await campaignPage.clickOnCampaignSaveButton();

                Test.Log(Status.Info, "Step 13: Verify Configure Campaign page is displaying");
                Assert.True(await campaignPage.verifyConfigureCampaignPageIsDisplaying());

                Test.Log(Status.Info, "Status 15: Add Video URL");
                await campaignPage.addVideoURL(url);

                Test.Log(Status.Info, "Step 15: Select Thank you Email Template");
                await campaignPage.selectThankYouEmailTemplate();

                Test.Log(Status.Info, "Step 16: Add email step");
                await campaignPage.addEmailStep();

                Test.Log(Status.Info, "Step 17: Verify Add Campaign Step is displaying");
                Assert.True(await campaignPage.VerifyAddCampaignStepIsDisplaying());

                Test.Log(Status.Info, "Step 18: Select step type email from add campaign step");
                await campaignPage.selectStepTypeEmailFromAddCampaignStep();

                Test.Log(Status.Info, "Step 19: Select Plan Administrator");
                await campaignPage.selectPlanAdministratorFromRecipient();

                Test.Log(Status.Info, "Step 20: Click on add button to add step");
                await campaignPage.clickOnAddButton();

                Test.Log(Status.Info, "Step 21: Click on participant tab on campaign page");
                await campaignPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 22: Verify Select Campaign Participant Modal is displaying");
                Assert.True(await campaignPage.VerifySelectCampaignParticipantsModalIsDisplaying());

                Test.Log(Status.Info, "Step 23: Click on add Button");
                await campaignPage.clickOnAddButton();

                Test.Log(Status.Info, "Step 24: Choose Files");
                await campaignPage.ChooseFiles();

                Test.Log(Status.Info, "Step 25: Verify file has been attached");
                Assert.True(await campaignPage.VerifyFileHasBeenAttached());

                Test.Log(Status.Info, "Step 26: Verify Campaign Status updated from not ready to ready");
                Assert.True(await campaignPage.VerifyCampaignStatusUpdatedFromNotReadyToReady());

                Test.Log(Status.Info, "Step 27: Click on Start Now Campaign");
                await campaignPage.ClickOnStartNowCampaign();

                Test.Log(Status.Info, "Step 28: Stop the running test camapign");
                await campaignPage.StopTheRunningTestCampaign();

                Test.Log(Status.Info, "Step 29: Verify Campaign has been stopped");
                Assert.True(await campaignPage.VerifyCampaignStatusUpdatedFromRunningToStopped());

                Test.Log(Status.Info, "Step 30: Delete the stopped test campaign");
                await campaignPage.DeleteTheStoppedTestCampaign();

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
        public async Task TestVerifyThatEmailCampaignShouldRunSuccessfullyAndViewTheNoticeByEnteringSSN()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var campaignPage = new CampaignPage(page);
            var campaignName = "Test Campaign: " + DateTime.Now.Ticks;
            var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1268";
                Test = Extent.CreateTest("Test Campaign 1268: Verify that Email campaign should run successfully and view notice by entering SSN", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that email campaign should run successfully and view notice by entering SSN</a>");

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

                Test.Log(Status.Info, "Step 5: click on campaign lift side menu option");
                await campaignPage.clickOnCampaignsSiteMenu();

                Test.Log(Status.Info, "Step 6: Verify that the campaign page title is displaying");
                Assert.True(await campaignPage.verifyCampaignsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Click on create campaign button");
                await campaignPage.ClickOnCreateCampaignButton();

                Test.Log(Status.Info, "Step 8: Verify Create Campaign Modal is displaying");
                Assert.True(await campaignPage.VerifyCreateCampaignModalIsDisplaying());

                Test.Log(Status.Info, "Step 9: Enter campaign name");
                await campaignPage.enterCreateCampaignName(campaignName);

                Test.Log(Status.Info, "Step 10: Select Plan");
                await campaignPage.selectPlanDropdownWhitelistOption();

                Test.Log(Status.Info, "Step 11: Select template dropdown first Option");
                await campaignPage.selectTemplateAccountDropdownFirstOption();

                Test.Log(Status.Info, "Step 12: click on campaign save button");
                await campaignPage.clickOnCampaignSaveButton();

                Test.Log(Status.Info, "Step 13: Verify Configure Campaign page is displaying");
                Assert.True(await campaignPage.verifyConfigureCampaignPageIsDisplaying());

                Test.Log(Status.Info, "Status 15: Add Video URL");
                await campaignPage.addVideoURL(url);

                Test.Log(Status.Info, "Step 15: Select Thank you Email Template");
                await campaignPage.selectThankYouEmailTemplate();

                Test.Log(Status.Info, "Step 16: Add email step");
                await campaignPage.addEmailStep();

                Test.Log(Status.Info, "Step 17: Verify Add Campaign Step is displaying");
                Assert.True(await campaignPage.VerifyAddCampaignStepIsDisplaying());

                Test.Log(Status.Info, "Step 18: Select step type email from add campaign step");
                await campaignPage.selectStepTypeEmailFromAddCampaignStep();
 
                Test.Log(Status.Info, "Step 19: Click on add button to add step");
                await campaignPage.clickOnAddButton();

                Test.Log(Status.Info, "Step 20: Click on participant tab on campaign page");
                await campaignPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 21: Verify Select Campaign Participant Modal is displaying");
                Assert.True(await campaignPage.VerifySelectCampaignParticipantsModalIsDisplaying());

                Test.Log(Status.Info, "Step 22: Click on add Button");
                await campaignPage.clickOnAddButton();

                Test.Log(Status.Info, "Step 23: Choose Files");
                await campaignPage.ChooseFiles();

                Test.Log(Status.Info, "Step 24: Verify file has been attached");
                Assert.True(await campaignPage.VerifyFileHasBeenAttached());

                Test.Log(Status.Info, "Step 25: Verify Campaign Status updated from not ready to ready");
                Assert.True(await campaignPage.VerifyCampaignStatusUpdatedFromNotReadyToReady());

                Test.Log(Status.Info, "Step 26: Click on Start Now Campaign");
                await campaignPage.ClickOnStartNowCampaign();

                Test.Log(Status.Info, "Step 27: Stop the running test camapign");
                await campaignPage.StopTheRunningTestCampaign();

                Test.Log(Status.Info, "Step 28: Verify Campaign has been stopped");
                Assert.True(await campaignPage.VerifyCampaignStatusUpdatedFromRunningToStopped());

                Test.Log(Status.Info, "Step 29: Delete the stopped test campaign");
                await campaignPage.DeleteTheStoppedTestCampaign();

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