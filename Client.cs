using System.Net.Http.Headers;using System.Text.Json;
namespace UptimeScout;
public sealed class Client : IDisposable {
 private readonly HttpClient http;private readonly string baseUrl;
 public Client(string licenseKey,string baseUrl="https://api.uptimescout.com"){if(string.IsNullOrWhiteSpace(licenseKey)||licenseKey.Contains('\r')||licenseKey.Contains('\n'))throw new ArgumentException("License key required");this.baseUrl=baseUrl.TrimEnd('/');http=new HttpClient(new HttpClientHandler{AllowAutoRedirect=false}){Timeout=TimeSpan.FromSeconds(30)};http.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer",licenseKey);}
 public async Task<JsonElement> RequestAsync(string method,IReadOnlyDictionary<string,string>? parameters=null,CancellationToken cancellationToken=default){if(!new[]{"services","status","statuses","summary","history","stats","incidents"}.Contains(method))throw new ArgumentException("Unknown method");string query=string.Join("&",(parameters??new Dictionary<string,string>()).Select(p=>Uri.EscapeDataString(p.Key)+"="+Uri.EscapeDataString(p.Value)));using var response=await http.GetAsync(baseUrl+"/v1/"+method+"?"+query,cancellationToken);if(!response.IsSuccessStatusCode)throw new HttpRequestException("UptimeScout HTTP "+(int)response.StatusCode);using var doc=JsonDocument.Parse(await response.Content.ReadAsStringAsync(cancellationToken));return doc.RootElement.Clone();}
 public void Dispose()=>http.Dispose();
}
