using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

public partial class Mantenimiento
{
    [Key]
    public int Id { get; set; }

    public int VehiculoId { get; set; }

    public DateOnly FechaMantenimiento { get; set; }

    [StringLength(20)]
    public string Tipo { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeAlMomento { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Costo { get; set; }

    public string? Observaciones { get; set; }

    [ForeignKey("VehiculoId")]
    [InverseProperty("Mantenimientos")]
    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
