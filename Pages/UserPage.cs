using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlanNotePlaywrite.Pages
{
    internal class UserPage : Base
    {
        private readonly IPage page;

        private const string UsersSiteMenu = "(//label[contains(text(),'Users')])[1]";
        private const string UsersPageTitle = "//h3[contains(text(),'Users')]";
        private const string NewUserBtn = "//button[contains(text(),'New User')]";
        private const string NewUserTitle = "//strong[contains(text(),'New User')]";
        private const string FirstNameTxt = "//label[text()='First Name']/following-sibling::input";
        private const string LastNameTxt = "//label[text()='Last Name']/following-sibling::input";
        private const string EmailTxt = "//label[text()='Email']/following-sibling::input";
        private const string PasswordTxt = "//label[text()='Password']/following-sibling::input";
        private const string ConfirmPasswordTxt = "//label[text()='Confirm Password']/following-sibling::input";
        private const string AccountDropdown = "//label[text()='Account']/following-sibling::div";        
        private const string RoleDropdown = "//label[text()='Role']/following-sibling::div";        
        private const string NewUserSaveBtn = "//button[text()='Save']";
        private const string NewUserDeleteBtn = "//button[text()='Delete']";
        private const string UserUpdatedSuccessfullyPopup = "//p[text()='User updated successfully!']";
        private const string UserAddedSuccessfullyPopup = "//p[text()='User added successfully!']";
        private const string SearchByPhoneTxt = "((//div[contains(text(),'Search by Phone')]/parent::div)[1]//input)[1]";
        private const string SearchByEmailTxt = "((//div[contains(text(),'Search by Email')]/parent::div)[1]//input)[2]";
        private const string FilterPhoneInput = "(//div[text()='Search by Phone'])/parent::div/div[2]/input";
        private const string ResetButton = "//button[contains(text(), 'Reset')]";

        public UserPage(IPage page)
        {
            this.page = page;
        }

        public async Task clickOnUsersSiteMenu()
        {
            await page.ClickAsync(UsersSiteMenu);
        }

        public async Task<bool> verifyUsersPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, UsersPageTitle);
        }
        public async Task clickOnNewUserButton()
        {
            await page.ClickAsync(NewUserBtn);
        }

        public async Task<bool> verifyNewUserTitleIsVisible()
        {
            return await WaitForElementVisible(page, NewUserTitle);
        }

        public async Task enterFirstNameValue(string FirstName)
        {
            await page.FillAsync(FirstNameTxt, FirstName);
        }

        public async Task enterLastNameValue(string lastName)
        {
            await page.FillAsync(LastNameTxt, lastName);
        }

        public async Task enterEmail(string email)
        {
            await page.FillAsync(EmailTxt, email);
        }

        public async Task enterPassword(string password)
        {
            await page.FillAsync(PasswordTxt, password);
        }
        public async Task enterConfirmPassword(string ConfirmPassword)
        {
            await page.FillAsync(ConfirmPasswordTxt, ConfirmPassword);
        }

        public async Task selectAccountFromDropdown(string option)
        {
            await page.ClickAsync(AccountDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='"+option+"']");
        }

        public async Task selectRoleFromDropdown(string option)
        {
            await page.ClickAsync(RoleDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='" + option + "']");
        }
        public async Task clickOnNewUserPopupSaveButton()
        {
            await page.ClickAsync(NewUserSaveBtn);
        }        

        public async Task<bool> verifyAddedUserIsVisible(string name)
        {
            return await WaitForElementVisible(page, "//a[text()='"+name+ "'] | //h3[text()='"+name+"']");
        }

        public async Task clickOnAddedUser(string name)
        {
            await page.ClickAsync("//a[text()='" + name + "']");
        }

        public async Task clickOnNewUserDeleteButton()
        {
            await page.ClickAsync(NewUserDeleteBtn);
            await Task.Delay(3000);
        }

        public async Task<bool> verifyUserAddedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, UserAddedSuccessfullyPopup);
        }

        public async Task<bool> verifyUserUpdatedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, UserUpdatedSuccessfullyPopup);
        }

        public async Task<bool> VerifySearchByPhoneFilterReset()
        {
            var inputElement = await page.QuerySelectorAsync(SearchByPhoneTxt);
            string fieldValue = await inputElement.EvaluateAsync<string>("input => input.value");
            return string.IsNullOrEmpty(fieldValue);
        }

        public async Task<bool> VerifySearchByEmailFilterReset()
        {
            var inputElement = await page.QuerySelectorAsync(SearchByEmailTxt);
            string fieldValue = await inputElement.EvaluateAsync<string>("input => input.value");
            return string.IsNullOrEmpty(fieldValue);
        }

        public async Task enterSearchByPhone(string value)
        {
            await page.FillAsync(SearchByPhoneTxt, value);
        }

        public async Task enterSearchByEmaile(string value)
        {
            await page.FillAsync(SearchByEmailTxt, value);
        }
    
        public async Task clickOnPhoneNumberFilter()
        {
            await page.ClickAsync(FilterPhoneInput);
            await Task.Delay(3000); // 1-second delay
            await page.FillAsync(FilterPhoneInput, "123");
            await Task.Delay(3000); // 1-second delay
        }
        public async Task ResetUserFilters()
        {
            await page.ClickAsync(ResetButton);
            await Task.Delay(2000);
        }
    }
}
