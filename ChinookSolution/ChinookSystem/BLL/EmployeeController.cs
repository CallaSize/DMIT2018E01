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
    public class EmployeeController
    {
        public List<string> Employees_GetTitles()
        {
            using (var context = new ChinookContext())
            {
                var results = (from x in context.Employees
                               select x.Title).Distinct();
                return results.ToList();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> Employee_ListNames()
        {
            using (var context = new ChinookContext())
            {
                var employeelist = from x in context.Employees
                                   orderby x.LastName, x.FirstName
                                   select new SelectionList
                                   {
                                       DisplayText = x.LastName + ", " + x.FirstName,
                                       IDValueField = x.EmployeeId
                                   };
                return employeelist.ToList();
            }
        }
    }
	
    
}
