using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TiendaBlazor.Data;

namespace TiendaBlazor.Pages
{
    public partial class Login
    {
        private string correo;
        private string contraseña;
        private bool loginFailed;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private ApplicationDbContext _context { get; set; }

        private async Task HandleLogin()
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

            if (user != null)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                customAuthStateProvider.MarkUserAsAuthenticated(user.Correo, user.Rol);

                // Redirige al usuario según su rol
                if (user.Rol == "Admin")
                {
                    Navigation.NavigateTo("/admin-page");
                }
                else if (user.Rol == "Cliente")
                {
                    Navigation.NavigateTo("/cliente-page");
                }
            }
            else
            {
                loginFailed = true;
            }
        }
    }
}
