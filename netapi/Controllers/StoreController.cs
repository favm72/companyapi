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
	public class StoreController : ControllerBase
	{
		private readonly ILogger<StoreController> _logger;
		private StoreBL storeBL;

		public StoreController(ILogger<StoreController> logger, MyContext context)
		{
			_logger = logger;
			storeBL = new StoreBL(context);
		}

		[HttpGet("{id}")]
		public async Task<MyResponse> Find(int id)
		{
			return await storeBL.Find(id);
		}

		[HttpGet()]
		public async Task<MyResponse> Get([FromQuery]int page)
		{
			return await storeBL.List(page);
		}

		[HttpPost()]
		public async Task<MyResponse> Post(Store store)
		{
			return await storeBL.Insert(store);
		}

		[HttpPut()]
		public async Task<MyResponse> Update(Store store)
		{
			return await storeBL.Update(store);
		}

		[HttpDelete("{id}")]
		public async Task<MyResponse> Delete(int id)
		{
			return await storeBL.Delete(id);
		}
	}
}
