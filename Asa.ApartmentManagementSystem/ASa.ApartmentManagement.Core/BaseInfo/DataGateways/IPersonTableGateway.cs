using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.DataGateways
{
    public interface IPersonTableGateway
    {
        Task<IEnumerable<OwnerTenantInfoDto>> GetAllPeopleByPageAndTypeAsync(int page, int size, int isOwner);
    }
}
