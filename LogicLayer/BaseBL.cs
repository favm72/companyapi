using Common;
using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	public class BaseBL
	{
		public async Task<MyResponse> GetResponse(Func<MyResponse, Task> myaction)
		{
			MyResponse response = new MyResponse();
			try
			{
				await myaction(response);
				response.status = true;
			}			
			catch (MyException ex)
			{
				response.status = false;
				response.message = ex.Message;
				response.data = null;
			}
			catch (Exception ex)
			{
				response.status = false;
				response.message = ex.Message;
				//response.message = "Ocurrió un error";
				response.data = null;
			}
			return response;
		}
	}
}
