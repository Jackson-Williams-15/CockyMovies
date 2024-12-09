<%@ Page Language="C#" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">   
        Test Form<br />
</body>
</html>
<asp:SqlDataSource runat="server" id="SqlDataSource1" ConflictDetection="CompareAllValues" ConnectionString="User Id=cockysa;Host=localhost;Port=3307;Database=cm_db" DeleteCommand="DELETE FROM [products] WHERE [ProductID] = ? AND [ProductName] = ? AND [Price] = ?" InsertCommand="INSERT INTO [products] ([ProductID], [ProductName], [Price]) VALUES (?, ?, ?)" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:cm_dbConnectionString3.ProviderName %>" SelectCommand="SELECT * FROM [products]" UpdateCommand="UPDATE [products] SET [ProductName] = ?, [Price] = ? WHERE [ProductID] = ? AND [ProductName] = ? AND [Price] = ?">
    <DeleteParameters>
        <asp:Parameter Name="original_ProductID" Type="Int32" />
        <asp:Parameter Name="original_ProductName" Type="String" />
        <asp:Parameter Name="original_Price" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="ProductID" Type="Int32" />
        <asp:Parameter Name="ProductName" Type="String" />
        <asp:Parameter Name="Price" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="ProductName" Type="String" />
        <asp:Parameter Name="Price" Type="Decimal" />
        <asp:Parameter Name="original_ProductID" Type="Int32" />
        <asp:Parameter Name="original_ProductName" Type="String" />
        <asp:Parameter Name="original_Price" Type="Decimal" />
    </UpdateParameters>
    </asp:SqlDataSource>
	<asp:GridView id="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
        </Columns>
	</asp:GridView>
    </form>
