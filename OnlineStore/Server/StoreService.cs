using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class StoreService
	{
		public event EventHandler<NewProductEventArgs> NewProductArrived;

		public void NewProduct(string productName)
		{
			if (NewProductArrived != null)
			{
				NewProductArrived(this, new NewProductEventArgs(productName));
			}
		}
	}
}
