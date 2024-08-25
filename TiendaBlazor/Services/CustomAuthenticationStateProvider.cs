using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TiendaBlazor.Data;
using Microsoft.EntityFrameworkCore;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ApplicationDbContext _context;

    public CustomAuthenticationStateProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsIdentity = new ClaimsIdentity();

        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == "admin@correo.com");

        if (user != null)
        {
            claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Correo),
                new Claim(ClaimTypes.Role, user.Rol),
            }, "CustomAuthentication");
        }

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public void MarkUserAsAuthenticated(string correo)
    {
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, correo),
        }, "apiauth_type");

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    public void MarkUserAsLoggedOut()
    {
        var claimsIdentity = new ClaimsIdentity();
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}
