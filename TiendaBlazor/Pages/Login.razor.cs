using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TiendaBlazor.Data;

namespace TiendaBlazor.Pages
{
    public partial class Login
    {
        private string email;
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
                .FirstOrDefaultAsync(u => u.Email == email && u.Contraseña == contraseña);

            if (user != null)
            {
                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
                customAuthStateProvider.MarkUserAsAuthenticated(user.Email, user.Rol);

                if (user.Rol == "Admin")
                {
                    Navigation.NavigateTo("/admin-productos");
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
