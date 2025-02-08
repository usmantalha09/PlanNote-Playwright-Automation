using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class TemplatePage : Base
    {
        private readonly IPage page;

        private const string TemplatesLiftSideMenuOption = "//label[text()='Templates']";
        private const string TemplatePageTitle = "//strong[text()='Templates']";
        private const string TemplateTabletLoaded = "//span[text()='Created On']";
        private const string NewTemplateBtn = "//button[text()='New Template']";
        private const string TemplateNameTxt = "//label[text()='Template Name']/following-sibling::input";
        private const string StepTypeDropdown = "//label[text()='Step Type']/following-sibling::div";
        private const string EmailTypeDropdown = "//label[text()='Email Type']/following-sibling::div";
        private const string EmailSubjectTxt = "//label[text()='Email Subject']/following-sibling::input";
        private const string AccountDropdown = "//label[text()='Account']/following-sibling::div";
        private const string AccountDropdownFirstOption = "((//input[@aria-label='Search']/parent::div/following-sibling::div)[last()]//span)[1]";
        private const string UploadTemplateTxt = "//label[text()='Upload Template']/following-sibling::input";
        private const string TemplateSaveBtn = "//button[text()='Save']";
        private const string TemplateUpdatedSuccessfullyPopup = "//p[text()='Template updated successfully!']";
        private const string TemplateAddedSuccessfullyPopup = "//p[text()='Template Added successfully!']";
        private const string TemplateDeleteBtn = "//button[text()='Delete']";
        private const string FiltersAccountDropdown = "(//div[text()='Accounts']/parent::div/div/div/span)[1]";
        private const string FiltersAccountDropdownFirstOption = "((//ul[@role='listbox'])[last()]/li//span)[2]";
        private const string FiltersStepTypeDropdown = "(//div[text()='Step Type']/parent::div/div/div/span)[2]";
        private const string ResetButton = "//button[contains(text(), 'Reset')]";

        public TemplatePage(IPage page)
        {
            this.page = page;
        }

        public async Task clickOnTemplatesLiftSideMenuOption()
        {
            await page.ClickAsync(TemplatesLiftSideMenuOption);
        }

        public async Task<bool> verifyTemplatePageTitleIsVisible()
        {
            await WaitForElementVisible(page, TemplatePageTitle);
            return await WaitForElementVisible(page, TemplateTabletLoaded);
        }

        public async Task clickOnNewTemplateButton()
        {
            await page.ClickAsync(NewTemplateBtn);
        }

        public async Task enterTemplateName(string name)
        {
            await WaitForElementVisible(page, TemplateNameTxt);
            await page.FillAsync(TemplateNameTxt, name);
        }
        
        public async Task selectEmailTypeDropdownOption(string option)
        {
            await page.ClickAsync(EmailTypeDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='" + option + "']");
            await Task.Delay(3000);
        }
        public async Task selectStepTypeDropdownOption(string option)
        {
            await page.ClickAsync(StepTypeDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='" + option + "']");
            await Task.Delay(3000);
        }

        public async Task enterEmailSubject(string emailSubject)
        {
            await page.FillAsync(EmailSubjectTxt, emailSubject);
        }
        public async Task selectAccountDropdownFirstOption()
        {
            await page.ClickAsync(AccountDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(AccountDropdownFirstOption);
            await Task.Delay(3000);
        }
        public async Task UploadTemplate()
        {
            await page.SetInputFilesAsync(UploadTemplateTxt, fullPathHtmlTemplate);
        }
        public async Task clickOnTemplateSaveButton()
        {
            await page.ClickAsync(TemplateSaveBtn);
        }

        public async Task<bool> verifyTemplateAddedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, TemplateAddedSuccessfullyPopup);
        }

        public async Task<bool> verifyTemplateUpdatedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, TemplateUpdatedSuccessfullyPopup);
        }
        
        public async Task<bool> verifyAddedTemplateIsVisible(string templateName)
        {
            return await WaitForElementVisible(page, "//a[text()='"+ templateName + "']");
        }

        public async Task clickOAddedTemplateName(string templateName)
        {
            await page.ClickAsync("//a[text()='" + templateName + "']");
        }

        public async Task clickOnTemplateDeleteButton()
        {
            await page.ClickAsync(TemplateDeleteBtn);
        }

        public async Task<string> selectAccount()
        {
            await page.ClickAsync(FiltersAccountDropdown);
            await Task.Delay(3000);
            // Get the text content of the element
            var element = await page.QuerySelectorAsync(FiltersAccountDropdownFirstOption);
            var textContent = await element.InnerTextAsync();
            await page.ClickAsync(FiltersAccountDropdownFirstOption);
            await Task.Delay(5000);
            await page.ClickAsync(FiltersAccountDropdown);
            return textContent;
        }

        public async Task selectStepType(string option)
        {
            await page.ClickAsync(FiltersStepTypeDropdown);
            await Task.Delay(3000);
            
            await page.ClickAsync("//li[@aria-label='"+option+"']");
            await Task.Delay(2000);
            await page.ClickAsync(FiltersStepTypeDropdown);
        }

        public async Task<bool> VerifyAccountIsVisible(string account)
        {
            return await WaitForElementVisible(page, "(//span[text()='" + account + "'])[1]");
        }

        public async Task ResetTemplateFilters()
        {
            await page.ClickAsync(ResetButton);
            await Task.Delay(2000);
        }

    }
}
