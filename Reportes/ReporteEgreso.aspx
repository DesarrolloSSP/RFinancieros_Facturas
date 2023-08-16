<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ReporteEgreso.aspx.cs" Inherits="RFinancieros_Facturas.Reportes.ReporteEgreso" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"></asp:UpdatePanel>
        <rsweb:ReportViewer ID="egreso" runat="server" Height="500" Width="500" ProcessingMode="Remote" SizeToReportContent="True" DocumentMapCollapsed="False" DocumentMapWidth="80%" ZoomMode="PageWidth"></rsweb:ReportViewer>
    </div>
</asp:Content>

