<%@ Page Title="Mapeo de Procesos Internos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/MapeoProcesos.aspx.cs" Inherits="Isomanager.Pages.MapeoProcesos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >
    <div class="container mt-5 position-relative">
        <!-- Label para mostrar la norma actual -->
        <div class="position-absolute top-0 end-0">
            <asp:Label ID="lblNormaActual" runat="server" CssClass="badge bg-primary"></asp:Label>
        </div>
    </div>

    <div class="container mt-4">
        <h1 class="mb-4">Mapeo de Procesos Internos</h1>

        <!-- Tabs -->
        <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="identificacion-tab" data-bs-toggle="tab" data-bs-target="#identificacion" type="button" role="tab" aria-controls="identificacion" aria-selected="true">Identificación</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="diagrama-tab" data-bs-toggle="tab" data-bs-target="#diagrama" type="button" role="tab" aria-controls="diagrama" aria-selected="false">Diagrama de Flujo</button>
            </li>
        </ul>

        <!-- Contenido de Tabs -->
        <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade show active" id="identificacion" role="tabpanel" aria-labelledby="identificacion-tab">
                <div class="card">
                    <div class="card-body">
                        <h2 class="card-title">Identificación de Procesos Clave</h2>
                        <p class="card-text text-muted">Gestiona los procesos clave de la organización</p>

                        <asp:GridView ID="gvProcesosClave" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre del Proceso" SortExpression="Nombre" />
                                <asp:BoundField DataField="Propietario" HeaderText="Propietario" SortExpression="Propietario" />
                                <asp:BoundField DataField="Objetivo" HeaderText="Objetivos" SortExpression="Objetivo" />
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

                        <asp:Button ID="btnAddProcess" runat="server" Text="Añadir Proceso" CssClass="btn btn-primary mt-3" data-bs-toggle="modal" data-bs-target="#addProcessModal" />
                    </div>
                </div>
            </div>
        </div>
          <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
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

        <!-- Modal para editar proceso -->
        <div class="modal fade" id="editProcessModal" tabindex="-1" role="dialog" aria-labelledby="editProcessModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="editProcessModalLabel">Editar Proceso</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtEditNombre" runat="server" CssClass="form-control" Placeholder="Nombre del Proceso" ></asp:TextBox>
                        <asp:TextBox ID="txtEditPropietario" runat="server" CssClass="form-control mt-2" Placeholder="Propietario" ></asp:TextBox>
                        <asp:TextBox ID="txtEditObjetivo" runat="server" CssClass="form-control mt-2" Placeholder="Objetivo"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Actualizar Proceso" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    
        <!-- Modal para agregar proceso -->
        <div class="modal fade" id="addProcessModal" tabindex="-1" role="dialog" aria-labelledby="addProcessModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="addProcessModalLabel">Agregar Proceso</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre del Proceso" ></asp:TextBox>
                        <asp:TextBox ID="txtPropietario" runat="server" CssClass="form-control mt-2" Placeholder="Propietario" ></asp:TextBox>
                        <asp:TextBox ID="txtObjetivo" runat="server" CssClass="form-control mt-2" Placeholder="Objetivo" ></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGuardar" runat="server" Text="Agregar Proceso" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
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
                            <asp:DropDownList ID="ddlEvalProcess" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label for="txtImprovements" class="form-label">Sugerencias de Mejora</label>
                            <asp:TextBox ID="txtImprovements" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>

                        <asp:Button ID="btnSaveEvaluation" runat="server" Text="Guardar Evaluación" CssClass="btn btn-primary" OnClick="btnSaveEvaluation_Click" />
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h2 class="card-title">Seguimiento de Cambios y Auditorías</h2>
                        <p class="card-text text-muted">Registra cambios y realiza auditorías internas</p>

                        <asp:GridView ID="gvCambios" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:d}" SortExpression="Fecha" />
                                <asp:BoundField DataField="Proceso" HeaderText="Proceso" SortExpression="Proceso" />
                                <asp:BoundField DataField="Cambio" HeaderText="Cambio" SortExpression="Cambio" />
                                <asp:BoundField DataField="Responsable" HeaderText="Responsable" SortExpression="Responsable" />
                            </Columns>
                        </asp:GridView>

                        <asp:Button ID="btnProgramarAuditoria" runat="server" Text="Programar Auditoría" CssClass="btn btn-primary mt-3" OnClick="btnProgramarAuditoria_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    
</asp:Content>
