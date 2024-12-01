@page 
@model YourNamespace.UsersModel
<html>
    <head>
        <styles></styles>
    </head>
    <body>
        <asp:Menu>This will display an API Menu</asp:Menu>
         <form id="MyForm" runat="server">
            <asp:textbox id="MyTextBox" text="Hello World" runat="server"></asp:textbox>
            <asp:button id="MyButton" text="Echo Input" Onclick="MyButton_Click" runat="server"></asp:button>
            <asp:label id="MyLabel" runat="server" />
        </form>
    </body>
    <footer>
        <span align=center></span>Copyright CockyEntertainment - 2020-2025</span>

    </footer>

</html>