using MaquinasUbicacion.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestMaquinasUbicacion
{
    [TestClass]
    public class TestClientesController
    {
        [TestMethod]
        public void GetAllClientes_ShouldReturnAllClientes()
        {
            //invocar método que devuelva clientes para test, sin involucrar BD ni otro medio externo
            var testClientes = GetTestClientes();

            //invocar al controller pasandole los parámetros obtenidos
            var controller = new ClientesController(testClientes);
        }


        private List<Cliente> GetTestClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            lista.Add(new Cliente { id = 1, nombre = "Cliente Test 1" });
            lista.Add(new Cliente { id = 2, nombre = "Cliente Test 2" });
            lista.Add(new Cliente { id = 3, nombre = "Cliente Test 3" });
            lista.Add(new Cliente { id = 4, nombre = "Cliente Test 4" });

            return lista;
        }

        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}
