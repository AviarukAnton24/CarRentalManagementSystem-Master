<%@ Page Title="" Language="C#" MasterPageFile="~/View/Customer/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="PendingRentals.aspx.cs" Inherits="CarRentalManagementSystem.View.Customer.PendingRentals" %>
<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center" style="height: 100vh;">
            <div class="col-md-8 text-center">
                <h2>Your Pending Rentals</h2>
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="Carlist" CssClass="table table-hover" OnSelectedIndexChanged="Carlist_SelectedIndexChanged">
                        <AlternatingRowStyle Backcolor="#FFCC00" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
