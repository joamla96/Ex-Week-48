using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		private static StoreService StoreService;
		static void Main(string[] args)
		{
			Program program = new Program();
			TcpListener listener = new TcpListener(IPAddress.Any, 5000);
			listener.Start();

			StoreService = new StoreService();

			Thread GetNewProductsThread = new Thread(program.CheckForNewItem);
			GetNewProductsThread.Start();

			while(true)
			{
				Console.WriteLine("Server is ready to accept Client");
				Socket clientSocket = listener.AcceptSocket();
				Console.WriteLine("A Client has connected");
				ClientHandler clientHandler = new ClientHandler(clientSocket, StoreService);

				Thread clientThread = new Thread(clientHandler.RunClient);
				clientThread.Start();

			}
		}

		public void CheckForNewItem()
		{
			while (true)
			{
				ServiceReference.ServiceClient wcfservice = new ServiceReference.ServiceClient();
				string newProduct = wcfservice.GetNewProducts();
				Console.WriteLine(newProduct + " has arrived in stores!");
				StoreService.NewProduct(newProduct);
				Thread.Sleep(5000);
			}
		}
	}
}
