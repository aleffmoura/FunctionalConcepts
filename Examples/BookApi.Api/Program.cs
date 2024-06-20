namespace BookApi.Api;

using BookApi.Api.Behaviors;
using BookApi.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
                       .AddNewtonsoftJson(op =>
                       {
                           op.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                           op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                       });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.OperationFilter<RemoveAntiforgeryHeaderOperationFilter>();
        });
        builder.Services.AddLogging(cfg =>
        {
            cfg.AddConsole();
            cfg.AddDebug();
        });
        builder.Host.ConfigureAutofac(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

}
