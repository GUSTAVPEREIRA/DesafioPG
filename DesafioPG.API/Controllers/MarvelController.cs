using DesafioPG.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace DesafioPG.API.Controllers
{    
    /// <summary>
    /// Controller da API da marvel
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MarvelController : ControllerBase
    {
        
        private readonly IMarvelService MarvelService;
        private readonly IConfiguration Configuration;
        private readonly IWebHostEnvironment WebHostEnvironment;

        /// <summary>
        /// Injeção de dependência
        /// </summary>
        /// <param name="marvelService"></param>
        /// <param name="configuration"></param>
        /// <param name="webHostEnvironment"></param>
        public MarvelController(IMarvelService marvelService, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            MarvelService = marvelService;
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Busca os personagens da marvel e os grava em um arquivo
        /// </summary>
        /// <param name="limit">Aceita valores de 20 a 100, caso seja informado valores discrepantes, será efetuado a busca pelo valor 20 ou 100</param>
        /// <param name="offset">Navega pelas páginas</param>        
        /// <returns></returns>
        [HttpPost]
        [Route("WriteCharactersInFile")]
        public ActionResult<dynamic> WriteMarvelCharactersInFile(int limit = 20, int offset = 0)
        {
            var api = Configuration.GetSection("Marvel:API").Value;
            var apikey = Configuration.GetSection("Marvel:APIKey").Value;
            var hash = Configuration.GetSection("Marvel:Hash").Value;
            var ts = Configuration.GetSection("Marvel:TS").Value;

            if(limit < 20 || limit > 100)
            {
                limit = limit < 20 ? 20 : 100;
            }

            if (offset < 0)
            {
                offset = 0;
            }

            var url = $"{api}?limit={limit}&offset={offset}&ts={ts}&apikey={apikey}&hash={hash}";
            var rootPath = WebHostEnvironment.ContentRootPath;

            try
            {
                var path = MarvelService.WriteMarvelInformationsFileTXT(url, rootPath);

                return new OkObjectResult(new
                {
                    Resultado = "Arquivo escrito com sucesso!",
                    Caminho = path
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new
                {
                    Mensagem = "Por favor contate o suporte gugupereira123@hotmail.com",
                    Erro = ex.Message
                });
            }
        }
    }
}