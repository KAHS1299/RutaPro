using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Keyless]
public partial class VwViajesDetallado
{
    public int ViajeId { get; set; }

    [StringLength(20)]
    public string EstadoViaje { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaSalida { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaLlegadaEstimada { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaLlegadaReal { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal PesoCargaKg { get; set; }

    public int ClienteId { get; set; }

    [StringLength(150)]
    public string Cliente { get; set; } = null!;

    [Column("ClienteNIT")]
    [StringLength(30)]
    public string ClienteNit { get; set; } = null!;

    public int ConductorId { get; set; }

    [StringLength(150)]
    public string Conductor { get; set; } = null!;

    [StringLength(50)]
    public string NumeroLicencia { get; set; } = null!;

    [StringLength(30)]
    public string? TelefonoConductor { get; set; }

    public int VehiculoId { get; set; }

    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal CapacidadKg { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeActual { get; set; }

    [StringLength(20)]
    public string EstadoVehiculo { get; set; } = null!;

    [StringLength(50)]
    public string TipoCarga { get; set; } = null!;

    public int RutaId { get; set; }

    [StringLength(100)]
    public string Origen { get; set; } = null!;

    [StringLength(100)]
    public string Destino { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DistanciaKm { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal TarifaBase { get; set; }

    public int? FacturaId { get; set; }

    [StringLength(30)]
    public string? NumeroFactura { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaFactura { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? FacturaSubtotal { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? FacturaImpuestos { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? FacturaTotal { get; set; }

    [StringLength(20)]
    public string? EstadoFactura { get; set; }
}
