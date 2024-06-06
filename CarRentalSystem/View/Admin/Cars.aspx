<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="CarRentalManagementSystem.View.Admin.Cars" enableEventValidation="false"%>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.WebControls" Assembly="System.Web.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col"></div>
                    <div class="col">
                        <h3 class="text-danger pl-4"> Manage Cars</h3>
                        <asp:Image ID="CarImage" runat="server" ImageUrl="~/Assets/Image/car (1).png" />
                    </div>
                    <div class="col"></div>
                </div>
                <div class="row">
                    <div class="col">
                        <div class="mb-3">
                            <label for="licenceNumber" class="form-label">Licence Number</label>
                            <input type="text" class="form-control" id="NumberField" placeholder="Enter Plate Number" runat="server">
                            <asp:Label ID="NumberErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="brand" class="form-label">Brand</label>
                            <input type="text" class="form-control" id="BrandField" placeholder="Enter The Car's Brand" runat="server">
                            <asp:Label ID="BrandErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="model" class="form-label">Model</label>
                            <input type="text" class="form-control" id="ModelField" placeholder="Enter Model" runat="server">
                            <asp:Label ID="ModelErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="price" class="form-label">Price</label>
                            <input type="text" class="form-control" id="PriceField" placeholder="Enter Price" runat="server">
                            <asp:Label ID="PriceErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="color" class="form-label">Color</label>
                            <input type="text" class="form-control" id="ColorField" placeholder="Enter Color" runat="server">
                            <asp:Label ID="ColorErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label for="available" class="form-label">Available</label>
                            <asp:DropDownList ID="AvailableField" runat="server" class="form-control">
                                <asp:ListItem>Available</asp:ListItem>
                                <asp:ListItem>Booked</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Button ID="EditButton" CssClass="btn btn-danger" Text="Edit" runat="server" OnClick="EditButton_Click"/>
                        <asp:Button ID="AddButton" CssClass="btn btn-danger" Text="Add" runat="server" OnClick="AddButton_Click"/>
                        <asp:Button ID="DeleteButton" CssClass="btn btn-danger" Text="Delete" runat="server" OnClick="DeleteButton_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <table class="table"></table>
                <h1>Cars List</h1>
                <asp:GridView runat="server" ID="Carlist" Class="table table-hover" AutoGenerateSelectButton="True" OnSelectedIndexChanged="Carlist_SelectedIndexChanged">
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
