using RutaPro.Domain;

namespace RutaPro.Domain.Tests;

public class ClienteTests
{
    [Fact]
    public void Crear_cliente_genera_guid_y_estado_activo()
    {
        var antesDeCrear = DateTime.UtcNow;
        var cliente = Cliente.Crear(
            "CLI-001",
            "Cliente Ejemplo S.A.S.",
            "DOC-001",
            "3105550101",
            "Calle 1",
            "cliente@ejemplo.test");
        var despuesDeCrear = DateTime.UtcNow;

        Assert.NotEqual(Guid.Empty, cliente.Id);
        Assert.Equal(EstadoCliente.Activo, cliente.Estado);
        Assert.Equal("CLI-001", cliente.Codigo);
        Assert.Equal("Cliente Ejemplo S.A.S.", cliente.RazonSocial);
        Assert.Equal("DOC-001", cliente.NitDocumento);
        Assert.Equal("3105550101", cliente.Telefono);
        Assert.Equal("Calle 1", cliente.Direccion);
        Assert.Equal("cliente@ejemplo.test", cliente.Email);
        Assert.InRange(cliente.FechaRegistro, antesDeCrear, despuesDeCrear);
        Assert.Equal(DateTimeKind.Utc, cliente.FechaRegistro.Kind);
    }

    [Fact]
    public void Datos_opcionales_del_cliente_pueden_omitirse()
    {
        var cliente = Cliente.Crear("CLI-001", "Cliente Ejemplo", "DOC-001");

        Assert.Null(cliente.Telefono);
        Assert.Null(cliente.Direccion);
        Assert.Null(cliente.Email);
    }

    [Fact]
    public void Desactivar_cliente_cambia_su_estado_a_inactivo()
    {
        var cliente = Cliente.Crear("CLI-001", "Cliente Ejemplo", "DOC-001");

        cliente.Desactivar();

        Assert.Equal(EstadoCliente.Inactivo, cliente.Estado);
    }
}
