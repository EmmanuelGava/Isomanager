<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModificarNorma.aspx.cs" Inherits="Isomanager.ModificarNorma" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Modificar Norma</h2>

        <!-- ID del Documento (solo lectura) -->
        <div class="form-group">
            <label for="DocumentId">ID del Documento:</label>
            <asp:TextBox ID="DocumentId" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <!-- Versión -->
        <div class="form-group">
            <label for="Version">Versión:</label>
            <asp:TextBox ID="Version" runat="server" CssClass="form-control" />
        </div>

        <!-- Estado (Dropdown) -->
        <div class="form-group">
            <label for="Status">Estado:</label>
            <asp:DropDownList ID="Status" runat="server" CssClass="form-control">
                <asp:ListItem Text="Valido" Value="Valido" />
                <asp:ListItem Text="En revisión" Value="EnRevision" />
                <asp:ListItem Text="Obsoleto" Value="Obsoleto" />
            </asp:DropDownList>
        </div>

        <!-- Persona Responsable -->
        <div class="form-group">
            <label for="ResponsiblePerson">Persona Responsable:</label>
            <asp:TextBox ID="ResponsiblePerson" runat="server" CssClass="form-control" />
        </div>

        <!-- Botón para Guardar Cambios -->
        <asp:Button ID="btnSave" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar Documento" OnClick="btnEliminar_Click"
            OnClientClick="return confirm('¿Está seguro de que desea eliminar este documento? Esta acción no se puede deshacer.');" />

        <!-- Mensaje de error o éxito -->
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    </div>
</asp:Content>
