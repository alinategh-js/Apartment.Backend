using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASa.ApartmentManagement.Core.BaseInfo.Managers
{
    public class BuildingManager
    {
        ITableGatwayFactory _tablegatwayFactory;
        
        public BuildingManager(ITableGatwayFactory tablegatwayFactory)
        {
            _tablegatwayFactory = tablegatwayFactory;
        }
        
        public async Task AddBuilding(BuildingDTO building)
        {
            const int MAX_BUILDING_NAME_LENGTH = 50;
            var buildingNameIsValid = string.IsNullOrWhiteSpace(building.Name) || building.Name.Length > MAX_BUILDING_NAME_LENGTH;
            if (buildingNameIsValid)
            {
                throw new ValidationException(ErrorCodes.Invalid_Building_Name, $"Building name cannot be neither empty nor larger than {MAX_BUILDING_NAME_LENGTH} character");
            }
            const int MINIMUM_BUILDING_UNITS_COUNT = 1;
            if (building.NumberOfUnits < MINIMUM_BUILDING_UNITS_COUNT)
            {
                throw new ValidationException(ErrorCodes.Invalid_Number_Of_Units, $"The number of units cannot be less than {MINIMUM_BUILDING_UNITS_COUNT }.");
            }

            IBuildingTableGateway tableGateway = _tablegatwayFactory.CreateBuildingTableGateway();
            var id = await tableGateway.InsertBuildingAsync(building).ConfigureAwait(false);
            building.Id = id;
        }

        public async Task<BuildingDTO> GetOnlyBuilding()
        {
            
            IBuildingTableGateway tableGateway = _tablegatwayFactory.CreateBuildingTableGateway();
            BuildingDTO building = await tableGateway.GetOnlyBuildingAsync().ConfigureAwait(false);
            return building;
        }

        public async Task<ApartmentUnitDTO> GetUnitByIdAsync(int unitId)
        {
            IApartmentTableGateway tableGateway = _tablegatwayFactory.CreateIApartmentTableGateway();
            ApartmentUnitDTO unit = await tableGateway.GetUnitByIdAsync(unitId).ConfigureAwait(false);
            return unit;
        }

        public async Task<IEnumerable<UnitPersonDTO>> GetUnitsByPage(int page, int size)
        {
            IApartmentTableGateway tableGateway = _tablegatwayFactory.CreateIApartmentTableGateway();
            IEnumerable<UnitPersonDTO> unitPersonDTOList = await tableGateway.GetUnitsByPageAsync(page, size).ConfigureAwait(false);
            return unitPersonDTOList;
        }

        public async Task InsertUnitAsync(ApartmentUnitDTO apartmentUnitDTO)
        {
            //validations:
            // 1: check if buildingId exists in the database or not
            var buildingDTO = GetOnlyBuilding();
            var buildingId = buildingDTO.Id;
            if(buildingId != apartmentUnitDTO.Id)
            {
                throw new ValidationException(ErrorCodes.Building_Not_Found, "Building Id doesn't exist in the database.");
            }

            IApartmentTableGateway tableGateway = _tablegatwayFactory.CreateIApartmentTableGateway();
            int unitId = await tableGateway.InsertUnitAsync(apartmentUnitDTO).ConfigureAwait(false);
            apartmentUnitDTO.Id = unitId;
        }

    }
}
