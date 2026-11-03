namespace RutaPro.Domain;

public sealed class Cliente
{
    private Cliente(
        Guid id,
        string codigo,
        string razonSocial,
        string nitDocumento,
        string? telefono,
        string? direccion,
        string? email,
        DateTime fechaRegistro)
    {
        Id = id;
        Codigo = codigo;
        RazonSocial = razonSocial;
        NitDocumento = nitDocumento;
        Telefono = telefono;
        Direccion = direccion;
        Email = email;
        FechaRegistro = fechaRegistro;
        Estado = EstadoCliente.Activo;
    }

    public Guid Id { get; }

    public string Codigo { get; }

    public string RazonSocial { get; }

    public string NitDocumento { get; }

    public string? Telefono { get; }

    public string? Direccion { get; }

    public string? Email { get; }

    public DateTime FechaRegistro { get; }

    public EstadoCliente Estado { get; private set; }

    public static Cliente Crear(
        string codigo,
        string razonSocial,
        string nitDocumento,
        string? telefono = null,
        string? direccion = null,
        string? email = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(codigo);
        ArgumentException.ThrowIfNullOrWhiteSpace(razonSocial);
        ArgumentException.ThrowIfNullOrWhiteSpace(nitDocumento);

        return new Cliente(
            Guid.NewGuid(),
            codigo,
            razonSocial,
            nitDocumento,
            telefono,
            direccion,
            email,
            DateTime.UtcNow);
    }

    public void Desactivar()
    {
        Estado = EstadoCliente.Inactivo;
    }
}
