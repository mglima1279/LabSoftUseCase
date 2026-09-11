using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProJ.Models;

[Table("Consulta")]
public partial class Consulta
{
    public int Codigo { get; set; }

    public DateTime DataHora { get; set; }

    public string StatusConsulta { get; set; } = "Agendada";

    public int PacienteId { get; set; }

    public int MedicoId { get; set; }

    public virtual Medico? Medico { get; set; }

    public virtual Paciente? Paciente { get; set; }
}
