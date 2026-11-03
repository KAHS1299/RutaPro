using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Keyless]
public partial class VwFacturasYcliente
{
    public int FacturaId { get; set; }

    [StringLength(30)]
    public string NumeroFactura { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaEmision { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Impuestos { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Total { get; set; }

    [StringLength(20)]
    public string EstadoFactura { get; set; } = null!;

    public int OrdenServicioId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaSolicitud { get; set; }

    public int ClienteId { get; set; }

    [StringLength(150)]
    public string Cliente { get; set; } = null!;

    [Column("NIT")]
    [StringLength(30)]
    public string Nit { get; set; } = null!;

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(30)]
    public string? Telefono { get; set; }
}
