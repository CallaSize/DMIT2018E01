<Query Kind="Expression">
  <Connection>
    <ID>d776e8cb-57a1-48db-80a2-29b6bc7c9ea4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//to get both the Albums with tracks and the albums without tracks you can use a .Union()
//This is assuming you are trying to do some mathematical equations, division on milliseconds, or something, then you  need .Union, becuase you can't do an arithmetic operation against null. 
//Otherwise you could just pull the report and it would put the nulls in where there was no tracks.
//In a union you need to ensure cast typing is correct, colum cast types match identically, each query has the same number of columns, same order of columns
//Create a list of all albums show the Title, Number of Tracks, Total Cost of Tracks, Average length in milliseconds of the tracks

//problem exists for albums without any tracks.Summing and averages need data to work!!! If an album has no tracks you would get an abort exception
//solution
//create 2 queries a) with tracks and b)without tracks, then union the results.


//syntax (query1).Union(query2).Union(queryn).Oderby(first sort).Thenby(sortn)
//var unionsample = (from x in Albums
//				where x.Tracks.Count() >0
//				select new {
//					title = x.Title,
//					trackcount = x.Tracks.Count(),
//					priceoftracks = x.Tracks.Sum(y => y.UnitPrice),
//					avglengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,
//					avglengthB = x.Tracks.Average(y => y.Milliseconds/1000.0)					
//				}).Union(
// 						from x in Albums
//						where x.Tracks.Count()==0
//						select new {
//							title = x.Title,
//							trackcount = 0,
//							priceoftracks = 0.00m,
//							avglengthA = 0.00,
//							avglengthB = 0.00					
//				}).OrderBy(y => y.trackcount).ThenBy(y => y.title);
//unionsample.Dump();


//Boolean filters .All() or .Any()
//.Any() method iterates through the entire collection to see if any of the items match the specified condition
//returns a true or false
// an instance fo the collection that receives a true is a selected for processing
//Genres.OrderBy( x => x.Name).Dump();

//list Genres that have track which are not on any playlist
//var genretrack = from x in Genres
//				where x.Tracks.Any(tr => tr.PlaylistTracks.Count()==0)
//				orderby x.Name
//				select new { 
//				name = x.Name
//				};
//genretrack.Dump();


//.All() method iterates throught he entire colleciton to see if all of the tiems match teh specified condition
//returns a true or false
////an instance of the collection that receives a true is selected for processing
//var populargenre = from x in Genres
//					where x.Tracks.All(tr => tr.PlaylistTracks.Count() >0 )
//					orderby x.Name
//						select new {
//							name = x.Name,
//							thetracks = (from y in x.Tracks
//											where y.PlaylistTracks.Count()>0
//											select new
//											{
//											song = y.Name,
//											count = y.PlaylistTracks.Count()
//											
//											})
//						};
//populargenre.Dump();
//				
				
//sometimes you have two lists that need to be compared ususlaly you are looking for tiems that are the same (in both collections)
//OR you are looking for item that are different
//in either case:  ypu are comparing one colleciton to a seocn collection

////obtain a distinct list of all playlist tracks for Roberto Almeida (username AlmeidaR)
//var almeida = (from x in PlaylistTracks
//				where x.Playlist.UserName.Contains("Almeida")
//				orderby x.Track.Name
//				select new
//				{
//					genre = x.Track.Genre.Name,
//					id = x.TrackId,
//					song = x.Track.Name
//				}).Distinct();
////almeida.Dump();
//
//
////obtain a distinct list of all playlist tracks for Michelle Brooks (username BrooksM)
//var brooks = (from x in PlaylistTracks
//				where x.Playlist.UserName.Contains("Brooks")
//				orderby x.Track.Name
//				select new
//				{
//					genre = x.Track.Genre.Name,
//					id = x.TrackId,
//					song = x.Track.Name
//				}).Distinct();
////brooks.Dump();
//
////List tracks that both Roberto and Michelle likes
////var likes = almeida.Where(a => brooks.Any( b => b.id == a.id))
////				.OrderBy(a => a.genre)
////				.Select(a => a);
////likes.Dump();
//			
//// list the roberto's tracks that michelle does not have				
////var almeidaif = almeida.Where(a => !brooks.Any( b => b.id == a.id))
////				.OrderBy(a => a.genre)
////				.Select(a => a);
////almeidaif.Dump();
//				
//				
////list the michelle's tracks that roberto does not have				
//var michelledif = brooks.Where(a => !almeida.Any( b => b.id == a.id))
//				.OrderBy(a => a.genre)
//				.Select(a => a);
//michelledif.Dump();



//Joins can be used where navigational properties do not exist.
//Joins can be used betwene associate ntitieis
//scenario pkey = fkey

//left side of the join should be the support data
//right side fo the join is the record collection to be processed

//List albums showing title, releaseyear, label artistname and track count
var results = from xRightSide in Albums
				join yLeftSide in Artists
				on xRightSide.ArtistId equal yLeftSide.ArtistId
				select new 
				{
					title = xRightSide.Title,
					year = xRightSide.ReleaseYear,
					label = xRightSide.ReleaseLabel == null ? "Unknown" : xRightSide.ReleaseYear,
					artistjoin = yLeftSide.Name,
					artistnav = xRightSide.Artist.Name,
					trackcount = x.RightSide.Tracks.Count()				
				};				
results.Dump();				