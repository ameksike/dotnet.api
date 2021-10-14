using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;

using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

using priberam.Models.DAO;
using priberam.Models.DTO;


namespace priberam.xunit.IntegrationTest
{
    public abstract class AbstractIntegrationTest
    {
        protected HttpClient TestClient { get; set; }
        public AbstractIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
				.WithWebHostBuilder(builder =>
				{
					builder.ConfigureServices(services =>
					{
						var descriptor = services.SingleOrDefault(d => 
							d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

						if (descriptor != null)
						{
							services.Remove(descriptor);
						}
						
						services.AddDbContext<ApplicationDbContext>(options =>
						{
							options.UseInMemoryDatabase("TDD.db");
						});
					});
				});

			this.TestClient  = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await this.GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("/api/Account", new AccountUser {
                Email = "tony@gmail.com",
                Password = "Tony-123"
            });
            var registrationResponse = await response.Content.ReadAsStringAsync(); 
            var res = JsonConvert.DeserializeObject<AccountToken>(registrationResponse); 
            return res.Token;
        }
        /**/
    }
}
