using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{
    public class CustomerController
    {
        public Customer Customer_Get(int customerid)
        {
            using (var context = new ChinookContext())
            {
                return context.Customers.Find(customerid);
            }
        }


    }
}
