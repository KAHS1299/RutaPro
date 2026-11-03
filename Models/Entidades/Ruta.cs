using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

public partial class Ruta
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Origen { get; set; } = null!;

    [StringLength(100)]
    public string Destino { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DistanciaKm { get; set; }

    [Column(TypeName = "decimal(14, 2)")]
    public decimal TarifaBase { get; set; }

    [InverseProperty("Ruta")]
    public virtual ICollection<Remesa> Remesas { get; set; } = new List<Remesa>();
}
