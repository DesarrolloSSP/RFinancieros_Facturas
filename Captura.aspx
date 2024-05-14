<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Captura.aspx.cs" Inherits="RFinancieros_Facturas.Captura" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet">
    <div class="container">
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="d-flex justify-content-start">
                    <div class="col-md-6 d-flex justify-content-md-start">
                        <div class="d-flex flex-row bd-highlight mb-3">
                            <div class="p-1 bd-highlight">
                                <div class="form-floating">
                                    <asp:TextBox ID="tbbuscar" runat="server" class="form-control" placeholder="Folio de la factura"></asp:TextBox>
                                    <label for="floatingInputGrid">Valor a buscar <i class="fas fa-search"></i></label>
                                </div>
                            </div>
                            <div class="p-1 bd-highlight">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="BtnBuscar_Click" />
                            </div>
                            <div class="p-1 bd-highlight">
                                <asp:CheckBox ID="chkEjercicio" runat="server" Text="Ejercicio 2023" Checked="True" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 d-flex justify-content-end">
                        <b>
                            <asp:Label ID="lblnumreg" runat="server" Text="Importe Total por Odp(): N/A" CssClass="col-11 form-control-sm text-end" ForeColor="#6D132D"></asp:Label></b>
                    </div>
                </div>
            </div>
        </div>
        <asp:GridView ID="gvfacturas" class="table table-bordered" Font-Size="Small" AllowPaging="True" runat="server" TabIndex="-1" AutoGenerateColumns="False"
            EmptyDataText="No existen datos de facturas" OnRowDataBound="Gvfacturas_RowDataBound" OnPageIndexChanged="Gvfacturas_PageIndexChanged" OnPageIndexChanging="Gvfacturas_PageIndexChanging" PageSize="5" DataKeyNames="Id,NoOdp">
            <PagerSettings PreviousPageText="Previa" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:RadioButton ID="rd1" GroupName="rdBox" AutoPostBack="true" runat="server" OnCheckedChanged="Rd1_CheckedChanged" />
                        <headerstyle horizontalalign="Center" forecolor="White" backcolor="#6D132D" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Folio" HeaderText="Folio Fiscal" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Importe" HeaderText="Importe" DataFormatString="{0:$0.00}" ReadOnly="True" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Rfce" HeaderText="RFC" ReadOnly="True" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Rsemisor" HeaderText="Razón Social" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaEmision" HeaderText="Fecha Emision" DataFormatString="{0:d}" ReadOnly="True" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaRevision" HeaderText="Fecha Revisión" DataFormatString="{0:d}" ReadOnly="True" HeaderStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Paccertifico" HeaderText="Pac" ReadOnly="True" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Estreg" HeaderText="Status" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Revisor" HeaderText="Rev" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Capturista" HeaderText="Cap" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="validador" HeaderText="Val" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" BackColor="#6D132D" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <HeaderStyle BackColor="#990033" />
            <PagerSettings PreviousPageText="Previa" />
            <PagerStyle HorizontalAlign="Left" />
        </asp:GridView>
        <asp:Panel ID="Panel1" runat="server" Visible="false" DefaultButton="btnguardar">


            <div class="row mt-2">

                <div class="d-flex justify-content-center">
                <%--<div class="col-xl">--%>

                    <asp:LinkButton ID="btnValidaSAT" runat="server" Text="" OnClientClick="callSAT();">VERIFICACIÓN DE COMPROBANTES FISCALES DIGITALES POR INTERNET</asp:LinkButton>

                </div>

            </div>



            <div class="row mt-2">

                <div class="col-xl">
                    <asp:Label ID="lbarea" runat="server" Text="Area que trámita" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlareas" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlareas_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

               <%-- <div class="col-xl">
                    <asp:Label ID="lbconcepto" runat="server" Text="Concepto" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlconcepto" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>--%>

                <div class="col-xl">
                    <asp:Label ID="lbtipopago" runat="server" Text="Tipo de Pago" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlTipopago" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlTipopago_SelectedIndexChanged">
                        <asp:ListItem Value="0">Pago directo</asp:ListItem>
                        <asp:ListItem Value="1">Comprobación de sujeto</asp:ListItem>
                        <asp:ListItem Value="2">Fondo revolvente</asp:ListItem>
                        <asp:ListItem Value="3">FASP</asp:ListItem>
                        <asp:ListItem Value="4">FOFISP</asp:ListItem>
                    </asp:DropDownList>
                </div>


            </div>

            <div class="row">

                <div class="col-2">
                    <asp:Label ID="Label2" runat="server" Text="Clave Partida" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="txtPartida" AutoPostBack="true" OnTextChanged="txtPartida_TextChanged" runat="server" CssClass=" form-control"></asp:TextBox>
                </div>

                <div class="col-xl">
                    <asp:Label ID="Label1" runat="server" Text="Partida" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlPartida" runat="server" Enabled="false" CssClass="form-control" DataSourceID="edsPartida" DataTextField="partida" DataValueField="idtcpartida" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>

            <div class="row">
                <div class="col-xl">
                    <asp:Label ID="lbop" runat="server" Text="Número Orden de pago" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbordpag" runat="server" CssClass=" form-control"></asp:TextBox>
                </div>
                <div class="col-xl">
                    <asp:Label ID="lbfolint" runat="server" Text="Folio Interno factura" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbfolint" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-xl">
                    <asp:Label ID="lbcp" runat="server" Text="Código Postal" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbcp" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-xl">
                    <asp:Label ID="lbdevol" runat="server" Text="¿Se devuelve la factura?" class="small" Font-Bold="True"></asp:Label>
                    <asp:CheckBox ID="chkdevol" runat="server" CssClass="form-control" Checked="false" CausesValidation="true" OnCheckedChanged="Chkdevol_CheckedChanged" AutoPostBack="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-xl">
                    <asp:Label ID="lbmotivo" runat="server" Text="Motivo de la devolución" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbMotivo" runat="server" CssClass="form-control" Enabled="false" MaxLength="300" CharacterCasing="Upper"></asp:TextBox>
                </div>
                <div class="col-xl">
                    <asp:Label ID="lbfecdev" runat="server" Text="Fecha de Devolución" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbfecdev" runat="server" CssClass="form-control" Enabled="False" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-xl">
                    <asp:Label ID="lbestatus" runat="server" Text="Estatus" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlestatus" runat="server" CssClass="form-control" DataSourceID="edsstatus" DataTextField="nombre" DataValueField="nombre">
                    </asp:DropDownList>
                    <asp:EntityDataSource runat="server" ID="edsstatus" DefaultContainerName="dbControlDocumentosEntities" ConnectionString="name=dbControlDocumentosEntities" EnableFlattening="False" EntitySetName="tcstatus"></asp:EntityDataSource>
                </div>
            </div>
            <div class="row">
                <div class="col-xl">
                    <asp:Label ID="lblfolsuj" runat="server" Text="Folio del sujeto" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbfolsuj" runat="server" CssClass="form-control" Enabled="False" TextMode="Number" MaxLength="6"></asp:TextBox>
                </div>


<%--                <div class="col-xl">
                    <asp:Label ID="Label3" runat="server" Text="Terceros Institucionales" class="small" Font-Bold="True"></asp:Label>
                    <asp:DropDownList ID="ddlTercerosInst" runat="server" CssClass="form-control" DataSourceID="edsTercerosInst" DataTextField="nombre_tercero" DataValueField="idtcterceros_institucional" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                    </asp:DropDownList>
                </div>--%>


            </div>
            <br />
            <asp:Button ID="btnguardar" runat="server" Text="Revisado" CssClass="btn btn-warning" OnClick="Btnguardar_Click" />
            <asp:Button ID="btnCaptura" runat="server" Text="Enviar a Captura" CssClass="btn btn-warning" OnClick="BtnCaptura_Click" Enabled="false" />
            <asp:Button ID="btnReingreso" runat="server" Text="Reingreso" CssClass="btn btn-warning" Enabled="false" OnClick="BtnReingreso_Click" />
        </asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <asp:GridView ID="gvcapturistas" class="table table-bordered" Font-Size="Small" AllowPaging="True" runat="server" TabIndex="-1" AutoGenerateColumns="False"
                EmptyDataText="No existen datos" OnRowDataBound="Gvcapturistas_RowDataBound" OnPageIndexChanging="gvcapturistas_PageIndexChanging">
                <PagerSettings PreviousPageText="Previa" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rd2" runat="server" AutoPostBack="true" GroupName="rdBox" OnCheckedChanged="Rd2_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nombre" HeaderText="nombre" ReadOnly="True">
                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" BackColor="#6D132D" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="iniciales" HeaderStyle-HorizontalAlign="Center" HeaderText="iniciales" ReadOnly="True">
                        <HeaderStyle ForeColor="White" HorizontalAlign="Center" BackColor="#6D132D" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#6D132D" />
                <PagerSettings PreviousPageText="Previa" />
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
            <asp:Button ID="btnfinal" runat="server" Text="Guardar Registro" CssClass="btn btn-warning" OnClick="Btnfinal_Click" />
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" Visible="false">
            <div class="row">
                <div class="col-xl">
                    <asp:Label ID="lblEgreso" runat="server" Text="Egreso" class="small" Font-Bold="True"></asp:Label>
                    <asp:TextBox ID="tbEgreso" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                </div>
            </div>
            <br />
            <asp:Button ID="btnTermina" runat="server" Text="Guardar" CssClass="btn btn-warning" OnClick="BtnTermina_Click" />
        </asp:Panel>
    </div>

    <asp:EntityDataSource ID="edsPartida" runat="server" ConnectionString="name=dbFacturasFinancierosEntities" DefaultContainerName="dbFacturasFinancierosEntities" EnableFlattening="False" EnableUpdate="True" EntitySetName="tcpartida"></asp:EntityDataSource>
    <asp:EntityDataSource ID="edsTercerosInst" runat="server" ConnectionString="name=dbFacturasFinancierosEntities" DefaultContainerName="dbFacturasFinancierosEntities" EnableFlattening="False" EnableUpdate="True" EntitySetName="tctercero_institucional"></asp:EntityDataSource>



    <script>
        function callSAT() {
            
            window.open("https://verificacfdi.facturaelectronica.sat.gob.mx/", "Popup", "toolbar=no, location=no,status=yes,menubar=no,scrollbars=yes,resizable=no, width=900,height=500,left=430,top=100");

        }
    </script>


</asp:Content>

