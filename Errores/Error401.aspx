<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Error401.aspx.cs"
    Inherits="RFinancieros_Facturas.Errores._401" %>

<!DOCTYPE html>

<html lang="es-MX">

<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>401 - No autorizado</title>

    <link href="../css/Basico.css" rel="stylesheet" />
    <link href="../css/Pages.css" rel="stylesheet" />

</head>

<body class="error-401">

<form id="form1" runat="server">

<main class="error-container">

    <h1 class="error-code">
        401
    </h1>

    <h2 class="error-title">
        Acceso no autorizado
    </h2>

    <p class="error-description">

        Su sesión no es válida o ha expirado.
        Inicia sesión nuevamente para continuar.

    </p>

    <span class="http-code">

        HTTP 401

    </span>

    <asp:Button
        ID="btnInicio"
        runat="server"
        CssClass="error-button"
        Text="Iniciar sesión"
        OnClick="btnInicio_Click" />

    <footer class="error-footer">

        <strong>VAREFACT</strong><br />

        Validación y Registro de Facturas<br />

        Secretaría de Seguridad Pública

    </footer>

</main>

</form>

</body>

</html>