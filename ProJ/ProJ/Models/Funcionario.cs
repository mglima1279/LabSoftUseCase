using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProJ.Models;

public partial class Funcionario
{
    [Key]
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    [ValidateNever]
    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}
