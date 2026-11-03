using RutaPro.Domain;

namespace RutaPro.Application.Clientes;

public interface IClienteRepository
{
    Task<bool> ExisteCodigoAsync(string codigo, CancellationToken cancellationToken);

    Task AgregarAsync(Cliente cliente, CancellationToken cancellationToken);
}
