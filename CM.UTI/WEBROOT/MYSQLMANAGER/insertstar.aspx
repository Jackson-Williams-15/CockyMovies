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
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" DeleteCommand="DELETE FROM  velo_users_moviestars  WHERE  id  = ? AND (( starid  = ?) OR ( starid  IS NULL AND ? IS NULL)) AND (( starbirthdayyear  = ?) OR ( starbirthdayyear  IS NULL AND ? IS NULL)) AND (( starname  = ?) OR ( starname  IS NULL AND ? IS NULL)) AND (( otherwork1  = ?) OR ( otherwork1  IS NULL AND ? IS NULL)) AND (( otherwork2  = ?) OR ( otherwork2  IS NULL AND ? IS NULL)) AND (( otherwork3  = ?) OR ( otherwork3  IS NULL AND ? IS NULL)) AND (( pictureurl  = ?) OR ( pictureurl  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO  velo_users_moviestars  ( id ,  starid ,  starbirthdayyear ,  starname ,  otherwork1 ,  otherwork2 ,  otherwork3 ,  pictureurl ) VALUES (?, ?, ?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM  velo_users_moviestars " UpdateCommand="UPDATE  velo_users_moviestars  SET  starid  = ?,  starbirthdayyear  = ?,  starname  = ?,  otherwork1  = ?,  otherwork2  = ?,  otherwork3  = ?,  pictureurl  = ? WHERE  id  = ? AND (( starid  = ?) OR ( starid  IS NULL AND ? IS NULL)) AND (( starbirthdayyear  = ?) OR ( starbirthdayyear  IS NULL AND ? IS NULL)) AND (( starname  = ?) OR ( starname  IS NULL AND ? IS NULL)) AND (( otherwork1  = ?) OR ( otherwork1  IS NULL AND ? IS NULL)) AND (( otherwork2  = ?) OR ( otherwork2  IS NULL AND ? IS NULL)) AND (( otherwork3  = ?) OR ( otherwork3  IS NULL AND ? IS NULL)) AND (( pictureurl  = ?) OR ( pictureurl  IS NULL AND ? IS NULL))" ConflictDetection="CompareAllValues">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_starid" Type="Int32" />
			<asp:Parameter Name="original_starid" Type="Int32" />
			<asp:Parameter Name="original_starbirthdayyear" Type="String" />
			<asp:Parameter Name="original_starbirthdayyear" Type="String" />
			<asp:Parameter Name="original_starname" Type="String" />
			<asp:Parameter Name="original_starname" Type="String" />
			<asp:Parameter Name="original_otherwork1" Type="Int32" />
		    <asp:Parameter Name="original_otherwork1" Type="Int32" />
            <asp:Parameter Name="original_otherwork2" Type="Int32" />
            <asp:Parameter Name="original_otherwork2" Type="Int32" />
            <asp:Parameter Name="original_otherwork3" Type="Int32" />
            <asp:Parameter Name="original_otherwork3" Type="Int32" />
            <asp:Parameter Name="original_pictureurl" Type="String" />
            <asp:Parameter Name="original_pictureurl" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="id" Type="Int32" />
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
			<asp:Parameter Name="original_starid" Type="Int32" />
			<asp:Parameter Name="original_starbirthdayyear" Type="String" />
			<asp:Parameter Name="original_starbirthdayyear" Type="String" />
			<asp:Parameter Name="original_starname" Type="String" />
			<asp:Parameter Name="original_starname" Type="String" />
			<asp:Parameter Name="original_otherwork1" Type="Int32" />
		    <asp:Parameter Name="original_otherwork1" Type="Int32" />
            <asp:Parameter Name="original_otherwork2" Type="Int32" />
            <asp:Parameter Name="original_otherwork2" Type="Int32" />
            <asp:Parameter Name="original_otherwork3" Type="Int32" />
            <asp:Parameter Name="original_otherwork3" Type="Int32" />
            <asp:Parameter Name="original_pictureurl" Type="String" />
            <asp:Parameter Name="original_pictureurl" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p class="auto-style1">
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" Font-Size="10pt" EnableModelValidation="True">
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
		    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managestores.aspx">Return to Store Console</a>

</body>
</html>