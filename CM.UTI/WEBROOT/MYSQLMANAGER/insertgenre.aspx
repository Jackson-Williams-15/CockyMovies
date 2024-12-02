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
		DRIVE<br>INSERT A NEW GENRE<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" DeleteCommand="DELETE FROM  genres  WHERE  Id  = ? AND (( Name  = ?) OR ( Name  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO  genres  ( Id ,  Name ) VALUES (?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM  genres " UpdateCommand="UPDATE  genres  SET  Name  = ? WHERE  Id  = ? AND (( Name  = ?) OR ( Name  IS NULL AND ? IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_Id" Type="Int32" />
			<asp:Parameter Name="original_Name" Type="String" />
			<asp:Parameter Name="original_Name" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="Id" Type="Int32" />
			<asp:Parameter Name="Name" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="Name" Type="String" />
			<asp:Parameter Name="original_Id" Type="Int32" />
			<asp:Parameter Name="original_Name" Type="String" />
			<asp:Parameter Name="original_Name" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" EnableModelValidation="True">
		<Fields>
			<asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id">
			</asp:BoundField>
			<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
			</asp:BoundField>
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx"><button>Return to Movie Console</button></a>

</body>
</html>