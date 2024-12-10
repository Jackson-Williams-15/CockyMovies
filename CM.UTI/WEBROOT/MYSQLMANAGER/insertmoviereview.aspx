<html>
<head><title>CockyMoviesManager</title>
<style type="text/css">
.auto-style1 {
	text-align: center;
}
</style>
</head>
<body>
<div align="center">
<form id="form1" runat="server">
	<div class="auto-style1">
		<br>
		<asp:Image id="Image1" runat="server" ImageUrl="./cocky547.png" />
		<br><br><br><br>STORE 33 REVIEW MANAGER - COLUMBIA, SC<br>2300 FORREST 
		DRIVE<br>INSERT A NEW REVIEW<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=VELOCITY;User ID=sa;Password=*Columbia1" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [velo_users_moviereviews]">
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" horizontalalign="Center" AllowPaging="True">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="uid" HeaderText="uid" SortExpression="uid">
			</asp:BoundField>
			<asp:BoundField DataField="movieid" HeaderText="movieid" SortExpression="movieid">
			</asp:BoundField>
			<asp:BoundField DataField="moviename" HeaderText="moviename" SortExpression="moviename">
			</asp:BoundField>
			<asp:BoundField DataField="moviestars" HeaderText="moviestars" SortExpression="moviestars">
			</asp:BoundField>
			<asp:BoundField DataField="fullreview" HeaderText="fullreview" SortExpression="fullreview">
			</asp:BoundField>
			<asp:BoundField DataField="movietype" HeaderText="movietype" SortExpression="movietype">
			</asp:BoundField>
			<asp:BoundField DataField="moviegeneration" HeaderText="moviegeneration" SortExpression="moviegeneration">
			</asp:BoundField>
			<asp:BoundField DataField="authphone" HeaderText="authphone" SortExpression="authphone">
			</asp:BoundField>
			<asp:BoundField DataField="authemail" HeaderText="authemail" SortExpression="authemail">
			</asp:BoundField>
			<asp:BoundField DataField="authfax" HeaderText="authfax" SortExpression="authfax">
			</asp:BoundField>
			<asp:BoundField DataField="malestarid" HeaderText="malestarid" SortExpression="malestarid">
			</asp:BoundField>
			<asp:BoundField DataField="femalestarid" HeaderText="femalestarid" SortExpression="femalestarid">
			</asp:BoundField>
			<asp:BoundField DataField="reviewtime" HeaderText="reviewtime" SortExpression="reviewtime">
			</asp:BoundField>
			<asp:BoundField DataField="closed" HeaderText="closed" SortExpression="closed">
			</asp:BoundField>
			<asp:BoundField DataField="totalhits" HeaderText="totalhits" SortExpression="totalhits">
			</asp:BoundField>
			<asp:BoundField DataField="authname" HeaderText="authname" SortExpression="authname">
			</asp:BoundField>
			<asp:BoundField DataField="moviemanualoveride" HeaderText="moviemanualoveride" SortExpression="moviemanualoveride">
			</asp:BoundField>
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx"><button>Return to Movie Console</button></a>
</div>
</body>
</html>