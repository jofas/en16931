<xsl:stylesheet
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns="urn:todo"
    exclude-result-prefixes="cac cbc xsl"
    version="1.0">
  <xsl:template name="common-invoice-bt-1-2">
    <invoice-number id="bt-1">
      <content>
        <xsl:value-of select="cbc:ID"/>
      </content>
    </invoice-number>
    <invoice-issue-date id="bt-2">
      <xsl:value-of select="cbc:IssueDate"/>
    </invoice-issue-date>
  </xsl:template>

  <xsl:template name="common-invoice-bt-5-8">
    <invoice-currency-code id="bt-5">
      <xsl:value-of select="cbc:DocumentCurrencyCode"/>
    </invoice-currency-code>
    <xsl:if test="exists(cbc:TaxCurrencyCode)">
      <vat-accounting-currency-code id="bt-6">
        <xsl:value-of select="cbc:TaxCurrencyCode"/>
      </vat-accounting-currency-code>
    </xsl:if>
    <xsl:if test="exists(cbc:TaxPointDate)">
      <value-added-tax-point-date id="bt-7">
        <xsl:value-of select="cbc:TaxPointDate"/>
      </value-added-tax-point-date>
    </xsl:if>
    <xsl:if test="exists(cac:InvoicePeriod/cbc:DescriptionCode)">
      <value-added-tax-point-date-code id="bt-8">
        <xsl:value-of select="cac:InvoicePeriod/cbc:DescriptionCode"/>
      </value-added-tax-point-date-code>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-bt-10">
    <buyer-reference id="bt-10">
      <xsl:value-of select="cbc:BuyerReference"/>
    </buyer-reference>
  </xsl:template>

  <xsl:template name="common-invoice-bt-12-20">
    <xsl:if test="exists(cac:ContractDocumentReference/cbc:ID)">
      <contract-reference id="bt-12">
        <xsl:value-of select="cac:ContractDocumentReference/cbc:ID"/>
      </contract-reference>
    </xsl:if>
    <xsl:if test="exists(cac:OrderReference/cbc:ID)">
      <purchase-order-reference id="bt-13">
        <xsl:value-of select="cac:OrderReference/cbc:ID"/>
      </purchase-order-reference>
    </xsl:if>
    <xsl:if test="exists(cac:OrderReference/cbc:SalesOrderID)">
      <sales-order-reference id="bt-14">
        <xsl:value-of select="cac:OrderReference/cbc:SalesOrderID"/>
      </sales-order-reference>
    </xsl:if>
    <xsl:if test="exists(cac:ReceiptDocumentReference/cbc:ID)">
      <receiving-advice-reference id="bt-15">
        <xsl:value-of select="cac:ReceiptDocumentReference/cbc:ID"/>
      </receiving-advice-reference>
    </xsl:if>
    <xsl:if test="exists(cac:DespatchDocumentReference/cbc:ID)">
      <despatch-advice-reference id="bt-16">
        <xsl:value-of select="cac:DespatchDocumentReference/cbc:ID"/>
      </despatch-advice-reference>
    </xsl:if>
    <xsl:if test="exists(cac:OriginatorDocumentReference/cbc:ID)">
      <tender-or-lot-reference id="bt-17">
        <xsl:value-of select="cac:OriginatorDocumentReference/cbc:ID"/>
      </tender-or-lot-reference>
    </xsl:if>
    <xsl:if test="exists(cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID)">
      <invoiced-object-identifier id="bt-18">
        <content>
          <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID"/>
        </content>
        <xsl:if test="exists(cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID[@schemeID])">
          <scheme-identifier>
            <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID/@schemeID"/>
          </scheme-identifier>
        </xsl:if>
      </invoiced-object-identifier>
    </xsl:if>
    <xsl:if test="exists(cbc:AccountingCost)">
      <buyer-accounting-reference id="bt-19">
        <xsl:value-of select="cbc:AccountingCost"/>
      </buyer-accounting-reference>
    </xsl:if>
    <xsl:if test="exists(cac:PaymentTerms/cbc:Note)">
      <payment-terms id="bt-20">
        <xsl:value-of select="cac:PaymentTerms/cbc:Note"/>
      </payment-terms>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-bg-1-23">
    <xsl:variable name="root" select="."/>
    <xsl:if test="exists(cbc:Note)">
      <invoice-notes id="bg-1">
        <xsl:for-each select="cbc:Note">
          <invoice-note id="bg-1">
            <xsl:choose>
              <xsl:when test="contains(., '#') and string-length(substring-before(substring-after(., '#'), '#')) = 3">
                <invoice-note-subject-code id="bt-21">
                  <xsl:value-of select="substring-before(substring-after(., '#'), '#')"/>
                </invoice-note-subject-code>
                <invoice-note id="bt-22">
                  <xsl:value-of select="substring-after(substring-after(., '#'), '#')"/>
                </invoice-note>
              </xsl:when>
              <xsl:otherwise>
                <invoice-note id="bt-22">
                  <xsl:value-of select="."/>
                </invoice-note>
              </xsl:otherwise>
            </xsl:choose>
          </invoice-note>
        </xsl:for-each>
      </invoice-notes>
    </xsl:if>
    <process-control id="bg-2">
      <business-process-type id="bt-23">
        <xsl:value-of select="cbc:ProfileID"/>
      </business-process-type>
      <specification-identifier id="bt-24">
        <content>
          <xsl:value-of select="cbc:CustomizationID"/>
        </content>
      </specification-identifier>
    </process-control>
    <xsl:if test="cac:BillingReference">
      <preceding-invoice-references id="bg-3">
        <xsl:for-each select="cac:BillingReference">
          <preceding-invoice-reference id="bg-3">
            <preceding-invoice-reference id="bt-25">
              <xsl:value-of select="./cac:InvoiceDocumentReference/cbc:ID"/>
            </preceding-invoice-reference>
            <xsl:if test="exists(./cac:InvoiceDocumentReference/cbc:IssueDate)">
              <preceding-invoice-issue-date id="bt-26">
                <xsl:value-of select="./cac:InvoiceDocumentReference/cbc:IssueDate"/>
              </preceding-invoice-issue-date>
            </xsl:if>
          </preceding-invoice-reference>
        </xsl:for-each>
      </preceding-invoice-references>
    </xsl:if>
    <seller id="bg-4">
      <seller-name id="bt-27">
        <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName"/>
      </seller-name>
      <xsl:if test="cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name">
        <seller-trading-name id="bt-28">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name"/>
        </seller-trading-name>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[cbc:ID[not(normalize-space(upper-case(@schemeID)) = 'SEPA')]])">
        <seller-identifiers id="bt-29">
          <xsl:for-each select="cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[cbc:ID[not(normalize-space(upper-case(@schemeID)) = 'SEPA')]]">
            <seller-identifier id="bt-29">
              <content>
                <xsl:value-of select="./cbc:ID"/>
              </content>
              <xsl:if test="exists(./cbc:ID[@schemeID])">
                <scheme-identifier>
                  <xsl:value-of select="./cbc:ID/@schemeID"/>
                </scheme-identifier>
              </xsl:if>
            </seller-identifier>
          </xsl:for-each>
        </seller-identifiers>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID)">
        <seller-legal-registration-identifier id="bt-30">
          <content>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID"/>
          </content>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID[@schemeID])">
            <scheme-identifier>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID/@schemeID"/>
            </scheme-identifier>
          </xsl:if>
        </seller-legal-registration-identifier>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:CompanyID)">
        <seller-vat-identifier id="bt-31">
          <content>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:CompanyID"/>
          </content>
        </seller-vat-identifier>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/not(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID)">
        <seller-tax-registration-identifier id="bt-32">
          <content>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/not(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
          </content>
        </seller-tax-registration-identifier>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyLegalForm)">
        <seller-additional-legal-information id="bt-33">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyLegalForm"/>
        </seller-additional-legal-information>
      </xsl:if>
      <seller-electronic-address id="bt-34">
        <content>
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cbc:EndpointID"/>
        </content>
        <scheme-identifier>
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cbc:EndpointID/@schemeID"/>
        </scheme-identifier>
      </seller-electronic-address>
      <seller-postal-address id="bg-5">
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:StreetName)">
          <seller-address-line-1 id="bt-35">
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:StreetName"/>
          </seller-address-line-1>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName)">
          <seller-address-line-2 id="bt-36">
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName"/>
          </seller-address-line-2>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line)">
          <seller-address-line-3 id="bt-162">
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line"/>
          </seller-address-line-3>
        </xsl:if>
        <seller-city id="bt-37">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName"/>
        </seller-city>
        <seller-post-code id="bt-38">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone"/>
        </seller-post-code>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity)">
          <seller-country-subdivision id="bt-39">
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity"/>
          </seller-country-subdivision>
        </xsl:if>
        <seller-country-code id="bt-40">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
        </seller-country-code>
      </seller-postal-address>
      <seller-contact id="bg-6">
        <seller-contact-point id="bt-41">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:Name"/>
        </seller-contact-point>
        <seller-contact-telephone-number id="bt-42">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:Telephone"/>
        </seller-contact-telephone-number>
        <seller-contact-email-address id="bt-43">
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:ElectronicMail"/>
        </seller-contact-email-address>
      </seller-contact>
    </seller>
    <buyer id="bg-7">
      <buyer-name id="bt-44">
        <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName"/>
      </buyer-name>
      <xsl:if test="cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name">
        <buyer-trading-name id="bt-45">
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name"/>
        </buyer-trading-name>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID)">
        <buyer-identifier id="bt-46">
          <content>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID"/>
          </content>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID[@schemeID])">
            <scheme-identifier>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID/@schemeID"/>
            </scheme-identifier>
          </xsl:if>
        </buyer-identifier>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID)">
        <buyer-legal-registration-identifier id="bt-47">
          <content>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID"/>
          </content>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID[@schemeID])">
            <scheme-identifier>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID/@schemeID"/>
            </scheme-identifier>
          </xsl:if>
        </buyer-legal-registration-identifier>
      </xsl:if>
      <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID)">
        <buyer-vat-identifier id="bt-48">
          <content>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
          </content>
        </buyer-vat-identifier>
      </xsl:if>
      <buyer-electronic-address id="bt-49">
        <content>
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cbc:EndpointID"/>
        </content>
        <scheme-identifier>
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cbc:EndpointID/@schemeID"/>
        </scheme-identifier>
      </buyer-electronic-address>
      <buyer-postal-address id="bg-8">
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:StreetName)">
          <buyer-address-line-1 id="bt-50">
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:StreetName"/>
          </buyer-address-line-1>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName)">
          <buyer-address-line-2 id="bt-51">
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName"/>
          </buyer-address-line-2>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line)">
          <buyer-address-line-3 id="bt-163">
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line"/>
          </buyer-address-line-3>
        </xsl:if>
        <buyer-city id="bt-52">
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CityName"/>
        </buyer-city>
        <buyer-post-code id="bt-53">
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:PostalZone"/>
        </buyer-post-code>
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity)">
          <buyer-country-subdivision id="bt-54">
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity"/>
          </buyer-country-subdivision>
        </xsl:if>
        <buyer-country-code id="bt-55">
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
        </buyer-country-code>
      </buyer-postal-address>
      <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Name)
          or exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Telephone)
          or exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail)">
        <buyer-contact id="bg-9">
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Name)">
            <buyer-contact-point id="bt-56">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Name"/>
            </buyer-contact-point>
          </xsl:if>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Telephone)">
            <buyer-contact-telephone-number id="bt-57">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Telephone"/>
            </buyer-contact-telephone-number>
          </xsl:if>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail)">
            <buyer-contact-email-address id="bt-58">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail"/>
            </buyer-contact-email-address>
          </xsl:if>
        </buyer-contact>
      </xsl:if>
    </buyer>
    <xsl:if test="exists(cac:PayeeParty/cac:PartyName/cbc:Name)">
      <payee id="bg-10">
        <payee-name id="bt-59">
          <xsl:value-of select="cac:PayeeParty/cac:PartyName/cbc:Name"/>
        </payee-name>
        <xsl:if test="exists(cac:PayeeParty/cac:PartyIdentification/cbc:ID)">
          <payee-identifier id="bt-60">
            <content>
              <xsl:value-of select="cac:PayeeParty/cac:PartyIdentification/cbc:ID"/>
            </content>
            <xsl:if test="exists(cac:PayeeParty/cac:PartyIdentification/cbc:ID[@schemeID])">
              <scheme-identifier>
                <xsl:value-of select="cac:PayeeParty/cac:PartyIdentification/cbc:ID/@schemeID"/>
              </scheme-identifier>
            </xsl:if>
          </payee-identifier>
        </xsl:if>
        <xsl:if test="exists(cac:PayeeParty/cac:PartyLegalEntity/cbc:CompanyID)">
          <payee-legal-registration-identifier id="bt-61">
            <content>
              <xsl:value-of select="cac:PayeeParty/cac:PartyLegalEntity/cbc:CompanyID"/>
            </content>
            <xsl:if test="exists(cac:PayeeParty/cac:PartyLegalEntity/cbc:CompanyID[@schemeID])">
              <scheme-identifier>
                <xsl:value-of select="cac:PayeeParty/cac:PartyLegalEntity/cbc:CompanyID/@schemeID"/>
              </scheme-identifier>
            </xsl:if>
          </payee-legal-registration-identifier>
        </xsl:if>
      </payee>
    </xsl:if>
    <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PartyName/cbc:Name)">
      <seller-tax-representative-party id="bg-11">
        <seller-tax-representative-name id="bt-62">
          <xsl:value-of select="cac:TaxRepresentativeParty/cac:PartyName/cbc:Name"/>
        </seller-tax-representative-name>
        <seller-tax-representative-vat-identifier id="bt-63">
          <content>
            <xsl:value-of select="cac:TaxRepresentativeParty/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
          </content>
        </seller-tax-representative-vat-identifier>
        <seller-tax-representative-postal-address id="bg-12">
          <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:StreetName)">
            <tax-representative-address-line-1 id="bt-64">
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:StreetName"/>
            </tax-representative-address-line-1>
          </xsl:if>
          <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:AdditionalStreetName)">
            <tax-representative-address-line-2 id="bt-65">
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:AdditionalStreetName"/>
            </tax-representative-address-line-2>
          </xsl:if>
          <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cac:AddressLine/cbc:Line)">
            <tax-representative-address-line-3 id="bt-164">
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cac:AddressLine/cbc:Line"/>
            </tax-representative-address-line-3>
          </xsl:if>
          <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CityName)">
            <tax-representative-city id="bt-66">
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CityName"/>
            </tax-representative-city>
          </xsl:if>
          <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:PostalZone)">
            <tax-representative-post-code id="bt-67">
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:PostalZone"/>
            </tax-representative-post-code>
          </xsl:if>
          <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CountrySubentity)">
            <tax-representative-country-subdivision id="bt-68">
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CountrySubentity"/>
            </tax-representative-country-subdivision>
          </xsl:if>
          <tax-representative-country-code id="bt-69">
            <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
          </tax-representative-country-code>
        </seller-tax-representative-postal-address>
      </seller-tax-representative-party>
    </xsl:if>
    <xsl:if test="exists(cac:Delivery/cac:DeliveryParty/cac:PartyName/cbc:Name)
          or exists(cac:Delivery/cac:DeliveryLocation/cbc:ID)
          or exists(cac:Delivery/cbc:ActualDeliveryDate)
          or exists(cac:InvoicePeriod/cbc:StartDate)
          or exists(cac:InvoicePeriod/cbc:EndDate)
          or exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CityName)">
      <delivery-information id="bg-13">
        <xsl:if test="exists(cac:Delivery/cac:DeliveryParty/cac:PartyName/cbc:Name)">
          <deliver-to-party-name id="bt-70">
            <xsl:value-of select="cac:Delivery/cac:DeliveryParty/cac:PartyName/cbc:Name"/>
          </deliver-to-party-name>
        </xsl:if>
        <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cbc:ID)">
          <deliver-to-location-identifier id="bt-71">
            <content>
              <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cbc:ID"/>
            </content>
            <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cbc:ID[@schemeID])">
              <scheme-identifier>
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cbc:ID/@schemeID"/>
              </scheme-identifier>
            </xsl:if>
          </deliver-to-location-identifier>
        </xsl:if>
        <xsl:if test="exists(cac:Delivery/cbc:ActualDeliveryDate)">
          <actual-delivery-date id="bt-72">
            <xsl:value-of select="cac:Delivery/cbc:ActualDeliveryDate"/>
          </actual-delivery-date>
        </xsl:if>
        <xsl:if test="exists(cac:InvoicePeriod/cbc:StartDate) or exists(cac:InvoicePeriod/cbc:EndDate)">
          <invoicing-period id="bg-14">
            <xsl:if test="exists(cac:InvoicePeriod/cbc:StartDate)">
              <invoicing-period-start-date id="bt-73">
                <xsl:value-of select="cac:InvoicePeriod/cbc:StartDate"/>
              </invoicing-period-start-date>
            </xsl:if>
            <xsl:if test="exists(cac:InvoicePeriod/cbc:EndDate)">
              <invoicing-period-end-date id="bt-74">
                <xsl:value-of select="cac:InvoicePeriod/cbc:EndDate"/>
              </invoicing-period-end-date>
            </xsl:if>
          </invoicing-period>
        </xsl:if>
        <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CityName)">
          <deliver-to-address id="bg-15">
            <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:StreetName)">
              <deliver-to-address-line-1 id="bt-75">
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:StreetName"/>
              </deliver-to-address-line-1>
            </xsl:if>
            <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:AdditionalStreetName)">
              <deliver-to-address-line-2 id="bt-76">
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:AdditionalStreetName"/>
              </deliver-to-address-line-2>
            </xsl:if>
            <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cac:AddressLine/cbc:Line)">
              <deliver-to-address-line-3 id="bt-165">
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cac:AddressLine/cbc:Line"/>
              </deliver-to-address-line-3>
            </xsl:if>
            <deliver-to-city id="bt-77">
              <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CityName"/>
            </deliver-to-city>
            <deliver-to-post-code id="bt-78">
              <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:PostalZone"/>
            </deliver-to-post-code>
            <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CountrySubentity)">
              <deliver-to-country-subdivision id="bt-79">
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CountrySubentity"/>
              </deliver-to-country-subdivision>
            </xsl:if>
            <deliver-to-country-code id="bt-80">
              <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cac:Country/cbc:IdentificationCode"/>
            </deliver-to-country-code>
          </deliver-to-address>
        </xsl:if>
      </delivery-information>
    </xsl:if>
    <payment-instructions id="bg-16">
      <payment-means-type-code id="bt-81">
        <xsl:value-of select="cac:PaymentMeans/cbc:PaymentMeansCode"/>
      </payment-means-type-code>
      <xsl:if test="exists(cac:PaymentMeans/cbc:PaymentMeansCode[@name])">
        <payment-means-text id="bt-82">
          <xsl:value-of select="cac:PaymentMeans/cbc:PaymentMeansCode/@name"/>
        </payment-means-text>
      </xsl:if>
      <xsl:if test="exists(cac:PaymentMeans/cbc:PaymentID)">
        <remittance-information id="bt-83">
          <xsl:value-of select="cac:PaymentMeans/cbc:PaymentID"/>
        </remittance-information>
      </xsl:if>
      <xsl:if test="exists(cac:PaymentMeans/cac:PayeeFinancialAccount)">
        <credit-transfers id="bg-17">
          <!-- Note that UBL does not actually support multiple bg-17 instances -->
          <xsl:for-each select="cac:PaymentMeans/cac:PayeeFinancialAccount">
            <credit-transfer id="bg-17">
              <payment-account-identifier id="bt-84">
                <content>
                  <xsl:value-of select="./cbc:ID"/>
                </content>
              </payment-account-identifier>
              <xsl:if test="exists(./cbc:Name)">
                <payment-account-name id="bt-85">
                  <xsl:value-of select="./cbc:Name"/>
                </payment-account-name>
              </xsl:if>
              <xsl:if test="exists(./cac:FinancialInstitutionBranch/cbc:ID)">
                <payment-service-provider-identifier id="bt-86">
                  <content>
                    <xsl:value-of select="./cac:FinancialInstitutionBranch/cbc:ID"/>
                  </content>
                </payment-service-provider-identifier>
              </xsl:if>
            </credit-transfer>
          </xsl:for-each>
        </credit-transfers>
      </xsl:if>
      <xsl:if test="exists(cac:PaymentMeans/cac:CardAccount/cbc:PrimaryAccountNumberID)">
        <payment-card-information id="bg-18">
          <payment-card-primary-account-number id="bt-87">
            <xsl:value-of select="cac:PaymentMeans/cac:CardAccount/cbc:PrimaryAccountNumberID"/>
          </payment-card-primary-account-number>
          <xsl:if test="exists(cac:PaymentMeans/cac:CardAccount/cbc:HolderName)">
            <payment-card-holder-name id="bt-88">
              <xsl:value-of select="cac:PaymentMeans/cac:CardAccount/cbc:HolderName"/>
            </payment-card-holder-name>
          </xsl:if>
        </payment-card-information>
      </xsl:if>
      <xsl:if test="exists(cac:PaymentMeans/cac:PaymentMandate/cbc:ID)">
        <direct-debit id="bg-19">
          <mandate-reference-identifier id="bt-89">
            <content>
              <xsl:value-of select="cac:PaymentMeans/cac:PaymentMandate/cbc:ID"/>
            </content>
          </mandate-reference-identifier>
          <bank-assigned-creditor-identifier id="bt-90">
            <content>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification/cbc:ID[normalize-space(upper-case(@schemeID)) = 'SEPA']"/>
            </content>
          </bank-assigned-creditor-identifier>
          <debited-account-identifier id="bt-91">
            <content>
              <xsl:value-of select="cac:PaymentMeans/cac:PaymentMandate/cac:PayerFinancialAccount/cbc:ID"/>
            </content>
          </debited-account-identifier>
        </direct-debit>
      </xsl:if>
    </payment-instructions>
    <xsl:if test="exists(cac:AllowanceCharge[cbc:ChargeIndicator = 'false']/cbc:Amount)">
      <document-level-allowances id="bg-20">
        <xsl:for-each select="cac:AllowanceCharge[cbc:ChargeIndicator = 'false']">
          <document-level-allowance id="bg-20">
            <document-level-allowance-amount id="bt-92">
              <xsl:value-of select="./cbc:Amount"/>
            </document-level-allowance-amount>
            <xsl:if test="exists(./cbc:BaseAmount)">
              <document-level-allowance-base-amount id="bt-93">
                <xsl:value-of select="./cbc:BaseAmount"/>
              </document-level-allowance-base-amount>
            </xsl:if>
            <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
              <document-level-allowance-percentage id="bt-94">
                <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
              </document-level-allowance-percentage>
            </xsl:if>
            <document-level-allowance-vat-category-code id="bt-95">
              <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:ID"/>
            </document-level-allowance-vat-category-code>
            <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:Percent)">
              <document-level-allowance-vat-rate id="bt-96">
                <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:Percent"/>
              </document-level-allowance-vat-rate>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReason)">
              <document-level-allowance-reason id="bt-97">
                <xsl:value-of select="./cbc:AllowanceChargeReason"/>
              </document-level-allowance-reason>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
              <document-level-allowance-reason-code id="bt-98">
                <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
              </document-level-allowance-reason-code>
            </xsl:if>
          </document-level-allowance>
        </xsl:for-each>
      </document-level-allowances>
    </xsl:if>
    <xsl:if test="exists(cac:AllowanceCharge[cbc:ChargeIndicator = 'true']/cbc:Amount)">
      <document-level-charges id="bg-21">
        <xsl:for-each select="cac:AllowanceCharge[cbc:ChargeIndicator = 'true']">
          <document-level-charge id="bg-21">
            <document-level-charge-amount id="bt-99">
              <xsl:value-of select="./cbc:Amount"/>
            </document-level-charge-amount>
            <xsl:if test="exists(./cbc:BaseAmount)">
              <document-level-charge-base-amount id="bt-100">
                <xsl:value-of select="./cbc:BaseAmount"/>
              </document-level-charge-base-amount>
            </xsl:if>
            <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
              <document-level-charge-percentage id="bt-101">
                <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
              </document-level-charge-percentage>
            </xsl:if>
            <document-level-charge-vat-category-code id="bt-102">
              <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:ID"/>
            </document-level-charge-vat-category-code>
            <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:Percent)">
              <document-level-charge-vat-rate id="bt-103">
                <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID)) = 'VAT']/cbc:Percent"/>
              </document-level-charge-vat-rate>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReason)">
              <document-level-charge-reason id="bt-104">
                <xsl:value-of select="./cbc:AllowanceChargeReason"/>
              </document-level-charge-reason>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
              <document-level-charge-reason-code id="bt-105">
                <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
              </document-level-charge-reason-code>
            </xsl:if>
          </document-level-charge>
        </xsl:for-each>
      </document-level-charges>
    </xsl:if>
    <document-totals id="bg-22">
      <sum-of-invoice-line-net-amount id="bt-106">
        <xsl:value-of select="cac:LegalMonetaryTotal/cbc:LineExtensionAmount"/>
      </sum-of-invoice-line-net-amount>
      <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount)">
        <sum-of-allowances-on-document-level id="bt-107">
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount"/>
        </sum-of-allowances-on-document-level>
      </xsl:if>
      <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:ChargeTotalAmount)">
        <sum-of-charges-on-document-level id="bt-108">
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:ChargeTotalAmount"/>
        </sum-of-charges-on-document-level>
      </xsl:if>
      <invoice-total-amount-without-vat id="bt-109">
        <xsl:value-of select="cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount"/>
      </invoice-total-amount-without-vat>
      <xsl:if test="exists(cac:TaxTotal/cbc:TaxAmount[@currencyID=$root/cbc:DocumentCurrencyCode])">
        <invoice-total-vat-amount id="bt-110">
            <xsl:value-of select="cac:TaxTotal/cbc:TaxAmount[@currencyID=$root/cbc:DocumentCurrencyCode]"/>
        </invoice-total-vat-amount>
      </xsl:if>
      <xsl:if test="exists(cac:TaxTotal/cbc:TaxAmount[@currencyID=$root/cbc:TaxCurrencyCode])">
        <invoice-total-vat-amount-in-accounting-currency id="bt-111">
          <xsl:value-of select="cac:TaxTotal/cbc:TaxAmount[@currencyID=$root/cbc:TaxCurrencyCode]"/>
        </invoice-total-vat-amount-in-accounting-currency>
      </xsl:if>
      <invoice-total-amount-with-vat id="bt-112">
        <xsl:value-of select="cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount"/>
      </invoice-total-amount-with-vat>
      <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:PrepaidAmount)">
        <paid-amount id="bt-113">
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:PrepaidAmount"/>
        </paid-amount>
      </xsl:if>
      <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:PayableRoundingAmount)">
        <rounding-amount id="bt-114">
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:PayableRoundingAmount"/>
        </rounding-amount>
      </xsl:if>
      <amount-due-for-payment id="bt-115">
        <xsl:value-of select="cac:LegalMonetaryTotal/cbc:PayableAmount"/>
      </amount-due-for-payment>
    </document-totals>
    <vat-breakdown id="bg-23">
      <xsl:for-each select="cac:TaxTotal/cac:TaxSubtotal">
        <vat-breakdown id="bg-23">
          <vat-category-taxable-amount id="bt-116">
            <xsl:value-of select="./cbc:TaxableAmount"/>
          </vat-category-taxable-amount>
          <vat-category-tax-amount id="bt-117">
            <xsl:value-of select="./cbc:TaxAmount"/>
          </vat-category-tax-amount>
          <vat-category-code id="bt-118">
            <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:ID"/>
          </vat-category-code>
          <vat-category-rate id="bt-119">
            <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:Percent"/>
          </vat-category-rate>
          <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReason)">
            <vat-exemption-reason-text id="bt-120">
              <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReason"/>
            </vat-exemption-reason-text>
          </xsl:if>
          <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReasonCode)">
            <vat-exemption-reason-code id="bt-121">
              <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReasonCode"/>
            </vat-exemption-reason-code>
          </xsl:if>
        </vat-breakdown>
      </xsl:for-each>
    </vat-breakdown>
  </xsl:template>

  <xsl:template name="common-additional-supporting-document">
    <additional-supporting-document id="bg-24">
      <supporting-document-reference id="bt-122">
        <xsl:value-of select="./cbc:ID"/>
      </supporting-document-reference>
      <xsl:if test="exists(./cbc:DocumentDescription)">
        <supporting-document-description id="bt-123">
          <xsl:value-of select="./cbc:DocumentDescription"/>
        </supporting-document-description>
      </xsl:if>
      <xsl:if test="exists(./cac:Attachment/cac:ExternalReference/cbc:URI)">
        <external-document-location id="bt-124">
          <xsl:value-of select="./cac:Attachment/cac:ExternalReference/cbc:URI"/>
        </external-document-location>
      </xsl:if>
      <xsl:if test="exists(./cac:Attachment/cbc:EmbeddedDocumentBinaryObject)">
        <attached-document id="bt-125">
          <content>
            <xsl:value-of select="./cac:Attachment/cbc:EmbeddedDocumentBinaryObject"/>
          </content>
          <mime-code>
            <xsl:value-of select="./cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode"/>
          </mime-code>
          <filename>
            <xsl:value-of select="./cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@filename"/>
          </filename>
        </attached-document>
      </xsl:if>
    </additional-supporting-document>
  </xsl:template>

  <xsl:template name="common-invoice-line-bt-126-128">
    <invoice-line-identifier id="bt-126">
      <content>
        <xsl:value-of select="./cbc:ID"/>
      </content>
    </invoice-line-identifier>
    <xsl:if test="exists(./cbc:Note)">
      <invoice-line-note id="bt-127">
        <xsl:value-of select="./cbc:Note"/>
      </invoice-line-note>
    </xsl:if>
    <xsl:if test="exists(./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID)">
      <invoice-line-object-identifier id="bt-128">
        <content>
          <xsl:value-of select="./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID"/>
        </content>
        <xsl:if test="exists(./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID[@schemeID])">
          <scheme-identifier>
            <xsl:value-of select="./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID/@schemeID"/>
          </scheme-identifier>
        </xsl:if>
      </invoice-line-object-identifier>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-line-bt-131-133">
    <invoice-line-net-amount id="bt-131">
      <xsl:value-of select="./cbc:LineExtensionAmount"/>
    </invoice-line-net-amount>
    <xsl:if test="exists(./cac:OrderLineReference/cbc:LineID)">
      <referenced-purchase-order-line-reference id="bt-132">
        <xsl:value-of select="./cac:OrderLineReference/cbc:LineID"/>
      </referenced-purchase-order-line-reference>
    </xsl:if>
    <xsl:if test="exists(./cbc:AccountingCost)">
      <invoice-line-buyer-accounting-reference id="bt-133">
        <xsl:value-of select="./cbc:AccountingCost"/>
      </invoice-line-buyer-accounting-reference>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-line-bg-26-31">
    <xsl:if test="exists(./cac:InvoicePeriod/cbc:StartDate) or exists(./cac:InvoicePeriod/cbc:EndDate)">
      <invoice-line-period id="bg-26">
        <xsl:if test="exists(./cac:InvoicePeriod/cbc:StartDate)">
          <invoice-line-period-start-date id="bt-134">
            <xsl:value-of select="./cac:InvoicePeriod/cbc:StartDate"/>
          </invoice-line-period-start-date>
        </xsl:if>
        <xsl:if test="exists(./cac:InvoicePeriod/cbc:EndDate)">
          <invoice-line-period-end-date id="bt-135">
            <xsl:value-of select="./cac:InvoicePeriod/cbc:EndDate"/>
          </invoice-line-period-end-date>
        </xsl:if>
      </invoice-line-period>
    </xsl:if>
    <xsl:if test="exists(./cac:AllowanceCharge[cbc:ChargeIndicator = 'false']/cbc:Amount)">
      <invoice-line-allowances id="bg-27">
        <xsl:for-each select="./cac:AllowanceCharge[cbc:ChargeIndicator = 'false']">
          <invoice-line-allowance id="bg-27">
            <invoice-line-allowance-amount id="bt-136">
              <xsl:value-of select="./cbc:Amount"/>
            </invoice-line-allowance-amount>
            <xsl:if test="exists(./cbc:BaseAmount)">
              <invoice-line-allowance-base-amount id="bt-137">
                <xsl:value-of select="./cbc:BaseAmount"/>
              </invoice-line-allowance-base-amount>
            </xsl:if>
            <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
              <invoice-line-allowance-percentage id="bt-138">
                <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
              </invoice-line-allowance-percentage>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReason)">
              <invoice-line-allowance-reason id="bt-139">
                <xsl:value-of select="./cbc:AllowanceChargeReason"/>
              </invoice-line-allowance-reason>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
              <invoice-line-allowance-reason-code id="bt-140">
                <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
              </invoice-line-allowance-reason-code>
            </xsl:if>
          </invoice-line-allowance>
        </xsl:for-each>
      </invoice-line-allowances>
    </xsl:if>
    <xsl:if test="exists(./cac:AllowanceCharge[cbc:ChargeIndicator = 'true']/cbc:Amount)">
      <invoice-line-charges id="bg-28">
        <xsl:for-each select="./cac:AllowanceCharge[cbc:ChargeIndicator = 'true']">
          <invoice-line-charge id="bg-28">
            <invoice-line-charge-amount id="bt-141">
              <xsl:value-of select="./cbc:Amount"/>
            </invoice-line-charge-amount>
            <xsl:if test="exists(./cbc:BaseAmount)">
              <invoice-line-charge-base-amount id="bt-142">
                <xsl:value-of select="./cbc:BaseAmount"/>
              </invoice-line-charge-base-amount>
            </xsl:if>
            <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
              <invoice-line-charge-percentage id="bt-143">
                <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
              </invoice-line-charge-percentage>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReason)">
              <invoice-line-charge-reason id="bt-144">
                <xsl:value-of select="./cbc:AllowanceChargeReason"/>
              </invoice-line-charge-reason>
            </xsl:if>
            <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
              <invoice-line-charge-reason-code id="bt-145">
                <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
              </invoice-line-charge-reason-code>
            </xsl:if>
          </invoice-line-charge>
        </xsl:for-each>
      </invoice-line-charges>
    </xsl:if>
    <price-details id="bg-29">
      <item-net-price id="bt-146">
        <xsl:value-of select="./cac:Price/cbc:PriceAmount"/>
      </item-net-price>
      <xsl:if test="exists(./cac:Price/cac:AllowanceCharge/cbc:Amount)">
        <item-price-discount id="bt-147">
          <xsl:value-of select="./cac:Price/cac:AllowanceCharge/cbc:Amount"/>
        </item-price-discount>
      </xsl:if>
      <xsl:if test="exists(./cac:Price/cac:AllowanceCharge/cbc:BaseAmount)">
        <item-gross-price id="bt-148">
          <xsl:value-of select="./cac:Price/cac:AllowanceCharge/cbc:BaseAmount"/>
        </item-gross-price>
      </xsl:if>
      <xsl:if test="exists(./cac:Price/cbc:BaseQuantity)">
        <item-price-base-quantity id="bt-149">
          <xsl:value-of select="./cac:Price/cbc:BaseQuantity"/>
        </item-price-base-quantity>
      </xsl:if>
      <xsl:if test="exists(./cac:Price/cbc:BaseQuantity[@unitCode])">
        <item-price-base-quantity-unit-of-measure-code id="bt-150">
          <xsl:value-of select="./cac:Price/cbc:BaseQuantity/@unitCode"/>
        </item-price-base-quantity-unit-of-measure-code>
      </xsl:if>
    </price-details>
    <line-vat-information id="bg-30">
      <invoiced-item-vat-category-code id="bt-151">
        <xsl:value-of select="./cac:Item/cac:ClassifiedTaxCategory[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:ID"/>
      </invoiced-item-vat-category-code>
      <xsl:if test="exists(./cac:Item/cac:ClassifiedTaxCategory[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:Percent)">
        <invoiced-item-vat-rate id="bt-152">
          <xsl:value-of select="./cac:Item/cac:ClassifiedTaxCategory[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:Percent"/>
        </invoiced-item-vat-rate>
      </xsl:if>
    </line-vat-information>
    <item-information id="bg-31">
      <item-name id="bt-153">
        <xsl:value-of select="./cac:Item/cbc:Name"/>
      </item-name>
      <xsl:if test="exists(./cac:Item/cbc:Description)">
        <item-description id="bt-154">
          <xsl:value-of select="./cac:Item/cbc:Description"/>
        </item-description>
      </xsl:if>
      <xsl:if test="exists(./cac:Item/cac:SellersItemIdentification/cbc:ID)">
        <item-sellers-identifier id="bt-155">
          <content>
            <xsl:value-of select="./cac:Item/cac:SellersItemIdentification/cbc:ID"/>
          </content>
        </item-sellers-identifier>
      </xsl:if>
      <xsl:if test="exists(./cac:Item/cac:BuyersItemIdentification/cbc:ID)">
        <item-buyers-identifier id="bt-156">
          <content>
            <xsl:value-of select="./cac:Item/cac:BuyersItemIdentification/cbc:ID"/>
          </content>
        </item-buyers-identifier>
      </xsl:if>
      <xsl:if test="exists(./cac:Item/cac:StandardItemIdentification/cbc:ID)">
        <item-standard-identifier id="bt-157">
          <content>
            <xsl:value-of select="./cac:Item/cac:StandardItemIdentification/cbc:ID"/>
          </content>
          <scheme-identifier>
            <xsl:value-of select="./cac:Item/cac:StandardItemIdentification/cbc:ID/@schemeID"/>
          </scheme-identifier>
        </item-standard-identifier>
      </xsl:if>
      <xsl:if test="exists(./cac:Item/cac:CommoditiyClassification/cbc:ItemClassificationCode)">
        <item-classification-identifiers id="bt-158">
          <xsl:for-each select="./cac:Item/cac:CommoditiyClassification">
            <item-classification-identifier id="bt-158">
              <content>
                <xsl:value-of select="./cbc:ItemClassificationCode"/>
              </content>
              <scheme-identifier>
                <xsl:value-of select="./cbc:ItemClassificationCode/@listID"/>
              </scheme-identifier>
              <xsl:if test="exists(./cbc:ItemClassificationCode[@listVersionID])">
                <scheme-version-identifier>
                  <xsl:value-of select="./cbc:ItemClassificationCode/@listVersionID"/>
                </scheme-version-identifier>
              </xsl:if>
            </item-classification-identifier>
          </xsl:for-each>
        </item-classification-identifiers>
      </xsl:if>
      <xsl:if test="exists(./cac:Item/cac:OriginCountry/cbc:IdentificationCode)">
        <item-country-of-origin id="bt-159">
          <xsl:value-of select="./cac:Item/cac:OriginCountry/cbc:IdentificationCode"/>
        </item-country-of-origin>
      </xsl:if>
      <xsl:if test="exists(./cac:Item/cac:AdditionalItemProperty/cbc:Name)">
        <item-attributes id="bg-32">
          <xsl:for-each select="./cac:Item/cac:AdditionalItemProperty">
            <item-attribute id="bg-32">
              <item-attribute-name id="bt-160">
                <xsl:value-of select="./cbc:Name"/>
              </item-attribute-name>
              <item-attribute-value id="bt-161">
                <xsl:value-of select="./cbc:Value"/>
              </item-attribute-value>
            </item-attribute>
          </xsl:for-each>
        </item-attributes>
      </xsl:if>
    </item-information>
  </xsl:template>
</xsl:stylesheet>
