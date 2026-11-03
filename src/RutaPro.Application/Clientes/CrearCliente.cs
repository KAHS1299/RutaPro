using RutaPro.Domain;

namespace RutaPro.Application.Clientes;

public sealed class CrearCliente(IClienteRepository repositorio)
{
    public async Task<ResultadoCrearCliente> EjecutarAsync(
        CrearClienteSolicitud solicitud,
        CancellationToken cancellationToken = default)
    {
        Cliente cliente;

        try
        {
            cliente = Cliente.Crear(
                solicitud.Codigo,
                solicitud.RazonSocial,
                solicitud.NitDocumento,
                solicitud.Telefono,
                solicitud.Direccion,
                solicitud.Email);
        }
        catch (ArgumentException exception)
        {
            var error = MapearError(exception.ParamName);
            if (error is null)
            {
                throw;
            }

            return Rechazar(error.Value);
        }

        if (await repositorio.ExisteCodigoAsync(cliente.Codigo, cancellationToken))
        {
            return Rechazar(ErrorCrearCliente.CodigoDuplicado);
        }

        await repositorio.AgregarAsync(cliente, cancellationToken);

        return new ResultadoCrearCliente(true, cliente, null);
    }

    private static ErrorCrearCliente? MapearError(string? nombreParametro) => nombreParametro switch
    {
        "codigo" => ErrorCrearCliente.CodigoObligatorio,
        "razonSocial" => ErrorCrearCliente.RazonSocialObligatoria,
        "nitDocumento" => ErrorCrearCliente.NitDocumentoObligatorio,
        _ => null
    };

    private static ResultadoCrearCliente Rechazar(ErrorCrearCliente error) =>
        new(false, null, error);
}
