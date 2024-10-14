<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Normaspage.aspx.cs" Inherits="Isomanager.Normaspage" MasterPageFile="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2>Normas ISO</h2>

        <!-- Tarjetas de Normas ISO -->
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">Norma ISO 9001</div>
                    <div class="card-body">
                        <p class="card-text">Descripción de la norma ISO 9001.</p>
                        <a href="CrearNorma.aspx" class="btn btn-primary">Crear Norma Nueva</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-header">Norma ISO 14001</div>
                    <div class="card-body">
                        <p class="card-text">Descripción de la norma ISO 14001.</p>
                        <a href="CrearNorma.aspx" class="btn btn-primary">Crear Norma Nueva</a>
                    </div>
                </div>
            </div>
            <!-- Agrega más tarjetas según sea necesario -->
        </div>

        <hr />

        <h3>Documentos Creados</h3>
        <asp:GridView ID="GridViewNormas" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="DocumentId" HeaderText="ID del Documento" />
                <asp:BoundField DataField="Version" HeaderText="Versión" />
                <asp:BoundField DataField="Status" HeaderText="Estado" />
                <asp:BoundField DataField="ResponsiblePerson" HeaderText="Responsable" />
                <asp:BoundField DataField="LastModified" HeaderText="Última Modificación" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnModificar" runat="server" CssClass="btn btn-warning btn-sm"
                            Text="Modificar" PostBackUrl='<%# Eval("DocumentId", "ModificarNorma.aspx?id={0}") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
