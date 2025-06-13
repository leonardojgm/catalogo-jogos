using ApiCatalogoJogos.Controllers.V1;
using ApiCatalogoJogos.Middleware;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
//builder.Services.AddScoped<IJogoRepository, JogoSqlServerRepository>();
builder.Services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();
builder.Services.AddScoped<IExemploScoped, ExemploCicloDeVida>();
builder.Services.AddTransient<IExemploTransient, ExemploCicloDeVida>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    string xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";

    c.SwaggerDoc("v1", new() { Title = "ApiCatalogoJogos", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCatalogoJogos v1"));
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
