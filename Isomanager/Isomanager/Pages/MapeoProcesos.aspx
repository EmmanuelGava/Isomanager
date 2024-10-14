<%@ Page Title="Mapeo de Procesos Internos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/MapeoProcesos.aspx.cs" Inherits="Isomanager.Pages.MapeoProcesos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function showNormaAlert() {
            alert('Por favor, seleccione una norma antes de continuar.');
            return false;
        }
    </script>

    <div class="container mt-5">
        <h2>Mapeo de Procesos Internos</h2>
        
        <!-- Identificación de Procesos Clave -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Identificación de Procesos Clave</h5>
                <asp:GridView ID="gvProcesosClave" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Proceso" />
                        <asp:BoundField DataField="Propietario" HeaderText="Responsable" />
                        <asp:BoundField DataField="Objetivo" HeaderText="Objetivo" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnAgregarProceso" runat="server" Text="Agregar Proceso" CssClass="btn btn-primary" OnClick="btnAgregarProceso_Click" />
            </div>
        </div>

        <!-- Diagrama de Flujo de Procesos -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Diagrama de Flujo de Procesos</h5>
                <asp:PlaceHolder ID="phDiagramaFlujo" runat="server"></asp:PlaceHolder>
                <asp:Button ID="btnEditarDiagrama" runat="server" Text="Editar Diagrama" CssClass="btn btn-secondary" OnClick="btnEditarDiagrama_Click" />
            </div>
        </div>

        <!-- Descripción Detallada de los Procesos -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Descripción Detallada de los Procesos</h5>
                <asp:DropDownList ID="ddlProcesos" runat="server" CssClass="form-select mb-3" AutoPostBack="true" OnSelectedIndexChanged="ddlProcesos_SelectedIndexChanged"></asp:DropDownList>
                <div id="detallesProceso" runat="server">
                    <asp:Label ID="lblNombreProceso" runat="server" CssClass="h6"></asp:Label>
                    <asp:Label ID="lblDescripcionProceso" runat="server" CssClass="d-block mb-2"></asp:Label>
                    <h6>Entradas:</h6>
                    <asp:BulletedList ID="blEntradas" runat="server"></asp:BulletedList>
                    <h6>Salidas:</h6>
                    <asp:BulletedList ID="blSalidas" runat="server"></asp:BulletedList>
                    <h6>Recursos:</h6>
                    <asp:BulletedList ID="blRecursos" runat="server"></asp:BulletedList>
                    <h6>KPIs:</h6>
                    <asp:BulletedList ID="blKPIs" runat="server"></asp:BulletedList>
                </div>
            </div>
        </div>

        <!-- Interacción entre Procesos -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Interacción entre Procesos</h5>
                <asp:PlaceHolder ID="phInteraccionProcesos" runat="server"></asp:PlaceHolder>
            </div>
        </div>

        <!-- Documentación Relacionada -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Documentación Relacionada</h5>
                <asp:GridView ID="gvDocumentacion" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Documento" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkVerDocumento" runat="server" Text="Ver" CommandName="VerDocumento" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <!-- Evaluación y Mejora del Proceso -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Evaluación y Mejora del Proceso</h5>
                <asp:GridView ID="gvMejoras" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Proceso" HeaderText="Proceso" />
                        <asp:BoundField DataField="AreaMejora" HeaderText="Área de Mejora" />
                        <asp:BoundField DataField="AccionRecomendada" HeaderText="Acción Recomendada" />
                        <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                        <asp:BoundField DataField="FechaImplementacion" HeaderText="Fecha de Implementación" DataFormatString="{0:d}" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnAgregarMejora" runat="server" Text="Agregar Mejora" CssClass="btn btn-primary" OnClick="btnAgregarMejora_Click" />
            </div>
        </div>

        <!-- Gestión de Riesgos y Oportunidades -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Gestión de Riesgos y Oportunidades</h5>
                <asp:GridView ID="gvRiesgosOportunidades" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Proceso" HeaderText="Proceso" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="Impacto" HeaderText="Impacto" />
                        <asp:BoundField DataField="AccionPropuesta" HeaderText="Acción Propuesta" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnAgregarRiesgoOportunidad" runat="server" Text="Agregar Riesgo/Oportunidad" CssClass="btn btn-primary" OnClick="btnAgregarRiesgoOportunidad_Click" />
            </div>
        </div>
    </div>
</asp:Content>