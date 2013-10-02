/*
 * Created by SharpDevelop.
 * User: Samsung
 * Date: 02-10-2013
 * Time: 21:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using mailinblue;

namespace test
{
class Program
	{
		public static void Main(string[] args)
		{
			API test = new mailinblue.API("<Access ID>","<Secret ID>");
			Console.ReadKey(true);
		}
	}
}