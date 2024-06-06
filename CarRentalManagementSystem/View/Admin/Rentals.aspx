<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Rentals.aspx.cs" Inherits="CarRentalManagementSystem.View.Admin.Rentals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center" style="height: 100vh;">
            <div class="col-md-8 text-center">
                <h2>Manage Rental Cars</h2>
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="RentalList" CssClass="table table-hover" AutoGenerateSelectButton="True">
                        <AlternatingRowStyle Backcolor="#FFCC00" ForeColor="White" />
                    </asp:GridView>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="delay" class="form-label text-end">Delay</label>
                            <input type="text" class="form-control" id="DelayField" placeholder="Enter Delay" runat="server" required="required">
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="mb-3">
                            <label for="fine" class="form-label text-end">Fine</label>
                            <input type="text" class="form-control" id="FineField" placeholder="Enter Fine" runat="server" required="required">
                        </div>
                    </div>
                    <div class="form-group d-grid mx-auto">
                        <label id="InfoMessage" runat="server" class="text-danger"></label>
                        <asp:Button ID="ReturnButton" CssClass="btn btn-danger btn-block" Text="Return Car" runat="server" OnClick="ReturnButton_Click"/>
                    </div>
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
