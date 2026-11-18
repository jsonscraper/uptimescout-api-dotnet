# Ready-to-run examples

[README](../README.md) · [Methods](../docs/endpoints.md) · [Response example](../docs/responses.md)

Each example makes one data call. Set `UPTIMESCOUT_LICENSE_KEY` privately first. Clone this repository and run commands from its root. `--help` makes no API call.

Run the source-referenced console project with .NET 8+:

```sh
dotnet run --project examples/Quickstart -- --help
```

| Example | Run from repository root |
| --- | --- |
| [Search the service catalog](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method services --region GB --lang en` |
| [Read one regional signal](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method status --region GB --lang en` |
| [Check several services in one call](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method statuses --region GB --lang en` |
| [Summarize selected services](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method summary --region GB --lang en` |
| [Read observation history](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method history --region GB --lang en` |
| [Inspect observation statistics](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method stats --region GB --lang en` |
| [Find fresh elevated signals](Quickstart/Program.cs) | `dotnet run --project examples/Quickstart -- --method incidents --region GB --lang en` |

Read [the response example](../docs/responses.md) alongside each request. The optional `UPTIMESCOUT_BASE_URL` variable is for a trusted development/mock server; do not send a real key to an untrusted host.

## Responses for every method

Public demonstration snapshots captured on **2026-11-18**. Each file includes its exact request arguments and complete response. These historical signals are examples, not current status claims. They contain no license key or customer usage fields.

| Method | Request and response JSON |
| --- | --- |
| `services` | [services.json](responses/services.json) |
| `status` | [status.json](responses/status.json) |
| `statuses` | [statuses.json](responses/statuses.json) |
| `summary` | [summary.json](responses/summary.json) |
| `history` | [history.json](responses/history.json) |
| `stats` | [stats.json](responses/stats.json) |
| `incidents` | [incidents.json](responses/incidents.json) |
