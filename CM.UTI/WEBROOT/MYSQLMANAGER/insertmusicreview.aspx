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
		DRIVE<br>INSERT A NEW MUSIC REVIEW<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="Data Source=TEAM4LAB2\COCKYMSSQL;Initial Catalog=VELOCITY;User ID=sa;Password=*Columbia1" DeleteCommand="DELETE FROM [velo_users_musicreviews] WHERE [sequenceid] = @original_sequenceid AND (([uid] = @original_uid) OR ([uid] IS NULL AND @original_uid IS NULL)) AND (([musicid] = @original_musicid) OR ([musicid] IS NULL AND @original_musicid IS NULL)) AND (([musicname] = @original_musicname) OR ([musicname] IS NULL AND @original_musicname IS NULL)) AND (([musicstars] = @original_musicstars) OR ([musicstars] IS NULL AND @original_musicstars IS NULL)) AND (([fullreview] = @original_fullreview) OR ([fullreview] IS NULL AND @original_fullreview IS NULL)) AND (([musictype] = @original_musictype) OR ([musictype] IS NULL AND @original_musictype IS NULL)) AND (([musicgeneration] = @original_musicgeneration) OR ([musicgeneration] IS NULL AND @original_musicgeneration IS NULL)) AND (([authphone] = @original_authphone) OR ([authphone] IS NULL AND @original_authphone IS NULL)) AND (([authemail] = @original_authemail) OR ([authemail] IS NULL AND @original_authemail IS NULL)) AND (([authfax] = @original_authfax) OR ([authfax] IS NULL AND @original_authfax IS NULL)) AND (([malestarid] = @original_malestarid) OR ([malestarid] IS NULL AND @original_malestarid IS NULL)) AND (([femalestarid] = @original_femalestarid) OR ([femalestarid] IS NULL AND @original_femalestarid IS NULL)) AND (([reviewtime] = @original_reviewtime) OR ([reviewtime] IS NULL AND @original_reviewtime IS NULL)) AND (([closed] = @original_closed) OR ([closed] IS NULL AND @original_closed IS NULL)) AND (([totalhits] = @original_totalhits) OR ([totalhits] IS NULL AND @original_totalhits IS NULL)) AND (([playlist] = @original_playlist) OR ([playlist] IS NULL AND @original_playlist IS NULL))" InsertCommand="INSERT INTO [velo_users_musicreviews] ([uid], [musicid], [musicname], [musicstars], [fullreview], [musictype], [musicgeneration], [authphone], [authemail], [authfax], [malestarid], [femalestarid], [reviewtime], [closed], [totalhits], [playlist]) VALUES (@uid, @musicid, @musicname, @musicstars, @fullreview, @musictype, @musicgeneration, @authphone, @authemail, @authfax, @malestarid, @femalestarid, @reviewtime, @closed, @totalhits, @playlist)" OldValuesParameterFormatString="original_{0}" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [velo_users_musicreviews]" UpdateCommand="UPDATE [velo_users_musicreviews] SET [uid] = @uid, [musicid] = @musicid, [musicname] = @musicname, [musicstars] = @musicstars, [fullreview] = @fullreview, [musictype] = @musictype, [musicgeneration] = @musicgeneration, [authphone] = @authphone, [authemail] = @authemail, [authfax] = @authfax, [malestarid] = @malestarid, [femalestarid] = @femalestarid, [reviewtime] = @reviewtime, [closed] = @closed, [totalhits] = @totalhits, [playlist] = @playlist WHERE [sequenceid] = @original_sequenceid AND (([uid] = @original_uid) OR ([uid] IS NULL AND @original_uid IS NULL)) AND (([musicid] = @original_musicid) OR ([musicid] IS NULL AND @original_musicid IS NULL)) AND (([musicname] = @original_musicname) OR ([musicname] IS NULL AND @original_musicname IS NULL)) AND (([musicstars] = @original_musicstars) OR ([musicstars] IS NULL AND @original_musicstars IS NULL)) AND (([fullreview] = @original_fullreview) OR ([fullreview] IS NULL AND @original_fullreview IS NULL)) AND (([musictype] = @original_musictype) OR ([musictype] IS NULL AND @original_musictype IS NULL)) AND (([musicgeneration] = @original_musicgeneration) OR ([musicgeneration] IS NULL AND @original_musicgeneration IS NULL)) AND (([authphone] = @original_authphone) OR ([authphone] IS NULL AND @original_authphone IS NULL)) AND (([authemail] = @original_authemail) OR ([authemail] IS NULL AND @original_authemail IS NULL)) AND (([authfax] = @original_authfax) OR ([authfax] IS NULL AND @original_authfax IS NULL)) AND (([malestarid] = @original_malestarid) OR ([malestarid] IS NULL AND @original_malestarid IS NULL)) AND (([femalestarid] = @original_femalestarid) OR ([femalestarid] IS NULL AND @original_femalestarid IS NULL)) AND (([reviewtime] = @original_reviewtime) OR ([reviewtime] IS NULL AND @original_reviewtime IS NULL)) AND (([closed] = @original_closed) OR ([closed] IS NULL AND @original_closed IS NULL)) AND (([totalhits] = @original_totalhits) OR ([totalhits] IS NULL AND @original_totalhits IS NULL)) AND (([playlist] = @original_playlist) OR ([playlist] IS NULL AND @original_playlist IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_sequenceid" Type="Int32" />
			<asp:Parameter Name="original_uid" Type="Int32" />
			<asp:Parameter Name="original_musicid" Type="Int32" />
			<asp:Parameter Name="original_musicname" Type="String" />
			<asp:Parameter Name="original_musicstars" Type="Int32" />
			<asp:Parameter Name="original_fullreview" Type="String" />
			<asp:Parameter Name="original_musictype" Type="String" />
			<asp:Parameter Name="original_musicgeneration" Type="String" />
			<asp:Parameter Name="original_authphone" Type="String" />
			<asp:Parameter Name="original_authemail" Type="String" />
			<asp:Parameter Name="original_authfax" Type="String" />
			<asp:Parameter Name="original_malestarid" Type="Int32" />
			<asp:Parameter Name="original_femalestarid" Type="Int32" />
			<asp:Parameter Name="original_reviewtime" Type="String" />
			<asp:Parameter Name="original_closed" Type="Int32" />
			<asp:Parameter Name="original_totalhits" Type="Int32" />
			<asp:Parameter Name="original_playlist" Type="Byte" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="uid" Type="Int32" />
			<asp:Parameter Name="musicid" Type="Int32" />
			<asp:Parameter Name="musicname" Type="String" />
			<asp:Parameter Name="musicstars" Type="Int32" />
			<asp:Parameter Name="fullreview" Type="String" />
			<asp:Parameter Name="musictype" Type="String" />
			<asp:Parameter Name="musicgeneration" Type="String" />
			<asp:Parameter Name="authphone" Type="String" />
			<asp:Parameter Name="authemail" Type="String" />
			<asp:Parameter Name="authfax" Type="String" />
			<asp:Parameter Name="malestarid" Type="Int32" />
			<asp:Parameter Name="femalestarid" Type="Int32" />
			<asp:Parameter Name="reviewtime" Type="String" />
			<asp:Parameter Name="closed" Type="Int32" />
			<asp:Parameter Name="totalhits" Type="Int32" />
			<asp:Parameter Name="playlist" Type="Byte" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="uid" Type="Int32" />
			<asp:Parameter Name="musicid" Type="Int32" />
			<asp:Parameter Name="musicname" Type="String" />
			<asp:Parameter Name="musicstars" Type="Int32" />
			<asp:Parameter Name="fullreview" Type="String" />
			<asp:Parameter Name="musictype" Type="String" />
			<asp:Parameter Name="musicgeneration" Type="String" />
			<asp:Parameter Name="authphone" Type="String" />
			<asp:Parameter Name="authemail" Type="String" />
			<asp:Parameter Name="authfax" Type="String" />
			<asp:Parameter Name="malestarid" Type="Int32" />
			<asp:Parameter Name="femalestarid" Type="Int32" />
			<asp:Parameter Name="reviewtime" Type="String" />
			<asp:Parameter Name="closed" Type="Int32" />
			<asp:Parameter Name="totalhits" Type="Int32" />
			<asp:Parameter Name="playlist" Type="Byte" />
			<asp:Parameter Name="original_sequenceid" Type="Int32" />
			<asp:Parameter Name="original_uid" Type="Int32" />
			<asp:Parameter Name="original_musicid" Type="Int32" />
			<asp:Parameter Name="original_musicname" Type="String" />
			<asp:Parameter Name="original_musicstars" Type="Int32" />
			<asp:Parameter Name="original_fullreview" Type="String" />
			<asp:Parameter Name="original_musictype" Type="String" />
			<asp:Parameter Name="original_musicgeneration" Type="String" />
			<asp:Parameter Name="original_authphone" Type="String" />
			<asp:Parameter Name="original_authemail" Type="String" />
			<asp:Parameter Name="original_authfax" Type="String" />
			<asp:Parameter Name="original_malestarid" Type="Int32" />
			<asp:Parameter Name="original_femalestarid" Type="Int32" />
			<asp:Parameter Name="original_reviewtime" Type="String" />
			<asp:Parameter Name="original_closed" Type="Int32" />
			<asp:Parameter Name="original_totalhits" Type="Int32" />
			<asp:Parameter Name="original_playlist" Type="Byte" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p class="auto-style1">
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="sequenceid" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" horizontalalign="Center">
		<Fields>
			<asp:BoundField DataField="sequenceid" HeaderText="sequenceid" InsertVisible="False" ReadOnly="True" SortExpression="sequenceid">
			</asp:BoundField>
			<asp:BoundField DataField="uid" HeaderText="uid" SortExpression="uid">
			</asp:BoundField>
			<asp:BoundField DataField="musicid" HeaderText="musicid" SortExpression="musicid">
			</asp:BoundField>
			<asp:BoundField DataField="musicname" HeaderText="musicname" SortExpression="musicname">
			</asp:BoundField>
			<asp:BoundField DataField="musicstars" HeaderText="musicstars" SortExpression="musicstars">
			</asp:BoundField>
			<asp:BoundField DataField="fullreview" HeaderText="fullreview" SortExpression="fullreview">
			</asp:BoundField>
			<asp:BoundField DataField="musictype" HeaderText="musictype" SortExpression="musictype">
			</asp:BoundField>
			<asp:BoundField DataField="musicgeneration" HeaderText="musicgeneration" SortExpression="musicgeneration">
			</asp:BoundField>
			<asp:BoundField DataField="authphone" HeaderText="authphone" SortExpression="authphone">
			</asp:BoundField>
			<asp:BoundField DataField="authemail" HeaderText="authemail" SortExpression="authemail">
			</asp:BoundField>
			<asp:BoundField DataField="authfax" HeaderText="authfax" SortExpression="authfax">
			</asp:BoundField>
			<asp:BoundField DataField="malestarid" HeaderText="malestarid" SortExpression="malestarid">
			</asp:BoundField>
			<asp:BoundField DataField="femalestarid" HeaderText="femalestarid" SortExpression="femalestarid">
			</asp:BoundField>
			<asp:BoundField DataField="reviewtime" HeaderText="reviewtime" SortExpression="reviewtime">
			</asp:BoundField>
			<asp:BoundField DataField="closed" HeaderText="closed" SortExpression="closed">
			</asp:BoundField>
			<asp:BoundField DataField="totalhits" HeaderText="totalhits" SortExpression="totalhits">
			</asp:BoundField>
			<asp:BoundField DataField="playlist" HeaderText="playlist" SortExpression="playlist">
			</asp:BoundField>
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True">
			</asp:CommandField>
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx"><button>Return to Movie Console</button></a>
</div>
</body>
</html>