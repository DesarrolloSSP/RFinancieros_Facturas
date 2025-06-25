<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ReporteAuditoria.aspx.cs" Inherits="RFinancieros_Facturas.Reportes.ReporteAuditoria" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid">


        <div class="row">

            <div class="col-md-3">
                <h5>RFC</h5>
                <asp:TextBox ID="txtRfc" runat="server" CssClass="form-control"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRfc" runat="server" ErrorMessage="Ingrese RFC" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
            </div>


            <div class="col-md-3">
                <h5>Fecha inicio</h5>
                <asp:TextBox ID="txtFechaInicio" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFechaInicio" runat="server" ErrorMessage="Fecha inicio" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>


            <div class="col-md-3">
                <h5>Fecha fín</h5>
                <asp:TextBox ID="txtFechaFin" TextMode="Date" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFechaInicio" runat="server" ErrorMessage="Fecha fín" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>

            <div class="col-md-3">
                <h5>Estatus</h5>
                <asp:DropDownList ID="ddlEstatus" runat="server" DataSourceID="edsEstatus" DataTextField="estatus" DataValueField="idtcfactura_estatus" AppendDataBoundItems="true" CssClass="form-control">
                    <asp:ListItem Value="-1">--Seleccione--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlEstatus" runat="server" ErrorMessage="Estatus" InitialValue="-1" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
            </div>


        </div>



        <div class="row">
            <center>
                <div class="col-md-3">
                    <asp:Button ID="btnVerReporte" runat="server" Text="Ver Reporte" OnClick="btnVerReporte_Click" ValidationGroup="Save" />
                </div>

                <br />
                <div class="col-md-3">
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>

                </div>
            </center>

            <br />
            <br />
            <asp:EntityDataSource ID="edsEstatus" runat="server" ConnectionString="name=dbFacturasFinancierosEntities" DefaultContainerName="dbFacturasFinancierosEntities" EnableFlattening="False" EnableUpdate="True" EntitySetName="tcfactura_estatus"></asp:EntityDataSource>


            <%--<rsweb:ReportViewer ID="egreso" runat="server" Height="500" Width="500" ProcessingMode="Remote" AsyncRendering="false" KeepSessionAlive="true" SizeToReportContent="True" DocumentMapCollapsed="False" DocumentMapWidth="80%" ZoomMode="PageWidth"></rsweb:ReportViewer>--%>

            <rsweb:reportviewer
                id="egreso"
                runat="server"
                height="500"
                width="500"
                processingmode="Remote"
                asyncrendering="false"
                keepsessionalive="true"
                sizetoreportcontent="True"
                documentmapcollapsed="False"
                documentmapwidth="80%"
                zoommode="PageWidth">
            </rsweb:reportviewer>


        </div>

    </div>

</asp:Content>
