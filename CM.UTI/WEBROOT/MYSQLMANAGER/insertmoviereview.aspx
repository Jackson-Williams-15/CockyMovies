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
		<br><br><br><br>STORE 33 REVIEW MANAGER - COLUMBIA, SC<br>2300 FORREST 
		DRIVE<br>INSERT A NEW MOVIE REVIEW<br>
		<div class="auto-style1">
		</div>
	</div>
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cm_dbConnectionString2 %>" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString2.ProviderName %>" SelectCommand="SELECT * FROM  velo_users_musicreviews " ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM  velo_users_musicreviews  WHERE  id  = ? AND (( uid  = ?) OR ( uid  IS NULL AND ? IS NULL)) AND (( musicid  = ?) OR ( musicid  IS NULL AND ? IS NULL)) AND (( musicname  = ?) OR ( musicname  IS NULL AND ? IS NULL)) AND (( musicstars  = ?) OR ( musicstars  IS NULL AND ? IS NULL)) AND (( fullreview  = ?) OR ( fullreview  IS NULL AND ? IS NULL)) AND (( musictype  = ?) OR ( musictype  IS NULL AND ? IS NULL)) AND (( musicgeneration  = ?) OR ( musicgeneration  IS NULL AND ? IS NULL)) AND (( authphone  = ?) OR ( authphone  IS NULL AND ? IS NULL)) AND (( authemail  = ?) OR ( authemail  IS NULL AND ? IS NULL)) AND (( authfax  = ?) OR ( authfax  IS NULL AND ? IS NULL)) AND (( malestarid  = ?) OR ( malestarid  IS NULL AND ? IS NULL)) AND (( femalestarid  = ?) OR ( femalestarid  IS NULL AND ? IS NULL)) AND (( reviewtime  = ?) OR ( reviewtime  IS NULL AND ? IS NULL)) AND (( closed  = ?) OR ( closed  IS NULL AND ? IS NULL)) AND (( totalhits  = ?) OR ( totalhits  IS NULL AND ? IS NULL)) AND (( playlist  = ?) OR ( playlist  IS NULL AND ? IS NULL))" InsertCommand="INSERT INTO  velo_users_musicreviews  ( id ,  uid ,  musicid ,  musicname ,  musicstars ,  fullreview ,  musictype ,  musicgeneration ,  authphone ,  authemail ,  authfax ,  malestarid ,  femalestarid ,  reviewtime ,  closed ,  totalhits ,  playlist ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" UpdateCommand="UPDATE  velo_users_musicreviews  SET  uid  = ?,  musicid  = ?,  musicname  = ?,  musicstars  = ?,  fullreview  = ?,  musictype  = ?,  musicgeneration  = ?,  authphone  = ?,  authemail  = ?,  authfax  = ?,  malestarid  = ?,  femalestarid  = ?,  reviewtime  = ?,  closed  = ?,  totalhits  = ?,  playlist  = ? WHERE  id  = ? AND (( uid  = ?) OR ( uid  IS NULL AND ? IS NULL)) AND (( musicid  = ?) OR ( musicid  IS NULL AND ? IS NULL)) AND (( musicname  = ?) OR ( musicname  IS NULL AND ? IS NULL)) AND (( musicstars  = ?) OR ( musicstars  IS NULL AND ? IS NULL)) AND (( fullreview  = ?) OR ( fullreview  IS NULL AND ? IS NULL)) AND (( musictype  = ?) OR ( musictype  IS NULL AND ? IS NULL)) AND (( musicgeneration  = ?) OR ( musicgeneration  IS NULL AND ? IS NULL)) AND (( authphone  = ?) OR ( authphone  IS NULL AND ? IS NULL)) AND (( authemail  = ?) OR ( authemail  IS NULL AND ? IS NULL)) AND (( authfax  = ?) OR ( authfax  IS NULL AND ? IS NULL)) AND (( malestarid  = ?) OR ( malestarid  IS NULL AND ? IS NULL)) AND (( femalestarid  = ?) OR ( femalestarid  IS NULL AND ? IS NULL)) AND (( reviewtime  = ?) OR ( reviewtime  IS NULL AND ? IS NULL)) AND (( closed  = ?) OR ( closed  IS NULL AND ? IS NULL)) AND (( totalhits  = ?) OR ( totalhits  IS NULL AND ? IS NULL)) AND (( playlist  = ?) OR ( playlist  IS NULL AND ? IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_uid" Type="Int32" />
            <asp:Parameter Name="original_uid" Type="Int32" />
            <asp:Parameter Name="original_musicid" Type="Int32" />
            <asp:Parameter Name="original_musicid" Type="Int32" />
            <asp:Parameter Name="original_musicname" Type="String" />
            <asp:Parameter Name="original_musicname" Type="String" />
            <asp:Parameter Name="original_musicstars" Type="Int32" />
            <asp:Parameter Name="original_musicstars" Type="Int32" />
            <asp:Parameter Name="original_fullreview" Type="String" />
            <asp:Parameter Name="original_fullreview" Type="String" />
            <asp:Parameter Name="original_musictype" Type="String" />
            <asp:Parameter Name="original_musictype" Type="String" />
            <asp:Parameter Name="original_musicgeneration" Type="String" />
            <asp:Parameter Name="original_musicgeneration" Type="String" />
            <asp:Parameter Name="original_authphone" Type="String" />
            <asp:Parameter Name="original_authphone" Type="String" />
            <asp:Parameter Name="original_authemail" Type="String" />
            <asp:Parameter Name="original_authemail" Type="String" />
            <asp:Parameter Name="original_authfax" Type="String" />
            <asp:Parameter Name="original_authfax" Type="String" />
            <asp:Parameter Name="original_malestarid" Type="Int32" />
            <asp:Parameter Name="original_malestarid" Type="Int32" />
            <asp:Parameter Name="original_femalestarid" Type="Int32" />
            <asp:Parameter Name="original_femalestarid" Type="Int32" />
            <asp:Parameter Name="original_reviewtime" Type="String" />
            <asp:Parameter Name="original_reviewtime" Type="String" />
            <asp:Parameter Name="original_closed" Type="Int32" />
            <asp:Parameter Name="original_closed" Type="Int32" />
            <asp:Parameter Name="original_totalhits" Type="Int32" />
            <asp:Parameter Name="original_totalhits" Type="Int32" />
            <asp:Parameter Name="original_playlist" Type="Int16" />
            <asp:Parameter Name="original_playlist" Type="Int16" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="id" Type="Int32" />
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
            <asp:Parameter Name="playlist" Type="Int16" />
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
            <asp:Parameter Name="playlist" Type="Int16" />
            <asp:Parameter Name="original_id" Type="Int32" />
            <asp:Parameter Name="original_uid" Type="Int32" />
            <asp:Parameter Name="original_uid" Type="Int32" />
            <asp:Parameter Name="original_musicid" Type="Int32" />
            <asp:Parameter Name="original_musicid" Type="Int32" />
            <asp:Parameter Name="original_musicname" Type="String" />
            <asp:Parameter Name="original_musicname" Type="String" />
            <asp:Parameter Name="original_musicstars" Type="Int32" />
            <asp:Parameter Name="original_musicstars" Type="Int32" />
            <asp:Parameter Name="original_fullreview" Type="String" />
            <asp:Parameter Name="original_fullreview" Type="String" />
            <asp:Parameter Name="original_musictype" Type="String" />
            <asp:Parameter Name="original_musictype" Type="String" />
            <asp:Parameter Name="original_musicgeneration" Type="String" />
            <asp:Parameter Name="original_musicgeneration" Type="String" />
            <asp:Parameter Name="original_authphone" Type="String" />
            <asp:Parameter Name="original_authphone" Type="String" />
            <asp:Parameter Name="original_authemail" Type="String" />
            <asp:Parameter Name="original_authemail" Type="String" />
            <asp:Parameter Name="original_authfax" Type="String" />
            <asp:Parameter Name="original_authfax" Type="String" />
            <asp:Parameter Name="original_malestarid" Type="Int32" />
            <asp:Parameter Name="original_malestarid" Type="Int32" />
            <asp:Parameter Name="original_femalestarid" Type="Int32" />
            <asp:Parameter Name="original_femalestarid" Type="Int32" />
            <asp:Parameter Name="original_reviewtime" Type="String" />
            <asp:Parameter Name="original_reviewtime" Type="String" />
            <asp:Parameter Name="original_closed" Type="Int32" />
            <asp:Parameter Name="original_closed" Type="Int32" />
            <asp:Parameter Name="original_totalhits" Type="Int32" />
            <asp:Parameter Name="original_totalhits" Type="Int32" />
            <asp:Parameter Name="original_playlist" Type="Int16" />
            <asp:Parameter Name="original_playlist" Type="Int16" />
        </UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" horizontalalign="Center" AllowPaging="True" EnableModelValidation="True">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
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
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx"><button>Return to Movie Console</button></a>
</div>
</body>
</html>