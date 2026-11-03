using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("CodigoRadicadoRndc", Name = "IX_Manifiestos_RNDC")]
[Index("ViajeId", Name = "UQ_Manifiestos_ViajeId", IsUnique = true)]
public partial class Manifiesto
{
    [Key]
    public int Id { get; set; }

    public int ViajeId { get; set; }

    [Column("CodigoRadicadoRNDC")]
    [StringLength(50)]
    public string? CodigoRadicadoRndc { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal ValorFleteConductor { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaExpedicion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCumplimiento { get; set; }

    [ForeignKey("ViajeId")]
    [InverseProperty("Manifiesto")]
    public virtual Viaje Viaje { get; set; } = null!;
}
