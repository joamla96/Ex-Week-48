using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Connection Connection = new Connection("127.0.0.1", 5000);

			while(true)
			{
				Console.WriteLine(Connection.ReadLine());
			}
		}
	}
}
