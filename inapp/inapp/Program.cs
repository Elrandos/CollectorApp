using inapp.Data;
using inapp.Helpers;
using inapp.Interfaces.Repositories;
using inapp.Interfaces.Services;
using inapp.Repositories;
using inapp.Services;
using Microsoft.EntityFrameworkCore;

namespace inapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CollectorDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddSingleton<PasswordHasherHelper>();
            builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
}
