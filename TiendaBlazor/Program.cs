
using Microsoft.EntityFrameworkCore;
using TiendaBlazor.Data;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using TiendaBlazor.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ClientePolicy", policy => policy.RequireRole("Cliente"));
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProductoService>();


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitMaxRetained = 0;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Login");
    return Task.CompletedTask;
});


app.MapGet("/authenticate", async (HttpContext context, ApplicationDbContext db) =>
{
    var email = context.Request.Query["email"].ToString();
    var password = context.Request.Query["password"].ToString();

    var user = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Contraseña == password);

    if (user == null)
    {

        context.Response.Redirect("/Login?error=Credenciales incorrectas");
        return;
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Nombre),
        new Claim(ClaimTypes.Role, user.Rol)
    };

    var identity = new ClaimsIdentity(claims, "CookieAuth");
    var principal = new ClaimsPrincipal(identity);

    await context.SignInAsync("CookieAuth", principal);

    if (user.Rol == "Admin")
    {
        context.Response.Redirect("/admin");
    }
    else if (user.Rol == "Cliente")
    {
        context.Response.Redirect("/cliente");
    }
});


app.MapGet("/logout", async (HttpContext context) =>
{

    await context.SignOutAsync("CookieAuth");

    context.Response.Redirect("/Login");
});


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
