using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Main.Extensiones;
using PeruGroup.Ecommerce.Domain.Core.Extensiones;
using PeruGroup.Ecommerce.Infrastructure.Repository.Extensiones;
using PeruGroup.Ecommerce.Transversal.Logging.Extensiones;
using PeruGroup.Ecommerce.Transversal.Mapper.Extensiones;
using System;
using System.Threading.Tasks;

namespace PeruGroup.Ecommerce.Application.Test
{
    [TestClass]
    public class UserApplicationTest
    {
        private static IServiceProvider _serviceProvider;
        private static IServiceScopeFactory _scopeFactory;
        public TestContext TestContext { get; set; }
        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            // Cargar configuración si la necesitas
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddApplication();
            services.AddInfrastructure();
            services.AddDomain();
            services.AddMappers();
            services.AddLogger();

            // Logging si es necesario
            services.AddLogging();

            _serviceProvider = services.BuildServiceProvider();
            _scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task Authenticate_CuandoNoSeEnviaParametros_RetornaMensajeErrorValidacion()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersApplication>();
            //Arrange: Donde se inicializan los objetos necesarios para la ejecucion del código.
            var userName = string.Empty;
            var password = string.Empty;
            var mensajeEsperado = "Errores de Validacion";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.
            var result = await context!.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.
            
            Assert.AreEqual(mensajeEsperado, actual);
        }

        [TestMethod]
        public async Task Authenticate_CuandoNoSeEnviaParametros_RetornaMensajeExito()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersApplication>();
            //Arrange: Donde se inicializan los objetos necesarios para la ejecucion del código.
            var userName = "GUIDEV";
            var password = "123456";
            var mensajeEsperado = "Usuario autenticado correctamente.";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.
            var result = await context!.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.

            Assert.AreEqual(mensajeEsperado, actual);
        }

        [TestMethod]
        public async Task Authenticate_CuandoNoSeEnviaParametrosIncorrectos_RetornaMensajeUsuarioNoExiste()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUsersApplication>();
            //Arrange: Donde se inicializan los objetos necesarios para la ejecucion del código.
            var userName = "GUIDEV";
            var password = "1245789";
            var mensajeEsperado = "Usuario o contraseña invalidos.";

            //Act: Donde se ejecuta el metodo que se va a probar y se obtiene el resultado.
            var result = await context!.Authenticate(userName, password);
            var actual = result.Message;

            //Assert: Donde se comprueba que el resultado obtenido es el esperado.

            Assert.AreEqual(mensajeEsperado, actual);
        }
    }
}
