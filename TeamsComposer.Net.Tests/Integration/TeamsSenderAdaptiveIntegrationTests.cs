using TeamsComposer.Net.Builders.AdaptiveCards.Roots;
using TeamsComposer.Net.Http;

namespace TeamsComposer.Net.Tests.Integration
{
    public class TeamsSenderAdaptiveIntegrationTests
    {
        private readonly string? _webhookUrl;
        private readonly TeamsSender _sender;

        public TeamsSenderAdaptiveIntegrationTests()
        {
            _webhookUrl = Environment.GetEnvironmentVariable("WEBHOOK_URL");
            _sender = new TeamsSender(new HttpClient());
        }

        private void EnsureWebhookIsConfigured()
        {
            if (string.IsNullOrWhiteSpace(_webhookUrl))
            {
                throw new InvalidOperationException("WEBHOOK_URL environment variable is not set.");
            }
        }

        [Fact]
        //[Fact(Skip = "Manual integration test for Adaptive Card")]
        public async Task Send_ComplexRelatorioSky_ShouldRenderPerfectly()
        {
            EnsureWebhookIsConfigured();

            // --- 1. MOCK DATA (Simulating API/Database response) ---

            var otherErrors = new List<ReportMap>
            {
                new("ORA_CAMP_P1_TEST.TXT", 0.03, 3699, 1, 3700)
            };

            var notReceivedFiles = new List<string>
            {
                "CARGA__BLOCKLIST",
                "SMARTCARD",
                "ORA_UNI",
                "ORA_OFERTAS_CRM_"
            };

            var unknownFiles = new List<string>
            {
                "CAMP129147_CFATURA_D_.txt",
                "CAMP129147_D.txt",
                "CAMP129147.txt",
                "CAMP134545.TXT",
                "CAMP42398.TXT",
                "CAMP42398_C.TXT"
            };

            var successFiles = new List<ReportMap>
            {
                new("CAMP129704_.TXT", null, 6929, null, 6929),
                new("CAMP134546_HIG.TXT", null, 1, null, 1)
            };

            // --- 2. FLUID AND DYNAMIC CARD CONSTRUCTION ---

            var card = new AdaptiveCardBuilder()
                .WithVersion("1.4")
                .WithFallbackText("Relatório de Importação Empresa X - 00/00/0000")

                .AddTextBlock("Relatório de Importação Empresa X - 00/00/0000", size: "Large", weight: "Bolder")

                // Fixed Facts (could also come from an object)
                .AddFact("✅ Sucesso Total:", $"{successFiles.Count} arquivos")
                .AddFact("💤 Não Recebidos:", $"{notReceivedFiles.Count} arquivos")
                .AddFact("⚠️ Com Erros:", $"{otherErrors.Count} arquivos")
                .AddFact("🚨 Críticos:", "0 arquivos")
                .AddFact("❓ Desconhecidos:", $"{unknownFiles.Count} arquivos")

                // Block: Other Errors (Complex blocks using ForEach)
                .AddContainer(c => c
                    .WithStyle("emphasis")
                    .WithBleed()
                    .AddTextBlock("⚠️ Outros Erros", weight: "Bolder")
                    .ForEach(otherErrors, (builder, item) => builder
                        .AddTextBlock($"📁 {item.fileName}", weight: "Bolder")
                        .AddTextBlock($"⚠️ Taxa: {item.ErrorRate}% | ✅ Imp: {item.ImportedLines} | ❌ Erros: {item.ErrorLines} | 📊 Total: {item.TotalLines}")
                    )
                )

                // Block: Not Received (Simple string list with bullets)
                .AddTextBlock("💤 Não Recebidos", weight: "Bolder", color: "Accent")
                .AddMarkdownList(notReceivedFiles)

                // Block: Unknown (Simple list in warning container)
                .AddContainer(c => c
                    .WithStyle("warning")
                    .WithBleed()
                    .AddTextBlock("❓ Arquivos Desconhecidos (S3)", weight: "Bolder")
                    .AddMarkdownList(unknownFiles)
                )

                // Block: Success (Complex blocks using ForEach)
                .AddContainer(c => c
                    .WithStyle("good")
                    .WithBleed()
                    .AddTextBlock("✅ Arquivos com Sucesso", weight: "Bolder")
                    .ForEach(successFiles, (builder, item) => builder
                        .AddTextBlock($"📁 {item.fileName}", weight: "Bolder")
                        .AddTextBlock($"📊 Total: {item.TotalLines} | ✅ Imp: {item.ImportedLines}")
                    )
                )

                .AddOpenUrlAction("Ver Logs na AWS", "https://aws.amazon.com")

                .Build();

            // --- 3. SEND TO TEAMS ---
            await _sender.SendAsync(_webhookUrl!, card);
        }

        public record ReportMap(
            string fileName,
            double? ErrorRate,
            int? ImportedLines,
            int? ErrorLines,
            int? TotalLines);
    }
}
