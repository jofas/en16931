using System;
using System.IO;
using System.Text;
using System.Xml;
using En16931;
using En16931.Model;
using En16931.Model.Primitives;
using Xunit;

namespace Tests.IR;

public class Deserialize
{
    [Fact]
    public void DeserializeInvoice()
    {
        Blob content1 = new("hello world 1");
        Blob content2 = new("hello world 2");

        string xml = $"""
            <?xml version="1.0" encoding="utf-16"?>
            <invoice xmlns="urn:todo">
              <invoice-number id="bt-1">
                <content>1234567</content>
              </invoice-number>
              <invoice-issue-date id="bt-2">2018-04-13</invoice-issue-date>
              <invoice-type-code id="bt-3">380</invoice-type-code>
              <invoice-currency-code id="bt-5">EUR</invoice-currency-code>
              <vat-accounting-currency-code id="bt-6">GBP</vat-accounting-currency-code>
              <value-added-tax-point-date id="bt-7">2018-04-13</value-added-tax-point-date>
              <payment-due-date id="bt-9">2018-04-13</payment-due-date>
              <buyer-reference id="bt-10">90000000-03083-72</buyer-reference>
              <project-reference id="bt-11">PR12345678</project-reference>
              <contract-reference id="bt-12">0000000752</contract-reference>
              <purchase-order-reference id="bt-13">65002278</purchase-order-reference>
              <sales-order-reference id="bt-14">ABC123456789</sales-order-reference>
              <receiving-advice-reference id="bt-15">RAR123456789</receiving-advice-reference>
              <despatch-advice-reference id="bt-16">DAR123456789</despatch-advice-reference>
              <tender-or-lot-reference id="bt-17">ANG987654321</tender-or-lot-reference>
              <invoiced-object-identifier id="bt-18">
                <content>OK987654321</content>
              </invoiced-object-identifier>
              <buyer-accounting-reference id="bt-19">Buchungscode1</buyer-accounting-reference>
              <payment-terms id="bt-20">Beschreibung 1: Bitte überweisen Sie bis zum 13.04.2018 auf das unten aufgeführte Konto.</payment-terms>
              <invoice-notes id="bg-1">
                <invoice-note id="bg-1">
                  <invoice-note-subject-code id="bt-21">AAC</invoice-note-subject-code>
                  <invoice-note id="bt-22">Invoice Note Description</invoice-note>
                </invoice-note>
                <invoice-note id="bg-1">
                  <invoice-note-subject-code id="bt-21">AAC</invoice-note-subject-code>
                  <invoice-note id="bt-22">Invoice Note Description 2</invoice-note>
                </invoice-note>
              </invoice-notes>
              <process-control id="bg-2">
                <business-process-type id="bt-23">urn:fdc:peppol.eu:2017:poacc:billing:01:1.0</business-process-type>
                <specification-identifier id="bt-24">
                  <content>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</content>
                </specification-identifier>
              </process-control>
              <preceding-invoice-references id="bg-3">
                <preceding-invoice-reference id="bg-3">
                  <preceding-invoice-reference id="bt-25">PIR1234567890</preceding-invoice-reference>
                  <preceding-invoice-issue-date id="bt-26">2018-02-04</preceding-invoice-issue-date>
                </preceding-invoice-reference>
              </preceding-invoice-references>
              <seller id="bg-4">
                <seller-name id="bt-27">[Seller name]</seller-name>
                <seller-trading-name id="bt-28">[Seller trading name]</seller-trading-name>
                <seller-identifiers id="bt-29">
                  <seller-identifier id="bt-29">
                    <content>9876543217894897438</content>
                  </seller-identifier>
                  <seller-identifier id="bt-29">
                    <content>748387437438</content>
                  </seller-identifier>
                  <seller-identifier id="bt-29">
                    <content>987654321</content>
                    <scheme-identifier>0088</scheme-identifier>
                  </seller-identifier>
                </seller-identifiers>
                <seller-legal-registration-identifier id="bt-30">
                  <content>123456789</content>
                  <scheme-identifier>0088</scheme-identifier>
                </seller-legal-registration-identifier>
                <seller-vat-identifier id="bt-31">
                  <content>ATU123456789</content>
                </seller-vat-identifier>
                <seller-tax-registration-identifier id="bt-32">
                  <content>123/456/789</content>
                </seller-tax-registration-identifier>
                <seller-additional-legal-information id="bt-33">Amtsgericht […], Geschäftsführer: […], Sitz der Gesellschaft […], Aufsichtsratvorsitzender: […]</seller-additional-legal-information>
                <seller-electronic-address id="bt-34">
                  <content>rechnungsausgang@test.com</content>
                  <scheme-identifier>EM</scheme-identifier>
                </seller-electronic-address>
                <seller-postal-address id="bg-5">
                  <seller-address-line-1 id="bt-35">[Seller address line 1]</seller-address-line-1>
                  <seller-address-line-2 id="bt-36">[Seller address line 2]</seller-address-line-2>
                  <seller-address-line-3 id="bt-162">[Seller address line 3]</seller-address-line-3>
                  <seller-city id="bt-37">[Seller city]</seller-city>
                  <seller-post-code id="bt-38">12345</seller-post-code>
                  <seller-country-subdivision id="bt-39">Bayern</seller-country-subdivision>
                  <seller-country-code id="bt-40">DE</seller-country-code>
                </seller-postal-address>
                <seller-contact id="bg-6">
                  <seller-contact-point id="bt-41">Tim Tester</seller-contact-point>
                  <seller-contact-telephone-number id="bt-42">012 3456789</seller-contact-telephone-number>
                  <seller-contact-email-address id="bt-43">tim.tester@test.com</seller-contact-email-address>
                </seller-contact>
              </seller>
              <buyer id="bg-7">
                <buyer-name id="bt-44">[Buyer name]</buyer-name>
                <buyer-trading-name id="bt-45">[Buyer trading name]</buyer-trading-name>
                <buyer-identifier id="bt-46">
                  <content>138</content>
                </buyer-identifier>
                <buyer-legal-registration-identifier id="bt-47">
                  <content>90000000-03083-72</content>
                  <scheme-identifier>0204</scheme-identifier>
                </buyer-legal-registration-identifier>
                <buyer-vat-identifier id="bt-48">
                  <content>DE12345ABC</content>
                </buyer-vat-identifier>
                <buyer-electronic-address id="bt-49">
                  <content>rechnungseingang@test.de</content>
                  <scheme-identifier>EM</scheme-identifier>
                </buyer-electronic-address>
                <buyer-postal-address id="bg-8">
                  <buyer-address-line-1 id="bt-50">[Buyer address line 1]</buyer-address-line-1>
                  <buyer-address-line-2 id="bt-51">[Buyer address line 2]</buyer-address-line-2>
                  <buyer-address-line-3 id="bt-163">[Buyer address line 3]</buyer-address-line-3>
                  <buyer-city id="bt-52">[Buyer city]</buyer-city>
                  <buyer-post-code id="bt-53">98765</buyer-post-code>
                  <buyer-country-subdivision id="bt-54">Bayern</buyer-country-subdivision>
                  <buyer-country-code id="bt-55">DE</buyer-country-code>
                </buyer-postal-address>
                <buyer-contact id="bg-9">
                  <buyer-contact-point id="bt-56">Tina Tester</buyer-contact-point>
                  <buyer-contact-telephone-number id="bt-57">0800 123456</buyer-contact-telephone-number>
                  <buyer-contact-email-address id="bt-58">tester@test.de</buyer-contact-email-address>
                </buyer-contact>
              </buyer>
              <payee id="bg-10">
                <payee-name id="bt-59">[Payee name]</payee-name>
                <payee-identifier id="bt-60">
                  <content>74</content>
                </payee-identifier>
                <payee-legal-registration-identifier id="bt-61">
                  <content>90000000-03083-72</content>
                  <scheme-identifier>0204</scheme-identifier>
                </payee-legal-registration-identifier>
              </payee>
              <seller-tax-representative-party id="bg-11">
                <seller-tax-representative-name id="bt-62">[Seller tax representative name]</seller-tax-representative-name>
                <seller-tax-representative-vat-identifier id="bt-63">
                  <content>DE124567</content>
                </seller-tax-representative-vat-identifier>
                <seller-tax-representative-postal-address id="bg-12">
                  <tax-representative-address-line-1 id="bt-64">[Seller tax representative address line 1]</tax-representative-address-line-1>
                  <tax-representative-address-line-2 id="bt-65">[Seller tax representative address line 2]</tax-representative-address-line-2>
                  <tax-representative-address-line-3 id="bt-164">[Seller tax representative address line 3]</tax-representative-address-line-3>
                  <tax-representative-city id="bt-66">[Seller tax representative city]</tax-representative-city>
                  <tax-representative-post-code id="bt-67">12345</tax-representative-post-code>
                  <tax-representative-country-subdivision id="bt-68">Bayern</tax-representative-country-subdivision>
                  <tax-representative-country-code id="bt-69">DE</tax-representative-country-code>
                </seller-tax-representative-postal-address>
              </seller-tax-representative-party>
              <delivery-information id="bg-13">
                <deliver-to-party-name id="bt-70">[Deliver to party name]</deliver-to-party-name>
                <deliver-to-location-identifier id="bt-71">
                  <content>68</content>
                </deliver-to-location-identifier>
                <actual-delivery-date id="bt-72">2018-04-13</actual-delivery-date>
                <invoicing-period id="bg-14">
                  <invoicing-period-start-date id="bt-73">2018-04-13</invoicing-period-start-date>
                  <invoicing-period-end-date id="bt-74">2018-04-13</invoicing-period-end-date>
                </invoicing-period>
                <deliver-to-address id="bg-15">
                  <deliver-to-address-line-1 id="bt-75">[Deliver to street]</deliver-to-address-line-1>
                  <deliver-to-address-line-2 id="bt-76">4. OG</deliver-to-address-line-2>
                  <deliver-to-address-line-3 id="bt-165">More Details</deliver-to-address-line-3>
                  <deliver-to-city id="bt-77">[Deliver to city]</deliver-to-city>
                  <deliver-to-post-code id="bt-78">98765</deliver-to-post-code>
                  <deliver-to-country-subdivision id="bt-79">Bayern</deliver-to-country-subdivision>
                  <deliver-to-country-code id="bt-80">DE</deliver-to-country-code>
                </deliver-to-address>
              </delivery-information>
              <payment-instructions id="bg-16">
                <payment-means-type-code id="bt-81">58</payment-means-type-code>
                <payment-means-text id="bt-82">[Payment means text]</payment-means-text>
                <remittance-information id="bt-83">Deb. 12345 / Fact. 9876543</remittance-information>
                <credit-transfers id="bg-17">
                  <credit-transfer id="bg-17">
                    <payment-account-identifier id="bt-84">
                      <content>DE75512108001245126199</content>
                    </payment-account-identifier>
                    <payment-account-name id="bt-85">[Payment account name]</payment-account-name>
                    <payment-service-provider-identifier id="bt-86">
                      <content>[BIC]</content>
                    </payment-service-provider-identifier>
                  </credit-transfer>
                </credit-transfers>
              </payment-instructions>
              <document-level-allowances id="bg-20">
                <document-level-allowance id="bg-20">
                  <document-level-allowance-amount id="bt-92">10</document-level-allowance-amount>
                  <document-level-allowance-base-amount id="bt-93">100</document-level-allowance-base-amount>
                  <document-level-allowance-percentage id="bt-94">10</document-level-allowance-percentage>
                  <document-level-allowance-vat-category-code id="bt-95">E</document-level-allowance-vat-category-code>
                  <document-level-allowance-vat-rate id="bt-96">0</document-level-allowance-vat-rate>
                  <document-level-allowance-reason id="bt-97">Fixed long term</document-level-allowance-reason>
                  <document-level-allowance-reason-code id="bt-98">102</document-level-allowance-reason-code>
                </document-level-allowance>
                <document-level-allowance id="bg-20">
                  <document-level-allowance-amount id="bt-92">10</document-level-allowance-amount>
                  <document-level-allowance-base-amount id="bt-93">100</document-level-allowance-base-amount>
                  <document-level-allowance-percentage id="bt-94">10</document-level-allowance-percentage>
                  <document-level-allowance-vat-category-code id="bt-95">E</document-level-allowance-vat-category-code>
                  <document-level-allowance-vat-rate id="bt-96">0</document-level-allowance-vat-rate>
                  <document-level-allowance-reason id="bt-97">Fixed long term 2</document-level-allowance-reason>
                  <document-level-allowance-reason-code id="bt-98">102</document-level-allowance-reason-code>
                </document-level-allowance>
              </document-level-allowances>
              <document-level-charges id="bg-21">
                <document-level-charge id="bg-21">
                  <document-level-charge-amount id="bt-99">10</document-level-charge-amount>
                  <document-level-charge-base-amount id="bt-100">100</document-level-charge-base-amount>
                  <document-level-charge-percentage id="bt-101">10</document-level-charge-percentage>
                  <document-level-charge-vat-category-code id="bt-102">E</document-level-charge-vat-category-code>
                  <document-level-charge-vat-rate id="bt-103">0</document-level-charge-vat-rate>
                  <document-level-charge-reason id="bt-104">Testing</document-level-charge-reason>
                  <document-level-charge-reason-code id="bt-105">TAC</document-level-charge-reason-code>
                </document-level-charge>
                <document-level-charge id="bg-21">
                  <document-level-charge-amount id="bt-99">10</document-level-charge-amount>
                  <document-level-charge-base-amount id="bt-100">100</document-level-charge-base-amount>
                  <document-level-charge-percentage id="bt-101">10</document-level-charge-percentage>
                  <document-level-charge-vat-category-code id="bt-102">E</document-level-charge-vat-category-code>
                  <document-level-charge-vat-rate id="bt-103">0</document-level-charge-vat-rate>
                  <document-level-charge-reason id="bt-104">Testing 2</document-level-charge-reason>
                  <document-level-charge-reason-code id="bt-105">TAC</document-level-charge-reason-code>
                </document-level-charge>
              </document-level-charges>
              <document-totals id="bg-22">
                <sum-of-invoice-line-net-amount id="bt-106">10781.25</sum-of-invoice-line-net-amount>
                <sum-of-allowances-on-document-level id="bt-107">20</sum-of-allowances-on-document-level>
                <sum-of-charges-on-document-level id="bt-108">20</sum-of-charges-on-document-level>
                <invoice-total-amount-without-vat id="bt-109">10781.25</invoice-total-amount-without-vat>
                <invoice-total-vat-amount id="bt-110">2048.44</invoice-total-vat-amount>
                <invoice-total-vat-amount-in-accounting-currency id="bt-111">2048.44</invoice-total-vat-amount-in-accounting-currency>
                <invoice-total-amount-with-vat id="bt-112">12829.69</invoice-total-amount-with-vat>
                <paid-amount id="bt-113">0</paid-amount>
                <rounding-amount id="bt-114">0</rounding-amount>
                <amount-due-for-payment id="bt-115">12829.69</amount-due-for-payment>
              </document-totals>
              <vat-breakdown id="bg-23">
                <vat-breakdown id="bg-23">
                  <vat-category-taxable-amount id="bt-116">10781.25</vat-category-taxable-amount>
                  <vat-category-tax-amount id="bt-117">2048.44</vat-category-tax-amount>
                  <vat-category-code id="bt-118">S</vat-category-code>
                  <vat-category-rate id="bt-119">19</vat-category-rate>
                </vat-breakdown>
                <vat-breakdown id="bg-23">
                  <vat-category-taxable-amount id="bt-116">0</vat-category-taxable-amount>
                  <vat-category-tax-amount id="bt-117">0</vat-category-tax-amount>
                  <vat-category-code id="bt-118">E</vat-category-code>
                  <vat-category-rate id="bt-119">0</vat-category-rate>
                  <vat-exemption-reason-text id="bt-120">[VAT exemption reason text]</vat-exemption-reason-text>
                  <vat-exemption-reason-code id="bt-121">VATEX-EU-132-1A</vat-exemption-reason-code>
                </vat-breakdown>
              </vat-breakdown>
              <additional-supporting-documents id="bg-24">
                <additional-supporting-document id="bg-24">
                  <supporting-document-reference id="bt-122">01_15_Anhang_01.pdf</supporting-document-reference>
                  <supporting-document-description id="bt-123">Aufschlüsselung der einzelnen Leistungspositionen</supporting-document-description>
                  <external-document-location id="bt-124">https://xeinkauf.de/xrechnung/</external-document-location>
                  <attached-document id="bt-125">
                    <content>{content1.Base64Content}</content>
                    <mime-code>application/pdf</mime-code>
                    <filename>01_15_Anhang_01.pdf</filename>
                  </attached-document>
                </additional-supporting-document>
                <additional-supporting-document id="bg-24">
                  <supporting-document-reference id="bt-122">01_15_Anhang_02.pdf</supporting-document-reference>
                  <supporting-document-description id="bt-123">Aufschlüsselung der einzelnen Leistungspositionen</supporting-document-description>
                  <external-document-location id="bt-124">https://xeinkauf.de/xrechnung/</external-document-location>
                  <attached-document id="bt-125">
                    <content>{content2.Base64Content}</content>
                    <mime-code>application/pdf</mime-code>
                    <filename>01_15_Anhang_02.pdf</filename>
                  </attached-document>
                </additional-supporting-document>
              </additional-supporting-documents>
              <invoice-lines id="bg-25">
                <invoice-line id="bg-25">
                  <invoice-line-identifier id="bt-126">
                    <content>1</content>
                  </invoice-line-identifier>
                  <invoice-line-note id="bt-127">Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag</invoice-line-note>
                  <invoice-line-object-identifier id="bt-128">
                    <content>ANG987654321</content>
                    <scheme-identifier>ABZ</scheme-identifier>
                  </invoice-line-object-identifier>
                  <invoiced-quantity id="bt-129">30.00000000000</invoiced-quantity>
                  <invoiced-quantity-unit-of-measure-code id="bt-130">XPP</invoiced-quantity-unit-of-measure-code>
                  <invoice-line-net-amount id="bt-131">4743.75</invoice-line-net-amount>
                  <referenced-purchase-order-line-reference id="bt-132">6171175.1</referenced-purchase-order-line-reference>
                  <invoice-line-buyer-accounting-reference id="bt-133">Konto 1</invoice-line-buyer-accounting-reference>
                  <invoice-line-period id="bg-26">
                    <invoice-line-period-start-date id="bt-134">2018-04-13</invoice-line-period-start-date>
                    <invoice-line-period-end-date id="bt-135">2018-04-13</invoice-line-period-end-date>
                  </invoice-line-period>
                  <invoice-line-allowances id="bg-27">
                    <invoice-line-allowance id="bg-27">
                      <invoice-line-allowance-amount id="bt-136">20</invoice-line-allowance-amount>
                      <invoice-line-allowance-base-amount id="bt-137">200</invoice-line-allowance-base-amount>
                      <invoice-line-allowance-percentage id="bt-138">10</invoice-line-allowance-percentage>
                      <invoice-line-allowance-reason id="bt-139">Fixed long term</invoice-line-allowance-reason>
                      <invoice-line-allowance-reason-code id="bt-140">102</invoice-line-allowance-reason-code>
                    </invoice-line-allowance>
                    <invoice-line-allowance id="bg-27">
                      <invoice-line-allowance-amount id="bt-136">20</invoice-line-allowance-amount>
                      <invoice-line-allowance-base-amount id="bt-137">200</invoice-line-allowance-base-amount>
                      <invoice-line-allowance-percentage id="bt-138">10</invoice-line-allowance-percentage>
                      <invoice-line-allowance-reason id="bt-139">Fixed long term 2</invoice-line-allowance-reason>
                      <invoice-line-allowance-reason-code id="bt-140">102</invoice-line-allowance-reason-code>
                    </invoice-line-allowance>
                  </invoice-line-allowances>
                  <invoice-line-charges id="bg-28">
                    <invoice-line-charge id="bg-28">
                      <invoice-line-charge-amount id="bt-141">20</invoice-line-charge-amount>
                      <invoice-line-charge-base-amount id="bt-142">200</invoice-line-charge-base-amount>
                      <invoice-line-charge-percentage id="bt-143">10</invoice-line-charge-percentage>
                      <invoice-line-charge-reason id="bt-144">Testing</invoice-line-charge-reason>
                      <invoice-line-charge-reason-code id="bt-145">TAC</invoice-line-charge-reason-code>
                    </invoice-line-charge>
                    <invoice-line-charge id="bg-28">
                      <invoice-line-charge-amount id="bt-141">20</invoice-line-charge-amount>
                      <invoice-line-charge-base-amount id="bt-142">200</invoice-line-charge-base-amount>
                      <invoice-line-charge-percentage id="bt-143">10</invoice-line-charge-percentage>
                      <invoice-line-charge-reason id="bt-144">Testing 2</invoice-line-charge-reason>
                      <invoice-line-charge-reason-code id="bt-145">TAC</invoice-line-charge-reason-code>
                    </invoice-line-charge>
                  </invoice-line-charges>
                  <price-details id="bg-29">
                    <item-net-price id="bt-146">158.1250000000</item-net-price>
                    <item-price-discount id="bt-147">10.000000000000000000</item-price-discount>
                    <item-gross-price id="bt-148">168.1250000000</item-gross-price>
                    <item-price-base-quantity id="bt-149">1</item-price-base-quantity>
                    <item-price-base-quantity-unit-of-measure-code id="bt-150">XPP</item-price-base-quantity-unit-of-measure-code>
                  </price-details>
                  <line-vat-information id="bg-30">
                    <invoiced-item-vat-category-code id="bt-151">S</invoiced-item-vat-category-code>
                    <invoiced-item-vat-rate id="bt-152">19</invoiced-item-vat-rate>
                  </line-vat-information>
                  <item-information id="bg-31">
                    <item-name id="bt-153">Beratung</item-name>
                    <item-description id="bt-154">Anforderungsmanagement</item-description>
                    <item-sellers-identifier id="bt-155">
                      <content>1034</content>
                    </item-sellers-identifier>
                    <item-buyers-identifier id="bt-156">
                      <content>5034</content>
                    </item-buyers-identifier>
                    <item-standard-identifier id="bt-157">
                      <content>123456789</content>
                      <scheme-identifier>0088</scheme-identifier>
                    </item-standard-identifier>
                    <item-classification-identifiers id="bt-158">
                      <item-classification-identifier id="bt-158">
                        <content>0721-880X</content>
                        <scheme-identifier>IB</scheme-identifier>
                        <scheme-version-identifier>88</scheme-version-identifier>
                      </item-classification-identifier>
                      <item-classification-identifier id="bt-158">
                        <content>0721-880XYZ</content>
                        <scheme-identifier>IB</scheme-identifier>
                        <scheme-version-identifier>88</scheme-version-identifier>
                      </item-classification-identifier>
                    </item-classification-identifiers>
                    <item-country-of-origin id="bt-159">DE</item-country-of-origin>
                    <item-attributes id="bg-32">
                      <item-attribute id="bg-32">
                        <item-attribute-name id="bt-160">[Description]</item-attribute-name>
                        <item-attribute-value id="bt-161">[Value]</item-attribute-value>
                      </item-attribute>
                      <item-attribute id="bg-32">
                        <item-attribute-name id="bt-160">[Description 2]</item-attribute-name>
                        <item-attribute-value id="bt-161">[Value 2]</item-attribute-value>
                      </item-attribute>
                    </item-attributes>
                  </item-information>
                </invoice-line>
                <invoice-line id="bg-25">
                  <invoice-line-identifier id="bt-126">
                    <content>2</content>
                  </invoice-line-identifier>
                  <invoiced-quantity id="bt-129">42</invoiced-quantity>
                  <invoiced-quantity-unit-of-measure-code id="bt-130">XPP</invoiced-quantity-unit-of-measure-code>
                  <invoice-line-net-amount id="bt-131">6037.5</invoice-line-net-amount>
                  <price-details id="bg-29">
                    <item-net-price id="bt-146">143.75</item-net-price>
                  </price-details>
                  <line-vat-information id="bg-30">
                    <invoiced-item-vat-category-code id="bt-151">S</invoiced-item-vat-category-code>
                    <invoiced-item-vat-rate id="bt-152">19</invoiced-item-vat-rate>
                  </line-vat-information>
                  <item-information id="bg-31">
                    <item-name id="bt-153">Beratung</item-name>
                  </item-information>
                </invoice-line>
              </invoice-lines>
            </invoice>
            """;

        Invoice expected = new Invoice
        {
            InvoiceNumber = new Identifier("1234567"),
            InvoiceIssueDate = new Date(new DateTime(2018, 4, 13)),
            InvoiceTypeCode = new Code("380"),
            InvoiceCurrencyCode = new Code("EUR"),
            VatAccountingCurrencyCode = new Code("GBP"),
            ValueAddedTaxPointDate = new Date(new DateTime(2018, 4, 13)),
            ValueAddedTaxPointDateCode = null,
            PaymentDueDate = new Date(new DateTime(2018, 4, 13)),
            BuyerReference = new Text("90000000-03083-72"),
            ProjectReference = new DocumentReference("PR12345678"),
            ContractReference = new DocumentReference("0000000752"),
            PurchaseOrderReference = new DocumentReference("65002278"),
            SalesOrderReference = new DocumentReference("ABC123456789"),
            ReceivingAdviceReference = new DocumentReference("RAR123456789"),
            DespatchAdviceReference = new DocumentReference("DAR123456789"),
            TenderOrLotReference = new DocumentReference("ANG987654321"),
            InvoicedObjectIdentifier = new Identifier("OK987654321"),
            BuyerAccountingReference = new Text("Buchungscode1"),
            PaymentTerms = new Text("Beschreibung 1: Bitte überweisen Sie bis zum 13.04.2018 auf das unten aufgeführte Konto."),
            InvoiceNotes = [
                new InvoiceNote {
                    InvoiceNoteSubjectCode = new Code("AAC"),
                    Note = new Text("Invoice Note Description"),
                },
                new InvoiceNote {
                    InvoiceNoteSubjectCode = new Code("AAC"),
                    Note = new Text("Invoice Note Description 2"),
                },
            ],
            ProcessControl = new ProcessControl
            {
                BusinessProcessType = new Text("urn:fdc:peppol.eu:2017:poacc:billing:01:1.0"),
                SpecificationIdentifier = new Identifier("urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0"),
            },
            PrecedingInvoiceReferences = [
                new PrecedingInvoiceReference {
                    Reference = new DocumentReference("PIR1234567890"),
                    PrecedingInvoiceIssueDate = new Date(new DateTime(2018, 2, 4)),
                },
            ],
            Seller = new Seller
            {
                SellerName = new Text("[Seller name]"),
                SellerTradingName = new Text("[Seller trading name]"),
                SellerIdentifiers = [
                    new Identifier("9876543217894897438"),
                    new Identifier("748387437438"),
                    new Identifier("987654321", "0088")
                ],
                SellerLegalRegistrationIdentifier = new Identifier("123456789", "0088"),
                SellerVatIdentifier = new Identifier("ATU123456789"),
                SellerTaxRegistrationIdentifier = new Identifier("123/456/789"),
                SellerAdditionalLegalInformation = new Text("Amtsgericht […], Geschäftsführer: […], Sitz der Gesellschaft […], Aufsichtsratvorsitzender: […]"),
                SellerElectronicAddress = new Identifier("rechnungsausgang@test.com", "EM"),
                SellerPostalAddress = new SellerPostalAddress
                {
                    SellerAddressLine1 = new Text("[Seller address line 1]"),
                    SellerAddressLine2 = new Text("[Seller address line 2]"),
                    SellerAddressLine3 = new Text("[Seller address line 3]"),
                    SellerCity = new Text("[Seller city]"),
                    SellerPostCode = new Text("12345"),
                    SellerCountrySubdivision = new Text("Bayern"),
                    SellerCountryCode = new Code("DE"),
                },
                SellerContact = new SellerContact
                {
                    SellerContactPoint = new Text("Tim Tester"),
                    SellerContactTelephoneNumber = new Text("012 3456789"),
                    SellerContactEmailAddress = new Text("tim.tester@test.com"),
                },
            },
            Buyer = new Buyer
            {
                BuyerName = new Text("[Buyer name]"),
                BuyerTradingName = new Text("[Buyer trading name]"),
                BuyerIdentifier = new Identifier("138"),
                BuyerLegalRegistrationIdentifier = new Identifier("90000000-03083-72", "0204"),
                BuyerVatIdentifier = new Identifier("DE12345ABC"),
                BuyerElectronicAddress = new Identifier("rechnungseingang@test.de", "EM"),
                BuyerPostalAddress = new BuyerPostalAddress
                {
                    BuyerAddressLine1 = new Text("[Buyer address line 1]"),
                    BuyerAddressLine2 = new Text("[Buyer address line 2]"),
                    BuyerAddressLine3 = new Text("[Buyer address line 3]"),
                    BuyerCity = new Text("[Buyer city]"),
                    BuyerPostCode = new Text("98765"),
                    BuyerCountrySubdivision = new Text("Bayern"),
                    BuyerCountryCode = new Code("DE"),
                },
                BuyerContact = new BuyerContact
                {
                    BuyerContactPoint = new Text("Tina Tester"),
                    BuyerContactTelephoneNumber = new Text("0800 123456"),
                    BuyerContactEmailAddress = new Text("tester@test.de"),
                },
            },
            Payee = new Payee
            {
                PayeeName = new Text("[Payee name]"),
                PayeeIdentifier = new Identifier("74"),
                PayeeLegalRegistrationIdentifier = new Identifier("90000000-03083-72", "0204"),
            },
            SellerTaxRepresentativeParty = new SellerTaxRepresentativeParty
            {
                SellerTaxRepresentativeName = new Text("[Seller tax representative name]"),
                SellerTaxRepresentativeVatIdentifier = new Identifier("DE124567"),
                SellerTaxRepresentativePostalAddress = new SellerTaxRepresentativePostalAddress
                {
                    TaxRepresentativeAddressLine1 = new Text("[Seller tax representative address line 1]"),
                    TaxRepresentativeAddressLine2 = new Text("[Seller tax representative address line 2]"),
                    TaxRepresentativeAddressLine3 = new Text("[Seller tax representative address line 3]"),
                    TaxRepresentativeCity = new Text("[Seller tax representative city]"),
                    TaxRepresentativePostCode = new Text("12345"),
                    TaxRepresentativeCountrySubdivision = new Text("Bayern"),
                    TaxRepresentativeCountryCode = new Code("DE"),
                },
            },
            DeliveryInformation = new DeliveryInformation
            {
                DeliverToPartyName = new Text("[Deliver to party name]"),
                DeliverToLocationIdentifier = new Identifier("68"),
                ActualDeliveryDate = new Date(new DateTime(2018, 4, 13)),
                InvoicingPeriod = new InvoicingPeriod
                {
                    InvoicingPeriodStartDate = new Date(new DateTime(2018, 4, 13)),
                    InvoicingPeriodEndDate = new Date(new DateTime(2018, 4, 13)),
                },
                DeliverToAddress = new DeliverToAddress
                {
                    DeliverToAddressLine1 = new Text("[Deliver to street]"),
                    DeliverToAddressLine2 = new Text("4. OG"),
                    DeliverToAddressLine3 = new Text("More Details"),
                    DeliverToCity = new Text("[Deliver to city]"),
                    DeliverToPostCode = new Text("98765"),
                    DeliverToCountrySubdivision = new Text("Bayern"),
                    DeliverToCountryCode = new Code("DE"),
                },
            },
            PaymentInstructions = new PaymentInstructions
            {
                PaymentMeansTypeCode = new Code("58"),
                PaymentMeansText = new Text("[Payment means text]"),
                RemittanceInformation = new Text("Deb. 12345 / Fact. 9876543"),
                CreditTransfers = [
                    new CreditTransfer {
                        PaymentAccountIdentifier = new Identifier("DE75512108001245126199"),
                        PaymentAccountName = new Text("[Payment account name]"),
                        PaymentServiceProviderIdentifier = new Identifier("[BIC]"),
                    },
                ],
                PaymentCardInformation = null,
                DirectDebit = null,
            },
            DocumentLevelAllowances = [
                new DocumentLevelAllowance {
                    DocumentLevelAllowanceAmount = new Amount(10m),
                    DocumentLevelAllowanceBaseAmount = new Amount(100m),
                    DocumentLevelAllowancePercentage = new Percentage(10m),
                    DocumentLevelAllowanceVatCategoryCode = new Code("E"),
                    DocumentLevelAllowanceVatRate = new Percentage(0m),
                    DocumentLevelAllowanceReason = new Text("Fixed long term"),
                    DocumentLevelAllowanceReasonCode = new Code("102"),
                },
                new DocumentLevelAllowance {
                    DocumentLevelAllowanceAmount = new Amount(10m),
                    DocumentLevelAllowanceBaseAmount = new Amount(100m),
                    DocumentLevelAllowancePercentage = new Percentage(10m),
                    DocumentLevelAllowanceVatCategoryCode = new Code("E"),
                    DocumentLevelAllowanceVatRate = new Percentage(0m),
                    DocumentLevelAllowanceReason = new Text("Fixed long term 2"),
                    DocumentLevelAllowanceReasonCode = new Code("102"),
                },
            ],
            DocumentLevelCharges = [
                new DocumentLevelCharge {
                    DocumentLevelChargeAmount = new Amount(10m),
                    DocumentLevelChargeBaseAmount = new Amount(100m),
                    DocumentLevelChargePercentage = new Percentage(10m),
                    DocumentLevelChargeVatCategoryCode = new Code("E"),
                    DocumentLevelChargeVatRate = new Percentage(0m),
                    DocumentLevelChargeReason = new Text("Testing"),
                    DocumentLevelChargeReasonCode = new Code("TAC"),
                },
                new DocumentLevelCharge {
                    DocumentLevelChargeAmount = new Amount(10m),
                    DocumentLevelChargeBaseAmount = new Amount(100m),
                    DocumentLevelChargePercentage = new Percentage(10m),
                    DocumentLevelChargeVatCategoryCode = new Code("E"),
                    DocumentLevelChargeVatRate = new Percentage(0m),
                    DocumentLevelChargeReason = new Text("Testing 2"),
                    DocumentLevelChargeReasonCode = new Code("TAC"),
                },
            ],
            DocumentTotals = new DocumentTotals
            {
                SumOfInvoiceLineNetAmount = new Amount(10781.25m),
                SumOfAllowancesOnDocumentLevel = new Amount(20m),
                SumOfChargesOnDocumentLevel = new Amount(20m),
                InvoiceTotalAmountWithoutVat = new Amount(10781.25m),
                InvoiceTotalVatAmount = new Amount(2048.44m),
                InvoiceTotalVatAmountInAccountingCurrency = new Amount(2048.44m),
                InvoiceTotalAmountWithVat = new Amount(12829.69m),
                PaidAmount = new Amount(0),
                RoundingAmount = new Amount(0),
                AmountDueForPayment = new Amount(12829.69m),
            },
            VatBreakdown = [
                new VatBreakdown {
                    VatCategoryTaxableAmount = new Amount(10781.25m),
                    VatCategoryTaxAmount = new Amount(2048.44m),
                    VatCategoryCode = new Code("S"),
                    VatCategoryRate = new Percentage(19m),
                    VatExemptionReasonText = null,
                    VatExemptionReasonCode = null,
                },
                new VatBreakdown {
                    VatCategoryTaxableAmount = new Amount(0m),
                    VatCategoryTaxAmount = new Amount(0m),
                    VatCategoryCode = new Code("E"),
                    VatCategoryRate = new Percentage(0m),
                    VatExemptionReasonText = new Text("[VAT exemption reason text]"),
                    VatExemptionReasonCode = new Code("VATEX-EU-132-1A"),
                },
            ],
            AdditionalSupportingDocuments = [
                new AdditionalSupportingDocument {
                    SupportingDocumentReference = new DocumentReference("01_15_Anhang_01.pdf"),
                    SupportingDocumentDescription = new Text("Aufschlüsselung der einzelnen Leistungspositionen"),
                    ExternalDocumentLocation = new Text("https://xeinkauf.de/xrechnung/"),
                    AttachedDocument = new BinaryObject(
                        content1.RawContent,
                        "application/pdf",
                        "01_15_Anhang_01.pdf"
                    ),
                },
                new AdditionalSupportingDocument {
                    SupportingDocumentReference = new DocumentReference("01_15_Anhang_02.pdf"),
                    SupportingDocumentDescription = new Text("Aufschlüsselung der einzelnen Leistungspositionen"),
                    ExternalDocumentLocation = new Text("https://xeinkauf.de/xrechnung/"),
                    AttachedDocument = new BinaryObject(
                        content2.RawContent,
                        "application/pdf",
                        "01_15_Anhang_02.pdf"
                    ),
                },
            ],
            InvoiceLines = [
                new InvoiceLine {
                    InvoiceLineIdentifier = new Identifier("1"),
                    InvoiceLineNote = new Text("Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag"),
                    InvoiceLineObjectIdentifier = new Identifier("ANG987654321", "ABZ"),
                    InvoicedQuantity = new Quantity(30m),
                    InvoicedQuantityUnitOfMeasureCode = new Code("XPP"),
                    InvoiceLineNetAmount = new Amount(4743.75m),
                    ReferencedPurchaseOrderLineReference = new DocumentReference("6171175.1"),
                    InvoiceLineBuyerAccountingReference = new Text("Konto 1"),
                    InvoiceLinePeriod = new InvoiceLinePeriod {
                        InvoiceLinePeriodStartDate = new Date(new DateTime(2018, 04, 13)),
                        InvoiceLinePeriodEndDate = new Date(new DateTime(2018, 04, 13)),
                    },
                    InvoiceLineAllowances = [
                        new InvoiceLineAllowance {
                            InvoiceLineAllowanceAmount = new Amount(20m),
                            InvoiceLineAllowanceBaseAmount = new Amount(200m),
                            InvoiceLineAllowancePercentage = new Percentage(10m),
                            InvoiceLineAllowanceReason = new Text("Fixed long term"),
                            InvoiceLineAllowanceReasonCode = new Code("102"),
                        },
                        new InvoiceLineAllowance {
                            InvoiceLineAllowanceAmount = new Amount(20m),
                            InvoiceLineAllowanceBaseAmount = new Amount(200m),
                            InvoiceLineAllowancePercentage = new Percentage(10m),
                            InvoiceLineAllowanceReason = new Text("Fixed long term 2"),
                            InvoiceLineAllowanceReasonCode = new Code("102"),
                        }
                    ],
                    InvoiceLineCharges = [
                        new InvoiceLineCharge {
                            InvoiceLineChargeAmount = new Amount(20m),
                            InvoiceLineChargeBaseAmount = new Amount(200m),
                            InvoiceLineChargePercentage = new Percentage(10m),
                            InvoiceLineChargeReason = new Text("Testing"),
                            InvoiceLineChargeReasonCode = new Code("TAC"),
                        },
                        new InvoiceLineCharge {
                            InvoiceLineChargeAmount = new Amount(20m),
                            InvoiceLineChargeBaseAmount = new Amount(200m),
                            InvoiceLineChargePercentage = new Percentage(10m),
                            InvoiceLineChargeReason = new Text("Testing 2"),
                            InvoiceLineChargeReasonCode = new Code("TAC"),
                        },
                    ],
                    PriceDetails = new PriceDetails {
                        ItemNetPrice = new UnitPriceAmount(158.125m),
                        ItemPriceDiscount = new UnitPriceAmount(10m),
                        ItemGrossPrice = new UnitPriceAmount(168.125m),
                        ItemPriceBaseQuantity = new Quantity(1m),
                        ItemPriceBaseQuantityUnitOfMeasureCode = new Code("XPP"),
                    },
                    LineVatInformation = new LineVatInformation {
                        InvoicedItemVatCategoryCode = new Code("S"),
                        InvoicedItemVatRate = new Percentage(19m),
                    },
                    ItemInformation = new ItemInformation {
                        ItemName = new Text("Beratung"),
                        ItemDescription = new Text("Anforderungsmanagement"),
                        ItemSellersIdentifier = new Identifier("1034"),
                        ItemBuyersIdentifier = new Identifier("5034"),
                        ItemStandardIdentifier = new Identifier("123456789", "0088"),
                        ItemClassificationIdentifiers = [
                            new Identifier("0721-880X", "IB", "88"),
                            new Identifier("0721-880XYZ", "IB", "88"),
                        ],
                        ItemCountryOfOrigin = new Code("DE"),
                        ItemAttributes = [
                            new ItemAttribute {
                                ItemAttributeName = new Text("[Description]"),
                                ItemAttributeValue = new Text ("[Value]"),
                             },
                             new ItemAttribute {
                                 ItemAttributeName = new Text("[Description 2]"),
                                 ItemAttributeValue = new Text("[Value 2]"),
                              },
                        ],
                    },
                },
                new InvoiceLine {
                    InvoiceLineIdentifier = new Identifier("2"),
                    InvoiceLineNote = null,
                    InvoiceLineObjectIdentifier = null,
                    InvoicedQuantity = new Quantity(42m),
                    InvoicedQuantityUnitOfMeasureCode = new Code("XPP"),
                    InvoiceLineNetAmount = new Amount(6037.5m),
                    ReferencedPurchaseOrderLineReference = null,
                    InvoiceLineBuyerAccountingReference = null,
                    InvoiceLinePeriod = null,
                    InvoiceLineAllowances = [],
                    InvoiceLineCharges = [],
                    PriceDetails = new PriceDetails {
                        ItemNetPrice = new UnitPriceAmount(143.75m),
                        ItemPriceDiscount = null,
                        ItemGrossPrice = null,
                        ItemPriceBaseQuantity = null,
                        ItemPriceBaseQuantityUnitOfMeasureCode = null,
                    },
                    LineVatInformation = new LineVatInformation {
                        InvoicedItemVatCategoryCode = new Code("S"),
                        InvoicedItemVatRate = new Percentage(19m),
                    },
                    ItemInformation = new ItemInformation {
                        ItemName = new Text("Beratung"),
                        ItemDescription = null,
                        ItemSellersIdentifier = null,
                        ItemBuyersIdentifier = null,
                        ItemStandardIdentifier = null,
                        ItemClassificationIdentifiers = [],
                        ItemCountryOfOrigin = null,
                        ItemAttributes = [],
                    },
                },
            ],
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = Invoice.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeInvoiceNote()
    {
        string xml = """
            <invoice-note id="bg-1" xmlns="urn:todo">
              <invoice-note-subject-code id="bt-21">AAC</invoice-note-subject-code>
              <invoice-note id="bt-22">Invoice Note Description</invoice-note>
            </invoice-note>
            """;

        InvoiceNote expected = new InvoiceNote
        {
            InvoiceNoteSubjectCode = new Code("AAC"),
            Note = new Text("Invoice Note Description"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = InvoiceNote.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeProcessControl()
    {
        string xml = """
            <process-control id="bg-2" xmlns="urn:todo">
              <business-process-type id="bt-23">urn:fdc:peppol.eu:2017:poacc:billing:01:1.0</business-process-type>
              <specification-identifier id="bt-24">
                <content>urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0</content>
              </specification-identifier>
            </process-control>
            """;

        ProcessControl expected = new ProcessControl
        {
            BusinessProcessType = new Text("urn:fdc:peppol.eu:2017:poacc:billing:01:1.0"),
            SpecificationIdentifier = new Identifier("urn:cen.eu:en16931:2017#compliant#urn:xeinkauf.de:kosit:xrechnung_3.0"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = ProcessControl.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializePrecedingInvoiceReference()
    {
        string xml = """
            <preceding-invoice-reference id="bg-3" xmlns="urn:todo">
              <preceding-invoice-reference id="bt-25">PIR1234567890</preceding-invoice-reference>
              <preceding-invoice-issue-date id="bt-26">2018-02-04</preceding-invoice-issue-date>
            </preceding-invoice-reference>
            """;

        PrecedingInvoiceReference expected = new PrecedingInvoiceReference
        {
            Reference = new DocumentReference("PIR1234567890"),
            PrecedingInvoiceIssueDate = new Date(new DateTime(2018, 2, 4)),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = PrecedingInvoiceReference.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeSeller()
    {
        string xml = """
            <seller id="bg-4" xmlns="urn:todo">
              <seller-name id="bt-27">[Seller name]</seller-name>
              <seller-trading-name id="bt-28">[Seller trading name]</seller-trading-name>
              <seller-identifiers id="bt-29">
                <seller-identifier id="bt-29">
                  <content>9876543217894897438</content>
                </seller-identifier>
                <seller-identifier id="bt-29">
                  <content>748387437438</content>
                </seller-identifier>
                <seller-identifier id="bt-29">
                  <content>987654321</content>
                  <scheme-identifier>0088</scheme-identifier>
                </seller-identifier>
              </seller-identifiers>
              <seller-legal-registration-identifier id="bt-30">
                <content>123456789</content>
                <scheme-identifier>0088</scheme-identifier>
              </seller-legal-registration-identifier>
              <seller-vat-identifier id="bt-31">
                <content>ATU123456789</content>
              </seller-vat-identifier>
              <seller-tax-registration-identifier id="bt-32">
                <content>123/456/789</content>
              </seller-tax-registration-identifier>
              <seller-additional-legal-information id="bt-33">Amtsgericht […], Geschäftsführer: […], Sitz der Gesellschaft […], Aufsichtsratvorsitzender: […]</seller-additional-legal-information>
              <seller-electronic-address id="bt-34">
                <content>rechnungsausgang@test.com</content>
                <scheme-identifier>EM</scheme-identifier>
              </seller-electronic-address>
              <seller-postal-address id="bg-5">
                <seller-address-line-1 id="bt-35">[Seller address line 1]</seller-address-line-1>
                <seller-address-line-2 id="bt-36">[Seller address line 2]</seller-address-line-2>
                <seller-address-line-3 id="bt-162">[Seller address line 3]</seller-address-line-3>
                <seller-city id="bt-37">[Seller city]</seller-city>
                <seller-post-code id="bt-38">12345</seller-post-code>
                <seller-country-subdivision id="bt-39">Bayern</seller-country-subdivision>
                <seller-country-code id="bt-40">DE</seller-country-code>
              </seller-postal-address>
              <seller-contact id="bg-6">
                <seller-contact-point id="bt-41">Tim Tester</seller-contact-point>
                <seller-contact-telephone-number id="bt-42">012 3456789</seller-contact-telephone-number>
                <seller-contact-email-address id="bt-43">tim.tester@test.com</seller-contact-email-address>
              </seller-contact>
            </seller>
            """;

        Seller expected = new Seller
        {
            SellerName = new Text("[Seller name]"),
            SellerTradingName = new Text("[Seller trading name]"),
            SellerIdentifiers = [
                new Identifier("9876543217894897438"),
                new Identifier("748387437438"),
                new Identifier("987654321", "0088")
            ],
            SellerLegalRegistrationIdentifier = new Identifier("123456789", "0088"),
            SellerVatIdentifier = new Identifier("ATU123456789"),
            SellerTaxRegistrationIdentifier = new Identifier("123/456/789"),
            SellerAdditionalLegalInformation = new Text("Amtsgericht […], Geschäftsführer: […], Sitz der Gesellschaft […], Aufsichtsratvorsitzender: […]"),
            SellerElectronicAddress = new Identifier("rechnungsausgang@test.com", "EM"),
            SellerPostalAddress = new SellerPostalAddress
            {
                SellerAddressLine1 = new Text("[Seller address line 1]"),
                SellerAddressLine2 = new Text("[Seller address line 2]"),
                SellerAddressLine3 = new Text("[Seller address line 3]"),
                SellerCity = new Text("[Seller city]"),
                SellerPostCode = new Text("12345"),
                SellerCountrySubdivision = new Text("Bayern"),
                SellerCountryCode = new Code("DE"),
            },
            SellerContact = new SellerContact
            {
                SellerContactPoint = new Text("Tim Tester"),
                SellerContactTelephoneNumber = new Text("012 3456789"),
                SellerContactEmailAddress = new Text("tim.tester@test.com"),
            },
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = Seller.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeBuyer()
    {
        string xml = """
            <buyer id="bg-7" xmlns="urn:todo">
              <buyer-name id="bt-44">[Buyer name]</buyer-name>
              <buyer-trading-name id="bt-45">[Buyer trading name]</buyer-trading-name>
              <buyer-identifier id="bt-46">
                <content>138</content>
              </buyer-identifier>
              <buyer-legal-registration-identifier id="bt-47">
                <content>90000000-03083-72</content>
                <scheme-identifier>0204</scheme-identifier>
              </buyer-legal-registration-identifier>
              <buyer-vat-identifier id="bt-48">
                <content>DE12345ABC</content>
              </buyer-vat-identifier>
              <buyer-electronic-address id="bt-49">
                <content>rechnungseingang@test.de</content>
                <scheme-identifier>EM</scheme-identifier>
              </buyer-electronic-address>
              <buyer-postal-address id="bg-8">
                <buyer-address-line-1 id="bt-50">[Buyer address line 1]</buyer-address-line-1>
                <buyer-address-line-2 id="bt-51">[Buyer address line 2]</buyer-address-line-2>
                <buyer-address-line-3 id="bt-163">[Buyer address line 3]</buyer-address-line-3>
                <buyer-city id="bt-52">[Buyer city]</buyer-city>
                <buyer-post-code id="bt-53">98765</buyer-post-code>
                <buyer-country-subdivision id="bt-54">Bayern</buyer-country-subdivision>
                <buyer-country-code id="bt-55">DE</buyer-country-code>
              </buyer-postal-address>
              <buyer-contact id="bg-9">
                <buyer-contact-point id="bt-56">Tina Tester</buyer-contact-point>
                <buyer-contact-telephone-number id="bt-57">0800 123456</buyer-contact-telephone-number>
                <buyer-contact-email-address id="bt-58">tester@test.de</buyer-contact-email-address>
              </buyer-contact>
            </buyer>
            """;

        Buyer expected = new Buyer
        {
            BuyerName = new Text("[Buyer name]"),
            BuyerTradingName = new Text("[Buyer trading name]"),
            BuyerIdentifier = new Identifier("138"),
            BuyerLegalRegistrationIdentifier = new Identifier("90000000-03083-72", "0204"),
            BuyerVatIdentifier = new Identifier("DE12345ABC"),
            BuyerElectronicAddress = new Identifier("rechnungseingang@test.de", "EM"),
            BuyerPostalAddress = new BuyerPostalAddress
            {
                BuyerAddressLine1 = new Text("[Buyer address line 1]"),
                BuyerAddressLine2 = new Text("[Buyer address line 2]"),
                BuyerAddressLine3 = new Text("[Buyer address line 3]"),
                BuyerCity = new Text("[Buyer city]"),
                BuyerPostCode = new Text("98765"),
                BuyerCountrySubdivision = new Text("Bayern"),
                BuyerCountryCode = new Code("DE"),
            },
            BuyerContact = new BuyerContact
            {
                BuyerContactPoint = new Text("Tina Tester"),
                BuyerContactTelephoneNumber = new Text("0800 123456"),
                BuyerContactEmailAddress = new Text("tester@test.de"),
            },
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = Buyer.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializePayee()
    {
        string xml = """
            <payee id="bg-10" xmlns="urn:todo">
              <payee-name id="bt-59">[Payee name]</payee-name>
              <payee-identifier id="bt-60">
                <content>74</content>
              </payee-identifier>
              <payee-legal-registration-identifier id="bt-61">
                <content>90000000-03083-72</content>
                <scheme-identifier>0204</scheme-identifier>
              </payee-legal-registration-identifier>
            </payee>
            """;

        Payee expected = new Payee
        {
            PayeeName = new Text("[Payee name]"),
            PayeeIdentifier = new Identifier("74"),
            PayeeLegalRegistrationIdentifier = new Identifier("90000000-03083-72", "0204"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = Payee.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeSellerTaxRepresentativeParty()
    {
        string xml = """
            <seller-tax-representative-party id="bg-11" xmlns="urn:todo">
              <seller-tax-representative-name id="bt-62">[Seller tax representative name]</seller-tax-representative-name>
              <seller-tax-representative-vat-identifier id="bt-63">
                <content>DE124567</content>
              </seller-tax-representative-vat-identifier>
              <seller-tax-representative-postal-address id="bg-12">
                <tax-representative-address-line-1 id="bt-64">[Seller tax representative address line 1]</tax-representative-address-line-1>
                <tax-representative-address-line-2 id="bt-65">[Seller tax representative address line 2]</tax-representative-address-line-2>
                <tax-representative-address-line-3 id="bt-164">[Seller tax representative address line 3]</tax-representative-address-line-3>
                <tax-representative-city id="bt-66">[Seller tax representative city]</tax-representative-city>
                <tax-representative-post-code id="bt-67">12345</tax-representative-post-code>
                <tax-representative-country-subdivision id="bt-68">Bayern</tax-representative-country-subdivision>
                <tax-representative-country-code id="bt-69">DE</tax-representative-country-code>
              </seller-tax-representative-postal-address>
            </seller-tax-representative-party>
            """;

        SellerTaxRepresentativeParty expected = new SellerTaxRepresentativeParty
        {
            SellerTaxRepresentativeName = new Text("[Seller tax representative name]"),
            SellerTaxRepresentativeVatIdentifier = new Identifier("DE124567"),
            SellerTaxRepresentativePostalAddress = new SellerTaxRepresentativePostalAddress
            {
                TaxRepresentativeAddressLine1 = new Text("[Seller tax representative address line 1]"),
                TaxRepresentativeAddressLine2 = new Text("[Seller tax representative address line 2]"),
                TaxRepresentativeAddressLine3 = new Text("[Seller tax representative address line 3]"),
                TaxRepresentativeCity = new Text("[Seller tax representative city]"),
                TaxRepresentativePostCode = new Text("12345"),
                TaxRepresentativeCountrySubdivision = new Text("Bayern"),
                TaxRepresentativeCountryCode = new Code("DE"),
            },
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = SellerTaxRepresentativeParty.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeDeliveryInformation()
    {
        string xml = """
            <delivery-information id="bg-13" xmlns="urn:todo">
              <deliver-to-party-name id="bt-70">[Deliver to party name]</deliver-to-party-name>
              <deliver-to-location-identifier id="bt-71">
                <content>68</content>
              </deliver-to-location-identifier>
              <actual-delivery-date id="bt-72">2018-04-13</actual-delivery-date>
              <invoicing-period id="bg-14">
                <invoicing-period-start-date id="bt-73">2018-04-13</invoicing-period-start-date>
                <invoicing-period-end-date id="bt-74">2018-04-13</invoicing-period-end-date>
              </invoicing-period>
              <deliver-to-address id="bg-15">
                <deliver-to-address-line-1 id="bt-75">[Deliver to street]</deliver-to-address-line-1>
                <deliver-to-address-line-2 id="bt-76">4. OG</deliver-to-address-line-2>
                <deliver-to-address-line-3 id="bt-165">More Details</deliver-to-address-line-3>
                <deliver-to-city id="bt-77">[Deliver to city]</deliver-to-city>
                <deliver-to-post-code id="bt-78">98765</deliver-to-post-code>
                <deliver-to-country-subdivision id="bt-79">Bayern</deliver-to-country-subdivision>
                <deliver-to-country-code id="bt-80">DE</deliver-to-country-code>
              </deliver-to-address>
            </delivery-information>
            """;

        DeliveryInformation expected = new DeliveryInformation
        {
            DeliverToPartyName = new Text("[Deliver to party name]"),
            DeliverToLocationIdentifier = new Identifier("68"),
            ActualDeliveryDate = new Date(new DateTime(2018, 4, 13)),
            InvoicingPeriod = new InvoicingPeriod
            {
                InvoicingPeriodStartDate = new Date(new DateTime(2018, 4, 13)),
                InvoicingPeriodEndDate = new Date(new DateTime(2018, 4, 13)),
            },
            DeliverToAddress = new DeliverToAddress
            {
                DeliverToAddressLine1 = new Text("[Deliver to street]"),
                DeliverToAddressLine2 = new Text("4. OG"),
                DeliverToAddressLine3 = new Text("More Details"),
                DeliverToCity = new Text("[Deliver to city]"),
                DeliverToPostCode = new Text("98765"),
                DeliverToCountrySubdivision = new Text("Bayern"),
                DeliverToCountryCode = new Code("DE"),
            },
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = DeliveryInformation.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializePaymentInstructions()
    {
        string xml = """
            <payment-instructions id="bg-16" xmlns="urn:todo">
              <payment-means-type-code id="bt-81">58</payment-means-type-code>
              <payment-means-text id="bt-82">[Payment means text]</payment-means-text>
              <remittance-information id="bt-83">Deb. 12345 / Fact. 9876543</remittance-information>
              <credit-transfers id="bg-17">
                <credit-transfer id="bg-17">
                  <payment-account-identifier id="bt-84">
                    <content>DE75512108001245126199</content>
                  </payment-account-identifier>
                  <payment-account-name id="bt-85">[Payment account name]</payment-account-name>
                  <payment-service-provider-identifier id="bt-86">
                    <content>[BIC]</content>
                  </payment-service-provider-identifier>
                </credit-transfer>
              </credit-transfers>
            </payment-instructions>
            """;

        PaymentInstructions expected = new PaymentInstructions
        {
            PaymentMeansTypeCode = new Code("58"),
            PaymentMeansText = new Text("[Payment means text]"),
            RemittanceInformation = new Text("Deb. 12345 / Fact. 9876543"),
            CreditTransfers = [
                new CreditTransfer {
                    PaymentAccountIdentifier = new Identifier("DE75512108001245126199"),
                    PaymentAccountName = new Text("[Payment account name]"),
                    PaymentServiceProviderIdentifier = new Identifier("[BIC]"),
                },
            ],
            PaymentCardInformation = null,
            DirectDebit = null,
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = PaymentInstructions.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeCreditTransfer()
    {
        string xml = """
            <credit-transfer id="bg-17" xmlns="urn:todo">
              <payment-account-identifier id="bt-84">
                <content>DE75512108001245126199</content>
              </payment-account-identifier>
              <payment-account-name id="bt-85">[Payment account name]</payment-account-name>
              <payment-service-provider-identifier id="bt-86">
                <content>[BIC]</content>
              </payment-service-provider-identifier>
            </credit-transfer>
            """;

        CreditTransfer expected = new CreditTransfer
        {
            PaymentAccountIdentifier = new Identifier("DE75512108001245126199"),
            PaymentAccountName = new Text("[Payment account name]"),
            PaymentServiceProviderIdentifier = new Identifier("[BIC]"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = CreditTransfer.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializePaymentCardInformation()
    {
        string xml = """
            <payment-card-information id="bg-18" xmlns="urn:todo">
              <payment-card-primary-account-number id="bt-87">6789</payment-card-primary-account-number>
              <payment-card-holder-name id="bt-88">[Name cardholder]</payment-card-holder-name>
            </payment-card-information>
            """;

        PaymentCardInformation expected = new PaymentCardInformation
        {
            PaymentCardPrimaryAccountNumber = new Text("6789"),
            PaymentCardHolderName = new Text("[Name cardholder]"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = PaymentCardInformation.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeDirectDebit()
    {
        string xml = """
            <direct-debit id="bg-19" xmlns="urn:todo">
              <mandate-reference-identifier id="bt-89">
                <content>[Mandate Reference Identifier]</content>
              </mandate-reference-identifier>
              <bank-assigned-creditor-identifier id="bt-90">
                <content>[Bank assigned creditor identifier]</content>
              </bank-assigned-creditor-identifier>
              <debited-account-identifier id="bt-91">
                <content>DE75512108001245126199</content>
              </debited-account-identifier>
            </direct-debit>
            """;

        DirectDebit expected = new DirectDebit
        {
            MandateReferenceIdentifier = new Identifier("[Mandate Reference Identifier]"),
            BankAssignedCreditorIdentifier = new Identifier("[Bank assigned creditor identifier]"),
            DebitedAccountIdentifier = new Identifier("DE75512108001245126199"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = DirectDebit.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeDocumentLevelAllowance()
    {
        string xml = """
            <document-level-allowance id="bg-20" xmlns="urn:todo">
              <document-level-allowance-amount id="bt-92">10</document-level-allowance-amount>
              <document-level-allowance-base-amount id="bt-93">100</document-level-allowance-base-amount>
              <document-level-allowance-percentage id="bt-94">10</document-level-allowance-percentage>
              <document-level-allowance-vat-category-code id="bt-95">E</document-level-allowance-vat-category-code>
              <document-level-allowance-vat-rate id="bt-96">0</document-level-allowance-vat-rate>
              <document-level-allowance-reason id="bt-97">Fixed long term</document-level-allowance-reason>
              <document-level-allowance-reason-code id="bt-98">102</document-level-allowance-reason-code>
            </document-level-allowance>
            """;

        DocumentLevelAllowance expected = new DocumentLevelAllowance
        {
            DocumentLevelAllowanceAmount = new Amount(10m),
            DocumentLevelAllowanceBaseAmount = new Amount(100m),
            DocumentLevelAllowancePercentage = new Percentage(10m),
            DocumentLevelAllowanceVatCategoryCode = new Code("E"),
            DocumentLevelAllowanceVatRate = new Percentage(0m),
            DocumentLevelAllowanceReason = new Text("Fixed long term"),
            DocumentLevelAllowanceReasonCode = new Code("102"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = DocumentLevelAllowance.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeDocumentLevelCharge()
    {
        string xml = """
            <document-level-charge id="bg-21" xmlns="urn:todo">
              <document-level-charge-amount id="bt-99">10</document-level-charge-amount>
              <document-level-charge-base-amount id="bt-100">100</document-level-charge-base-amount>
              <document-level-charge-percentage id="bt-101">10</document-level-charge-percentage>
              <document-level-charge-vat-category-code id="bt-102">E</document-level-charge-vat-category-code>
              <document-level-charge-vat-rate id="bt-103">0</document-level-charge-vat-rate>
              <document-level-charge-reason id="bt-104">Testing</document-level-charge-reason>
              <document-level-charge-reason-code id="bt-105">TAC</document-level-charge-reason-code>
            </document-level-charge>
            """;

        DocumentLevelCharge expected = new DocumentLevelCharge
        {
            DocumentLevelChargeAmount = new Amount(10m),
            DocumentLevelChargeBaseAmount = new Amount(100m),
            DocumentLevelChargePercentage = new Percentage(10m),
            DocumentLevelChargeVatCategoryCode = new Code("E"),
            DocumentLevelChargeVatRate = new Percentage(0m),
            DocumentLevelChargeReason = new Text("Testing"),
            DocumentLevelChargeReasonCode = new Code("TAC"),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = DocumentLevelCharge.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeDocumentTotals()
    {
        string xml = """
            <document-totals id="bg-22" xmlns="urn:todo">
              <sum-of-invoice-line-net-amount id="bt-106">10781.25</sum-of-invoice-line-net-amount>
              <sum-of-allowances-on-document-level id="bt-107">20</sum-of-allowances-on-document-level>
              <sum-of-charges-on-document-level id="bt-108">20</sum-of-charges-on-document-level>
              <invoice-total-amount-without-vat id="bt-109">10781.25</invoice-total-amount-without-vat>
              <invoice-total-vat-amount id="bt-110">2048.44</invoice-total-vat-amount>
              <invoice-total-vat-amount-in-accounting-currency id="bt-111">2048.44</invoice-total-vat-amount-in-accounting-currency>
              <invoice-total-amount-with-vat id="bt-112">12829.69</invoice-total-amount-with-vat>
              <paid-amount id="bt-113">0</paid-amount>
              <rounding-amount id="bt-114">0</rounding-amount>
              <amount-due-for-payment id="bt-115">12829.69</amount-due-for-payment>
            </document-totals>
            """;

        DocumentTotals expected = new DocumentTotals
        {
            SumOfInvoiceLineNetAmount = new Amount(10781.25m),
            SumOfAllowancesOnDocumentLevel = new Amount(20m),
            SumOfChargesOnDocumentLevel = new Amount(20m),
            InvoiceTotalAmountWithoutVat = new Amount(10781.25m),
            InvoiceTotalVatAmount = new Amount(2048.44m),
            InvoiceTotalVatAmountInAccountingCurrency = new Amount(2048.44m),
            InvoiceTotalAmountWithVat = new Amount(12829.69m),
            PaidAmount = new Amount(0),
            RoundingAmount = new Amount(0),
            AmountDueForPayment = new Amount(12829.69m),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = DocumentTotals.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeVatBreakdown()
    {
        string xml = """
            <vat-breakdown id="bg-23" xmlns="urn:todo">
              <vat-category-taxable-amount id="bt-116">10781.25</vat-category-taxable-amount>
              <vat-category-tax-amount id="bt-117">2048.44</vat-category-tax-amount>
              <vat-category-code id="bt-118">S</vat-category-code>
              <vat-category-rate id="bt-119">19</vat-category-rate>
            </vat-breakdown>
            """;

        VatBreakdown expected = new VatBreakdown
        {
            VatCategoryTaxableAmount = new Amount(10781.25m),
            VatCategoryTaxAmount = new Amount(2048.44m),
            VatCategoryCode = new Code("S"),
            VatCategoryRate = new Percentage(19m),
            VatExemptionReasonText = null,
            VatExemptionReasonCode = null,
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = VatBreakdown.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeAdditionalSupportingDocument()
    {
        Blob content = new("hello world");

        string xml = $"""
            <additional-supporting-document id="bg-24" xmlns="urn:todo">
              <supporting-document-reference id="bt-122">01_15_Anhang_01.pdf</supporting-document-reference>
              <supporting-document-description id="bt-123">Aufschlüsselung der einzelnen Leistungspositionen</supporting-document-description>
              <external-document-location id="bt-124">https://xeinkauf.de/xrechnung/</external-document-location>
              <attached-document id="bt-125">
                <content>{content.Base64Content}</content>
                <mime-code>application/pdf</mime-code>
                <filename>01_15_Anhang_01.pdf</filename>
              </attached-document>
            </additional-supporting-document>
            """;

        AdditionalSupportingDocument expected = new AdditionalSupportingDocument
        {
            SupportingDocumentReference = new DocumentReference("01_15_Anhang_01.pdf"),
            SupportingDocumentDescription = new Text("Aufschlüsselung der einzelnen Leistungspositionen"),
            ExternalDocumentLocation = new Text("https://xeinkauf.de/xrechnung/"),
            AttachedDocument = new BinaryObject(
                content.RawContent,
                "application/pdf",
                "01_15_Anhang_01.pdf"
            ),
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = AdditionalSupportingDocument.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeInvoiceLine()
    {
        string xml = """
            <invoice-line id="bg-25" xmlns="urn:todo">
              <invoice-line-identifier id="bt-126">
                <content>1</content>
              </invoice-line-identifier>
              <invoice-line-note id="bt-127">Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag</invoice-line-note>
              <invoice-line-object-identifier id="bt-128">
                <content>ANG987654321</content>
                <scheme-identifier>ABZ</scheme-identifier>
              </invoice-line-object-identifier>
              <invoiced-quantity id="bt-129">30.00000000000</invoiced-quantity>
              <invoiced-quantity-unit-of-measure-code id="bt-130">XPP</invoiced-quantity-unit-of-measure-code>
              <invoice-line-net-amount id="bt-131">4743.75</invoice-line-net-amount>
              <referenced-purchase-order-line-reference id="bt-132">6171175.1</referenced-purchase-order-line-reference>
              <invoice-line-buyer-accounting-reference id="bt-133">Konto 1</invoice-line-buyer-accounting-reference>
              <invoice-line-period id="bg-26">
                <invoice-line-period-start-date id="bt-134">2018-04-13</invoice-line-period-start-date>
                <invoice-line-period-end-date id="bt-135">2018-04-13</invoice-line-period-end-date>
              </invoice-line-period>
              <invoice-line-allowances id="bg-27">
                <invoice-line-allowance id="bg-27">
                  <invoice-line-allowance-amount id="bt-136">20</invoice-line-allowance-amount>
                  <invoice-line-allowance-base-amount id="bt-137">200</invoice-line-allowance-base-amount>
                  <invoice-line-allowance-percentage id="bt-138">10</invoice-line-allowance-percentage>
                  <invoice-line-allowance-reason id="bt-139">Fixed long term</invoice-line-allowance-reason>
                  <invoice-line-allowance-reason-code id="bt-140">102</invoice-line-allowance-reason-code>
                </invoice-line-allowance>
                <invoice-line-allowance id="bg-27">
                  <invoice-line-allowance-amount id="bt-136">20</invoice-line-allowance-amount>
                  <invoice-line-allowance-base-amount id="bt-137">200</invoice-line-allowance-base-amount>
                  <invoice-line-allowance-percentage id="bt-138">10</invoice-line-allowance-percentage>
                  <invoice-line-allowance-reason id="bt-139">Fixed long term 2</invoice-line-allowance-reason>
                  <invoice-line-allowance-reason-code id="bt-140">102</invoice-line-allowance-reason-code>
                </invoice-line-allowance>
              </invoice-line-allowances>
              <invoice-line-charges id="bg-28">
                <invoice-line-charge id="bg-28">
                  <invoice-line-charge-amount id="bt-141">20</invoice-line-charge-amount>
                  <invoice-line-charge-base-amount id="bt-142">200</invoice-line-charge-base-amount>
                  <invoice-line-charge-percentage id="bt-143">10</invoice-line-charge-percentage>
                  <invoice-line-charge-reason id="bt-144">Testing</invoice-line-charge-reason>
                  <invoice-line-charge-reason-code id="bt-145">TAC</invoice-line-charge-reason-code>
                </invoice-line-charge>
                <invoice-line-charge id="bg-28">
                  <invoice-line-charge-amount id="bt-141">20</invoice-line-charge-amount>
                  <invoice-line-charge-base-amount id="bt-142">200</invoice-line-charge-base-amount>
                  <invoice-line-charge-percentage id="bt-143">10</invoice-line-charge-percentage>
                  <invoice-line-charge-reason id="bt-144">Testing 2</invoice-line-charge-reason>
                  <invoice-line-charge-reason-code id="bt-145">TAC</invoice-line-charge-reason-code>
                </invoice-line-charge>
              </invoice-line-charges>
              <price-details id="bg-29">
                <item-net-price id="bt-146">158.1250000000</item-net-price>
                <item-price-discount id="bt-147">10.000000000000000000</item-price-discount>
                <item-gross-price id="bt-148">168.1250000000</item-gross-price>
                <item-price-base-quantity id="bt-149">1</item-price-base-quantity>
                <item-price-base-quantity-unit-of-measure-code id="bt-150">XPP</item-price-base-quantity-unit-of-measure-code>
              </price-details>
              <line-vat-information id="bg-30">
                <invoiced-item-vat-category-code id="bt-151">S</invoiced-item-vat-category-code>
                <invoiced-item-vat-rate id="bt-152">19</invoiced-item-vat-rate>
              </line-vat-information>
              <item-information id="bg-31">
                <item-name id="bt-153">Beratung</item-name>
                <item-description id="bt-154">Anforderungsmanagement</item-description>
                <item-sellers-identifier id="bt-155">
                  <content>1034</content>
                </item-sellers-identifier>
                <item-buyers-identifier id="bt-156">
                  <content>5034</content>
                </item-buyers-identifier>
                <item-standard-identifier id="bt-157">
                  <content>123456789</content>
                  <scheme-identifier>0088</scheme-identifier>
                </item-standard-identifier>
                <item-classification-identifiers id="bt-158">
                  <item-classification-identifier id="bt-158">
                    <content>0721-880X</content>
                    <scheme-identifier>IB</scheme-identifier>
                    <scheme-version-identifier>88</scheme-version-identifier>
                  </item-classification-identifier>
                  <item-classification-identifier id="bt-158">
                    <content>0721-880XYZ</content>
                    <scheme-identifier>IB</scheme-identifier>
                    <scheme-version-identifier>88</scheme-version-identifier>
                  </item-classification-identifier>
                </item-classification-identifiers>
                <item-country-of-origin id="bt-159">DE</item-country-of-origin>
                <item-attributes id="bg-32">
                  <item-attribute id="bg-32">
                    <item-attribute-name id="bt-160">[Description]</item-attribute-name>
                    <item-attribute-value id="bt-161">[Value]</item-attribute-value>
                  </item-attribute>
                  <item-attribute id="bg-32">
                    <item-attribute-name id="bt-160">[Description 2]</item-attribute-name>
                    <item-attribute-value id="bt-161">[Value 2]</item-attribute-value>
                  </item-attribute>
                </item-attributes>
              </item-information>
            </invoice-line>
            """;

        InvoiceLine expected = new InvoiceLine
        {
            InvoiceLineIdentifier = new Identifier("1"),
            InvoiceLineNote = new Text("Die letzte Lieferung im Rahmen des abgerechneten Abonnements erfolgt in 12/2016 Lieferung erfolgt / erfolgte direkt vom Verlag"),
            InvoiceLineObjectIdentifier = new Identifier("ANG987654321", "ABZ"),
            InvoicedQuantity = new Quantity(30m),
            InvoicedQuantityUnitOfMeasureCode = new Code("XPP"),
            InvoiceLineNetAmount = new Amount(4743.75m),
            ReferencedPurchaseOrderLineReference = new DocumentReference("6171175.1"),
            InvoiceLineBuyerAccountingReference = new Text("Konto 1"),
            InvoiceLinePeriod = new InvoiceLinePeriod
            {
                InvoiceLinePeriodStartDate = new Date(new DateTime(2018, 04, 13)),
                InvoiceLinePeriodEndDate = new Date(new DateTime(2018, 04, 13)),
            },
            InvoiceLineAllowances = [
                new InvoiceLineAllowance {
                    InvoiceLineAllowanceAmount = new Amount(20m),
                    InvoiceLineAllowanceBaseAmount = new Amount(200m),
                    InvoiceLineAllowancePercentage = new Percentage(10m),
                    InvoiceLineAllowanceReason = new Text("Fixed long term"),
                    InvoiceLineAllowanceReasonCode = new Code("102"),
                },
                new InvoiceLineAllowance {
                    InvoiceLineAllowanceAmount = new Amount(20m),
                    InvoiceLineAllowanceBaseAmount = new Amount(200m),
                    InvoiceLineAllowancePercentage = new Percentage(10m),
                    InvoiceLineAllowanceReason = new Text("Fixed long term 2"),
                    InvoiceLineAllowanceReasonCode = new Code("102"),
                }
            ],
            InvoiceLineCharges = [
                new InvoiceLineCharge {
                    InvoiceLineChargeAmount = new Amount(20m),
                    InvoiceLineChargeBaseAmount = new Amount(200m),
                    InvoiceLineChargePercentage = new Percentage(10m),
                    InvoiceLineChargeReason = new Text("Testing"),
                    InvoiceLineChargeReasonCode = new Code("TAC"),
                },
                new InvoiceLineCharge {
                    InvoiceLineChargeAmount = new Amount(20m),
                    InvoiceLineChargeBaseAmount = new Amount(200m),
                    InvoiceLineChargePercentage = new Percentage(10m),
                    InvoiceLineChargeReason = new Text("Testing 2"),
                    InvoiceLineChargeReasonCode = new Code("TAC"),
                },
            ],
            PriceDetails = new PriceDetails
            {
                ItemNetPrice = new UnitPriceAmount(158.125m),
                ItemPriceDiscount = new UnitPriceAmount(10m),
                ItemGrossPrice = new UnitPriceAmount(168.125m),
                ItemPriceBaseQuantity = new Quantity(1m),
                ItemPriceBaseQuantityUnitOfMeasureCode = new Code("XPP"),
            },
            LineVatInformation = new LineVatInformation
            {
                InvoicedItemVatCategoryCode = new Code("S"),
                InvoicedItemVatRate = new Percentage(19m),
            },
            ItemInformation = new ItemInformation
            {
                ItemName = new Text("Beratung"),
                ItemDescription = new Text("Anforderungsmanagement"),
                ItemSellersIdentifier = new Identifier("1034"),
                ItemBuyersIdentifier = new Identifier("5034"),
                ItemStandardIdentifier = new Identifier("123456789", "0088"),
                ItemClassificationIdentifiers = [
                    new Identifier("0721-880X", "IB", "88"),
                    new Identifier("0721-880XYZ", "IB", "88"),
                ],
                ItemCountryOfOrigin = new Code("DE"),
                ItemAttributes = [
                    new ItemAttribute {
                       ItemAttributeName = new Text("[Description]"),
                       ItemAttributeValue = new Text ("[Value]"),
                    },
                    new ItemAttribute {
                        ItemAttributeName = new Text("[Description 2]"),
                        ItemAttributeValue = new Text("[Value 2]"),
                    },
                ],
            },
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = InvoiceLine.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeItemInformation()
    {
        string xml = """
            <item-information id="bg-31" xmlns="urn:todo">
              <item-name id="bt-153">Beratung</item-name>
              <item-description id="bt-154">Anforderungsmanagement</item-description>
              <item-sellers-identifier id="bt-155">
                <content>1034</content>
              </item-sellers-identifier>
              <item-buyers-identifier id="bt-156">
                <content>5034</content>
              </item-buyers-identifier>
              <item-standard-identifier id="bt-157">
                <content>123456789</content>
                <scheme-identifier>0088</scheme-identifier>
              </item-standard-identifier>
              <item-classification-identifiers id="bt-158">
                <item-classification-identifier id="bt-158">
                  <content>0721-880X</content>
                  <scheme-identifier>IB</scheme-identifier>
                  <scheme-version-identifier>88</scheme-version-identifier>
                </item-classification-identifier>
                <item-classification-identifier id="bt-158">
                  <content>0721-880XYZ</content>
                  <scheme-identifier>IB</scheme-identifier>
                  <scheme-version-identifier>88</scheme-version-identifier>
                </item-classification-identifier>
              </item-classification-identifiers>
              <item-country-of-origin id="bt-159">DE</item-country-of-origin>
              <item-attributes id="bg-32">
                <item-attribute id="bg-32">
                  <item-attribute-name id="bt-160">[Description]</item-attribute-name>
                  <item-attribute-value id="bt-161">[Value]</item-attribute-value>
                </item-attribute>
                <item-attribute id="bg-32">
                  <item-attribute-name id="bt-160">[Description 2]</item-attribute-name>
                  <item-attribute-value id="bt-161">[Value 2]</item-attribute-value>
                </item-attribute>
              </item-attributes>
            </item-information>
            """;

        ItemInformation expected = new ItemInformation
        {
            ItemName = new Text("Beratung"),
            ItemDescription = new Text("Anforderungsmanagement"),
            ItemSellersIdentifier = new Identifier("1034"),
            ItemBuyersIdentifier = new Identifier("5034"),
            ItemStandardIdentifier = new Identifier("123456789", "0088"),
            ItemClassificationIdentifiers = [
                new Identifier("0721-880X", "IB", "88"),
                new Identifier("0721-880XYZ", "IB", "88"),
            ],
            ItemCountryOfOrigin = new Code("DE"),
            ItemAttributes = [
                new ItemAttribute {
                    ItemAttributeName = new Text("[Description]"),
                    ItemAttributeValue = new Text ("[Value]"),
                 },
                 new ItemAttribute {
                     ItemAttributeName = new Text("[Description 2]"),
                     ItemAttributeValue = new Text("[Value 2]"),
                  },
            ],
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        var actual = ItemInformation.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DeserializeBinaryObject()
    {
        Blob content = new("hello world");

        string xml = $"""
            <binary-object xmlns="urn:todo">
                <content>{content.Base64Content}</content>
                <mime-code>text/utf-8</mime-code>
                <filename>hello.txt</filename>
            </binary-object>
            """;

        BinaryObject expected = new BinaryObject
        {
            Content = content.RawContent,
            MimeCode = "text/utf-8",
            Filename = "hello.txt",
        };

        using StringReader reader = new(xml);
        using XmlTextReader xmlReader = new(reader);

        xmlReader.ReadStartElement("binary-object");
        xmlReader.MoveToContent();

        var actual = BinaryObject.Deserialize(xmlReader);

        Assert.Equal(expected, actual);
    }
}
