using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iit.Models
{
    public class ProfilFonction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CodPrf { get; set; }

        public string Libelle { get; set; }

        public ICollection<Droits> Droits { get; set; }
    }
}
