using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class LoginPage : Base
    {
        private readonly IPage page;

        private const string EmailInputXPath = "//label[contains(text(),'Email')]/following-sibling::input";
        private const string PasswordInputXPath = "//label[contains(text(),'Password')]/following-sibling::input";
        private const string LoginButtonXPath = "//button[text()='Login']";
        private const string DashboardXPath = "//h3[contains(text(),'Plan Notice')]";
        private const string RememberMeCheckbox = "//label[contains(text(),'Remember Me?')]/parent::div/input";
        private const string ForgotpasswordLink = "//a[contains(text(),'Forgot password?')]";
        private const string RestorePasswordPage = "//h3[contains(text(),'Restore Password')]";
        private const string RestorePasswordEmailTxt = "//label[text()='Email']/following-sibling::input";
        private const string ProfileDropdownBtn = "//i[text()='keyboard_arrow_down']";
        private const string ProfileDropdownLogoutOption = "//div[contains(text(),'Logout')]";

        public LoginPage(IPage page)
        {
            this.page = page;
        }

        public async Task enterLoginCredentials(string username, string password)
        {
            await page.FillAsync(EmailInputXPath, username);
            await page.FillAsync(PasswordInputXPath, password);
        }

        public async Task clickOnSubmitButton()
        {
            await page.ClickAsync(LoginButtonXPath);
        }

        public async Task<bool> verifyDashboardOnLioinPageIsVisible()
        {
            bool isElementVisible;
            try
            {
                isElementVisible = await WaitForElementVisible(page, DashboardXPath, 100000);
            }
            catch (Exception)
            {
                isElementVisible = false;
            }
            return isElementVisible;
        }

        public async Task<bool> verifyRememberMeCheckboxIsVisible()
        {
            try {
                return await WaitForElementVisible(page, RememberMeCheckbox,120000);

            } catch (Exception e)
            {
                await loadURL(page, Constants.BaseUrl);
                return await WaitForElementVisible(page, RememberMeCheckbox, 120000);
            }
        }
        public async Task<bool> verifyForgotpasswordLinkIsVisible()
        {
            return await WaitForElementVisible(page, ForgotpasswordLink);
        }

        public async Task clickOnForgotpasswordLink()
        {
            await page.ClickAsync(ForgotpasswordLink);
        }

        public async Task<bool> verifyRestorePasswordPageIsVisible()
        {
            return await WaitForElementVisible(page, RestorePasswordPage);
        }

        public async Task enterRestorePasswordEmail(string email)
        {
            await page.FillAsync(RestorePasswordEmailTxt, email);
        }

        public async Task<bool> verifyEmailInputXPathIsVisible()
        {
            return await WaitForElementVisible(page, EmailInputXPath);
        }

        public async Task<bool> verifyPasswordInputXPathIsVisible()
        {
            return await WaitForElementVisible(page, PasswordInputXPath);
        }

        public async Task clickOnRememberMeCheckbox()
        {
            await page.ClickAsync(RememberMeCheckbox);
        }

        public async Task LogoutFromApplication()
        {
            await page.ClickAsync(ProfileDropdownBtn);
            await page.ClickAsync(ProfileDropdownLogoutOption);

        }
    }
}   
