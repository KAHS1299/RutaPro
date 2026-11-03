using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Table("EvidenciasRemesa")]
public partial class EvidenciasRemesa
{
    [Key]
    public long Id { get; set; }

    public int RemesaId { get; set; }

    [StringLength(255)]
    public string NombreArchivo { get; set; } = null!;

    [Column("UrlOContenido")]
    public string? UrlOcontenido { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaSubida { get; set; }

    [ForeignKey("RemesaId")]
    [InverseProperty("EvidenciasRemesas")]
    public virtual Remesa Remesa { get; set; } = null!;
}
