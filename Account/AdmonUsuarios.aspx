<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdmonUsuarios.aspx.cs" Inherits="RFinancieros_Facturas.Account.AdmonUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <div class="container">
        <div class="input-group">
            <asp:TextBox ID="tbBuscarx" runat="server" CssClass="form-control-sm" placeholder="Texto a buscar ..."></asp:TextBox>
            <div class="col-md">
                <asp:ImageButton ID="ImgBuscar" runat="server" AutoPostBack="true" ImageUrl="~/Images/buscar.png" Height="40px" Width="40px" ToolTip="Buscar usuario" OnClick="ImgBuscar_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6 d-flex justify-content-md-start">
                        <div class="input-group">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click1">Nuevo</asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-6 d-flex justify-content-end">
                        <asp:Label ID="lblnumreg" runat="server" Text="Registros por pagina: " CssClass="col-11 form-control-sm text-end"></asp:Label>
                        <asp:DropDownList ID="ddlpageSize" AutoPostBack="true" runat="server" CssClass="col-1 form-control" OnSelectedIndexChanged="DdlpageSize_SelectedIndexChanged" Width="90px">
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
                <asp:GridView ID="dgUsuarios" runat="server" class="table table-bordered" Font-Size="Small" AllowPaging="True" DataKeyNames="UserId,Status,Usuario,Bloqueo" OnRowDataBound="DgUsuarios_RowDataBound"
                    PageSize="15" OnPageIndexChanging="DgUsuarios_PageIndexChanging" AutoGenerateColumns="False"
                    OnRowCommand="DgUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />
                        <asp:BoundField DataField="Mail" HeaderText="email" />
                        <asp:BoundField DataField="creacion" HeaderText="Creación" DataFormatString="{0:d}" />
                        <asp:TemplateField HeaderText="Activo" Visible="true">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkstatus" runat="server" AutoPostBack="true" OnCheckedChanged="Chkstatus_CheckedChanged" CssClass="text-center" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bloqueo">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkbloq" runat="server" AutoPostBack="true" OnCheckedChanged="Chkbloq_CheckedChanged" CssClass="text-center" />
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
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="Imgcancelar" runat="server" Height="25px" ImageAlign="Middle" ImageUrl="~/Images/cancelar.png" ToolTip="Cancelar cuenta" Width="25px" CommandArgument='<%#Eval("Usuario") %>' OnCommand="Imgcancelar_Command" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <PagerSettings PreviousPageText="Previa" />
                    <AlternatingRowStyle BackColor="White" />
                    <PagerSettings PreviousPageText="Previa" />
                    <PagerStyle HorizontalAlign="Center" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>   
</asp:Content>
