using System.ComponentModel.DataAnnotations;

namespace pelis.Models
{
    public class UsuariosSistema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(50)]
        public string Rol { get; set; }
    }
}
