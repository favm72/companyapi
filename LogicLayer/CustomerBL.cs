using Common;
using DataLayer;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Data.SqlClient;

namespace LogicLayer
{
	public class CustomerBL : BaseBL
	{
		private MyContext context;
	
		public CustomerBL(MyContext context)
		{
			this.context = context;
		}

		public async Task<MyResponse> Find(int id)
		{
			return await GetResponse(async (response) => {
				response.data = await context.Customer.Find(id);
			});
		}

		public async Task<MyResponse> List(int page)
		{
			return await GetResponse(async (response) => {
				response.data = await context.Customer.List(page);
			});
		}
		public async Task<MyResponse> Insert(Customer customer)
		{
			return await GetResponse(async (response) => {
				await context.Customer.Add(customer);
				response.data = customer;
			});
		}

		public async Task<MyResponse> Update(Customer customer)
		{
			return await GetResponse(async (response) =>
			{
				await context.Customer.Update(customer);
			});
		}

		public async Task<MyResponse> Delete(int id)
		{
			return await GetResponse(async (response) =>
			{
				await context.Customer.Remove(id);
			});
		}
	}
}
