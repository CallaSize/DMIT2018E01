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
	
	
//create a list of albums release between 2007 amd 2018 
//order by release year then by title
from x in Albums
where x.ReleaseYear >= 2007
&& x.ReleaseYear <=2018
orderby x.ReleaseYear, x.Title
select x

Albums
   .Where (x => ((x.ReleaseYear >= 2007) && (x.ReleaseYear <= 2018)))
   .OrderBy (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   
   
   
from x in Albums
where x.ReleaseYear >= 2007
&& x.ReleaseYear <=2018
orderby x.ReleaseYear descending, x.Title
select x

//note the difference in method names usingn the method syntax
// a descending orderby is .OrderByDescending
// secondary and beyond ordering is .ThenBy
Albums
   .Where (x => ((x.ReleaseYear >= 2007) && (x.ReleaseYear <= 2018)))
   .OrderByDescending (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   
   
 //can navigational properties be used in queries. 
 //create a list of albums by Deep Purple
 //order by release year and title.
 //query syntax version:
 from x in Albums
 where x.Artist.Name.Contains("Deep Purple")
 orderby x.ReleaseYear, x.Title
 select x
 
 //use the navigational properties to obtain the artist data
 //new{...} creates a new dataset like when we create an instance of an object. (class definition) dynamically creating a new class
 //lets try to find the artist name and limit the resulting columns.
  from x in Albums
 where x.Artist.Name.Contains("Deep Purple")
 orderby x.ReleaseYear, x.Title
 select new
 {
 	Title = x.Title, 
	ArtistName = x.Artist.Name,
	RYear = x.ReleaseYear,
	Label = x.ReleaseLabel
	
 }