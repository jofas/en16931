using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Spectre.Console;
using Microsoft.Playwright;

Command downloadW3Schemas = new("w3", "Download schmema files from W3.");

Command downloadUblSchema = new("ubl", "Download UBL 2.1 schema files.");

Option<string> en16931SchematronVersionOption = new("--en16931-schematron-version")
{
    Description = "The version of the En16931 schmematron file.",
    DefaultValueFactory = (_) => "1.3.16",
    Required = true,
};

Command downloadEn16931Schematron = new("en16931", "Download schematron files with EN16931 rules.") {
    en16931SchematronVersionOption,
};

Option<string> xRechnungSchematronVersionOption = new("--xrechnung-schematron-version")
{
    Description = "The version of the XRechnung schmematron file.",
    DefaultValueFactory = (_) => "2.5.0",
    Required = true,
};

Command downloadXRechnungSchematron = new("xrechnung", "Download schematron files with XRechnung rules.") {
    xRechnungSchematronVersionOption,
};

Command downloadAll = new("all", "Download all external resources.") {
    en16931SchematronVersionOption,
    xRechnungSchematronVersionOption,
};

Command download = new("download", "Download external resources.");
download.Subcommands.Add(downloadW3Schemas);
download.Subcommands.Add(downloadUblSchema);
download.Subcommands.Add(downloadEn16931Schematron);
download.Subcommands.Add(downloadXRechnungSchematron);
download.Subcommands.Add(downloadAll);

Argument<string> ciiZipFileArgument = new("file") {
    Description = "Zip Archive downloaded from https://unece.org/DAM/cefact/xml_schemas/D16B_SCRDM__Subset__CII.zip.",
};

Command installCiiSchema = new("cii", "Install CII D16B schema files from Zip Archive downloaded from https://unece.org/DAM/cefact/xml_schemas/D16B_SCRDM__Subset__CII.zip.") {
    ciiZipFileArgument,
};

Command install = new("install", "Install external resources.");
install.Subcommands.Add(installCiiSchema);

RootCommand cmd = new("Build commands for the En16931 project.");
cmd.Subcommands.Add(download);
cmd.Subcommands.Add(install);

downloadW3Schemas.SetAction(DownloadW3Schemas);
downloadUblSchema.SetAction(DownloadUblSchema);
downloadEn16931Schematron.SetAction(DownloadEn16931Schematron);
downloadXRechnungSchematron.SetAction(DownloadXRechnungSchematron);
downloadAll.SetAction(DownloadAll);

installCiiSchema.SetAction(InstallCiiSchema);

cmd.Parse(args).Invoke();

async Task DownloadAll(ParseResult args)
{
    await DownloadXRechnungSchematron(args);
    await DownloadEn16931Schematron(args);
    await DownloadUblSchema(args);
    await DownloadW3Schemas(args);
}

async Task DownloadXRechnungSchematron(ParseResult args)
{
    string xRechnungVersion = "3.0.2";
    string schematronVersion = args.GetRequiredValue(xRechnungSchematronVersionOption);

    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_XRechnung_Schematron_");

    using HttpClient client = new();

    string url = $"https://github.com/itplr-kosit/xrechnung-schematron/releases/download/v{schematronVersion}/xrechnung-{xRechnungVersion}-schematron-{schematronVersion}.zip";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);

    foreach (string syntax in (string[])["cii", "ubl"]) {
        foreach (string file in Directory.GetFiles($"{temp.FullName}/schematron/{syntax}", "*.xsl")) {
            File.Copy(
                file,
                Path.Combine("en16931/resources/xrechnung", Path.GetFileName(file)),
                overwrite: true
            );
        }
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded XRechnung schematron files.");
}

async Task DownloadEn16931Schematron(ParseResult args)
{
    string schematronVersion = args.GetRequiredValue(en16931SchematronVersionOption);

    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_En16931_Schematron_");

    using HttpClient client = new();

    foreach (string syntax in (string[])["cii", "ubl"]) {
        string url = $"https://github.com/ConnectingEurope/eInvoicing-EN16931/releases/download/validation-{schematronVersion}/en16931-{syntax}-{schematronVersion}.zip";

        using HttpResponseMessage response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);
    }

    foreach (string file in Directory.GetFiles($"{temp.FullName}/xslt")) {
        File.Copy(
            file,
            Path.Combine("en16931/resources/en16931", Path.GetFileName(file)),
            overwrite: true
        );
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded En16931 schematron files.");
}

async Task DownloadUblSchema(ParseResult args)
{
    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_Ubl_Schema_");

    using HttpClient client = new();

    string url = "https://docs.oasis-open.org/ubl/os-UBL-2.1/UBL-2.1.zip";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);

    foreach (string file in Directory.GetFiles($"{temp.FullName}/xsd/common")) {
        File.Copy(
            file,
            Path.Combine("en16931/resources/ubl/common", Path.GetFileName(file)),
            overwrite: true
        );
    }

    File.Copy(
        $"{temp.FullName}/xsd/maindoc/UBL-CreditNote-2.1.xsd",
        "en16931/resources/ubl/maindoc/UBL-CreditNote-2.1.xsd",
        overwrite: true
    );

    File.Copy(
        $"{temp.FullName}/xsd/maindoc/UBL-Invoice-2.1.xsd",
        "en16931/resources/ubl/maindoc/UBL-Invoice-2.1.xsd",
        overwrite: true
    );

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded UBL 2.1 schema files.");
}

async Task DownloadW3Schemas(ParseResult args)
{
    using HttpClient client = new();

    string url = "https://www.w3.org/TR/2002/REC-xmldsig-core-20020212/xmldsig-core-schema.xsd";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    string content = await response.Content.ReadAsStringAsync();

    File.WriteAllText("en16931/resources/w3/xmldsig-core-schema.xsd", content);

    Console.WriteLine($"Successfully downloaded W3 schemas.");
}

void InstallCiiSchema(ParseResult args)
{
    string archive = args.GetRequiredValue(ciiZipFileArgument);

    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Install_Cii_Schema_");

    ZipFile.ExtractToDirectory(archive, temp.FullName);

    string schemaZip = Path.Combine(
        temp.FullName,
        "D16B SCRDM (Subset) CII/D16B SCRDM (Subset) CII uncoupled.zip"
    );

    ZipFile.ExtractToDirectory(schemaZip, temp.FullName);

    string schemaDir = Path.Combine(
        temp.FullName,
        "D16B SCRDM (Subset) CII uncoupled/uncoupled clm/CII/uncefact/data/standard"
    );

    foreach (string file in Directory.GetFiles(schemaDir)) {
        File.Copy(
            file,
            Path.Combine("en16931/resources/cii", Path.GetFileName(file)),
            overwrite: true
        );
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully installed CII D16B schema files.");
}
