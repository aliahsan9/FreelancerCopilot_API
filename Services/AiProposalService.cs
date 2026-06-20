using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FreelancerCopilot.API.Models;

namespace FreelancerCopilot.API.Services
{
    public class AiProposalService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AiProposalService> _logger;

        public AiProposalService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<AiProposalService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> GenerateProposal(Job job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            var apiKey = _configuration["OpenAI:ApiKey"];
            var model = _configuration["OpenAI:Model"] ?? "gpt-4o-mini";

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("OpenAI API Key not configured.");

            var prompt = BuildPrompt(job);

            var requestBody = new
            {
                model,
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "You are an expert Upwork proposal writer."
                    },
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                },
                temperature = 0.7,
                max_tokens = 500
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.openai.com/v1/chat/completions");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError(
                        "OpenAI Error: {Status} {Response}",
                        response.StatusCode,
                        json);

                    throw new Exception("AI service failed.");
                }

                using var document = JsonDocument.Parse(json);

                var content =
                    document.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return content ?? "Unable to generate proposal.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Proposal generation failed");

                throw new Exception(
                    "Failed to generate proposal. Please try again.");
            }
        }

        private static string BuildPrompt(Job job)
        {
            return $@"
Write a professional Upwork proposal.

Job Title:
{job.Title}

Job Description:
{job.Description}

Budget:
{job.Budget}

Requirements:
- 150-200 words
- Professional tone
- Focus on client problems
- Mention ASP.NET Core and Angular expertise when relevant
- End with a question
";
        }
    }
}