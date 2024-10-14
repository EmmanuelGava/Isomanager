<%@ Page Title="Auditorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/Auditorias.aspx.cs" Inherits="Isomanager.Pages.Auditorias" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Auditorías</h2>

        <!-- Formulario para registrar una nueva auditoría -->
        <h3>Registrar Nueva Auditoría</h3>
        <div class="form-group">
            <label for="NormaAuditada">Norma Auditada</label>
            <input type="text" class="form-control" id="NormaAuditada" runat="server" />
        </div>
        <div class="form-group">
            <label for="FechaAuditoria">Fecha de la Auditoría</label>
            <input type="date" class="form-control" id="FechaAuditoria" runat="server" />
        </div>
        <div class="form-group">
            <label for="AuditorResponsable">Auditor Responsable</label>
            <input type="text" class="form-control" id="AuditorResponsable" runat="server" />
        </div>
        <div class="form-group">
            <label for="EstadoAuditoria">Estado de la Auditoría</label>
            <select class="form-control" id="EstadoAuditoria" runat="server">
                <option value="Completada">Completada</option>
                <option value="Pendiente">Pendiente</option>
                <option value="En Progreso">En Progreso</option>
            </select>
        </div>
        <asp:Button ID="btnRegistrarAuditoria" runat="server" Text="Registrar Auditoría" CssClass="btn btn-success" OnClick="RegistrarAuditoria_Click" />

        <!-- Visualización de Auditorías -->
        <h3>Auditorías Registradas</h3>
        <asp:GridView ID="gvAuditorias" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="Norma" HeaderText="Norma Auditada" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha de Auditoría" />
                <asp:BoundField DataField="Auditor" HeaderText="Auditor Responsable" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
