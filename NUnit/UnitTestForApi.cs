using ForAutomaticTest.Controllers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using static ForAutomaticTest.Controllers.TestApiController;
using System.Net.Http;

namespace NUnit
{
    
    [TestFixture]
    public class UnitTestForApi
    {

        private WebApplicationFactory<Program> _factory = null!;
        private HttpClient _client = null!;

        [OneTimeSetUp]
        public void Init()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _factory.Dispose();
        }

        /// <summary>
        /// 驗證錯誤的訊息物件
        /// </summary>
        public class ValidationErrors
        {
            [JsonPropertyName("errors")] public Dictionary<string, List<string>> Errors { get; set; }
        }

        [Test]
        public async Task CreateUser_WithValidNameAndPassword_ReturnsSuccess()
        {
            // Arrange
            var user = new { name = "Bobby", password = "password123" };
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user", content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task CreateUser_WithEmptyName_ReturnsBadRequest()
        {
            // Arrange
            var user = new { name = "", password = "password123" };
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user", content);
            var errorObject = JsonConvert.DeserializeObject<ValidationErrors>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsNotNull(errorObject);
            Assert.That(errorObject.Errors.Count, Is.EqualTo(1));
            Assert.That(errorObject.Errors.First().Key, Is.EqualTo("Name"));
            Assert.That(errorObject.Errors["Name"][0], Is.EqualTo("The Name field is required."));
        }

        [Test]
        public async Task CreateUser_WithInvalidPassword_ReturnsBadRequest()
        {
            // Arrange
            var user = new { name = "John", password = "pw" };
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user", content);
            var errorObject = JsonConvert.DeserializeObject<ValidationErrors>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsNotNull(errorObject);
            Assert.That(errorObject.Errors.Count, Is.EqualTo(1));
            Assert.That(errorObject.Errors.First().Key, Is.EqualTo("Password"));
            Assert.That(errorObject.Errors["Password"][0], Is.EqualTo("The field Password must be a string or array type with a minimum length of '3'."));
        }

        [Test]
        public async Task CreateUser_WithEmptyNameAndInvalidPassword_ReturnsBadRequest()
        {
            // Arrange
            var user = new { name = "", password = "pw" };
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user", content);
            var errorObject = JsonConvert.DeserializeObject<ValidationErrors>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.IsNotNull(errorObject);
            Assert.That(errorObject.Errors.Count, Is.EqualTo(2));
            Assert.That(errorObject.Errors["Password"][0]
                , Is.EqualTo("The field Password must be a string or array type with a minimum length of '3'."));
            Assert.That(errorObject.Errors["Name"][0], Is.EqualTo("The Name field is required."));
        }

        [Test]
        public async Task GetUser_WithValidName_ReturnsSuccess()
        {
            // Arrange
            string name = "Bobby";

            // Act
            var httpResponseMessage = await _client.GetAsync("/api/user?Name="+name);
            httpResponseMessage.EnsureSuccessStatusCode();
            var body = httpResponseMessage.Content.ReadAsStringAsync().Result;
            UserDto user = JsonConvert.DeserializeObject<UserDto>(body)!;

            // Assert
            Assert.That(user.Name, Is.EqualTo(name));
            Assert.That(user.Password, Is.EqualTo(name + "1111"));
 
        }
    }
}