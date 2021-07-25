<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="myApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="rowDiv">
                <span style="width:15%">STUDENT ID</span>
                <span style="width:20%">NAME</span>
                <span style="width:20%">EMAIL</span> 
                <span style="width:20%">OPTIONS</span> 
            </div>
        <div id="viewList" runat="server">
            
        </div>
        <br />
        <hr />
        
        <div class="addAndUpdate">
            <asp:Label ID="sIDLabel" runat="server" Text="Student ID:"></asp:Label> 
            <asp:Label ID="nameLabel" runat="server" Text="Name:"></asp:Label>
            <asp:Label ID="emailLabel" runat="server" Text="Email:"></asp:Label>
            <br />
            <asp:TextBox ID="sIDTextBox" runat="server" ReadOnly="true" ></asp:TextBox>
            <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
            <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
            <asp:Button ID="addBtn" runat="server" Text="Add" OnClick="addBtn_Click" />
            <asp:Button ID="updateBtn" runat="server" Text="Update" OnClick="updateBtn_Click" />
        </div>

    </form>

    </body>
</html>

