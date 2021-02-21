using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.DataGateways
{
    public interface IBuildingTableGateway
    {
        Task<int> InsertBuildingAsync(BuildingDTO building);
        Task<BuildingDTO> GetBuildingByIdAsync(int buildingId);
        Task<BuildingDTO> GetOnlyBuildingAsync(); // we will use this to get the 'only' building in our database because our app has only one building for now.
    }
}
