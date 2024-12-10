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
		DRIVE<br>INSERT A NEW EMPLOYEE REVIEW<br>
		<div class="auto-style1">
		</div>
	</div>
	<div align="center">
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=VELOCITY;User ID=sa;Password=*Columbia1" DeleteCommand="DELETE FROM [employee_reviews] WHERE [id] = @original_id AND (([employeefullname] = @original_employeefullname) OR ([employeefullname] IS NULL AND @original_employeefullname IS NULL)) AND (([reviewdate] = @original_reviewdate) OR ([reviewdate] IS NULL AND @original_reviewdate IS NULL)) AND (([reviewgivendate] = @original_reviewgivendate) OR ([reviewgivendate] IS NULL AND @original_reviewgivendate IS NULL)) AND (([reviewdetails] = @original_reviewdetails) OR ([reviewdetails] IS NULL AND @original_reviewdetails IS NULL)) AND (([calendaryear] = @original_calendaryear) OR ([calendaryear] IS NULL AND @original_calendaryear IS NULL)) AND (([notesondelivery] = @original_notesondelivery) OR ([notesondelivery] IS NULL AND @original_notesondelivery IS NULL)) AND (([reviewurl] = @original_reviewurl) OR ([reviewurl] IS NULL AND @original_reviewurl IS NULL))" InsertCommand="INSERT INTO [employee_reviews] ([employeefullname], [reviewdate], [reviewgivendate], [reviewdetails], [calendaryear], [notesondelivery], [reviewurl]) VALUES (@employeefullname, @reviewdate, @reviewgivendate, @reviewdetails, @calendaryear, @notesondelivery, @reviewurl)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [employee_reviews]" UpdateCommand="UPDATE [employee_reviews] SET [employeefullname] = @employeefullname, [reviewdate] = @reviewdate, [reviewgivendate] = @reviewgivendate, [reviewdetails] = @reviewdetails, [calendaryear] = @calendaryear, [notesondelivery] = @notesondelivery, [reviewurl] = @reviewurl WHERE [id] = @original_id AND (([employeefullname] = @original_employeefullname) OR ([employeefullname] IS NULL AND @original_employeefullname IS NULL)) AND (([reviewdate] = @original_reviewdate) OR ([reviewdate] IS NULL AND @original_reviewdate IS NULL)) AND (([reviewgivendate] = @original_reviewgivendate) OR ([reviewgivendate] IS NULL AND @original_reviewgivendate IS NULL)) AND (([reviewdetails] = @original_reviewdetails) OR ([reviewdetails] IS NULL AND @original_reviewdetails IS NULL)) AND (([calendaryear] = @original_calendaryear) OR ([calendaryear] IS NULL AND @original_calendaryear IS NULL)) AND (([notesondelivery] = @original_notesondelivery) OR ([notesondelivery] IS NULL AND @original_notesondelivery IS NULL)) AND (([reviewurl] = @original_reviewurl) OR ([reviewurl] IS NULL AND @original_reviewurl IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_reviewdate" Type="String" />
			<asp:Parameter Name="original_reviewgivendate" Type="String" />
			<asp:Parameter Name="original_reviewdetails" Type="String" />
			<asp:Parameter Name="original_calendaryear" Type="String" />
			<asp:Parameter Name="original_notesondelivery" Type="String" />
			<asp:Parameter Name="original_reviewurl" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="employeefullname" Type="String" />
			<asp:Parameter Name="reviewdate" Type="String" />
			<asp:Parameter Name="reviewgivendate" Type="String" />
			<asp:Parameter Name="reviewdetails" Type="String" />
			<asp:Parameter Name="calendaryear" Type="String" />
			<asp:Parameter Name="notesondelivery" Type="String" />
			<asp:Parameter Name="reviewurl" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="employeefullname" Type="String" />
			<asp:Parameter Name="reviewdate" Type="String" />
			<asp:Parameter Name="reviewgivendate" Type="String" />
			<asp:Parameter Name="reviewdetails" Type="String" />
			<asp:Parameter Name="calendaryear" Type="String" />
			<asp:Parameter Name="notesondelivery" Type="String" />
			<asp:Parameter Name="reviewurl" Type="String" />
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_employeefullname" Type="String" />
			<asp:Parameter Name="original_reviewdate" Type="String" />
			<asp:Parameter Name="original_reviewgivendate" Type="String" />
			<asp:Parameter Name="original_reviewdetails" Type="String" />
			<asp:Parameter Name="original_calendaryear" Type="String" />
			<asp:Parameter Name="original_notesondelivery" Type="String" />
			<asp:Parameter Name="original_reviewurl" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="employeefullname" HeaderText="employeefullname" SortExpression="employeefullname">
			</asp:BoundField>
			<asp:BoundField DataField="reviewdate" HeaderText="reviewdate" SortExpression="reviewdate">
			</asp:BoundField>
			<asp:BoundField DataField="reviewgivendate" HeaderText="reviewgivendate" SortExpression="reviewgivendate">
			</asp:BoundField>
			<asp:BoundField DataField="reviewdetails" HeaderText="reviewdetails" SortExpression="reviewdetails">
			</asp:BoundField>
			<asp:BoundField DataField="calendaryear" HeaderText="calendaryear" SortExpression="calendaryear">
			</asp:BoundField>
			<asp:BoundField DataField="notesondelivery" HeaderText="notesondelivery" SortExpression="notesondelivery">
			</asp:BoundField>
			<asp:BoundField DataField="reviewurl" HeaderText="reviewurl" SortExpression="reviewurl">
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