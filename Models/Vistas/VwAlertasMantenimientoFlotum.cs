using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Keyless]
public partial class VwAlertasMantenimientoFlotum
{
    public int VehiculoId { get; set; }

    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [StringLength(50)]
    public string TipoCarga { get; set; } = null!;

    [StringLength(20)]
    public string EstadoVehiculo { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeActual { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeUltimoMantenimiento { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal FrecuenciaMantenimientoKm { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal? KmRecorridosDesdeUltimo { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal? KmRestantesParaMantenimiento { get; set; }

    [StringLength(34)]
    [Unicode(false)]
    public string EstadoAlertaMantenimiento { get; set; } = null!;
}
