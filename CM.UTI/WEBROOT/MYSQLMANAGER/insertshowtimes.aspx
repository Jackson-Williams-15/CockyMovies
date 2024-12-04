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
		<asp:Image id="Image1" runat="server" ImageUrl="cocky547.png" />
		<br><br><br><br>STORE 33 MOVIE MANAGER - COLUMBIA, SC<br>2300 FORREST 
		DRIVE<br>INSERT A NEW SHOWTIMES<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" DeleteCommand="DELETE FROM  velo_users_movies_times  WHERE  id  = ? AND (( moviename  = ?) OR ( moviename  IS NULL AND ? IS NULL)) AND (( movieid  = ?) OR ( movieid  IS NULL AND ? IS NULL)) AND (( timesone  = ?) OR ( timesone  IS NULL AND ? IS NULL)) AND (( timestwo  = ?) OR ( timestwo  IS NULL AND ? IS NULL)) AND (( timesthree  = ?) OR ( timesthree  IS NULL AND ? IS NULL)) AND (( timesfour  = ?) OR ( timesfour  IS NULL AND ? IS NULL)) AND (( timesfive  = ?) OR ( timesfive  IS NULL AND ? IS NULL)) AND (( timessix  = ?) OR ( timessix  IS NULL AND ? IS NULL)) AND (( timesseven  = ?) OR ( timesseven  IS NULL AND ? IS NULL)) AND (( moviepicture1url  = ?) OR ( moviepicture1url  IS NULL AND ? IS NULL)) AND (( moviepicture2url  = ?) OR ( moviepicture2url  IS NULL AND ? IS NULL)) AND (( moviepicture3url  = ?) OR ( moviepicture3url  IS NULL AND ? IS NULL)) AND (( youtubeurl  = ?) OR ( youtubeurl  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO  velo_users_movies_times  ( id ,  moviename ,  movieid ,  timesone ,  timestwo ,  timesthree ,  timesfour ,  timesfive ,  timessix ,  timesseven ,  moviepicture1url ,  moviepicture2url ,  moviepicture3url ,  youtubeurl ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM  velo_users_movies_times " UpdateCommand="UPDATE  velo_users_movies_times  SET  moviename  = ?,  movieid  = ?,  timesone  = ?,  timestwo  = ?,  timesthree  = ?,  timesfour  = ?,  timesfive  = ?,  timessix  = ?,  timesseven  = ?,  moviepicture1url  = ?,  moviepicture2url  = ?,  moviepicture3url  = ?,  youtubeurl  = ? WHERE  id  = ? AND (( moviename  = ?) OR ( moviename  IS NULL AND ? IS NULL)) AND (( movieid  = ?) OR ( movieid  IS NULL AND ? IS NULL)) AND (( timesone  = ?) OR ( timesone  IS NULL AND ? IS NULL)) AND (( timestwo  = ?) OR ( timestwo  IS NULL AND ? IS NULL)) AND (( timesthree  = ?) OR ( timesthree  IS NULL AND ? IS NULL)) AND (( timesfour  = ?) OR ( timesfour  IS NULL AND ? IS NULL)) AND (( timesfive  = ?) OR ( timesfive  IS NULL AND ? IS NULL)) AND (( timessix  = ?) OR ( timessix  IS NULL AND ? IS NULL)) AND (( timesseven  = ?) OR ( timesseven  IS NULL AND ? IS NULL)) AND (( moviepicture1url  = ?) OR ( moviepicture1url  IS NULL AND ? IS NULL)) AND (( moviepicture2url  = ?) OR ( moviepicture2url  IS NULL AND ? IS NULL)) AND (( moviepicture3url  = ?) OR ( moviepicture3url  IS NULL AND ? IS NULL)) AND (( youtubeurl  = ?) OR ( youtubeurl  IS NULL AND ? IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_timesone" Type="String" />
			<asp:Parameter Name="original_timesone" Type="String" />
			<asp:Parameter Name="original_timestwo" Type="String" />
			<asp:Parameter Name="original_timestwo" Type="String" />
			<asp:Parameter Name="original_timesthree" Type="String" />
			<asp:Parameter Name="original_timesthree" Type="String" />
			<asp:Parameter Name="original_timesfour" Type="String" />
			<asp:Parameter Name="original_timesfour" Type="String" />
			<asp:Parameter Name="original_timesfive" Type="String" />
		    <asp:Parameter Name="original_timesfive" Type="String" />
            <asp:Parameter Name="original_timessix" Type="String" />
            <asp:Parameter Name="original_timessix" Type="String" />
            <asp:Parameter Name="original_timesseven" Type="String" />
            <asp:Parameter Name="original_timesseven" Type="String" />
            <asp:Parameter Name="original_moviepicture1url" Type="String" />
            <asp:Parameter Name="original_moviepicture1url" Type="String" />
            <asp:Parameter Name="original_moviepicture2url" Type="String" />
            <asp:Parameter Name="original_moviepicture2url" Type="String" />
            <asp:Parameter Name="original_moviepicture3url" Type="String" />
            <asp:Parameter Name="original_moviepicture3url" Type="String" />
            <asp:Parameter Name="original_youtubeurl" Type="String" />
            <asp:Parameter Name="original_youtubeurl" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="id" Type="Int32" />
			<asp:Parameter Name="moviename" Type="String" />
			<asp:Parameter Name="movieid" Type="Int32" />
			<asp:Parameter Name="timesone" Type="String" />
			<asp:Parameter Name="timestwo" Type="String" />
			<asp:Parameter Name="timesthree" Type="String" />
			<asp:Parameter Name="timesfour" Type="String" />
			<asp:Parameter Name="timesfive" Type="String" />
			<asp:Parameter Name="timessix" Type="String" />
			<asp:Parameter Name="timesseven" Type="String" />
			<asp:Parameter Name="moviepicture1url" Type="String" />
			<asp:Parameter Name="moviepicture2url" Type="String" />
			<asp:Parameter Name="moviepicture3url" Type="String" />
		    <asp:Parameter Name="youtubeurl" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="moviename" Type="String" />
			<asp:Parameter Name="movieid" Type="Int32" />
			<asp:Parameter Name="timesone" Type="String" />
			<asp:Parameter Name="timestwo" Type="String" />
			<asp:Parameter Name="timesthree" Type="String" />
			<asp:Parameter Name="timesfour" Type="String" />
			<asp:Parameter Name="timesfive" Type="String" />
			<asp:Parameter Name="timessix" Type="String" />
			<asp:Parameter Name="timesseven" Type="String" />
			<asp:Parameter Name="moviepicture1url" Type="String" />
			<asp:Parameter Name="moviepicture2url" Type="String" />
			<asp:Parameter Name="moviepicture3url" Type="String" />
			<asp:Parameter Name="youtubeurl" Type="String" />
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_timesone" Type="String" />
			<asp:Parameter Name="original_timesone" Type="String" />
			<asp:Parameter Name="original_timestwo" Type="String" />
			<asp:Parameter Name="original_timestwo" Type="String" />
			<asp:Parameter Name="original_timesthree" Type="String" />
			<asp:Parameter Name="original_timesthree" Type="String" />
			<asp:Parameter Name="original_timesfour" Type="String" />
			<asp:Parameter Name="original_timesfour" Type="String" />
			<asp:Parameter Name="original_timesfive" Type="String" />
		    <asp:Parameter Name="original_timesfive" Type="String" />
            <asp:Parameter Name="original_timessix" Type="String" />
            <asp:Parameter Name="original_timessix" Type="String" />
            <asp:Parameter Name="original_timesseven" Type="String" />
            <asp:Parameter Name="original_timesseven" Type="String" />
            <asp:Parameter Name="original_moviepicture1url" Type="String" />
            <asp:Parameter Name="original_moviepicture1url" Type="String" />
            <asp:Parameter Name="original_moviepicture2url" Type="String" />
            <asp:Parameter Name="original_moviepicture2url" Type="String" />
            <asp:Parameter Name="original_moviepicture3url" Type="String" />
            <asp:Parameter Name="original_moviepicture3url" Type="String" />
            <asp:Parameter Name="original_youtubeurl" Type="String" />
            <asp:Parameter Name="original_youtubeurl" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" EnableModelValidation="True">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="moviename" HeaderText="moviename" SortExpression="moviename">
			</asp:BoundField>
			<asp:BoundField DataField="movieid" HeaderText="movieid" SortExpression="movieid">
			</asp:BoundField>
			<asp:BoundField DataField="timesone" HeaderText="timesone" SortExpression="timesone">
			</asp:BoundField>
			<asp:BoundField DataField="timestwo" HeaderText="timestwo" SortExpression="timestwo">
			</asp:BoundField>
			<asp:BoundField DataField="timesthree" HeaderText="timesthree" SortExpression="timesthree">
			</asp:BoundField>
			<asp:BoundField DataField="timesfour" HeaderText="timesfour" SortExpression="timesfour">
			</asp:BoundField>
			<asp:BoundField DataField="timesfive" HeaderText="timesfive" SortExpression="timesfive">
			</asp:BoundField>
			<asp:BoundField DataField="timessix" HeaderText="timessix" SortExpression="timessix">
			</asp:BoundField>
			<asp:BoundField DataField="timesseven" HeaderText="timesseven" SortExpression="timesseven">
			</asp:BoundField>
			<asp:BoundField DataField="moviepicture1url" HeaderText="moviepicture1url" SortExpression="moviepicture1url">
			</asp:BoundField>
			<asp:BoundField DataField="moviepicture2url" HeaderText="moviepicture2url" SortExpression="moviepicture2url">
			</asp:BoundField>
			<asp:BoundField DataField="moviepicture3url" HeaderText="moviepicture3url" SortExpression="moviepicture3url">
			</asp:BoundField>
			<asp:BoundField DataField="youtubeurl" HeaderText="youtubeurl" SortExpression="youtubeurl">
			</asp:BoundField>
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx">Return to Movie Console</a>
</div>
</body>
</html>