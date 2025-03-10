
using Microsoft.AspNetCore.Authentication.Cookies;
using Project.Infrastructure.Interfaces;
using Project.Application.Services;
using Project.Repositories;
using Project.Domain;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Project.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configuração do MongoDB
builder.Services.Configure<ConfigMongoDb>(builder.Configuration.GetSection("ConfigMongoDb"));

// Registra o cliente MongoDB como Transient, não Signton como o professor ensinou.
builder.Services.AddTransient<IMongoClient>(sp =>   
{
    var settings = sp.GetRequiredService<IOptions<ConfigMongoDb>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Registrar os serviços necessários

//Usuario -- Cadastro
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

// Login
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();

// Endereco
builder.Services.AddTransient<IEnderecoService, EnderecoService>();
builder.Services.AddTransient<IEnderecoRepository, EnderecoRepository>();

// Dias Preferencia
builder.Services.AddTransient<IDiasPreferenciaService, DiasPreferenciaService>();
builder.Services.AddTransient<IDiasPreferenciaRepository, DiasPreferenciaRepository>();

// Turno de preferencia
builder.Services.AddTransient<ITurnoService, TurnoService>();
builder.Services.AddTransient<ITurnoRepository, TurnoRepository>();

// Horários de preferencia
builder.Services.AddTransient<IHorariosService, HorariosService>();
builder.Services.AddTransient<IHorariosRepository, HorariosRepository>();

// Clinicas
builder.Services.AddTransient<IClinicaService, ClinicaService>();
builder.Services.AddTransient<IClinicaRepository, ClinicaRepository>();


// Configurar autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Logar";
    });

// Adicionando suporte a sessões
builder.Services.AddDistributedMemoryCache(); // Usar cache de memória para sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Define o tempo de inatividade
    options.Cookie.HttpOnly = true; // Garante que o cookie de sessão seja acessível apenas via HTTP
    options.Cookie.IsEssential = true; // Marca o cookie como essencial para a aplicação
});



// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Delfos Machine", 
        Version = "v3",
        Description = @"Esta API foi desenvolvida para o projeto *Challenge OdontoPrev*.  

    Para mais detalhes, assista ao vídeo da aplicação (https://link-do-video.com).  

    Sobre o Projeto
    O sistema sugere consultas para os clientes com base em três pilares principais:  
    1️⃣ Local, Data e Turno de preferência do cliente 📍  
    2️⃣ Qualidade do atendimento (avaliada por pesquisas de satisfação) ⭐  
    3️⃣ Baixo custo 💰  
    4️⃣ Com ciclos de renovações que deixam a saúde do cliente em ciclos preventivos e sem perda de tempo e gastos desnecessários
    5️⃣ Não é necessário acessar as agendas dos médicos. Antes de sugerir uma consulta ao cliente, o modelo de IA analisa as clínicas que atendem às preferências do paciente. Em seguida, a clínica recebe a oportunidade de aceitar ou recusar a solicitação, com base na disponibilidade de horários dentro de sua rotina de atendimentos. 

    Essa abordagem visa proporcionar a melhor experiência ao usuário, garantindo conveniência, qualidade e economia. Diferente dos modelos tradicionais disponíveis no mercado, onde o próprio cliente precisa buscar clínicas e especialidades — muitas vezes em situações de urgência —, nosso sistema automatiza esse processo. A inteligência artificial identifica as opções mais adequadas com base nas preferências do cliente, otimizando o agendamento e reduzindo o tempo de espera. Isso não apenas melhora a experiência do paciente, mas também contribui para uma gestão mais eficiente das clínicas, permitindo um melhor aproveitamento da agenda e aumentando a taxa de ocupação dos profissionais de saúde.",
        Contact = new OpenApiContact
        {
            Name = "Claudio Silva Bispo e Patricia Naomi",
            Email = "rm553472@fiap.com.br"
        },
        License = new OpenApiLicense
        {
            Name = "Delfos Machine Group - Segundo semestre",
            Url = new Uri("https://github.com/patinaomi/delfos-machine-2-sem.git")
        }
    });
    var xmlFile = $"{AppDomain.CurrentDomain.FriendlyName}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.WebHost.ConfigureKestrel(options =>
{
    //options.ListenAnyIP(3001);
    options.ListenLocalhost(3001);

});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita o Swagger
app.UseSwagger();

// Habilita o Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delfos Machine API v3");
    c.RoutePrefix = "swagger"; 
});

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Inicio}/{id?}");

app.Run();

