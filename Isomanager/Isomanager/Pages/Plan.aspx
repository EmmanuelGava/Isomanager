<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/Plan.aspx.cs" Inherits="Isomanager.Pages.Plan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h1 class="text-center">Plan Completo</h1>
         
        <!-- Añadir el DropDownList para seleccionar la norma -->
        <div class="row mt-4 mb-4">
            <div class="col-md-6 offset-md-3">
                <asp:DropDownList ID="ddlNormas" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNormas_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Contexto</h5>
                        <ul>
                            <li>Análisis FODA</li>
                            <li>Mapeo de procesos internos</li>
                            <li>Identificación de factores externos relevantes</li>
                            <li>Definición del alcance del sistema de gestión</li>
                        </ul>
                        <a href="Contextos.aspx" class="btn btn-primary">Acceder</a>
                    </div>
    
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Personas</h5>
                        <ul>
                            <li>Gestión de competencias y formación</li>
                            <li>Evaluación del desempeño</li>
                            <li>Programa de reconocimiento y motivación</li>
                            <li>Canal de comunicación interna y sugerencias</li>
                        </ul>
                        <a href="Personas.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Partes Interesadas</h5>
                        <ul>
                            <li>Registro y categorización</li>
                            <li>Matriz de necesidades y expectativas</li>
                            <li>Evaluación de influencia</li>
                            <li>Plan de comunicación</li>
                        </ul>
                        <a href="PartesInteresadas.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Requisitos Legales</h5>
                        <ul>
                            <li>Base de datos de legislación</li>
                            <li>Calendario de cumplimiento legal</li>
                            <li>Evaluación periódica de cumplimiento</li>
                            <li>Gestión de permisos y licencias</li>
                        </ul>
                        <a href="RequisitosLegales.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Medio Ambiente</h5>
                        <ul>
                            <li>Inventario de aspectos ambientales</li>
                            <li>Evaluación de impactos</li>
                            <li>Registro de consumo y generación de residuos</li>
                            <li>Plan de gestión ambiental</li>
                        </ul>
                        <a href="MedioAmbiente.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Liderazgo</h5>
                        <ul>
                            <li>Definición y comunicación de políticas</li>
                            <li>Asignación de roles y responsabilidades</li>
                            <li>Programa de revisión</li>
                            <li>Gestión del cambio organizacional</li>
                        </ul>
                        <a href="Liderazgo.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Planificación</h5>
                        <ul>
                            <li>Establecimiento de objetivos SMART</li>
                            <li>Análisis de riesgos y oportunidades</li>
                            <li>Planificación de recursos</li>
                            <li>Gestión de proyectos de mejora</li>
                        </ul>
                        <a href="Planificacion.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Operaciones</h5>
                        <ul>
                            <li>Documentación de procesos operativos</li>
                            <li>Control de calidad y puntos críticos</li>
                            <li>Gestión de proveedores y compras</li>
                            <li>Mantenimiento preventivo de equipos</li>
                        </ul>
                        <a href="Operaciones.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Verificación</h5>
                        <ul>
                            <li>Programa de auditorías internas</li>
                            <li>Seguimiento de indicadores clave (KPIs)</li>
                            <li>Gestión de no conformidades</li>
                            <li>Análisis de datos y tendencias</li>
                        </ul>
                        <a href="Verificacion.aspx" class="btn btn-primary">Acceder</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
