using System;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

public partial class _Default : System.Web.UI.Page
{
	protected void Button1_Click(object sender, EventArgs e)
	{
		lbOutput.Text = "";
		int inSRID = 0;
		int outSRID = 0;
		if (!int.TryParse(tbSRIDin.Text, out inSRID))
		{
			WriteError("Invalid SRID input number.");
			return;
		}
		if (!int.TryParse(tbSRIDout.Text, out outSRID))
		{
			WriteError("Invalid SRID output number.");
			return;
		}
		//Get input coordinate system by ID from CSV dataset
		ICoordinateSystem csIn = SridReader.GetCSbyID(inSRID);
		if (csIn == null)
		{
			WriteError("Unknown input SRID.");
			return;
		}
		//Get output coordinate system by ID from CSV dataset
		ICoordinateSystem csOut = SridReader.GetCSbyID(outSRID);
		if (csOut == null)
		{
			WriteError("Unknown output SRID.");
			return;
		}
		//Create coordinate transformation instance
		ICoordinateTransformation trans = null;
		try
		{
			trans = new CoordinateTransformationFactory().CreateFromCoordinateSystems(csIn, csOut);
		}
		catch (System.Exception ex)
		{
			WriteError(ex.Message);
			return;
		}
		lbOutput.Text += "<b>Input CS:</b>";
		WriteCoordSys(csIn);
		lbOutput.Text += "<b>Output CS:</b>";
		WriteCoordSys(csOut);
		string points = tbPoints.Text;
		lbOutput.Text += "<b>Transformed points:</b><br/>";
		foreach (string line in points.Split('\n'))
		{
			//Parse each line and transform the points.
			string[] vals = line.Split(',');
			if (vals.Length < 2) continue;
			double x = 0;
			double y = 0;
			if (!double.TryParse(vals[0], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out x))
			{
				WriteError("Error parsing X", true);
				continue;
			}
			if (!double.TryParse(vals[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out y))
			{
				WriteError("Error parsing Y", true);
				continue;
			}
			try
			{
				///Perform transformation
				double[] result = trans.MathTransform.Transform(new double[] { x, y });
				lbOutput.Text += String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} , {1}<br/>", result[0], result[1]);
			}
			catch(System.Exception ex)
			{
				WriteError(ex.Message, true);
			}
		}
	}
	private void WriteCoordSys(ICoordinateSystem cs)
	{
		lbOutput.Text += "<div style=\"font-family: courier;\">" + cs.WKT + "</div>";
	}
	private void WriteError(string message)
	{
		WriteError(message, false);
	}
	private void WriteError(string message, bool append)
	{
		string html = "<span style=\"color: red;\"><b>" + message + "</b></span>";
		if (append)
			lbOutput.Text += html + "<br/>";
		else
			lbOutput.Text = html;
	}
}
