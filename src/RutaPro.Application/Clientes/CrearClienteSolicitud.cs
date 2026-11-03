namespace RutaPro.Application.Clientes;

public sealed record CrearClienteSolicitud(
    string Codigo,
    string RazonSocial,
    string NitDocumento,
    string? Telefono = null,
    string? Direccion = null,
    string? Email = null);
