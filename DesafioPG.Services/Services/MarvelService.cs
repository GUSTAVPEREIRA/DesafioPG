using DesafioPG.DTO.Marvel;
using DesafioPG.Services.IServices;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DesafioPG.Services.Services
{
    public class MarvelService : IMarvelService
    {
        /// <summary>
        /// Escreve informações dos personagens em um arquivo seguinte o padrão id, name, description \n comics: names..., series: names..., stories: names..., events: names....
        /// </summary>
        /// <param name="url">Host a ser requisitado</param>
        /// <param name="rootPath">Caminho da raiz do projeto</param>
        public string WriteMarvelInformationsFileTXT(string url, string rootPath)
        {
            var response = GetMarvelCharacteresInfo(url);
            string yaml = ConverInYaml(response);
            var pathfile = Path.Combine(rootPath, "personagensmarvel.txt");

            if (File.Exists(pathfile))
            {
                File.Delete(pathfile);
            }

            using (StreamWriter outputFile = new StreamWriter(pathfile, true))
            {
                outputFile.WriteLine(yaml);
            }

            return pathfile;
        }

        /// <summary>
        /// convert o array de personagens em formato yaml, também foi pensado em realizar essa solução usando string builder, ou serializado em outro objeto abstrato.
        /// </summary>
        /// <param name="character">Array contendo uma lista de personagens</param>
        /// <returns></returns>
        private string ConverInYaml(CharacterDTO[] character)
        {
            CamelCaseNamingConvention convention = new CamelCaseNamingConvention();
            var serializer = new SerializerBuilder().WithNamingConvention(convention).Build();

            var yaml = serializer.Serialize(character);
            yaml = yaml.Replace("- name: ", "").Replace("items:\r\n    ", "");
            yaml = yaml.Replace("items: []\r\n  ", "").Replace("    items: []\r\n", "");
            yaml = yaml.Replace("- id: ", "  \r\n  id: ").Replace("'", "");
            return yaml;
        }

        /// <summary>
        /// Realiza a busca dos personagens da marvel, e os converte em objeto
        /// </summary>
        /// <param name="url">Host a ser requisitado</param>
        /// <returns></returns>
        private CharacterDTO[] GetMarvelCharacteresInfo(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;

                try
                {
                    response.EnsureSuccessStatusCode();
                    string conteudo = response.Content.ReadAsStringAsync().Result;

                    var resultado = JsonConvert.DeserializeObject<CharacterDataWrapperDTO>(conteudo);
                    return resultado.Data.Results;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}