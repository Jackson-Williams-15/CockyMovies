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
		DRIVE<br>INSERT A NEW MOVIE<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="Hostname=127.0.0.1;Database:VELOCITY;Username:cm_user;Password=CockyMoviePassword" DeleteCommand="DELETE FROM [velo_users_movies] WHERE [sequenceid] = @original_sequenceid AND (([moviename] = @original_moviename) OR ([moviename] IS NULL AND @original_moviename IS NULL)) AND (([movieid] = @original_movieid) OR ([movieid] IS NULL AND @original_movieid IS NULL)) AND (([moviegeneration] = @original_moviegeneration) OR ([moviegeneration] IS NULL AND @original_moviegeneration IS NULL)) AND (([movietype] = @original_movietype) OR ([movietype] IS NULL AND @original_movietype IS NULL)) AND (([maleleadid] = @original_maleleadid) OR ([maleleadid] IS NULL AND @original_maleleadid IS NULL)) AND (([femaleleadid] = @original_femaleleadid) OR ([femaleleadid] IS NULL AND @original_femaleleadid IS NULL)) AND (([moviepicture1url] = @original_moviepicture1url) OR ([moviepicture1url] IS NULL AND @original_moviepicture1url IS NULL)) AND (([moviepicture2url] = @original_moviepicture2url) OR ([moviepicture2url] IS NULL AND @original_moviepicture2url IS NULL)) AND (([moviepicture3url] = @original_moviepicture3url) OR ([moviepicture3url] IS NULL AND @original_moviepicture3url IS NULL)) AND (([youtubeurl] = @original_youtubeurl) OR ([youtubeurl] IS NULL AND @original_youtubeurl IS NULL))" InsertCommand="INSERT INTO [velo_users_movies] ([moviename], [movieid], [moviegeneration], [movietype], [maleleadid], [femaleleadid], [moviepicture1url], [moviepicture2url], [moviepicture3url], [youtubeurl]) VALUES (@moviename, @movieid, @moviegeneration, @movietype, @maleleadid, @femaleleadid, @moviepicture1url, @moviepicture2url, @moviepicture3url, @youtubeurl)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [velo_users_movies]" UpdateCommand="UPDATE [velo_users_movies] SET [moviename] = @moviename, [movieid] = @movieid, [moviegeneration] = @moviegeneration, [movietype] = @movietype, [maleleadid] = @maleleadid, [femaleleadid] = @femaleleadid, [moviepicture1url] = @moviepicture1url, [moviepicture2url] = @moviepicture2url, [moviepicture3url] = @moviepicture3url, [youtubeurl] = @youtubeurl WHERE [sequenceid] = @original_sequenceid AND (([moviename] = @original_moviename) OR ([moviename] IS NULL AND @original_moviename IS NULL)) AND (([movieid] = @original_movieid) OR ([movieid] IS NULL AND @original_movieid IS NULL)) AND (([moviegeneration] = @original_moviegeneration) OR ([moviegeneration] IS NULL AND @original_moviegeneration IS NULL)) AND (([movietype] = @original_movietype) OR ([movietype] IS NULL AND @original_movietype IS NULL)) AND (([maleleadid] = @original_maleleadid) OR ([maleleadid] IS NULL AND @original_maleleadid IS NULL)) AND (([femaleleadid] = @original_femaleleadid) OR ([femaleleadid] IS NULL AND @original_femaleleadid IS NULL)) AND (([moviepicture1url] = @original_moviepicture1url) OR ([moviepicture1url] IS NULL AND @original_moviepicture1url IS NULL)) AND (([moviepicture2url] = @original_moviepicture2url) OR ([moviepicture2url] IS NULL AND @original_moviepicture2url IS NULL)) AND (([moviepicture3url] = @original_moviepicture3url) OR ([moviepicture3url] IS NULL AND @original_moviepicture3url IS NULL)) AND (([youtubeurl] = @original_youtubeurl) OR ([youtubeurl] IS NULL AND @original_youtubeurl IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_sequenceid" Type="Int32" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_moviegeneration" Type="String" />
			<asp:Parameter Name="original_movietype" Type="String" />
			<asp:Parameter Name="original_maleleadid" Type="Int32" />
			<asp:Parameter Name="original_femaleleadid" Type="Int32" />
			<asp:Parameter Name="original_moviepicture1url" Type="String" />
			<asp:Parameter Name="original_moviepicture2url" Type="String" />
			<asp:Parameter Name="original_moviepicture3url" Type="String" />
			<asp:Parameter Name="original_youtubeurl" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="moviename" Type="String" />
			<asp:Parameter Name="movieid" Type="Int32" />
			<asp:Parameter Name="moviegeneration" Type="String" />
			<asp:Parameter Name="movietype" Type="String" />
			<asp:Parameter Name="maleleadid" Type="Int32" />
			<asp:Parameter Name="femaleleadid" Type="Int32" />
			<asp:Parameter Name="moviepicture1url" Type="String" />
			<asp:Parameter Name="moviepicture2url" Type="String" />
			<asp:Parameter Name="moviepicture3url" Type="String" />
			<asp:Parameter Name="youtubeurl" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="moviename" Type="String" />
			<asp:Parameter Name="movieid" Type="Int32" />
			<asp:Parameter Name="moviegeneration" Type="String" />
			<asp:Parameter Name="movietype" Type="String" />
			<asp:Parameter Name="maleleadid" Type="Int32" />
			<asp:Parameter Name="femaleleadid" Type="Int32" />
			<asp:Parameter Name="moviepicture1url" Type="String" />
			<asp:Parameter Name="moviepicture2url" Type="String" />
			<asp:Parameter Name="moviepicture3url" Type="String" />
			<asp:Parameter Name="youtubeurl" Type="String" />
			<asp:Parameter Name="original_sequenceid" Type="Int32" />
			<asp:Parameter Name="original_moviename" Type="String" />
			<asp:Parameter Name="original_movieid" Type="Int32" />
			<asp:Parameter Name="original_moviegeneration" Type="String" />
			<asp:Parameter Name="original_movietype" Type="String" />
			<asp:Parameter Name="original_maleleadid" Type="Int32" />
			<asp:Parameter Name="original_femaleleadid" Type="Int32" />
			<asp:Parameter Name="original_moviepicture1url" Type="String" />
			<asp:Parameter Name="original_moviepicture2url" Type="String" />
			<asp:Parameter Name="original_moviepicture3url" Type="String" />
			<asp:Parameter Name="original_youtubeurl" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="sequenceid" DataSourceID="SqlDataSource1" Height="50px" Width="622px">
		<Fields>
			<asp:BoundField DataField="sequenceid" HeaderText="sequenceid" InsertVisible="False" ReadOnly="True" SortExpression="sequenceid">
			</asp:BoundField>
			<asp:BoundField DataField="moviename" HeaderText="moviename" SortExpression="moviename">
			</asp:BoundField>
			<asp:BoundField DataField="movieid" HeaderText="movieid" SortExpression="movieid">
			</asp:BoundField>
			<asp:BoundField DataField="moviegeneration" HeaderText="moviegeneration" SortExpression="moviegeneration">
			</asp:BoundField>
			<asp:BoundField DataField="movietype" HeaderText="movietype" SortExpression="movietype">
			</asp:BoundField>
			<asp:BoundField DataField="maleleadid" HeaderText="maleleadid" SortExpression="maleleadid">
			</asp:BoundField>
			<asp:BoundField DataField="femaleleadid" HeaderText="femaleleadid" SortExpression="femaleleadid">
			</asp:BoundField>
			<asp:BoundField DataField="moviepicture1url" HeaderText="moviepicture1url" SortExpression="moviepicture1url">
			</asp:BoundField>
			<asp:BoundField DataField="moviepicture2url" HeaderText="moviepicture2url" SortExpression="moviepicture2url">
			</asp:BoundField>
			<asp:BoundField DataField="moviepicture3url" HeaderText="moviepicture3url" SortExpression="moviepicture3url">
			</asp:BoundField>
			<asp:BoundField DataField="youtubeurl" HeaderText="youtubeurl" SortExpression="youtubeurl">
			</asp:BoundField>
			<asp:CommandField ShowInsertButton="True">
			</asp:CommandField>
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx"><button>Return to Movie Console</button></a>

</body>
</html>