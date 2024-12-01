using TiendaBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using TiendaBlazor.Data;

namespace TiendaBlazor.Pages
{
    public partial class Register
    {
        private string nombre; 
        private string email;
        private string contraseña;
        private string confirmarContraseña;
        private bool registrationFailed;
        private bool passwordsDoNotMatch;

        [Inject]
        private ApplicationDbContext _context { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        private async Task HandleRegister()
        {
            passwordsDoNotMatch = contraseña != confirmarContraseña;

            if (!passwordsDoNotMatch)
            {
                try
                {
                    var newUser = new Usuario
                    {
                        Nombre = nombre,
                        Email = email,
                        Contraseña = contraseña,
                        Rol = "Cliente"
                    };

                    _context.Usuarios.Add(newUser);
                    await _context.SaveChangesAsync();

                    Navigation.NavigateTo("/login");
                }
                catch (DbUpdateException dbEx)
                {
                    registrationFailed = true;
                    Console.WriteLine($"Error en la base de datos: {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    registrationFailed = true;
                    Console.WriteLine($"Error general: {ex.Message}");
                }
            }
        }

    }
}

