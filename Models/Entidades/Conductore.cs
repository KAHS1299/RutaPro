using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("NumeroLicencia", Name = "UQ_Conductores_NumeroLicencia", IsUnique = true)]
public partial class Conductore
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(50)]
    public string NumeroLicencia { get; set; } = null!;

    [StringLength(30)]
    public string? Telefono { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Conductor")]
    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
