<%@ Page Title="Mapeo de Procesos Internos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/MapeoProcesos.aspx.cs" Inherits="Isomanager.Pages.MapeoProcesos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" ValidateRequestMode="Disabled">
    <div class="container mt-5 position-relative">
        <div class="position-absolute top-0 end-0">
            <asp:Label ID="lblNormaActual" runat="server" CssClass="badge bg-primary"></asp:Label>
        </div>
    </div>

    <div class="container mt-4">
        <h1 class="mb-4">Mapeo de Procesos Internos</h1>

        <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="identificacion-tab" data-bs-toggle="tab" data-bs-target="#identificacion" type="button" role="tab" aria-controls="identificacion" aria-selected="true">Identificación</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="diagrama-tab" data-bs-toggle="tab" data-bs-target="#diagrama" type="button" role="tab" aria-controls="diagrama" aria-selected="false">Diagrama de Flujo</button>
            </li>
        </ul>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvProcesosClave" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre del Proceso" />
                        <asp:BoundField DataField="Propietario" HeaderText="Propietario" />
                        <asp:BoundField DataField="Objetivo" HeaderText="Objetivos" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="btn btn-sm btn-outline-primary"
                                    OnClick="btnEdit_Click" CommandArgument='<%# Eval("ProcesoId") %>' />
                                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-outline-danger"
                                    OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este proceso?');"
                                    OnClick="btnDelete_Click" CommandArgument='<%# Eval("ProcesoId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Button ID="btnAddProcess" runat="server" Text="Añadir Proceso" CssClass="btn btn-primary mt-3"
                    data-bs-toggle="modal" data-bs-target="#addProcessModal" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- Modal para agregar proceso -->
        <div class="modal fade" id="addProcessModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar Proceso</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre del Proceso"></asp:TextBox>
                        <asp:TextBox ID="txtPropietario" runat="server" CssClass="form-control mt-2" Placeholder="Propietario"></asp:TextBox>
                        <asp:TextBox ID="txtObjetivo" runat="server" CssClass="form-control mt-2" Placeholder="Objetivo"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGuardar" runat="server" Text="Agregar Proceso" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal para editar proceso -->
        <div class="modal fade" id="editProcessModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Editar Proceso</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtEditNombre" runat="server" CssClass="form-control" Placeholder="Nombre del Proceso"></asp:TextBox>
                        <asp:TextBox ID="txtEditPropietario" runat="server" CssClass="form-control mt-2" Placeholder="Propietario"></asp:TextBox>
                        <asp:TextBox ID="txtEditObjetivo" runat="server" CssClass="form-control mt-2" Placeholder="Objetivo"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Actualizar Proceso" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="row mt-4">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h2 class="card-title">Evaluación y Mejora del Proceso</h2>
                        <p class="card-text text-muted">Evalúa y sugiere mejoras para los procesos</p>

                        <div class="mb-3">
                            <label for="ddlEvalProcess" class="form-label">Seleccionar Proceso</label>
                            <asp:DropDownList ID="ddlEvalProcess" runat="server" CssClass="form-select" OnSelectedIndexChanged="ddlEvalProcess_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label for="txtDescripcion" class="form-label">Descripción de la Mejora</label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtAreaMejora" class="form-label">Área de Mejora</label>
                            <asp:TextBox ID="txtAreaMejora" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtAccionRecomendada" class="form-label">Acción Recomendada</label>
                            <asp:TextBox ID="txtAccionRecomendada" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtResponsable" class="form-label">Responsable</label>
                            <asp:TextBox ID="txtResponsable" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="txtFechaImplementacion" class="form-label">Fecha de Implementación</label>
                            <asp:TextBox ID="txtFechaImplementacion" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>

                        <asp:Button ID="btnAgregarMejora" runat="server" Text="Guardar Mejora" CssClass="btn btn-primary" OnClick="btnAgregarMejora_Click" />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="card mt-4">
                    <div class="card-body">
                        <h2 class="card-title">Mejoras Sugeridas</h2>
                        <asp:GridView ID="gvMejoras" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="AreaMejora" HeaderText="Área de Mejora" />
                                <asp:BoundField DataField="AccionRecomendada" HeaderText="Acción Recomendada" />
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="FechaImplementacion" HeaderText="Fecha de Implementación" DataFormatString="{0:dd/MM/yyyy}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div class="row">
            <!-- Columna de botones -->
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h2 class="card-title">Seguimiento de Cambios y Auditorías</h2>
                        <p class="card-text text-muted">Registra y audita los cambios en los procesos internos.</p>

                        <!-- Botón para registrar un nuevo cambio -->
                        <asp:Button ID="btnRegistrarCambio" runat="server" Text="Registrar Cambio" CssClass="btn btn-primary mb-3"
                            data-bs-toggle="modal" data-bs-target="#registrarCambioModal" />

                        <!-- Botón para programar una auditoría -->
                        <asp:Button ID="btnProgramarAuditoria" runat="server" Text="Programar Auditoría" CssClass="btn btn-primary mb-3"
                            data-bs-toggle="modal" data-bs-target="#programarAuditoriaModal" />
                    </div>
                </div>
            </div>

            <!-- Columna de tablas (GridView) -->
            <div class="col-md-8">
                <div class="card">
                    <div class="card-body">
                        <!-- Grid de seguimiento de cambios -->
                        <h4 class="card-title">Cambios</h4>
                        <asp:GridView ID="gvCambios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="ProcesoNombre" HeaderText="Proceso" />
                                <asp:BoundField DataField="FechaCambio" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" />
                            </Columns>
                        </asp:GridView>

                        <!-- Grid de auditorías -->
                        <h4 class="card-title">Auditorías</h4>
                        <asp:GridView ID="gvAuditorias" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="FechaAuditoria" HeaderText="Fecha de Auditoría" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable Auditoria" />
                                <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" />
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

        <!-- Modal para registrar un nuevo cambio -->
        <div class="modal fade" id="registrarCambioModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Registrar Cambio</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlProcesoCambio" runat="server" CssClass="form-control"
                                    DataTextField="Nombre" DataValueField="ProcesoId"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlProcesoCambio_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlMejoraCambio" runat="server" CssClass="form-control"
                                    DataTextField="Descripcion" DataValueField="MejoraId">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:TextBox ID="txtDescripcionCambio" runat="server" CssClass="form-control mt-2"
                            Placeholder="Descripción del Cambio"></asp:TextBox>
                        <asp:TextBox ID="txtResponsableCambio" runat="server" CssClass="form-control mt-2"
                            Placeholder="Responsable del Cambio"></asp:TextBox>
                        <asp:TextBox ID="txtFechaCambio" runat="server" CssClass="form-control mt-2"
                            TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGuardarCambio" runat="server" Text="Guardar Cambio"
                            CssClass="btn btn-primary" OnClick="btnGuardarCambio_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal para programar una auditoría -->
        <div class="modal fade" id="programarAuditoriaModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Programar Auditoría</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:DropDownList ID="ddlProcesoAuditoria" runat="server" CssClass="form-control" DataTextField="Nombre" DataValueField="ProcesoId"></asp:DropDownList>
                        <asp:TextBox ID="txtFechaAuditoria" runat="server" CssClass="form-control mt-2" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="txtResponsableAuditoria" runat="server" CssClass="form-control mt-2" Placeholder="Responsable de la Auditoría"></asp:TextBox>
                        <asp:TextBox ID="txtComentarios" runat="server" CssClass="form-control mt-2" Placeholder="Comentarios"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnProgramarAuditoriaModal" runat="server" Text="Programar Auditoría" CssClass="btn btn-primary" OnClick="btnProgramarAuditoria_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
