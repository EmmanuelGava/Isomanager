﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Pages/Factores.aspx.cs" Inherits="Isomanager.Pages.Factores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h1>Agregar Factor Externo</h1>
            <div class="mb-3">
                <label for="tipoFactor" class="form-label">Tipo de Factor Externo</label>
                <asp:DropDownList ID="tipoFactor" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione un tipo" Value="" />
                    <asp:ListItem Text="Económico" Value="Económico" />
                    <asp:ListItem Text="Político" Value="Político" />
                    <asp:ListItem Text="Social" Value="Social" />
                    <asp:ListItem Text="Tecnológico" Value="Tecnológico" />
                    <asp:ListItem Text="Ambiental" Value="Ambiental" />
                    <asp:ListItem Text="Legal" Value="Legal" />
                   
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="descripcion" class="form-label">Descripción del Factor</label>
                <textarea id="descripcion" class="form-control" runat="server"></textarea>
            </div>
            <div class="mb-3">
                <label for="impacto" class="form-label">Nivel de Impacto</label>
                <asp:DropDownList ID="impacto" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione el impacto" Value="" />
                    <asp:ListItem Text="Alto" Value="Alto" />
                    <asp:ListItem Text="Medio" Value="Medio" />
                    <asp:ListItem Text="Bajo" Value="Bajo" />
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="probabilidad" class="form-label">Probabilidad</label>
                <asp:DropDownList ID="probabilidad" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione la probabilidad" Value="" />
                    <asp:ListItem Text="Alta" Value="Alta" />
                    <asp:ListItem Text="Media" Value="Media" />
                    <asp:ListItem Text="Baja" Value="Baja" />
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="accionesSugeridas" class="form-label">Acciones Sugeridas</label>
                <textarea id="accionesSugeridas" class="form-control" runat="server"></textarea>
            </div>
            <div class="mb-3">
                <label for="fechaCreacion" class="form-label">Fecha de Creación</label>
                <input type="date" id="fechaCreacion" class="form-control" runat="server" />
            </div>
            <div class="mb-3">
                <label for="responsable" class="form-label">Responsable</label>
                <input type="text" id="responsable" class="form-control" runat="server" />
            </div>
            <button type="submit" class="btn btn-primary" runat="server" onserverclick="factorForm_Submit">Agregar Factor</button>

    </div>
   

</asp:Content>