using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace priberam.xunit.IntegrationTest
{
    public class CandidateControllerTest : AbstractIntegrationTest
    {
        protected readonly string _Url = "/api/candidate";

        [Fact]
        public async Task GetAll_Unauthorized()
        {
            // Arrange: Prepare all the required data and preconditions.
            // Act: Performs the actual test
            var response = await this.TestClient.GetAsync(_Url);
            // Assert: Checks if the expected result has occurred
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_Success()
        {
            // Arrange: Prepare all the required data and preconditions.
            await AuthenticateAsync();
            // Act: Performs the actual test
            var response = await this.TestClient.GetAsync(_Url);
            // Assert: Checks if the expected result has occurred
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /*[Fact]
        public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange: Prepare all the required data and preconditions.
            var defaultPage = await TestClient.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            // Act: Performs the actual test
            var response = await TestClient.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='messages']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

            // Assert: Checks if the expected result has occurred
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/", response.Headers.Location.OriginalString);
        }*/
    }
}
