using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Sql;
using System.Threading.Tasks;
using System.Data;

namespace Asa.ApartmentSystem.Infra.DataGateways
{
    public class ApartmentTableGateway : IApartmentTableGateway
    {
        string _connectionString;

        public ApartmentTableGateway(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<ApartmentUnitDTO>> GetAllByBuildingId(int buildingId)
        {
            var result = new List<ApartmentUnitDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[units_get_all]";
                    cmd.Parameters.AddWithValue("@building_id", buildingId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            
                            var unitDTO = new ApartmentUnitDTO();

                            unitDTO.BuildingId = dataReader.Extract<int>("BuildingId");
                            unitDTO.Id = dataReader.Extract<int>("UnitId");
                            unitDTO.Number = dataReader.Extract<int>("Number");
                            unitDTO.Area = dataReader.Extract<decimal>("Area");
                            result.Add(unitDTO);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<ApartmentUnitDTO> GetUnitByIdAsync(int unitId)
        {
            var unitDTO = new ApartmentUnitDTO();
            
            using(var connection = new SqlConnection(_connectionString))
            {
                using(var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetUnitById]";
                    cmd.Parameters.AddWithValue("@unitId", unitId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {

                        await dataReader.ReadAsync();
                        
                        unitDTO.Id = dataReader.Extract<int>("UnitId");
                        unitDTO.BuildingId = dataReader.Extract<int>("BuildingId");
                        unitDTO.Number = dataReader.Extract<int>("Number");
                        unitDTO.Area = dataReader.Extract<decimal>("Area");
                    }
                }
            }
            return unitDTO;
        }

        public async Task<IEnumerable<UnitPersonDTO>> GetUnitsByPageAsync(int page, int size)
        {
            var result = new List<UnitPersonDTO>();

            using(var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetUnitsByPage]";
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@size", size);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    using(var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while(await dataReader.ReadAsync())
                        {
                            var unitPersonDTO = new UnitPersonDTO();

                            unitPersonDTO.UnitId = dataReader.Extract<int>("UnitId");
                            unitPersonDTO.UnitNumber = dataReader.Extract<int>("Number");
                            unitPersonDTO.Area = dataReader.Extract<decimal>("Area");
                            unitPersonDTO.OwnerName = dataReader.Extract<string>("Owner");
                            unitPersonDTO.ResidentName = dataReader.Extract<string>("Resident");

                            result.Add(unitPersonDTO);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<int> InsertUnitAsync(ApartmentUnitDTO apartmentUnit)
        {
            int id = 0;
            using(var connection = new SqlConnection(_connectionString))
            {
                using(var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpInsertUnit]";
                    cmd.Parameters.AddWithValue("buildingId", apartmentUnit.BuildingId);
                    cmd.Parameters.AddWithValue("unitNumber", apartmentUnit.Number);
                    cmd.Parameters.AddWithValue("area", apartmentUnit.Area);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    id = Convert.ToInt32(result);
                }
            }
            return id;
        }

        public async Task<int> GetCountOfUnitPersonAsync()
        {
            int count = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                using(var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetCountOfUnitPerson]";
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    var result = await cmd.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }
            }
            return count;
        }
    }
}
