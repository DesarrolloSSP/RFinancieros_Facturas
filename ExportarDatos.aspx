<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ExportarDatos.aspx.cs" Inherits="RFinancieros_Facturas.ExportarDatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-xl">
                <asp:Label ID="lbfecini" runat="server" Text="Fecha de Inicio" class="small" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="tbfechaini" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="col-xl">
                <asp:Label ID="lbfecfin" runat="server" Text="Fecha Final" class="small" Font-Bold="True"></asp:Label>
                <asp:TextBox ID="tbfechafin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
        </div>
        <br />        
        <div class="row">            
            <asp:Button ID="BtnExportar" runat="server" CssClass="btn btn-warning" Text="Exportar datos a Excel" OnClick="BtnExportar_Click"/>
        </div>
         <asp:GridView ID="GridView1" runat="server" visible="true"></asp:GridView>
    </div>
</asp:Content>
