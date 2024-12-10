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
		DRIVE<br>INSERT A NEW STORE<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=COCKY;User ID=sa;Password=*Columbia1" DeleteCommand="DELETE FROM [stores] WHERE [storesid] = @original_storesid" InsertCommand="INSERT INTO [stores] ([storeid], [address1], [address2], [city], [region], [country], [regionid], [companyid], [subaccount]) VALUES (@storeid, @address1, @address2, @city, @region, @country, @regionid, @companyid, @subaccount)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [stores]" UpdateCommand="UPDATE [stores] SET [storeid] = @storeid, [address1] = @address1, [address2] = @address2, [city] = @city, [region] = @region, [country] = @country, [regionid] = @regionid, [companyid] = @companyid, [subaccount] = @subaccount WHERE [storesid] = @original_storesid">
		<DeleteParameters>
			<asp:Parameter Name="original_storesid" Type="Int32" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="storeid" Type="String" />
			<asp:Parameter Name="address1" Type="String" />
			<asp:Parameter Name="address2" Type="String" />
			<asp:Parameter Name="city" Type="String" />
			<asp:Parameter Name="region" Type="String" />
			<asp:Parameter Name="country" Type="String" />
			<asp:Parameter Name="regionid" Type="Int32" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="storeid" Type="String" />
			<asp:Parameter Name="address1" Type="String" />
			<asp:Parameter Name="address2" Type="String" />
			<asp:Parameter Name="city" Type="String" />
			<asp:Parameter Name="region" Type="String" />
			<asp:Parameter Name="country" Type="String" />
			<asp:Parameter Name="regionid" Type="Int32" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="original_storesid" Type="Int32" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="storesid" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" Font-Size="10pt" HorizontalAlign="Center">
		<Fields>
			<asp:BoundField DataField="storesid" HeaderText="storesid" InsertVisible="False" ReadOnly="True" SortExpression="storesid">
			</asp:BoundField>
			<asp:BoundField DataField="storeid" HeaderText="storeid" SortExpression="storeid">
			</asp:BoundField>
			<asp:BoundField DataField="address1" HeaderText="address1" SortExpression="address1">
			</asp:BoundField>
			<asp:BoundField DataField="address2" HeaderText="address2" SortExpression="address2">
			</asp:BoundField>
			<asp:BoundField DataField="city" HeaderText="city" SortExpression="city">
			</asp:BoundField>
			<asp:BoundField DataField="region" HeaderText="region" SortExpression="region">
			</asp:BoundField>
			<asp:BoundField DataField="country" HeaderText="country" SortExpression="country">
			</asp:BoundField>
			<asp:BoundField DataField="regionid" HeaderText="regionid" SortExpression="regionid">
			</asp:BoundField>
			<asp:BoundField DataField="companyid" HeaderText="companyid" SortExpression="companyid">
			</asp:BoundField>
			<asp:BoundField DataField="subaccount" HeaderText="subaccount" SortExpression="subaccount">
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