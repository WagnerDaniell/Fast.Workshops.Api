using Fast.Workshops.Api.Middleware;
using Fast.Workshops.Application.Services;
using Fast.Workshops.Application.UseCases.Auth;
using Fast.Workshops.Application.UseCases.Colaboradores;
using Fast.Workshops.Application.UseCases.Stats;
using Fast.Workshops.Application.UseCases.Workshops;
using Fast.Workshops.Domain.Repositories;
using Fast.Workshops.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<Context>((options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.0-mysql"), mySqlOptions =>
    {
        mySqlOptions.MigrationsAssembly("Fast.Workshops.Infrastructure");
        mySqlOptions.EnableRetryOnFailure(3);
        mySqlOptions.CommandTimeout(60);
    });
});

builder.Services.AddScoped<TokenService>();

//auth
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUseCase>();

//workshop
builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
builder.Services.AddScoped<ReadWorkshopUseCase>();
builder.Services.AddScoped<CreateWorkshopUseCase>();
builder.Services.AddScoped<UpdateWorkshopUseCase>();
builder.Services.AddScoped<DeleteWorkshopUseCase>();

//colaborador
builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
builder.Services.AddScoped<CreateColaboradorUseCase>();
builder.Services.AddScoped<ReadColaboradorUseCase>();
builder.Services.AddScoped<UpdateColaboradorUseCase>();
builder.Services.AddScoped<DeleteColaboradorUseCase>();

//workshopcolaboradores
builder.Services.AddScoped<IWorkshopColaboradorRepository, WorkshopColaboradorRepository>();
builder.Services.AddScoped<AddColaboradorToWorkshopUseCase>();
builder.Services.AddScoped<RemoveColaboradorFromWorkshopUseCase>();

//stats
builder.Services.AddScoped<IStatsRepository, StatsRepository>();
builder.Services.AddScoped<ReadStatsUseCase>();

builder.Services.AddCors(opcoes =>
{
    opcoes.AddPolicy("Permission", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var _secretKey = builder.Configuration["JWT:SECRETKEY"]!;
var key = Encoding.ASCII.GetBytes(_secretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fast Workshops",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT. Exemplo: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<Context>();
        db.Database.Migrate(); // roda as migrations automaticamente so em ambiente dev
        //sei que o correto não é a propria api conseguir rodar migrations mas como na descrição do teste pedia instruções para rodar locamente
        //então tentei facilitar para rodar facilmente, em produção não seria legal usar isso!
    }
}

app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsDevelopment())
{
    //Desligado pq tirei as portas https já que no container não tem o ssl
    //app.UseHttpsRedirection();
}

app.UseCors("Permission");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
