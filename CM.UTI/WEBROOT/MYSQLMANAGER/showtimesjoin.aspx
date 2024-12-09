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
		<br><br><br><br>STORE 33 - MOVIE MANAGER - COLUMBIA, SC<br>2300 FORREST 
		DRIVE<br>UPDATE SHOWTIMES<br><br>
		<div class="auto-style1">
			<asp:GridView id="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateSelectButton="True" DataKeyNames="id" DataSourceID="SqlDataSource1" Width="1408px" AllowPaging="True">
				<Columns>
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
				</Columns>
			</asp:GridView>
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="Hostname=127.0.0.1;Database:VELOCITY;Username:cm_user;Password=CockyMoviePassword" DeleteCommand="DELETE FROM [velo_users_movies_times] WHERE [id] = @original_id AND (([moviename] = @original_moviename) OR ([moviename] IS NULL AND @original_moviename IS NULL)) AND (([movieid] = @original_movieid) OR ([movieid] IS NULL AND @original_movieid IS NULL)) AND (([timesone] = @original_timesone) OR ([timesone] IS NULL AND @original_timesone IS NULL)) AND (([timestwo] = @original_timestwo) OR ([timestwo] IS NULL AND @original_timestwo IS NULL)) AND (([timesthree] = @original_timesthree) OR ([timesthree] IS NULL AND @original_timesthree IS NULL)) AND (([timesfour] = @original_timesfour) OR ([timesfour] IS NULL AND @original_timesfour IS NULL)) AND (([timesfive] = @original_timesfive) OR ([timesfive] IS NULL AND @original_timesfive IS NULL)) AND (([timessix] = @original_timessix) OR ([timessix] IS NULL AND @original_timessix IS NULL)) AND (([timesseven] = @original_timesseven) OR ([timesseven] IS NULL AND @original_timesseven IS NULL)) AND (([moviepicture1url] = @original_moviepicture1url) OR ([moviepicture1url] IS NULL AND @original_moviepicture1url IS NULL)) AND (([moviepicture2url] = @original_moviepicture2url) OR ([moviepicture2url] IS NULL AND @original_moviepicture2url IS NULL)) AND (([moviepicture3url] = @original_moviepicture3url) OR ([moviepicture3url] IS NULL AND @original_moviepicture3url IS NULL)) AND (([youtubeurl] = @original_youtubeurl) OR ([youtubeurl] IS NULL AND @original_youtubeurl IS NULL))" InsertCommand="INSERT INTO [velo_users_movies_times] ([moviename], [movieid], [timesone], [timestwo], [timesthree], [timesfour], [timesfive], [timessix], [timesseven], [moviepicture1url], [moviepicture2url], [moviepicture3url], [youtubeurl]) VALUES (@moviename, @movieid, @timesone, @timestwo, @timesthree, @timesfour, @timesfive, @timessix, @timesseven, @moviepicture1url, @moviepicture2url, @moviepicture3url, @youtubeurl)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [velo_users_movies_times]" UpdateCommand="UPDATE [velo_users_movies_times] SET [moviename] = @moviename, [movieid] = @movieid, [timesone] = @timesone, [timestwo] = @timestwo, [timesthree] = @timesthree, [timesfour] = @timesfour, [timesfive] = @timesfive, [timessix] = @timessix, [timesseven] = @timesseven, [moviepicture1url] = @moviepicture1url, [moviepicture2url] = @moviepicture2url, [moviepicture3url] = @moviepicture3url, [youtubeurl] = @youtubeurl WHERE [id] = @original_id AND (([moviename] = @original_moviename) OR ([moviename] IS NULL AND @original_moviename IS NULL)) AND (([movieid] = @original_movieid) OR ([movieid] IS NULL AND @original_movieid IS NULL)) AND (([timesone] = @original_timesone) OR ([timesone] IS NULL AND @original_timesone IS NULL)) AND (([timestwo] = @original_timestwo) OR ([timestwo] IS NULL AND @original_timestwo IS NULL)) AND (([timesthree] = @original_timesthree) OR ([timesthree] IS NULL AND @original_timesthree IS NULL)) AND (([timesfour] = @original_timesfour) OR ([timesfour] IS NULL AND @original_timesfour IS NULL)) AND (([timesfive] = @original_timesfive) OR ([timesfive] IS NULL AND @original_timesfive IS NULL)) AND (([timessix] = @original_timessix) OR ([timessix] IS NULL AND @original_timessix IS NULL)) AND (([timesseven] = @original_timesseven) OR ([timesseven] IS NULL AND @original_timesseven IS NULL)) AND (([moviepicture1url] = @original_moviepicture1url) OR ([moviepicture1url] IS NULL AND @original_moviepicture1url IS NULL)) AND (([moviepicture2url] = @original_moviepicture2url) OR ([moviepicture2url] IS NULL AND @original_moviepicture2url IS NULL)) AND (([moviepicture3url] = @original_moviepicture3url) OR ([moviepicture3url] IS NULL AND @original_moviepicture3url IS NULL)) AND (([youtubeurl] = @original_youtubeurl) OR ([youtubeurl] IS NULL AND @original_youtubeurl IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_timesone" Type="String" />
			<asp:Parameter Name="original_timestwo" Type="String" />
			<asp:Parameter Name="original_timesthree" Type="String" />
			<asp:Parameter Name="original_timesfour" Type="String" />
			<asp:Parameter Name="original_timesfive" Type="String" />
			<asp:Parameter Name="original_timessix" Type="String" />
			<asp:Parameter Name="original_timesseven" Type="String" />
			<asp:Parameter Name="original_moviepicture1url" Type="String" />
			<asp:Parameter Name="original_moviepicture2url" Type="String" />
			<asp:Parameter Name="original_moviepicture3url" Type="String" />
			<asp:Parameter Name="original_youtubeurl" Type="String" />
		</DeleteParameters>
		<InsertParameters>
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
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_timesone" Type="String" />
			<asp:Parameter Name="original_timestwo" Type="String" />
			<asp:Parameter Name="original_timesthree" Type="String" />
			<asp:Parameter Name="original_timesfour" Type="String" />
			<asp:Parameter Name="original_timesfive" Type="String" />
			<asp:Parameter Name="original_timessix" Type="String" />
			<asp:Parameter Name="original_timesseven" Type="String" />
			<asp:Parameter Name="original_moviepicture1url" Type="String" />
			<asp:Parameter Name="original_moviepicture2url" Type="String" />
			<asp:Parameter Name="original_moviepicture3url" Type="String" />
			<asp:Parameter Name="original_youtubeurl" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>

<div><a href="insertshowtimes.aspx">Insert a New Showtime</a></div>

</form>

</body>
</html>