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
    public class PersonTableGateway : IPersonTableGateway
    {
        string _connectionString;

        public PersonTableGateway(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<OwnerTenantInfoDto>> GetAllPeopleByPageAndTypeAsync(int page, int size, int isOwner)
        {
            var result = new List<OwnerTenantInfoDto>();
            DataTable dataTable = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[spGetPersonOwnerTenantByPageAndType]";
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@Size", size);
                    cmd.Parameters.AddWithValue("@Owner", isOwner);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);
                }
            }

            foreach (DataRow item in dataTable.Rows)
            {
                var people = new OwnerTenantInfoDto
                {
                    FullName = Convert.ToString(item["full_name"]),                    
                    PersonId = Convert.ToInt32(item["id"]),
                    PhoneNumber = Convert.ToString(item["phone_number"]),
                    UnitId = Convert.ToInt32(item["unit_id"]),
                    IsOwner = Convert.ToBoolean(item["is_owner"])
                };
                result.Add(people);
            }
            return result;
        }

        public async Task<int> InsertPerson(PersonDTO person)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[person_create]";
                    cmd.Parameters.AddWithValue("@Name", person.FullName);
                    cmd.Parameters.AddWithValue("@NumberOfUnits", person.PhoneNumber);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    id = Convert.ToInt32(result);
                }
            }
            return id;
        }
    }
}
