using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace CityPointRoomHire.Models
{
    public class Installations
    {
        [Key]
        public int InstallationsId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Foreign key to IdentityUser
        public string UserId { get; set; }  // IdentityUser uses string as ID by default
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
