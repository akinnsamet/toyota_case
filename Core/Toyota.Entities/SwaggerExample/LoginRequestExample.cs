using Toyota.Entities.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace Toyota.Entities.SwaggerExample
{
    public class LoginRequestExample : IExamplesProvider<LoginRequestEntity>
    {
        public LoginRequestEntity GetExamples()
        {
            return new LoginRequestEntity
            {
                Username = "admin",
                Password = "TestAdmin312",
            };
        }
    }
}
