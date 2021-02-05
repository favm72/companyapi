using System;

namespace Common
{
	public class MyResponse
	{
		public bool status { get; set; }
		public string message { get; set; }
		public object data { get; set; }
	}
}
