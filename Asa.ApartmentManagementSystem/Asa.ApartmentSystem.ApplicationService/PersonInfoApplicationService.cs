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

        public PersonInfoApplicationService(string connectionString)
        {
            _tableGatewayFactory = new SqlTableGatewayFactory(connectionString);
        }

        public async Task<IEnumerable<OwnerTenantInfoDto>> GetAllPeopleByPageAndType(int page, int size, int isOwner)
        {
            var personManager = new PersonManager(_tableGatewayFactory);
            return await personManager.GetAllPeopleByPageAndTypeAsync(page, size, isOwner);
        }

        public async Task<int> CreatePersonAsync(string fullName, string phoneNumber)
        {
            var personManager = new PersonManager(_tableGatewayFactory);
            var person = new PersonDTO { FullName = fullName, PhoneNumber = phoneNumber };
            return await personManager.CreatePersonAsync(person);
        }
    }
}
