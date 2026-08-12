using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppTask.Models
{
    [Table("Projeto")]
    public class Projeto
    {
        [Key]
        public int Codigo { get; set; }
        public string NomeProjeto { get; set; } = "";
        public decimal Orcamento {  get; set; }
        public string Status { get; set; } = "";
    }
}
