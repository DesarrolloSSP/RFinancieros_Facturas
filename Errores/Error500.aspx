<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Error500.aspx.cs"
    Inherits="RFinancieros_Facturas.Errores._500" %>

<!DOCTYPE html>

<html lang="es-MX">

<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>500 - Error interno</title>

    <link href="../css/Basico.css" rel="stylesheet" />
    <link href="../css/Pages.css" rel="stylesheet" />

</head>

<body class="error-500">

<form id="form1" runat="server">

<main class="error-container">

    <h1 class="error-code">

        500

    </h1>

    <h2 class="error-title">

        Ocurrió un error inesperado

    </h2>

    <p class="error-description">

        Se produjo un error interno mientras procesábamos tu solicitud.
        Inténtelo nuevamente en unos minutos.
        Si el problema persiste, comuníquese con el administrador del sistema.

    </p>

    <span class="http-code">

        HTTP 500

    </span>

    <asp:Button
        ID="btnInicio"
        runat="server"
        CssClass="error-button"
        Text="Regresar al inicio"
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