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
		DRIVE<br>INSERT A NEW EMPLOYEE<br>
		<div class="auto-style1">
		</div>
	</div>
	<div align="center">
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" DeleteCommand="DELETE FROM employees WHERE  id  = ? AND (( employeefullname  = ?) OR ( employeefullname  IS NULL AND ? IS NULL)) AND (( firstname  = ?) OR ( firstname  IS NULL AND ? IS NULL)) AND (( middle  = ?) OR ( middle  IS NULL AND ? IS NULL)) AND (( lastname  = ?) OR ( lastname  IS NULL AND ? IS NULL)) AND (( companyid  = ?) OR ( companyid  IS NULL AND ? IS NULL)) AND (( subaccount  = ?) OR ( subaccount  IS NULL AND ? IS NULL)) AND (( jid  = ?) OR ( jid  IS NULL AND ? IS NULL)) AND (( ncrid  = ?) OR ( ncrid  IS NULL AND ? IS NULL)) AND (( dynamicsid  = ?) OR ( dynamicsid  IS NULL AND ? IS NULL)) AND (( address1  = ?) OR ( address1  IS NULL AND ? IS NULL)) AND (( address2  = ?) OR ( address2  IS NULL AND ? IS NULL)) AND (( city  = ?) OR ( city  IS NULL AND ? IS NULL)) AND (( regionstate  = ?) OR ( regionstate  IS NULL AND ? IS NULL)) AND (( postalcode  = ?) OR ( postalcode  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO employees ( id ,  employeefullname ,  firstname ,  middle ,  lastname ,  companyid ,  subaccount ,  jid ,  ncrid ,  dynamicsid ,  address1 ,  address2 ,  city ,  regionstate ,  postalcode ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM employees" UpdateCommand="UPDATE employees SET  employeefullname  = ?,  firstname  = ?,  middle  = ?,  lastname  = ?,  companyid  = ?,  subaccount  = ?,  jid  = ?,  ncrid  = ?,  dynamicsid  = ?,  address1  = ?,  address2  = ?,  city  = ?,  regionstate  = ?,  postalcode  = ? WHERE  id  = ? AND (( employeefullname  = ?) OR ( employeefullname  IS NULL AND ? IS NULL)) AND (( firstname  = ?) OR ( firstname  IS NULL AND ? IS NULL)) AND (( middle  = ?) OR ( middle  IS NULL AND ? IS NULL)) AND (( lastname  = ?) OR ( lastname  IS NULL AND ? IS NULL)) AND (( companyid  = ?) OR ( companyid  IS NULL AND ? IS NULL)) AND (( subaccount  = ?) OR ( subaccount  IS NULL AND ? IS NULL)) AND (( jid  = ?) OR ( jid  IS NULL AND ? IS NULL)) AND (( ncrid  = ?) OR ( ncrid  IS NULL AND ? IS NULL)) AND (( dynamicsid  = ?) OR ( dynamicsid  IS NULL AND ? IS NULL)) AND (( address1  = ?) OR ( address1  IS NULL AND ? IS NULL)) AND (( address2  = ?) OR ( address2  IS NULL AND ? IS NULL)) AND (( city  = ?) OR ( city  IS NULL AND ? IS NULL)) AND (( regionstate  = ?) OR ( regionstate  IS NULL AND ? IS NULL)) AND (( postalcode  = ?) OR ( postalcode  IS NULL AND ? IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_firstname" Type="String" />
			<asp:Parameter Name="original_firstname" Type="String" />
			<asp:Parameter Name="original_middle" Type="String" />
			<asp:Parameter Name="original_middle" Type="String" />
			<asp:Parameter Name="original_lastname" Type="String" />
			<asp:Parameter Name="original_lastname" Type="String" />
			<asp:Parameter Name="original_companyid" Type="String" />
		    <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_jid" Type="Int32" />
            <asp:Parameter Name="original_jid" Type="Int32" />
            <asp:Parameter Name="original_ncrid" Type="String" />
            <asp:Parameter Name="original_ncrid" Type="String" />
            <asp:Parameter Name="original_dynamicsid" Type="String" />
            <asp:Parameter Name="original_dynamicsid" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_regionstate" Type="String" />
            <asp:Parameter Name="original_regionstate" Type="String" />
            <asp:Parameter Name="original_postalcode" Type="String" />
            <asp:Parameter Name="original_postalcode" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="id" Type="Int32" />
			<asp:Parameter Name="employeefullname" Type="String" />
			<asp:Parameter Name="firstname" Type="String" />
			<asp:Parameter Name="middle" Type="String" />
			<asp:Parameter Name="lastname" Type="String" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="jid" Type="Int32" />
			<asp:Parameter Name="ncrid" Type="String" />
		    <asp:Parameter Name="dynamicsid" Type="String" />
            <asp:Parameter Name="address1" Type="String" />
            <asp:Parameter Name="address2" Type="String" />
            <asp:Parameter Name="city" Type="String" />
            <asp:Parameter Name="regionstate" Type="String" />
            <asp:Parameter Name="postalcode" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="employeefullname" Type="String" />
			<asp:Parameter Name="firstname" Type="String" />
			<asp:Parameter Name="middle" Type="String" />
			<asp:Parameter Name="lastname" Type="String" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="jid" Type="Int32" />
			<asp:Parameter Name="ncrid" Type="String" />
			<asp:Parameter Name="dynamicsid" Type="String" />
			<asp:Parameter Name="address1" Type="String" />
			<asp:Parameter Name="address2" Type="String" />
			<asp:Parameter Name="city" Type="String" />
			<asp:Parameter Name="regionstate" Type="String" />
			<asp:Parameter Name="postalcode" Type="String" />
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_firstname" Type="String" />
			<asp:Parameter Name="original_firstname" Type="String" />
		    <asp:Parameter Name="original_middle" Type="String" />
            <asp:Parameter Name="original_middle" Type="String" />
            <asp:Parameter Name="original_lastname" Type="String" />
            <asp:Parameter Name="original_lastname" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_companyid" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_subaccount" Type="String" />
            <asp:Parameter Name="original_jid" Type="Int32" />
            <asp:Parameter Name="original_jid" Type="Int32" />
            <asp:Parameter Name="original_ncrid" Type="String" />
            <asp:Parameter Name="original_ncrid" Type="String" />
            <asp:Parameter Name="original_dynamicsid" Type="String" />
            <asp:Parameter Name="original_dynamicsid" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address1" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_address2" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_city" Type="String" />
            <asp:Parameter Name="original_regionstate" Type="String" />
            <asp:Parameter Name="original_regionstate" Type="String" />
            <asp:Parameter Name="original_postalcode" Type="String" />
            <asp:Parameter Name="original_postalcode" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" EnableModelValidation="True">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="employeefullname" HeaderText="employeefullname" SortExpression="employeefullname">
			</asp:BoundField>
			<asp:BoundField DataField="firstname" HeaderText="firstname" SortExpression="firstname">
			</asp:BoundField>
			<asp:BoundField DataField="middle" HeaderText="middle" SortExpression="middle">
			</asp:BoundField>
			<asp:BoundField DataField="lastname" HeaderText="lastname" SortExpression="lastname">
			</asp:BoundField>
			<asp:BoundField DataField="companyid" HeaderText="companyid" SortExpression="companyid">
			</asp:BoundField>
			<asp:BoundField DataField="subaccount" HeaderText="subaccount" SortExpression="subaccount">
			</asp:BoundField>
			<asp:BoundField DataField="jid" HeaderText="jid" SortExpression="jid">
			</asp:BoundField>
			<asp:BoundField DataField="ncrid" HeaderText="ncrid" SortExpression="ncrid">
			</asp:BoundField>
			<asp:BoundField DataField="dynamicsid" HeaderText="dynamicsid" SortExpression="dynamicsid">
			</asp:BoundField>
			<asp:BoundField DataField="address1" HeaderText="address1" SortExpression="address1">
			</asp:BoundField>
			<asp:BoundField DataField="address2" HeaderText="address2" SortExpression="address2">
			</asp:BoundField>
			<asp:BoundField DataField="city" HeaderText="city" SortExpression="city">
			</asp:BoundField>
			<asp:BoundField DataField="regionstate" HeaderText="regionstate" SortExpression="regionstate">
			</asp:BoundField>
			<asp:BoundField DataField="postalcode" HeaderText="postalcode" SortExpression="postalcode">
			</asp:BoundField>
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
	</asp:DetailsView>
	</p>
</form>
</div>
<a href="manageemployees.aspx">Return to Movie Console</a>
</div>
</body>
</html>