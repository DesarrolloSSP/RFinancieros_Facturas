<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ReporteEgresoFechas.aspx.cs" Inherits="RFinancieros_Facturas.Reportes.ReporteEgresoFechas" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="container-fluid">

        <div class="panel panel-default" style="margin-top: 20px;">

            <!-- ENCABEZADO -->
            <div class="panel-heading"
                style="font-size: 18px; font-weight: 600;">

                <span class="glyphicon glyphicon-search"></span>
                Consulta de Egresos

            </div>


            <div class="panel-body">

                <!-- FILTROS -->
                <div class="row">

                    <!-- RFC -->
                    <div class="col-md-3">

                        <div class="form-group">

                            <label for="<%= txtRfc.ClientID %>">
                                RFC
                            </label>

                            <asp:TextBox
                                ID="txtRfc"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Ingrese RFC">
                            </asp:TextBox>

                            <%--  <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator1"
                                ControlToValidate="txtRfc"
                                runat="server"
                                ErrorMessage="Ingrese RFC"
                                ForeColor="Red"
                                ValidationGroup="Save"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>--%>
                        </div>

                    </div>


                    <!-- FECHA INICIO -->
                    <div class="col-md-2">

                        <div class="form-group">

                            <label for="<%= txtFechaInicio.ClientID %>">
                                Fecha inicio
                            </label>

                            <asp:TextBox
                                ID="txtFechaInicio"
                                TextMode="Date"
                                runat="server"
                                CssClass="form-control">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2"
                                ControlToValidate="txtFechaInicio"
                                runat="server"
                                ErrorMessage="Seleccione la fecha de inicio"
                                ForeColor="Red"
                                ValidationGroup="Save"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>

                        </div>

                    </div>


                    <!-- FECHA FIN -->
                    <div class="col-md-2">

                        <div class="form-group">

                            <label for="<%= txtFechaFin.ClientID %>">
                                Fecha fin
                            </label>

                            <asp:TextBox
                                ID="txtFechaFin"
                                TextMode="Date"
                                runat="server"
                                CssClass="form-control">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator3"
                                ControlToValidate="txtFechaFin"
                                runat="server"
                                ErrorMessage="Seleccione la fecha fin"
                                ForeColor="Red"
                                ValidationGroup="Save"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>

                        </div>

                    </div>


                    <!-- ESTATUS -->
                    <div class="col-md-3">

                        <div class="form-group">

                            <label for="<%= ddlEstatus.ClientID %>">
                                Estatus
                            </label>

                            <asp:DropDownList
                                ID="ddlEstatus"
                                runat="server"
                                DataSourceID="edsEstatus"
                                DataTextField="estatus"
                                DataValueField="idtcfactura_estatus"
                                AppendDataBoundItems="true"
                                CssClass="form-control">

                                <asp:ListItem Value="-1">
                                -- Seleccione --
                                </asp:ListItem>

                            </asp:DropDownList>

                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4"
                                ControlToValidate="ddlEstatus"
                                runat="server"
                                ErrorMessage="Seleccione un estatus"
                                InitialValue="-1"
                                ForeColor="Red"
                                ValidationGroup="Save"
                                Display="Dynamic">
                            </asp:RequiredFieldValidator>

                        </div>

                    </div>


                    <!-- BOTÓN CONSULTAR -->
                    <div class="col-md-2">

                        <div class="form-group"
                            style="padding-top: 25px;">

                            <asp:Button
                                ID="btnConsultarDatos"
                                runat="server"
                                Text="Consultar"
                                OnClick="btnConsultarDatos_Click"
                                ValidationGroup="Save"
                                CssClass="btn btn-primary btn-block" />

                        </div>

                    </div>

                </div>


                <!-- MENSAJE -->
                <div class="row">

                    <div class="col-md-12">

                        <asp:Label
                            ID="lblError"
                            runat="server"
                            Text="">
                        </asp:Label>

                    </div>

                </div>

            </div>

        </div>


        <!-- ================================================= -->
        <!-- RESULTADOS -->
        <!-- ================================================= -->

        <br />
        <div class="panel panel-default">




            <div class="panel-body">

                <!-- GRIDVIEW -->

                <div id="PanelEgresos" runat="server" visible="false">

                    <div class="panel-heading"
                        style="font-size: 17px; font-weight: 600; display: flex; justify-content: center; align-items: center; gap: 8px;">

                        <span class="glyphicon glyphicon-list-alt"></span>
                        <span>Resumen de Egresos</span>

                    </div>
                    <br />


                    <div class="table-responsive">

                        <asp:GridView
                            ID="dgvResumen"
                            runat="server"
                            CssClass="table table-bordered table-striped table-hover"
                            AutoGenerateColumns="false">

                            <HeaderStyle
                                CssClass="active"
                                Font-Bold="true"
                                HorizontalAlign="Center" />

                            <Columns>

                                <asp:BoundField
                                    DataField="Area"
                                    HeaderText="Área" />

                                <asp:BoundField
                                    DataField="CantidadFacturas"
                                    HeaderText="Cantidad de Facturas"
                                    ItemStyle-HorizontalAlign="Center" />

                                <asp:BoundField
                                    DataField="TotalImporte"
                                    HeaderText="Total Importe"
                                    DataFormatString="{0:C2}"
                                    ItemStyle-HorizontalAlign="Right" />

                            </Columns>

                        </asp:GridView>



                    </div>

                    <div class="row"
                        style="margin-top: 15px;">

                        <div class="col-md-12">

                            <asp:LinkButton
                                ID="btnRptResumen"
                                runat="server"
                                OnClick="btnRptResumen_Click"
                                CssClass="btn btn-warning">

                    <span class="glyphicon glyphicon-download-alt"></span>
                    Descargar Resumen Egresos

                            </asp:LinkButton>

                            &nbsp;

               <asp:LinkButton
                   ID="btnRptGeneral"
                   runat="server"
                   OnClick="btnRptGeneral_Click"
                   ValidationGroup="Save"
                   CssClass="btn btn-warning"
                   Style="display: inline-flex; align-items: center; gap: 8px;">
<span class="glyphicon glyphicon-file"></span>
<span>Descargar Reporte general</span>

               </asp:LinkButton>

                        </div>

                    </div>

                </div>


                <div id="PanelEstatus" runat="server" visible="false">

                    <div class="panel-heading"
                        style="font-size: 17px; font-weight: 600; display: flex; justify-content: center; align-items: center; gap: 8px;">

                        <span class="glyphicon glyphicon-list-alt"></span>
                        <span>Estatus Facturas</span>

                    </div>
                    <br />

                    <div class="table-responsive">

                        <asp:GridView
                            ID="dgvEstatusFacturas"
                            runat="server"
                            CssClass="table table-bordered table-striped table-hover"
                            AutoGenerateColumns="false">

                            <HeaderStyle
                                CssClass="active"
                                Font-Bold="true"
                                HorizontalAlign="Center" />

                            <Columns>

                                <asp:BoundField
                                    DataField="estatus"
                                    HeaderText="Estatus DRF" ItemStyle-HorizontalAlign="Center" />

                                <asp:BoundField
                                    DataField="CantidadFacturas"
                                    HeaderText="Cantidad de Facturas"
                                    ItemStyle-HorizontalAlign="Center" />

                                <asp:BoundField
                                    DataField="TotalImporte"
                                    HeaderText="Total Importe"
                                    DataFormatString="{0:C2}"
                                    ItemStyle-HorizontalAlign="Center" />

                            </Columns>

                        </asp:GridView>

                    </div>


                    <asp:LinkButton
                        ID="btnRptEstatus"
                        runat="server"
                        OnClick="btnRptEstatus_Click"
                        ValidationGroup="Save"
                        CssClass="btn btn-warning"
                        Style="display: inline-flex; align-items: center; gap: 8px;">
<span class="glyphicon glyphicon-file"></span>
<span>Descargar Reporte Estatus</span>

                    </asp:LinkButton>

                </div>






            </div>

        </div>


        <!-- DATASOURCE -->

        <asp:EntityDataSource
            ID="edsEstatus"
            runat="server"
            ConnectionString="name=dbFacturasFinancierosEntities"
            DefaultContainerName="dbFacturasFinancierosEntities"
            EnableFlattening="False"
            EnableUpdate="True"
            EntitySetName="tcfactura_estatus">
        </asp:EntityDataSource>


    </div>



</asp:Content>
