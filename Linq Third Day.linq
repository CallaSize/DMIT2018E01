<Query Kind="Statements">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>


	//create a list of all customer sin alphabetic order by last name, firs tname ad who live in the usa with a yahoo email. 
	//List full name  (last, first), city state and email only. Create the class defintion of that list.
	string countryname = "USA";
	string domainname = "@yahoo";
	var customerlist = from x in Customers
				where x.Country.Equals(countryname)
				where x.Email.Contains(domainname)
				orderby x.LastName, x.FirstName
				select new customerEmail{
					 Name = x.LastName + ", " + x.FirstName,
					 State = x.State,	
					 City = x.City,
					 Email = x.Email
				};
	customerlist.Dump();
	