using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Keyless]
public partial class VwMonitoreoFlotaGp
{
    public int VehiculoId { get; set; }

    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [StringLength(50)]
    public string TipoCarga { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal KilometrajeActual { get; set; }

    [StringLength(20)]
    public string EstadoVehiculo { get; set; } = null!;

    [Column(TypeName = "decimal(10, 7)")]
    public decimal? UltimaLatitud { get; set; }

    [Column(TypeName = "decimal(10, 7)")]
    public decimal? UltimaLongitud { get; set; }

    [Column("UltimoReporteGPS", TypeName = "datetime")]
    public DateTime? UltimoReporteGps { get; set; }
}
