using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.DataGateways
{
    public class BuildingTableGateway : IBuildingTableGateway
    {
        string _connectionString;
        public BuildingTableGateway(string connectionString)
        {            
            _connectionString = connectionString;
        }

        public async Task<int> InsertBuildingAsync(BuildingDTO building)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[buildings_create]";
                    cmd.Parameters.AddWithValue("@Name", building.Name);
                    cmd.Parameters.AddWithValue("@NumberOfUnits", building.NumberOfUnits);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    id = Convert.ToInt32(result);
                }
            }
            return id;
        }

        public async Task<BuildingDTO> GetOnlyBuildingAsync() 
        {
            var buildingDTO = new BuildingDTO();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetOnlyBuilding]";
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        await dataReader.ReadAsync();

                        buildingDTO.Id = dataReader.Extract<int>("BuildingId");
                        buildingDTO.Name = dataReader.Extract<string>("Name");
                        buildingDTO.NumberOfUnits = dataReader.Extract<int>("NumberOfUnits");
                    }
                }
            }
            return buildingDTO;
        }

        public Task<BuildingDTO> GetBuildingByIdAsync(int buildingId)
        {
            throw new NotImplementedException();
        }
    }
}