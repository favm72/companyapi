using Common;
using DataLayer;
using LogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netapi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly ILogger<CustomerController> _logger;
		private CustomerBL customerBL;

		public CustomerController(ILogger<CustomerController> logger, MyContext context)
		{
			_logger = logger;
			customerBL = new CustomerBL(context);
		}

		[HttpGet("{id}")]
		public async Task<MyResponse> Find(int id)
		{
			return await customerBL.Find(id);
		}

		[HttpGet()]
		public async Task<MyResponse> Get([FromQuery]int page)
		{
			return await customerBL.List(page);
		}

		[HttpPost()]
		public async Task<MyResponse> Post(Customer customer)
		{
			return await customerBL.Insert(customer);
		}

		[HttpPut()]
		public async Task<MyResponse> Update(Customer customer)
		{
			return await customerBL.Update(customer);
		}

		[HttpDelete("{id}")]
		public async Task<MyResponse> Delete(int id)
		{
			return await customerBL.Delete(id);
		}
	}
}
