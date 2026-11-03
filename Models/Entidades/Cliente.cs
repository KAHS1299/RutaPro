using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransportesERP.Api.Models;

[Index("NitDocumento", Name = "UQ_Clientes_NIT_Documento", IsUnique = true)]
public partial class Cliente
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string RazonSocial { get; set; } = null!;

    [Column("NIT_Documento")]
    [StringLength(30)]
    public string NitDocumento { get; set; } = null!;

    [StringLength(30)]
    public string? Telefono { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("Cliente")]
    public virtual ICollection<OrdenesServicio> OrdenesServicios { get; set; } = new List<OrdenesServicio>();
}
