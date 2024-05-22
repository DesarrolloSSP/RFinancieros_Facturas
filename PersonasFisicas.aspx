<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" Async="true" UICulture="en" Culture="en-US" AutoEventWireup="true" CodeBehind="PersonasFisicas.aspx.cs" Inherits="RFinancieros_Facturas.PersonasFisicas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row">

                    <div class="col-3">
                        <asp:Label ID="lbrecibo" runat="server" Text="Sin folio Fiscal" class="small" Font-Bold="True"></asp:Label>
                        <asp:CheckBox ID="chkrecibo" runat="server" CssClass="form-control" Checked="false" AutoPostBack="true" CausesValidation="true" OnCheckedChanged="chkrecibo_CheckedChanged" />
                    </div>

                    <div class="col-3">
                        <asp:Label ID="Label3" runat="server" Text="Terceros Institucionales" class="small" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlTercerosInst" runat="server" AutoPostBack="true" CssClass="form-control" DataSourceID="edsTercerosInst" DataTextField="nombre_tercero" DataValueField="idtcterceros_institucional" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTercerosInst_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl">
                        <asp:Label ID="lbcadena" runat="server" Text="QR Factura" class="small" Font-Bold="True"></asp:Label>
                        <asp:TextBox ID="tbcadena" runat="server" CssClass=" form-control" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="col-xl">
                    <asp:Button ID="tbverificar" runat="server" Text="Validar" CssClass="btn btn-warning" OnClick="Tbverificar_Click" />
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-xl">
                                <asp:Label ID="lbfolio" runat="server" Text="Folio Fiscal" class="small" Font-Bold="True" autocomplete="off"></asp:Label>
                                <asp:TextBox ID="tbFolio" runat="server" CssClass=" form-control" MaxLength="36" CharacterCasing="Upper"></asp:TextBox>
                            </div>
                            <div class="col-xl">
                                <asp:Label ID="lbimporte" runat="server" Text="Importe" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbImporte" runat="server" CssClass=" form-control" MaxLength="30" formaTextMode="Number" DataFormatString="{0:$0.00}" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-xl">
                                <asp:Label ID="lbRFC" runat="server" Text="RFC" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbrfc" runat="server" CssClass=" form-control" MaxLength="13" CharacterCasing="Upper" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl">
                                <asp:Label ID="lbrazon" runat="server" Text="Razón Social" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbrazon" runat="server" CssClass=" form-control" MaxLength="300" CharacterCasing="Upper"></asp:TextBox>
                            </div>
                            <div class="col-xl">
                                <asp:Label ID="lbfechae" runat="server" Text="Fecha de Emisión" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbfechae" runat="server" CssClass=" form-control" TextMode="Date" required="true"></asp:TextBox>
                            </div>
                            <div class="col-xl">
                                <asp:Label ID="lbarea" runat="server" Text="Area que trámita" class="small" Font-Bold="True"></asp:Label>
                                <asp:DropDownList ID="ddlareas" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">

                            <div class="col-3">
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

                            <%--   <div class="col-xl">
                        <asp:Label ID="lbconcepto" runat="server" Text="Concepto" class="small" Font-Bold="True"></asp:Label>
                        <asp:DropDownList ID="ddlconcepto" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>--%>


                            <div class="col-xl">
                                <asp:Label ID="lbtipopago" runat="server" Text="Tipo de Pago" class="small" Font-Bold="True"></asp:Label>
                                <asp:DropDownList ID="ddlTipopago" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DdlTipopago_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Pago directo</asp:ListItem>
                                    <asp:ListItem Value="1">Comprobación de sujeto</asp:ListItem>
                                    <asp:ListItem Value="2">Fondo revolvente</asp:ListItem>
                                    <asp:ListItem Value="3">FASP</asp:ListItem>
                                    <asp:ListItem Value="4">FOFISP</asp:ListItem>
                                    <asp:ListItem Value="5">N/A</asp:ListItem>


                                </asp:DropDownList>
                            </div>

                            <div class="col-xl">
                                <asp:Label ID="lbop" runat="server" Text="Número de Oficio/ Tarjeta / No.Op" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbordpag" runat="server" CssClass=" form-control" MaxLength="30"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-xl">
                                <asp:Label ID="lbfolint" runat="server" Text="Folio Interno factura" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbfolint" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="col-xl">
                                <asp:Label ID="lbcp" runat="server" Text="Código Postal" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbcp" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            </div>
                            <div class="col-xl">
                                <asp:Label ID="lbdevol" runat="server" Text="¿Se devuelve la factura?" class="small" Font-Bold="True"></asp:Label>
                                <asp:CheckBox ID="chkdevol" runat="server" CssClass="form-control" Checked="false" AutoPostBack="true" CausesValidation="true" OnCheckedChanged="Chkdevol_CheckedChanged" />
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
                                <asp:DropDownList ID="ddlestatus" runat="server" CssClass="form-control">
                                    <asp:ListItem>Vigente</asp:ListItem>
                                    <asp:ListItem>Cancelado</asp:ListItem>
                                    <asp:ListItem>Devolucion</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl">
                                <asp:Label ID="lblfolsuj" runat="server" Text="Folio del sujeto" class="small" Font-Bold="True"></asp:Label>
                                <asp:TextBox ID="tbfolsuj" runat="server" CssClass="form-control" Enabled="False" TextMode="Number" MaxLength="6"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnguardar" runat="server" Text="Guardar" CssClass="btn btn-warning" OnClick="Btnguardar_Click" />
                    </div>
                    <div class="col-md-6">
                        <iframe src="#" id="ruta" width="600" visible="false" height="350" style="border: 1; zoom=80%" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade" runat="server"></iframe>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>




    </div>


    <asp:EntityDataSource ID="edsTercerosInst" runat="server" ConnectionString="name=dbFacturasFinancierosEntities" DefaultContainerName="dbFacturasFinancierosEntities" EnableFlattening="False" EnableUpdate="True" EntitySetName="tctercero_institucional"></asp:EntityDataSource>
    <asp:EntityDataSource ID="edsPartida" runat="server" ConnectionString="name=dbFacturasFinancierosEntities" DefaultContainerName="dbFacturasFinancierosEntities" EnableFlattening="False" EnableUpdate="True" EntitySetName="tcpartida"></asp:EntityDataSource>

</asp:Content>
