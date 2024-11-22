using Microsoft.EntityFrameworkCore;
using pelis.Models;
using System.Security.Cryptography;
using System.Text;

namespace pelis.Data
{
    public class Logic
    {
        private readonly pelisContext _context;

        public Logic(pelisContext context)
        {
            _context = context;
        }

        public static string EncriptarClave(string clave)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(clave));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public async Task<List<UsuariosSistema>> ListaUsuarios()
        {
            return await _context.UsuariosSistema.ToListAsync();
        }


        public async Task<UsuariosSistema> ValidarUsuario(string _username, string _password)
        {
            // Encriptar la contraseña ingresada
            var passwordHash = EncriptarClave(_password);

            // Buscar el usuario directamente en la base de datos de forma asíncrona
            return await _context.UsuariosSistema
                .FirstOrDefaultAsync(u => u.Username == _username && u.PasswordHash == passwordHash);
        }
    }
}
