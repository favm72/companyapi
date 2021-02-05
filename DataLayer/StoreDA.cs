using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class StoreDA
	{
		private string connectionString { get; set; }

		public StoreDA(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public async Task<List<Store>> List(int page)
		{
			List<Store> stores = new List<Store>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_store_list";
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@page", page);

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						if (reader.HasRows)
						{
							Store store;
							while (reader.Read())
							{
								store = new Store();
								
								store.Id = (int)reader["store_id"];
								store.Name = reader["store_name"].ToString();
								store.Phone = reader["phone"].ToString();
								store.City = reader["city"].ToString();
								store.Email = reader["email"].ToString();
								store.Street = reader["street"].ToString();
								store.State = reader["state"].ToString();
								store.ZipCode = reader["zip_code"].ToString();

								stores.Add(store);
							}
						}
					}
				}
			}
			return stores;
		}

		public async Task Remove(int id)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_store_delete";
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@id", id);

					await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task Update(Store store)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_store_update";
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@store_id", store.Id);
					command.Parameters.AddWithValue("@store_name", store.Name);
					command.Parameters.AddWithValue("@email", store.Email);
					command.Parameters.AddWithValue("@phone", store.Phone);
					command.Parameters.AddWithValue("@street", store.Street);
					command.Parameters.AddWithValue("@city", store.City);
					command.Parameters.AddWithValue("@state", store.State);
					command.Parameters.AddWithValue("@zip_code", store.ZipCode);

					await command.ExecuteNonQueryAsync();
				}
			}
		}

		public async Task<Store> Find(int id)
		{
			Store store = new Store();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_store_find";
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@id", id);

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								store.Id = (int)reader["store_id"];
								store.Name = reader["store_name"].ToString();
								store.Phone = reader["phone"].ToString();
								store.Email = reader["email"].ToString();
								store.Street = reader["street"].ToString();
								store.City = reader["city"].ToString();
								store.State = reader["state"].ToString();
								store.ZipCode = reader["zip_code"].ToString();
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
			return store;
		}

		public async Task Add(Store store)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand())
				{
					command.Connection = connection;
					command.CommandText = "sales.usp_store_insert";
					command.CommandType = System.Data.CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@store_name", store.Name);
					command.Parameters.AddWithValue("@email", store.Email);
					command.Parameters.AddWithValue("@phone", store.Phone);
					command.Parameters.AddWithValue("@street", store.Street);
					command.Parameters.AddWithValue("@city", store.City);
					command.Parameters.AddWithValue("@state", store.State);
					command.Parameters.AddWithValue("@zip_code", store.ZipCode);
					command.Parameters.Add("@store_id", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

					await command.ExecuteNonQueryAsync();
					store.Id = (int)command.Parameters["@store_id"].Value;
				}
			}
		}
	}
}
