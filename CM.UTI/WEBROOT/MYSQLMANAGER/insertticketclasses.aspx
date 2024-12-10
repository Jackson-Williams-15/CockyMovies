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
	<asp:SqlDataSource id="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:COCKYConnectionString %>" DeleteCommand="DELETE FROM [products] WHERE [id] = @original_id AND (([prodid] = @original_prodid) OR ([prodid] IS NULL AND @original_prodid IS NULL)) AND (([productdescr] = @original_productdescr) OR ([productdescr] IS NULL AND @original_productdescr IS NULL)) AND (([productmrc] = @original_productmrc) OR ([productmrc] IS NULL AND @original_productmrc IS NULL)) AND (([productnrc] = @original_productnrc) OR ([productnrc] IS NULL AND @original_productnrc IS NULL)) AND (([discount1] = @original_discount1) OR ([discount1] IS NULL AND @original_discount1 IS NULL)) AND (([discount2] = @original_discount2) OR ([discount2] IS NULL AND @original_discount2 IS NULL)) AND (([discount3] = @original_discount3) OR ([discount3] IS NULL AND @original_discount3 IS NULL)) AND (([discountid] = @original_discountid) OR ([discountid] IS NULL AND @original_discountid IS NULL))" InsertCommand="INSERT INTO [products] ([prodid], [productdescr], [productmrc], [productnrc], [discount1], [discount2], [discount3], [discountid]) VALUES (@prodid, @productdescr, @productmrc, @productnrc, @discount1, @discount2, @discount3, @discountid)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:COCKYConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [products]" UpdateCommand="UPDATE [products] SET [prodid] = @prodid, [productdescr] = @productdescr, [productmrc] = @productmrc, [productnrc] = @productnrc, [discount1] = @discount1, [discount2] = @discount2, [discount3] = @discount3, [discountid] = @discountid WHERE [id] = @original_id AND (([prodid] = @original_prodid) OR ([prodid] IS NULL AND @original_prodid IS NULL)) AND (([productdescr] = @original_productdescr) OR ([productdescr] IS NULL AND @original_productdescr IS NULL)) AND (([productmrc] = @original_productmrc) OR ([productmrc] IS NULL AND @original_productmrc IS NULL)) AND (([productnrc] = @original_productnrc) OR ([productnrc] IS NULL AND @original_productnrc IS NULL)) AND (([discount1] = @original_discount1) OR ([discount1] IS NULL AND @original_discount1 IS NULL)) AND (([discount2] = @original_discount2) OR ([discount2] IS NULL AND @original_discount2 IS NULL)) AND (([discount3] = @original_discount3) OR ([discount3] IS NULL AND @original_discount3 IS NULL)) AND (([discountid] = @original_discountid) OR ([discountid] IS NULL AND @original_discountid IS NULL))">
		<DeleteParameters>
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_prodid" Type="String" />
			<asp:Parameter Name="original_productdescr" Type="String" />
			<asp:Parameter Name="original_productmrc" Type="Double" />
			<asp:Parameter Name="original_productnrc" Type="Double" />
			<asp:Parameter Name="original_discount1" Type="Double" />
			<asp:Parameter Name="original_discount2" Type="Double" />
			<asp:Parameter Name="original_discount3" Type="Double" />
			<asp:Parameter Name="original_discountid" Type="String" />
		</DeleteParameters>
		<InsertParameters>
			<asp:Parameter Name="prodid" Type="String" />
			<asp:Parameter Name="productdescr" Type="String" />
			<asp:Parameter Name="productmrc" Type="Double" />
			<asp:Parameter Name="productnrc" Type="Double" />
			<asp:Parameter Name="discount1" Type="Double" />
			<asp:Parameter Name="discount2" Type="Double" />
			<asp:Parameter Name="discount3" Type="Double" />
			<asp:Parameter Name="discountid" Type="String" />
		</InsertParameters>
		<UpdateParameters>
			<asp:Parameter Name="prodid" Type="String" />
			<asp:Parameter Name="productdescr" Type="String" />
			<asp:Parameter Name="productmrc" Type="Double" />
			<asp:Parameter Name="productnrc" Type="Double" />
			<asp:Parameter Name="discount1" Type="Double" />
			<asp:Parameter Name="discount2" Type="Double" />
			<asp:Parameter Name="discount3" Type="Double" />
			<asp:Parameter Name="discountid" Type="String" />
			<asp:Parameter Name="original_id" Type="Int32" />
			<asp:Parameter Name="original_prodid" Type="String" />
			<asp:Parameter Name="original_productdescr" Type="String" />
			<asp:Parameter Name="original_productmrc" Type="Double" />
			<asp:Parameter Name="original_productnrc" Type="Double" />
			<asp:Parameter Name="original_discount1" Type="Double" />
			<asp:Parameter Name="original_discount2" Type="Double" />
			<asp:Parameter Name="original_discount3" Type="Double" />
			<asp:Parameter Name="original_discountid" Type="String" />
		</UpdateParameters>
	</asp:SqlDataSource>
	<p>
	<asp:DetailsView id="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="id" DataSourceID="SqlDataSource1" Height="50px" Width="622px" AllowPaging="True" EnableModelValidation="True" HorizontalAlign="Center">
		<Fields>
			<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id">
			</asp:BoundField>
			<asp:BoundField DataField="prodid" HeaderText="prodid" SortExpression="prodid">
			</asp:BoundField>
			<asp:BoundField DataField="productdescr" HeaderText="productdescr" SortExpression="productdescr">
			</asp:BoundField>
			<asp:BoundField DataField="productmrc" HeaderText="productmrc" SortExpression="productmrc">
			</asp:BoundField>
			<asp:BoundField DataField="productnrc" HeaderText="productnrc" SortExpression="productnrc">
			</asp:BoundField>
			<asp:BoundField DataField="discount1" HeaderText="discount1" SortExpression="discount1">
			</asp:BoundField>
			<asp:BoundField DataField="discount2" HeaderText="discount2" SortExpression="discount2">
			</asp:BoundField>
			<asp:BoundField DataField="discount3" HeaderText="discount3" SortExpression="discount3">
			</asp:BoundField>
			<asp:BoundField DataField="discountid" HeaderText="discountid" SortExpression="discountid">
			</asp:BoundField>
			<asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
		</Fields>
	</asp:DetailsView>
	</p>
</form>
<a href="managemovies.aspx"><button>Return to Movie Console</button></a>

</body>
</html>