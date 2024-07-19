// Models/Utilisateur.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iit.Models
{
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodUsr { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string CodPrm { get; set; }
        public string CodPrf { get; set; }
        public bool Active { get; set; }
    }
}
