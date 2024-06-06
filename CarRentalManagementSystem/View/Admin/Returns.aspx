<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Returns.aspx.cs" Inherits="CarRentalManagementSystem.View.Admin.Returns" %>
<asp:Content ID="Content2" ContentPlaceHolderID="mybody" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center" style="height: 100vh;">
            <div class="col-md-8 text-center">
                <h2>Returned Cars</h2>
                <div class="table-responsive">
                    <asp:GridView runat="server" ID="Returnlist" CssClass="table table-hover">
                        <AlternatingRowStyle Backcolor="#FFCC00" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

