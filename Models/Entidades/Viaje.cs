using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("VehiculoId", "Estado", Name = "IX_Viajes_Vehiculo_Estado")]
[Index("RemesaId", Name = "UQ_Viajes_RemesaId", IsUnique = true)]
public partial class Viaje
{
    [Key]
    public int Id { get; set; }

    public int RemesaId { get; set; }

    public int VehiculoId { get; set; }

    public int ConductorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaSalida { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaLlegadaEstimada { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaLlegadaReal { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [ForeignKey("ConductorId")]
    [InverseProperty("Viajes")]
    public virtual Conductore Conductor { get; set; } = null!;

    [InverseProperty("Viaje")]
    public virtual Manifiesto? Manifiesto { get; set; }

    [ForeignKey("RemesaId")]
    [InverseProperty("Viaje")]
    public virtual Remesa Remesa { get; set; } = null!;

    [ForeignKey("VehiculoId")]
    [InverseProperty("Viajes")]
    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
