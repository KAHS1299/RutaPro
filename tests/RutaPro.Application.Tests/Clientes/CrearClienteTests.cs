using RutaPro.Application.Clientes;
using RutaPro.Domain;

namespace RutaPro.Application.Tests.Clientes;

public class CrearClienteTests
{
    [Fact]
    public async Task Crear_cliente_con_datos_validos_genera_id_y_estado_activo()
    {
        var repositorio = new RepositorioClienteEnMemoria();
        var casoDeUso = new CrearCliente(repositorio);
        var solicitud = new CrearClienteSolicitud(
            "CLI-001",
            "Transportes Ejemplo S.A.S.",
            "NIT-123",
            "3105550101",
            "Calle 1",
            "contacto@ejemplo.test");

        var resultado = await casoDeUso.EjecutarAsync(solicitud, CancellationToken.None);

        Assert.True(resultado.EsExitoso);
        Assert.Null(resultado.Error);
        Assert.NotNull(resultado.Cliente);
        Assert.NotEqual(Guid.Empty, resultado.Cliente.Id);
        Assert.Equal("CLI-001", resultado.Cliente.Codigo);
        Assert.Equal("Transportes Ejemplo S.A.S.", resultado.Cliente.RazonSocial);
        Assert.Equal("NIT-123", resultado.Cliente.NitDocumento);
        Assert.Equal("3105550101", resultado.Cliente.Telefono);
        Assert.Equal("Calle 1", resultado.Cliente.Direccion);
        Assert.Equal("contacto@ejemplo.test", resultado.Cliente.Email);
        Assert.NotEqual(default, resultado.Cliente.FechaRegistro);
        Assert.Equal(DateTimeKind.Utc, resultado.Cliente.FechaRegistro.Kind);
        Assert.Equal(EstadoCliente.Activo, resultado.Cliente.Estado);
        Assert.Single(repositorio.Clientes);
    }

    [Fact]
    public async Task Crear_cliente_con_codigo_duplicado_rechaza_la_operacion()
    {
        var repositorio = new RepositorioClienteEnMemoria();
        repositorio.AgregarExistente(Cliente.Crear("CLI-001", "Cliente existente", "DOC-001"));
        var casoDeUso = new CrearCliente(repositorio);
        var solicitud = new CrearClienteSolicitud("CLI-001", "Otro cliente", "DOC-002");

        var resultado = await casoDeUso.EjecutarAsync(solicitud, CancellationToken.None);

        Assert.False(resultado.EsExitoso);
        Assert.Null(resultado.Cliente);
        Assert.Equal(ErrorCrearCliente.CodigoDuplicado, resultado.Error);
        Assert.Single(repositorio.Clientes);
    }

    [Fact]
    public async Task Crear_cliente_con_codigo_vacio_rechaza_la_operacion()
    {
        var repositorio = new RepositorioClienteEnMemoria();
        var casoDeUso = new CrearCliente(repositorio);
        var solicitud = new CrearClienteSolicitud("", "Cliente válido", "DOC-001");

        var resultado = await casoDeUso.EjecutarAsync(solicitud, CancellationToken.None);

        Assert.False(resultado.EsExitoso);
        Assert.Null(resultado.Cliente);
        Assert.Equal(ErrorCrearCliente.CodigoObligatorio, resultado.Error);
        Assert.Empty(repositorio.Clientes);
    }

    [Fact]
    public async Task Crear_cliente_con_razon_social_vacia_rechaza_la_operacion()
    {
        var repositorio = new RepositorioClienteEnMemoria();
        var casoDeUso = new CrearCliente(repositorio);
        var solicitud = new CrearClienteSolicitud("CLI-001", "", "DOC-001");

        var resultado = await casoDeUso.EjecutarAsync(solicitud, CancellationToken.None);

        Assert.False(resultado.EsExitoso);
        Assert.Null(resultado.Cliente);
        Assert.Equal(ErrorCrearCliente.RazonSocialObligatoria, resultado.Error);
        Assert.Empty(repositorio.Clientes);
    }

    [Fact]
    public async Task Crear_clientes_con_el_mismo_nit_y_codigos_diferentes_es_permitido()
    {
        var repositorio = new RepositorioClienteEnMemoria();
        var casoDeUso = new CrearCliente(repositorio);

        var primerResultado = await casoDeUso.EjecutarAsync(
            new CrearClienteSolicitud("CLI-001", "Cliente Uno", "DOC-REPETIDO"),
            CancellationToken.None);
        var segundoResultado = await casoDeUso.EjecutarAsync(
            new CrearClienteSolicitud("CLI-002", "Cliente Dos", "DOC-REPETIDO"),
            CancellationToken.None);

        Assert.True(primerResultado.EsExitoso);
        Assert.True(segundoResultado.EsExitoso);
        Assert.Equal(2, repositorio.Clientes.Count);
    }

    [Fact]
    public async Task Crear_cliente_con_nit_documento_vacio_rechaza_la_operacion()
    {
        var repositorio = new RepositorioClienteEnMemoria();
        var casoDeUso = new CrearCliente(repositorio);
        var solicitud = new CrearClienteSolicitud("CLI-001", "Cliente válido", "");

        var resultado = await casoDeUso.EjecutarAsync(solicitud, CancellationToken.None);

        Assert.False(resultado.EsExitoso);
        Assert.Null(resultado.Cliente);
        Assert.Equal(ErrorCrearCliente.NitDocumentoObligatorio, resultado.Error);
        Assert.Empty(repositorio.Clientes);
    }
}
