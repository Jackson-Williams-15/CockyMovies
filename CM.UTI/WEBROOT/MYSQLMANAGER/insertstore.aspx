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
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" DeleteCommand="DELETE FROM  stores  WHERE  id  = ? AND (( storeid  = ?) OR ( storeid  IS NULL AND ? IS NULL)) AND (( address1  = ?) OR ( address1  IS NULL AND ? IS NULL)) AND (( address2  = ?) OR ( address2  IS NULL AND ? IS NULL)) AND (( city  = ?) OR ( city  IS NULL AND ? IS NULL)) AND (( region  = ?) OR ( region  IS NULL AND ? IS NULL)) AND (( country  = ?) OR ( country  IS NULL AND ? IS NULL)) AND (( regionid  = ?) OR ( regionid  IS NULL AND ? IS NULL)) AND (( companyid  = ?) OR ( companyid  IS NULL AND ? IS NULL)) AND (( subaccount  = ?) OR ( subaccount  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO  stores  ( id ,  storeid ,  address1 ,  address2 ,  city ,  region ,  country ,  regionid ,  companyid ,  subaccount ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM  stores " UpdateCommand="UPDATE  stores  SET  storeid  = ?,  address1  = ?,  address2  = ?,  city  = ?,  region  = ?,  country  = ?,  regionid  = ?,  companyid  = ?,  subaccount  = ? WHERE  id  = ? AND (( storeid  = ?) OR ( storeid  IS NULL AND ? IS NULL)) AND (( address1  = ?) OR ( address1  IS NULL AND ? IS NULL)) AND (( address2  = ?) OR ( address2  IS NULL AND ? IS NULL)) AND (( city  = ?) OR ( city  IS NULL AND ? IS NULL)) AND (( region  = ?) OR ( region  IS NULL AND ? IS NULL)) AND (( country  = ?) OR ( country  IS NULL AND ? IS NULL)) AND (( regionid  = ?) OR ( regionid  IS NULL AND ? IS NULL)) AND (( companyid  = ?) OR ( companyid  IS NULL AND ? IS NULL)) AND (( subaccount  = ?) OR ( subaccount  IS NULL AND ? IS NULL))" ConflictDetection="CompareAllValues">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_storeid" Type="String" />
            <asp:Parameter Name="original_storeid" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_region" Type="String" />
            <asp:Parameter Name="original_region" Type="String" />
            <asp:Parameter Name="original_country" Type="String" />
            <asp:Parameter Name="original_country" Type="String" />
		    <asp:Parameter Name="original_regionid" Type="Int32" />
            <asp:Parameter Name="original_regionid" Type="Int32" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="id" Type="Int32" />
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
			<asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_storeid" Type="String" />
            <asp:Parameter Name="original_storeid" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_region" Type="String" />
            <asp:Parameter Name="original_region" Type="String" />
            <asp:Parameter Name="original_country" Type="String" />
            <asp:Parameter Name="original_country" Type="String" />
		    <asp:Parameter Name="original_regionid" Type="Int32" />
            <asp:Parameter Name="original_regionid" Type="Int32" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" Font-Size="10pt" EnableModelValidation="True" HorizontalAlign="Center">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
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
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managestores.aspx">Return to Store Console</a>

</body>
</html>