using System;

namespace Server
{
	public class NewProductEventArgs : EventArgs
	{
		public string Product { get; set; }

		public NewProductEventArgs(string productName)
		{
			this.Product = productName;
		}
	}
}