<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/Contextos.aspx.cs" Inherits="Isomanager.Pages.Contextos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5 position-relative">
        <!-- Label para mostrar la norma actual -->
        <div class="position-absolute top-0 end-0">
            <asp:Label ID="lblNormaActual" runat="server" CssClass="badge bg-primary"></asp:Label>
        </div>

        <h1 class="text-center">Contexto del Sistema de Gestión</h1>
        <div class="row mt-4">
            <!-- Análisis FODA -->
            <div class="col-md-12 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Análisis FODA</h5>
                        <p>El análisis FODA (Fortalezas, Oportunidades, Debilidades y Amenazas) es una herramienta estratégica utilizada para identificar los factores internos y externos que pueden afectar el desempeño de la organización.</p>
                        <asp:Button ID="btnFODA" runat="server" Text="Realizar Análisis FODA" CssClass="btn btn-primary" OnClick="btnFODA_Click"/>
                    </div>
                </div>
                <!-- Sección para cargar el análisis FODA -->
                <div class="row mt-4" id="CargarFODA" runat="server" visible="false">
                    <div class="col-md-12 mb-4">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Cargar Análisis FODA</h5>
                                <div class="mb-3">
                                    <label for="txtFortalezas" class="form-label">Fortalezas</label>
                                    <asp:TextBox ID="txtFortalezas" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="5000"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtOportunidades" class="form-label">Oportunidades</label>
                                    <asp:TextBox ID="txtOportunidades" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="5000"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtDebilidades" class="form-label">Debilidades</label>
                                    <asp:TextBox ID="txtDebilidades" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="5000"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <label for="txtAmenazas" class="form-label">Amenazas</label>
                                    <asp:TextBox ID="txtAmenazas" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" MaxLength="5000"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnGuardarFODA" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarFODA_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección para mostrar los resultados del análisis FODA -->
            <div class="row mt-4" id="MostrarFODA" runat="server" visible="false">
                <div class="col-md-12 mb-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Resultados del Análisis FODA</h5>
                            <h6>Fortalezas</h6>
                            <p>
                                <asp:Label ID="lblFortalezas" runat="server"></asp:Label>
                            </p>
                            <h6>Oportunidades</h6>
                            <p>
                                <asp:Label ID="lblOportunidades" runat="server"></asp:Label>
                            </p>
                            <h6>Debilidades</h6>
                            <p>
                                <asp:Label ID="lblDebilidades" runat="server"></asp:Label>
                            </p>
                            <h6>Amenazas</h6>
                            <p>
                                <asp:Label ID="lblAmenazas" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Mapeo de Procesos Internos -->
            <div class="col-md-12 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Mapeo de Procesos Internos</h5>
                        <p>El mapeo de procesos internos ayuda a identificar y documentar los procesos clave dentro de la organización, facilitando la mejora continua y la eficiencia operativa.</p>
                        <asp:Button ID="btnMapeoProcesos" runat="server" Text="Mapear Procesos" CssClass="btn btn-primary" OnClick="btnMapeoProcesos_Click" />
                    </div>
                </div>
            </div>
            <!-- Identificación de Factores Externos Relevantes -->
            <div class="col-md-12 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Identificación de Factores Externos Relevantes</h5>
                        <p>La identificación de factores externos relevantes permite a la organización anticipar y adaptarse a cambios en el entorno que puedan impactar su desempeño.</p>
                        <asp:Button ID="btnFactoresExternos" runat="server" Text="Identificar Factores Externos" CssClass="btn btn-primary" OnClick="btnFactoresExternos_Click" />
                    </div>
                </div>
            </div>
            <!-- Definición del Alcance del Sistema de Gestión -->
            <div class="col-md-12 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Definición del Alcance del Sistema de Gestión</h5>
                        <p>La definición del alcance del sistema de gestión establece los límites y la aplicabilidad del sistema dentro de la organización, asegurando que todos los aspectos relevantes sean considerados.</p>
                        <asp:Button ID="btnAlcanceSistema" runat="server" Text="Definir Alcance" CssClass="btn btn-primary" OnClick="btnAlcanceSistema_Click" />
                    </div>
                </div>
            </div>
        </div>




    </div>
</asp:Content>
