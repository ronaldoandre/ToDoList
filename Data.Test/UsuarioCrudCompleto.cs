using Data.Repository.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TODOLIST.Data.Context;
using TODOLIST.Domain.Models;
using Xunit;

namespace Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Usuario")]
        [Trait("Registro", "User")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserRepository _repositorio = new UserRepository(context);
                User _userEntity = new User
                {
                    Email = "test@email.com",
                    FullName = "teste",
                    Password = "teste"
                };

                var _registroCriado = await _repositorio.Register(_userEntity);
                Assert.NotNull(_registroCriado);
                Assert.Equal("test@email.com", _registroCriado.Email);
                Assert.Equal("teste", _registroCriado.FullName);
                Assert.Equal("teste", _registroCriado.Password);
            }
        }
    }
}
