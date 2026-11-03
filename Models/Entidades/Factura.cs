using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("NumeroFactura", Name = "UQ_Facturas_NumeroFactura", IsUnique = true)]
[Index("OrdenServicioId", Name = "UQ_Facturas_OrdenServicioId", IsUnique = true)]
public partial class Factura
{
    [Key]
    public int Id { get; set; }

    public int OrdenServicioId { get; set; }

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
    public string Estado { get; set; } = null!;

    [ForeignKey("OrdenServicioId")]
    [InverseProperty("Factura")]
    public virtual OrdenesServicio OrdenServicio { get; set; } = null!;
}
