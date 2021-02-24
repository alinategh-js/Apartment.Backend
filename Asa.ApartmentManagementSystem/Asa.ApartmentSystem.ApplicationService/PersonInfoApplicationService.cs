using Asa.ApartmentSystem.Infra.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.BaseInfo.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService
{
    class PersonInfoApplicationService
    {
        ITableGatwayFactory _tableGatewayFactory;
        PersonManager _personManager;

        public PersonInfoApplicationService(string connectionString)
        {
            _tableGatewayFactory = new SqlTableGatewayFactory(connectionString);
            _personManager = new PersonManager(_tableGatewayFactory);
        }

        public async Task<IEnumerable<OwnerTenantInfoDto>> GetAllPeopleByPageAndType(int page, int size, int isOwner)
        {
            return await _personManager.GetAllPeopleByPageAndTypeAsync(page, size, isOwner);
        }

        public async Task<int> CreatePersonAsync(string fullName, string phoneNumber)
        {
            var person = new PersonDTO { FullName = fullName, PhoneNumber = phoneNumber };
            return await _personManager.CreatePersonAsync(person);
        }

        public async Task<PersonDTO> GetPersonByIdAsync(int personId)
        {
            return await _personManager.GetPersonByIdAsync(personId);
        }

        public async Task<int> UpdatePersonAsync(int personId, string fullName, string phoneNumber)
        {
            var person = new PersonDTO { Id = personId, FullName = fullName, PhoneNumber = phoneNumber };
            return await _personManager.UpdatePersonAsync(person);
        }
    }
}
