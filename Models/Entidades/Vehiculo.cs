using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("Placa", Name = "UQ_Vehiculos_Placa", IsUnique = true)]
public partial class Vehiculo
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal CapacidadKg { get; set; }

    public int TipoCargaId { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeActual { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeUltimoMantenimiento { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal FrecuenciaMantenimientoKm { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Vehiculo")]
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    [ForeignKey("TipoCargaId")]
    [InverseProperty("Vehiculos")]
    public virtual TiposCarga TipoCarga { get; set; } = null!;

    [InverseProperty("Vehiculo")]
    public virtual ICollection<Ubicacione> Ubicaciones { get; set; } = new List<Ubicacione>();

    [InverseProperty("Vehiculo")]
    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
