using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.Managers
{
    public class PersonManager
    {
        ITableGatwayFactory _tableGatewayFactory;
        public PersonManager(ITableGatwayFactory tableGatwayFactory)
        {
            _tableGatewayFactory = tableGatwayFactory;
        }

        public async Task<IEnumerable<OwnerTenantInfoDto>> GetAllPeopleByPageAndTypeAsync(int page, int size, int isOwner)
        {
            var tableGateway = _tableGatewayFactory.CreateIPersonTableGateway();
            return await tableGateway.GetAllPeopleByPageAndTypeAsync(page, size, isOwner).ConfigureAwait(false);
        }

        public async Task<int> CreatePerson(PersonDTO person)
        {
            const int MAX_PERSON_NAME_LENGTH = 50;
            const int MIN_PERSON_NAME_LENGTH = 5;

            const int VALID_PHONE_NUMBER = 11;
            const string PHONE_NUMBER_PATTERN = @"^(0)?9\d{9}$";
            Regex phoneNumberRegex = new Regex(PHONE_NUMBER_PATTERN);

            var personNameIsNotValid = string.IsNullOrWhiteSpace(person.FullName) || person.FullName.Length > MAX_PERSON_NAME_LENGTH || person.FullName.Length < MIN_PERSON_NAME_LENGTH;
            var phoneNumberIsNotValid = phoneNumberRegex.IsMatch(person.PhoneNumber);

            if (personNameIsNotValid)
            {
                throw new ValidationException(ErrorCodes.Invalid_Person_Name, $"Person name should be between {MIN_PERSON_NAME_LENGTH} and {MAX_PERSON_NAME_LENGTH}.");
            }
            
            if (phoneNumberIsNotValid)
            {
                throw new ValidationException(ErrorCodes.Invalid_Person_Phone_Number, $"The person phone number must be in {VALID_PHONE_NUMBER} number characters.");
            }

            var tableGateway = _tableGatewayFactory.CreateIPersonTableGateway();
            return await tableGateway.InsertPerson(person).ConfigureAwait(false);
        }
    }
}
