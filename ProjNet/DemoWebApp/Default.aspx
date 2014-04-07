<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Proj.NET Demo Application</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
		<tr>
			<td>
				<b>Input SRID</b> EPSG:<asp:TextBox ID="tbSRIDin" runat="server" Text="4326" Width="75px"></asp:TextBox>
			</td>
			<td>
				<b>Output SRID</b> EPSG:<asp:TextBox ID="tbSRIDout" runat="server" Text="900913" Width="75px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<b>Input points</b><br />
<asp:TextBox ID="tbPoints" runat="server" TextMode="MultiLine" Columns="50" Rows="5">
0,0
-90,45
12,56
180,85
</asp:TextBox>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="right">
				<asp:Button ID="Button1" runat="server" Text="Transform" OnClick="Button1_Click" />
			</td>
		</tr>
	</table>
	<div style="border:solid 1px #000; padding:5px; background-color:#ffffee;">
		<asp:Label id="lbOutput" runat="server" />
	</div>
	</form>
</body>
</html>
