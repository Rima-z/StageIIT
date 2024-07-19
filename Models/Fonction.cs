using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iit.Models
{
    public class Fonction
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodeF { get; set; }

        [Required]
        public string Libelle { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public string Options { get; set; }

        public bool Autorisation { get; set; }

        public ICollection<Droits> Droits { get; set; }
    }
}
