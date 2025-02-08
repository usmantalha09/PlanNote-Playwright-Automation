using AventStack.ExtentReports.Gherkin.Model;
using DnsClient.Protocol;
using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class PlansPage : Base
    {
        private readonly IPage page;

        private const string PlansSection = "//label[text()='Plans']";
        private const string CreatePlanButton = "//button[text()='Create Plan']";
        private const string CreatePlanAPopupTitle = "//h3[text()='Create a Plan']";
        private const string CreatePlanName = "//label[text()='Name']/parent::div//input";
        private const string CreatePlanAPopupSaveBtn = "//button[text()='Save']";
        private const string PlanDashboardTitle = "//strong[text()='Plan Dashboard']";
        private const string DetailsTab = "//div[text()='Details']";
        private const string DashboardTab = "//div[text()='Dashboard']";
        private const string SettingsTab = "//div[text()='Settings']";
        private const string BillingsTab = "//div[text()='Billings']";
        private const string ContactsTab = "//div[text()='Contacts']";
        private const string CampaignsTab = "//div[text()='Campaigns']";
        private const string CensusTab = "//div[text()='Census']";
        private const string ParticipantsTab = "//div[text()='Participants']";
        private const string ListTab = "//div[text()='List']";
        private const string InvoicesTab = "//div[text()='Invoices']";
        private const string RecordkeeperTab = "//div[text()='Recordkeeper']";
        private const string NoticesTab = "//div[text()='Notices']";
        private const string PlanSearchTxt = "//input[@placeholder='Search']";
        private const string NoRecordsToDisplayMsg = "//span[text()='No records to display.']";
        private const string CreatePlanAPopupCancelBtn = "//button[text()='Cancel']";
        private const string CreatePlanDeleteBtn = "//button[text()='Delete']";
        private const string AreYouSureYouWantToDeleteMsgOnPopup = "//label[contains(text(),'Are you sure, you want to delete')]";
        private const string DeletePlanNobtn = "//button[contains(text(),'No')]";
        private const string DeletePlanYesbtn = "//button[contains(text(),'Yes')]";
        private const string PlanDashboardNameTxt = "//strong[contains(text(),'Name')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardEINTxt = "//strong[contains(text(),'EIN')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardWebsiteTxt = "//strong[contains(text(),'Website')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardEmailTxt = "//strong[contains(text(),'Email')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardPhoneTxt = "//strong[contains(text(),'Phone')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardAddressOneTxt = "//strong[contains(text(),'Address 1')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardAddressTwoTxt = "//strong[contains(text(),'Address 2')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardCityTxt = "//strong[contains(text(),'City')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardStateTxt = "//strong[contains(text(),'State')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardZipTxt = "//strong[contains(text(),'Zip')]/parent::div/following-sibling::div/input";
        private const string PlanDashboardSaveBtn = "//button[text()='Save']";
        private const string PlanUpdatedSuccessfullyPopup = "//p[contains(text(),'Plan updated successfully!')]";
        private const string PlanDashboardCancelBtn = "//button[text()='Cancel']";
        private const string PlanNoticesTitle = "//strong[text()='Plan Notices']";
        private const string CreateNoticeBtn = "//span[text()[contains(.,'Create Notice')]]";
        private const string NoticePopupTitle = "//h1[text()='Notice']";
        private const string NoticePopupNameTxt = "//label[text()='Name']/following-sibling::input";
        private const string NoticePopupTagDropdown = "//label[text()='Tag']/parent::div/div";
        private const string NoticePopupTagDropdownFirstOption = "(//li[@class='rz-multiselect-item ']/span)[1]";
        private const string NoticePopupNoticeTypeDropdown = "//label[text()='Notice Type']/parent::div/div";
        private const string NoticePopupNoticeTypeDropdownFirstOption = "//div[@class='rz-dropdown-panel rz-popup']/ul/li[1]/span";
        private const string NoticePopupWorkflowDropdown = "//label[text()='Workflow']/parent::div/div";
        private const string NoticePopupWorkflowDropdownFirstOption = "(//li[@class='rz-dropdown-item '])[last()]/span";
        private const string NoticePopupVideoUrlTxt = "//label[text()='Video Url']/following-sibling::input";
        private const string NoticePopupChooseFiles = "//input[@class='form-control']";
        private const string NoticePopupSummaryTxt = "//label[text()='Summary']/following-sibling::textarea";
        private const string NoticePopupSaveBtn = "//button[text()='Save']";
        private const string NoticePopupCancelBtn = "//button[text()='Cancel']";
        private const string CreateNoticeNoRecordsToDisplay = "//span[text()='No records to display.']";
        private const string ErrorMessageForAllFields = "//button[contains(text(),'No')]";
        private const string RecordkeeperPageTitle = "//strong[text()='Recordkeeper']";
        private const string RecordkeeperPlanIdTxt = "//label[text()='Recordkeeper Plan Id']/parent::div//input";
        private const string RecordkeeperProductIdTxt = "//label[text()='Recordkeeper Product Id']/parent::div//input";
        private const string RecordkeeperFeedIdTxt = "//label[text()='Recordkeeper Feed Id']/parent::div//input";
        private const string RecordkeeperSaveBtn = "//button[text()='Save']";
        private const string RecordkeeperPlanDetailsUpdatedSuccessfullyPopupMsg = "//p[text()='Plan details updated successfully!']";
        private const string BilliSettingsAndServicesTitle = "//strong[text()='Settings & Services']";
        private const string BillingPeriodDropdown = "//label[text()='Billing Period']/parent::div/div/div";
        private const string BillingMethodDropdown = "//label[text()='Billing Method']/parent::div/div/div";
        private const string BilliSaveBtn = "//button[text()='Save']";
        private const string BilliPlanSettingsUpdatedSuccessfullyPopup = "//p[contains(text(),'Plan Settings updated successfully!')]";
        private const string CensusPageTitle = "//strong[text()='Plan Census Files']";
        private const string ImportCensusFileBtn = "//button[contains(text(),'Import Census File')]";
        private const string NewCensusFileTitle = "//strong[contains(text(),'New Census File')]";
        private const string CensusImportCensusFile = "//label[contains(text(),'Census File')]/following-sibling::input";
        private const string CensusImportCensusFileSaveBtn = "//span[contains(text(),'Save')]/..";
        private const string ImportStatus = "//a[contains(text(),'Import')]";
        private const string ConversionCompleteStatus = "//span[contains(text(),'Conversion Complete')]";
        private const string MappingPageTitle = "//h4[text()='Map Columns']";
        private const string MappingSSNDropdown = "//td[text()='SSN']/following-sibling::td/select";
        private const string MappingFullNameDropdown = "//td[text()='Full Name']/following-sibling::td/select";
        private const string MappingEmployementStatusDropdown = "//td[text()='Employement Status']/following-sibling::td/select";
        private const string MappingEmailOneDropdown = "//td[text()='Email 1']/following-sibling::td/select";
        private const string MappingPhoneOneDropdown = "//td[text()='Phone 1']/following-sibling::td/select";
        private const string MappingFullAddressDropdown = "//td[text()='Full Address']/following-sibling::td/select";
        private const string MappingCityDropdown = "//td[text()='City']/following-sibling::td/select";
        private const string MappingStateDropdown = "//td[text()='State']/following-sibling::td/select";
        private const string MappingZipDropdown = "//td[text()='Zip']/following-sibling::td/select";
        private const string MappingBirthdayDropdown = "//td[text()='Birthday']/following-sibling::td/select";
        private const string MappingHiredDateDropdown = "//td[text()='Hired Date']/following-sibling::td/select";
        private const string MappingRehiredDateDropdown = "//td[text()='Rehired Date']/following-sibling::td/select";
        private const string MappingTerminationDateDropdown = "//td[text()='Termination Date']/following-sibling::td/select";
        private const string MappingBalanceDropdown = "//td[text()='Balance']/following-sibling::td/select";
        private const string MappingEligibleForEscalationDropdown = "//td[text()='Eligible for Escalation?']/following-sibling::td/select";
        private const string MappingNextBtn = "//span[text()='Next']";
        private const string PreProcessingComplete = "//div[text()='Pre-Processing Complete']";
        private const string ImportBtn = "//button[text()='Import']";
        private const string AllEmployeesRecords = "//tr[@class='table-row']";
        private const string AllNewEmployees = "//td[text()='Active']";
        private const string AllExEmployees = "//td[text()='Inactive']";
        private const string QueuedForImportStatus = "//span[contains(text(),'Queued for Import')]";
        private const string ImportedStatus = "//span[text()='Imported']";
        private const string createCampaignBtn = "//button[contains(text(),'Create Campaign')]";
        private const string createACampaignPopupTitle = "//h1[contains(text(),'Create a Campaign')]";
        private const string createACampaignPopupNameTxt = "//label[contains(text(),'Name')]/following-sibling::div/input";
        private const string createACampaignPopupSaveBtn = "//button[contains(text(),'Save')]";
        private const string ConfigureNoticeText = "//div[text()='Configure Notice']";



        public PlansPage(IPage page)
        {
            this.page = page;
        }

        public async Task clickOnPlansSection()
        {
            await page.ClickAsync(PlansSection);
        }

        public async Task clickOnCreatePlanButton()
        {
            await page.ClickAsync(CreatePlanButton);
        }

        public async Task<bool> verifyCreatePlanAPopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, CreatePlanAPopupTitle);
        }

        public async Task enterCreatePlanName(string planName)
        {
            await WaitForElementVisible(page, CreatePlanName);
            await EnterValue(page, CreatePlanName, planName);
        }

        public async Task clickOnCreatePlanAPopupSaveButton()
        {
            await page.ClickAsync(CreatePlanAPopupSaveBtn);
        }

        public async Task<bool> verifyPlanDashboardTitleIsVisible()
        {
            return await WaitForElementVisible(page, PlanDashboardTitle);
        }

        public async Task<bool> verifyDetailsTabIsVisible()
        {
            return await WaitForElementVisible(page, DetailsTab);
        }

        public async Task<bool> verifySettingsTabIsVisible()
        {
            return await WaitForElementVisible(page, SettingsTab);
        }

        public async Task<bool> verifyBillingsTabIsVisible()
        {
            return await WaitForElementVisible(page, BillingsTab);
        }

        public async Task<bool> verifyContactsTabIsVisible()
        {
            return await WaitForElementVisible(page, ContactsTab);
        }

        public async Task<bool> verifyCampaignsTabIsVisible()
        {
            return await WaitForElementVisible(page, CampaignsTab);
        }

        public async Task<bool> verifyCensusTabIsVisible()
        {
            return await WaitForElementVisible(page, CensusTab);
        }

        public async Task<bool> verifyParticipantsTabIsVisible()
        {
            return await WaitForElementVisible(page, ParticipantsTab);
        }

        public async Task<bool> verifyListTabIsVisible()
        {
            return await WaitForElementVisible(page, ListTab);
        }

        public async Task<bool> verifyInvoicesTabIsVisible()
        {
            return await WaitForElementVisible(page, InvoicesTab);
        }

        public async Task<bool> verifyRecordkeeperTabIsVisible()
        {
            return await WaitForElementVisible(page, RecordkeeperTab);
        }

        public async Task<bool> verifyNoticesTabIsVisible()
        {
            return await WaitForElementVisible(page, NoticesTab);
        }

        public async Task<bool> verifyNoNewPlanCreated()
        {
            return await WaitForElementVisible(page, NoRecordsToDisplayMsg, 5000);
        }

        public async Task enterPlanNameSearch(string planName)
        {
            await WaitForElementVisible(page, PlanSearchTxt);
            await page.FillAsync(PlanSearchTxt, planName);
        }

        public async Task clickOnCreatePlanAPopupCancelButton()
        {
            await page.ClickAsync(CreatePlanAPopupCancelBtn);
        }

        public async Task<bool> verifySystemDetectsTheBlankNameFieldAndHighlightsTheBorderOfTheNameFieldInRed()
        {
            // Wait for the element to be visible
            if (await WaitForElementVisible(page, CreatePlanName))
            {
                // Check the background color
                string backgroundColor = await GetElementBackgroundColorAsync(page, CreatePlanName);

                // Add your logic to verify the background color
                if (backgroundColor == "rgb(128, 0, 0)")
                {
                    // Background color is as expected
                    return true;
                }
                else
                {
                    // Background color is not as expected
                    return false;
                }
            }

            // Element not visible, return false
            return false;
        }

        public async Task clickOnCreatePlanDeleteButton()
        {
            await WaitForElementVisible(page, CreatePlanDeleteBtn);
            await page.ClickAsync(CreatePlanDeleteBtn);
        }

        public async Task<bool> verifyAreYouSureYouWantToDeleteMsgOnPopupIsVisible()
        {
            return await WaitForElementVisible(page, AreYouSureYouWantToDeleteMsgOnPopup);
        }

        public async Task clickOnDeletePlanNoButton()
        {
            await page.ClickAsync(DeletePlanNobtn);
        }

        public async Task clickOnDeletePlanYesButton()
        {
            await page.ClickAsync(DeletePlanYesbtn);
        }

        public async Task<bool> VerifyPlanNotDeleted(string planName)
        {
            return await WaitForElementVisible(page, "//strong[contains(text(),'" + planName + "')]");
        }

        public async Task<bool> verifyCreatePlanDeleteButtonIsVisible()
        {
            return await WaitForElementVisible(page, CreatePlanDeleteBtn);
        }

        public async Task enterPlanDashboardName(string updatePlanName)
        {
            await EnterValue(page, PlanDashboardNameTxt, updatePlanName);
        }

        public async Task enterPlanDashboardEIN(string updatePlanEIN)
        {
            await page.FillAsync(PlanDashboardEINTxt, updatePlanEIN);
        }

        public async Task enterPlanDashboardWebsite(string updatePlanWebsite)
        {
            await page.FillAsync(PlanDashboardWebsiteTxt, updatePlanWebsite);
        }

        public async Task enterPlanDashboardEmail(string updatePlanEmail)
        {
            await page.FillAsync(PlanDashboardEmailTxt, updatePlanEmail);
        }

        public async Task enterPlanDashboardPhone(string updatePlanPhone)
        {
            await page.FillAsync(PlanDashboardPhoneTxt, updatePlanPhone);
        }

        public async Task enterPlanDashboardAddressOne(string updatePlanAddressOne)
        {
            await page.FillAsync(PlanDashboardAddressOneTxt, updatePlanAddressOne);

        }
        public async Task enterPlanDashboardAddressTwo(string updatePlanAddressTwo)
        {
            await page.FillAsync(PlanDashboardAddressTwoTxt, updatePlanAddressTwo);
        }

        public async Task enterPlanDashboardCity(string updatePlanCity)
        {
            await page.FillAsync(PlanDashboardCityTxt, updatePlanCity);
        }

        public async Task enterPlanDashboardState(string updatePlanState)
        {
            await page.FillAsync(PlanDashboardStateTxt, updatePlanState);
        }

        public async Task enterPlanDashboardZip(string updatePlanZip)
        {
            await page.FillAsync(PlanDashboardZipTxt, updatePlanZip);
        }

        public async Task clickOnPlanDashboardSaveButton()
        {
            await page.ClickAsync(PlanDashboardSaveBtn);
        }

        public async Task<bool> verifyPlanUpdatedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, PlanUpdatedSuccessfullyPopup);
        }

        public async Task<bool> VerifyUpdatePlanName(string updatePlanName)
        {
            return await WaitForElementVisible(page, "//a[text()='" + updatePlanName + "']");
        }

        public async Task clickOnPlanDashboardCancelButton()
        {
            await page.ClickAsync(PlanDashboardCancelBtn);
        }

        public async Task clickOnPlanDashboardNoticesTab()
        {
            await page.ClickAsync(NoticesTab);
        }

        public async Task<bool> VerifyPlanNoticesTitle()
        {
            return await WaitForElementVisible(page, PlanNoticesTitle);
        }

        public async Task clickOnCreateNoticeButton()
        {
            await page.ClickAsync(CreateNoticeBtn);
        }

        public async Task<bool> VerifyNoticePopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, NoticePopupTitle);
        }

        public async Task enterNoticePopupName(string noticePopupName)
        {
            await EnterValue(page, NoticePopupNameTxt, noticePopupName);
        }
        public async Task selectTagFirstOption()
        {
            await page.ClickAsync(NoticePopupTagDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(NoticePopupTagDropdownFirstOption);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(NoticePopupTagDropdown);
        }

        public async Task selectNoticeTypeFirstOption()
        {
            await page.ClickAsync(NoticePopupNoticeTypeDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(NoticePopupNoticeTypeDropdownFirstOption);
        }

        public async Task selectWorkflowFirstOption()
        {
            await page.ClickAsync(NoticePopupWorkflowDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync(NoticePopupWorkflowDropdownFirstOption);
        }

        public async Task noticePopupChooseFiles()
        {
            // Select one file
            await Task.Delay(1000);
            await page.SetInputFilesAsync(NoticePopupChooseFiles, resourcesDataFolderPath + "\\Simple.pdf");
        }

        public async Task<bool> waitForConfigureNoticeText()
        {
            return await WaitForElementVisible(page, ConfigureNoticeText);
        }

        public async Task enterNoticePopupSummary(string summary)
        {
            await EnterValue(page, NoticePopupSummaryTxt, summary);
        }
        public async Task enterNoticePopupVideoUrl(string videoUrl)
        {
            await EnterValue(page, NoticePopupVideoUrlTxt, videoUrl);
        }
        public async Task clickOnNoticePopupSaveButton()
        {
            await page.ClickAsync(NoticePopupSaveBtn);
            await Task.Delay(3000); // 1-second delay
        }

        public async Task clickOnNoticePopupCancelButton()
        {
            await page.ClickAsync(NoticePopupCancelBtn);
        }
        public async Task<bool> VerifyNewNoticeInTheNoticesListIsVisible(string description)
        {
            return await WaitForElementVisible(page, "//a[text()='" + description + "']");
        }

        public async Task<bool> VerifyPlanNoticesNoRecordsToDisplayIsVisible()
        {
            return await WaitForElementVisible(page, CreateNoticeNoRecordsToDisplay);
        }

        public async Task<bool> VerifyErrorMessagesForBlankFieldsAreAreVisible()
        {
            return await WaitForElementVisible(page, ErrorMessageForAllFields);
        }

        public async Task clickOnRecordkeeperTab()
        {
            await page.ClickAsync(RecordkeeperTab);
        }

        public async Task<bool> VerifyRecordkeeperPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, RecordkeeperPageTitle);
        }

        public async Task enterRecordkeeperPlanId(string recordkeeperPlanId)
        {
            await EnterValue(page, RecordkeeperPlanIdTxt, recordkeeperPlanId);
        }

        public async Task enterRecordkeeperProductId(string recordkeeperProductId)
        {
            await EnterValue(page, RecordkeeperProductIdTxt, recordkeeperProductId);
        }

        public async Task enterRecordkeeperFeedId(string recordkeeperFeedId)
        {
            await EnterValue(page, RecordkeeperFeedIdTxt, recordkeeperFeedId);
        }
        public async Task clickOnRecordkeeperSaveButton()
        {
            await page.ClickAsync(RecordkeeperSaveBtn);
        }

        public async Task<bool> VerifyRecordkeeperPlanDetailsUpdatedSuccessfullyPopupMsgIsVisible()
        {
            return await WaitForElementVisible(page, RecordkeeperPlanDetailsUpdatedSuccessfullyPopupMsg);
        }

        public async Task clickOnBillingsTab()
        {
            await page.ClickAsync(BillingsTab);
        }
        public async Task<bool> VerifyBilliSettingsAndServicesTitleIsVisible()
        {
            return await WaitForElementVisible(page, BilliSettingsAndServicesTitle);
        }

        public async Task selectBillingPeriod(string option)
        {
            await page.ClickAsync(BillingPeriodDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='" + option + "']");
        }

        public async Task selectBillingMethod(string option)
        {
            await page.ClickAsync(BillingMethodDropdown);
            await Task.Delay(3000); // 1-second delay
            await page.ClickAsync("//li[@aria-label='" + option + "']");
        }

        public async Task clickOnBilliSaveButton()
        {
            await page.ClickAsync(BilliSaveBtn);
        }

        public async Task<bool> VerifyBilliPlanSettingsUpdatedSuccessfullyPopupIsVisible()
        {
            return await WaitForElementVisible(page, BilliPlanSettingsUpdatedSuccessfullyPopup);
        }

        public async Task clickOnCreatedPlanName(string planName)
        {
            await page.ClickAsync("//a[text()='" + planName + "']");
        }

        public async Task<bool> VerifyBillMethodSelectedOptionIsVisible(string selectedOption)
        {
            return await WaitForElementVisible(page, "//span[text()='" + selectedOption + "']");
        }
        public async Task clickOnDashboardTab()
        {
            await page.ClickAsync(DashboardTab);
        }
        public async Task clickOnContactsTab()
        {
            await page.ClickAsync(ContactsTab);
        }
        public async Task clickOnParticipantsTab()
        {
            await page.ClickAsync(ParticipantsTab);
        }
        public async Task clickOnCensusTab()
        {
            await page.ClickAsync(CensusTab);
        }

        public async Task<bool> verifyNewCensusFileTitleIsVisible()
        {
            return await WaitForElementVisible(page, NewCensusFileTitle);
        }

        public async Task clickOnImportCensusFileButton()
        {
            await page.ClickAsync(ImportCensusFileBtn);
        }

        public async Task UploadNewCensusFile()
        {

            await page.SetInputFilesAsync(CensusImportCensusFile, fullPath);
        }

        public async Task UploadAnotherNewCensusFile()
        {
            await page.SetInputFilesAsync(CensusImportCensusFile, fullPath2);
        }

        public async Task clickOnCensusImportCensusFileSaveButton()
        {
            await page.ClickAsync(CensusImportCensusFileSaveBtn);
        }

        public async Task<bool> verifyImportStatusIsVisible()
        {
            return await WaitForElementVisible(page, ImportStatus,100000);
        }

        public async Task<bool> verifyConversionCompleteStatusIsVisible()
        {
            return await WaitForElementVisible(page, ConversionCompleteStatus);
        }

        public async Task clickOnImportStatusLink()
        {
            await page.ClickAsync(ImportStatus);
        }

        public async Task<bool> verifyMappingPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, MappingPageTitle);
        }
        public async Task mappingSSN(string option)
        {
            await page.SelectOptionAsync(MappingSSNDropdown, option);
        }

        public async Task mappingFullName(string option)
        {
            await ScrollToElement(page, MappingFullNameDropdown);
            await page.SelectOptionAsync(MappingFullNameDropdown, option);
        }

        public async Task mappingEmployementStatus(string option)
        {
            await ScrollToElement(page, MappingEmployementStatusDropdown);
            await page.SelectOptionAsync(MappingEmployementStatusDropdown, option);
        }

        public async Task mappingEmail(string option)
        {
            await ScrollToElement(page, MappingEmailOneDropdown);
            await page.SelectOptionAsync(MappingEmailOneDropdown, option);
        }

        public async Task mappingPhone(string option)
        {
            await ScrollToElement(page, MappingPhoneOneDropdown);
            await page.SelectOptionAsync(MappingPhoneOneDropdown, option);
        }

        public async Task mappingFullAddress(string option)
        {
            await ScrollToElement(page, MappingFullAddressDropdown);
            await page.SelectOptionAsync(MappingFullAddressDropdown, option);
        }
        public async Task mappingCity(string option)
        {
            await ScrollToElement(page, MappingCityDropdown);
            await page.SelectOptionAsync(MappingCityDropdown, option);
        }
        public async Task mappingState(string option)
        {
            await ScrollToElement(page, MappingStateDropdown);
            await page.SelectOptionAsync(MappingStateDropdown, option);
        }
        public async Task mappingZip(string option)
        {
            await ScrollToElement(page, MappingZipDropdown);
            await page.SelectOptionAsync(MappingZipDropdown, option);
        }
        public async Task mappingBirthday(string option)
        {
            await ScrollToElement(page, MappingBirthdayDropdown);
            await page.SelectOptionAsync(MappingBirthdayDropdown, option);
        }
        public async Task mappingHiredDate(string option)
        {
            await ScrollToElement(page, MappingHiredDateDropdown);
            await page.SelectOptionAsync(MappingHiredDateDropdown, option);
        }
        public async Task mappingRehiredDate(string option)
        {
            await ScrollToElement(page, MappingHiredDateDropdown);
            await page.SelectOptionAsync(MappingRehiredDateDropdown, option);
        }
        public async Task mappingTerminationDate(string option)
        {
            await ScrollToElement(page, MappingTerminationDateDropdown);
            await page.SelectOptionAsync(MappingTerminationDateDropdown, option);
        }

        public async Task mappingBalance(string option)
        {
            await ScrollToElement(page, MappingBalanceDropdown);
            await page.SelectOptionAsync(MappingBalanceDropdown, option);
        }

        public async Task mappingEligibleForEscalation(string option)
        {
            await ScrollToElement(page, MappingEligibleForEscalationDropdown);
            await page.SelectOptionAsync(MappingEligibleForEscalationDropdown, option);
        }

        public async Task<int> getCountOfAllEmployeesRecords()
        {
            // Get the length of elements matching the selector
            var elementsCount = await page.EvaluateAsync<int>(@"(xpath) => {
            const elements = document.evaluate(xpath, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
            return elements.snapshotLength;
            }", AllEmployeesRecords);
            return elementsCount;
        }

        public async Task<int> getCountOfAllNewEmployees()
        {
            // Get the length of elements matching the selector
            var elementsCount = await page.EvaluateAsync<int>(@"(xpath) => {
            const elements = document.evaluate(xpath, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
            return elements.snapshotLength;
            }", AllNewEmployees);
            return elementsCount;
        }

        public async Task<int> getCountOfAllExEmployees()
        {
            // Get the length of elements matching the selector
            var elementsCount = await page.EvaluateAsync<int>(@"(xpath) => {
            const elements = document.evaluate(xpath, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
            return elements.snapshotLength;
            }", AllExEmployees);

            return elementsCount;
        }
        public async Task clickOnMappingNextButton()
        {
            await page.ClickAsync(MappingNextBtn);
        }

        public async Task<bool> verifyPreProcessingCompleteIsVisible()
        {
            return await WaitForElementVisible(page, PreProcessingComplete,100000);
        }

        public async Task<bool> verifyImportButtonIsVisible()
        {
            return await WaitForElementVisible(page, ImportBtn);
        }        

        public async Task<bool> verifyAllNewEmployeesIsVisible(int value)
        {
            return await WaitForElementVisible(page, "//strong[text()='New Records']/../parent::div/h5[text()='" + value + "']");
        }

        public async Task<bool> verifyAllExEmployeesIsVisible(int value)
        {
            return await WaitForElementVisible(page, "//strong[text()='Are Ex-Employees']/../parent::div/h5[text()='" + value + "']");
        }
        public async Task clickOnImportButton()
        {
            await ScrollToElement(page, ImportBtn);
            await page.ClickAsync(ImportBtn);
        }

        public async Task<bool> verifyQueuedForImportStatusIsVisible()
        {
            return await WaitForElementVisible(page, QueuedForImportStatus,50000);
        }
        public async Task<bool> verifyImportedStatusIsVisible()
        {
            return await WaitForElementVisible(page, ImportedStatus, 100000);
        }

        public async Task clickOnListTab()
        {
            await page.ClickAsync(ListTab);
        }

        public async Task<bool> verifyImportedFileIsVisible()
        {
            return await WaitForElementVisible(page, "//a[text()='"+ fileName + "']");
        }

        public async Task clickOnCampaignsTab()
        {
            await page.ClickAsync(CampaignsTab);
        }

        public async Task clickOnCreateCampaignButton()
        {
            await page.ClickAsync(createCampaignBtn);
        }

        public async Task<bool> verifyCreateACampaignPopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, createACampaignPopupTitle, 100000);
        }

        public async Task enterNameInCreateCampaignPopup(string name)
        {
            await EnterValue(page, createACampaignPopupNameTxt, name);
        }

        public async Task clickOnCreateACampaignPopupSaveButton()
        {
            await page.ClickAsync(createACampaignPopupSaveBtn);
        }

        public async Task<bool> VerifyFilesAreListedLatestOnTop()
        {
            return await WaitForElementVisible(page, "//a[contains(text(), 'ADNAN Test Census File - 2.xlsx')]", 100000);
        }


    }
}
