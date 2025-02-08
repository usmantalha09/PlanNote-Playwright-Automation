using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlanNotePlaywrite.Pages;
using PlanNotePlaywrite.Utils;
using RazorEngine.Templating;
using System;
using System.IO;
using System.Numerics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlanNotePlaywrite.Tests
{
    public class TemplateTest : Base
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
            var templatePage = new TemplatePage(page);
            var campaignPage = new CampaignPage(page);
            var templateName = "user " + GetRandomLastName(4);
            var stepType = "Email";
            var emailType = "Email Step";
            var emailSubject = "Test Subject";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1186";
                Test = Extent.CreateTest("Test Template 1186: Verify that user is able to add template successfully", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to add template successfully</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 1: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 1: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 1: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 2: click on templates lift side menu option");
                await templatePage.clickOnTemplatesLiftSideMenuOption();

                Test.Log(Status.Info, "Step 2: Verify that the template page title is displaying");
                Assert.True(await templatePage.verifyTemplatePageTitleIsVisible());

                Test.Log(Status.Info, "step 3: click on create an template button");
                await templatePage.clickOnNewTemplateButton();
               
                Test.Log(Status.Info, "Step 4: Enter Template Name :" + templateName);
                await templatePage.enterTemplateName(templateName);

                Test.Log(Status.Info, "Step 4: Select Step Type Dropdown Option :" + stepType);
                await templatePage.selectStepTypeDropdownOption(stepType);

                Test.Log(Status.Info, "Step 4: Select Email Type Dropdown Option :" + emailType);
                await templatePage.selectEmailTypeDropdownOption(emailType);

                Test.Log(Status.Info, "Step 4: Enter Email Subject :" + emailSubject);
                await templatePage.enterEmailSubject(emailSubject);

                Test.Log(Status.Info, "Step 4: Select Account Dropdown First Option");
                await templatePage.selectAccountDropdownFirstOption();

                Test.Log(Status.Info, "step 4: Upload Template");
                await templatePage.UploadTemplate();

                Test.Log(Status.Info, "step 5: click on template save button");
                await templatePage.clickOnTemplateSaveButton();

                await Task.Delay(2000);
                byte[] screenshotBytes = await page.ScreenshotAsync();
                try
                {
                    Assert.True(await templatePage.verifyTemplateAddedSuccessfullyPopupIsVisible());
                    Test.Log(Status.Info, "Step 5: Verify that the template added successfully popup is displaying");
                }
                catch (Exception e)
                {
                    Test.Error("Step 5: Verify that the template added successfully popup is displaying", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                }

                Test.Log(Status.Info, "Step 5: Enter valid search value :" + templateName);
                await campaignPage.enterCampaignsSearchValue(templateName);

                Test.Log(Status.Info, "Step 5: Verify that the added template is displaying: "+ templateName);
                await templatePage.verifyTemplatePageTitleIsVisible();
                Assert.True(await templatePage.verifyAddedTemplateIsVisible(templateName));

                Test.Log(Status.Info, "Delete added template");
                await templatePage.clickOAddedTemplateName(templateName);
                await templatePage.clickOnTemplateDeleteButton();
                await templatePage.verifyTemplatePageTitleIsVisible();

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
        public async Task TestVerifyThatPaginationWorksProperlyOnTheTemplatesPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var templatePage = new TemplatePage(page);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1187";
                Test = Extent.CreateTest("Test Template 1187: Verify that pagination works properly on the Templates page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that pagination works properly on the Templates page</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 1: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 1: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 1: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 2: click on templates lift side menu option");
                await templatePage.clickOnTemplatesLiftSideMenuOption();

                Test.Log(Status.Info, "Step 2: Verify that the template page title is displaying");
                Assert.True(await templatePage.verifyTemplatePageTitleIsVisible());

                Test.Log(Status.Info, "Step 3: Verify that the Forward Go To Next Page Pagination Work Properly");
                Assert.True(await accountsPage.VerifyForwardGoToNextPagePaginationWorkProperly());

                Test.Log(Status.Info, "Step 4: Verify that the Backward Go To Previous Page Pagination Work Properly");
                Assert.True(await accountsPage.VerifyBackwardGoToPreviousPagePaginationWorkProperly());

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
        public async Task TestVerifyThatTheFiltersOfTemplatesPageShouldBeWorkingCorrectly()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var templatePage = new TemplatePage(page);
            var particpantsPage = new ParticpantsPage(page);
            var stepTypeOption = "Email";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1179";
                Test = Extent.CreateTest("Test Template 1179: Verify that the filters of Templates page should be working correctly", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that the filters of Templates page should be working correctly</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await loadURL(page, Constants.BaseUrl);

                Test.Log(Status.Info, "Step 1: Verify that the Remember Me Checkbox, Forgot Password Link, Password Input field and Email Input field are displaying");
                Assert.True(await loginPage.verifyRememberMeCheckboxIsVisible());
                Assert.True(await loginPage.verifyForgotpasswordLinkIsVisible());
                Assert.True(await loginPage.verifyPasswordInputXPathIsVisible());
                Assert.True(await loginPage.verifyEmailInputXPathIsVisible());

                Test.Log(Status.Info, "Step 1: Enter Credentials: Email :" + email + " Password: " + password);
                await loginPage.enterLoginCredentials(email, password);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 1: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "step 2: click on templates lift side menu option");
                await templatePage.clickOnTemplatesLiftSideMenuOption();

                Test.Log(Status.Info, "Step 2: Verify that the template page title is displaying");
                Assert.True(await templatePage.verifyTemplatePageTitleIsVisible());

                Test.Log(Status.Info, "step 2: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 4: Verify that template filter title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantFilterpopupTitleIsVisible());

                Test.Log(Status.Info, "step 6: select account");
                await templatePage.selectAccount();

                Test.Log(Status.Info, "step 6: Select Step Type:"+ stepTypeOption);
                await templatePage.selectStepType(stepTypeOption);

                Test.Log(Status.Info, "step 6: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 2: Verify that the filtered account type is displaying");
                Assert.True(await accountsPage.VerifyNoRecordsToDisplayMessageIsVisible());

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
        public async Task TestVerifyThatUserIsAbleToResetAppliedFiltersOfTemplatesPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var templatePage = new TemplatePage(page);
            var particpantsPage = new ParticpantsPage(page);
            var stepTypeOption = "Email";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1180";
                Test = Extent.CreateTest("Test Template 1180: Verify that the user is able to reset applied filters on Template page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that the resetting filters of Templates page should be working correctly</a>");

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

                Test.Log(Status.Info, "Step 5: click on templates lift side menu option");
                await templatePage.clickOnTemplatesLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the template page title is displaying");
                Assert.True(await templatePage.verifyTemplatePageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that template filter title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantFilterpopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: select account");
                await templatePage.selectAccount();

                Test.Log(Status.Info, "Step 10: Select Step Type:" + stepTypeOption);
                await templatePage.selectStepType(stepTypeOption);

                Test.Log(Status.Info, "Step 11: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 12: Verify that the filtered account type is displaying");
                Assert.True(await accountsPage.VerifyNoRecordsToDisplayMessageIsVisible());

                Test.Log(Status.Info, "Step 13: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Status 14: Reset filters");
                await templatePage.ResetTemplateFilters();

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
        public async Task TestVerifyThatTheSearchbarOfTemplatesPageShouldBeWorkingCorrectly()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var templatePage = new TemplatePage(page);
            var campaignPage = new CampaignPage(page);
            var templateName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1178";
                Test = Extent.CreateTest("Test Template 1178: Verify that the searchbar of Templates page should be working correctly", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that the searchbar of Templates page should be working correctly</a>");

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

                Test.Log(Status.Info, "Step 5: click on templates lift side menu option");
                await templatePage.clickOnTemplatesLiftSideMenuOption();

                Test.Log(Status.Info, "Step 7: Verify that the template page title is displaying");
                Assert.True(await templatePage.verifyTemplatePageTitleIsVisible());

                Test.Log(Status.Info, "Step 8: Enter valid search value :" + templateName);
                await campaignPage.enterCampaignsSearchValue(templateName);

                Test.Log(Status.Info, "Step 9: Verify that the filtered account type is displaying");
                Assert.True(await accountsPage.VerifyNoRecordsToDisplayMessageIsVisible());

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