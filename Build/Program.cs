using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Build;
using static Build.Constants;

Command downloadUblSchema = new("ubl", "Download UBL 2.1 schema files.");

Command downloadEn16931Schematron = new("en16931", "Download schematron files with EN16931 rules.");

Command downloadXRechnungSchematron = new("xrechnung", "Download schematron files with XRechnung rules.");

Command downloadPeppolBisBillingSchematron = new("peppol", "Downliad schematron files with PEPPOL BIS Billing rules.");

Command downloadSchXslt2 = new("schxslt2", "Download SchXslt2 compiler.");

Command downloadAll = new("all", "Download all external resources.");

Command download = new("download", "Download external resources.");

download.Subcommands.Add(downloadUblSchema);
download.Subcommands.Add(downloadEn16931Schematron);
download.Subcommands.Add(downloadXRechnungSchematron);
download.Subcommands.Add(downloadPeppolBisBillingSchematron);
download.Subcommands.Add(downloadSchXslt2);
download.Subcommands.Add(downloadAll);

Argument<string> ciiD16bZipFileArgument = new("file")
{
    Description = "Zip Archive downloaded from https://unece.org/DAM/cefact/xml_schemas/D16B_SCRDM__Subset__CII.zip.",
};

Command installCiiD16b = new("d16b", "Install CII D16B schema files from Zip Archive downloaded from https://unece.org/DAM/cefact/xml_schemas/D16B_SCRDM__Subset__CII.zip.") {
    ciiD16bZipFileArgument,
};

Argument<string> ciiD22bZipFileArgument = new("file")
{
    Description = "Zip Archive downloaded from https://unece.org/sites/default/files/2025-01/XMLSchemas-D22B_0.zip.",
};

Command installCiiD22b = new("d22b", "Install CII D22B schema files from Zip Archive downloaded from https://unece.org/sites/default/files/2025-01/XMLSchemas-D22B_0.zip.") {
    ciiD22bZipFileArgument,
};

Command installCii = new("cii", "Install CII schemas.");

installCii.Subcommands.Add(installCiiD16b);
installCii.Subcommands.Add(installCiiD22b);

Argument<string> facturXZipFileArgument = new("file")
{
    Description = "Zip Archive downloaded from https://www.ferd-net.de/.",
};

Command installFacturX = new("factur-x", "Install Factur-X files from Zip Archive downloaded from https://www.ferd-net.de/.") {
    facturXZipFileArgument,
};

Command install = new("install", "Install external resources.");

install.Subcommands.Add(installCii);
install.Subcommands.Add(installFacturX);

RootCommand cmd = new("Build commands for the En16931 project.");

cmd.Subcommands.Add(download);
cmd.Subcommands.Add(install);

downloadUblSchema.SetAction(DownloadUblSchema);
downloadEn16931Schematron.SetAction(DownloadEn16931Schematron);
downloadXRechnungSchematron.SetAction(DownloadXRechnungSchematron);
downloadPeppolBisBillingSchematron.SetAction(DownloadPeppolBisBillingSchematron);
downloadSchXslt2.SetAction(DownloadSchXslt2);
downloadAll.SetAction(DownloadAll);

installCiiD16b.SetAction(InstallCiiD16b);
installCiiD22b.SetAction(InstallCiiD22b);
installFacturX.SetAction(InstallFacturX);

cmd.Parse(args).Invoke();

async Task DownloadAll(ParseResult args)
{
    await DownloadUblSchema(args);
    await DownloadEn16931Schematron(args);
    await DownloadXRechnungSchematron(args);
    await DownloadPeppolBisBillingSchematron(args);
    await DownloadSchXslt2(args);
}

async Task DownloadUblSchema(ParseResult args)
{
    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_Ubl_Schema_");

    using HttpClient client = new();

    string url = "https://docs.oasis-open.org/ubl/os-UBL-2.1/UBL-2.1.zip";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);

    foreach (string file in Directory.GetFiles($"{temp.FullName}/xsd/common"))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDir, "Ubl/common", Path.GetFileName(file)),
            overwrite: true
        );
    }

    File.Copy(
        $"{temp.FullName}/xsd/maindoc/UBL-CreditNote-2.1.xsd",
        $"{BaseResourceDir}/Ubl/maindoc/UBL-CreditNote-2.1.xsd",
        overwrite: true
    );

    File.Copy(
        $"{temp.FullName}/xsd/maindoc/UBL-Invoice-2.1.xsd",
        $"{BaseResourceDir}/Ubl/maindoc/UBL-Invoice-2.1.xsd",
        overwrite: true
    );

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded UBL 2.1 schema files.");
}

async Task DownloadEn16931Schematron(ParseResult args)
{
    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_En16931_Schematron_");

    using HttpClient client = new();

    foreach (string syntax in (string[])["cii", "ubl"])
    {
        string url = $"https://github.com/ConnectingEurope/eInvoicing-EN16931/releases/download/validation-{En16931SchematronVersion}/en16931-{syntax}-{En16931SchematronVersion}.zip";

        using HttpResponseMessage response = await client.GetAsync(url);

        response.EnsureSuccessStatusCode();

        ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);
    }

    foreach (string file in Directory.GetFiles($"{temp.FullName}/xslt"))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDir, "En16931", Path.GetFileName(file)),
            overwrite: true
        );
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded En16931 schematron files.");
}

async Task DownloadXRechnungSchematron(ParseResult args)
{
    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_XRechnung_Schematron_");

    using HttpClient client = new();

    string url = $"https://github.com/itplr-kosit/xrechnung-schematron/releases/download/v{XRechnungSchematronVersion}/xrechnung-{XRechnungVersion}-schematron-{XRechnungSchematronVersion}.zip";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);

    foreach (string syntax in (string[])["cii", "ubl"])
    {
        foreach (string file in Directory.GetFiles($"{temp.FullName}/schematron/{syntax}", "*.xsl"))
        {
            File.Copy(
                file,
                Path.Combine(BaseResourceDir, "XRechnung", Path.GetFileName(file)),
                overwrite: true
            );
        }
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded XRechnung schematron files.");
}

async Task DownloadPeppolBisBillingSchematron(ParseResult args)
{
    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_PeppolBisBilling_");

    using HttpClient client = new();

    string url = $"https://github.com/OpenPEPPOL/peppol-bis-invoice-3/archive/refs/tags/v{PeppolBisBillingVersion}.zip";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);

    SchXslt2.Compile(
        $"{temp.FullName}/peppol-bis-invoice-3-{PeppolBisBillingVersion}/rules/sch/PEPPOL-EN16931-CII.sch",
        $"{BaseResourceDir}/PeppolBisBilling/PEPPOL-CII-validation.xslt"
    );

    SchXslt2.Compile(
        $"{temp.FullName}/peppol-bis-invoice-3-{PeppolBisBillingVersion}/rules/sch/PEPPOL-EN16931-UBL.sch",
        $"{BaseResourceDir}/PeppolBisBilling/PEPPOL-UBL-validation.xslt"
    );

    string exampleDir = $"{temp.FullName}/peppol-bis-invoice-3-{PeppolBisBillingVersion}/rules/examples";

    foreach (string file in Directory.GetFiles(exampleDir))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDirTests, "PeppolBisBilling/examples", Path.GetFileName(file)),
            overwrite: true
        );
    }

    string nationalExamplesDir = $"{temp.FullName}/peppol-bis-invoice-3-{PeppolBisBillingVersion}/rules/national-examples";

    foreach (string dir in Directory.GetDirectories(nationalExamplesDir))
    {
        foreach (string file in Directory.GetFiles(dir))
        {
            File.Copy(
                file,
                Path.Combine(BaseResourceDirTests, "PeppolBisBilling/national-examples", new DirectoryInfo(dir).Name, Path.GetFileName(file)),
                overwrite: true
            );
        }
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded and compiled PEPPOL BIS Billing schematron files.");
}

async Task DownloadSchXslt2(ParseResult args)
{
    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Download_SchXslt2_");

    using HttpClient client = new();

    string url = $"https://codeberg.org/SchXslt/schxslt2/releases/download/v{SchXslt2Version}/schxslt2-{SchXslt2Version}.zip";

    using HttpResponseMessage response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    ZipFile.ExtractToDirectory(await response.Content.ReadAsStreamAsync(), temp.FullName);

    File.Copy(
        $"{temp.FullName}/schxslt2-{SchXslt2Version}/transpile.xsl",
        SchXslt2Location,
        overwrite: true
    );

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully downloaded SchXslt2 compiler.");
}

void InstallCiiD16b(ParseResult args)
{
    string archive = args.GetRequiredValue(ciiD16bZipFileArgument);

    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Install_Cii_D16b_");

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

    foreach (string file in Directory.GetFiles(schemaDir))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDir, "Cii/D16b", Path.GetFileName(file)),
            overwrite: true
        );
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully installed CII D16B schema files.");
}

void InstallCiiD22b(ParseResult args)
{
    string archive = args.GetRequiredValue(ciiD22bZipFileArgument);

    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Install_Cii_D22b_");

    ZipFile.ExtractToDirectory(archive, temp.FullName);

    string codelistsDir = $"{temp.FullName}/10DEC22/uncefact/codelist/standard/";

    foreach (string file in Directory.GetFiles(codelistsDir))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDir, "Cii/D22b/codelist/standard", Path.GetFileName(file)),
            overwrite: true
        );
    }

    string identifierDir = $"{temp.FullName}/10DEC22/uncefact/identifierlist/standard/";

    foreach (string file in Directory.GetFiles(identifierDir))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDir, "Cii/D22b/identifierlist/standard", Path.GetFileName(file)),
            overwrite: true
        );
    }

    File.Copy(
        $"{temp.FullName}/10DEC22/uncefact/data/standard/CrossIndustryInvoice_24p1.xsd",
        $"{BaseResourceDir}/Cii/D22b/data/standard/CrossIndustryInvoice_24p1.xsd",
        overwrite: true
    );

    File.Copy(
        $"{temp.FullName}/10DEC22/uncefact/data/standard/ReusableAggregateBusinessInformationEntity_32p0.xsd",
        $"{BaseResourceDir}/Cii/D22b/data/standard/ReusableAggregateBusinessInformationEntity_32p0.xsd",
        overwrite: true
    );

    File.Copy(
        $"{temp.FullName}/10DEC22/uncefact/data/standard/UnqualifiedDataType_32p0.xsd",
        $"{BaseResourceDir}/Cii/D22b/data/standard/UnqualifiedDataType_32p0.xsd",
        overwrite: true
    );

    File.Copy(
        $"{temp.FullName}/10DEC22/uncefact/data/standard/QualifiedDataType_32p0.xsd",
        $"{BaseResourceDir}/Cii/D22b/data/standard/QualifiedDataType_32p0.xsd",
        overwrite: true
    );

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully installed CII D22B schema files.");
}

void InstallFacturX(ParseResult args)
{
    string archive = args.GetRequiredValue(facturXZipFileArgument);

    DirectoryInfo temp = Directory.CreateTempSubdirectory("En16931_Install_Factur_X_");

    ZipFile.ExtractToDirectory(archive, temp.FullName);

    string basicDir = $"{temp.FullName}/Schema/2_Factur-X_{FacturXVersion}_BASIC/_XSLT_BASIC";

    foreach (string file in Directory.GetFiles(basicDir))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDir, "FacturX", Path.GetFileName(file)),
            overwrite: true
        );
    }

    string basicExamplesDir = $"{temp.FullName}/Beispiele/2. BASIC";

    foreach (string file in Directory.GetFiles(basicExamplesDir, "*.xml", SearchOption.AllDirectories))
    {
        File.Copy(
            file,
            Path.Combine(BaseResourceDirTests, "FacturX/Basic", Path.GetFileName(file)),
            overwrite: true
        );
    }

    Directory.Delete(temp.FullName, recursive: true);

    Console.WriteLine($"Successfully installed Factur-X files.");
}
