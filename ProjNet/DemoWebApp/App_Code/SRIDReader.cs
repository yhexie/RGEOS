using System.Collections.Generic;
using ProjNet.CoordinateSystems;

public class SridReader
{
	private static string filename = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/SRID.csv");

	public struct WKTstring {
		/// <summary>
		/// Well-known ID
		/// </summary>
		public int WKID;
		/// <summary>
		/// Well-known Text
		/// </summary>
		public string WKT;
	}

	/// <summary>
	/// Enumerates all SRID's in the SRID.csv file.
	/// </summary>
	/// <returns>Enumerator</returns>
	public static IEnumerable<WKTstring> GetSRIDs()
	{
		using (System.IO.StreamReader sr = System.IO.File.OpenText(filename))
		{
			while (!sr.EndOfStream)
			{
				string line = sr.ReadLine();
				int split = line.IndexOf(';');
				if (split > -1)
				{
					WKTstring wkt = new WKTstring();
					wkt.WKID = int.Parse(line.Substring(0, split));
					wkt.WKT = line.Substring(split + 1);
					yield return wkt;
				}
			}
			sr.Close();
		}
	}
	/// <summary>
	/// Gets a coordinate system from the SRID.csv file
	/// </summary>
	/// <param name="id">EPSG ID</param>
	/// <returns>Coordinate system, or null if SRID was not found.</returns>
	public static ICoordinateSystem GetCSbyID(int id)
	{
		//TODO: Enhance this with an index so we don't have to loop all the lines
		ICoordinateSystemFactory fac = new CoordinateSystemFactory();
		foreach (SridReader.WKTstring wkt in SridReader.GetSRIDs())
		{
			if (wkt.WKID == id)
			{
				return ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(wkt.WKT) as ICoordinateSystem;
			}
		}
		return null;
	}
}
