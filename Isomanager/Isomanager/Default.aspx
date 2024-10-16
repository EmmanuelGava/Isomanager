<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Isomanager._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center">
        <h1>Sistema de Gestión</h1>
        <p>Bienvenido al panel de control del Sistema de Gestión Integrado</p>
        <div class="row">
            <div class="col-md-4">
                <div class="card h-100">
                    <div class="card-body">
                        <h5 class="card-title">Contexto</h5>
                        <p class="card-text">Analizar el contexto organizacional.</p>
                        <a href="contexto.aspx" class="btn btn-primary">Haga clic para gestionar</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card h-100">
                    <div class="card-body">
                        <h5 class="card-title">Operaciones</h5>
                        <p class="card-text">Gestionar procesos operativos.</p>
                        <a href="operaciones.aspx" class="btn btn-primary">Haga clic para gestionar</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card h-100">
                    <div class="card-body">
                        <h5 class="card-title">Planificación</h5>
                        <p class="card-text">Establecer objetivos y planificar acciones.</p>
                        <a href="planificacion.aspx" class="btn btn-primary">Haga clic para gestionar</a>
                    </div>
                </div>
            </div>
        </div>
        <!-- Añade más tarjetas aquí -->
        
        <!-- Contenedor del diagrama -->
        <div id="diagram-container"></div>
    </div>
</asp:Content>

<asp:Content ID="ScriptsContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    <script src="https://d3js.org/d3.v7.min.js"></script>

    <!-- Script para el diagrama interactivo -->
    <script>
        var svg = d3.select("#diagram-container").append("svg")
            .attr("width", 500)
            .attr("height", 400);

        // Crear círculos para los procesos
        svg.append("circle").attr("cx", 150).attr("cy", 150).attr("r", 50).style("fill", "#f5a623");
        svg.append("text").attr("x", 150).attr("y", 150).attr("text-anchor", "middle").text("Planificación");

        svg.append("circle").attr("cx", 350).attr("cy", 150).attr("r", 50).style("fill", "#4a90e2");
        svg.append("text").attr("x", 350).attr("y", 150).attr("text-anchor", "middle").text("Operaciones");

        // Crear conexión (flecha)
        svg.append("line")
            .attr("x1", 200).attr("y1", 150)
            .attr("x2", 300).attr("y2", 150)
            .attr("stroke", "black").attr("stroke-width", 2)
            .attr("marker-end", "url(#arrow)");

        // Definir marcador de flecha
        svg.append("defs").append("marker")
            .attr("id", "arrow")
            .attr("markerWidth", 10).attr("markerHeight", 10)
            .attr("refX", 5).attr("refY", 5)
            .append("path").attr("d", "M0,0 L0,10 L10,5 z").attr("fill", "#000");
    </script>
</asp:Content>
