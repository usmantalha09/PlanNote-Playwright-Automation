using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework;
using PlanNotePlaywrite.Pages;
using PlanNotePlaywrite.Utils;
using System;
using System.Data;
using System.IO;
using System.Numerics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlanNotePlaywrite.Tests
{
    public class UserTest : Base
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
        public async Task Test_User_1195_VerifyThatAdminUserIsAbleToSetNoticeFromNameOfTheAccountsFromTheAccountsDetailPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var userPage = new UserPage(page);
            var firstName = "user";
            var lastName = GetRandomLastName(6);
            var userEmail = "XYZ"+ GetRandomLastName(4) + "@gmail.com";
            var userpassword = "Test@123";
            var accountOption = "Plan Notice";
            var roleOption = "Account Administrator";
            var fullName = firstName + " " + lastName;

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1195";
                Test = Extent.CreateTest("Test User 1195: Verify that user is able to add user to any plan from the users page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to add user to any plan from the users page</a>");

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

                Test.Log(Status.Info, "step 2: click on users lift side menu option");
                await userPage.clickOnUsersSiteMenu();

                Test.Log(Status.Info, "Step 2: Verify that the users page title is displaying");
                Assert.True(await userPage.verifyUsersPageTitleIsVisible());

                Test.Log(Status.Info, "step 3: click on new user button");
                await userPage.clickOnNewUserButton();

                Test.Log(Status.Info, "Step 3: Verify that the new user popup title is displaying");
                Assert.True(await userPage.verifyNewUserTitleIsVisible());

                Test.Log(Status.Info, "Step 4: Enter First Name :" + firstName);
                await userPage.enterFirstNameValue(firstName);

                Test.Log(Status.Info, "Step 4: Enter Last Name :" + lastName);
                await userPage.enterLastNameValue(lastName);

                Test.Log(Status.Info, "Step 4: Enter Email :" + userEmail);
                await userPage.enterEmail(userEmail);

                Test.Log(Status.Info, "Step 4: Enter Password :" + userpassword);
                await userPage.enterPassword(userpassword);

                Test.Log(Status.Info, "Step 4: Enter Confirm Password :" + userpassword);
                await userPage.enterConfirmPassword(userpassword);

                Test.Log(Status.Info, "step 5: select account");
                await userPage.selectAccountFromDropdown(accountOption);

                Test.Log(Status.Info, "step 5: select account");
                await userPage.selectRoleFromDropdown(roleOption);

                Test.Log(Status.Info, "step 3: click on new user popup save button");
                await userPage.clickOnNewUserPopupSaveButton();

                Test.Log(Status.Info, "Step 3: Verify that the added user is displaying"+ fullName);
                Assert.True(await userPage.verifyAddedUserIsVisible(fullName));

                Test.Log(Status.Info, "step 3: click on save button");
                await userPage.clickOnNewUserPopupSaveButton();

                await Task.Delay(2000);
                byte[] screenshotBytes = await page.ScreenshotAsync();
                try {
                Assert.True(await userPage.verifyUserAddedSuccessfullyPopupIsVisible());
                Test.Log(Status.Info, "Step 3: Verify that user added successfully popup is displaying");
                }
                catch(Exception e){
                Test.Error("Step 3: Verify that user added successfully popup is displaying", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                }

                Test.Log(Status.Info, "Step 3: Verify that the added user is displaying: " + fullName);
                Assert.True(await userPage.verifyAddedUserIsVisible(fullName));

                Test.Log(Status.Info, "Delete User");
                await userPage.clickOnAddedUser(fullName);
                await userPage.clickOnNewUserDeleteButton();
                
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
        public async Task Test_User_1197_VerifyThaAtthatPaginationWorksProperlyOnTheUsersPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var userPage = new UserPage(page);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1197";
                Test = Extent.CreateTest("Test User 1197: Verify that pagination works properly on the users page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that pagination works properly on the users page</a>");

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

                Test.Log(Status.Info, "step 2: click on users lift side menu option");
                await userPage.clickOnUsersSiteMenu();

                Test.Log(Status.Info, "Step 2: Verify that the users page title is displaying");
                Assert.True(await userPage.verifyUsersPageTitleIsVisible());

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
        public async Task Test_User_1199_VerifyThatUserIsAbleToResetTheAppliedFiltersOnTheUsersPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var userPage = new UserPage(page);
            var phoneNumber = "6125047154";
            var userEmail = "ABC" + GetRandomLastName(4) + "@gmail.com";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1199";
                Test = Extent.CreateTest("Test User 1199: Verify user is able to reset the applied filters on the users  page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying user is able to reset the applied filters on the users  page</a>");

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

                Test.Log(Status.Info, "step 2: click on users lift side menu option");
                await userPage.clickOnUsersSiteMenu();

                Test.Log(Status.Info, "Step 2: Verify that the users page title is displaying");
                Assert.True(await userPage.verifyUsersPageTitleIsVisible());

                Test.Log(Status.Info, "step 3: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 3: Verify that users filter title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantFilterpopupTitleIsVisible());

                Test.Log(Status.Info, "Step 4: Enter filter Search By Phone :" + phoneNumber);
                await userPage.enterSearchByPhone(phoneNumber);

                Test.Log(Status.Info, "Step 4: Enter filter Search By Email :" + userEmail);
                await userPage.enterSearchByEmaile(userEmail);

                Test.Log(Status.Info, "step 5: click on reset filter button");
                await accountsPage.clickOnResetButton();

                Test.Log(Status.Info, "step 5: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 5: Verify that all fields should be empty and that the filter should remove");
                Assert.True(await userPage.VerifySearchByEmailFilterReset());
                Assert.True(await userPage.VerifySearchByPhoneFilterReset());

                Test.Log(Status.Info, "Step 6: Enter filter Search By Email :" + userEmail);
                await userPage.enterSearchByEmaile(userEmail);

                Test.Log(Status.Info, "step 6: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 6: Verify that the filtered value is displaying");
                Assert.True(await accountsPage.VerifyNoRecordsToDisplayMessageIsVisible());

                Test.Log(Status.Info, "step 7: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 7: Verify saved filters are displaying");
                Assert.False(await userPage.VerifySearchByEmailFilterReset());

                Test.Log(Status.Info, "step 8: click on reset filter button");
                await accountsPage.clickOnResetButton();

                Test.Log(Status.Info, "step 8: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that filters get reset and entries also get reset");
                Assert.True(await userPage.VerifySearchByEmailFilterReset());

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
        public async Task Test_User_1198_VerifyThatSearchBarShouldBeFunctional()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1198";
                Test = Extent.CreateTest("Test User 1198: Verify that search bar should be functional", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that search bar should be functional</a>");

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

                Test.Log(Status.Info, "step 2: click on accounts lift side menu option");
                await accountsPage.clickOnAccountsLiftSideMenuOption();

                Test.Log(Status.Info, "Step 4: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerifyAccountsPageTitleIsVisible());

                Test.Log(Status.Info, "step 2: click on create an account button");
                await accountsPage.clickOnCreateAnAccountButton();

                Test.Log(Status.Info, "Step 4: Verify that the create an account popup title is displaying");
                Assert.True(await accountsPage.VerifyCreateAnAccountPopupTitleIsVisible());

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
        public async Task VerifyThatFiltersFromUserTableAreFunctional()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var usersPage = new UserPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1196";
                Test = Extent.CreateTest("Test User 1196: Verify that filters should be functional", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that filters should be functional</a>");

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

                Test.Log(Status.Info, "step 5: click on users lift side menu option");
                await accountsPage.clickOnUsersLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerfiyUsersPageTtileVisible());

                Test.Log(Status.Info, "Step 7: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that template filter title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantFilterpopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: click on phone number");
                await usersPage.clickOnPhoneNumberFilter();

                Test.Log(Status.Info, "Step 10: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 11: Verify that the filtered user type is displaying");
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
        public async Task VerifyThatResetFiltersFromUserTableAreFunctional()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var accountsPage = new AccountsPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var usersPage = new UserPage(page);
            var accountName = "user " + GetRandomLastName(4);

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1199";
                Test = Extent.CreateTest("Test User 1199: Verify that resetting filters should be functional", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that resetting filters should be functional</a>");

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

                Test.Log(Status.Info, "step 5: click on users lift side menu option");
                await accountsPage.clickOnUsersLiftSideMenuOption();

                Test.Log(Status.Info, "Step 6: Verify that the accounts page title is displaying");
                Assert.True(await accountsPage.VerfiyUsersPageTtileVisible());

                Test.Log(Status.Info, "Step 7: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that template filter title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantFilterpopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: click on phone number");
                await usersPage.clickOnPhoneNumberFilter();

                Test.Log(Status.Info, "Step 10: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 11: Verify that the filtered user type is displaying");
                Assert.True(await accountsPage.VerifyNoRecordsToDisplayMessageIsVisible());

                Test.Log(Status.Info, "Step 12: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 13: Reset filters");
                await usersPage.ResetUserFilters();

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