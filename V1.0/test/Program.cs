using System;
using mailinblue;

namespace test
{
class Program
	{
		public static void Main(string[] args)
		{
			API test = new mailinblue.API("<access key>","<secret key>");
            Console.Write(test.get_processes());
			Console.ReadKey(true);
		}
	}
}