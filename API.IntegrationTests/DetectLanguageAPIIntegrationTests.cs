namespace seb_detectlanguage;

using System.Threading.Tasks;
using DetectLanguage;
using FluentAssertions;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System.Net;
using RestSharp.Serializers.NewtonsoftJson;
using Backend.IntegrationTests.Properties;

[TestFixture]
public class DetectLanguageAPIIntegrationTests
{
    readonly string BASE_URL = EnvironmentVariables.Variables.Environment.BaseUrl;
    readonly string DETECT_LANGUAGE_API_TOKEN = EnvironmentVariables.Variables.Environment.DetectLanguageAPIToken;

    private RestClient _client;

    [SetUp]
    public void Setup()
    {
        // Setup RestSharp client
        var options = new RestClientOptions(BASE_URL)
        {
            Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(DETECT_LANGUAGE_API_TOKEN, "Bearer")
        };

        _client = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson());
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [Test]
    public async Task Should_ReturnDetections_When_SingleValidStringIsSent()
    {
        // Arrange
        RestRequest request = new RestRequest("/detect", Method.Post);
        const string japanese = "こんにちは";
        request.AddParameter("q", japanese);

        // Act
        RestResponse<DetectResponse> response = await _client.ExecuteAsync<DetectResponse>(request);
        DetectResult[] responseData = response.Data!.data.detections;

        // Assert
        Assert.Multiple(() =>
        {
            responseData.Should().HaveCount(1);
            responseData[0].language.Should().Contain("ja");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        });
    }

    [Test]
    public async Task Should_ReturnDetections_When_MultipleValidStringsAreSent()
    {
        // Arrange
        RestRequest request = new RestRequest("/detect", Method.Post);
        const string spanish = "Buenos";
        const string english = "This+is+English";
        request.AddParameter("q[]", spanish);
        request.AddParameter("q[]", english);

        // Act
        RestResponse<BatchDetectResponse> response = await _client.ExecuteAsync<BatchDetectResponse>(request);
        DetectResult[][] responseData = response.Data!.data.detections;

        // Assert
        Assert.Multiple(() =>
        {
            responseData.Should().HaveCount(2);
            responseData[0].Should().HaveCount(3);
            responseData[0].Select(result => result.language).Should().ContainInOrder(["es", "pt", "fr"]);
            responseData[1].Should().HaveCount(1);
            responseData[1].Select(result => result.language).Should().Contain("en");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }

    [Test]
    public async Task Should_ReturnAllSupportedLanguages_When_Queried()
    {
        // Arrange & Act
        RestRequest request = new RestRequest("/languages", Method.Get);
        RestResponse<Language[]> response = await _client.ExecuteAsync<Language[]>(request);
        Language[] languages = response.Data!;

        // Assert
        languages.Length.Should().Be(164);
    }

    [TestCase("!#¤%&/()=", 0)]
    [TestCase("1234567890", 0)]
    public async Task Should_NotReturnDetections_When_InvalidInputIsSent(string textToIdentify, int detectionCount)
    {
        // Arrange
        RestRequest request = new RestRequest("/detect", Method.Post);
        request.AddParameter("q", textToIdentify);

        // Act
        RestResponse<DetectResponse> response = await _client.ExecuteAsync<DetectResponse>(request);
        DetectResult[] responseData = response.Data!.data.detections;

        // Assert
        responseData.Should().HaveCount(detectionCount);
    }
}
