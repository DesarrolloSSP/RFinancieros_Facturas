<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ImportarDatosSiafev.aspx.cs" Inherits="RFinancieros_Facturas.Importar.ImportarDatosSiafev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        body {
            font-family: Segoe UI;
            margin: 30px;
        }

        .titulo {
            font-size: 24px;
            font-weight: bold;
        }
    </style>



<div class="container-fluid">

    <div class="row">
        <div class="col-md-12">

            <!-- TÍTULO -->
            <div class="titulo-importacion">
                <span class="glyphicon glyphicon-import"></span>
                Importar Datos SIAFEV
            </div>

            <div class="subtitulo-importacion">
                Seleccione el archivo de Excel para consultar e importar los datos.
            </div>

            <hr />

            <!-- ÁREA DE ARCHIVO Y BOTONES -->
            <div class="panel panel-default">

                <div class="panel-body">

                    <div class="row">

                        <!-- ARCHIVO -->
                        <div class="col-md-5">

                            <div class="form-group">

                                <label class="control-label">
                                    Archivo de Excel
                                </label>

                                <asp:FileUpload
                                    ID="fuExcel"
                                    runat="server"
                                    CssClass="form-control" accept=".xlsx"  />

                            </div>

                        </div>

                        <!-- BOTONES -->
                        <div class="col-md-7">

                            <label class="control-label">
                                Acciones
                            </label>

                            <div>

                                <asp:Button
                                    ID="btnLeer"
                                    runat="server"
                                    Text=" Leer Excel"
                                    OnClick="btnLeer_Click"
                                    CssClass="btn btn-warning btn-importacion" />

                                <asp:Button
                                    ID="btnGuardar"
                                    runat="server"
                                    Text=" Guardar en BD"
                                    Visible="false"
                                    OnClick="btnGuardar_Click"
                                    CssClass="btn btn-success btn-importacion" />

                            </div>

                        </div>

                    </div>

                    <!-- MENSAJE -->
                    <div class="row">
                        <div class="col-md-12">

                            <asp:Label
                                ID="lblMensaje"
                                runat="server"
                                CssClass="mensaje-importacion" />

                        </div>
                    </div>

                </div>

            </div>


            <!-- VISTA PREVIA -->
            <div class="vista-previa">

                <div class="vista-previa-header">
                    <span class="glyphicon glyphicon-list-alt"></span>
                    Vista previa de los datos
                </div>

                <div class="vista-previa-body">

                    <div class="table-responsive">

                        <asp:GridView
                            ID="gvDatos"
                            runat="server"
                            CssClass="table table-bordered table-striped table-hover tabla-excel"
                            AutoGenerateColumns="false">

                            <Columns>

                                <asp:BoundField
                                    HeaderText="Sector"
                                    DataField="SectorDependencia" />

                                <asp:BoundField
                                    HeaderText="Beneficiario"
                                    DataField="Beneficiario" />

                                <asp:BoundField
                                    HeaderText="No. Beneficiario"
                                    DataField="NumeroBeneficiario" />

                                <asp:BoundField
                                    HeaderText="Folio"
                                    DataField="Folio" />

                                <asp:BoundField
                                    HeaderText="Concepto"
                                    DataField="Concepto" />

                                <asp:BoundField
                                    HeaderText="Fecha"
                                    DataField="Fecha"
                                    DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:BoundField
                                    HeaderText="Monto"
                                    DataField="Monto"
                                    DataFormatString="{0:C2}"
                                    ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField
                                    HeaderText="Pagado"
                                    DataField="Pagado"
                                    DataFormatString="{0:C2}"
                                    ItemStyle-HorizontalAlign="Right" />

                                <asp:BoundField
                                    HeaderText="Fecha de Pago"
                                    DataField="FechaPago"
                                    DataFormatString="{0:dd/MM/yyyy}" />

                                <asp:BoundField
                                    HeaderText="OC"
                                    DataField="OC" />

                            </Columns>

                        </asp:GridView>

                    </div>

                </div>

            </div>

        </div>
    </div>

</div>


<style>

    /* TÍTULO */

    .titulo-importacion {
        font-size: 26px;
        font-weight: 600;
        color: #333;
        margin-top: 25px;
        margin-bottom: 5px;
    }

    .titulo-importacion .glyphicon {
        margin-right: 8px;
    }

    .subtitulo-importacion {
        color: #777;
        font-size: 14px;
        margin-bottom: 15px;
    }


    /* BOTONES */

    .btn-importacion {
        min-width: 130px;
        margin-right: 8px;
        margin-bottom: 5px;
    }


    /* MENSAJE */

    .mensaje-importacion {
        display: block;
        margin-top: 15px;
        font-size: 14px;
        font-weight: 500;
    }


    /* VISTA PREVIA */

    .vista-previa {
        margin-top: 25px;
        border: 1px solid #ddd;
        border-radius: 4px;
        background-color: #fff;
    }

    .vista-previa-header {
        background-color: #337ab7;
        color: #fff;
        padding: 12px 15px;
        font-size: 16px;
        font-weight: 600;
    }

    .vista-previa-header .glyphicon {
        margin-right: 7px;
    }

    .vista-previa-body {
        padding: 15px;
    }


    /* GRIDVIEW */

    .tabla-excel {
        margin-bottom: 0;
        font-size: 13px;
        white-space: nowrap;
    }

    .tabla-excel th {
        background-color: #f5f5f5;
        color: #333;
        font-weight: 600;
        text-align: center;
        vertical-align: middle !important;
        white-space: nowrap;
    }

    .tabla-excel td {
        vertical-align: middle !important;
    }

    .tabla-excel tbody tr:hover {
        background-color: #f5f9fc;
    }

</style>






</asp:Content>



