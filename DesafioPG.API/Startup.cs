using DesafioPG.Initializer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DesafioPG.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddServices();
            services.AddGlobalExceptionHandlerMiddleware();

            //Uso essa linha para buscar todos arquivos do projeto, para construir a documentação da API            
            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("DesafioPG", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Desafio realizado pela Paschoalotto",                                        
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo Antonio Pereira",
                        Email = "gugupereira123@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/gustavo-pereira-1a1554168")                        
                    },                                                            
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                xmlFiles.ForEach(xml => c.IncludeXmlComments(xml));
            });            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>        
        public void Configure(IApplicationBuilder app)
        {
            //Essas linhas foram criadas para os arquivos customizados do SWAGGER
            app.UseDefaultFiles();
            app.UseStaticFiles();            

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "docs/{documentName}/docs.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/docs/DesafioPG/docs.json", "V1");
                c.RoutePrefix = "docs";
                c.InjectStylesheet($"/swagger/custom.css");
                c.InjectJavascript($"/swagger/custom.js");                
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());            
            app.UseRouting();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}