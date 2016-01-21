using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mailinblue;

namespace test
{
class Program
	{
		public static void Main(string[] args)
		{
			API test = new mailinblue.API("<access key>");
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("page", 1);
            data.Add("page_limit", 3);
            Object getProcesses = test.get_processes(data);
            Console.WriteLine(getProcesses);
			Console.ReadKey(true);
		}
	}
}