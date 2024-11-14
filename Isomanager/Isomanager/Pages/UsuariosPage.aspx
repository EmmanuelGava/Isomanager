<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/UsuariosPage.aspx.cs" Inherits="Isomanager.Pages.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="text-center">Gestión de Usuarios</h2>
        <hr />

        <!-- Gráficos -->
        <div class="row mb-4">
            <div class="col-md-6">
                <h4>Tendencia de Desempeño</h4>
                <canvas id="desempenoChart" width="400" height="200"></canvas>
            </div>
            <div class="col-md-6">
                <h4>Horas de Formación por Área</h4>
                <canvas id="formacionChart" width="400" height="200"></canvas>
            </div>
        </div>

        <!-- Formulario para agregar nuevo usuario -->
        <div class="card mb-4">
            <div class="card-header">Agregar Nuevo Usuario</div>
            <div class="card-body">
                <asp:Panel ID="pnlAgregarUsuario" runat="server">
                    <div class="form-group">
                        <label for="txtNombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre completo"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="Nombre es requerido" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Correo electrónico"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email es requerido" CssClass="text-danger" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido" CssClass="text-danger" Display="Dynamic" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                    </div>
                    <div class="form-group">
                        <label for="txtRol">Rol</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Seleccionar rol" Value="" />
                            <asp:ListItem Text="Administrador" Value="Administrador" />
                            <asp:ListItem Text="Usuario" Value="Usuario" />
                            <asp:ListItem Text="Supervisor" Value="Supervisor" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol" InitialValue="" ErrorMessage="Rol es requerido" CssClass="text-danger" Display="Dynamic" />
                    </div>
                    <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar Usuario" CssClass="btn btn-primary" OnClick="btnAgregarUsuario_Click" />
                </asp:Panel>
            </div>
        </div>

        <!-- Tabla para mostrar usuarios existentes -->
        <div class="card">
            <div class="card-header">Lista de Usuarios</div>
            <div class="card-body">
                <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False" DataKeyNames="UsuarioId,Email" OnRowCommand="gvUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="d-flex justify-content-start">
                                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-warning me-2" />
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-danger" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        // Gráfico de Tendencia de Desempeño
        var ctx1 = document.getElementById('desempenoChart').getContext('2d');
        var desempenoChart = new Chart(ctx1, {
            type: 'line',
            data: {
                labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun'],
                datasets: [{
                    label: 'Promedio de Desempeño',
                    data: [90, 92, 88, 95, 93, 97],
                    backgroundColor: 'rgba(0, 123, 255, 0.2)',
                    borderColor: 'rgba(0, 123, 255, 1)',
                    borderWidth: 2
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });

        // Gráfico de Horas de Formación por Área
        var ctx2 = document.getElementById('formacionChart').getContext('2d');
        var formacionChart = new Chart(ctx2, {
            type: 'bar',
            data: {
                labels: ['Calidad', 'Seguridad', 'Ambiental', 'Procesos', 'Liderazgo'],
                datasets: [{
                    label: 'Horas de Formación',
                    data: [20, 15, 25, 30, 10],
                    backgroundColor: 'rgba(0, 123, 255, 0.5)',
                    borderColor: 'rgba(0, 123, 255, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    </script>
</asp:Content>