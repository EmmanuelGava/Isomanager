<%@ Page Title="Objetivo y Alcance Detallado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/ObjetivoAlcanceDetallado.aspx.cs" Inherits="Isomanager.Pages.ObjetivoAlcanceDetallado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h1 class="text-center mb-4">Objetivo y Alcance Detallado del Sistema de Gestión</h1>

        <!-- Objetivo del Sistema de Gestión -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Objetivo del Sistema de Gestión</h5>

                <!-- Propósito Principal -->
                <div class="mb-3">
                    <label for="txtPropositoPrincipal" class="form-label">Propósito Principal</label>
                    <asp:TextBox ID="txtPropositoPrincipal" runat="server" CssClass="form-control" MaxLength="500" TextMode="MultiLine"  placeholder="Ejemplo: Mejorar la calidad del producto..."></asp:TextBox>
                </div>

                <!-- Resultados Esperados -->
                <div class="mb-3">
                    <label for="txtResultadosEsperados" class="form-label">Resultados Esperados</label>
                    <asp:TextBox ID="txtResultadosEsperados" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="Ejemplo: Aumentar la satisfacción del cliente en un 20%..."></asp:TextBox>
                </div>
            </div>
        </div>

        <!-- Alcance del Sistema de Gestión -->
        <div class="card mb-4">
            <div class="card-body">
                <h5 class="card-title">Alcance del Sistema de Gestión</h5>

                   <!-- Lista de Áreas -->
            <div class="card mb-4">
                <div class="card-body">
                    <h5 class="card-title">Áreas y Limitaciones</h5>
                    <asp:ListView ID="lvAreas" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between align-items-center">
                                <span><%# Eval("Nombre") %></span>
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandArgument='<%# Eval("AreaId") %>' OnClick="btnEliminar_Click" CssClass="btn btn-danger btn-sm" />
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    
                    <asp:TextBox ID="txtNuevaArea" runat="server" CssClass="form-control mt-2" placeholder="Agregar nueva área..." />
                    <asp:Button ID="btnAgregarArea" runat="server" Text="Agregar Área" OnClick="btnAgregarArea_Click" CssClass="btn btn-success mt-2" />
                </div>
            </div>

                <!-- Mensaje de confirmación -->
            <div class="mt-4">
                <asp:Label ID="lblSuccess" runat="server" CssClass="text-success"></asp:Label>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
            </div>

                <!-- Limitaciones o Exclusiones -->
                <div class="mb-3">
                    <label for="txtLimitaciones" class="form-label">Limitaciones o Exclusiones</label>
                    <asp:TextBox ID="txtLimitaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="Ejemplo: Departamento de Marketing excluido..."></asp:TextBox>
                </div>

                <!-- Ubicación Geográfica o Sedes -->
                <div class="mb-3">
                    <label for="txtUbicaciones" class="form-label">Ubicación Geográfica o Sedes</label>
                    <asp:TextBox ID="txtUbicaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500" placeholder="Ejemplo: Sede Principal, Planta 1..."></asp:TextBox>
                </div>
            </div>
        </div>

        <!-- Botón de Guardado -->
        <div class="text-center">
            <asp:Button ID="btnGuardarDefinicionDetallada" runat="server" Text="Guardar Definición" CssClass="btn btn-primary" onclick="btnGuardarDefinicion_Click" />
        </div>
    </div>
</asp:Content>
