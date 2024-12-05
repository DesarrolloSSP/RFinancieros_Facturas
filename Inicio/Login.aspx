<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RFinancieros_Facturas.Inicio.Login" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" />
    <title>Iniciar Sesión</title>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <link rel="stylesheet" href="styles.css" />

    <!-- Archivo CSS separado -->
    <link href="../Content/source/css/GeneralSSP.css" rel="stylesheet" />


    <script type="text/javascript">
        function error() {
            swal({
                title: "Error!",
                text: "El usuario y la contraseña no son correctos",
                icon: "error",
                button: "Aceptar",

            });
            return false
        }
    </script>

</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="principal"></asp:ScriptManager>

        <div class="container">
            <div class="row justify-content-center align-items-center min-vh-100">
                <div class="col-12 col-md-8 col-lg-6 card-1" style="margin-top: -15%">
                    <!-- Ajusta el margen superior -->
                    <div class="shadow p-3 mb-5 bg-body-tertiary rounded fadeInDown">
                        <div class="row">
                            <div class="col-md-6 d-flex justify-content-center align-items-center">
                                <img src="../Images/logo.png" class="img-fluid" style="width: 65%;" />
                            </div>

                            <div class="col-md-6 container text-center">
                                <div class="row">
                                    <h4 class="text-titulo">INICIAR SESIÓN</h4>
                                    <div class="input-group flex-nowrap p-2">
                                        <span class="input-group-text" id="addon-wrapping"><i class="fas fa-user icono-c"></i></span>
                                        <asp:TextBox runat="server" ID="UserName" class="form-control short-textbox" placeholder="Usuario" required autocomplete="off"> </asp:TextBox>
                                    </div>

                                    <div class="input-group flex-nowrap p-2">
                                        <span class="input-group-text" id="addon-wrapping2"><i class="fas fa-lock icono-c"></i></span>
                                        <asp:TextBox runat="server" ID="Password" class="form-control short-textbox" placeholder="Contraseña" TextMode="Password" required autocomplete="off"> </asp:TextBox>
                                    </div>

                                    <asp:Button ID="LoginButton" CommandName="Login" OnClick="LoginButton_Click" runat="server" class="btn col-11 mx-auto btn-principal mt-3" ValidationGroup="Login1" Text="Ingresar" />
                                    <!-- Añadido mt-3 para margen superior -->
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="red" Visible="True"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="text-center fadeInDown mt-3">
                        <img class="avatar" src="~/Images/Veracruz2025.png" runat="server" style="width: 40%" />
                    </div>
                </div>
            </div>
        </div>




        <footer class="footer p-2 card">
            <div class="container-fluid float-md-start">
                <div class="row">
                    <div class="col-sm-12 col-md-12" style="color: white">
                        <p class="align-self-center text-center">Se prohíbe la reproducción total o parcial contenida en este sistema informático sin el consentimiento expreso y por escrito de la Secretaría de Seguridad Pública del Estado de Veracruz. Esta plataforma digital deberá ser utilizada únicamente por el personal autorizado. El acceso no autorizado a sistemas informáticos es un delito grave.</p>
                        <p class="align-self-center text-center">&copy; <%= DateTime.Now.Year %> - DERECHOS RESERVADOS.</p>
                        <%--   <p class="align-self-center text-center">DERECHOS RESERVADOS.</p>--%>
                    </div>
                </div>
            </div>
        </footer>
    </form>



</body>
</html>


