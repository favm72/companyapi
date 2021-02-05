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
	public class StoreBL : BaseBL
	{
		private MyContext context;
	
		public StoreBL(MyContext context)
		{
			this.context = context;
		}

		public async Task<MyResponse> Find(int id)
		{
			return await GetResponse(async (response) => {
				response.data = await context.Store.Find(id);
			});
		}

		public async Task<MyResponse> List(int page)
		{
			return await GetResponse(async (response) => {
				response.data = await context.Store.List(page);
			});
		}
		public async Task<MyResponse> Insert(Store store)
		{
			return await GetResponse(async (response) => {
				await context.Store.Add(store);
				response.data = store;
			});
		}

		public async Task<MyResponse> Update(Store store)
		{
			return await GetResponse(async (response) =>
			{
				await context.Store.Update(store);
			});
		}

		public async Task<MyResponse> Delete(int id)
		{
			return await GetResponse(async (response) =>
			{
				await context.Store.Remove(id);
			});
		}
	}
}
