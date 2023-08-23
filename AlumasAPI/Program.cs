using AlumasAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AlumasAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //Vamos a leer la etiqueta CNNSTR de appsettings.json para configurar la conexion
            //a la base de datos.
            var CnnStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            //eliminamos de la CNNSTR el dato del password ya que seria muy sencillo obtener la info de conexion
            //del usuario de SQL Server del archivo de config appsettings.json

            CnnStringBuilder.Password = "123456";

            //CnnStrBuilder es un objeto que permite la construccion de cadenas de conexion a bases de datos.
            //se pueden modificar cada parte de la misma,pero el fianl debemos extraer un string con la info final

            string cnnStr = CnnStringBuilder.ConnectionString;

            //ahora conectamos el proyecto a la base de datos usando cnnStr

            builder.Services.AddDbContext<AlumasContext>(optionsAction => optionsAction.UseSqlServer(cnnStr));





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

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}