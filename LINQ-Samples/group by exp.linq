<Query Kind="Expression">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//group record collection by a single field on the record
//the selected grouping field is referred to as the group Key
from x in Tracks
group x by x.GenreId

//group record collection using multiple fields on the record
//the multiple fields become a group key instance (which means it is going to be a class)
//referring to a property in the group key isntance is by key.property
from x in Tracks
group x by new {x.GenreId, x.MediaTypeId}


//we can place the grouping of the large data collection into a temporary data collection
//any further reporting on the groups with in the temporary data collection, will use, the temporary
//data collection name as it's data source.

//report the groups
from x in Tracks
group x by x.GenreId into gGenre
select (gGenre)



//report the details on each group
from x in Tracks
group x by x.GenreId into gGenre
select new 
{
	//in the gGenre groups I want the group id to be genreid
	groupid = gGenre.Key,
	tracks = gGenre.ToList()
}

//selected fields from each group
from x in Tracks
group x by x.GenreId into gGenre
select new 
{
	groupid = gGenre.Key,
	tracks = from x in gGenre
			select new 
			{
				trackid = x.TrackId,
				song = x.Name,
				songlength = x.Milliseconds/10000000.0
			}
}

//you can also group by class...



//selected fields from each group and using navigational properties
from x in Tracks
group x by x.GenreId into gGenre
select new 
{
	groupid = gGenre.Key,
	tracks = from x in gGenre
			select new 
			{
				trackid = x.TrackId,
				song = x.Name,
				artist = x.Album.Artist.Name,
				songlength = x.Milliseconds/10000000.0
			}
}


//refer to a specific key
from x in Tracks
group x by x.Genre into gTracks
select new {
	genre = gTracks.Key.Genre,
	name = gTracks.Key.Name,
	trackcount = gTracks.Count()
}

//change it to albums
from x in Tracks
group x by x.Album into gTracks
select new {
	name = gTracks.Key.Title,
	artist = gTracks.Key.Artist.Name,
	trackcount = gTracks.Count()
}


//create a list of albums by release year showing 
//showing the year
//number of albums in that year,
//album title and count fo tracks
from x in Albums
group x by x.ReleaseYear into gRYear
select new {
	year = gRYear.Key,
	//each one of my piles is a collection and to refer to that individual pile I'm using gRYear
	albumcount = gRYear.Count(),
	//I'm going to start reporting on each item within that group
	anAlbum = from y in gRYear
		select new
		{
			title= y.Title,
			trackcount = (from t in y.Tracks select t).Count()
		}
}

//order the previous report by the number fo albums per year descending
//the tip is once you have grouped, all further commands/clauses are against the group.
from x in Albums
group x by x.ReleaseYear into gRYear
orderby gRYear.Count() descending
select new {
	year = gRYear.Key,
	//each one of my piles is a collection and to refer to that individual pile I'm using gRYear
	albumcount = gRYear.Count(),

	//I'm going to start reporting on each item within that group
	anAlbum = from y in gRYear
		select new
		{
			title= y.Title,
			trackcount = (from t in y.Tracks select t).Count()
		}
}


//order the previous report by the number fo albums per year descending
//also order within count by year acscending
from x in Albums
where x.ReleaseYear >1989
group x by x.ReleaseYear into gRYear
orderby gRYear.Count() descending, gRYear.Key
select new {
	year = gRYear.Key,
	//each one of my piles is a collection and to refer to that individual pile I'm using gRYear
	albumcount = gRYear.Count(),

	//I'm going to start reporting on each item within that group
	anAlbum = from y in gRYear
	
		select new
		{
			title= y.Title,
			trackcount = (from t in y.Tracks select t).Count()
		}
}
