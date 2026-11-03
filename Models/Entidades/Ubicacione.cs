using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("VehiculoId", "FechaHoraReporte", Name = "IX_Ubicaciones_Vehiculo_Fecha", IsDescending = new[] { false, true })]
public partial class Ubicacione
{
    [Key]
    public long Id { get; set; }

    public int VehiculoId { get; set; }

    [Column(TypeName = "decimal(10, 7)")]
    public decimal Latitud { get; set; }

    [Column(TypeName = "decimal(10, 7)")]
    public decimal Longitud { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaHoraReporte { get; set; }

    [ForeignKey("VehiculoId")]
    [InverseProperty("Ubicaciones")]
    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
