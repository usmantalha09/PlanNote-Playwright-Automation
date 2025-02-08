using DnsClient.Protocol;
using Microsoft.Playwright;
using PlanNotePlaywrite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanNotePlaywrite.Pages
{
    internal class ParticpantsPage : Base
    {
        private readonly IPage page;

        private const string ParticipantsTab = "//label[text()='Participants']";
        private const string ParticipantsPageTitle = "//strong[text()='Participants']";
        private const string FirstParticipants = "(//span[text()='Plan']/ancestor::thead/following-sibling::tbody//a)[2]";
        private const string FirstParticipantsTitle = "(//div[text()='Personal Info'])[1]";
        private const string ParticipantsContactTab = "//div[text()='Contact']";
        private const string PhoneNumberDeleteOption = "//span[text()='Phone']/ancestor::thead/following-sibling::tbody//i[contains(@class,'fa-trash')]";
        private const string PhoneNumberEditOption = "//span[text()='Phone']/ancestor::thead/following-sibling::tbody//i[contains(@class,'fa-pencil')]";
        private const string DeleteYesPopupBtn = "//button[text()='Yes']";
        private const string EmailDeleteOption = "//span[text()='Email']/ancestor::thead/following-sibling::tbody//i[contains(@class,'fa-trash')]";
        private const string EmailEditOption = "//span[text()='Email']/ancestor::thead/following-sibling::tbody//i[contains(@class,'fa-pencil')]";
        private const string AddressLineDeleteOption = "//span[text()='Address line 1']/ancestor::thead/following-sibling::tbody//i[contains(@class,'fa-trash')]";
        private const string AddParticipantBtn = "//button[text()='Add Participant']";
        private const string AddParticipantPopupTitle = "//h1[text()='Add a Participant']";
        private const string AddParticipantFirstNameTxt = "//label[text()='First Name']/parent::div//input";
        private const string AddParticipantLastNameTxt = "//label[text()='Last Name']/parent::div//input";
        private const string AddParticipantPhoneTxt = "//label[text()='Phone']/parent::div//input";
        private const string AddParticipantEmailTxt = "//label[text()='Email']/parent::div//input";
        private const string AddParticipantAddressOneTxt = "//label[text()='Address 1']/parent::div//input";
        private const string AddParticipantPopupSaveBtn = "//button[text()='Save']";
        private const string ParticipantPhoneNoRecordsToDisplayLbl = "//span[text()='Phone']/ancestor::thead/following-sibling::tbody//span[text()='No records to display.']";
        private const string ParticipantEmailNoRecordsToDisplayLbl = "//span[text()='Phone']/ancestor::thead/following-sibling::tbody//span[text()='No records to display.']";
        private const string ParticipantAddressOneNoRecordsToDisplayLbl = "//span[text()='Phone']/ancestor::thead/following-sibling::tbody//span[text()='No records to display.']";
        private const string ParticipantSearchTxt = "//input[@placeholder='Search']";
        private const string ParticipantAddPhoneBtn = "//button[text()='Add Phone']";
        private const string ParticipantAddPhonePhoneTxt = "//label[text()='Phone']/following::input";
        private const string ParticipantSave = "//button[text()='Save']";
        private const string ParticipantAddPhoneAndAddEmailStatusDropdown = "//label[text()='Status ']/following-sibling::select";
        private const string ParticipantAddEmailBtn = "//button[text()='Add Email']";
        private const string ParticipantAddEmailTxt = "//label[text()='Email']/following-sibling::input";
        private const string ParticipantFilterBtn = "//button[text()[contains(.,'Filter')]]";
        private const string ParticipantFilterpopupTitle = "//div[text()[contains(.,'Filter')]]";
        private const string ParticipantSelectFilterDropdownBtn = "//div[text()[contains(.,'Plan')]]/parent::div/div/div/following-sibling::label";
        private const string PlanDropdown = "//div[text()='Plan']/parent::div/div/span";
        private const string PlanDropdownOption = "(//ul[@role='listbox']//span)[2]";



        public ParticpantsPage(IPage page)
        {
            this.page = page;
        }

        public async Task clickOnParticipantsTab()
        {
            await page.ClickAsync(ParticipantsTab);
        }

        public async Task<bool> VerifyParticipantsPageTitleIsVisible()
        {
            return await WaitForElementVisible(page, ParticipantsPageTitle);
        }

        public async Task clickOnAddParticipantButton()
        {
            await page.ClickAsync(AddParticipantBtn);
        }

        public async Task<bool> VerifyAddParticipantPopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, AddParticipantPopupTitle);
        }

        public async Task enterParticipantFirstName(string firstName)
        {
            await page.FillAsync(AddParticipantFirstNameTxt, firstName);
        }
        public async Task enterParticipantLastName(string lastName)
        {
            await page.FillAsync(AddParticipantLastNameTxt, lastName);
        }

        public async Task enterParticipantPhone(string phoneName)
        {
            await page.FillAsync(AddParticipantPhoneTxt, phoneName);
        }

        public async Task enterParticipantEmail(string email)
        {
            await page.FillAsync(AddParticipantEmailTxt, email);
        }

        public async Task enterParticipantAddressOne(string addressOne)
        {
            await page.FillAsync(AddParticipantAddressOneTxt, addressOne);
        }

        public async Task clickOnAddParticipantPopupSaveButton()
        {
            await page.ClickAsync(AddParticipantPopupSaveBtn);
        }
        public async Task clickOnParticipantsContactTab()
        {
            await page.ClickAsync(ParticipantsContactTab);
        }
        public async Task clickOnPhoneNumberDeleteOption()
        {
            await WaitForElementVisible(page, PhoneNumberDeleteOption);
            await page.ClickAsync(PhoneNumberDeleteOption);
        }
        public async Task clickOnEmailDeleteOption()
        {
            await WaitForElementVisible(page, EmailDeleteOption);
            await page.ClickAsync(EmailDeleteOption);
        }
        public async Task clickOnAddressLineDeleteOption()
        {
            //await WaitForElementVisible(page, AddressLineDeleteOption);
            await page.ClickAsync(AddressLineDeleteOption);
        }
        public async Task<bool> VerifyPhoneNumberDeleteOptionIsVisible()
        {
            return await WaitForElementVisible(page, PhoneNumberDeleteOption);
        }
        public async Task<bool> VerifyEmailDeleteOptionIsVisible()
        {
            return await WaitForElementVisible(page, EmailDeleteOption);
        }
        public async Task<bool> VerifyAddressLineDeleteOptionIsVisible()
        {
            return await WaitForElementVisible(page, AddressLineDeleteOption);
        }

        public async Task<bool> VerifyParticipantPhoneNoRecordsToDisplayIsVisible()
        {
            return await WaitForElementVisible(page, ParticipantPhoneNoRecordsToDisplayLbl);
        }
        public async Task<bool> VerifyParticipantEmailNoRecordsToDisplayIsVisible()
        {
            return await WaitForElementVisible(page, ParticipantEmailNoRecordsToDisplayLbl);
        }

        public async Task<bool> VerifyParticipantAddressOneNoRecordsToDisplayIsVisible()
        {
            return await WaitForElementVisible(page, ParticipantAddressOneNoRecordsToDisplayLbl);
        }
        public async Task clickOnDeleteYesPopupButton()
        {
            await WaitForElementVisible(page, DeleteYesPopupBtn);
            await page.ClickAsync(DeleteYesPopupBtn);
        }

        public async Task enterParticipantSearchField(string participantName)
        {
            await page.FillAsync(ParticipantSearchTxt, participantName);
        }

        public async Task<bool> VerifySearchParticipantNameIsVisible(string participantName)
        {
            return await WaitForElementVisible(page, "(//a[contains(text(),'"+ participantName + "')])[last()]",50000);
        }

        public async Task clickOnFirstParticipants()
        {
            await page.ClickAsync(FirstParticipants);
        }

        public async Task<bool> VerifyFirstParticipantsTitleIsVisible()
        {
            return await WaitForElementVisible(page, FirstParticipantsTitle);
        }

        public async Task clickOnParticipantAddPhoneButton()
        {
            await page.ClickAsync(ParticipantAddPhoneBtn);
        }

        public async Task enterAddPhonePhoneField(string participantPhone)
        {
            await WaitForElementVisible(page, ParticipantAddPhonePhoneTxt);
            await page.FillAsync(ParticipantAddPhonePhoneTxt, participantPhone);
        }
        public async Task clickOnParticipantSaveButton()
        {
            await page.ClickAsync(ParticipantSave);
        }

        public async Task selectAddPhoneAndAddEmailStatusDropdown(string participantStatus)
        {
            await page.SelectOptionAsync(ParticipantAddPhoneAndAddEmailStatusDropdown, participantStatus);
        }
        public async Task<bool> verifyEmailOrPhoneIsAdded(string emailOrPhone)
        {
            return await WaitForElementVisible(page, "//span[text()='"+ emailOrPhone + "']");
        }

        public async Task clickOnParticipantAddEmailButton()
        {
            await page.ClickAsync(ParticipantAddEmailBtn);
        }

        public async Task enterParticipantAddEmail(string participantAddEmail)
        {
            await page.FillAsync(ParticipantAddEmailTxt, participantAddEmail);
        }

        public async Task clickOnParticipantFilterButton()
        {
            await page.ClickAsync(ParticipantFilterBtn);
        }

        public async Task<bool> VerifyParticipantFilterpopupTitleIsVisible()
        {
            return await WaitForElementVisible(page, ParticipantFilterpopupTitle);
        }

        public async Task selectFilterDropdown()
        {
            await page.ClickAsync(ParticipantSelectFilterDropdownBtn); ;
            
        }

        public async Task<bool> VerifyFilterdPlanIsVisible(string plan)
        {
            return await WaitForElementVisible(page, "(//a[text()='" + plan + "'])[1]");
        }

        public async Task<string> selectPlan()
        {
            await page.ClickAsync(PlanDropdown);
            await Task.Delay(3000);
            // Get the text content of the element
            var element = await page.QuerySelectorAsync(PlanDropdownOption);
            var textContent = await element.InnerTextAsync();
            await page.ClickAsync(PlanDropdownOption);
            await Task.Delay(2000);
            await page.ClickAsync(PlanDropdown);
            return textContent;
        }

        public async Task EditContactDetailPhoneNumber(string phoneNumber)
        {
            await WaitForElementVisible(page, PhoneNumberEditOption);
            await page.ClickAsync(PhoneNumberEditOption);
            await page.FillAsync(AddParticipantPhoneTxt, phoneNumber);
        }

        public async Task EditContactDetailEmail(string email)
        {
            await WaitForElementVisible(page, EmailEditOption);
            await page.ClickAsync(EmailEditOption);
            await page.FillAsync(AddParticipantEmailTxt, email);
        }

        public async Task<bool> VerifyEditedPhoneNumberOrEmailIsDispalying(string record)
        {
            return await WaitForElementVisible(page, "//span[text()='" + record + "']");
        }
    }
}
