using RutaPro.Domain;

namespace RutaPro.Domain.Tests;

public class RemesaViajeTests
{
    [Fact]
    public void Crear_asociacion_conserva_la_remesa_y_el_viaje()
    {
        var remesa = new Remesa();
        var viaje = new Viaje();

        var asociacion = remesa.AsociarA(viaje);

        Assert.Same(remesa, asociacion.Remesa);
        Assert.Same(viaje, asociacion.Viaje);
    }

    [Fact]
    public void Una_remesa_puede_asociarse_con_dos_viajes_diferentes()
    {
        var remesa = new Remesa();
        var primerViaje = new Viaje();
        var segundoViaje = new Viaje();

        remesa.AsociarA(primerViaje);
        remesa.AsociarA(segundoViaje);

        Assert.Equal(2, remesa.Asociaciones.Count);
        Assert.Contains(remesa.Asociaciones, asociacion => asociacion.Viaje == primerViaje);
        Assert.Contains(remesa.Asociaciones, asociacion => asociacion.Viaje == segundoViaje);
    }

    [Fact]
    public void Dos_remesas_pueden_asociarse_con_un_mismo_viaje()
    {
        var primeraRemesa = new Remesa();
        var segundaRemesa = new Remesa();
        var viaje = new Viaje();

        primeraRemesa.AsociarA(viaje);
        segundaRemesa.AsociarA(viaje);

        Assert.Equal(2, viaje.Asociaciones.Count);
        Assert.Contains(viaje.Asociaciones, asociacion => asociacion.Remesa == primeraRemesa);
        Assert.Contains(viaje.Asociaciones, asociacion => asociacion.Remesa == segundaRemesa);
    }

    [Fact]
    public void Las_asociaciones_se_pueden_consultar_desde_ambos_extremos()
    {
        var remesa = new Remesa();
        var viaje = new Viaje();

        var asociacion = remesa.AsociarA(viaje);

        Assert.Contains(asociacion, remesa.Asociaciones);
        Assert.Contains(asociacion, viaje.Asociaciones);
    }

    [Fact]
    public void Asociar_un_viaje_nulo_lanza_excepcion_sin_modificar_las_asociaciones()
    {
        var remesa = new Remesa();
        var viajeExistente = new Viaje();
        var asociacionExistente = remesa.AsociarA(viajeExistente);

        Assert.Throws<ArgumentNullException>(() => remesa.AsociarA(null!));

        Assert.Collection(remesa.Asociaciones, asociacion => Assert.Same(asociacionExistente, asociacion));
        Assert.Collection(viajeExistente.Asociaciones, asociacion => Assert.Same(asociacionExistente, asociacion));
    }

    [Fact]
    public void Las_colecciones_de_asociaciones_no_admiten_modificaciones_externas()
    {
        var remesa = new Remesa();
        var viaje = new Viaje();
        var asociacion = remesa.AsociarA(viaje);
        var asociacionesRemesa = Assert.IsAssignableFrom<ICollection<RemesaViaje>>(remesa.Asociaciones);
        var asociacionesViaje = Assert.IsAssignableFrom<ICollection<RemesaViaje>>(viaje.Asociaciones);

        Assert.True(asociacionesRemesa.IsReadOnly);
        Assert.True(asociacionesViaje.IsReadOnly);
        Assert.Throws<NotSupportedException>(() => asociacionesRemesa.Add(asociacion));
        Assert.Throws<NotSupportedException>(() => asociacionesViaje.Add(asociacion));
        Assert.Single(remesa.Asociaciones);
        Assert.Single(viaje.Asociaciones);
    }
}
