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
	var results = from x in Albums
	where x.Tracks.Count()>25
	select new AlbumDTO {
		AlbumTitle = x.Title,
		TrackCount = x.Tracks.Count(),
		PlayTime = x.Tracks.Sum(z => z.Milliseconds),
		AlbumArtist = x.Artist.Name,
		AlbumTracks = (from y in x.Tracks
			select new TrackPOCO {
				SongName = y.Name,
				SongGenre = y.Genre.Name,
				SongLength = y.Milliseconds		
			}).ToList()
	}
	results.Dump()
}

// Define other methods and classes here
//creating these descriptions, if you start at the inner most, they basically build themselves.
public class TrackPOCO{
	public string SongName {get; set;}
	public string SongGenre {get; set;}
	public int SongLength {get; set;}
}

public class AlbumDTO {
	public string AlbumTitle {get; set;}
	public string AlbumArtist {get; set;}
	public int TrackCount {get; set;}
	public int PlayTime {get; set;}
	public List<TrackPOCO> AlbumTracks {get; set;}
	
}