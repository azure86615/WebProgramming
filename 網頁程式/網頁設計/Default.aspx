<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="網頁設計.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: 標楷體;
            font-size: xx-large;
            color: #9933FF;
            margin-left: 360px;
            margin-bottom: 15px;
        }
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 368px;
        }
        .style4
        {
            width: 48px;
        }
    </style>
</head>
<body background="bg2.png" >
    <form id="form1" runat="server">
    <div class="style1">
    
        從冰開始的飲茶坊</div>
    <table class="style2">
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                帳號：</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                密碼：</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="登入" Width="67px" 
                    onclick="Button1_Click" />
&nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                    PostBackUrl="~/store.aspx" Visible="False">進入茶坊</asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
        DataSourceID="SqlDataSource1" EmptyDataText="帳號或密碼錯誤，請重新輸入" Height="50px" 
        Visible="False" Width="125px">
        <Fields>
            <asp:BoundField DataField="user_name" HeaderText="user_name" 
                SortExpression="user_name" />
            <asp:BoundField DataField="user_money" HeaderText="user_money" 
                SortExpression="user_money" />
        </Fields>
    </asp:DetailsView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT [user_name], [user_money] FROM [user1] WHERE (([user_name] = @user_name) AND ([user_phone] = @user_phone))">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" Name="user_name" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="TextBox2" Name="user_phone" 
                PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
