<xsl:stylesheet
    xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    exclude-result-prefixes="ubl cac cbc xsl"
    version="1.0">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="ubl:Invoice">
    <invoice xmlns="urn:todo">
      <invoice-number id="bt-1">
        <content>
          <xsl:value-of select="cbc:ID"/>
        </content>
      </invoice-number>
      <invoice-issue-date id="bt-2">
        <xsl:value-of select="cbc:IssueDate"/>
      </invoice-issue-date>
      <invoice-type-code id="bt-3">
        <xsl:value-of select="cbc:InvoiceTypeCode"/>
      </invoice-type-code>
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
      <xsl:if test="exists(cbc:DueDate)">
        <payment-due-date id="bt-9">
          <xsl:value-of select="cbc:DueDate"/>
        </payment-due-date>
      </xsl:if>
      <buyer-reference id="bt-10">
        <xsl:value-of select="cbc:BuyerReference"/>
      </buyer-reference>
      <xsl:if test="exists(cac:ProjectReference/cbc:ID)">
        <project-reference id="bt-11">
          <!-- UBL CreditNote: cac:AdditionalDocumentReference[cbc:DocumentTypeCode='50']/cbc:ID -->
          <xsl:value-of select="cac:ProjectReference/cbc:ID"/>
        </project-reference>
      </xsl:if>
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
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[(normalize-space(upper-case(@schmeID)) != 'SEPA')])">
          <seller-identifiers id="bt-29">
            <xsl:for-each select="cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[(normalize-space(upper-case(@schmeID)) != 'SEPA')]">
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
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID)">
          <seller-vat-identifier id="bt-31">
            <content>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
            </content>
          </seller-vat-identifier>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) != 'VAT')]/cbc:CompanyID)">
          <seller-tax-registration-identifier id="bt-32">
            <content>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) != 'VAT')]/cbc:CompanyID"/>
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
      <buyer id="bt-7">
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
        <xsl:element name="payee">
          <xsl:attribute name="id">bg-10</xsl:attribute>
          <xsl:element name="payee-name">
            <xsl:attribute name="id">bt-59</xsl:attribute>
            <xsl:value-of select="cac:PayeeParty/cac:PartyName/cbc:Name"/>
          </xsl:element>
          <xsl:if test="exists(cac:PayeeParty/cac:PartyIdentification/cbc:ID)">
            <xsl:element name="payee-identifier">
              <xsl:attribute name="id">bt-60</xsl:attribute>
              <xsl:element name="content">
                <xsl:value-of select="cac:PayeeParty/cac:PartyIdentification/cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(cac:PayeeParty/cac:PartyIdentification/cbc:ID[@schemeID])">
                <xsl:element name="scheme-identifier">
                  <xsl:value-of select="cac:PayeeParty/cac:PartyIdentification/cbc:ID/@schemeID"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:PayeeParty/cac:PartyIdentification/cbc:CompanyID)">
            <xsl:element name="payee-legal-registration-identifier">
              <xsl:attribute name="id">bt-61</xsl:attribute>
              <xsl:element name="content">
                <xsl:value-of select="cac:PayeeParty/cac:PartyIdentification/cbc:CompanyID"/>
              </xsl:element>
              <xsl:if test="exists(cac:PayeeParty/cac:PartyIdentification/cbc:CompanyID[@schemeID])">
                <xsl:element name="scheme-identifier">
                  <xsl:value-of select="cac:PayeeParty/cac:PartyIdentification/cbc:CompanyID/@schemeID"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:if>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PartyName/cbc:Name)">
        <xsl:element name="seller-tax-representative-party">
          <xsl:attribute name="id">bg-11</xsl:attribute>
          <xsl:element name="seller-tax-representative-name">
            <xsl:attribute name="id">bt-62</xsl:attribute>
            <xsl:value-of select="cac:TaxRepresentativeParty/cac:PartyName/cbc:Name"/>
          </xsl:element>
          <xsl:element name="seller-tax-representative-vat-identifier">
            <xsl:attribute name="id">bt-63</xsl:attribute>
            <xsl:value-of select="cac:TaxRepresentativeParty/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
          </xsl:element>
          <xsl:element name="seller-tax-representative-postal-address">
            <xsl:attribute name="id">bg-12</xsl:attribute>
            <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:StreetName)">
              <xsl:element name="tax-representative-address-line-1">
                <xsl:attribute name="id">bt-64</xsl:attribute>
                <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:StreetName"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:AdditionalStreetName)">
              <xsl:element name="tax-representative-address-line-2">
                <xsl:attribute name="id">bt-65</xsl:attribute>
                <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:AdditionalStreetName"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cac:AddressLine/cbc:Line)">
              <xsl:element name="tax-representative-address-line-3">
                <xsl:attribute name="id">bt-164</xsl:attribute>
                <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cac:AddressLine/cbc:Line"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CityName)">
              <xsl:element name="tax-representative-city">
                <xsl:attribute name="id">bt-66</xsl:attribute>
                <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CityName"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:PostalZone)">
              <xsl:element name="tax-representative-post-code">
                <xsl:attribute name="id">bt-67</xsl:attribute>
                <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:PostalZone"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="exists(cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CountrySubentity)">
              <xsl:element name="tax-representative-address-line-3">
                <xsl:attribute name="id">bt-68</xsl:attribute>
                <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cbc:CountrySubentity"/>
              </xsl:element>
            </xsl:if>
            <xsl:element name="tax-representative-post-code">
              <xsl:attribute name="id">bt-69</xsl:attribute>
              <xsl:value-of select="cac:TaxRepresentativeParty/cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
            </xsl:element>
          </xsl:element>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:Delivery/cac:DeliveryParty/cac:PartyName/cbc:Name)
            or exists(cac:Delivery/cac:DeliveryLocation/cbc:ID)
            or exists(cac:Delivery/cbc:ActualDeliveryDate)
            or exists(cac:InvoicePeriod/cbc:StartDate)
            or exists(cac:InvoicePeriod/cbc:EndDate)
            or exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CityName)">
        <xsl:element name="delivery-information">
          <xsl:attribute name="id">bg-13</xsl:attribute>
          <xsl:if test="exists(cac:Delivery/cac:DeliveryParty/cac:PartyName/cbc:Name)">
            <xsl:element name="deliver-to-party-name">
              <xsl:attribute name="id">bt-70</xsl:attribute>
              <xsl:value-of select="cac:Delivery/cac:DeliveryParty/cac:PartyName/cbc:Name"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cbc:ID)">
            <xsl:element name="deliver-to-location-identifier">
              <xsl:attribute name="id">bt-71</xsl:attribute>
              <xsl:element name="content">
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cbc:ID[@schemeID])">
                <xsl:element name="scheme-identifier">
                  <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cbc:ID/@schemeID"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:Delivery/cbc:ActualDeliveryDate)">
            <xsl:element name="actual-delivery-date">
              <xsl:attribute name="id">bt-72</xsl:attribute>
              <xsl:value-of select="cac:Delivery/cbc:ActualDeliveryDate"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:InvoicePeriod/cbc:StartDate) or exists(cac:InvoicePeriod/cbc:EndDate)">
            <xsl:element name="invoicing-period">
              <xsl:attribute name="id">bg-14</xsl:attribute>
              <xsl:if test="exists(cac:InvoicePeriod/cbc:StartDate)">
                <xsl:element name="invoicing-period-start-date">
                  <xsl:attribute name="id">bt-73</xsl:attribute>
                  <xsl:value-of select="cac:InvoicePeriod/cbc:StartDate"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(cac:InvoicePeriod/cbc:EndDate)">
                <xsl:element name="invoicing-period-end-date">
                  <xsl:attribute name="id">bt-74</xsl:attribute>
                  <xsl:value-of select="cac:InvoicePeriod/cbc:EndDate"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CityName)">
            <xsl:element name="deliver-to-address">
              <xsl:attribute name="id">bg-15</xsl:attribute>
              <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:StreetName)">
                <xsl:element name="deliver-to-address-line-1">
                  <xsl:attribute name="id">bt-75</xsl:attribute>
                  <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:StreetName"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:AdditionalStreetName)">
                <xsl:element name="deliver-to-address-line-2">
                  <xsl:attribute name="id">bt-76</xsl:attribute>
                  <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:AdditionalStreetName"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cac:AddressLine/cbc:Line)">
                <xsl:element name="deliver-to-address-line-3">
                  <xsl:attribute name="id">bt-165</xsl:attribute>
                  <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cac:AddressLine/cbc:Line"/>
                </xsl:element>
              </xsl:if>
              <xsl:element name="deliver-to-city">
                <xsl:attribute name="id">bt-77</xsl:attribute>
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CityName"/>
              </xsl:element>
              <xsl:element name="deliver-to-post-code">
                <xsl:attribute name="id">bt-78</xsl:attribute>
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:PostalZone"/>
              </xsl:element>
              <xsl:if test="exists(cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CountrySubentity)">
                <xsl:element name="deliver-to-address-line-3">
                  <xsl:attribute name="id">bt-79</xsl:attribute>
                  <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cbc:CountrySubentity"/>
                </xsl:element>
              </xsl:if>
              <xsl:element name="deliver-to-post-code">
                <xsl:attribute name="id">bt-80</xsl:attribute>
                <xsl:value-of select="cac:Delivery/cac:DeliveryLocation/cac:Address/cac:Country/cbc:IdentificationCode"/>
              </xsl:element>
            </xsl:element>
          </xsl:if>
        </xsl:element>
      </xsl:if>
      <xsl:element name="payment-instructions">
        <xsl:attribute name="id">bg-16</xsl:attribute>
        <xsl:element name="payment-means-type-code">
          <xsl:attribute name="id">bt-81</xsl:attribute>
          <xsl:value-of select="cac:PaymentMeans/cbc:PaymentMeansCode"/>
        </xsl:element>
        <xsl:if test="exists(cac:PaymentMeans/cbc:PaymentMeansCode[@name])">
          <xsl:element name="payment-means-text">
            <xsl:attribute name="id">bt-82</xsl:attribute>
            <xsl:value-of select="cac:PaymentMeans/cbc:PaymentMeansCode/@name"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:PaymentMeans/cbc:PaymentID)">
          <xsl:element name="remittance-information">
            <xsl:attribute name="id">bt-83</xsl:attribute>
            <xsl:value-of select="cac:PaymentMeans/cbc:PaymentID"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:PaymentMeans/cac:PayeeFinancialAccount)">
          <xsl:element name="credit-transfers">
            <xsl:attribute name="id">bg-17</xsl:attribute>
            <!-- Note that UBL does not actually support multiple bg-17 instances -->
            <xsl:for-each select="cac:PaymentMeans/cac:PayeeFinancialAccount">
              <xsl:element name="credit-transfer">
                <xsl:attribute name="id">bg-17</xsl:attribute>
                <xsl:element name="payment-account-identifier">
                  <xsl:attribute name="id">bt-84</xsl:attribute>
                  <xsl:element name="content">
                    <xsl:value-of select="./cbc:ID"/>
                  </xsl:element>
                </xsl:element>
                <xsl:if test="exists(./cbc:Name)">
                  <xsl:element name="payment-account-name">
                    <xsl:attribute name="id">bt-85</xsl:attribute>
                    <xsl:value-of select="./cbc:Name"/>
                  </xsl:element>
                </xsl:if>
                <xsl:if test="exists(./cac:FinancialInstitutionBranch/cbc:ID)">
                  <xsl:element name="payment-service-provider-identifier">
                    <xsl:attribute name="id">bt-86</xsl:attribute>
                    <xsl:element name="content">
                      <xsl:value-of select="./cac:FinancialInstitutionBranch/cbc:ID"/>
                    </xsl:element>
                  </xsl:element>
                </xsl:if>
              </xsl:element>
            </xsl:for-each>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:PaymentMeans/cac:CardAccount/cbc:PrimaryAccountNumberID)">
          <xsl:element name="payment-card-information">
            <xsl:attribute name="id">bg-18</xsl:attribute>
            <xsl:element name="payment-card-primary-account-number">
              <xsl:attribute name="id">bt-87</xsl:attribute>
              <xsl:value-of select="cac:PaymentMeans/cac:CardAccount/cbc:PrimaryAccountNumberID"/>
            </xsl:element>
            <xsl:if test="exists(cac:PaymentMeans/cac:CardAccount/cbc:HolderName)">
              <xsl:element name="payment-card-holder-name">
                <xsl:attribute name="id">bt-88</xsl:attribute>
                <xsl:value-of select="cac:PaymentMeans/cac:CardAccount/cbc:HolderName"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:PaymentMeans/cac:PaymentMandate/cbc:ID)">
          <xsl:element name="direct-debit">
            <xsl:attribute name="id">bg-19</xsl:attribute>
            <xsl:element name="mandate-reference-identifier">
              <xsl:attribute name="id">bt-89</xsl:attribute>
              <xsl:value-of select="cac:PaymentMeans/cac:PaymentMandate/cbc:ID"/>
            </xsl:element>
            <xsl:element name="bank-assigned-creditor-identifier">
              <xsl:attribute name="id">bt-90</xsl:attribute>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[normalize-space(upper-case(@schemeID)) = 'SEPA']/cbc:ID"/>
            </xsl:element>
            <xsl:element name="debited-account-identifier">
              <xsl:attribute name="id">bt-91</xsl:attribute>
              <xsl:value-of select="cac:PaymentMeans/cac:PaymentMandate/cac:PayerFinancialAccount/cbc:ID"/>
            </xsl:element>
          </xsl:element>
        </xsl:if>
      </xsl:element>
      <xsl:if test="exists(cac:AllowanceCharge[cbc:ChargeIndicator = false()]/cbc:Amount)">
        <xsl:element name="document-level-allowances">
          <xsl:attribute name="id">bg-20</xsl:attribute>
          <xsl:for-each select="cac:AllowanceCharge[cbc:ChargeIndicator = false()]">
            <xsl:element name="document-level-allowance">
              <xsl:attribute name="id">bg-20</xsl:attribute>
              <xsl:element name="document-level-allowance-amount">
                <xsl:attribute name="id">bt-92</xsl:attribute>
                <xsl:value-of select="./cbc:Amount"/>
              </xsl:element>
              <xsl:if test="exists(./cbc:BaseAmount)">
                <xsl:element name="document-level-allowance-base-amount">
                  <xsl:attribute name="id">bt-93</xsl:attribute>
                  <xsl:value-of select="./cbc:BaseAmount"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
                <xsl:element name="document-level-allowance-percentage">
                  <xsl:attribute name="id">bt-94</xsl:attribute>
                  <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
                </xsl:element>
              </xsl:if>
              <xsl:element name="document-level-allowance-vat-category-code">
                <xsl:attribute name="id">bt-95</xsl:attribute>
                <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:Percent)">
                <xsl:element name="document-level-allowance-vat-rate">
                  <xsl:attribute name="id">bt-96</xsl:attribute>
                  <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:Percent"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:AllowanceChargeReason)">
                <xsl:element name="document-level-allowance-reason">
                  <xsl:attribute name="id">bt-97</xsl:attribute>
                  <xsl:value-of select="./cbc:AllowanceChargeReason"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
                <xsl:element name="document-level-allowance-reason-code">
                  <xsl:attribute name="id">bt-98</xsl:attribute>
                  <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:AllowanceCharge[cbc:ChargeIndicator = true()]/cbc:Amount)">
        <xsl:element name="document-level-charges">
          <xsl:attribute name="id">bg-21</xsl:attribute>
          <xsl:for-each select="cac:AllowanceCharge[cbc:ChargeIndicator = false()]">
            <xsl:element name="document-level-charge">
              <xsl:attribute name="id">bg-21</xsl:attribute>
              <xsl:element name="document-level-charge-amount">
                <xsl:attribute name="id">bt-99</xsl:attribute>
                <xsl:value-of select="./cbc:Amount"/>
              </xsl:element>
              <xsl:if test="exists(./cbc:BaseAmount)">
                <xsl:element name="document-level-charge-base-amount">
                  <xsl:attribute name="id">bt-100</xsl:attribute>
                  <xsl:value-of select="./cbc:BaseAmount"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
                <xsl:element name="document-level-charge-percentage">
                  <xsl:attribute name="id">bt-101</xsl:attribute>
                  <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
                </xsl:element>
              </xsl:if>
              <xsl:element name="document-level-charge-vat-category-code">
                <xsl:attribute name="id">bt-102</xsl:attribute>
                <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:Percent)">
                <xsl:element name="document-level-charge-vat-rate">
                  <xsl:attribute name="id">bt-103</xsl:attribute>
                  <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:Percent"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:AllowanceChargeReason)">
                <xsl:element name="document-level-charge-reason">
                  <xsl:attribute name="id">bt-104</xsl:attribute>
                  <xsl:value-of select="./cbc:AllowanceChargeReason"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
                <xsl:element name="document-level-charge-reason-code">
                  <xsl:attribute name="id">bt-105</xsl:attribute>
                  <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
      </xsl:if>
      <xsl:element name="document-totals">
        <xsl:attribute name="id">bg-22</xsl:attribute>
        <xsl:element name="sum-of-invoice-line-net-amount">
          <xsl:attribute name="id">bt-106</xsl:attribute>
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:LineExtensionAmount"/>
        </xsl:element>
        <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount)">
          <xsl:element name="sum-of-allowances-on-document-level">
            <xsl:attribute name="id">bt-107</xsl:attribute>
            <xsl:value-of select="cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:ChargeTotalAmount)">
          <xsl:element name="sum-of-charges-on-document-level">
            <xsl:attribute name="id">bt-108</xsl:attribute>
            <xsl:value-of select="cac:LegalMonetaryTotal/cbc:ChargeTotalAmount"/>
          </xsl:element>
        </xsl:if>
        <xsl:element name="invoice-total-amount-without-vat">
          <xsl:attribute name="id">bt-109</xsl:attribute>
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount"/>
        </xsl:element>
        <xsl:if test="exists(cac:TaxTotal/cbc:TaxAmount[@currencyID=cbc:DocumentCurrencyCode])">
          <xsl:element name="invoice-total-vat-amount">
            <xsl:attribute name="id">bt-110</xsl:attribute>
            <xsl:value-of select="cac:TaxTotal/cbc:TaxAmount[@currencyID=cbc:DocumentCurrencyCode]"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:TaxTotal/cbc:TaxAmount[@currencyID=cbc:TaxCurrencyCode])">
          <xsl:element name="invoice-total-vat-amount-in-accounting-currency">
            <xsl:attribute name="id">bt-111</xsl:attribute>
            <xsl:value-of select="cac:TaxTotal/cbc:TaxAmount[@currencyID=cbc:TaxCurrencyCode]"/>
          </xsl:element>
        </xsl:if>
        <xsl:element name="invoice-total-amount-with-vat">
          <xsl:attribute name="id">bt-112</xsl:attribute>
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount"/>
        </xsl:element>
        <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:PrepaidAmount)">
          <xsl:element name="paid-amount">
            <xsl:attribute name="id">bt-113</xsl:attribute>
            <xsl:value-of select="cac:LegalMonetaryTotal/cbc:PrepaidAmount"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:LegalMonetaryTotal/cbc:PayableRoundingAmount)">
          <xsl:element name="rounding-amount">
            <xsl:attribute name="id">bt-114</xsl:attribute>
            <xsl:value-of select="cac:LegalMonetaryTotal/cbc:PayableRoundingAmount"/>
          </xsl:element>
        </xsl:if>
        <xsl:element name="amount-due-for-payment">
          <xsl:attribute name="id">bt-115</xsl:attribute>
          <xsl:value-of select="cac:LegalMonetaryTotal/cbc:PayableAmount"/>
        </xsl:element>
      </xsl:element>
      <xsl:element name="vat-breakdown">
        <xsl:attribute name="id">bg-23</xsl:attribute>
        <xsl:for-each select="cac:TaxTotal/cac:TaxSubtotal">
          <xsl:element name="vat-breakdown">
            <xsl:attribute name="id">bg-23</xsl:attribute>
            <xsl:element name="vat-category-taxable-amount">
              <xsl:attribute name="id">bt-116</xsl:attribute>
              <xsl:value-of select="./cbc:TaxableAmount"/>
            </xsl:element>
            <xsl:element name="vat-category-tax-amount">
              <xsl:attribute name="id">bt-117</xsl:attribute>
              <xsl:value-of select="./cbc:TaxAmount"/>
            </xsl:element>
            <xsl:element name="vat-category-code">
              <xsl:attribute name="id">bt-118</xsl:attribute>
              <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:ID"/>
            </xsl:element>
            <xsl:element name="vat-category-rate">
              <xsl:attribute name="id">bt-119</xsl:attribute>
              <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:Percent"/>
            </xsl:element>
            <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReason)">
              <xsl:element name="vat-exemption-reason-text">
                <xsl:attribute name="id">bt-120</xsl:attribute>
                <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReason"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="exists(./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReasonCode)">
              <xsl:element name="vat-exemption-reason-code">
                <xsl:attribute name="id">bt-121</xsl:attribute>
                <xsl:value-of select="./cac:TaxCategory[cac:TaxScheme/normalize-space(upper-case(cbc:ID))='VAT']/cbc:TaxExemptionReasonCode"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:for-each>
      </xsl:element>
      <!-- UBL CreditNote DocumentType can't be 90 either -->
      <xsl:if test="exists(cac:AdditionalDocumentReference[cbc:DocumentTypeCode != '130']/cbc:ID)">
        <xsl:element name="additional-supporting-documents">
          <xsl:attribute name="id">bg-24</xsl:attribute>
          <xsl:for-each select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode != '130']">
            <xsl:element name="additional-supporting-document">
              <xsl:attribute name="id">bg-24</xsl:attribute>
              <xsl:element name="supporting-document-reference">
                <xsl:attribute name="id">bt-122</xsl:attribute>
                <xsl:value-of select="./cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(./cbc:DocumentDescription)">
                <xsl:element name="supporting-document-description">
                  <xsl:attribute name="id">bt-123</xsl:attribute>
                  <xsl:value-of select="./cbc:DocumentDescription"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:Attachment/cac:ExternalReference/cbc:URI)">
                <xsl:element name="external-document-location">
                  <xsl:attribute name="id">bt-124</xsl:attribute>
                  <xsl:value-of select="./cac:Attachment/cac:ExternalReference/cbc:URI"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:Attachment/cbc:EmbeddedDocumentBinaryObject)">
                <xsl:element name="attached-document">
                  <xsl:attribute name="id">bt-125</xsl:attribute>
                  <xsl:element name="content">
                    <xsl:value-of select="./cac:Attachment/cbc:EmbeddedDocumentBinaryObject"/>
                  </xsl:element>
                  <xsl:element name="mime-code">
                    <xsl:value-of select="./cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode"/>
                  </xsl:element>
                  <xsl:element name="filename">
                    <xsl:value-of select="./cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@filename"/>
                  </xsl:element>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
        <xsl:element name="invoice-lines">
          <xsl:attribute name="id">bg-25</xsl:attribute>
          <!-- CreditNote: cac:CreditNoteLine -->
          <xsl:for-each select="cac:InvoiceLine">
            <xsl:element name="invoice-line">
              <xsl:attribute name="id">bg-25</xsl:attribute>
              <xsl:element name="invoice-line-identifier">
                <xsl:attribute name="id">bt-126</xsl:attribute>
                <xsl:element name="content">
                  <xsl:value-of select="./cbc:ID"/>
                </xsl:element>
              </xsl:element>
              <xsl:if test="exists(./cbc:Note)">
                <xsl:element name="invoice-line-note">
                  <xsl:attribute name="id">bt-127</xsl:attribute>
                  <xsl:value-of select="./cbc:Note"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID)">
                <xsl:element name="invoice-line-object-identifier">
                  <xsl:attribute name="id">bt-128</xsl:attribute>
                  <xsl:element name="content">
                    <xsl:value-of select="./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID"/>
                  </xsl:element>
                  <xsl:if test="exists(./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID[@schemeID])">
                    <xsl:element name="scheme-identifier">
                      <xsl:value-of select="./cac:DocumentReference[cbc:DocumentTypeCode='130']/cbc:ID/@schemeID"/>
                    </xsl:element>
                  </xsl:if>
                </xsl:element>
              </xsl:if>
              <xsl:element name="invoiced-quantity">
                <xsl:attribute name="id">bt-129</xsl:attribute>
                <!-- UBL CreditNote: cbc:CreditedQuantity -->
                <xsl:value-of select="./cbc:InvoicedQuantity"/>
              </xsl:element>
              <xsl:element name="invoiced-quantity-unit-of-measure-code">
                <xsl:attribute name="id">bt-130</xsl:attribute>
                <!-- UBL CreditNote: cbc:CreditedQuantity -->
                <xsl:value-of select="./cbc:InvoicedQuantity/@unitCode"/>
              </xsl:element>
              <xsl:element name="invoice-line-net-amount">
                <xsl:attribute name="id">bt-131</xsl:attribute>
                <xsl:value-of select="./cbc:LineExtensionAmount"/>
              </xsl:element>
              <xsl:if test="exists(./cac:OrderLineReference/cbc:LineID)">
                <xsl:element name="referenced-purchase-order-line-reference">
                  <xsl:attribute name="id">bt-132</xsl:attribute>
                  <xsl:value-of select="./cac:OrderLineReference/cbc:LineID"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cbc:AccountingCost)">
                <xsl:element name="invoice-line-buyer-accounting-reference">
                  <xsl:attribute name="id">bt-133</xsl:attribute>
                  <xsl:value-of select="./cbc:AccountingCost"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:InvoicePeriod/cbc:StartDate) or exists(./cac:InvoicePeriod/cbc:EndDate)">
                <xsl:element name="invoice-line-period">
                  <xsl:attribute name="id">bg-26</xsl:attribute>
                  <xsl:if test="exists(./cac:InvoicePeriod/cbc:StartDate)">
                    <xsl:element name="invoice-line-period-start-date">
                      <xsl:attribute name="id">bt-134</xsl:attribute>
                      <xsl:value-of select="./cac:InvoicePeriod/cbc:StartDate"/>
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="exists(./cac:InvoicePeriod/cbc:EndDate)">
                    <xsl:element name="invoice-line-period-end-date">
                      <xsl:attribute name="id">bt-135</xsl:attribute>
                      <xsl:value-of select="./cac:InvoicePeriod/cbc:EndDate"/>
                    </xsl:element>
                  </xsl:if>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:AllowanceCharge[cbc:ChargeIndicator = false()]/cbc:Amount)">
                <xsl:element name="invoice-line-allowances">
                  <xsl:attribute name="id">bg-27</xsl:attribute>
                  <xsl:for-each select="./cac:AllowanceCharge[cbc:ChargeIndicator = false()]">
                    <xsl:element name="invoice-line-allowance">
                      <xsl:attribute name="id">bg-27</xsl:attribute>
                      <xsl:element name="invoice-line-allowance-amount">
                        <xsl:attribute name="id">bt-136</xsl:attribute>
                        <xsl:value-of select="./cbc:Amount"/>
                      </xsl:element>
                      <xsl:if test="exists(./cbc:BaseAmount)">
                        <xsl:element name="invoice-line-allowance-base-amount">
                          <xsl:attribute name="id">bt-137</xsl:attribute>
                          <xsl:value-of select="./cbc:BaseAmount"/>
                        </xsl:element>
                      </xsl:if>
                      <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
                        <xsl:element name="invoice-line-allowance-percentage">
                          <xsl:attribute name="id">bt-138</xsl:attribute>
                          <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
                        </xsl:element>
                      </xsl:if>
                      <xsl:if test="exists(./cbc:AllowanceChargeReason)">
                        <xsl:element name="invoice-line-allowance-reason">
                          <xsl:attribute name="id">bt-139</xsl:attribute>
                          <xsl:value-of select="./cbc:AllowanceChargeReason"/>
                        </xsl:element>
                      </xsl:if>
                      <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
                        <xsl:element name="invoice-line-allowance-reason-code">
                          <xsl:attribute name="id">bt-140</xsl:attribute>
                          <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
                        </xsl:element>
                      </xsl:if>
                    </xsl:element>
                  </xsl:for-each>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:AllowanceCharge[cbc:ChargeIndicator = true()]/cbc:Amount)">
                <xsl:element name="invoice-line-charges">
                  <xsl:attribute name="id">bg-28</xsl:attribute>
                  <xsl:for-each select="./cac:AllowanceCharge[cbc:ChargeIndicator = true()]">
                    <xsl:element name="invoice-line-charge">
                      <xsl:attribute name="id">bg-28</xsl:attribute>
                      <xsl:element name="invoice-line-charge-amount">
                        <xsl:attribute name="id">bt-141</xsl:attribute>
                        <xsl:value-of select="./cbc:Amount"/>
                      </xsl:element>
                      <xsl:if test="exists(./cbc:BaseAmount)">
                        <xsl:element name="invoice-line-charge-base-amount">
                          <xsl:attribute name="id">bt-142</xsl:attribute>
                          <xsl:value-of select="./cbc:BaseAmount"/>
                        </xsl:element>
                      </xsl:if>
                      <xsl:if test="exists(./cbc:MultiplierFactorNumeric)">
                        <xsl:element name="invoice-line-charge-percentage">
                          <xsl:attribute name="id">bt-143</xsl:attribute>
                          <xsl:value-of select="./cbc:MultiplierFactorNumeric"/>
                        </xsl:element>
                      </xsl:if>
                      <xsl:if test="exists(./cbc:AllowanceChargeReason)">
                        <xsl:element name="invoice-line-charge-reason">
                          <xsl:attribute name="id">bt-144</xsl:attribute>
                          <xsl:value-of select="./cbc:AllowanceChargeReason"/>
                        </xsl:element>
                      </xsl:if>
                      <xsl:if test="exists(./cbc:AllowanceChargeReasonCode)">
                        <xsl:element name="invoice-line-charge-reason-code">
                          <xsl:attribute name="id">bt-145</xsl:attribute>
                          <xsl:value-of select="./cbc:AllowanceChargeReasonCode"/>
                        </xsl:element>
                      </xsl:if>
                    </xsl:element>
                  </xsl:for-each>
                </xsl:element>
              </xsl:if>
              <xsl:element name="price-details">
                <xsl:attribute name="id">bg-29</xsl:attribute>
                <xsl:element name="item-net-price">
                  <xsl:attribute name="id">bt-146</xsl:attribute>
                  <xsl:value-of select="./cac:Price/cbc:PriceAmount"/>
                </xsl:element>
                <xsl:if test="exists(./cac:Price/cac:AllowanceCharge/cbc:Amount)">
                  <xsl:element name="item-price-discount">
                    <xsl:attribute name="id">bt-147</xsl:attribute>
                    <xsl:value-of select="./cac:Price/cac:AllowanceCharge/cbc:Amount"/>
                  </xsl:element>
                </xsl:if>
                <xsl:if test="exists(./cac:Price/cac:AllowanceCharge/cbc:BaseAmount)">
                  <xsl:element name="item-gross-price">
                    <xsl:attribute name="id">bt-148</xsl:attribute>
                    <xsl:value-of select="./cac:Price/cac:AllowanceCharge/cbc:BaseAmount"/>
                  </xsl:element>
                </xsl:if>
                <xsl:if test="exists(./cac:Price/cbc:BaseQuantity)">
                  <xsl:element name="item-price-base-quantity">
                    <xsl:attribute name="id">bt-149</xsl:attribute>
                    <xsl:value-of select="./cac:Price/cbc:BaseQuantity"/>
                  </xsl:element>
                </xsl:if>
                <xsl:if test="exists(./cac:Price/cbc:BaseQuantity[@unitCode])">
                  <xsl:element name="item-price-base-quantity-unit-of-measure">
                    <xsl:attribute name="id">bt-150</xsl:attribute>
                    <xsl:value-of select="./cac:Price/cbc:BaseQuantity/@unitCode"/>
                  </xsl:element>
                </xsl:if>
              </xsl:element>
            </xsl:element>
          </xsl:for-each>
          <xsl:element name="line-vat-information">
            <xsl:attribute name="id">bg-30</xsl:attribute>
            <xsl:element name="invoiced-item-vat-category-code">
              <xsl:attribute name="id">bt-151</xsl:attribute>
              <xsl:value-of select="./cac:Item/cac:ClassifiedTaxCategory[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:ID"/>
            </xsl:element>
            <xsl:if test="exists(./cac:Item/cac:ClassifiedTaxCategory[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:Percent)">
              <xsl:element name="invoiced-item-vat-rate">
                <xsl:attribute name="id">bt-152</xsl:attribute>
                <xsl:value-of select="./cac:Item/cac:ClassifiedTaxCategory[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:Percent"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
          <xsl:element name="item-information">
            <xsl:attribute name="id">bg-31</xsl:attribute>
            <xsl:element name="item-name">
              <xsl:attribute name="id">bt-153</xsl:attribute>
              <xsl:value-of select="./cac:Item/cbc:Name"/>
              <xsl:if test="exists(./cac:Item/cbc:Description)">
                <xsl:element name="item-description">
                  <xsl:attribute name="id">bt-154</xsl:attribute>
                  <xsl:value-of select="./cac:Item/cbc:Description"/>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:Item/cac:SellersItemIdentification/cbc:ID)">
                <xsl:element name="item-sellers-identifier">
                  <xsl:attribute name="id">bt-155</xsl:attribute>
                  <xsl:element name="content">
                    <xsl:value-of select="./cac:Item/cac:SellersItemIdentification/cbc:ID"/>
                  </xsl:element>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:Item/cac:BuyersItemIdentification/cbc:ID)">
                <xsl:element name="item-buyers-identifier">
                  <xsl:attribute name="id">bt-156</xsl:attribute>
                  <xsl:element name="content">
                    <xsl:value-of select="./cac:Item/cac:BuyersItemIdentification/cbc:ID"/>
                  </xsl:element>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:Item/cac:StandardItemIdentification/cbc:ID)">
                <xsl:element name="item-standard-identifier">
                  <xsl:attribute name="id">bt-157</xsl:attribute>
                  <xsl:element name="content">
                    <xsl:value-of select="./cac:Item/cac:StandardItemIdentification/cbc:ID"/>
                  </xsl:element>
                  <xsl:element name="scheme-identifier">
                    <xsl:value-of select="./cac:Item/cac:StandardItemIdentification/cbc:ID/@schemeID"/>
                  </xsl:element>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cac:Item/cac:CommoditiyClassification/cbc:ItemClassificationCode)">
                <xsl:element name="item-classification-identifiers">
                  <xsl:attribute name="id">bt-158</xsl:attribute>
                  <xsl:for-each select="./cac:Item/cac:CommoditiyClassification">
                    <xsl:element name="item-classification-identifier">
                      <xsl:attribute name="id">bt-158</xsl:attribute>
                      <xsl:element name="content">
                        <xsl:value-of select="./cbc:ItemClassificationCode"/>
                      </xsl:element>
                      <xsl:element name="scheme-identifier">
                        <xsl:value-of select="./cbc:ItemClassificationCode/@listID"/>
                      </xsl:element>
                      <xsl:if test="exists(./cbc:ItemClassificationCode[@listVersionID])">
                        <xsl:element name="scheme-version-identifier">
                          <xsl:value-of select="./cbc:ItemClassificationCode/@listVersionID"/>
                        </xsl:element>
                      </xsl:if>
                    </xsl:element>
                  </xsl:for-each>
                </xsl:element>
              </xsl:if>
              <xsl:if test="exists(./cacItem/cac:OriginCountry/cbc:IdentificationCode)">
                <xsl:element name="item-country-of-origin">
                  <xsl:attribute name="id">bt-159</xsl:attribute>
                  <xsl:value-of select="./cac:Item/cac:OriginCountry/cbc:IdentificationCode"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
            <xsl:if test="exists(./cac:Item/cac:AdditionalItemProperty/cbc:Name)">
              <xsl:element name="item-attributes">
                <xsl:attribute name="id">bg-32</xsl:attribute>
                <xsl:for-each select="./cac:Item/cac:AdditionalItemProperty">
                  <xsl:element name="item-attribute">
                    <xsl:attribute name="id">bg-32</xsl:attribute>
                    <xsl:element name="item-attribute-name">
                      <xsl:attribute name="id">bt-160</xsl:attribute>
                      <xsl:value-of select="./cbc:Name"/>
                    </xsl:element>
                    <xsl:element name="item-attribute-value">
                      <xsl:attribute name="id">bt-161</xsl:attribute>
                      <xsl:value-of select="./cbc:Value"/>
                    </xsl:element>
                  </xsl:element>
                </xsl:for-each>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:element>
      </xsl:if>
    </invoice>
  </xsl:template>
</xsl:stylesheet>
