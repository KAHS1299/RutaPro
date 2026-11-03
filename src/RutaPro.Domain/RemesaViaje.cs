namespace RutaPro.Domain;

public sealed class RemesaViaje
{
    internal RemesaViaje(Remesa remesa, Viaje viaje)
    {
        Remesa = remesa;
        Viaje = viaje;
    }

    public Remesa Remesa { get; }

    public Viaje Viaje { get; }
}
