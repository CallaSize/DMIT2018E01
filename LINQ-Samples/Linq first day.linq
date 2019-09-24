<Query Kind="Expression">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sample of query syntax to dump the artist data
from x in Artists
select x

//sample of method syntrax to dump the artist data
Artists
   .Select (x => x)
   
//sort datainfo.Sort((x,y)=>x.AttributeName.CompareTo(y.AttributeName))

//find any artist whose name contains the string "son"
from x in Artists
where x.Name.Contains("son")
select x

Artists
	.Where(x => x.Name.Contains("son"))
	.Select (x => x)
	
//create a list of albums released in 1970
Albums
	.Where(x => x.ReleaseYear == 1970)
	.Select (x => x)
	
from x in Albums
where x.ReleaseYear == 1970
select x

//create a list of albums released in 1970 with an orderby
from x in Albums
where x.ReleaseYear == 1970
orderby x.Title
select x

Albums
	.Where(x => x.ReleaseYear == 1970)
	.OrderBy(x => x.Title)
	.Select (x => x)
	