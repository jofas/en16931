# Contribution Guidelines

## Development

### Testing

#### Package Tests

To make sure that package tests run based on the latest code changes, run the
following commands:

```bash
dotnet pack
dotnet nuget locals all --clear
rm -r Tests.PackageTests/bin/
```
