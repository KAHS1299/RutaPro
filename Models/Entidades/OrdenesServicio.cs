using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Table("OrdenesServicio")]
public partial class OrdenesServicio
{
    [Key]
    public int Id { get; set; }

    public int ClienteId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaSolicitud { get; set; }

    [StringLength(20)]
    public string Estado { get; set; } = null!;

    public string? Observaciones { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("OrdenesServicios")]
    public virtual Cliente Cliente { get; set; } = null!;

    [InverseProperty("OrdenServicio")]
    public virtual Factura? Factura { get; set; }

    [InverseProperty("OrdenServicio")]
    public virtual ICollection<Remesa> Remesas { get; set; } = new List<Remesa>();
}
