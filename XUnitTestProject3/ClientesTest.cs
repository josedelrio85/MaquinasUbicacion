using MaquinasUbicacion.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject3
{
    public class TestInitializer
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public TestInitializer()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            var response = await _client.GetAsync("/api/clientes");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            //JArray aa = JArray.Parse(responseString);
            System.Collections.Generic.List<Cliente> lista = JArray.Parse(responseString).ToObject<System.Collections.Generic.List<Cliente>>();

            Assert.NotNull(responseString);
            Assert.NotEmpty(lista);
            Assert.Equal(4, lista.Count);
        }

        [Fact]
        public async Task Test2()
        {
            var response = await _client.GetAsync("/api/clientes/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            dynamic jsonObject = JObject.Parse(responseString);
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(responseString);

            Assert.NotNull(responseString);
            Assert.IsType<Cliente>(cliente);
            // Assert.Same(cliente, new Cliente { id = 1, nombre = "Cliente Test 1" });
            //Assert.Equal(cliente, new Cliente { id = 1, nombre = "Cliente Test 1" });
        }

        [Fact]
        public async Task Test3()
        {
            var response = await _client.GetAsync("/api/clientes/SearchClientes/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            List<Cliente> lista = JArray.Parse(responseString).ToObject<List<Cliente>>();

            Assert.NotNull(responseString);
            Assert.NotEmpty(lista);
            Assert.Single<Cliente>(lista);
            Assert.Contains("Cliente Test 1", lista[0].nombre);
        }

        [Fact]
        public async Task Test4()
        {
            Cliente cliente = new Cliente { id = 5, nombre = "Cliente Test de Prueba " };
            var response = await _client.PostAsJsonAsync("/api/clientes/", cliente).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            
            Assert.NotNull(response);

            var responseString = await response.Content.ReadAsStringAsync();
            var clienteResp = JsonConvert.DeserializeObject<Cliente>(responseString);
            Assert.Equal(5, clienteResp.id);
        }

        [Fact]
        public async Task Test5()
        {
            Cliente cliente = new Cliente { id = 4, nombre = "Cliente Test 6" };
            var response = await _client.PutAsJsonAsync("/api/clientes/" + cliente.id, cliente).ContinueWith((putTask) => putTask.Result.EnsureSuccessStatusCode());

            Assert.NotNull(response);

            var responseString = await response.Content.ReadAsStringAsync();
            var clientePut = JsonConvert.DeserializeObject<Cliente>(responseString);

            Assert.Equal(4, clientePut.id);
            Assert.Contains("Test 6", cliente.nombre);

        }


        [Fact]
        public async Task Test6()
        {
            var response = await _client.GetAsync("/api/clientes/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            dynamic jsonObject = JObject.Parse(responseString);
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(responseString);

            Assert.NotNull(responseString);
            Assert.IsType<Cliente>(cliente);
        }
    }
}
