using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
	public class MyContext : IMyContext
	{
		public CustomerDA Customer { get; }
		public StoreDA Store { get; }
		public MyContext(string connectionString)
		{
			this.Customer = new CustomerDA(connectionString);
			this.Store = new StoreDA(connectionString);
		}
	}
}
