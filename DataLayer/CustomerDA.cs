using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class CustomerDA
	{
		private string connectionString { get; set; }

		public CustomerDA(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public async Task<List<Customer>> List(int page)
		{
			List<Customer> customers = new List<Customer>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_customer_list";
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@page", page);

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						if (reader.HasRows)
						{
							Customer customer;
							while (reader.Read())
							{
								customer = new Customer();
								
								customer.Id = (int)reader["customer_id"];
								customer.FirstName = reader["first_name"].ToString();
								customer.LastName = reader["last_name"].ToString();
								customer.Phone = reader["phone"].ToString();
								customer.City = reader["city"].ToString();
								customer.Email = reader["email"].ToString();
								customer.Street = reader["street"].ToString();
								customer.State = reader["state"].ToString();
								customer.ZipCode = reader["zip_code"].ToString();

								customers.Add(customer);
							}
						}
					}
				}
			}
			return customers;
		}

		public async Task Remove(int id)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_customer_delete";
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@id", id);

					await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task Update(Customer customer)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_customer_update";
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@customer_id", customer.Id);
					command.Parameters.AddWithValue("@first_name", customer.FirstName);
					command.Parameters.AddWithValue("@last_name", customer.LastName);
					command.Parameters.AddWithValue("@email", customer.Email);
					command.Parameters.AddWithValue("@phone", customer.Phone);
					command.Parameters.AddWithValue("@street", customer.Street);
					command.Parameters.AddWithValue("@city", customer.City);
					command.Parameters.AddWithValue("@state", customer.State);
					command.Parameters.AddWithValue("@zip_code", customer.ZipCode);

					await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task<Customer> Find(int id)
		{
			Customer customer = new Customer();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_customer_find";
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@id", id);

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								customer.Id = (int)reader["customer_id"];
								customer.FirstName = reader["first_name"].ToString();
								customer.LastName = reader["last_name"].ToString();
								customer.Phone = reader["phone"].ToString();
								customer.Email = reader["email"].ToString();
								customer.Street = reader["street"].ToString();
								customer.City = reader["city"].ToString();
								customer.State = reader["state"].ToString();
								customer.ZipCode = reader["zip_code"].ToString();
								break;
							}
						}
						else
						{
							throw new MyException("Id not Found.");
						}
					}
				}
			}
			return customer;
		}

		public async Task Add(Customer customer)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_customer_insert";
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@first_name", customer.FirstName);
					command.Parameters.AddWithValue("@last_name", customer.LastName);
					command.Parameters.AddWithValue("@email", customer.Email);
					command.Parameters.AddWithValue("@phone", customer.Phone);
					command.Parameters.AddWithValue("@street", customer.Street);
					command.Parameters.AddWithValue("@city", customer.City);
					command.Parameters.AddWithValue("@state", customer.State);
					command.Parameters.AddWithValue("@zip_code", customer.ZipCode);
					command.Parameters.Add("@customer_id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

					await command.ExecuteNonQueryAsync();
					customer.Id = (int)command.Parameters["@customer_id"].Value;
				}
			}
		}
	}
}
