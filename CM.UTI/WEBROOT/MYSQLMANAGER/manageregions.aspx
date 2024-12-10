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
		<br><br><br><br>STORE 33 MOVIE MANAGER - COLUMBIA, SC<br>2300 FORREST 
		DRIVE<br>
		<div class="auto-style1">
			<asp:GridView id="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" DataKeyNames="regionid" DataSourceID="SqlDataSource1" Width="1408px" horizontalalign="Center">
				<Columns>
					<asp:BoundField DataField="regionid" HeaderText="regionid" InsertVisible="False" ReadOnly="True" SortExpression="regionid">
					</asp:BoundField>
					<asp:BoundField DataField="regid" HeaderText="regid" SortExpression="regid">
					</asp:BoundField>
					<asp:BoundField DataField="hqaddress1" HeaderText="hqaddress1" SortExpression="hqaddress1">
					</asp:BoundField>
					<asp:BoundField DataField="hqaddress2" HeaderText="hqaddress2" SortExpression="hqaddress2">
					</asp:BoundField>
					<asp:BoundField DataField="hqcity" HeaderText="hqcity" SortExpression="hqcity">
					</asp:BoundField>
					<asp:BoundField DataField="hqregion" HeaderText="hqregion" SortExpression="hqregion">
					</asp:BoundField>
					<asp:BoundField DataField="hqcountry" HeaderText="hqcountry" SortExpression="hqcountry">
					</asp:BoundField>
					<asp:BoundField DataField="companyid" HeaderText="companyid" SortExpression="companyid">
					</asp:BoundField>
					<asp:BoundField DataField="subaccount" HeaderText="subaccount" SortExpression="subaccount">
					</asp:BoundField>
					<asp:BoundField DataField="storeid" HeaderText="storeid" SortExpression="storeid">
					</asp:BoundField>
				</Columns>
			</asp:GridView>
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=COCKY;User ID=sa;Password=*Columbia1" DeleteCommand="DELETE FROM [region] WHERE [regionid] = @original_regionid AND (([regid] = @original_regid) OR ([regid] IS NULL AND @original_regid IS NULL)) AND (([hqaddress1] = @original_hqaddress1) OR ([hqaddress1] IS NULL AND @original_hqaddress1 IS NULL)) AND (([hqaddress2] = @original_hqaddress2) OR ([hqaddress2] IS NULL AND @original_hqaddress2 IS NULL)) AND (([hqcity] = @original_hqcity) OR ([hqcity] IS NULL AND @original_hqcity IS NULL)) AND (([hqregion] = @original_hqregion) OR ([hqregion] IS NULL AND @original_hqregion IS NULL)) AND (([hqcountry] = @original_hqcountry) OR ([hqcountry] IS NULL AND @original_hqcountry IS NULL)) AND (([companyid] = @original_companyid) OR ([companyid] IS NULL AND @original_companyid IS NULL)) AND (([subaccount] = @original_subaccount) OR ([subaccount] IS NULL AND @original_subaccount IS NULL)) AND (([storeid] = @original_storeid) OR ([storeid] IS NULL AND @original_storeid IS NULL))" InsertCommand="INSERT INTO [region] ([regid], [hqaddress1], [hqaddress2], [hqcity], [hqregion], [hqcountry], [companyid], [subaccount], [storeid]) VALUES (@regid, @hqaddress1, @hqaddress2, @hqcity, @hqregion, @hqcountry, @companyid, @subaccount, @storeid)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [region]" UpdateCommand="UPDATE [region] SET [regid] = @regid, [hqaddress1] = @hqaddress1, [hqaddress2] = @hqaddress2, [hqcity] = @hqcity, [hqregion] = @hqregion, [hqcountry] = @hqcountry, [companyid] = @companyid, [subaccount] = @subaccount, [storeid] = @storeid WHERE [regionid] = @original_regionid AND (([regid] = @original_regid) OR ([regid] IS NULL AND @original_regid IS NULL)) AND (([hqaddress1] = @original_hqaddress1) OR ([hqaddress1] IS NULL AND @original_hqaddress1 IS NULL)) AND (([hqaddress2] = @original_hqaddress2) OR ([hqaddress2] IS NULL AND @original_hqaddress2 IS NULL)) AND (([hqcity] = @original_hqcity) OR ([hqcity] IS NULL AND @original_hqcity IS NULL)) AND (([hqregion] = @original_hqregion) OR ([hqregion] IS NULL AND @original_hqregion IS NULL)) AND (([hqcountry] = @original_hqcountry) OR ([hqcountry] IS NULL AND @original_hqcountry IS NULL)) AND (([companyid] = @original_companyid) OR ([companyid] IS NULL AND @original_companyid IS NULL)) AND (([subaccount] = @original_subaccount) OR ([subaccount] IS NULL AND @original_subaccount IS NULL)) AND (([storeid] = @original_storeid) OR ([storeid] IS NULL AND @original_storeid IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_regionid" Type="Int32" />
			<asp:Parameter Name="original_regid" Type="Int32" />
			<asp:Parameter Name="original_hqaddress1" Type="String" />
			<asp:Parameter Name="original_hqaddress2" Type="String" />
			<asp:Parameter Name="original_hqcity" Type="String" />
			<asp:Parameter Name="original_hqregion" Type="String" />
			<asp:Parameter Name="original_hqcountry" Type="String" />
			<asp:Parameter Name="original_companyid" Type="String" />
			<asp:Parameter Name="original_subaccount" Type="String" />
			<asp:Parameter Name="original_storeid" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="regid" Type="Int32" />
			<asp:Parameter Name="hqaddress1" Type="String" />
			<asp:Parameter Name="hqaddress2" Type="String" />
			<asp:Parameter Name="hqcity" Type="String" />
			<asp:Parameter Name="hqregion" Type="String" />
			<asp:Parameter Name="hqcountry" Type="String" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="storeid" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="regid" Type="Int32" />
			<asp:Parameter Name="hqaddress1" Type="String" />
			<asp:Parameter Name="hqaddress2" Type="String" />
			<asp:Parameter Name="hqcity" Type="String" />
			<asp:Parameter Name="hqregion" Type="String" />
			<asp:Parameter Name="hqcountry" Type="String" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="storeid" Type="String" />
			<asp:Parameter Name="original_regionid" Type="Int32" />
			<asp:Parameter Name="original_regid" Type="Int32" />
			<asp:Parameter Name="original_hqaddress1" Type="String" />
			<asp:Parameter Name="original_hqaddress2" Type="String" />
			<asp:Parameter Name="original_hqcity" Type="String" />
			<asp:Parameter Name="original_hqregion" Type="String" />
			<asp:Parameter Name="original_hqcountry" Type="String" />
			<asp:Parameter Name="original_companyid" Type="String" />
			<asp:Parameter Name="original_subaccount" Type="String" />
			<asp:Parameter Name="original_storeid" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>

<div><a href="insertregion.aspx">Insert a New Store</a><br></div>
<div><a href="regionalmanager.aspx">ReturnToRegionalManager</a></div>
</form>
</div>
</body>
</html>