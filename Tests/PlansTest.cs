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
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanNotePlaywrite.Tests
{
    public class PlansTest : Base
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
        public async Task TestVerifyThatPlanDetailPageHasAllSubPagesAsPerRequirement()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1055";
                Test = Extent.CreateTest("Test Plan 1055: Verify that plan detail page has all sub pages as per requirement", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying  that plan detail page has all sub pages as per requirement</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: Verify Details Tab, Settings Tab, Billings Tab, Contacts Tab, Campaigns Tab, Census Tab, Participants Tab, List Tab, Invoices Tab, Recordkeeper Tab and Notices Tab are displaying");
                Assert.True(await PlansPage.verifyDetailsTabIsVisible());
                Assert.True(await PlansPage.verifySettingsTabIsVisible());
                Assert.True(await PlansPage.verifyBillingsTabIsVisible());
                Assert.True(await PlansPage.verifyContactsTabIsVisible());
                Assert.True(await PlansPage.verifyCampaignsTabIsVisible());
                Assert.True(await PlansPage.verifyCensusTabIsVisible());
                Assert.True(await PlansPage.verifyParticipantsTabIsVisible());
                Assert.True(await PlansPage.verifyListTabIsVisible());
                Assert.True(await PlansPage.verifyInvoicesTabIsVisible());
                Assert.True(await PlansPage.verifyRecordkeeperTabIsVisible());
                Assert.True(await PlansPage.verifyNoticesTabIsVisible());

                Test.Log(Status.Info, "Deleting created Plan");
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifyCancelingPlanCreation()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1056";
                Test = Extent.CreateTest("Test Plan 1056: Verify canceling Plan Creation", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying canceling Plan Creation</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "step 8: click on Plan A Popup Cancel Button");
                await PlansPage.clickOnCreatePlanAPopupCancelButton();

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name In Search :" + testPlanName);
                await PlansPage.enterPlanNameSearch(testPlanName);

                Test.Log(Status.Info, "Step 9: Verify No new plan is created, and the list of plans remains unchanged");
                Assert.True(await PlansPage.verifyNoNewPlanCreated());


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
        public async Task TestVerifyPlanCreationWithBlankName()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1057";
                Test = Extent.CreateTest("Test Plan 1057: Verify Plan Creation with Blank Name", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying Plan Creation with Blank Name</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "step 7: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 8: Verify System Detects The Blank Name Field And Highlights The Border Of The Name Field In Red");
                Assert.True(await PlansPage.verifySystemDetectsTheBlankNameFieldAndHighlightsTheBorderOfTheNameFieldInRed());

                Test.Log(Status.Info, "Step 9: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

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
        public async Task TestVerifyVerifyThatUserIsAbleToDeleteThePlan()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1058";
                Test = Extent.CreateTest("Test Plan 1058: Verify that user is able to Delete the plan", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to Delete the plan</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: Verify Create Plan Delete Button is displaying");
                Assert.True(await PlansPage.verifyCreatePlanDeleteButtonIsVisible());

                Test.Log(Status.Info, "Step 11: click on Create Plan Delete Button");
                await PlansPage.clickOnCreatePlanDeleteButton();

                Test.Log(Status.Info, "Step 12: Verify Are You Sure You Want To Delete Massage On Popup is displaying");
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());

                Test.Log(Status.Info, "Step 13: click on delete a plan popup No Button");
                await PlansPage.clickOnDeletePlanNoButton();

                Test.Log(Status.Info, "Step 14: Verify The confirmation prompt is closed, and the user stays on the dashboard is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 15: Verify Plan Not Deleted and The plan remains unchanged, and no data is lost");
                Assert.True(await PlansPage.VerifyPlanNotDeleted(testPlanName));

                Test.Log(Status.Info, "Step 16: click on Create Plan Delete Button");
                await PlansPage.clickOnCreatePlanDeleteButton();

                Test.Log(Status.Info, "Step 17: Verify Are You Sure You Want To Delete Massage On Popup is displaying");
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());

                Test.Log(Status.Info, "Step 18: click on delete a plan popup Yes Button");
                await PlansPage.clickOnDeletePlanYesButton();

                Test.Log(Status.Info, "Step 19: Verify Plan Deletion and The plan is successfully deleted, and it no longer appears in the list of plans");
                await PlansPage.enterPlanNameSearch(testPlanName);
                Assert.True(await PlansPage.verifyNoNewPlanCreated());


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
        public async Task TestVerifyThatUserIsAbleToUpdateThePlan()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var testPlanEIN = "123321";
            var updatePlanWebsite = "www.test.com";
            var updatePhonenumber = "6125047154";
            var updatePlanEmail = "test123@gmail.com";
            var updatePlanAddressOne = "test XYZ";
            var updatePlanAddressTwo = "test ZYX";
            var updatePlanCity = "test City";
            var updatePlanState = "test State";
            var updatePlanZip = "10004";

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1059";
                Test = Extent.CreateTest("Test Plan 1059: Verify that user is able to update the plan", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to update the plan</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: Enter Test Updated Plan Name :" + "Update" + testPlanName);
                await PlansPage.enterPlanDashboardName("Update" + testPlanName);

                Test.Log(Status.Info, "Step 11: Enter Test Updated EIN :" + testPlanEIN);
                await PlansPage.enterPlanDashboardEIN(testPlanEIN);

                Test.Log(Status.Info, "Step 12: Enter Test Updated Website :" + updatePlanWebsite);
                await PlansPage.enterPlanDashboardWebsite(updatePlanWebsite);

                Test.Log(Status.Info, "Step 13: Enter Test Updated Email :" + updatePlanEmail);
                await PlansPage.enterPlanDashboardEmail(updatePlanEmail);

                Test.Log(Status.Info, "Step 14: Enter Test Phone Number :" + updatePhonenumber);
                await PlansPage.enterPlanDashboardPhone(updatePhonenumber);

                Test.Log(Status.Info, "Step 15: Enter Test Address One :" + updatePlanAddressOne);
                await PlansPage.enterPlanDashboardAddressOne(updatePlanAddressOne);

                Test.Log(Status.Info, "Step 16: Enter Test Updated Address Two :" + updatePlanAddressTwo);
                await PlansPage.enterPlanDashboardAddressTwo(updatePlanAddressTwo);

                Test.Log(Status.Info, "Step 17: Enter Test Updated City :" + updatePlanCity);
                await PlansPage.enterPlanDashboardCity(updatePlanCity);

                Test.Log(Status.Info, "Step 18: Enter Test Updated Plan Name :" + updatePlanState);
                await PlansPage.enterPlanDashboardState(updatePlanState);

                Test.Log(Status.Info, "Step 19: Enter Test Updated Zip Code :" + updatePlanZip);
                await PlansPage.enterPlanDashboardZip(updatePlanZip);

                Test.Log(Status.Info, "Step 20: click on Edit Plan Save Button");
                await PlansPage.clickOnPlanDashboardSaveButton();

                Test.Log(Status.Info, "Step 21: Verify Updated Plan Details");
                Assert.True(await PlansPage.verifyPlanUpdatedSuccessfullyPopupIsVisible());
                await PlansPage.enterPlanNameSearch("Update" + testPlanName);
                //Assert.True(await PlansPage.VerifyUpdatePlanName("Update" + testPlanName));

                Test.Log(Status.Info, "Step 22: Deleting created Plan"); ;
                await PlansPage.clickOnCreatedPlanName("Update" + testPlanName);
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifyCancelingPlanUpdate()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1060";
                Test = Extent.CreateTest("Test Plan 1060: Verify Canceling Plan Update", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that Canceling Plan Update</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: Enter Test Updated Plan Name :" + "Update" + testPlanName);
                await PlansPage.enterPlanDashboardName("Update" + testPlanName);

                Test.Log(Status.Info, "Step 11: click on Edit Plan Cancel Button");
                await PlansPage.clickOnPlanDashboardCancelButton();

                Test.Log(Status.Info, "Deleting created Plan");
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifySuccessfulNoticeCreation()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var noticeName = "Test notice plan";
            var noticeSummary = "Test notice plan";
            var noticeVideoUrl = "https://filetransfer.io/data-package/gT7RrmRZ#link";

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1061";
                Test = Extent.CreateTest("Test Plan 1061: Verify Successful Notice Creation", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying Successful Notice Creation</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Plan Dashboard Notices Tab");
                await PlansPage.clickOnPlanDashboardNoticesTab();

                Test.Log(Status.Info, "Step 11: Verify Plan Notices Title is displaying");
                Assert.True(await PlansPage.VerifyPlanNoticesTitle());

                Test.Log(Status.Info, "Step 12: click on Create Notice Button");
                await PlansPage.clickOnCreateNoticeButton();

                Test.Log(Status.Info, "Step 13: Verify Notice Popup Title is displaying");
                Assert.True(await PlansPage.VerifyNoticePopupTitleIsVisible());

                Test.Log(Status.Info, "Step 14: Enter Notice Name :" + noticeName);
                await PlansPage.enterNoticePopupName(noticeName);

                Test.Log(Status.Info, "Step 15: Select Tag First Option");
                await PlansPage.selectTagFirstOption();

                Test.Log(Status.Info, "Step 16: Select Type First Option");
                await PlansPage.selectNoticeTypeFirstOption();

                Test.Log(Status.Info, "Step 17: Select Workflow Last Option");
                await PlansPage.selectWorkflowFirstOption();

                Test.Log(Status.Info, "Step 5: Enter Notice Summary :" + noticeSummary);
                await PlansPage.enterNoticePopupSummary(noticeSummary);

                Test.Log(Status.Info, "Step 18: Choose Files");
                await PlansPage.noticePopupChooseFiles();

                Test.Log(Status.Info, "Step 19: Enter Notice Summary :" + noticeSummary);
                await PlansPage.enterNoticePopupSummary(noticeSummary);

                Test.Log(Status.Info, "Step 20: click on Notice Save Button");
                await PlansPage.clickOnNoticePopupSaveButton();

                // Test.Log(Status.Info, "Step 21: Verify New Notice In The Notices List is displaying");
                //        Assert.True(await PlansPage.VerifyNewNoticeInTheNoticesListIsVisible(noticeSummary));                

                Test.Log(Status.Info, "Deleting created Plan");
                Assert.True(await PlansPage.waitForConfigureNoticeText());
                await PlansPage.clickOnDashboardTab();
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifyCancelingNoticeCreation()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var noticeName = "Test notice plan";
            var noticeSummary = "Test notice plan";

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1062";
                Test = Extent.CreateTest("Test Plan 1062: Verify canceling Notice Creation", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying canceling Notice Creation</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 1: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 1: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 2: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "step 3: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 3: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 3: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "step 3: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 3: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "step 4: click on Plan Dashboard Notices Tab");
                await PlansPage.clickOnPlanDashboardNoticesTab();

                Test.Log(Status.Info, "Step 4: Verify Plan Notices Title is displaying");
                Assert.True(await PlansPage.VerifyPlanNoticesTitle());

                Test.Log(Status.Info, "step 5: click on Create Notice Button");
                await PlansPage.clickOnCreateNoticeButton();

                Test.Log(Status.Info, "Step 5: Verify Notice Popup Title is displaying");
                Assert.True(await PlansPage.VerifyNoticePopupTitleIsVisible());

                Test.Log(Status.Info, "Step 5: Enter Notice Name :" + noticeName);
                await PlansPage.enterNoticePopupName(noticeName);

                Test.Log(Status.Info, "Step 5: Select Tag First Option");
                await PlansPage.selectTagFirstOption();

                Test.Log(Status.Info, "Step 5: Select Type First Option");
                await PlansPage.selectNoticeTypeFirstOption();

                Test.Log(Status.Info, "Step 5: Choose Files");
                await PlansPage.noticePopupChooseFiles();

                Test.Log(Status.Info, "Step 5: Enter Notice Summary :" + noticeSummary);
                await PlansPage.enterNoticePopupSummary(noticeSummary);

                //Test.Log(Status.Info, "Step 5: Enter Video Url :" + noticeVideoUrl);
                //await PlansPage.enterNoticePopupVideoUrl(noticeVideoUrl);

                Test.Log(Status.Info, "step 6: click on Notice Cancel Button");
                await PlansPage.clickOnNoticePopupCancelButton();

                Test.Log(Status.Info, "Step 7: Verify no new notice created");
                Assert.True(await PlansPage.VerifyPlanNoticesNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Deleting created Plan");
                await PlansPage.clickOnDashboardTab();
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifyWhileRequiredFieldsAreBlankVerifyNoticeCreation()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1063";
                Test = Extent.CreateTest("Test Plan 1063: Verify While required fields are blank, Verify Notice Creation", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying While required fields are blank, Verify Notice Creation</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Plan Dashboard Notices Tab");
                await PlansPage.clickOnPlanDashboardNoticesTab();

                Test.Log(Status.Info, "Step 11: Verify Plan Notices Title is displaying");
                Assert.True(await PlansPage.VerifyPlanNoticesTitle());

                Test.Log(Status.Info, "Step 12: click on Create Notice Button");
                await PlansPage.clickOnCreateNoticeButton();

                Test.Log(Status.Info, "Step 13: Verify Notice Popup Title is displaying");
                Assert.True(await PlansPage.VerifyNoticePopupTitleIsVisible());

                Test.Log(Status.Info, "Step 14: click on Notice Save Button");
                await PlansPage.clickOnNoticePopupSaveButton();

                //not implemented bug reported
                //Test.Log(Status.Info, "Step 15: Verify Error Messages for Blank Fields are isplaying");
                //Assert.True(await PlansPage.VerifyErrorMessagesForBlankFieldsAreAreVisible());

                Test.Log(Status.Info, "Step 16: Verify Stay on the 'Create Notice' Popup");
                Assert.True(await PlansPage.VerifyNoticePopupTitleIsVisible());

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
        public async Task TestVerifyThatUserIsAbleToUpdateRecordkeeperData()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var recordkeeperPlanId = "123321";
            var RecordkeeperProductId = "123321";
            var RecordkeeperFeedId = "123321";

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1064";
                Test = Extent.CreateTest("Test Plan 1064: Verify that user is able to update Recordkeeper data", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to update Recordkeeper data</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 1: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 1: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 2: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "step 3: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 4: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 5: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 6: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 7: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 8: click on Recordkeeper Tab");
                await PlansPage.clickOnRecordkeeperTab();

                Test.Log(Status.Info, "Step 9: Verify Recordkeeper Page Title is displaying");
                Assert.True(await PlansPage.VerifyRecordkeeperPageTitleIsVisible());

                Test.Log(Status.Info, "Step 10: Enter Recordkeeper Plan Id :" + recordkeeperPlanId);
                await PlansPage.enterRecordkeeperPlanId(recordkeeperPlanId);

                Test.Log(Status.Info, "Step 11: Enter Recordkeeper Product Id :" + RecordkeeperProductId);
                await PlansPage.enterRecordkeeperProductId(RecordkeeperProductId);

                Test.Log(Status.Info, "Step 12: Enter Recordkeeper Feed Id :" + RecordkeeperFeedId);
                await PlansPage.enterRecordkeeperFeedId(RecordkeeperFeedId);

                Test.Log(Status.Info, "Step 13: click on Recordkeeper Save Button");
                await PlansPage.clickOnRecordkeeperSaveButton();

                Test.Log(Status.Info, "Step 14: Verify Recordkeeper Plan Details Updated Successfully Popup is displaying");
                Assert.True(await PlansPage.VerifyRecordkeeperPlanDetailsUpdatedSuccessfullyPopupMsgIsVisible());

                Test.Log(Status.Info, "Deleting created Plan");
                await PlansPage.clickOnDashboardTab();
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifyBillingMethodAsPlan()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var BillingMethodOption = "Plan";

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1019";
                Test = Extent.CreateTest("Test Plan 1019: Verify that billing method as Plan", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that billing method as Plan</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Billings Tab");
                await PlansPage.clickOnBillingsTab();

                Test.Log(Status.Info, "Step 11: Verify Billings Settings And Services Title is displaying");
                Assert.True(await PlansPage.VerifyBilliSettingsAndServicesTitleIsVisible());

                Test.Log(Status.Info, "Step 12: Select Billing Method Option:" + BillingMethodOption);
                await PlansPage.selectBillingMethod(BillingMethodOption);

                Test.Log(Status.Info, "Step 13: click on Billings Save Button");
                await PlansPage.clickOnBilliSaveButton();

                Test.Log(Status.Info, "Step 14: Verify Billings Plan Settings Updated Successfully Popup is displaying");
                Assert.True(await PlansPage.VerifyBilliPlanSettingsUpdatedSuccessfullyPopupIsVisible());

                Test.Log(Status.Info, "Step 15: click on Plan Name: " + testPlanName);
                await PlansPage.clickOnCreatedPlanName(testPlanName);

                Test.Log(Status.Info, "Step 16: click on Billings Tab");
                await PlansPage.clickOnBillingsTab();

                Test.Log(Status.Info, "Step 17: Verify The billing method for the selected plan is updated to Plan is displaying");
                Assert.True(await PlansPage.VerifyBillMethodSelectedOptionIsVisible(BillingMethodOption));

                Test.Log(Status.Info, "Deleting created Plan");
                await PlansPage.clickOnDashboardTab();
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

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
        public async Task TestVerifyBillingMethodAsParticipant()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var BillingMethodOption = "Participant";

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1018";
                Test = Extent.CreateTest("Test Plan 1018: Verify that billing method as Participant", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that billing method as Participant</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify Successfully logged in, and the home/dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Billings Tab");
                await PlansPage.clickOnBillingsTab();

                Test.Log(Status.Info, "Step 11: Verify Billings Settings And Services Title is displaying");
                Assert.True(await PlansPage.VerifyBilliSettingsAndServicesTitleIsVisible());

                Test.Log(Status.Info, "Step 12: Select Billing Method Option:" + BillingMethodOption);
                await PlansPage.selectBillingMethod(BillingMethodOption);

                Test.Log(Status.Info, "Step 13: click on Billings Save Button");
                await PlansPage.clickOnBilliSaveButton();

                Test.Log(Status.Info, "Step 14: Verify Billings Plan Settings Updated Successfully Popup is displaying");
                Assert.True(await PlansPage.VerifyBilliPlanSettingsUpdatedSuccessfullyPopupIsVisible());

                Test.Log(Status.Info, "Step 15: click on Plan Name: " + testPlanName);
                await PlansPage.clickOnCreatedPlanName(testPlanName);

                Test.Log(Status.Info, "Step 16: click on Billings Tab");
                await PlansPage.clickOnBillingsTab();

                Test.Log(Status.Info, "Step 17: Verify The billing method for the selected plan is updated to Plan is displaying");
                Assert.True(await PlansPage.VerifyBillMethodSelectedOptionIsVisible(BillingMethodOption));

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());

                Test.Log(Status.Info, "Deleting created Plan");
                await PlansPage.clickOnDashboardTab();
                await PlansPage.clickOnCreatePlanDeleteButton();
                Assert.True(await PlansPage.verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible());
                await PlansPage.clickOnDeletePlanYesButton();
                await PlansPage.enterPlanNameSearch(testPlanName);

                screenshotBytes = await page.ScreenshotAsync();
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
        public async Task TestVerifyThatUserIsAbleToCreatePlansWithParticipantsImport()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var SSNOption = "SSN";
            var NameOption = "Name";
            var EmploymentStatusOption = "Employment Status";
            var WorkEmailAddressOption = "Work Email Address";
            var PhoneOption = "Phone";
            var AddressOption = "Address";
            var CityOption = "City";
            var StateOption = "State";
            var ZipOption = "Zip";
            var DateOfBirthOption = "Date of Birth";
            var DateOfHireOption = "Date of Hire";
            var DateOfRehireOption = "Date of Rehire";
            var TermDateOption = "Term Date";
            var BalanceOption = "Balance*";
            var EligibleForrEscalationOption = "Eligible forr Escalation?";
            int AllEmployeesRecords;
            int AllExEmployees;



            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1088";
                Test = Extent.CreateTest("Test Plan 1088: Verify that user is able to create plans with participants import", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to create plans with participants import</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Census Tab");
                await PlansPage.clickOnCensusTab();

                Test.Log(Status.Info, "Step 11: click on import census file button");
                await PlansPage.clickOnImportCensusFileButton();

                Test.Log(Status.Info, "Step 12: Verify new census file title is displaying");
                Assert.True(await PlansPage.verifyNewCensusFileTitleIsVisible());

                Test.Log(Status.Info, "Step 13: Upload New Census File");
                await PlansPage.UploadNewCensusFile();

                Test.Log(Status.Info, "Step 14: click on Census Tab");
                await PlansPage.clickOnCensusImportCensusFileSaveButton();

                Test.Log(Status.Info, "Step 15: Verify import status is displaying");
                Assert.True(await PlansPage.verifyImportStatusIsVisible());

                Test.Log(Status.Info, "Step 16: Verify conversion complete status is displaying");
                Assert.True(await PlansPage.verifyConversionCompleteStatusIsVisible());

                Test.Log(Status.Info, "Step 17: click on Import Status Link");
                await PlansPage.clickOnImportStatusLink();
                
                Test.Log(Status.Info, "Step 18: Verify mapping page title is displaying");
                Assert.True(await PlansPage.verifyMappingPageTitleIsVisible());

                Test.Log(Status.Info, "Step 19: Map each file column with database column ");
                await PlansPage.mappingSSN(SSNOption);
                await PlansPage.mappingFullName(NameOption);
                await PlansPage.mappingEmployementStatus(EmploymentStatusOption);
                await PlansPage.mappingEmail(WorkEmailAddressOption);
                await PlansPage.mappingPhone(PhoneOption);
                await PlansPage.mappingFullAddress(AddressOption);
                await PlansPage.mappingCity(CityOption);
                await PlansPage.mappingState(StateOption);
                await PlansPage.mappingZip(ZipOption);
                await PlansPage.mappingBirthday(DateOfBirthOption);
                await PlansPage.mappingHiredDate(DateOfHireOption);
                await PlansPage.mappingRehiredDate(DateOfRehireOption);
                await PlansPage.mappingTerminationDate(TermDateOption);
                await PlansPage.mappingBalance(BalanceOption);
                await PlansPage.mappingEligibleForEscalation(EligibleForrEscalationOption);

                AllEmployeesRecords = await PlansPage.getCountOfAllEmployeesRecords().ConfigureAwait(false);
                AllExEmployees = await PlansPage.getCountOfAllExEmployees().ConfigureAwait(false);

                Test.Log(Status.Info, "Step 20: click on Mapping Next Button");
                await PlansPage.clickOnMappingNextButton();

                Test.Log(Status.Info, "Step 21: verify pre-processing complete is displaying");
                Assert.True(await PlansPage.verifyPreProcessingCompleteIsVisible());

                Test.Log(Status.Info, "Step 22: verify import button is displaying");
                Assert.True(await PlansPage.verifyImportButtonIsVisible());

                Test.Log(Status.Info, "Step 23: Verify all employees records are displaying: " + AllEmployeesRecords);
                Assert.True(await PlansPage.verifyAllNewEmployeesIsVisible(AllEmployeesRecords));

                Test.Log(Status.Info, "Step 24: Verify all ex-employees are displaying: " + AllExEmployees);
                Assert.True(await PlansPage.verifyAllExEmployeesIsVisible(AllExEmployees));

                Test.Log(Status.Info, "Step 25: click on Import Button");
                await PlansPage.clickOnImportButton();

                Test.Log(Status.Info, "Step 26: verify queued for import status is displaying");
                Assert.True(await PlansPage.verifyQueuedForImportStatusIsVisible());

                Test.Log(Status.Info, "Step 27: verify imported status is displaying");
                Assert.True(await PlansPage.verifyImportedStatusIsVisible());

                Test.Log(Status.Info, "Step 28: click on List Tab");
                await PlansPage.clickOnListTab();

                Test.Log(Status.Info, "Step 29: verify imported status is displaying");
                Assert.True(await PlansPage.verifyImportedFileIsVisible());
                

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
        public async Task TestVerifyThatAllImportedDataOfParticipantsStoresAndAvailableOnTheImportedParticipantsDetailPages()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);

            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var SSNOption = "SSN";
            var NameOption = "Name";
            var EmploymentStatusOption = "Employment Status";
            var WorkEmailAddressOption = "Work Email Address";
            var PhoneOption = "Phone";
            var AddressOption = "Address";
            var CityOption = "City";
            var StateOption = "State";
            var ZipOption = "Zip";
            var DateOfBirthOption = "Date of Birth";
            var DateOfHireOption = "Date of Hire";
            var DateOfRehireOption = "Date of Rehire";
            var TermDateOption = "Term Date";
            var BalanceOption = "Balance*";
            var EligibleForrEscalationOption = "Eligible forr Escalation?";
            int AllEmployeesRecords;
            int AllExEmployees;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1091";
                Test = Extent.CreateTest("Test Plan 1091: Verify that all imported data of participants stores and available on the imported participants detail pages", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying all imported data of participants stores and available on the imported participants detail pages</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Census Tab");
                await PlansPage.clickOnCensusTab();

                Test.Log(Status.Info, "Step 11: click on import census file button");
                await PlansPage.clickOnImportCensusFileButton();

                Test.Log(Status.Info, "Step 12: Verify new census file title is displaying");
                Assert.True(await PlansPage.verifyNewCensusFileTitleIsVisible());

                Test.Log(Status.Info, "Step 13: Upload New Census File");
                await PlansPage.UploadNewCensusFile();

                Test.Log(Status.Info, "Step 14: click on Census Tab");
                await PlansPage.clickOnCensusImportCensusFileSaveButton();

                Test.Log(Status.Info, "Step 15: Verify import status is displaying");
                Assert.True(await PlansPage.verifyImportStatusIsVisible());

                Test.Log(Status.Info, "Step 16: Verify conversion complete status is displaying");
                Assert.True(await PlansPage.verifyConversionCompleteStatusIsVisible());

                Test.Log(Status.Info, "Step 17: click on Import Status Link");
                await PlansPage.clickOnImportStatusLink();

                Test.Log(Status.Info, "Step 18: Verify mapping page title is displaying");
                Assert.True(await PlansPage.verifyMappingPageTitleIsVisible());

                Test.Log(Status.Info, "Step 19: Map each file column with database column ");
                await PlansPage.mappingSSN(SSNOption);
                await PlansPage.mappingFullName(NameOption);
                await PlansPage.mappingEmployementStatus(EmploymentStatusOption);
                await PlansPage.mappingEmail(WorkEmailAddressOption);
                await PlansPage.mappingPhone(PhoneOption);
                await PlansPage.mappingFullAddress(AddressOption);
                await PlansPage.mappingCity(CityOption);
                await PlansPage.mappingState(StateOption);
                await PlansPage.mappingZip(ZipOption);
                await PlansPage.mappingBirthday(DateOfBirthOption);
                await PlansPage.mappingHiredDate(DateOfHireOption);
                await PlansPage.mappingRehiredDate(DateOfRehireOption);
                await PlansPage.mappingTerminationDate(TermDateOption);
                await PlansPage.mappingBalance(BalanceOption);
                await PlansPage.mappingEligibleForEscalation(EligibleForrEscalationOption);

                AllEmployeesRecords = await PlansPage.getCountOfAllEmployeesRecords().ConfigureAwait(false);
                AllExEmployees = await PlansPage.getCountOfAllExEmployees().ConfigureAwait(false);

                Test.Log(Status.Info, "Step 20: click on Mapping Next Button");
                await PlansPage.clickOnMappingNextButton();

                Test.Log(Status.Info, "Step 21: verify pre-processing complete is displaying");
                Assert.True(await PlansPage.verifyPreProcessingCompleteIsVisible());

                Test.Log(Status.Info, "Step 22: verify import button is displaying");
                Assert.True(await PlansPage.verifyImportButtonIsVisible());

                Test.Log(Status.Info, "Step 23: Verify all employees records are displaying: " + AllEmployeesRecords);
                Assert.True(await PlansPage.verifyAllNewEmployeesIsVisible(AllEmployeesRecords));

                Test.Log(Status.Info, "Step 24: Verify all ex-employees are displaying: " + AllExEmployees);
                Assert.True(await PlansPage.verifyAllExEmployeesIsVisible(AllExEmployees));

                Test.Log(Status.Info, "Step 25: click on Import Button");
                await PlansPage.clickOnImportButton();

                Test.Log(Status.Info, "Step 26: verify queued for import status is displaying");
                Assert.True(await PlansPage.verifyQueuedForImportStatusIsVisible());

                Test.Log(Status.Info, "Step 27: verify imported status is displaying");
                Assert.True(await PlansPage.verifyImportedStatusIsVisible());

                Test.Log(Status.Info, "Step 28: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 29: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 30: Enter Participant Name Search Field :" + testPlanName);
                await particpantsPage.enterParticipantSearchField(testPlanName);

                Test.Log(Status.Info, "Step 31: Verify that the search participant: "+ testPlanName + " are displaying");
                Assert.True(await particpantsPage.VerifySearchParticipantNameIsVisible(testPlanName));                       


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
        public async Task TestVerifyThatUserIsAbleToCreateCampaignsFromThePlan()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var CampaignPage = new CampaignPage(page);
            var campaignName = "user " + GetRandomLastName(4);
            var particpantsPage = new ParticpantsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1139";
                Test = Extent.CreateTest("Test Plan 1139: Verify that user is able to create campaigns from the plan", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to create campaigns from the plan</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on campaigns tab");
                await PlansPage.clickOnCampaignsTab();

                Test.Log(Status.Info, "Step 11: click on create campaign Button");
                await PlansPage.clickOnCreateCampaignButton();

                Test.Log(Status.Info, "Step 12: Verify create a campaign popup title is displaying");
                Assert.True(await PlansPage.verifyCreateACampaignPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 13: Enter Create Campaign Name :" + campaignName);
                await PlansPage.enterNameInCreateCampaignPopup(campaignName);

                Test.Log(Status.Info, "Step 14: click on create campaign popup save Button");
                await PlansPage.clickOnCreateACampaignPopupSaveButton();

                Test.Log(Status.Info, "Step 15: click on campaigns site menu");
                await CampaignPage.clickOnCampaignsSiteMenu();

                Test.Log(Status.Info, "Step 16: Verify campaigns page title is displaying");
                Assert.True(await CampaignPage.verifyCampaignsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 17: Enter campaigns search value :" + campaignName);
                await CampaignPage.enterCampaignsSearchValue(campaignName);

                Test.Log(Status.Info, "Step 18: Verify campaigns search value "+ campaignName + " is displaying");
                await CampaignPage.verifyCampaignsSearchValueIsVisible(campaignName);
                Assert.True(await CampaignPage.verifyCampaignsSearchValueIsVisible(campaignName));


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
        public async Task TestVerifyThatAllImportedCensusFileShouldBeSavedInListPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                // Adding the link
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1140";
                Test = Extent.CreateTest("Test Plan 1140: Verify that all imported census file should be saved in list page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that all imported census file should be saved in page list</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 2: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 3: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 4: click on Plans Section");
                await PlansPage.clickOnPlansSection();

                Test.Log(Status.Info, "Step 5: click on Create Plan Button");
                await PlansPage.clickOnCreatePlanButton();

                Test.Log(Status.Info, "Step 6: Verify Create Plan A Popup Title is displaying");
                Assert.True(await PlansPage.verifyCreatePlanAPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Test Plan Name :" + testPlanName);
                await PlansPage.enterCreatePlanName(testPlanName);

                Test.Log(Status.Info, "Step 8: click on Plan A Popup Save Button");
                await PlansPage.clickOnCreatePlanAPopupSaveButton();

                Test.Log(Status.Info, "Step 9: Verify Plan Dashboard Title is displaying");
                Assert.True(await PlansPage.verifyPlanDashboardTitleIsVisible());

                Test.Log(Status.Info, "Step 10: click on Census Tab");
                await PlansPage.clickOnCensusTab();

                Test.Log(Status.Info, "Step 11: click on import census file button");
                await PlansPage.clickOnImportCensusFileButton();

                Test.Log(Status.Info, "Step 12: Verify new census file title is displaying");
                Assert.True(await PlansPage.verifyNewCensusFileTitleIsVisible());

                Test.Log(Status.Info, "Step 13: Upload New Census File");
                await PlansPage.UploadNewCensusFile();

                Test.Log(Status.Info, "Step 14: click on Census Tab");
                await PlansPage.clickOnCensusImportCensusFileSaveButton();

                Test.Log(Status.Info, "Step 15: Verify import status is displaying");
                Assert.True(await PlansPage.verifyImportStatusIsVisible());

                Test.Log(Status.Info, "Step 16: click on import census file button");
                await PlansPage.clickOnImportCensusFileButton();

                Test.Log(Status.Info, "Step 17: Verify new census file title is displaying");
                Assert.True(await PlansPage.verifyNewCensusFileTitleIsVisible());

                Test.Log(Status.Info, "Step 18: Upload New Census File");
                await PlansPage.UploadAnotherNewCensusFile();

                Test.Log(Status.Info, "Step 19: click on Census Tab");
                await PlansPage.clickOnCensusImportCensusFileSaveButton();

                Test.Log(Status.Info, "Step 20: Verify import status is displaying");
                Assert.True(await PlansPage.verifyImportStatusIsVisible());
                
                Test.Log(Status.Info, "Step 21: Verify files are sorted latest on top");
                await PlansPage.VerifyFilesAreListedLatestOnTop();

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