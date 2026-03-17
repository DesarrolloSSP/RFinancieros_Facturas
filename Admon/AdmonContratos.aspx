<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdmonContratos.aspx.cs" Inherits="RFinancieros_Facturas.Admon.AdmonContratos" MaintainScrollPositionOnPostback="true"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        <style >
        .tabla-contratos {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0 6px; /* Espaciado entre filas */
        }

        .tabla-contratos th,
        .tabla-contratos td {
            padding: 10px;
            text-align: left;
        }

        .campo-formulario {
            padding: 6px 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            min-width: 120px;
        }

        .btn-insertar {
            background-color: #28a745;
            color: white;
            padding: 6px 12px;
            border-radius: 4px;
            text-decoration: none;
        }

            .btn-insertar:hover {
                background-color: #218838;
                color: white;
            }
    </style>



    <h2>Administración de Contratos</h2>


    <asp:GridView ID="gvContratos" runat="server" CssClass="tabla-contratos" PageSize="10" AllowPaging="True"
        DataKeyNames="idtc_contrato" AutoGenerateColumns="False" ShowFooter="True"
        OnRowEditing="gvContratos_RowEditing"
        OnRowUpdating="gvContratos_RowUpdating"
        OnRowCancelingEdit="gvContratos_RowCancelingEdit"
        OnRowDeleting="gvContratos_RowDeleting"
        OnRowCommand="gvContratos_RowCommand"
        OnRowDataBound="gvContratos_RowDataBound" OnPageIndexChanging="gvContratos_PageIndexChanging">

        <Columns>
            <asp:BoundField DataField="idtc_contrato" HeaderText="ID" ReadOnly="true" />

            <asp:TemplateField HeaderText="No. Contrato">
                <ItemTemplate>
                    <asp:Label ID="lblNoContrato" runat="server" Text='<%# Eval("nocontrato") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNoContratoEdit" runat="server" MaxLength="20"
                        Text='<%# Bind("nocontrato") %>' CssClass="campo-formulario" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtNoContratoNuevo" runat="server"
                        placeholder="No. Contrato" CssClass="campo-formulario" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="rfvContrato" runat="server"
                        ControlToValidate="txtNoContratoNuevo"
                        ErrorMessage="* Requerido"
                        ForeColor="Red"
                        Display="Dynamic"
                        ValidationGroup="InsertarContrato" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Año">
                <ItemTemplate>
                    <asp:Label ID="lblAño" runat="server" Text='<%# Eval("anio") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlAñoEdit" runat="server" CssClass="campo-formulario" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlAñoNuevo" runat="server" CssClass="campo-formulario" />
                    <asp:RequiredFieldValidator ID="rfvAño" runat="server"
                        ControlToValidate="ddlAñoNuevo"
                        InitialValue=""
                        ErrorMessage="* Selecciona un año"
                        ForeColor="Red"
                        Display="Dynamic"
                        ValidationGroup="InsertarContrato" />
                </FooterTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:Label ID="lblActivo" runat="server" Text='<%# Eval("Estado") %>' />
                </ItemTemplate>



                <EditItemTemplate>
                    <asp:DropDownList ID="ddlActivoEdit" runat="server" CssClass="campo-formulario" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlActivoNuevo" runat="server" CssClass="campo-formulario" />
                    <asp:RequiredFieldValidator ID="rfvActivo" runat="server"
                        ControlToValidate="ddlActivoNuevo"
                        InitialValue=""
                        ErrorMessage="* Selecciona un estatus"
                        ForeColor="Red"
                        Display="Dynamic"
                        ValidationGroup="InsertarContrato" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField ShowHeader="False">
                <FooterTemplate>
                    <asp:LinkButton ID="btnInsertar" runat="server"
                        CommandName="Insert" Text="Agregar"
                        CssClass="btn-insertar"
                        CausesValidation="true"
                        ValidationGroup="InsertarContrato" />
                </FooterTemplate>
            </asp:TemplateField>




            <asp:CommandField ShowEditButton="true" ShowDeleteButton="false" />
        </Columns>
    </asp:GridView>

    <br />





    <%-- <asp:ValidationSummary ID="vsErrores" runat="server" ForeColor="Red" HeaderText="Corrige los siguientes errores:" />--%>
</asp:Content>
