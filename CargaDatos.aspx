<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CargaDatos.aspx.cs" Inherits="RFinancieros_Facturas.CargaDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <div class="row">
            <asp:FileUpload CssClass="form-control" runat="server" ID="fuArchivo" AllowMultiple="true" type="file" date-min-file-count="1" />
        </div>
        <br />
        <div class="row">
            <asp:Button ID="BtnCargarDatos" runat="server" CssClass="btn btn-warning" Text="Cargar Datos del SAT" OnClick="BtnCargarDatos_Click"  />
        </div>
        <br />
        <div class="d-flex justify-content-center" runat="server" id="sppiner" style="visibility:hidden">
            <div class="spinner-border" role="status" >
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
        <%-- prueba --%>
        <asp:GridView ID="gvDatosExcel" runat="server" CssClass="table table-bordered" AutoGenerateColumn="true">
        </asp:GridView>
    </div>
    <script type="text/javascript">
        $(function () {
            $('[id*=spiiner]').hide();
            $('[id*=BtnCargarDatos]').on("click", function () {
                setTimeout(function () {
                    $('[id*=sppiner]').show();
                    const spinerr = document.getElementById('<%=sppiner.ClientID %>').style.visibility = "visible";
                }, 200);
            });
        });
    </script>
</asp:Content>
