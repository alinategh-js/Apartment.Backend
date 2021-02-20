using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System.Collections.Generic;
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
    }
}
