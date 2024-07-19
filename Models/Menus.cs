using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iit.Models
{
    public class Menus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodeMenu { get; set; }
        public string Type { get; set; }
        public string Parent { get; set; }
        public string Libelle { get; set; }
        public String Rang { get; set; }
    }
}
