﻿using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
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

        public async Task<IEnumerable<OwnerResidentDTO>> GetAllPeopleByPageAndTypeAsync(int page, int size, int isOwner)
        {
            var result = new List<OwnerResidentDTO>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetOwnerResidentByPageAndType]";
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@size", size);
                    cmd.Parameters.AddWithValue("@owner", isOwner);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var ownerResident = new OwnerResidentDTO();

                            ownerResident.PersonId = dataReader.Extract<int>("PersonId");
                            ownerResident.FullName = dataReader.Extract<string>("FullName");
                            ownerResident.PhoneNumber = dataReader.Extract<string>("PhoneNumber");
                            ownerResident.UnitId = dataReader.Extract<int>("UnitId");
                            ownerResident.UnitNumber = dataReader.Extract<int>("UnitNumber");
                            ownerResident.IsOwner = dataReader.Extract<bool>("IsOwner");
                            result.Add(ownerResident);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<int> GetTotalCountOfPeopleAsync()
        {
            int count = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetTotalCountOfPersonUnit]";
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }
            }

            return count;
        }

        public async Task<int> InsertPersonAsync(PersonDTO person)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpPersonCreate]";
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

        public async Task<PersonDTO> GetPersonByIdAsync(int personId)
        {
            var person = new PersonDTO();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetPersonById]";
                    cmd.Parameters.AddWithValue("@personId", personId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        await dataReader.ReadAsync();
                        person.Id = dataReader.Extract<int>("PersonId");
                        person.FullName = dataReader.Extract<string>("FullName");
                        person.PhoneNumber = dataReader.Extract<string>("PhoneNumber");
                    }
                }
            }
            return person;
        }

        public async Task<int> UpdatePersonAsync(PersonDTO person)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpUpdatePerson]";
                    cmd.Parameters.AddWithValue("@PersonId", person.Id);
                    cmd.Parameters.AddWithValue("@FullName", person.FullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    id = Convert.ToInt32(result);
                }
            }
            return id;
        }

        public async Task DeletePersonByIdAsync(int personId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpDeletePersonById]";
                    cmd.Parameters.AddWithValue("@PersonId", personId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                }
            }
        }
    }
}
