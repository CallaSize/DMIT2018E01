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
	string artistname = "AC/DC";
	var results = 	from x in Albums
				 where x.Artist.Name.Contains(artistname)
				 orderby x.ReleaseYear, x.Title
				 select new AlbumsOfArtist
				 {
				 	Title = x.Title, 
					ArtistName = x.Artist.Name,
					RYear = x.ReleaseYear,
					Label = x.ReleaseLabel
				 };
	//results.Dump();
	
	//create a list of all customer sin alphabetic order by last name, firs tname ad who live in the usa with a yahoo email. List full name  (last, first)
		
		//create a list of all customer sin alphabetic order by last name, firs tname ad who live in the usa with a yahoo email. 
		//List full name  (last, first), city state and email only. Create the class defintion of that list.
		string countryname = "USA";
		string domainname = "@yahoo";
		var customerlist = from x in Customers
					where x.Country.Equals(countryname)
					&& x.Email.Contains(domainname)
					orderby x.LastName, x.FirstName
					select new customerWithEmail{
						 Name = x.LastName + ", " + x.FirstName,
						 State = x.State,	
						 City = x.City,
						 Email = x.Email
					};
	//	customerlist.Dump();
	
	
//	var whosang = from x in Tracks
//		where x.Name.Equals ("Rag Doll")
//		select x;
//		whosang.Dump();
	
	//who is the artist who sang who sang rag doll? List the Artist Name, the album title,r eelase year and label along with teh song track composer
	string trackname = "rag doll";
	var artistlist = from x in Tracks
		where x.Name.Contains(trackname)
		select new artistListFromTrack {
			Composer = x.Composer,
			ArtistName = x.Album.Artist.Name,
			AlbumTitle = x.Album.Title,
			AlbumRYear = x.Album.ReleaseYear,
			AlbumRLabel = x.Album.ReleaseLabel
		};
	artistlist.Dump();
}

public class artistListFromTrack{
	public string ArtistName {get; set;}
	public string AlbumTitle {get; set;}
	public int AlbumRYear {get; set;}
	public string AlbumRLabel {get; set;}
	public string Composer {get; set;}
}


// Define other methods and classes here
public class customerWithEmail{
	public string Name {get; set;}
	public string City {get; set;}
	public string State {get; set;}
	public string Email {get; set;}
}


// Define other methods and classes here
public class AlbumsOfArtist
{
	public string Title {get;set;}
	public string ArtistName{get;set;}
	public int RYear{get;set;}
	public string Label{get;set;}
}