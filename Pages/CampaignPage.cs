using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class CampaignPage : Base
    {
        private readonly IPage page;

        private const string campaignsSiteMenu = "(//label[contains(text(),'Campaigns')])[1]";
        private const string campaignsPageTitle = "//strong[contains(text(),'Campaigns')]";
        private const string campaignsPageSearchTxt = "//input[@placeholder='Search']";
        private const string createCampaignButton = "//button[contains(text(), 'Create Campaign')]";
        private const string CreateCampaignModalHeading = "//h1[contains(text(), 'Create a Campaign')]";
        private const string CreateCampaignName = "//label[text()='Name']/parent::div//input";
        private const string PlanDropdown = "//label[text()='Plan']/following-sibling::div";
        private const string PlanDropdownFirstOption = "((//input[@aria-label='Search']/parent::div/following-sibling::div)[last()]//span)[1]";
        private const string PlanDropdownWhitelistOption = "((//input[@aria-label='Search']/parent::div/following-sibling::div)[last()]//span[text()='WhilteList-11-11-23'])[1]";
        private const string searchOptionInPlanDropdown = "(//input[@aria-label='Search'])[2]";
        private const string TemplateAccountDropdownBtn = "//label[contains(text(),'Template Account')]/following-sibling::div";
        private const string TemplateAccountDropdownBtnFirstOption = "//*[@class=\"rz-dropdown-panel rz-popup\"][last()]/div/ul/li/span";
        private const string CampaignSaveBtn = "//button[text()='Save']";
        private const string configureCampaignText = "//strong[text()=\"Configure Campaign\"]";
        private const string ThankyouEmailTemplateAccountDropdownBtn = "//label[contains(text(),'Thank You Email Template')]/following-sibling::div";
        private const string ThankyouEmailTemplateAccountDropdownBtnFirstOption = "//div[@class='rz-dropdown-panel rz-popup']/div[2]/ul/li/span";
        private const string CampaignAddStep = "//button[text()='Add Step']";
        private const string AddStepCampaignModalHeader = "//h4[contains(text(), 'Add Campaign Step')]";
        private const string StepTypeDropdownBtn = "//label[contains(text(),'Step Type')]/following-sibling::div";
        private const string StepTypeDropdownBtnEmailOption = "//label[contains(text(),'Step Type')]/following-sibling::div/span[text()='Email']";
        private const string addStepButton = "//button[text()='Add']";
        private const string ParticipantsTab = "//span[contains(text(), 'Participants')]";
        private const string AddParticipantsButton = "//button[contains(text(), 'Add Participants')]";
        private const string SelectCampaignParticipantsModalHeading = "//h4[contains(text(), 'Select Campaign Participants')]";
        private const string StartNowButton = "//button[contains(text(), 'Start Now')]";
        private const string VideoURL = "//label[text()='Video URL']/parent::div//input";
        private const string ReadyStatusOfCampaign = "//div[contains(@class, \"rounded-pill\") and contains(text(),'Ready')]";
        private const string StoppedStatusOfCampaign = "//div[contains(@class, \"rounded-pill\") and contains(text(),'Stopped')]";
        private const string FileUploadedConfirmation = "//a[contains(text(), 'Update')]";
        private const string RecipientOnEmailStep = "//label[contains(text(),'Recipients')]/following-sibling::div";
        private const string PlanAdministratorDropdownOption = "//div[@class='rz-dropdown-panel rz-popup']/div/ul/li[2]/span";
        private const string ActionButtonOnCampaignPage = "//button[contains(text(), 'Actions')]";
        private const string StopAndArchiveOption = "//a[contains(text(), 'Stop & Archive')]";
        private const string DeleteOption = "//a[contains(text(), 'Delete')]";


        public CampaignPage(IPage page)
        {
            this.page = page;
        }

        public async Task enterCampaignsSearchValue(string name)
        {
            await Task.Delay(4000);
            await page.FillAsync(campaignsPageSearchTxt, name);
        }

        public async Task clickOnCampaignsSiteMenu()
        {
            await page.ClickAsync(campaignsSiteMenu);
        }

        public async Task<bool> verifyCampaignsPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, campaignsPageTitle);
        }

        public async Task<bool> verifyCampaignsSearchValueIsVisible(string searchName)
        {
            return await WaitForElementVisible(page, "(//a[contains(text(),'" + searchName + "')])[1]");
        }

        public async Task ClickOnCreateCampaignButton()
        {
            await page.ClickAsync(createCampaignButton);
        }

        public async Task<bool> VerifyCreateCampaignModalIsDisplaying()
        {
            return await WaitForElementVisible(page, CreateCampaignModalHeading);
        }

        public async Task enterCreateCampaignName(string campaignName)
        {
            await WaitForElementVisible(page, CreateCampaignName);
            await EnterValue(page, CreateCampaignName, campaignName);
        }

        public async Task selectPlanDropdownWhitelistOption()
        {
            await page.ClickAsync(PlanDropdown);
            await Task.Delay(3000); // 1-second delay

            foreach (char character in "whi")
            {
                await page.Keyboard.TypeAsync(character.ToString());
                await Task.Delay(500);
            }
            await Task.Delay(5000); // 1-second delay
            await page.ClickAsync(PlanDropdownWhitelistOption);
            await Task.Delay(3000);
        }

        public async Task selectTemplateAccountDropdownFirstOption()
        {
            await Task.Delay(1000); // 1-second delay
            await page.ClickAsync(TemplateAccountDropdownBtn);
            await page.ClickAsync(TemplateAccountDropdownBtnFirstOption);
        }

        public async Task clickOnCampaignSaveButton()
        {
            await page.ClickAsync(CampaignSaveBtn);
            await Task.Delay(4000);
        }

        public async Task<bool> verifyConfigureCampaignPageIsDisplaying()
        {
            return await WaitForElementVisible(page, configureCampaignText, 120000);
        }

        public async Task addVideoURL(string video)
        {
            await Task.Delay(2000);
            await page.FillAsync(VideoURL, video);
            await Task.Delay(2000);
        }

        public async Task selectThankYouEmailTemplate()
        {
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(ThankyouEmailTemplateAccountDropdownBtn);
            await page.ClickAsync(ThankyouEmailTemplateAccountDropdownBtnFirstOption);
            await Task.Delay(1000); // 1-second delay
        }

        public async Task addEmailStep()
        {
            await page.ClickAsync(CampaignAddStep);
        }

        public async Task<bool> VerifyAddCampaignStepIsDisplaying()
        {
            return await WaitForElementVisible(page, AddStepCampaignModalHeader);
        }

        public async Task selectStepTypeEmailFromAddCampaignStep()
        {
            await Task.Delay(1000); // 1-second delay
            await page.ClickAsync(StepTypeDropdownBtn);
            await page.ClickAsync(StepTypeDropdownBtnEmailOption);
            await Task.Delay(4000); // 1-second delay
        }

        public async Task clickOnAddButton()
        {
            await page.ClickAsync(addStepButton);
            await Task.Delay(4000);
        }

        public async Task clickOnParticipantsTab()
        {
            await page.ClickAsync(ParticipantsTab);
            await page.ClickAsync(AddParticipantsButton);
            await Task.Delay(1000);
        }

        public async Task<bool> VerifySelectCampaignParticipantsModalIsDisplaying()
        {
            return await WaitForElementVisible(page, SelectCampaignParticipantsModalHeading);
        }

        public async Task ClickOnStartNowCampaign()
        {
            await page.ClickAsync(StartNowButton);
            await Task.Delay(1000);
        }

        public async Task<bool> VerifyCampaignStatusUpdatedFromNotReadyToReady()
        {
            return await WaitForElementVisible(page, ReadyStatusOfCampaign);
        }

        public async Task<bool> VerifyFileHasBeenAttached()
        {
            return await WaitForElementVisible(page, FileUploadedConfirmation);
        }

        public async Task ChooseFiles()
        {

            await Task.Delay(4000);
            var path = "C:\\Users\\USER\\Desktop\\Plan_notes_Playwright\\PlanNotePlaywrite\\resources\\data";
            var fileInputElement = await page.QuerySelectorAsync("input[type=file]");
            await fileInputElement.SetInputFilesAsync(path + "\\Simple.pdf");

            await Task.Delay(1000);
        }

        public async Task selectPlanAdministratorFromRecipient()
        {
            await Task.Delay(1000);
            await page.ClickAsync(RecipientOnEmailStep);
            await page.Keyboard.PressAsync("ArrowDown");

//            await page.ClickAsync(PlanAdministratorDropdownOption);
            await Task.Delay(1000);
        }

        public async Task StopTheRunningTestCampaign()
        {
            await page.ClickAsync(ActionButtonOnCampaignPage);
            await page.ClickAsync(StopAndArchiveOption);
        }

        public async Task<bool> VerifyCampaignStatusUpdatedFromRunningToStopped()
        {
            return await WaitForElementVisible(page, StoppedStatusOfCampaign);
        }

        public async Task DeleteTheStoppedTestCampaign()
        {
            await page.ClickAsync(ActionButtonOnCampaignPage);
            await page.ClickAsync(DeleteOption);

        }
    }
}
