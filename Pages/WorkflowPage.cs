using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class WorkflowPage : Base
    {
        private readonly IPage page;

        private const string WorkflowsLiftSideMenuOption = "//label[contains(text(),'Workflows')]";
        private const string WorkflowsPageTitle = "//h3[contains(text(),'Workflows')]";
        private const string NewWorkflowBtn = "//button[contains(text(),'New Workflow')]";
        private const string WorkflowNameTxt= "//label[contains(text(),'Name')]/following-sibling::input";
        private const string AccountDropdown = "//label[contains(text(),'Account')]/following-sibling::div";
        private const string AccountDropdownFirstOption = "//*[@class=\"rz-dropdown-panel rz-popup\"][last()]/div/ul/li/span";
        private const string StepNametxt = "//label[contains(text(),'Step Name')]/following-sibling::input";
        private const string AddStepBtn = "//button[contains(text(),'Add Step')]";
        private const string AddWorkflowStepPopupTitle = "//h4[contains(text(),'Add')]";
        private const string StepTypeDropdownBtn = "//label[contains(text(),'Step Type')]/following-sibling::div";        
        private const string TemplateDropdownBtn = "//label[contains(text(),'Workflow Type')]/following-sibling::div";
        private const string TemplateDropdownFirstOption = "//*[@class=\"rz-dropdown-panel rz-popup\"][last()]/div/ul/li/span";
        private const string AddWorkflowStepPopupAddBtn = "//h4[contains(text(),'Add')]/parent::div/..//button[contains(text(),'Add')]";
        private const string WorkflowSaveBtn = "//button[text()='Save']";
        private const string WorkflowCreateSuccessfullyPopupMsg = "//p[contains(text(),'Workflow create successfully!')]";
        private const string WorkflowDeleteBtn = "//button[text()='Delete']";
        private const string NoRecordsToDisplayMsg = "//span[text()='No records to display.']";
        private const string WorkflowFilterBtn = "//button[text()[contains(.,'Filter')]]";
        private const string FiltersAccountDropdown = "(//div[text()='Accounts']/parent::div/div/div/span)[1]";
        private const string FiltersAccountDropdownFirstOption = "((//ul[@role='listbox'])[last()]/li//span)[2]";



        public WorkflowPage(IPage page)
        {
            this.page = page;
        }

        public async Task clickOnWorkflowsLiftSideMenuOption()
        {
            await page.ClickAsync(WorkflowsLiftSideMenuOption);
        }
        public async Task<bool> verifyWorkflowsPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, WorkflowsPageTitle, 50000);
        }

        public async Task clickOnNewWorkflowButton()
        {
            await page.ClickAsync(NewWorkflowBtn);
        }

        public async Task<bool> verifyWorkflowNameTitleIsVisible()
        {
            return await WaitForElementVisible(page, WorkflowNameTxt, 50000);
        }

        public async Task selectAccountDropdownFirstOption()
        {
            await page.ClickAsync(AccountDropdown);            
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(AccountDropdownFirstOption);
        }
        public async Task enterWorkflowName(string workflowName)
        {
            await page.FillAsync(WorkflowNameTxt, workflowName);
        }
        
        public async Task enterStepName(string stepName)
        {
            await EnterValue(page, StepNametxt, stepName);
        }

        public async Task clickOnAddStepButton()
        {
            await page.ClickAsync(AddStepBtn);
        }

        public async Task<bool> verifyAddWorkflowStepPopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, AddWorkflowStepPopupTitle, 50000);
        }

        public async Task selectStepTypeDropdownOption(string option)
        {
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(StepTypeDropdownBtn);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='"+ option + "']");
            await Task.Delay(3000);
        }
        public async Task selectTemplateDropdownFirstOption()
        {
            await Task.Delay(1000); // 1-second delay
            await page.ClickAsync(TemplateDropdownBtn);
            await page.ClickAsync(TemplateDropdownFirstOption);
        }

        public async Task clickOnAddWorkflowStepPopupAddButton()
        {
            await page.ClickAsync(AddWorkflowStepPopupAddBtn);
        }

        public async Task<bool> verifyAddedStepIsVisible(string option)
        {
            return await WaitForElementVisible(page, "//span[text()='" + option + "']", 50000);
        }
        public async Task clickOnWorkflowSaveButton()
        {
            await page.ClickAsync(WorkflowSaveBtn);
        }

        public async Task<bool> verifyWorkflowCreateSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, WorkflowCreateSuccessfullyPopupMsg, 50000);
        }

        public async Task<bool> verifyAddedWorkflowIsVisible(string option)
        {
            string workflow_name = "//label[text()='Workflow Name']/parent::div/input";
            //var element = await page.QuerySelectorAsync(workflow_name);
            //var textContent = await element.InnerTextAsync();

            return await WaitForElementVisible(page, workflow_name, 50000);
            //return await WaitForElementVisible(page, "//a[text()='" + option + "']", 50000);
        }

        public async Task clickOnAddedWorkflow(string option)
        {
            await page.ClickAsync("//a[text()='" + option + "']");
        }

        public async Task clickOnWorkflowDeleteButton()
        {
            await page.ClickAsync(WorkflowDeleteBtn);
        }
        public async Task<bool> verifyNoWorkflowsIsDisplay()
        {
            return await WaitForElementVisible(page, NoRecordsToDisplayMsg, 5000);
        }

        public async Task<bool> VerifyWorkflowFilterIsDisplaying()
        {
            return await WaitForElementVisible(page, WorkflowFilterBtn);
        }

        public async Task clickOnWorkflowFilterButton()
        {
            await page.ClickAsync(WorkflowFilterBtn);
            await Task.Delay(10000); // 1-second delay
        }

        public async Task SelectAccountFilter()
        {
            await page.ClickAsync(FiltersAccountDropdown);
            await Task.Delay(3000);
            await page.ClickAsync(FiltersAccountDropdownFirstOption);
            await Task.Delay(3000);
            await page.ClickAsync(FiltersAccountDropdown);
        }
    }
}
