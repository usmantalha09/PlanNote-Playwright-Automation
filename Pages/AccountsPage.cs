using DnsClient.Protocol;
using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class AccountsPage : Base
    {
        private readonly IPage page;

        private const string AccountsLiftSideMenuOption = "//label[text()='Accounts']";
        private const string UsersLiftSideMenuOption = "//label[text()='Users']";
        private const string AccountsPageTitle = "//strong[text()='Accounts']";
        private const string UsersPageTitle = "//h3[text()='Users']";
        private const string CreateAnAccountBtn = "//button[text()='Create an Account']";
        private const string CreateAnAccountPopupTitle = "//strong[text()='Create an Account']";
        private const string CreateAnAccountNameTxt = "//label[text()='Name']/following-sibling::input";
        private const string CreateAnAccountAccountTypeDropdown = "//label[text()='Account Type']/following-sibling::div";
        private const string CreateAnAccountAccountTypeDropdownServiceProviderOption = "//li[@aria-label='Service Provider']";
        private const string CreateAnAccountSaveBtn = "//button[text()='Save']";
        private const string AccountDetailsPageTitle = "//strong[text()='Account Details']";
        private const string AccountDetailsAccountName = "//label[text()='Account Name']/following-sibling::input";
        private const string AccountDetailsSaveBtn = "//button[text()='Save']";
        private const string AccountUpdatedSuccessfullyPopup = "//p[text()='Account updated successfully!']";
        private const string CreateAnAccountDeleteBtn = "//button[text()='Delete']";
        private const string AccountSearchTxt = "//input[@placeholder='Search']";
        private const string NoRecordsToDisplayMsg = "//span[contains(text(),'No records to display')]";
        private const string AccountFilterBtn = "//button[contains(text(),'Filter')]";
        private const string FilterPopup = "//button[contains(text(),'Filter')]";
        private const string AccountTypeDropdown = "//div[text()='Associated Plans number']/parent::div/div/span";
        private const string AccountTypeDropdownOption = "(//ul[@role='listbox']//span)[2]";
        private const string AssociatedPlansNumberFromTxt = "(//div[contains(text(),'From')]/parent::div)[2]//input";
        private const string AssociatedPlansNumberToTxt = "(//div[contains(text(),'To')]/parent::div)[2]//input";
        private const string UsersNumberFromTxt = "(//div[contains(text(),'From')]/parent::div)[1]//input";
        private const string UsersNumberToTxt = "(//div[contains(text(),'To')]/parent::div)[1]//input";
        private const string ApplyBtn = "//button[text()='Apply']";
        private const string ResetBtn = "//button[text()='Reset']";
        private const string SelectTypesMsg = "//span[text()='Select types']";
        private const string GoToNextPageBtn = "//a[@aria-label='Go to next page.']";
        private const string GoToLastPageBtn = "//a[@aria-label='Go to last page.']";
        private const string PaginationPageCount = "//span[contains(text(),'Page')]";
        private const string GoToPreviousPageBtn = "//a[@aria-label='Go to previous page.']";
        private const string GoToFirstPageBtn = "//a[@aria-label='Go to first page.']";
        private const string GoToPlanTab = "//div[@class='btn tab-item ' and text()='Plans']";
        private const string GoToUserTab = "//div[@class='btn tab-item ' and text()='Users']";
        private const string GoToDetailsTab = "//div[@class='btn tab-item ' and text()='Details']";
        private const string AssociateButton = "//button[@id=\"search-addon\"]";
        private const string AssociatePlanPopup = "//div[@class=\"bmodal-header\"]";
        private const string SelectPlanDropdown = "//label[text()='Select a Plan']/following-sibling::div";
        private const string SelectPlanDropdownFirstOption = "//li[@class='rz-dropdown-item ']/span[1]";
        private const string SelectPlanButton = "//button[contains(text(), \"Select\")]";
        private const string UnassociatePlanButton = "//button[contains(text(), \"Unassociate\")]";
        private const string CheckboxIcon = "//div[@class=\"rz-chkbox-box\"]";
        private const string UnassocaitePlanConfirmPrompt = "//button[contains(text(), \"Yes\")]";
        private const string AssociateUserButton = "//button[contains(text(), 'Associate User')]";
        private const string SelectUserDropdown = "//label[text()='User']/following-sibling::div";
        private const string SelectRoleDropdown = "//label[text()='Role']/following-sibling::div";
        private const string SelectUserDropdownFirstOption = "(//li[@class='rz-dropdown-item '][1])[2]";
        private const string SelectRoleDropdownLastOption = "(//li[@class='rz-dropdown-item '][1])[2]";
        private const string SaveButtonForUserAssociation = "//span[text()='Save']";


        public AccountsPage(IPage page)
        {
            this.page = page;
        }

        public async Task clickOnAccountsLiftSideMenuOption()
        {
            await page.ClickAsync(AccountsLiftSideMenuOption);
        }

        public async Task<bool> VerifyAccountsPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, AccountsPageTitle);
        }

        public async Task<bool> VerfiyUsersPageTtileVisible()
        {
            return await WaitForElementVisible(page, UsersPageTitle);
        }

        public async Task clickOnCreateAnAccountButton()
        {
            await page.ClickAsync(CreateAnAccountBtn);
        }

        public async Task<bool> VerifyCreateAnAccountPopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, CreateAnAccountPopupTitle);
        }

        public async Task enterCreateAnAccountName(string accountName)
        {
            await page.FillAsync(CreateAnAccountNameTxt, accountName);
        }

        public async Task<bool> VerifyAssociatePlanPopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, AssociatePlanPopup);
        }

        public async Task selectServiceProviderOptionFromDropdown()
        {
            await page.ClickAsync(CreateAnAccountAccountTypeDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(CreateAnAccountAccountTypeDropdownServiceProviderOption);
        }
        public async Task clickOnCreateAnAccountSaveButton()
        {
            await page.ClickAsync(CreateAnAccountSaveBtn);
        }

        public async Task clickOnCreateUserAssociation()
        {
            await page.ClickAsync(SaveButtonForUserAssociation);
            await Task.Delay(3000); // 1-second delay
        }

        public async Task<bool> VerifyUserAssociatedWithAccount(string UserSelected)
        {
            return await WaitForElementVisible(page, "//a[contains(text(), '" + UserSelected +"')]");
        }

        public async Task<bool> VerifyAccountDetailsPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, AccountDetailsPageTitle);
        }

        public async Task enterDetailsAccountName(string detailsAccountName)
        {
            await page.FillAsync(AccountDetailsAccountName, detailsAccountName);
        }

        public async Task clickOnAccountDetailsSaveButton()
        {
            await page.ClickAsync(AccountDetailsSaveBtn);
        }

        public async Task<bool> VerifyAccountUpdatedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, AccountUpdatedSuccessfullyPopup);
        }

        public async Task<bool> VerifyAccountNameUpdatedSuccessfully(string accountName)
        {
            return await WaitForElementVisible(page, "//a[text()='" + accountName + "']");
        }

        public async Task clickOnAccountName(string accountName)
        {
            await page.ClickAsync("//a[text()='" + accountName + "']");
        }
        public async Task clickOnCreateAnAccountDelete()
        {
            await page.ClickAsync(CreateAnAccountDeleteBtn);
        }

        public async Task deleteAccount(string accountName)
        {
            await clickOnAccountName(accountName);
            await clickOnCreateAnAccountDelete();
            await Task.Delay(3000);
        }

        public async Task enterValueInAccountSearch(string value)
        {
            await page.FillAsync(AccountSearchTxt, value);
        }

        public async Task<bool> VerifyNoRecordsToDisplayMessageIsVisible()
        {
            return await WaitForElementVisible(page, NoRecordsToDisplayMsg);
        }

        public async Task<bool> VerifyAccountTypeIsVisible(string accountType)
        {
            return await WaitForElementVisible(page, "(//span[text()='"+ accountType + "'])[1]");
        }

        public async Task clickOnAccountFilterButton()
        {
            await page.ClickAsync(AccountFilterBtn);
        }

        public async Task<bool> VerifyFilterPopupIsVisible()
        {
            return await WaitForElementVisible(page, FilterPopup);
        }

        public async Task<string> selectAccountType()
        {
            await page.ClickAsync(AccountTypeDropdown);
            await Task.Delay(3000);
            // Get the text content of the element
            var element = await page.QuerySelectorAsync(AccountTypeDropdownOption);
            var textContent = await element.InnerTextAsync();
            await page.ClickAsync(AccountTypeDropdownOption);
            await Task.Delay(2000);
            await page.ClickAsync(AccountTypeDropdown);
            return textContent;
        }

        public async Task enterAssociatedPlansNumberFromAndTo(string fromValue, string toValue)
        {
            await page.FillAsync(AssociatedPlansNumberFromTxt, fromValue);
            await page.FillAsync(AssociatedPlansNumberToTxt, toValue);
        }

        public async Task enterUsersNumberFromAndTo(string fromValue, string toValue)
        {
            await page.FillAsync(UsersNumberFromTxt, fromValue);
            await page.FillAsync(UsersNumberToTxt, toValue);
        }

        public async Task clickOnApplyButton()
        {
            await page.ClickAsync(ApplyBtn);
            await Task.Delay(3000);
        }

        public async Task clickOnResetButton()
        {
            await page.ClickAsync(ResetBtn);
            await Task.Delay(3000);
        }        

        public async Task<bool> VerifyAssociatedPlansNumberIsVisible(string from, string to)
        {
                try
                {
                    return await WaitForElementVisible(page, "(//td/span[text()='"+ from + "'])[1]");
                }catch(Exception ex)
                {
                    return await WaitForElementVisible(page, "(//td/span[text()='" + to + "'])[1]");
                }
        }

        public async Task<bool> VerifyUsersNumberIsVisible(string from, string to)
        {
            try
            {
                return await WaitForElementVisible(page, "(//td/span[text()='" + from + "'])[2]");
            }
            catch (Exception ex)
            {
                return await WaitForElementVisible(page, "(//td/span[text()='" + to + "'])[2]");
            }
        }

        public async Task<bool> verifyAccountTypeFilterReset()
        {
            return await WaitForElementVisible(page, SelectTypesMsg);
        }

        public async Task<bool> VerifyAssociatedPlansNumberFilterReset()
        {
            var inputElement = await page.QuerySelectorAsync(AssociatedPlansNumberFromTxt);
            string fieldValue = await inputElement.EvaluateAsync<string>("input => input.value");
            return string.IsNullOrEmpty(fieldValue);
        }

        public async Task<bool> VerifyUsersNumberFilterReset()
        {
            var inputElement = await page.QuerySelectorAsync(UsersNumberFromTxt);
            string fieldValue = await inputElement.EvaluateAsync<string>("input => input.value");
            return string.IsNullOrEmpty(fieldValue);
        }
        public async Task clickOnGoToNextPageButton()
        {
            await ScrollToElement(page, GoToNextPageBtn);
            await page.ClickAsync(GoToNextPageBtn);
        }
        
        public async Task<bool> VerifyForwardGoToNextPagePaginationWorkProperly()
        {
            bool status = true;
            string val = await GetElementTextContent(page, PaginationPageCount);
            int count = int.Parse(await ExtractPageNumberAsync(val));
            while (true)
            {
                count++;
                if (await IsButtonDisabled(page, GoToNextPageBtn) != true)
                {
                await clickOnGoToNextPageButton();
                status = await WaitForElementVisible(page, "//span[contains(text(),'Page "+ count + "')]");
                }
                if (count == 5)
                {
                    break;
                }
            }
            return status;
        }


        public async Task clickOnGoToLastPageButton()
        {
            await ScrollToElement(page, GoToLastPageBtn);
            await page.ClickAsync(GoToLastPageBtn);
        }

        public async Task clickOnPlanTab()
        {
            await page.ClickAsync(GoToPlanTab);
        }

        public async Task clickOnUserTab()
        {
            await page.ClickAsync(GoToUserTab);
        }

        public async Task<bool> VerifyAssociateButtonIsVisible()
        {
            return await WaitForElementVisible(page, AssociateButton);
        }

        public async Task<bool> VerifyAssocateUserButtonIsVisible()
        {
            return await WaitForElementVisible(page, AssociateUserButton);
        }
        public async Task clickOnAssociateButton()
        {
            await Task.Delay(3000);
            await page.ClickAsync(AssociateUserButton);
        }

        public async Task cliickOnAssociateUserButton()
        {
            await Task.Delay(3000);
            await page.ClickAsync(AssociateUserButton);
        }

        public async Task<String> selectPlanFromDropdown()
        {
            await page.ClickAsync(SelectPlanDropdown);
            await Task.Delay(3000); // 1-second delay
            string PlanSelected = await GetElementTextContent(page, SelectPlanDropdownFirstOption);
            await page.ClickAsync(SelectPlanDropdownFirstOption);
            return PlanSelected;
        }

        public async Task<String> selectUserFromDropdown()
        {
            await page.ClickAsync(SelectUserDropdown);
            await Task.Delay(3000); // 1-second delay
            string UserSelected = await GetElementTextContent(page, SelectUserDropdownFirstOption);
            await page.ClickAsync(SelectUserDropdownFirstOption);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(SelectRoleDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(SelectRoleDropdownLastOption);

            return UserSelected;
        }

        public async Task<bool> VerifyPlanAssociatedWithAccount(string PlanSelected)
        {
            await Task.Delay(3000); // 1-second delay
            return await WaitForElementVisible(page, "//a[contains(text(), \"" + PlanSelected + "\")]");
        }

        public async Task DeleteTestAccount()
        {
            await page.ClickAsync(GoToDetailsTab);
            await page.ClickAsync(CreateAnAccountDeleteBtn);
            await Task.Delay(3000); // 1-second delay
        }

        public async Task UnassocaiteLinkedPlan()
        {
            await page.ClickAsync(SelectPlanButton);
            await page.ClickAsync(CheckboxIcon);
            await page.ClickAsync(UnassociatePlanButton);
            await WaitForElementVisible(page, UnassocaitePlanConfirmPrompt);
            await page.ClickAsync(UnassocaitePlanConfirmPrompt);
            await WaitForElementVisible(page, "//span[contains(text(), \"No records to display.\")]");
        }

        public async Task clickOnUsersLiftSideMenuOption()
        {
            await page.ClickAsync(UsersLiftSideMenuOption);
        }

        public async Task clickOnGoToPreviousPageButton()
        {
            await ScrollToElement(page, GoToPreviousPageBtn);
            await page.ClickAsync(GoToPreviousPageBtn);
        }
        public async Task<bool> VerifyBackwardGoToPreviousPagePaginationWorkProperly()
        {
            bool status = true;
            string val = await GetElementTextContent(page, PaginationPageCount);
            int count = int.Parse(await ExtractPageNumberAsync(val));
            while (true)
            {
                count--;
                if (await IsButtonDisabled(page, GoToPreviousPageBtn) != true)
                {
                    await clickOnGoToPreviousPageButton();
                    status = await WaitForElementVisible(page, "//span[contains(text(),'Page " + count + "')]");
                }
                if (count == 1)
                {
                    break;
                }
            }
            return status;
        }

    }
}
