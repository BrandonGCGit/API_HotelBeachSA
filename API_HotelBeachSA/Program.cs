using API_HotelBeachSA.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Configuration del servicio JWT
//Se agrega la interface y el objeto que la implementa
builder.Services.AddScoped<IAutorizacionServices, AutorizacionServices>();
//Se toma la llave para el token
var key = builder.Configuration.GetValue<string>("JwtSettings:Key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(
    config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(
    config =>
    {
        config.RequireHttpsMetadata = false;//No requiere de metadata
        config.SaveToken = true;//Se almacena el token
        config.TokenValidationParameters = new TokenValidationParameters //COnfiguration de validaciones
        {
            ValidateIssuerSigningKey = true,//Valida la key para inicio de session
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),//se asigna el valor para la key
            ValidateIssuer = false,//no se valida el emisor
            ValidateAudience = false,//no se valida la audiencian
            ValidateLifetime = true,//Se valida el tiempo de vida al token
            ClockSkew = TimeSpan.Zero,//No debe existir diferencia desviacion para el tiempo del reloj
        };
    });


builder.Services.AddDbContext<API_HotelBeachSA.Context.DBContextGestionHotel>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StringConexion")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
