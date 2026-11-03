using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("OrdenServicioId", Name = "IX_Remesas_OrdenServicio")]
[Index("NumeroRemesa", Name = "UQ_Remesas_Numero", IsUnique = true)]
public partial class Remesa
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string NumeroRemesa { get; set; } = null!;

    public int OrdenServicioId { get; set; }

    public int RutaId { get; set; }

    [StringLength(255)]
    public string DescripcionMercancia { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal PesoTotal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Remesa")]
    public virtual ICollection<EvidenciasRemesa> EvidenciasRemesas { get; set; } = new List<EvidenciasRemesa>();

    [ForeignKey("OrdenServicioId")]
    [InverseProperty("Remesas")]
    public virtual OrdenesServicio OrdenServicio { get; set; } = null!;

    [ForeignKey("RutaId")]
    [InverseProperty("Remesas")]
    public virtual Ruta Ruta { get; set; } = null!;

    [InverseProperty("Remesa")]
    public virtual Viaje? Viaje { get; set; }
}
