using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppTask.Models;

public partial class Projeto
{
    [Key]
    public int Codigo { get; set; }

    public string NomeProjeto { get; set; } = null!;

    public decimal Orcamento { get; set; }

    public string Status { get; set; } = null!;
}
