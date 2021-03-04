using ASa.ApartmentManagement.Core.BaseInfo.DataGateways;
using ASa.ApartmentManagement.Core.BaseInfo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Asa.ApartmentSystem.Infra.DataGateways
{
    public class ExpenseTableGateway : IExpenseTableGateway
    {
        string _connectionString;

        public ExpenseTableGateway(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task DeleteExpenseByIdAsync(int expenseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using(var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpDeleteExpenseById]";
                    cmd.Parameters.AddWithValue("@expenseId", expenseId);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    await cmd.ExecuteScalarAsync();
                }
            }
        }

        

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesByPageAsync(int page, int size)
        {
            var result = new List<ExpenseDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetExpensesByPage]";
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@size", size);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (await dataReader.ReadAsync())
                        {
                            var expenseDTO = new ExpenseDTO();

                            expenseDTO.Id = dataReader.Extract<int>("ExpenseId");
                            expenseDTO.Title = dataReader.Extract<string>("Title");
                            expenseDTO.CategoryId = dataReader.Extract<int>("ExpenseTypeId");
                            expenseDTO.From = dataReader.Extract<DateTime>("From");
                            expenseDTO.To = dataReader.Extract<DateTime>("To");
                            expenseDTO.Cost = dataReader.Extract<decimal>("Cost");

                            result.Add(expenseDTO);
                        }
                    }
                }
            }
            return result;
        }

        public async Task<int> InsertExpenseAsync(ExpenseDTO expenseDto)
        {
            int id = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpInsertExpense]";
                    cmd.Parameters.AddWithValue("Title", expenseDto.Title);
                    cmd.Parameters.AddWithValue("ExpenseTypeId", expenseDto.CategoryId);
                    cmd.Parameters.AddWithValue("From", expenseDto.From);
                    cmd.Parameters.AddWithValue("To", expenseDto.To);
                    cmd.Parameters.AddWithValue("Cost", expenseDto.Cost);
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    var result = await cmd.ExecuteScalarAsync();
                    id = Convert.ToInt32(result);
                }
            }
            return id;
        }

        public async Task<int> GetCountOfExpensesAsync()
        {
            int count = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[SpGetCountOfExpenses]";
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
