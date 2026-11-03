namespace RutaPro.Domain;

public sealed class Viaje
{
    private readonly List<RemesaViaje> _asociaciones = [];

    public IReadOnlyCollection<RemesaViaje> Asociaciones => _asociaciones.AsReadOnly();

    internal void AgregarAsociacion(RemesaViaje asociacion)
    {
        _asociaciones.Add(asociacion);
    }
}
