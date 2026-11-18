using UptimeScout;
using System.Text.Json;

if (args.Length == 0 || args.Contains("--help"))
{
    Console.WriteLine("Usage: dotnet run --project examples/Quickstart -- --method status --region GB --lang en\nMethods: services, status, statuses, summary, history, stats, incidents\nOptions: --service github --services github,cloudflare --hours 24 --limit 50 --offset 0 --q github");
    return;
}
try
{
    var options = new Dictionary<string, string>();
    for (int i = 0; i < args.Length; i += 2)
    {
        if (!args[i].StartsWith("--") || i + 1 >= args.Length) throw new ArgumentException("Expected --option value");
        options[args[i][2..]] = args[i + 1];
    }
    string method = options.GetValueOrDefault("method", ""); options.Remove("method");
    if (!new[] { "services", "status", "statuses", "summary", "history", "stats", "incidents" }.Contains(method)) throw new ArgumentException("Choose a valid --method");
    var parameters = new Dictionary<string, string> { ["region"] = "GB", ["lang"] = "en" };
    switch (method)
    {
        case "status": parameters["service"] = "github"; break;
        case "services": parameters["q"] = "github"; parameters["limit"] = "10"; break;
        case "statuses": case "summary": parameters["services"] = "github,cloudflare"; break;
        case "history": case "stats": parameters["service"] = "github"; parameters["hours"] = "24"; break;
        case "incidents": parameters["limit"] = "10"; break;
    }
    if (method == "history") { parameters["limit"] = "50"; parameters["offset"] = "0"; }
    foreach (var pair in options) parameters[pair.Key] = pair.Value;
    using var client = new Client(Environment.GetEnvironmentVariable("UPTIMESCOUT_LICENSE_KEY") ?? "",
        Environment.GetEnvironmentVariable("UPTIMESCOUT_BASE_URL") ?? "https://api.uptimescout.com");
    using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    var response = await client.RequestAsync(method, parameters, cancellation.Token);
    Console.WriteLine(JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
}
catch (Exception)
{
    Console.Error.WriteLine("UptimeScout request failed; check key, method, parameters, plan and network.");
    Environment.ExitCode = 1;
}
