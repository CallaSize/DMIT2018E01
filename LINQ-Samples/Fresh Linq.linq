<Query Kind="Expression">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//create a kust if albums released in 2001
//list the album title, artist name and label and if the releaselabel is null then unknown

//var chosenyear = 2001;
//var albumlist = from x in Albums
//	where x.ReleaseYear == chosenyear
//	select new {
//		AlbumTitle = x.Title,
//		ArtistName =x.Artist.Name,
//		AlbumLabel = x.ReleaseLabel==null ? "unknown" : x.ReleaseLabel
//	};
//albumlist.Dump();


//list of all albums in 1970's and above, list the title and decard
from x in Albums
where x.ReleaseYear >=1970
select new {
	Title = x.Title,
	RYear = x.ReleaseYear<=1979 ? "70's" : (x.ReleaseYear<=1989 ? "80's" : (x.ReleaseYear<=1999 ? "90's" : "modern"))
}
