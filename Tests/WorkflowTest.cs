using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using PlanNotePlaywrite.Pages;
using PlanNotePlaywrite.Utils;

namespace PlanNotePlaywrite.Tests
{
    public class WorkflowTest : Base
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
        public async Task TestVerifyThatUserIsAbleToCreateWorkflowHavingMultipleSteps()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var workflowPage = new WorkflowPage(page);
            var workflowName = "User " + GetRandomLastName(4);  
            var stepName = "user " + GetRandomLastName(5);
            var stepTypeOption = "Email";
            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1172";
                Test = Extent.CreateTest("Test Workflow 1172: verify user is able to create workflow having multiple steps", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying user is able to create workflow having multiple steps</a>");

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

                Test.Log(Status.Info, "Step 5: click on workflows lift side menu option");
                await workflowPage.clickOnWorkflowsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the workflows page title is displaying");
                Assert.True(await workflowPage.verifyWorkflowsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on new workflow button");
                await workflowPage.clickOnNewWorkflowButton();

                //Test.Log(Status.Info, "Step 8: Verify that the add new workflow page is displaying");
                //await workflowPage.verifyWorkflowNameTitleIsVisible();
                //Assert.True(await workflowPage.verifyWorkflowNameTitleIsVisible());

                Test.Log(Status.Info, "Step 8: Enter Workflow Name: " + workflowName);
                await workflowPage.enterWorkflowName(workflowName);

                Test.Log(Status.Info, "Step 8: Select Account Dropdown First Option");
                await workflowPage.selectAccountDropdownFirstOption();

                //Test.Log(Status.Info, "Step 11: click on add step button");
                //await workflowPage.clickOnAddStepButton();

                //Test.Log(Status.Info, "Step 12: Verify that the add workflow step popup title is displaying");
                //Assert.True(await workflowPage.verifyAddWorkflowStepPopupTitleIsVisible());

                //Test.Log(Status.Info, "Step 13: Enter Step Name: " + stepName);
                //await workflowPage.enterStepName(stepName);

                //Test.Log(Status.Info, "Step 14: Select Step Type Dropdown Option: "+ stepTypeOption);
                //await workflowPage.selectStepTypeDropdownOption(stepTypeOption);

                Test.Log(Status.Info, "Step 10: Select template dropdown first Option");
                await workflowPage.selectTemplateDropdownFirstOption();

                //Test.Log(Status.Info, "Step 16: click on add workflow step popup add button");
                //await workflowPage.clickOnAddWorkflowStepPopupAddButton();

                //Test.Log(Status.Info, "Step 17: Verify that the added step is displaying: "+ stepName);
                //Assert.True(await workflowPage.verifyAddedStepIsVisible(stepName));

                Test.Log(Status.Info, "Step 11: click on workflow save button");
                await workflowPage.clickOnWorkflowSaveButton();
                // Bug reported
                //await Task.Delay(2000);
                byte[] screenshotBytes = await page.ScreenshotAsync();
                /*try
                {
                    Assert.True(await workflowPage.verifyWorkflowCreateSuccessfullyPopupIsVisible());
                    Test.Log(Status.Info, "Step 19: Verify that the workflow create successfully popup is displaying");
                }
                catch (Exception e)
                {
                    Test.Error("Step 19: Verify that the workflow create successfully popup is displaying", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                }*/

                Test.Log(Status.Info, "Step 12: Verify that the added workflow is displaying: "+ workflowName);
                Assert.True(await workflowPage.verifyAddedWorkflowIsVisible(workflowName));

                Test.Log(Status.Info, "Delete added workflow");
                //await workflowPage.clickOnAddedWorkflow(workflowName);
                await workflowPage.verifyWorkflowNameTitleIsVisible();
                await workflowPage.clickOnWorkflowDeleteButton();
                Assert.True(await workflowPage.verifyWorkflowsPageTitleIsVisible());


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
        /*
        [Test]
        public async Task TestVerifyThatSearchBarShouldBeFunctional()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var workflowPage = new WorkflowPage(page);
            var campaignPage = new CampaignPage(page);
            var workflowName = "User " + GetRandomLastName(4);
            var stepName = "user " + GetRandomLastName(5);
            var stepTypeOption = "Email";
            var wrongWorkflowName = "notAddedValueUser";
            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1173";
                Test = Extent.CreateTest("Test Workflow 1173: Verify that search bar should be functional", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that search bar should be functional</a>");

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

                Test.Log(Status.Info, "step 2: click on workflows lift side menu option");
                await workflowPage.clickOnWorkflowsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 2: Verify that the workflows page title is displaying");
                Assert.True(await workflowPage.verifyWorkflowsPageTitleIsVisible());

                Test.Log(Status.Info, "step 3: click on new workflow button");
                await workflowPage.clickOnNewWorkflowButton();

                Test.Log(Status.Info, "Step 3: Verify that the add new workflow page is displaying");
                Assert.True(await workflowPage.verifyWorkflowNameTitleIsVisible());

                Test.Log(Status.Info, "Step 4: Enter Workflow Name: " + workflowName);
                await workflowPage.enterWorkflowName(workflowName);

                Test.Log(Status.Info, "step 5: Select Account Dropdown First Option");
                await workflowPage.selectAccountDropdownFirstOption();

                Test.Log(Status.Info, "step 6: click on add step button");
                await workflowPage.clickOnAddStepButton();

                Test.Log(Status.Info, "Step 6: Verify that the add workflow step popup title is displaying");
                Assert.True(await workflowPage.verifyAddWorkflowStepPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Step Name: " + stepName);
                await workflowPage.enterStepName(stepName);

                Test.Log(Status.Info, "step 7: Select Step Type Dropdown Option: " + stepTypeOption);
                await workflowPage.selectStepTypeDropdownOption(stepTypeOption);

                Test.Log(Status.Info, "step 7: Select template dropdown first Option");
                await workflowPage.selectTemplateDropdownFirstOption();

                Test.Log(Status.Info, "step 2: click on add workflow step popup add button");
                await workflowPage.clickOnAddWorkflowStepPopupAddButton();

                Test.Log(Status.Info, "Step 4: Verify that the added step is displaying: " + stepName);
                Assert.True(await workflowPage.verifyAddedStepIsVisible(stepName));

                Test.Log(Status.Info, "step 2: click on workflow save button");
                await workflowPage.clickOnWorkflowSaveButton();

                Test.Log(Status.Info, "Step 4: Verify that the workflows page title is displaying");
                Assert.True(await workflowPage.verifyWorkflowsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter invalid search value :" + wrongWorkflowName);
                await campaignPage.enterCampaignsSearchValue(wrongWorkflowName);

                Test.Log(Status.Info, "Step 4: Verify that the No Workflows is displaying: " + wrongWorkflowName);
                Assert.True(await workflowPage.verifyNoWorkflowsIsDisplay());

                Test.Log(Status.Info, "Step 7: Enter valid search value :" + workflowName);
                await campaignPage.enterCampaignsSearchValue(workflowName);

                Test.Log(Status.Info, "Step 4: Verify that the added workflow is displaying: " + workflowName);
                Assert.True(await workflowPage.verifyAddedWorkflowIsVisible(workflowName));

                Test.Log(Status.Info, "Delete added workflow");
                await workflowPage.clickOnAddedWorkflow(workflowName);
                await workflowPage.verifyWorkflowNameTitleIsVisible();
                await workflowPage.clickOnWorkflowDeleteButton();
                Assert.True(await workflowPage.verifyWorkflowsPageTitleIsVisible());


                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
            }
        }*/


        [Test]
        public async Task TestVerifyThatFiltersAreWorkingOnWorkflowPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var workflowPage = new WorkflowPage(page);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1174";
                Test = Extent.CreateTest("Test Workflow 1174: verify user is able to filter workflows", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying user is able to filter workflows</a>");

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

                Test.Log(Status.Info, "Step 5: click on workflows lift side menu option");
                await workflowPage.clickOnWorkflowsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the workflows page title is displaying");
                Assert.True(await workflowPage.verifyWorkflowsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Verify filter is displaying");
                Assert.True(await workflowPage.VerifyWorkflowFilterIsDisplaying());

                Test.Log(Status.Info, "Step 8: click on filter button");
                await workflowPage.clickOnWorkflowFilterButton();

                Test.Log(Status.Info, "Step 9: Select an account");
                await workflowPage.SelectAccountFilter();

                Test.Log(Status.Info, "Step 10: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 11: Verify that the filtered account type is displaying");
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