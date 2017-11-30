using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
	public class ClientHandler
	{
		private Socket clientSocket;
		private NetworkStream netStream;
		private StreamReader reader;
		private StreamWriter writer;

		private ConcurrentQueue<string> MessagesToSend;

		public ClientHandler(Socket clientSocket, StoreService service)
		{
			this.clientSocket = clientSocket;
			service.NewProductArrived += this.RecieveNewProductNotification;
			MessagesToSend = new ConcurrentQueue<string>();
		}

		internal void RunClient()
		{
			netStream = new NetworkStream(clientSocket);
			reader = new StreamReader(netStream);
			writer = new StreamWriter(netStream);

			while(true)
			{
				if (MessagesToSend.Count > 0)
				{
					string msg;
					MessagesToSend.TryDequeue(out msg);
					writer.WriteLine(msg);
					writer.Flush();
				}
				else Thread.Sleep(25);
			}
		}

		public void RecieveNewProductNotification(object sender, NewProductEventArgs e)
		{
			MessagesToSend.Enqueue($"The product {e.Product} has arrived in stores!");
		}
	}
}
