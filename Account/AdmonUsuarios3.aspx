<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdmonUsuarios.aspx.cs" Inherits="RFinancieros_Facturas.Account.AdmonUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"><div class="container">
        <div class="input-group">
            <asp:TextBox ID="tbBuscarx" runat="server" CssClass="form-control-sm" placeholder="Texto a buscar ..."></asp:TextBox>
            <div class="col-md">
                <asp:ImageButton ID="ImgBuscar" runat="server" AutoPostBack="true" ImageUrl="~/Images/buscar.png" Height="40px" Width="40px" ToolTip="Buscar usuario" OnClick="ImgBuscar_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6 d-flex justify-content-md-start">si 
                        <div class="input-group">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1">Nuevo</asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-6 d-flex justify-content-end">
                        <asp:Label ID="lblnumreg" runat="server" Text="Registros por pagina: " CssClass="col-11 form-control-sm text-end"></asp:Label>
                        <asp:DropDownList ID="ddlpageSize" AutoPostBack="true" runat="server" CssClass="col-1 form-control" OnSelectedIndexChanged="ddlpageSize_SelectedIndexChanged" Width="90px">
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="dgUsuarios" runat="server" class="table table-bordered" Font-Size="Small"  AllowPaging="True" DataKeyNames="UserId,Status,Bloqueo" OnRowDataBound="dgUsuarios_RowDataBound"
                    PageSize="15" OnPageIndexChanging="dgUsuarios_PageIndexChanging" AutoGenerateColumns="False"
                    OnRowCommand="dgUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />
                        <asp:BoundField DataField="Mail" HeaderText="email" />
                        <asp:BoundField DataField="creacion" HeaderText="Creación" DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkstatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkstatus_CheckedChanged" CssClass="text-center"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bloqueo">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkbloq" runat="server" AutoPostBack="true" OnCheckedChanged="chkbloq_CheckedChanged" CssClass="text-center"/>
                            </ItemTemplate>
                        </asp:TemplateField>
     <%--                    <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="Imgcambiarcontra" runat="server" Height="25px" ImageAlign="Middle" ImageUrl="~/Images/contrasena.png" ToolTip="Cambiar contraseña ..." Width="25px" CommandArgument='<%# Eval("Usuario") %>' OnCommand="Imgcambiarcontra_Command" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="Imgimprimeres" runat="server" Height="25px" ImageAlign="Middle" ImageUrl="~/Images/impresora.png" ToolTip="Imprimir resguardo ..." Width="25px" CommandArgument='<%# Eval("Usuario") %>' OnCommand="Imgimprimeres_Command" Enabled="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="Imgcancelar" runat="server" Height="25px" ImageAlign="Middle" ImageUrl="~/Images/cancelar.png" ToolTip="Cancelar cuenta" Width="25px" CommandArgument='<%# Eval("Usuario") %>' OnCommand="Imgcancelar_Command" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PreviousPageText="Previa" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PreviousPageText="Previa" />
                    <PagerStyle HorizontalAlign="Center" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Cambio de Contraseña</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Contraseña Actual</asp:Label>
                        <asp:TextBox runat="server" ID="CurrentPassword" CssClass="form-control" TextMode="Password" Width="300px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="field-validation-error" ErrorMessage="El campo 'Contraseña Actual' es requerido."
                            ValidationGroup="ChangePassword" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">Nueva Contraseña</asp:Label>
                        <asp:TextBox runat="server" ID="NewPassword" CssClass="form-control" TextMode="Password" Width="300px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewPassword"
                            CssClass="field-validation-error" ErrorMessage="El campo 'Nueva Contraseña' es requerido."
                            ValidationGroup="ChangePassword" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Confirmar Nueva Contraseña</asp:Label>
                        <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="form-control" TextMode="Password" Width="300px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="field-validation-error" Display="Dynamic" ErrorMessage="El campo 'Confirmar Nueva Contraseña' es requerido."
                            ValidationGroup="ChangePassword" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_CambiaPassword" CssClass="btn btn-primary" runat="server" Text="Guardar" ValidationGroup="ChangePassword" OnClick="btn_CambiaPassword_Click" />
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>


    <script type="text/javascript">
        function AbrirModal() {
            $("#exampleModal").modal("show");
        }
    </script>
</asp:Content>
