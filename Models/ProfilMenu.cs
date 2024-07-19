using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace iit.Models
{
    public class ProfilMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CodPrm { get; set; }
        public string Libelle { get; set; }
        public string Menu { get; set; } // Will contain a comma-separated list of CodeMenu
    }
}
