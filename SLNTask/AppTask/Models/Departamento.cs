using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTask.Models
{
    [Table("Departamento")]
    public partial class Departamento
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }
}
