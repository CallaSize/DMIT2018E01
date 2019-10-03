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

//nested query. What you need is some header/top/category/information
//to go along with it you need from each record, some details for the above info
//report on employees and their expenses for the years (employee name, contact number, total number fo expenses, then underneath a list of individual expenses..
//this is where ht repeater comes in

//aggregates are executed against a collection of records. (eg. class.roster.count)
//aggregate demonstration: how many tracks there are on an albums
//.Count() .Sum() .Min() .Max() .Average() <--everything but count needs an x => x.field in it!

from x in Albums
where x.Tracks.Count()>0
select new {
	albumtitle = x.Title,
	trackcount = x.Tracks.Count(),
	playtime = x.Tracks.Sum(z => z.Milliseconds),
	artistname = x.Artist.Name,
	tracks = from y in x.Tracks
		select new {
			sname = y.Name,
			genre = y.Genre.Name,
			length = y.Milliseconds		
		}
}