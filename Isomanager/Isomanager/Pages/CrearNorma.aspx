<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Pages/CrearNorma.aspx.cs" Inherits="Isomanager.Pages.CrearNorma" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Crear Norma Nueva</h2>

        <div class="form-group">
            <label for="Titulo">Título de la Norma:</label>
            <asp:TextBox ID="Titulo" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="Version">Versión:</label>
            <asp:TextBox ID="Version" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="Estado">Estado:</label>
            <asp:DropDownList ID="Estado" runat="server" CssClass="form-control">
                <asp:ListItem Text="Valido" Value="Valido" />
                <asp:ListItem Text="En revisión" Value="Revision" />
                <asp:ListItem Text="Obsoleto" Value="Obsoleto" />
            </asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="Fecha">Fecha de Creación:</label>
            <asp:Label ID="lblFechaCreacion" runat="server" CssClass="form-control" />
        </div>

        <asp:UpdatePanel ID="UpdatePanelModal" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group">
                    <label for="Responsable">Responsable:</label>
                    <asp:DropDownList ID="ddlResponsable" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlResponsable_SelectedIndexChanged">
                        <asp:ListItem Text="Seleccione un responsable" Value="" />
                        <asp:ListItem Text="Agregar Nuevo Usuario" Value="nuevo" />
                    </asp:DropDownList>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAgregarUsuario" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
<!-- Modal de Bootstrap -->
<div class="modal fade" id="modalAgregarUsuario" tabindex="-1" aria-labelledby="modalAgregarUsuarioLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalAgregarUsuarioLabel">Agregar Nuevo Usuario</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <!-- Agrega ValidationSummary para mostrar errores de validación -->
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" 
                    ValidationGroup="AgregarUsuario" HeaderText="Errores:" />

                <!-- UpdatePanel que contiene los controles a actualizar dinámicamente -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!-- Nombre -->
                        <div class="form-group">
                            <label for="txtNombre">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre completo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" 
                                ErrorMessage="Nombre es requerido" CssClass="text-danger" Display="Dynamic" 
                                ValidationGroup="AgregarUsuario" />
                        </div>

                        <!-- Email -->
                        <div class="form-group">
                            <label for="txtEmail">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Correo electrónico"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" 
                                ErrorMessage="Email es requerido" CssClass="text-danger" Display="Dynamic" 
                                ValidationGroup="AgregarUsuario" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" 
                                ErrorMessage="Formato de email inválido" CssClass="text-danger" Display="Dynamic" 
                                ValidationGroup="AgregarUsuario" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                        </div>

                        <!-- Rol -->
                        <div class="form-group">
                            <label for="txtRol">Rol</label>
                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccionar rol" Value="" />
                                <asp:ListItem Text="Administrador" Value="Administrador" />
                                <asp:ListItem Text="Usuario" Value="Usuario" />
                                <asp:ListItem Text="Supervisor" Value="Supervisor" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol" 
                                InitialValue="" ErrorMessage="Rol es requerido" CssClass="text-danger" Display="Dynamic" 
                                ValidationGroup="AgregarUsuario" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <!-- Botones de acción -->
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                <asp:Button ID="btnAgregarUsuario" runat="server" Text="Guardar" CssClass="btn btn-primary" 
                    OnClick="btnAgregarUsuario_Click" ValidationGroup="AgregarUsuario" />
            </div>
        </div>
    </div>
</div>


        <asp:HiddenField ID="Contexto" runat="server" />
        <asp:Button ID="btnGuardarNorma" runat="server" Text="Guardar Norma" CssClass="btn btn-primary" OnClick="btnGuardarNorma_Click" />
    </div>




<asp:GridView ID="gvNormas" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False" OnRowCommand="gvNormas_RowCommand">
    <Columns>
        <asp:BoundField DataField="Titulo" HeaderText="Título" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        <asp:BoundField DataField="Version" HeaderText="Versión" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        <asp:BoundField DataField="Estado" HeaderText="Estado" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha de Creación" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        <asp:BoundField DataField="Responsable.Nombre" HeaderText="Responsable" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("NormaId") %>' CssClass="btn btn-warning btn-sm me-2" />
                    <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("NormaId") %>' CssClass="btn btn-danger btn-sm" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
