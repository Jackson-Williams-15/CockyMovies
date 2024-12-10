<html>
<head><title>CockyMoviesManager</title>
<style type="text/css">
.auto-style1 {
	text-align: center;
}
</style>
</head>
<body>
<form id="form1" runat="server">
	<div class="auto-style1">
		<br>
		<asp:Image id="Image1" runat="server" ImageUrl="./cocky547.png" />
		<br><br><br><br>STORE 33 MOVIE MANAGER - COLUMBIA, SC<br>2300 FORREST 
		DRIVE<br>INSERT A NEW STAR<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=VELOCITY;User ID=sa;Password=*Columbia1" DeleteCommand="DELETE FROM [velo_users_moviestars] WHERE [id] = @original_id AND (([starid] = @original_starid) OR ([starid] IS NULL AND @original_starid IS NULL)) AND (([starbirthdayyear] = @original_starbirthdayyear) OR ([starbirthdayyear] IS NULL AND @original_starbirthdayyear IS NULL)) AND (([starname] = @original_starname) OR ([starname] IS NULL AND @original_starname IS NULL)) AND (([otherwork1] = @original_otherwork1) OR ([otherwork1] IS NULL AND @original_otherwork1 IS NULL)) AND (([otherwork2] = @original_otherwork2) OR ([otherwork2] IS NULL AND @original_otherwork2 IS NULL)) AND (([otherwork3] = @original_otherwork3) OR ([otherwork3] IS NULL AND @original_otherwork3 IS NULL)) AND (([pictureurl] = @original_pictureurl) OR ([pictureurl] IS NULL AND @original_pictureurl IS NULL))" InsertCommand="INSERT INTO [velo_users_moviestars] ([starid], [starbirthdayyear], [starname], [otherwork1], [otherwork2], [otherwork3], [pictureurl]) VALUES (@starid, @starbirthdayyear, @starname, @otherwork1, @otherwork2, @otherwork3, @pictureurl)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [velo_users_moviestars]" UpdateCommand="UPDATE [velo_users_moviestars] SET [starid] = @starid, [starbirthdayyear] = @starbirthdayyear, [starname] = @starname, [otherwork1] = @otherwork1, [otherwork2] = @otherwork2, [otherwork3] = @otherwork3, [pictureurl] = @pictureurl WHERE [id] = @original_id AND (([starid] = @original_starid) OR ([starid] IS NULL AND @original_starid IS NULL)) AND (([starbirthdayyear] = @original_starbirthdayyear) OR ([starbirthdayyear] IS NULL AND @original_starbirthdayyear IS NULL)) AND (([starname] = @original_starname) OR ([starname] IS NULL AND @original_starname IS NULL)) AND (([otherwork1] = @original_otherwork1) OR ([otherwork1] IS NULL AND @original_otherwork1 IS NULL)) AND (([otherwork2] = @original_otherwork2) OR ([otherwork2] IS NULL AND @original_otherwork2 IS NULL)) AND (([otherwork3] = @original_otherwork3) OR ([otherwork3] IS NULL AND @original_otherwork3 IS NULL)) AND (([pictureurl] = @original_pictureurl) OR ([pictureurl] IS NULL AND @original_pictureurl IS NULL))" ConflictDetection="CompareAllValues">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_starid" Type="Int32" />
			<asp:Parameter Name="original_starbirthdayyear" Type="String" />
			<asp:Parameter Name="original_starname" Type="String" />
			<asp:Parameter Name="original_otherwork1" Type="Int32" />
			<asp:Parameter Name="original_otherwork2" Type="Int32" />
			<asp:Parameter Name="original_otherwork3" Type="Int32" />
			<asp:Parameter Name="original_pictureurl" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="starid" Type="Int32" />
			<asp:Parameter Name="starbirthdayyear" Type="String" />
			<asp:Parameter Name="starname" Type="String" />
			<asp:Parameter Name="otherwork1" Type="Int32" />
			<asp:Parameter Name="otherwork2" Type="Int32" />
			<asp:Parameter Name="otherwork3" Type="Int32" />
			<asp:Parameter Name="pictureurl" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="starid" Type="Int32" />
			<asp:Parameter Name="starbirthdayyear" Type="String" />
			<asp:Parameter Name="starname" Type="String" />
			<asp:Parameter Name="otherwork1" Type="Int32" />
			<asp:Parameter Name="otherwork2" Type="Int32" />
			<asp:Parameter Name="otherwork3" Type="Int32" />
			<asp:Parameter Name="pictureurl" Type="String" />
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_starid" Type="Int32" />
			<asp:Parameter Name="original_starbirthdayyear" Type="String" />
			<asp:Parameter Name="original_starname" Type="String" />
			<asp:Parameter Name="original_otherwork1" Type="Int32" />
			<asp:Parameter Name="original_otherwork2" Type="Int32" />
			<asp:Parameter Name="original_otherwork3" Type="Int32" />
			<asp:Parameter Name="original_pictureurl" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p class="auto-style1">
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" Font-Size="10pt">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="starid" HeaderText="starid" SortExpression="starid">
			</asp:BoundField>
			<asp:BoundField DataField="starbirthdayyear" HeaderText="starbirthdayyear" SortExpression="starbirthdayyear">
			</asp:BoundField>
			<asp:BoundField DataField="starname" HeaderText="starname" SortExpression="starname">
			</asp:BoundField>
			<asp:BoundField DataField="otherwork1" HeaderText="otherwork1" SortExpression="otherwork1">
			</asp:BoundField>
			<asp:BoundField DataField="otherwork2" HeaderText="otherwork2" SortExpression="otherwork2">
			</asp:BoundField>
			<asp:BoundField DataField="otherwork3" HeaderText="otherwork3" SortExpression="otherwork3">
			</asp:BoundField>
			<asp:BoundField DataField="pictureurl" HeaderText="pictureurl" SortExpression="pictureurl">
			</asp:BoundField>
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True">
			</asp:CommandField>
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managestores.aspx">Return to Store Console</a>

</body>
</html>