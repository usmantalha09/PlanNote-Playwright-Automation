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
    public class AccountsTest : Base
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
                Test = Extent.CreateTest("Test Account 1078: Verify that admin user is able to set notice from name of the accounts from the accounts detail page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that admin user is able to set notice from name of the accounts from the accounts detail page</a>");

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
        public async Task TestVerifyThatUserIsAbleToAddAccountSuccessfully()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1144";
                Test = Extent.CreateTest("Test Account 1144:  Verify that user is able to add account successfully", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to add account successfully</a>");

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

                Test.Log(Status.Info, "Step 9: Enter create account name :" + accountName);
                await accountsPage.enterCreateAnAccountName(accountName);

                Test.Log(Status.Info, "Step 10: select Service Provider Option From Dropdown");
                await accountsPage.selectServiceProviderOptionFromDropdown();

                Test.Log(Status.Info, "Step 11: click create an account save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 12: Verify that account details page title is displaying");
                Assert.True(await accountsPage.VerifyAccountDetailsPageTitleIsVisible());                

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
        public async Task TestVerifyThatSearchBarShouldBeFunctional()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var search = "No records to display Text";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1145";
                Test = Extent.CreateTest("Test Account 1145: Verify that search bar should be functional", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that search bar should be functional</a>");

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

                Test.Log(Status.Info, "step 5: click on accounts lift side menu option");
                await accountsPage.clickOnAccountsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerifyAccountsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: Enter Value In Account Search :" + search);
                await accountsPage.enterValueInAccountSearch(search);

                Test.Log(Status.Info, "Step 8: Verify that the <b>No Records To Display</b> message is displaying");
                await accountsPage.VerifyNoRecordsToDisplayMessageIsVisible();
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
        public async Task TestVerifythatTheFiltersOfAccountPageShouldBeWorkingCorrectly()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var accountType = "";
            var fromNumber = "1";
            var toNumber = "10";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1146";
                Test = Extent.CreateTest("Test Account 1146: Verify that the filters of account page should be working correctly", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that the filters of account page should be working correctly</a>");

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

                Test.Log(Status.Info, "step 5: click on accounts lift side menu option");
                await accountsPage.clickOnAccountsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerifyAccountsPageTitleIsVisible());

                Test.Log(Status.Info, "step 7: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that the filter popup is displaying");
                Assert.True(await accountsPage.VerifyFilterPopupIsVisible());

                Test.Log(Status.Info, "Step 9: select account type");
                accountType = await accountsPage.selectAccountType();

                Test.Log(Status.Info, "Step 10: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 11: Verify that the filtered account type is displaying: " + accountType);
                Assert.True(await accountsPage.VerifyAccountTypeIsVisible(accountType));

                Test.Log(Status.Info, "Step 12: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();
                await accountsPage.clickOnResetButton();
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 13: Verify that the filter popup is displaying");
                Assert.True(await accountsPage.VerifyFilterPopupIsVisible());

                Test.Log(Status.Info, "Step 14: Enter associated plans number From :" + fromNumber + " and To: " + toNumber);
                await accountsPage.enterAssociatedPlansNumberFromAndTo(fromNumber, toNumber);

                Test.Log(Status.Info, "Step 15: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 16: Verify that the filtered associated plans number is displaying: " + "between "+ fromNumber+ " and "+ toNumber);
                Assert.True(await accountsPage.VerifyAssociatedPlansNumberIsVisible(fromNumber, toNumber));

                Test.Log(Status.Info, "Step 17: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();
                await accountsPage.clickOnResetButton();
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 18: Verify that the filter popup is displaying");
                Assert.True(await accountsPage.VerifyFilterPopupIsVisible());

                Test.Log(Status.Info, "Step 19: Enter users number From :" + fromNumber + " and To: " + toNumber);
                await accountsPage.enterUsersNumberFromAndTo(fromNumber, toNumber);

                Test.Log(Status.Info, "Step 20: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 21: Verify that the filtered users number is displaying: " + "between " + fromNumber + " and " + toNumber);
                Assert.True(await accountsPage.VerifyUsersNumberIsVisible(fromNumber, toNumber));

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
        public async Task TestVerifyThatUserIsAbleToResetTheAppliedFilters()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1147";
                Test = Extent.CreateTest("Test Account 1147: verify user is able to reset the applied filters", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that the user is able to reset the applied filters</a>");

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

                Test.Log(Status.Info, "Step 7: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that the filter popup is displaying");
                Assert.True(await accountsPage.VerifyFilterPopupIsVisible());

                Test.Log(Status.Info, "Step 9: select account type");
                await accountsPage.selectAccountType();

                Test.Log(Status.Info, "Step 10: click on reset filter button");
                await accountsPage.clickOnResetButton();

                Test.Log(Status.Info, "Step 11: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 12: Verify that all fields should be empty and that the filter should remove");
                Assert.True(await accountsPage.verifyAccountTypeFilterReset());
                Assert.True(await accountsPage.VerifyAssociatedPlansNumberFilterReset());
                Assert.True(await accountsPage.VerifyUsersNumberFilterReset());

                Test.Log(Status.Info, "Step 13: select account type");
                await accountsPage.selectAccountType();

                Test.Log(Status.Info, "Step 14: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 15: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 16: click on reset filter button");
                await accountsPage.clickOnResetButton();

                Test.Log(Status.Info, "Step 17: click on account filter button");
                await accountsPage.clickOnAccountFilterButton();

                Test.Log(Status.Info, "Step 18: Verify that all fields should be empty and that the filter should remove");
                Assert.True(await accountsPage.verifyAccountTypeFilterReset());
                Assert.True(await accountsPage.VerifyAssociatedPlansNumberFilterReset());
                Assert.True(await accountsPage.VerifyUsersNumberFilterReset());

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
        public async Task TestVerifythatPaginationWorksProperlyOnTheAccountsPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1148";
                Test = Extent.CreateTest("Test Account 1148: Verify that pagination works properly on the Accounts page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that pagination works properly on the Accounts page</a>");

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

                Test.Log(Status.Info, "Step 7: Verify that the Forward Go To Next Page Pagination Work Properly");
                Assert.True(await accountsPage.VerifyForwardGoToNextPagePaginationWorkProperly());

                Test.Log(Status.Info, "Step 8: Verify that the Backward Go To Previous Page Pagination Work Properly");
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
        public async Task TestVerifythatAdminIsAbleToAssociatePlanWithAccount()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1317";
                Test = Extent.CreateTest("Test Account 1317: Verify that admin can associate plan with Account", $"<a href=\"{link}\" target=\"_blank\">As an admin, I am verifying that I can associate plan with Account</a>");

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

                Test.Log(Status.Info, "Step 9: Enter create account name :" + accountName);
                await accountsPage.enterCreateAnAccountName(accountName);

                Test.Log(Status.Info, "Step 10: select Service Provider Option From Dropdown");
                await accountsPage.selectServiceProviderOptionFromDropdown();

                Test.Log(Status.Info, "Step 11: click create an account save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 12: Verify that account details page title is displaying");
                Assert.True(await accountsPage.VerifyAccountDetailsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 13: click on plan tab");
                await accountsPage.clickOnPlanTab();

                Test.Log(Status.Info, "Step 14: Verify that assocate button is visible");
                Assert.True(await accountsPage.VerifyAssociateButtonIsVisible());

                Test.Log(Status.Info, "Step 15: click on associate button");
                await accountsPage.clickOnAssociateButton();

                Test.Log(Status.Info, "Step 16: Verfiy that associate plan pop up is displaying");
                Assert.True(await accountsPage.VerifyAssociatePlanPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 17: Select plan from downdown");
                var PlanSelected = await accountsPage.selectPlanFromDropdown();
                Test.Log(Status.Info, PlanSelected);

                Test.Log(Status.Info, "Status 18: Click on save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 19: Verify Plan associated with account");
                Assert.True(await accountsPage.VerifyPlanAssociatedWithAccount(PlanSelected));

                Test.Log(Status.Info, "Step 20: Delete test data");
                await accountsPage.DeleteTestAccount();

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
        public async Task TestVerifythatAdminIsAbleToUnassociatePlanWithAccount()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1318";
                Test = Extent.CreateTest("Test Account 1318: Verify that admin can unassociate plan with Account", $"<a href=\"{link}\" target=\"_blank\">As an admin, I am verifying that I can unassociate plan with Account</a>");

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

                Test.Log(Status.Info, "Step 9: Enter create account name :" + accountName);
                await accountsPage.enterCreateAnAccountName(accountName);

                Test.Log(Status.Info, "Step 10: select Service Provider Option From Dropdown");
                await accountsPage.selectServiceProviderOptionFromDropdown();

                Test.Log(Status.Info, "Step 11: click create an account save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 12: Verify that account details page title is displaying");
                Assert.True(await accountsPage.VerifyAccountDetailsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 13: click on plan tab");
                await accountsPage.clickOnPlanTab();

                Test.Log(Status.Info, "Step 14: Verify that assocate button is visible");
                Assert.True(await accountsPage.VerifyAssociateButtonIsVisible());

                Test.Log(Status.Info, "Step 15: click on associate button");
                await accountsPage.clickOnAssociateButton();

                Test.Log(Status.Info, "Step 16: Verfiy that associate plan pop up is displaying");
                Assert.True(await accountsPage.VerifyAssociatePlanPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 17: Select plan from downdown");
                var PlanSelected = await accountsPage.selectPlanFromDropdown();
                Test.Log(Status.Info, PlanSelected);

                Test.Log(Status.Info, "Status 18: Click on save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 19: Verify Plan associated with account");
                Assert.True(await accountsPage.VerifyPlanAssociatedWithAccount(PlanSelected));

                Test.Log(Status.Info, "Step 20: Unassocaite Linked Plan");
                await accountsPage.UnassocaiteLinkedPlan();

                Test.Log(Status.Info, "Step 21: Delete test data");
                await accountsPage.DeleteTestAccount();

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
        public async Task TestVerifythatAdminIsAbleToAssociateUserWithAccount()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1313";
                Test = Extent.CreateTest("Test Account 1313: Verify that admin can associate user with Account", $"<a href=\"{link}\" target=\"_blank\">As an admin, I am verifying that I can associate user with Account</a>");

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

                Test.Log(Status.Info, "Step 9: Enter create account name :" + accountName);
                await accountsPage.enterCreateAnAccountName(accountName);

                Test.Log(Status.Info, "Step 10: select Service Provider Option From Dropdown");
                await accountsPage.selectServiceProviderOptionFromDropdown();

                Test.Log(Status.Info, "Step 11: click create an account save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 12: Verify that account details page title is displaying");
                Assert.True(await accountsPage.VerifyAccountDetailsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 13: click on user tab");
                await accountsPage.clickOnUserTab();

                Test.Log(Status.Info, "Step 14: Verify that assocate user button is visible");
                Assert.True(await accountsPage.VerifyAssocateUserButtonIsVisible());

                Test.Log(Status.Info, "Step 15: click on associate button");
                await accountsPage.clickOnAssociateButton();

                Test.Log(Status.Info, "Step 16: Verfiy that associate user pop up is displaying");
                Assert.True(await accountsPage.VerifyAssociatePlanPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 17: Select user from downdown");
                var UserSelected = await accountsPage.selectUserFromDropdown();

                Test.Log(Status.Info, "Step 18: click on save button");
                await accountsPage.clickOnCreateUserAssociation();

                Test.Log(Status.Info, "Step 19: Verify User associated with account");
                Assert.True(await accountsPage.VerifyUserAssociatedWithAccount(UserSelected));

                Test.Log(Status.Info, "Step 20: Delete test data");
                await accountsPage.DeleteTestAccount();
     
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

        /* Blocked by: Add a whitelist an email, invite that specific user
        [Test]
        public async Task TestVerifythatAdminIsAbleToInviteUserInAnAccount()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1314";
                Test = Extent.CreateTest("Test Account 1314: Verify that admin can invite user in an Account", $"<a href=\"{link}\" target=\"_blank\">As an admin, I am verifying that I can invite user in an Account</a>");

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

                Test.Log(Status.Info, "Step 9: Enter create account name :" + accountName);
                await accountsPage.enterCreateAnAccountName(accountName);

                Test.Log(Status.Info, "Step 10: select Service Provider Option From Dropdown");
                await accountsPage.selectServiceProviderOptionFromDropdown();

                Test.Log(Status.Info, "Step 11: click create an account save button");
                await accountsPage.clickOnCreateAnAccountSaveButton();

                Test.Log(Status.Info, "Step 12: Verify that account details page title is displaying");
                Assert.True(await accountsPage.VerifyAccountDetailsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 13: click on user tab");
                await accountsPage.clickOnUserTab();

                Test.Log(Status.Info, "Step 14: Verify that assocate user button is visible");
                Assert.True(await accountsPage.VerifyAssocateUserButtonIsVisible());

                Test.Log(Status.Info, "Step 15: click on associate button");
                await accountsPage.clickOnAssociateButton();

                Test.Log(Status.Info, "Step 16: Verfiy that associate user pop up is displaying");
                Assert.True(await accountsPage.VerifyAssociatePlanPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 17: Select user from downdown");
                var UserSelected = await accountsPage.selectUserFromDropdown();

                Test.Log(Status.Info, "Step 18: click on save button");
                await accountsPage.clickOnCreateUserAssociation();

                Test.Log(Status.Info, "Step 19: Verify User associated with account");
                Assert.True(await accountsPage.VerifyUserAssociatedWithAccount(UserSelected));

                Test.Log(Status.Info, "Step 20: Delete test data");
                await accountsPage.DeleteTestAccount();
                
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

    }
}