using Asa.ApartmentSystem.Infra.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.BaseInfo.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.ApplicationService
{
    public class BuildingInfoApplicationService
    {
        ITableGatwayFactory tableGatewayFactory;

        public BuildingInfoApplicationService(string connectionString)
        {
            tableGatewayFactory = new SqlTableGatewayFactory(connectionString);
        }

        public async Task<int> CreateBuilding(string Name, int numberofUnits)
        {
            BuildingManager buildingManager = new BuildingManager(tableGatewayFactory);
            var buildingDto = new BuildingDTO { Name = Name, NumberOfUnits = numberofUnits };
            await buildingManager.AddBuilding(buildingDto);
            return buildingDto.Id;
        }

        public async Task<BuildingDTO> GetFirstBuilding()
        {
            BuildingManager buildingManager = new BuildingManager(tableGatewayFactory);
            return await buildingManager.GetOnlyBuilding();
        }

        public async Task<ApartmentUnitDTO> GetUnit(int unitId)
        {
            BuildingManager buildingManager = new BuildingManager(tableGatewayFactory);
            return await buildingManager.GetUnitByIdAsync(unitId);
        }
        
        public async Task<IEnumerable<UnitPersonDTO>> GetUnitsByPage(int page, int size)
        {
            BuildingManager buildingManager = new BuildingManager(tableGatewayFactory);
            return await buildingManager.GetUnitsByPage(page, size);
        }

        public async Task<int> InsertUnit(int buildingId, int unitNumber, decimal area)
        {
            BuildingManager buildingManager = new BuildingManager(tableGatewayFactory);
            var unitDTO = new ApartmentUnitDTO { BuildingId = buildingId, Number = unitNumber, Area = area };
            await buildingManager.InsertUnitAsync(unitDTO);
            return unitDTO.Id;
        }

        public async Task<int> GetCountOfUnitPerson()
        {
            BuildingManager buildingManager = new BuildingManager(tableGatewayFactory);
            return await buildingManager.GetCountOfUnitPerson();
        }
    }
}
