﻿using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using ASa.ApartmentManagement.Core.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Asa.ApartmentSystem.Infra.DataGateways
{
    public class ExpenseTypeTableGateway : IExpenseTypeTableGateway
    {
        string _connectionString;

        public ExpenseTypeTableGateway(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<ExpenseTypeDTO>> GetAllExpenseTypesByPageAsync(int page, int size)
        {
            var result = new List<ExpenseTypeDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetAllExpenseTypesByPage]";
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@size", size);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var expenseType = new ExpenseTypeDTO();

                            expenseType.ExpenseTypeId = dataReader.Extract<int>("ExpenseTypeId");
                            expenseType.Name = dataReader.Extract<string>("Name");
                            expenseType.FormulaName = dataReader.Extract<string>("FormulaName");
                            expenseType.ForOwner = dataReader.Extract<bool>("ForOwner");
                            result.Add(expenseType);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<int> GetTotalCountOfExpenseTypes()
        {
            int count = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetTotalCountOfExpenseTypes]";
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
                }
            }

            return count;
        }

        public async Task<int> InsertExpenseTypeAsync(ExpenseTypeDTO expenseType)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpInsertExpenseType]";
                    cmd.Parameters.AddWithValue("@name", expenseType.Name);
                    cmd.Parameters.AddWithValue("@formulaName", expenseType.FormulaName);
                    cmd.Parameters.AddWithValue("@forOwner", expenseType.ForOwner);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    id = Convert.ToInt32(result);
                }
            }
            return id;
        }

        public async Task DeleteExpenseTypeByIdAsync(int typeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpDeleteExpenseTypeById]";
                    cmd.Parameters.AddWithValue("@expenseTypeId", typeId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    await cmd.ExecuteScalarAsync();
                }
            }
        }

        public async Task<ExpenseTypeDTO> GetExpenseTypeByIdAsync(int expenseTypeId)
        {
            var expenseTypeDTO = new ExpenseTypeDTO();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetExpenseTypeById]";
                    cmd.Parameters.AddWithValue("@expenseTypeById", expenseTypeId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        await dataReader.ReadAsync();

                        expenseTypeDTO.ExpenseTypeId = dataReader.Extract<int>("ExpenseTypeId");
                        expenseTypeDTO.Name = dataReader.Extract<string>("Name");
                        expenseTypeDTO.FormulaName = dataReader.Extract<string>("FormulaName");
                        expenseTypeDTO.ForOwner = dataReader.Extract<bool>("ForOwner");
                    }
                }
            }
            return expenseTypeDTO;
        }
    }
}
