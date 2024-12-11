<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CargarXml.aspx.cs" Inherits="RFinancieros_Facturas.Xml.CargarXml" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        .TableHeader {
            background-color: #6D132D !important;
            color: white !important;
            text-align: center
        }
    </style>


    <div class="container-fluid">

        <div class="row mt-5">

            <div class="col-6">
                <asp:FileUpload ID="FileUploadControl" runat="server" AllowMultiple="true" />
            </div>

            <div class="col-6">
                <asp:Button ID="UploadButton" runat="server" Text="Ingresar Archivos Xml" OnClick="UploadButton_Click" />
            </div>

        </div>


        <div class="row mt-5">

            <div class="col-auto">
                <asp:Label ID="lblTotal" runat="server" Text="" CssClass="btn btn-warning"></asp:Label>
            </div>

        </div>




        <asp:GridView ID="gvDatos" runat="server" AutoGenerateColumns="false" PageSize="20" AllowPaging="true" CssClass="table table-sm table-borderless table-bordered table-hover">
            <Columns>
                <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="TableHeader" ItemStyle-CssClass="text-center">
                    <ItemTemplate>
                        <div style="font-size: 20px; color: black; font: bold">
                            <%# Container.DataItemIndex + 1%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Folio" HeaderText="FOLIO" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="EmisorNombre" HeaderText="EMISOR" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ReceptorNombre" HeaderText="RECEPTOR" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Cantidad" HeaderText="CANTIDAD" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Importe" HeaderText="IMPORTE" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Descripcion" HeaderText="DESCRIPCIÓN" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="SubTotal" HeaderText="SUB TOTAL" HeaderStyle-CssClass="TableHeader" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>

    </div>

    

    <br />
    <br />
    <br />


    <%--<asp:Label ID="StatusLabel" runat="server" Text="" />--%>
</asp:Content>
