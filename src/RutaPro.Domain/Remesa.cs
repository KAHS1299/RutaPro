namespace RutaPro.Domain;

public sealed class Remesa
{
    private readonly List<RemesaViaje> _asociaciones = [];

    public IReadOnlyCollection<RemesaViaje> Asociaciones => _asociaciones.AsReadOnly();

    public RemesaViaje AsociarA(Viaje viaje)
    {
        ArgumentNullException.ThrowIfNull(viaje);

        var asociacion = new RemesaViaje(this, viaje);

        _asociaciones.Add(asociacion);
        viaje.AgregarAsociacion(asociacion);

        return asociacion;
    }
}
