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
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=COCKY;User ID=sa;Password=*Columbia1" DeleteCommand="DELETE FROM [employees] WHERE [id] = @original_id AND (([employeefullname] = @original_employeefullname) OR ([employeefullname] IS NULL AND @original_employeefullname IS NULL)) AND (([firstname] = @original_firstname) OR ([firstname] IS NULL AND @original_firstname IS NULL)) AND (([middle] = @original_middle) OR ([middle] IS NULL AND @original_middle IS NULL)) AND (([lastname] = @original_lastname) OR ([lastname] IS NULL AND @original_lastname IS NULL)) AND (([companyid] = @original_companyid) OR ([companyid] IS NULL AND @original_companyid IS NULL)) AND (([subaccount] = @original_subaccount) OR ([subaccount] IS NULL AND @original_subaccount IS NULL)) AND (([jid] = @original_jid) OR ([jid] IS NULL AND @original_jid IS NULL)) AND (([ncrid] = @original_ncrid) OR ([ncrid] IS NULL AND @original_ncrid IS NULL)) AND (([dynamicsid] = @original_dynamicsid) OR ([dynamicsid] IS NULL AND @original_dynamicsid IS NULL))" InsertCommand="INSERT INTO [employees] ([employeefullname], [firstname], [middle], [lastname], [companyid], [subaccount], [jid], [ncrid], [dynamicsid]) VALUES (@employeefullname, @firstname, @middle, @lastname, @companyid, @subaccount, @jid, @ncrid, @dynamicsid)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [employees]" UpdateCommand="UPDATE [employees] SET [employeefullname] = @employeefullname, [firstname] = @firstname, [middle] = @middle, [lastname] = @lastname, [companyid] = @companyid, [subaccount] = @subaccount, [jid] = @jid, [ncrid] = @ncrid, [dynamicsid] = @dynamicsid WHERE [id] = @original_id AND (([employeefullname] = @original_employeefullname) OR ([employeefullname] IS NULL AND @original_employeefullname IS NULL)) AND (([firstname] = @original_firstname) OR ([firstname] IS NULL AND @original_firstname IS NULL)) AND (([middle] = @original_middle) OR ([middle] IS NULL AND @original_middle IS NULL)) AND (([lastname] = @original_lastname) OR ([lastname] IS NULL AND @original_lastname IS NULL)) AND (([companyid] = @original_companyid) OR ([companyid] IS NULL AND @original_companyid IS NULL)) AND (([subaccount] = @original_subaccount) OR ([subaccount] IS NULL AND @original_subaccount IS NULL)) AND (([jid] = @original_jid) OR ([jid] IS NULL AND @original_jid IS NULL)) AND (([ncrid] = @original_ncrid) OR ([ncrid] IS NULL AND @original_ncrid IS NULL)) AND (([dynamicsid] = @original_dynamicsid) OR ([dynamicsid] IS NULL AND @original_dynamicsid IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_firstname" Type="String" />
			<asp:Parameter Name="original_middle" Type="String" />
			<asp:Parameter Name="original_lastname" Type="String" />
			<asp:Parameter Name="original_companyid" Type="String" />
			<asp:Parameter Name="original_subaccount" Type="String" />
			<asp:Parameter Name="original_jid" Type="Int32" />
			<asp:Parameter Name="original_ncrid" Type="String" />
			<asp:Parameter Name="original_dynamicsid" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="employeefullname" Type="String" />
			<asp:Parameter Name="firstname" Type="String" />
			<asp:Parameter Name="middle" Type="String" />
			<asp:Parameter Name="lastname" Type="String" />
			<asp:Parameter Name="companyid" Type="String" />
			<asp:Parameter Name="subaccount" Type="String" />
			<asp:Parameter Name="jid" Type="Int32" />
			<asp:Parameter Name="ncrid" Type="String" />
			<asp:Parameter Name="dynamicsid" Type="String" />
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
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_firstname" Type="String" />
			<asp:Parameter Name="original_middle" Type="String" />
			<asp:Parameter Name="original_lastname" Type="String" />
			<asp:Parameter Name="original_companyid" Type="String" />
			<asp:Parameter Name="original_subaccount" Type="String" />
			<asp:Parameter Name="original_jid" Type="Int32" />
			<asp:Parameter Name="original_ncrid" Type="String" />
			<asp:Parameter Name="original_dynamicsid" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True">
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
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True">
			</asp:CommandField>
		</Fields>
	</asp:DetailsView>
	</p>
</form>
</div>
<a href="manageemployees.aspx">Return to Movie Console</a>
</div>
</body>
</html>