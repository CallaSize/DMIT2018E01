<Query Kind="Statements">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//using multiple steps to obtain the required data query


//create a list showing whether a particular track length
// is greater than, less than, or the average track length

//problem, I need the average track length before testing
//the individual track length against the average
//Step 1 get the average
//var resultavg = (from x in Tracks
//select x.Milliseconds).Average();
//resultavg.Dump();
//
//new {
//	thisTrackName = x.Name,
//	thisTrackLength = x.Milliseconds.Average()
////	comparison =  x.Milliseconds < y => y.Milliseconds.Average() ? "less than" : x.Milliseconds > x.Milliseconds.Average() ? "greater than" : "equal to"
////	
////}
////results.
//
////Step 1 get the averageDump()
//var resultavg = Tracks.Average(x => x.Milliseconds);
////resultavg.Dump();
//
////create wuery using average length
//var resultreport = from x in Tracks
//				select new
//				{
//					song =x.Name,
//					length = x.Milliseconds,
//					compare = x.Milliseconds < resultavg ? "less than" : x.Milliseconds > resultavg ? "greater than" : "equal to"
//					
//				};
//resultreport.Dump();



//list all the playlists which have a track showing the playlist name, number of tracks on the playlist, the cost of th playlist, the total storage size for hte playlsit in megabytes.
var results = from pl in Playlists
where pl.PlaylistTracks.Count()>0
select new
{
	PlayListName = pl.Name,
	TrackCount = pl.PlaylistTracks.Count(),
	Cost = pl.PlaylistTracks.Sum(plt => plt.Track.UnitPrice),
	Megabytes = pl.PlaylistTracks.Sum(plt => plt.Track.Bytes)/10000000
};
results.Dump();
			
			
var results10 = from x in Tracks
orderby x.Bytes
select x;
results10.Dump();


//list all albums with tracks showing the album title, the artist name, number of tracks, 
//number of tracks and the album cost
var results85 = from x in Albums
	where x.Tracks.Count() > 0 
	select new {
		aname = x.Artist.Name,
		numOfTrack = x.Tracks.Count(),
		album = x.Title,
		cost = x.Tracks.Sum(tr => tr.UnitPrice)
	};
results85.Dump();


//what is the maximum album count for all artists Get the collection. then get the max of the collection.
var albumcount = Artists.Select(x => x.Albums.Count());
var maxcount = albumcount.Max();
maxcount.Dump();