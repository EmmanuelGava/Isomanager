<%@ Page Title="Documentación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/Documentos.aspx.cs" Inherits="Isomanager.Pages.Documentos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Documentación de Normas ISO</h2>
        
        <!-- Información General -->
        <h3>Información General</h3>
        <p>Esta sección proporciona una visión general de todas las normas ISO gestionadas por el software.</p>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre de la Norma</th>
                    <th>Estado</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>ISO 9001</td>
                    <td>Sistema de Gestión de Calidad</td>
                    <td>Válido</td>
                </tr>
                <tr>
                    <td>ISO 14001</td>
                    <td>Sistema de Gestión Ambiental</td>
                    <td>En revisión</td>
                </tr>
                <tr>
                    <td>ISO 45001</td>
                    <td>Sistema de Gestión de Seguridad y Salud en el Trabajo</td>
                    <td>Obsoleto</td>
                </tr>
            </tbody>
        </table>

        <!-- Visualización de Datos -->
        <h3>Visualización de Datos</h3>
        <div class="chart-container">
            <canvas id="myChart" width="800" height="400"></canvas>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
        <script>
            var ctx = document.getElementById('myChart').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'doughnut', // Tipo de gráfico
                data: {
                    labels: ["Válido", "En revisión", "Obsoleto"],
                    datasets: [{
                        label: 'Número de Documentos',
                        data: [10, 5, 2], // Reemplazar con datos reales
                        backgroundColor: [
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(255, 206, 86, 0.2)'
                        ],
                        borderColor: [
                            'rgba(75, 192, 192, 1)',
                            'rgba(255, 99, 132, 1)',
                            'rgba(255, 206, 86, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false // Puedes ajustar esto según sea necesario
                }
            });
        </script>



        <!-- Acceso a Documentos -->
        <h3>Acceso a Documentos</h3>
        <p>A continuación, se presentan enlaces a los documentos específicos de cada norma:</p>
        <ul>
            <li><a href="#">ISO 9001 Documentación</a></li>
            <li><a href="#">ISO 14001 Documentación</a></li>
            <li><a href="#">ISO 45001 Documentación</a></li>
        </ul>

        <!-- Historial de Cambios -->
        <h3>Historial de Cambios</h3>
        <p>Aquí se puede incluir un historial de cambios para cada norma:</p>
        <ul>
            <li>ISO 9001: Actualización en enero de 2023.</li>
            <li>ISO 14001: Revisión pendiente desde febrero de 2024.</li>
        </ul>

        <!-- Integración con Otras Funciones -->
        <h3>Integración con Otras Funciones</h3>
        <p>Accede a otras funciones del sistema:</p>
        <ul>
            <li><a href="CrearNorma.aspx">Crear Nueva Norma</a></li>
            <li><a href="EditarNorma.aspx">Editar Normas Existentes</a></li>
            <li><a href="Reportes.aspx">Generar Reportes</a></li>
        </ul>

        <!-- Instrucciones y Procedimientos -->
        <h3>Instrucciones y Procedimientos</h3>
        <p>Consulte la sección de ayuda sobre cómo utilizar el sistema y manejar las normas ISO.</p>
        <p><a href="#">Ayuda y Soporte</a></p>
    </div>
</asp:Content>
