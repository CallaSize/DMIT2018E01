<Query Kind="Statements">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//when using the language C# statement(s) your work will need to confirm to C# statement syntax.
//that means we need ei datatype variable = expression;
var results = 	from x in Albums
				 where x.Artist.Name.Contains("Deep Purple")
				 orderby x.ReleaseYear, x.Title
				 select new
				 {
				 	Title = x.Title, 
					ArtistName = x.Artist.Name,
					RYear = x.ReleaseYear,
					Label = x.ReleaseLabel
				 };
//to display the contents of that variablein Linqpad use the method .Dump()
//it is NOT a C# method
results.Dump();