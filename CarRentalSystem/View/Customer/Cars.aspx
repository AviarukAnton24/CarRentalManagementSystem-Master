<%@ Page Title="" Language="C#" MasterPageFile="~/View/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="CarRentalManagementSystem.View.Customer.Cars" %>
<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4 ml-5"><Image src="../../Assets/Image/YelowFerrari.png" /></div>
            <div class="col-md-4"></div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-8 text-center">
                <asp:GridView runat="server" ID="Carlist" Class="table table-hover" AutoGenerateSelectButton="True" PageSize="10" OnSelectedIndexChanged="Carlist_SelectedIndexChanged">
                    <AlternatingRowStyle Backcolor="#FFCC00" ForeColor="White" />
                </asp:GridView>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-4">
                <div class="mb-3 mx-auto">
                    <input type="date" class="form-control" id="ReturnDateField" runat="server" required="required" />
                </div>
            </div>
            <div class="col-4">
                <div class="form-group d-grid mx-auto">
                    <label id="InfoMessage" runat="server" class="text-danger"></label>
                    <asp:Button ID="BookButton" CssClass="btn btn-warning btn-block" Text="Book" runat="server" OnClick="BookButton_Click" />
                </div>
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

