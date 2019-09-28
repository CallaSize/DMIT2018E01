<Query Kind="Expression">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//create a list of all albums containing the album title and artist along with all the tracks (song name and genre and length)of that album

from x in Albums
select new {
	albumtitle = x.Title,
	artistname = x.Artist.Name,
	tracks = from y in x.Tracks
		select new {
			sname = y.Name,
			genre = y.Genre.Name,
			length = y.Milliseconds		
		}
}