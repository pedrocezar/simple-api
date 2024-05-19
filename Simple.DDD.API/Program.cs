#region BuilderServices

using Simple.DDD.API.Filters;
using Simple.DDD.API.Handlers;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Interfaces.Services;
using Simple.DDD.Domain.Services;
using Simple.DDD.Domain.Settings;
using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();

#region Filters

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
    options.Filters.Add(typeof(ValidationFilter));
});

#endregion

#region HttpContext

builder.Services.AddHttpContextAccessor();

#endregion

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

#region AppSettings

var appSetting = builder.Configuration.GetSection(nameof(AppSetting)).Get<AppSetting>();
builder.Services.AddSingleton(appSetting);

#endregion

#region Mapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region Services    

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IFinalizacaoService, FinalizacaoService>();
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();

#endregion

#region Repositories    

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IFinalizacaoRepository, FinalizacaoRepository>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();

#endregion

#region Dbcontext

builder.Services.AddDbContext<ManutencaoContext>(options => {
    options.UseSqlServer(appSetting.SqlServerConnection);
    options.UseLazyLoadingProxies();
});

#endregion

#region HttpClient

builder.Services.AddHttpClient<INacionalidadeRepository, NacionalidadeRepository>(options => {
    options.BaseAddress = new Uri(appSetting.ApiNacionalidade);
});

#endregion

#region Jwt

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSetting.JwtSecurityKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

#endregion

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = true;
}).AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

#endregion

#endregion

#region AppConfiguration

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


#endregion
