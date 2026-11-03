using RutaPro.Domain;

namespace RutaPro.Application.Clientes;

public enum ErrorCrearCliente
{
    CodigoObligatorio,
    RazonSocialObligatoria,
    NitDocumentoObligatorio,
    CodigoDuplicado
}

public sealed record ResultadoCrearCliente(
    bool EsExitoso,
    Cliente? Cliente,
    ErrorCrearCliente? Error);
