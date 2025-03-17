tempdir=$(mktemp -d)
resourcedir="en16931/resources"

mkdir -p $resourcedir

echo "Downloading UBL 2.1 schemas"

wget -P $tempdir https://docs.oasis-open.org/ubl/os-UBL-2.1/UBL-2.1.zip
unzip -o -d $tempdir/ubl $tempdir/UBL-2.1.zip

echo "Download done"

echo "Extracing UBL 2.1 schemas"

ubldir=$resourcedir/ubl/2.1

mkdir -p $ubldir/maindoc/
cp $tempdir/ubl/xsd/maindoc/UBL-CreditNote-2.1.xsd $ubldir/maindoc/
cp $tempdir/ubl/xsd/maindoc/UBL-Invoice-2.1.xsd $ubldir/maindoc/

cp -r $tempdir/ubl/xsd/common/ $ubldir/common/

# MAYBEDO: extract xml/ examples for testing

echo "Extraction done"

echo "Downloading CII D16B schemas"

# TODO: wait for file to be downloaded
xdg-open https://unece.org/DAM/cefact/xml_schemas/D16B_SCRDM__Subset__CII.zip
unzip -o -d $tempdir/cii ~/Downloads/D16B_SCRDM__Subset__CII.zip
unzip -o -d $tempdir/cii/d16b/ $tempdir/cii/D16B\ SCRDM\ \(Subset\)\ CII/D16B\ SCRDM\ \(Subset\)\ CII\ uncoupled.zip

echo "Download done"

echo "Extracting CII D16B schemas"

ciidir=$resourcedir/cii/d16b

mkdir -p $ciidir
cp $tempdir/cii/d16b/D16B\ SCRDM\ \(Subset\)\ CII\ uncoupled/uncoupled\ clm/CII/uncefact/data/standard/* $ciidir

echo "Extraction done"

echo "Downloading EN 16931 schematrons"

en16931_version=${EN16931_VERSION:-1.3.13}
syntax=("cii" "ubl")

for s in ${syntax[@]}; do
  wget -P $tempdir https://github.com/ConnectingEurope/eInvoicing-EN16931/releases/download/validation-$en16931_version/en16931-$s-$en16931_version.zip
  unzip -o -d $tempdir/en16931-$s $tempdir/en16931-$s-$en16931_version.zip
done

echo "Download done"

echo "Extracting EN 16931 schematrons"

for s in ${syntax[@]}; do
  en16931dir=$resourcedir/en16931/$s

  mkdir -p $en16931dir
  cp $tempdir/en16931-$s/xslt/* $en16931dir
done

echo "Extraction done"

echo "Downloading XRechnung schematrons"

xrechnung_version=${XRECHNUNG_VERSION:-3.0.2}
xrechnung_schematron_version=${XRECHNUNG_SCHEMATRON_VERSION:-2.2.0}

wget -P $tempdir https://github.com/itplr-kosit/xrechnung-schematron/releases/download/release-$xrechnung_schematron_version/xrechnung-$xrechnung_version-schematron-$xrechnung_schematron_version.zip
unzip -o -d $tempdir/xrechnung $tempdir/xrechnung-$xrechnung_version-schematron-$xrechnung_schematron_version.zip

echo "Download done"

echo "Extracting XRechnung schematrons"

for s in ${syntax[@]}; do
  xrechnungdir=$resourcedir/xrechnung/$s

  mkdir -p $xrechnungdir
  cp $tempdir/xrechnung/schematron/$s/*.xsl $xrechnungdir
done

echo "Extraction done"
