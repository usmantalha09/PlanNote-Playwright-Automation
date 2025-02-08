using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PlanNotePlaywrite.Pages;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlanNotePlaywrite.Tests
{
    public class ParticpantsTest : Base
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
        public async Task TestVerifythatTheSystemAccountSuperUserSystemAccountAdminSystemAccountManagerRolesAreAbleToDeleteTheParticipantContactsPoints()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var participantFirstName = "test";
            var participantLastName = "user " + GetRandomLastName(4);
            var participantPhone = "4155550111";
            var participantEmail = "adnan.qat1234@gmail.com";
            var participantAddressOne = "540 Howard St";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1074";
                Test = Extent.CreateTest("Test Particpants 1074: Verify that the SystemAccount.SuperUser, SystemAccount.Admin, SystemAccount.Manager roles are able to delete the Participant contacts points (email, phone, address)", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that the SystemAccount.SuperUser, SystemAccount.Admin, SystemAccount.Manager roles are able to delete the Participant contacts points (email, phone, address) </a>");

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on add participant button");
                await particpantsPage.clickOnAddParticipantButton();

                Test.Log(Status.Info, "Step 8: Verify that the add participant popup title is displaying");
                Assert.True(await particpantsPage.VerifyAddParticipantPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: Enter Participant First Name :" + participantFirstName);
                await particpantsPage.enterParticipantFirstName(participantFirstName);

                Test.Log(Status.Info, "Step 10: Enter Participant Last Name :" + participantLastName);
                await particpantsPage.enterParticipantLastName(participantLastName);

                Test.Log(Status.Info, "Step 11: Enter Participant phone :" + participantPhone);
                await particpantsPage.enterParticipantPhone(participantPhone);

                Test.Log(Status.Info, "Step 12: Enter Participant Email :" + participantEmail);
                await particpantsPage.enterParticipantEmail(participantEmail);

                Test.Log(Status.Info, "Step 13: Enter Participant Address One :" + participantAddressOne);
                await particpantsPage.enterParticipantAddressOne(participantAddressOne);

                Test.Log(Status.Info, "Step 14: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 15: click on participant contact button");
                await particpantsPage.clickOnParticipantsContactTab();

                Test.Log(Status.Info, "Step 16: Verify that the phone number delete option is displaying");
                Assert.True(await particpantsPage.VerifyPhoneNumberDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 17: Verify that the Email delete option is displaying");
                Assert.True(await particpantsPage.VerifyEmailDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 18: Verify that the Address Line delete option is displaying");
                Assert.True(await particpantsPage.VerifyAddressLineDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 19: click on phone number delete option");
                await particpantsPage.clickOnPhoneNumberDeleteOption();

                Test.Log(Status.Info, "Step 20: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 21: Verify that participant phone no records to display is displaying");
                Assert.True(await particpantsPage.VerifyParticipantPhoneNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Step 22: click on email delete option");
                await particpantsPage.clickOnEmailDeleteOption();

                Test.Log(Status.Info, "Step 23: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 24: Verify that participant email no records to display is displaying");
                await particpantsPage.VerifyParticipantEmailNoRecordsToDisplayIsVisible();
                Assert.True(await particpantsPage.VerifyParticipantEmailNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Step 25: click on address line delete option");
                await particpantsPage.clickOnAddressLineDeleteOption();

                Test.Log(Status.Info, "Step 26: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 27: Verify that participant address one no records to display is displaying");
                await particpantsPage.VerifyParticipantAddressOneNoRecordsToDisplayIsVisible();
                Assert.True(await particpantsPage.VerifyParticipantAddressOneNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Step 28: Logout From Application");
                await loginPage.LogoutFromApplication();

                Test.Log(Status.Info, "Step 29: Login as system manager");
                Test.Log(Status.Info, "Step 30: Enter Credentials: Email :" + emailManager + " Password: " + managerPassword);
                await loginPage.enterLoginCredentials(emailManager, managerPassword);
                await loginPage.clickOnSubmitButton();

                Test.Log(Status.Info, "Step 31: Verify that the dashboard page is displaying");
                Assert.True(await loginPage.verifyDashboardOnLioinPageIsVisible());

                Test.Log(Status.Info, "Step 32: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 33: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 34: click on add participant button");
                await particpantsPage.clickOnAddParticipantButton();

                Test.Log(Status.Info, "Step 35: Verify that the add participant popup title is displaying");
                Assert.True(await particpantsPage.VerifyAddParticipantPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 36: Enter Participant First Name :" + participantFirstName);
                await particpantsPage.enterParticipantFirstName(participantFirstName);

                Test.Log(Status.Info, "Step 37: Enter Participant Last Name :" + participantLastName);
                await particpantsPage.enterParticipantLastName(participantLastName);

                Test.Log(Status.Info, "Step 38: Enter Participant phone :" + participantPhone);
                await particpantsPage.enterParticipantPhone(participantPhone);

                Test.Log(Status.Info, "Step 39: Enter Participant Email :" + participantEmail);
                await particpantsPage.enterParticipantEmail(participantEmail);

                Test.Log(Status.Info, "Step 40: Enter Participant Address One :" + participantAddressOne);
                await particpantsPage.enterParticipantAddressOne(participantAddressOne);

                Test.Log(Status.Info, "Step 41: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 42: click on participant contact button");
                await particpantsPage.clickOnParticipantsContactTab();

                Test.Log(Status.Info, "Step 43: Verify that the phone number delete option is displaying");
                Assert.True(await particpantsPage.VerifyPhoneNumberDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 44: Verify that the Email delete option is displaying");
                Assert.True(await particpantsPage.VerifyEmailDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 45: Verify that the Address Line delete option is displaying");
                Assert.True(await particpantsPage.VerifyAddressLineDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 46: click on phone number delete option");
                await particpantsPage.clickOnPhoneNumberDeleteOption();

                Test.Log(Status.Info, "Step 47: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 48: Verify that participant phone no records to display is displaying");
                Assert.True(await particpantsPage.VerifyParticipantPhoneNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Step 49: click on email delete option");
                await particpantsPage.clickOnEmailDeleteOption();

                Test.Log(Status.Info, "Step 50: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 51: Verify that participant email no records to display is displaying");
                await particpantsPage.VerifyParticipantEmailNoRecordsToDisplayIsVisible();
                Assert.True(await particpantsPage.VerifyParticipantEmailNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Step 52: click on address line delete option");
                await particpantsPage.clickOnAddressLineDeleteOption();

                Test.Log(Status.Info, "Step 53: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 54: Verify that participant address one no records to display is displaying");
                Assert.True(await particpantsPage.VerifyParticipantAddressOneNoRecordsToDisplayIsVisible());

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
        public async Task TestVerifyThatParticipantShouldNoticeFieldIsAvailableOnParticipantMainScreen()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1087";
                Test = Extent.CreateTest("Test Particpants 1087: Verify that Participant should notice field is available on participant main screen", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that Participant should notice field is available on participant main screen</a>");

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

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
        public async Task TestVerifyThatUserIsAbleToMultipleAddressesPhoneNumbersAndEmailsToAnyParticipantsContactDetailPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var participantFirstName = "test";
            var participantLastName = "user " + GetRandomLastName(4);
            var participantPhone = "4155550111";
            var participantEmail = "adnan.qat1234@gmail.com";
            var participantAddressOne = "540 Howard St";
            var participantNewPhone = "5048120576";
            var participantNewEmail = "adnan.qat12345@gmail.com";
            var participantStatus = "Valid";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1096";
                Test = Extent.CreateTest("Test Particpants 1096: Verify that user is able to multiple addresses, phone numbers and emails to any participants contact detail page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to multiple addresses, phone numbers and emails to any participants contact detail page</a>");

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on add participant button");
                await particpantsPage.clickOnAddParticipantButton();

                Test.Log(Status.Info, "Step 8: Verify that the add participant popup title is displaying");
                Assert.True(await particpantsPage.VerifyAddParticipantPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: Enter Participant First Name :" + participantFirstName);
                await particpantsPage.enterParticipantFirstName(participantFirstName);

                Test.Log(Status.Info, "Step 10: Enter Participant Last Name :" + participantLastName);
                await particpantsPage.enterParticipantLastName(participantLastName);

                Test.Log(Status.Info, "Step 11: Enter Participant phone :" + participantPhone);
                await particpantsPage.enterParticipantPhone(participantPhone);

                Test.Log(Status.Info, "Step 12: Enter Participant Email :" + participantEmail);
                await particpantsPage.enterParticipantEmail(participantEmail);

                Test.Log(Status.Info, "Step 13: Enter Participant Address One :" + participantAddressOne);
                await particpantsPage.enterParticipantAddressOne(participantAddressOne);

                Test.Log(Status.Info, "Step 14: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 15: click on participant contact button");
                await particpantsPage.clickOnParticipantsContactTab();

                Test.Log(Status.Info, "Step 16: click on participant add phone button");
                await particpantsPage.clickOnParticipantAddPhoneButton();

                Test.Log(Status.Info, "Step 17: Enter Participant Add Phone :" + participantNewPhone);
                await particpantsPage.enterAddPhonePhoneField(participantNewPhone);

                Test.Log(Status.Info, "Step 18: Select participant status :" + participantStatus);
                await particpantsPage.selectAddPhoneAndAddEmailStatusDropdown(participantStatus);

                Test.Log(Status.Info, "Step 19: click on participant Save button");
                await particpantsPage.clickOnParticipantSaveButton();

                Test.Log(Status.Info, "Step 20: Verify that the phone number is displaying: "+ participantNewPhone);
                Assert.True(await particpantsPage.verifyEmailOrPhoneIsAdded(participantNewPhone));

                Test.Log(Status.Info, "Step 21: click on participant add email button");
                await particpantsPage.clickOnParticipantAddEmailButton();

                Test.Log(Status.Info, "Step 22: Enter Participant Add Email :" + participantNewEmail);
                await particpantsPage.enterParticipantAddEmail(participantNewEmail);

                Test.Log(Status.Info, "Step 23: Select participant status :" + participantStatus);
                await particpantsPage.selectAddPhoneAndAddEmailStatusDropdown(participantStatus);

                Test.Log(Status.Info, "Step 24: click on participant Save button");
                await particpantsPage.clickOnParticipantSaveButton();

                Test.Log(Status.Info, "Step 25: Verify that the Email is displaying: " + participantNewEmail);
                Assert.True(await particpantsPage.verifyEmailOrPhoneIsAdded(participantNewEmail));


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
        public async Task TestVerifythatAdminIsableTodeleteContactDetailsOfAnyParticipant()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var participantFirstName = "test";
            var participantLastName = "user " + GetRandomLastName(4);
            var participantPhone = "4155550111";
            var participantEmail = "adnan.qat1234@gmail.com";
            var participantAddressOne = "540 Howard St";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1097";
                Test = Extent.CreateTest("Test Particpants 1097: Verify that admin is able to delete contact details of any participant", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that admin is able to delete contact details of any participant</a>");

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on add participant button");
                await particpantsPage.clickOnAddParticipantButton();

                Test.Log(Status.Info, "Step 8: Verify that the add participant popup title is displaying");
                Assert.True(await particpantsPage.VerifyAddParticipantPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: Enter Participant First Name :" + participantFirstName);
                await particpantsPage.enterParticipantFirstName(participantFirstName);

                Test.Log(Status.Info, "Step 10: Enter Participant Last Name :" + participantLastName);
                await particpantsPage.enterParticipantLastName(participantLastName);

                Test.Log(Status.Info, "Step 11: Enter Participant phone :" + participantPhone);
                await particpantsPage.enterParticipantPhone(participantPhone);

                Test.Log(Status.Info, "Step 12: Enter Participant Email :" + participantEmail);
                await particpantsPage.enterParticipantEmail(participantEmail);

                Test.Log(Status.Info, "Step 13: Enter Participant Address One :" + participantAddressOne);
                await particpantsPage.enterParticipantAddressOne(participantAddressOne);

                Test.Log(Status.Info, "Step 14: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 15: click on participant contact button");
                await particpantsPage.clickOnParticipantsContactTab();

                Test.Log(Status.Info, "Step 16: Verify that the phone number delete option is displaying");
                Assert.True(await particpantsPage.VerifyPhoneNumberDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 17: Verify that the Email delete option is displaying");
                Assert.True(await particpantsPage.VerifyEmailDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 18: Verify that the Address Line delete option is displaying");
                Assert.True(await particpantsPage.VerifyAddressLineDeleteOptionIsVisible());

                Test.Log(Status.Info, "Step 19: click on phone number delete option");
                await particpantsPage.clickOnPhoneNumberDeleteOption();

                Test.Log(Status.Info, "Step 20: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 21: Verify that participant phone no records to display is displaying");
                Assert.True(await particpantsPage.VerifyParticipantPhoneNoRecordsToDisplayIsVisible());

                Test.Log(Status.Info, "Step 22: click on email delete option");
                await particpantsPage.clickOnEmailDeleteOption();

                Test.Log(Status.Info, "Step 23: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 24: Verify that participant email no records to display is displaying");
                await particpantsPage.VerifyParticipantEmailNoRecordsToDisplayIsVisible();
                Assert.True(await particpantsPage.VerifyParticipantEmailNoRecordsToDisplayIsVisible());
                    
                Test.Log(Status.Info, "Step 25: click on address line delete option");
                await particpantsPage.clickOnAddressLineDeleteOption();

                Test.Log(Status.Info, "Step 26: click on delete yes button");
                await particpantsPage.clickOnDeleteYesPopupButton();

                Test.Log(Status.Info, "Step 27: Verify that participant address one no records to display is displaying");
                await particpantsPage.VerifyParticipantAddressOneNoRecordsToDisplayIsVisible();
                Assert.True(await particpantsPage.VerifyParticipantAddressOneNoRecordsToDisplayIsVisible());

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
        public async Task TestVerifyThatUserIsAbleToFilterTheDataFromTheParticipantsTable()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var accountsPage = new AccountsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var plan = "";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1118";
                Test = Extent.CreateTest("Test Particpants 1118: Verify that user is able to filter the data from the participants table", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that user is able to filter the data from the participants table</a>");

                Test.Log(Status.Info, "Step 1: Launching the app");
                await page.GotoAsync(Constants.BaseUrl, new PageGotoOptions { Timeout = 100000 });

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on filter button");
                await particpantsPage.clickOnParticipantFilterButton();

                Test.Log(Status.Info, "Step 8: Verify that participant filter title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantFilterpopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: select plan");
                plan = await particpantsPage.selectPlan();

                Test.Log(Status.Info, "Step 10: click on apply button");
                await accountsPage.clickOnApplyButton();

                Test.Log(Status.Info, "Step 11: Verify that the filtered participant is displaying: " + plan);
                Assert.True(await particpantsPage.VerifyFilterdPlanIsVisible(plan));

                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Pass("Test passed Screenshot", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
            }
            catch (Exception e)
            {
                {
                byte[] screenshotBytes = await page.ScreenshotAsync();
                Test.Fail($"Test failed Screenshot: {e.Message}", MediaEntityBuilder.CreateScreenCaptureFromBase64String(Convert.ToBase64String(screenshotBytes)).Build());
                Assert.True(false);
                }
            }
        }

        [Test]
        public async Task TestVerifythatPaginationWorksProperlyOnTheParticipantsPage()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var accountsPage = new AccountsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1124";
                Test = Extent.CreateTest("Test Particpants 1124: Verify that pagination works properly on the participants page", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that pagination works properly on the participants page</a>");

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                //Test.Log(Status.Info, "Step 7: Verify that the add participant popup title is displaying");
                //Assert.True(await particpantsPage.VerifyAddParticipantPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 8: Verify that the Forward Go To Next Page Pagination Work Properly");
                Assert.True(await accountsPage.VerifyForwardGoToNextPagePaginationWorkProperly());

                Test.Log(Status.Info, "Step 9: Verify that the Backward Go To Previous Page Pagination Work Properly");
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
        public async Task TestVerifyThatAllImportedCensusFileShouldBeSavedInListPage()
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
                Test = Extent.CreateTest("Test Particpants 1091: Verify that all imported data of participants stores and available on the imported participants detail pages", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying all imported data of participants stores and available on the imported participants detail pages</a>");

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

                Test.Log(Status.Info, "Step 27: verify queued for import status is displaying");
                Assert.True(await PlansPage.verifyQueuedForImportStatusIsVisible());

                Test.Log(Status.Info, "Step 28: verify imported status is displaying");
                Assert.True(await PlansPage.verifyImportedStatusIsVisible());

                Test.Log(Status.Info, "Step 29: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 30: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 31: Enter Participant Name Search Field :" + testPlanName);
                await particpantsPage.enterParticipantSearchField(testPlanName);

                Test.Log(Status.Info, "Step 32: Verify that the search participant: " + testPlanName + " are displaying");
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
        public async Task TestVerifyThatUserShouldUpdateContactDetailsOfAnyParticipant()
        {
            var page = await context.NewPageAsync();
            var loginPage = new LoginPage(page);
            var PlansPage = new PlansPage(page);
            var particpantsPage = new ParticpantsPage(page);
            var testPlanName = "test plan" + DateTime.Now.Ticks;
            var participantFirstName = "test";
            var participantLastName = "user " + GetRandomLastName(4);
            var participantPhone = "4155550111";
            var editedParticipantPhone = "4155550112";
            var participantEmail = "adnan.qat1234@gmail.com";
            var editedparticipantEmail = "adnan.qat1234+123@gmail.com";
            var participantAddressOne = "540 Howard St";

            try
            {
                string link = "https://dev.azure.com/plannotice/Plan-Notice/_workitems/edit/1095";
                Test = Extent.CreateTest("Test Particpants 1095: Verify that User should be able to update Contact details of any Participant", $"<a href=\"{link}\" target=\"_blank\">As a user, I am verifying that User can update contact details of Participant</a>");

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

                Test.Log(Status.Info, "Step 5: click on Participants Section");
                await particpantsPage.clickOnParticipantsTab();

                Test.Log(Status.Info, "Step 6: Verify that the participants page title is displaying");
                Assert.True(await particpantsPage.VerifyParticipantsPageTitleIsVisible());

                Test.Log(Status.Info, "Step 7: click on add participant button");
                await particpantsPage.clickOnAddParticipantButton();

                Test.Log(Status.Info, "Step 8: Verify that the add participant popup title is displaying");
                Assert.True(await particpantsPage.VerifyAddParticipantPopupTitleIsVisible());

                Test.Log(Status.Info, "Step 9: Enter Participant First Name :" + participantFirstName);
                await particpantsPage.enterParticipantFirstName(participantFirstName);

                Test.Log(Status.Info, "Step 10: Enter Participant Last Name :" + participantLastName);
                await particpantsPage.enterParticipantLastName(participantLastName);

                Test.Log(Status.Info, "Step 11: Enter Participant phone :" + participantPhone);
                await particpantsPage.enterParticipantPhone(participantPhone);

                Test.Log(Status.Info, "Step 12: Enter Participant Email :" + participantEmail);
                await particpantsPage.enterParticipantEmail(participantEmail);

                Test.Log(Status.Info, "Step 13: Enter Participant Address One :" + participantAddressOne);
                await particpantsPage.enterParticipantAddressOne(participantAddressOne);

                Test.Log(Status.Info, "Step 14: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 15: click on participant contact button");
                await particpantsPage.clickOnParticipantsContactTab();

                Test.Log(Status.Info, "Step 16: Edit Phone Number in contact details");
                await particpantsPage.EditContactDetailPhoneNumber(editedParticipantPhone);

                Test.Log(Status.Info, "Step 17: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 18: Verify edited phone number is shown");
                Assert.True(await particpantsPage.VerifyEditedPhoneNumberOrEmailIsDispalying(editedParticipantPhone));

                Test.Log(Status.Info, "Step 19: Edit Email in contact details");
                await particpantsPage.EditContactDetailEmail(editedparticipantEmail);

                Test.Log(Status.Info, "Step 20: click on add participant popup save button");
                await particpantsPage.clickOnAddParticipantPopupSaveButton();

                Test.Log(Status.Info, "Step 21: Verify edited phone number is shown");
                Assert.True(await particpantsPage.VerifyEditedPhoneNumberOrEmailIsDispalying(editedparticipantEmail));

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