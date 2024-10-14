<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Pages/CrearNorma.aspx.cs" Inherits="Isomanager.Pages.CrearNorma" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Crear Norma Nueva</h2>

       <div class="form-group">
    <label for="Titulo">Título de la Norma:</label>
    <input type="text" class="form-control" id="Titulo" runat="server" />
</div>

<div class="form-group">
    <label for="Descripcion">Descripción:</label>
    <input type="text" class="form-control" id="Descripcion" runat="server" />
</div>

<div class="form-group">
    <label for="Version">Versión:</label>
    <input type="text" class="form-control" id="Version" runat="server" />
</div>

<div class="form-group">
    <label for="Status">Estado:</label>
    <asp:DropDownList ID="Status" runat="server" CssClass="form-control">
        <asp:ListItem Text="Valido" Value="Valido" />
        <asp:ListItem Text="En revisión" Value="Revision" />
        <asp:ListItem Text="Obsoleto" Value="Obsoleto" />
    </asp:DropDownList>
</div>

<div class="form-group">
    <label for="ResponsiblePerson">Persona Responsable:</label>
    <input type="text" class="form-control" id="ResponsiblePerson" runat="server" />
</div>
        </div>

<!-- Campo oculto para almacenar el ID de la norma (que será igual al contexto) -->
<asp:HiddenField ID="Contexto" runat="server" />

<asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSave_Click" />

</asp:Content>
