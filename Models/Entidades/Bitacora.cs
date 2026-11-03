using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Table("Bitacora")]
[Index("Fecha", Name = "IX_Bitacora_Fecha", AllDescending = true)]
public partial class Bitacora
{
    [Key]
    public long Id { get; set; }

    public int? UsuarioId { get; set; }

    [StringLength(150)]
    public string Accion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    public string? Detalles { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("Bitacoras")]
    public virtual Usuario? Usuario { get; set; }
}
