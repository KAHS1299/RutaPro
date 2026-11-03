using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Keyless]
public partial class VwViajesYmanifiesto
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

    public int? ManifiestoId { get; set; }

    [Column("CodigoRadicadoRNDC")]
    [StringLength(50)]
    public string? CodigoRadicadoRndc { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? ValorFleteConductor { get; set; }

    [StringLength(20)]
    public string? EstadoManifiesto { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaExpedicion { get; set; }

    public int VehiculoId { get; set; }

    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeActual { get; set; }

    [StringLength(20)]
    public string EstadoVehiculo { get; set; } = null!;

    [StringLength(50)]
    public string TipoCarga { get; set; } = null!;

    public int ConductorId { get; set; }

    [StringLength(150)]
    public string Conductor { get; set; } = null!;

    [StringLength(50)]
    public string NumeroLicencia { get; set; } = null!;

    [StringLength(30)]
    public string? TelefonoConductor { get; set; }

    public int RemesaId { get; set; }

    [StringLength(50)]
    public string NumeroRemesa { get; set; } = null!;

    [StringLength(255)]
    public string DescripcionMercancia { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal PesoTotal { get; set; }

    public int OrdenServicioId { get; set; }

    [StringLength(20)]
    public string EstadoOrdenServicio { get; set; } = null!;

    [StringLength(150)]
    public string Cliente { get; set; } = null!;

    [Column("NIT")]
    [StringLength(30)]
    public string Nit { get; set; } = null!;

    [StringLength(100)]
    public string Origen { get; set; } = null!;

    [StringLength(100)]
    public string Destino { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DistanciaKm { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal TarifaBase { get; set; }
}
