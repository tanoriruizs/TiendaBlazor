using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TiendaBlazor.Data;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claimsIdentity = new ClaimsIdentity();
        var userPrincipal = _httpContextAccessor.HttpContext?.User;

        if (userPrincipal?.Identity != null && userPrincipal.Identity.IsAuthenticated)
        {
            claimsIdentity = (ClaimsIdentity)userPrincipal.Identity;
        }

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return Task.FromResult(new AuthenticationState(claimsPrincipal));
    }

    public void MarkUserAsAuthenticated(string correo, string rol)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, correo),
        new Claim(ClaimTypes.Role, rol)  
    };

        var claimsIdentity = new ClaimsIdentity(claims, "apiauth_type");
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
