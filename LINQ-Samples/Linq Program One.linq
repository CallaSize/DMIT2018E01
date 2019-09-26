<Query Kind="Program">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	var results = 	from x in Albums
				 where x.Artist.Name.Contains("Deep Purple")
				 orderby x.ReleaseYear, x.Title
				 select new AlbumsOfArtist
				 {
				 	Title = x.Title, 
					ArtistName = x.Artist.Name,
					RYear = x.ReleaseYear,
					Label = x.ReleaseLabel
				 };
	results.Dump();
}

// Define other methods and classes here
public class AlbumsOfArtist
{
	public string Title {get;set;}
	public string ArtistName{get;set;}
	public int RYear{get;set;}
	public string Label{get;set;}
}