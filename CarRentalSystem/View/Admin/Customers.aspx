<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="CarRentalManagementSystem.View.Admin.Customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
    <div class="row">
        <div class="col-md-4">
            <div class="row justify-content-center align-items-center">
                <div class="col"></div>
                <div class="col">
                        <h3 class="text-danger pl-4"> Manage Customers</h3>
                    <Image src="../../Assets/Image/customer_picture.png"></Image></div>
                <div class="col"></div>
            </div>
            <div class="row">
                <div class="col">
                        <div class="mb-3">
                            <label for="licenceNumber" class="form-label">Customer Name</label>
                            <input type="text" class="form-control" id="NameField" placeHolder="Enter Customer's Name" runat="server">
                            <asp:Label ID="NameErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="brand" class="form-label">Customer EmailAddress</label>
                            <input type="text" class="form-control" id="EmailAddressField" placeHolder="Enter Customers's EmailAddress" runat="server">
                            <asp:Label ID="EmailAddressErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="model" class="form-label">Customer Phone</label>
                            <input type="text" class="form-control" id="PhoneField" placeHolder="Enter Phone" runat="server">
                            <asp:Label ID="PhoneErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="password" class="form-label">Customer Password</label>
                            <input type="text" class="form-control" id="PasswordField" placeHolder="Enter Password" runat="server">
                            <asp:Label ID="PasswordErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        
                        <asp:Label ID="SuccessMessageLabel" runat="server" Text="Car added successfully" 
                            Font-Names="Times New Roman" Font-Size="100pt" 
                            ForeColor="Black" Font-Bold="true" 
                            CssClass="message-label" 
                            Style="text-align: center; display: none;"></asp:Label>
                        <asp:Label ID="FailureMessageLabel" runat="server" Text="Failed to add car. Please try again." 
                            Font-Names="Times New Roman" Font-Size="100pt" 
                            ForeColor="Red" Font-Bold="true" 
                            CssClass="message-label" 
                            Style="text-align: center; display: none;"></asp:Label>
                        <asp:Button ID="EditButton" CssClass="btn btn-danger" Text="Edit" runat="server" OnClick="EditButton_Click"/>
                        <asp:Button ID="AddButton" CssClass="btn btn-danger" Text="Add" runat="server" OnClick="AddButton_Click"/>
                        <asp:Button ID="DeleteButton" CssClass="btn btn-danger" Text="Delete" runat="server" OnClick="DeleteButton_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <table class="table"></table>
                <h1>Customers List</h1>
                <asp:GridView runat="server" ID="Customerlist" Class="table table-hover" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Customerlist_SelectedIndexChanged">
                    <AlternatingRowStyle Backcolor="#FFCC00" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <!-- JavaScript код для всплывающего окна -->
     <script>
     function showMessage(message) {
         var popup = document.getElementById("popup");
         var popupText = document.getElementById("popup-text");
         popupText.innerText = message;
         popup.style.display = "block";
     }
     </script>
     <!-- HTML для всплывающего окна -->
    <div id="popup" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); width: 400px; height: 200px; background-color: azure; border: 1px solid black; text-align: center;">
     <p id="popup-text" style="margin-top: 50px;"></p>
     <button onclick="document.getElementById('popup').style.display = 'none';" style="position: absolute; left: 50%; bottom: 20px; transform: translateX(-50%);">OK</button>
   </div>
</asp:Content>
    