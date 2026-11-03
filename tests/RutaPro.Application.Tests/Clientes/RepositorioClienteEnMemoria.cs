using RutaPro.Application.Clientes;
using RutaPro.Domain;

namespace RutaPro.Application.Tests.Clientes;

internal sealed class RepositorioClienteEnMemoria : IClienteRepository
{
    private readonly List<Cliente> _clientes = [];

    public IReadOnlyCollection<Cliente> Clientes => _clientes.AsReadOnly();

    public Task<bool> ExisteCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        var existe = _clientes.Any(cliente => cliente.Codigo == codigo);
        return Task.FromResult(existe);
    }

    public Task AgregarAsync(Cliente cliente, CancellationToken cancellationToken)
    {
        _clientes.Add(cliente);
        return Task.CompletedTask;
    }

    public void AgregarExistente(Cliente cliente)
    {
        _clientes.Add(cliente);
    }
}
