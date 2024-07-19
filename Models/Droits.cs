using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iit.Models
{
    public class Droits
    {
        [Key]
        public int Id { get; set; }

        public string CodPrf { get; set; }
        [ForeignKey("CodPrf")]
        public ProfilFonction ProfilFonction { get; set; }

        public int CodeF { get; set; }
        [ForeignKey("CodeF")]
        public Fonction Fonction { get; set; }
    }
}
