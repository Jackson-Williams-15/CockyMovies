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
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" DeleteCommand="DELETE FROM  region  WHERE  id  = ? AND (( regionid  = ?) OR ( regionid  IS NULL AND ? IS NULL)) AND (( hqaddress1  = ?) OR ( hqaddress1  IS NULL AND ? IS NULL)) AND (( hqaddress2  = ?) OR ( hqaddress2  IS NULL AND ? IS NULL)) AND (( hqcity  = ?) OR ( hqcity  IS NULL AND ? IS NULL)) AND (( hqregion  = ?) OR ( hqregion  IS NULL AND ? IS NULL)) AND (( hqcountry  = ?) OR ( hqcountry  IS NULL AND ? IS NULL)) AND (( companyid  = ?) OR ( companyid  IS NULL AND ? IS NULL)) AND (( subaccount  = ?) OR ( subaccount  IS NULL AND ? IS NULL)) AND (( storeid  = ?) OR ( storeid  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO  region  ( id ,  regionid ,  hqaddress1 ,  hqaddress2 ,  hqcity ,  hqregion ,  hqcountry ,  companyid ,  subaccount ,  storeid ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM  region " UpdateCommand="UPDATE  region  SET  regionid  = ?,  hqaddress1  = ?,  hqaddress2  = ?,  hqcity  = ?,  hqregion  = ?,  hqcountry  = ?,  companyid  = ?,  subaccount  = ?,  storeid  = ? WHERE  id  = ? AND (( regionid  = ?) OR ( regionid  IS NULL AND ? IS NULL)) AND (( hqaddress1  = ?) OR ( hqaddress1  IS NULL AND ? IS NULL)) AND (( hqaddress2  = ?) OR ( hqaddress2  IS NULL AND ? IS NULL)) AND (( hqcity  = ?) OR ( hqcity  IS NULL AND ? IS NULL)) AND (( hqregion  = ?) OR ( hqregion  IS NULL AND ? IS NULL)) AND (( hqcountry  = ?) OR ( hqcountry  IS NULL AND ? IS NULL)) AND (( companyid  = ?) OR ( companyid  IS NULL AND ? IS NULL)) AND (( subaccount  = ?) OR ( subaccount  IS NULL AND ? IS NULL)) AND (( storeid  = ?) OR ( storeid  IS NULL AND ? IS NULL))" ConflictDetection="CompareAllValues">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_regionid" Type="Int32" />
			<asp:Parameter Name="original_regionid" Type="Int32" />
			<asp:Parameter Name="original_hqaddress1" Type="String" />
			<asp:Parameter Name="original_hqaddress1" Type="String" />
			<asp:Parameter Name="original_hqaddress2" Type="String" />
			<asp:Parameter Name="original_hqaddress2" Type="String" />
			<asp:Parameter Name="original_hqcity" Type="String" />
			<asp:Parameter Name="original_hqcity" Type="String" />
			<asp:Parameter Name="original_hqregion" Type="String" />
		    <asp:Parameter Name="original_hqregion" Type="String" />
            <asp:Parameter Name="original_hqcountry" Type="String" />
            <asp:Parameter Name="original_hqcountry" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_storeid" Type="String" />
            <asp:Parameter Name="original_storeid" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="id" Type="Int32" />
			<asp:Parameter Name="regionid" Type="Int32" />
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
			<asp:Parameter Name="regionid" Type="Int32" />
			<asp:Parameter Name="hqaddress1" Type="String" />
			<asp:Parameter Name="hqaddress2" Type="String" />
			<asp:Parameter Name="hqcity" Type="String" />
			<asp:Parameter Name="hqregion" Type="String" />
			<asp:Parameter Name="hqcountry" Type="String" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="storeid" Type="String" />
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_regionid" Type="Int32" />
			<asp:Parameter Name="original_regionid" Type="Int32" />
			<asp:Parameter Name="original_hqaddress1" Type="String" />
			<asp:Parameter Name="original_hqaddress1" Type="String" />
			<asp:Parameter Name="original_hqaddress2" Type="String" />
			<asp:Parameter Name="original_hqaddress2" Type="String" />
			<asp:Parameter Name="original_hqcity" Type="String" />
			<asp:Parameter Name="original_hqcity" Type="String" />
			<asp:Parameter Name="original_hqregion" Type="String" />
		    <asp:Parameter Name="original_hqregion" Type="String" />
            <asp:Parameter Name="original_hqcountry" Type="String" />
            <asp:Parameter Name="original_hqcountry" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_storeid" Type="String" />
            <asp:Parameter Name="original_storeid" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" Font-Size="10pt" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" EnableModelValidation="True">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="regionid" HeaderText="regionid" SortExpression="regionid">
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
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
		<EmptyDataTemplate>No Records Available</EmptyDataTemplate> 
	</asp:DetailsView>
	</p>
</form>
<a href="managestores.aspx">Return to Store Console</a>

</body>
</html>