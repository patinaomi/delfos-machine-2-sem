
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using LexusTech.Infrastructure.Interfaces;
using LexusTech.Application.Services;
using LexusTech.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

// Registrar os serviços necessários

//Usuario -- Cadastro
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

//Clinica -- Cadastro
builder.Services.AddTransient<IClinicaService, ClinicaService>();
builder.Services.AddTransient<IClinicaRepository, ClinicaRepository>();


//Medico -- Cadastro
builder.Services.AddTransient<IMedicoService, MedicoService>();
builder.Services.AddTransient<IMedicoRepository, MedicoRepository>();

// Configurar autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Logar";
    });

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:5121");

// Aplica as migrações pendentes e cria o banco se necessário. Melhor do que eu esperar ter a tabela.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Aplica migrações e cria o banco se não existir
    context.Database.Migrate();  
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();