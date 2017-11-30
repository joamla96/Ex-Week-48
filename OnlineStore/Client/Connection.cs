using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
	public class Connection
	{
		string serverIP;
		int port;

		TcpClient serverSocket;
		NetworkStream netStream;
		StreamWriter writer;
		StreamReader reader;

		public Connection(string serverIP, int port)
		{
			this.serverIP = serverIP;
			this.port = port;

			Open();
		}
		public void Open()
		{
			if (serverSocket != null) return;

			serverSocket = new TcpClient(serverIP, port);
			netStream = serverSocket.GetStream();
			writer = new StreamWriter(netStream);
			reader = new StreamReader(netStream);
		}

		public void Close()
		{
			writer.Close();
			reader.Close();
			netStream.Close();
			serverSocket.Close();
			serverSocket = null;
		}

		public string ReadLine()
		{
			return reader.ReadLine();
		}
	}
}
