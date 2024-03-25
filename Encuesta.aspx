<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encuesta.aspx.cs" Inherits="WebApplication2.Encuesta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Encuesta</title>
    <style>
        body {
            font-family: "Times New Roman", Times, serif;
            background-color: #D5B2F0;
        }
        .container {
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
            background-color: #ffffff; 
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1 {
            text-align: center;
        }
        h2 {
            margin-top: 20px;
            font-size: 18px;
            font-weight: normal;
        }
        label {
            display: block;
            margin-top: 10px;
        }
        input[type="text"],
        input[type="email"],
        select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            margin-top: 6px;
            margin-bottom: 16px;
        }
        button {
            background-color: #32640B; 
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            float: right;
        }
        button:hover {
            background-color: #45a049;
        }
    </style>
    <script>
        function validarFormulario() {
            var nombre = document.getElementById("nombre").value;
            var apellidos = document.getElementById("apellidos").value;
            var fechaNacimiento = document.getElementById("fechaNacimiento").value;
            var edad = document.getElementById("edad").value;
            var correo = document.getElementById("correo").value;
            var carroPropio = document.getElementById("carroPropio").value;

            if (nombre === "" || apellidos === "" || fechaNacimiento === "" || edad === "" || correo === "" || carroPropio === "") {
                alert("Todos los campos son obligatorios.");
                return false;
            }

            if (edad < 18 || edad > 50) {
                alert("La edad debe estar entre 18 y 50 años.");
                return false;
            }

            return true;
        }
    </script>

    <script>
        function consultarEncuesta() {
            var numeroEncuesta = document.getElementById("numeroEncuestaConsulta").value;
            // Hacer la solicitud para obtener los detalles de la encuesta utilizando AJAX
            // Aquí deberías implementar la lógica para enviar una solicitud al servidor y recibir los datos de la encuesta
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" onsubmit="return validarFormulario();">
        <div class="container">
            <h1>Cuestionario</h1>
            <h2>Responda adecuadamente a todas las preguntas</h2>
            <asp:Label ID="lblNumeroEncuesta" runat="server" Text="Número de Encuesta:"></asp:Label>
            <asp:TextBox ID="txtNumeroEncuesta" runat="server" ReadOnly="true"></asp:TextBox>
            
            <label for="nombre">Nombre:</label>
            <input type="text" id="nombre" name="nombre" required />
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            
            <label for="apellidos">Apellidos:</label>
            <input type="text" id="apellidos" name="apellidos" required />
            <asp:TextBox ID="txtApellidos" runat="server"></asp:TextBox>
            
            <label for="fechaNacimiento">Fecha de Nacimiento:</label>
            <input type="date" id="fechaNacimiento" name="fechaNacimiento" required />
            <asp:TextBox ID="txtFechaNacimiento" runat="server"></asp:TextBox>
            
            <label for="edad">Edad:</label>
            <input type="number" id="edad" name="edad" min="18" max="50" required />
            <asp:TextBox ID="txtEdad" runat="server"></asp:TextBox>
            
            <label for="correo">Correo Electrónico:</label>
            <input type="email" id="correo" name="correo" required />
            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
            
            <label for="carroPropio">Carro Propio:</label>
            <select id="carroPropio" name="carroPropio" required>
            <asp:TextBox ID="txtCarroPropio" runat="server"></asp:TextBox>
                <option value="">Seleccione...</option>
                <option value="Si">Si</option>
                <option value="No">No</option>
            </select>

            <button type="submit" onclick="guardarEncuesta();">Guardar</button>
            <button type="button">Modificar</button>
            <div id="detallesEncuesta"></div>
            <button id="btnModificar" type="button" style="display:none;" onclick="modificarEncuesta();">Modificar</button>

            <button type="button">Buscar</button>
                <label for="txtBuscarEncuesta">Buscar Encuesta por Número:</label>
                    <input type="text" id="txtBuscarEncuesta" name="txtBuscarEncuesta" />
                    <button type="button" onclick="buscarEncuesta();">Consultar</button>
                    <div id="resultadoEncuesta" style="margin-top: 20px;"></div> 

            <button type="button">Eliminar</button>
            <div id="detallesEncuesta"></div>
            <button id="btnEliminar" type="button" style="display:none;" onclick="eliminarEncuesta();">Eliminar</button>

            <button type="button">Reportes</button>
            <button type="button" onclick="generarReporte();">Generar Reporte</button>
            <a href="#" onclick="generarReporte();">Generar Reporte</a>


        </div>
    </form>

    <script>
        function guardarEncuesta() {
            alert("Encuesta guardada correctamente.");
        }
        function consultarEncuesta() {
            var numeroEncuesta = document.getElementById("numeroEncuestaConsulta").value;
            document.getElementById("detallesEncuesta").innerHTML = "Detalles de la encuesta...";
            document.getElementById("btnEliminar").style.display = "inline";
        }
        
        function generarReporte() {
      
            var reporteWindow = window.open('', '_blank');
            reporteWindow.document.write('<h1>Reporte de Encuestas</h1>');
            reporteWindow.document.write('<p>Cantidad de encuestas realizadas: 100</p>');
            reporteWindow.document.write('<p>Cantidad de personas con carro propio: 70</p>');
            reporteWindow.document.write('<p>Cantidad de personas sin carro propio: 30</p>');
        }
        function mostrarReporte(reporteJson) {
            var reporte = JSON.parse(reporteJson);
            var reporteWindow = window.open('', '_blank');
            reporteWindow.document.write('<h1>Reporte de Encuestas</h1>');
            reporteWindow.document.write('<p>Cantidad de encuestas realizadas: ' + reporte.TotalEncuestas + '</p>');
            reporteWindow.document.write('<p>Cantidad de personas con carro propio: ' + reporte.PersonasConCarroPropio + '</p>');
            reporteWindow.document.write('<p>Cantidad de personas sin carro propio: ' + reporte.PersonasSinCarroPropio + '</p>');
        }


    </script>
</body>
</html>

