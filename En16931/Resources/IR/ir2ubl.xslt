<xsl:stylesheet
    xmlns:invoice="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
    xmlns:credit-note="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2"
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:ir="urn:todo"
    exclude-result-prefixes="invoice credit-note xsl ir"
    version="1.0">
  <xsl:template match="/ir:invoice" mode="invoice">
    <invoice:Invoice xmlns:invoice="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2">
      <cbc:CustomizationID>
        <!-- bt-24 -->
        <xsl:value-of select="ir:process-control/ir:specification-identifier/ir:content"/>
      </cbc:CustomizationID>
      <cbc:ProfileID>
        <!-- bt-23 -->
        <xsl:value-of select="ir:process-control/ir:business-process-type"/>
      </cbc:ProfileID>
      <cbc:ID>
		<!-- bt-1 -->
        <xsl:value-of select="ir:invoice-number"/>
      </cbc:ID>
      <cbc:IssueDate>
        <!-- bt-2 -->
        <xsl:value-of select="ir:invoice-issue-date"/>
      </cbc:IssueDate>
      <xsl:if test="exists(ir:payment-due-date)">
        <cbc:DueDate>
          <!-- bt-9 -->
          <xsl:value-of select="ir:payment-due-date"/>
        </cbc:DueDate>
      </xsl:if>
      <cbc:InvoiceTypeCode>
		<!-- bt-3 -->
        <xsl:value-of select="ir:invoice-type-code"/>
      </cbc:InvoiceTypeCode>
      <xsl:for-each select="ir:invoice-notes/ir:invoice-note">
        <cbc:Note>
          <xsl:if test="exists(./ir:invoice-note-subject-code)">
            <xsl:text>#</xsl:text>
		    <!-- bt-21 -->
            <xsl:value-of select="./ir:invoice-note-subject-code"/>
            <xsl:text>#</xsl:text>
          </xsl:if>
	      <!-- bt-22 -->
          <xsl:value-of select="./ir:invoice-note"/>
        </cbc:Note>
      </xsl:for-each>
      <xsl:if test="exists(ir:value-added-tax-point-date)">
        <cbc:TaxPointDate>
          <!-- bt-7 -->
          <xsl:value-of select="ir:value-added-tax-point-date"/>
        </cbc:TaxPointDate>
      </xsl:if>
      <cbc:DocumentCurrencyCode>
        <!-- bt-5 -->
        <xsl:value-of select="ir:invoice-currency-code"/>
      </cbc:DocumentCurrencyCode>
      <xsl:if test="exists(ir:vat-accounting-currency-code)">
        <cbc:TaxCurrencyCode>
          <!-- bt-6 -->
          <xsl:value-of select="ir:vat-accounting-currency-code"/>
        </cbc:TaxCurrencyCode>
      </xsl:if>
      <xsl:if test="exists(ir:buyer-accounting-reference)">
        <cbc:AccountingCost>
          <!-- bt-19 -->
          <xsl:value-of select="ir:buyer-accounting-reference"/>
        </cbc:AccountingCost>
      </xsl:if>
      <cbc:BuyerReference>
        <!-- bt-10 -->
        <xsl:value-of select="ir:buyer-reference"/>
      </cbc:BuyerReference>
      <xsl:if test="exists(ir:delivery-information/ir:invoicing-period/ir:invoicing-period-start-date)
          or exists(ir:delivery-information/ir:invoicing-period/ir:invoicing-period-end-date)">
        <cac:InvoicePeriod>
          <xsl:if test="exists(ir:delivery-information/ir:invoicing-period/ir:invoicing-period-start-date)">
            <cbc:StartDate>
              <!-- bt-73 -->
              <xsl:value-of select="ir:delivery-information/ir:invoicing-period/ir:invoicing-period-start-date"/>
            </cbc:StartDate>
          </xsl:if>
          <xsl:if test="exists(ir:delivery-information/ir:invoicing-period/ir:invoicing-period-end-date)">
            <cbc:EndDate>
              <!-- bt-74 -->
              <xsl:value-of select="ir:delivery-information/ir:invoicing-period/ir:invoicing-period-end-date"/>
            </cbc:EndDate>
          </xsl:if>
        </cac:InvoicePeriod>
      </xsl:if>
      <xsl:if test="exists(ir:purchase-order-reference)
          or exists(ir:sales-order-reference)">
        <cac:OrderReference>
          <xsl:if test="exists(ir:purchase-order-reference)">
            <cbc:ID>
              <!-- bt-13 -->
              <xsl:value-of select="ir:purchase-order-reference"/>
            </cbc:ID>
          </xsl:if>
          <xsl:if test="exists(ir:sales-order-reference)">
            <cbc:SalesOrderID>
              <!-- bt-14 -->
              <xsl:value-of select="ir:sales-order-reference"/>
            </cbc:SalesOrderID>
          </xsl:if>
        </cac:OrderReference>
      </xsl:if>
      <xsl:for-each select="ir:preceding-invoice-references/ir:preceding-invoice-reference">
        <cac:BillingReference>
          <cac:InvoiceDocumentReference>
            <cbc:ID>
              <!-- bt-25 -->
              <xsl:value-of select="./ir:preceding-invoice-reference"/>
            </cbc:ID>
            <xsl:if test="exists(./ir:preceding-invoice-issue-date)">
              <cbc:IssueDate>
                <!-- bt-26 -->
                <xsl:value-of select="./ir:preceding-invoice-issue-date"/>
              </cbc:IssueDate>
            </xsl:if>
          </cac:InvoiceDocumentReference>
        </cac:BillingReference>
      </xsl:for-each>
      <xsl:if test="exists(ir:despatch-advice-reference)">
        <cac:DespatchDocumentReference>
          <cbc:ID>
            <!-- bt-16 -->
            <xsl:value-of select="ir:despatch-advice-reference"/>
          </cbc:ID>
        </cac:DespatchDocumentReference>
      </xsl:if>
      <xsl:if test="exists(ir:receiving-advice-reference)">
        <cac:ReceiptDocumentReference>
          <cbc:ID>
            <!-- bt-15 -->
            <xsl:value-of select="ir:receiving-advice-reference"/>
          </cbc:ID>
        </cac:ReceiptDocumentReference>
      </xsl:if>
      <xsl:if test="exists(ir:tender-or-lot-reference)">
        <cac:OriginatorDocumentReference>
          <cbc:ID>
            <!-- bt-17 -->
            <xsl:value-of select="ir:tender-or-lot-reference"/>
          </cbc:ID>
        </cac:OriginatorDocumentReference>
      </xsl:if>
      <xsl:if test="exists(ir:contract-reference)">
        <cac:ContractDocumentReference>
          <cbc:ID>
            <!-- bt-12 -->
            <xsl:value-of select="ir:contract-reference"/>
          </cbc:ID>
        </cac:ContractDocumentReference>
      </xsl:if>
      <xsl:for-each select="ir:additional-supporting-documents/ir:additional-supporting-document">
        <cac:AdditionalDocumentReference>
          <cbc:ID>
            <!-- bt-122 -->
            <xsl:value-of select="./ir:supporting-document-reference"/>
          </cbc:ID>
          <xsl:if test="exists(./ir:supporting-document-description)">
            <cbc:DocumentDescription>
              <!-- bt-123 -->
              <xsl:value-of select="./ir:supporting-document-description"/>
            </cbc:DocumentDescription>
          </xsl:if>
          <xsl:if test="exists(./ir:attached-document)
              or exists(./ir:external-document-location)">
            <cac:Attachment>
              <xsl:if test="exists(./ir:attached-document)">
                <cbc:EmbeddedDocumentBinaryObject>
                  <xsl:attribute name="mimeCode">
                    <!-- bt-125-1 -->
                    <xsl:value-of select="./ir:attached-document/ir:mime-code"/>
                  </xsl:attribute>
                  <xsl:attribute name="filename">
                    <!-- bt-125-2 -->
                    <xsl:value-of select="./ir:attached-document/ir:filename"/>
                  </xsl:attribute>
                  <!-- bt-125 -->
                  <xsl:text>dGVzdA==</xsl:text>
                  <!-- TODO: uncomment and replace above <xsl:text> with this
                  <xsl:value-of select="./ir:attached-document/ir:content"/>
                  -->
                </cbc:EmbeddedDocumentBinaryObject>
              </xsl:if>
              <xsl:if test="exists(./ir:external-document-location)">
                <cac:ExternalReference>
                  <cbc:URI>
                    <!-- bt-124 -->
                    <xsl:value-of select="./ir:external-document-location"/>
                  </cbc:URI>
                </cac:ExternalReference>
              </xsl:if>
            </cac:Attachment>
          </xsl:if>
        </cac:AdditionalDocumentReference>
      </xsl:for-each>
      <xsl:if test="exists(ir:invoiced-object-identifier)">
        <cac:AdditionalDocumentReference>
          <cbc:ID>
            <!-- bt-18 -->
            <xsl:value-of select="ir:invoiced-object-identifier"/>
          </cbc:ID>
          <cbc:DocumentTypeCode>130</cbc:DocumentTypeCode>
        </cac:AdditionalDocumentReference>
      </xsl:if>
      <xsl:if test="exists(ir:project-reference)">
        <cac:ProjectReference>
          <cbc:ID>
            <!-- bt-11 -->
            <xsl:value-of select="ir:project-reference"/>
          </cbc:ID>
        </cac:ProjectReference>
      </xsl:if>
      <cac:AccountingSupplierParty>
        <cac:Party>
          <cbc:EndpointID>
            <xsl:attribute name="schemeID">
              <!-- bt-34-1 -->
              <xsl:value-of select="ir:seller/ir:seller-electronic-address/ir:scheme-identifier"/>
            </xsl:attribute>
            <!-- bt-34 -->
            <xsl:value-of select="ir:seller/ir:seller-electronic-address/ir:content"/>
          </cbc:EndpointID>
          <xsl:for-each select="ir:seller/ir:seller-identifiers/ir:seller-identifier">
            <cac:PartyIdentification>
              <cbc:ID>
                <xsl:if test="exists(./ir:scheme-identifier)">
                  <xsl:attribute name="schemeID">
                    <!-- bt-29-1 -->
                    <xsl:value-of select="./ir:scheme-identifier"/>
                  </xsl:attribute>
                </xsl:if>
                <!-- bt-29 -->
                <xsl:value-of select="./ir:content"/>
              </cbc:ID>
            </cac:PartyIdentification>
          </xsl:for-each>
          <xsl:if test="exists(ir:seller/ir:seller-trading-name)">
            <cac:PartyName>
              <cbc:Name>
                <!-- bt-28 -->
                <xsl:value-of select="ir:seller/ir:seller-trading-name"/>
              </cbc:Name>
            </cac:PartyName>
          </xsl:if>
          <cac:PostalAddress>
            <xsl:if test="exists(ir:seller/ir:seller-postal-address/ir:seller-address-line-1)">
              <cbc:StreetName>
                <!-- bt-35 -->
                <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-address-line-1"/>
              </cbc:StreetName>
            </xsl:if>
            <xsl:if test="exists(ir:seller/ir:seller-postal-address/ir:seller-address-line-2)">
              <cbc:AdditionalStreetName>
                <!-- bt-36 -->
                <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-address-line-2"/>
              </cbc:AdditionalStreetName>
            </xsl:if>
            <cbc:CityName>
              <!-- bt-37 -->
              <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-city"/>
            </cbc:CityName>
            <cbc:PostalZone>
              <!-- bt-38 -->
              <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-post-code"/>
            </cbc:PostalZone>
            <xsl:if test="exists(ir:seller/ir:seller-postal-address/ir:seller-country-subdivision)">
              <cbc:CountrySubentity>
                <!-- bt-39 -->
                <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-country-subdivision"/>
              </cbc:CountrySubentity>
            </xsl:if>
            <xsl:if test="exists(ir:seller/ir:seller-postal-address/ir:seller-address-line-3)">
              <cac:AddressLine>
                <cbc:Line>
                  <!-- bt-162 -->
                  <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-address-line-3"/>
                </cbc:Line>
              </cac:AddressLine>
            </xsl:if>
            <cac:Country>
              <cbc:IdentificationCode>
                <!-- bt-40 -->
                <xsl:value-of select="ir:seller/ir:seller-postal-address/ir:seller-country-code"/>
              </cbc:IdentificationCode>
            </cac:Country>
          </cac:PostalAddress>
          <xsl:if test="exists(ir:seller/ir:seller-vat-identifier)">
            <cac:PartyTaxScheme>
              <cbc:CompanyID>
                <!-- bt-31 -->
                <xsl:value-of select="ir:seller/ir:seller-vat-identifier/ir:content"/>
              </cbc:CompanyID>
              <cac:TaxScheme>
                <cbc:ID>VAT</cbc:ID>
              </cac:TaxScheme>
            </cac:PartyTaxScheme>
          </xsl:if>
          <xsl:if test="exists(ir:seller/ir:seller-tax-registration-identifier)">
            <cac:PartyTaxScheme>
              <cbc:CompanyID>
                <!-- bt-32 -->
                <xsl:value-of select="ir:seller/ir:seller-tax-registration-identifier/ir:content"/>
              </cbc:CompanyID>
              <cac:TaxScheme>
                <cbc:ID>FC</cbc:ID>
              </cac:TaxScheme>
            </cac:PartyTaxScheme>
          </xsl:if>
          <cac:PartyLegalEntity>
            <cbc:RegistrationName>
              <!-- bt-27 -->
              <xsl:value-of select="ir:seller/ir:seller-name"/>
            </cbc:RegistrationName>
            <xsl:if test="exists(ir:seller/ir:seller-legal-registration-identifier)">
              <cbc:CompanyID>
                <xsl:if test="exists(ir:seller/ir:seller-legal-registration-identifier/ir:scheme-identifier)">
                  <xsl:attribute name="schemeID">
                    <!-- bt-30-1 -->
                    <xsl:value-of select="ir:seller/ir:seller-legal-registration-identifier/ir:scheme-identifier"/>
                  </xsl:attribute>
                  <!-- bt-30 -->
                  <xsl:value-of select="ir:seller/ir:seller-legal-registration-identifier/ir:content"/>
                </xsl:if>
              </cbc:CompanyID>
            </xsl:if>
            <xsl:if test="exists(ir:seller/ir:seller-additional-legal-information)">
              <cbc:CompanyLegalForm>
                <!-- bt-33 -->
                <xsl:value-of select="ir:seller/ir:seller-additional-legal-information"/>
              </cbc:CompanyLegalForm>
            </xsl:if>
          </cac:PartyLegalEntity>
          <cac:Contact>
            <cbc:Name>
              <!-- bt-41 -->
              <xsl:value-of select="ir:seller/ir:seller-contact/ir:seller-contact-point"/>
            </cbc:Name>
            <cbc:Telephone>
              <!-- bt-42 -->
              <xsl:value-of select="ir:seller/ir:seller-contact/ir:seller-contact-telephone-number"/>
            </cbc:Telephone>
            <cbc:ElectronicMail>
              <!-- bt-43 -->
              <xsl:value-of select="ir:seller/ir:seller-contact/ir:seller-contact-email-address"/>
            </cbc:ElectronicMail>
          </cac:Contact>
        </cac:Party>
      </cac:AccountingSupplierParty>
      <cac:AccountingCustomerParty>
        <cac:Party>
          <xsl:if test="exists(ir:buyer/ir:buyer-electronic-address)">
            <cbc:EndpointID>
              <xsl:attribute name="schemeID">
                <!-- bt-49-1 -->
                <xsl:value-of select="ir:buyer/ir:buyer-electronic-address/ir:scheme-identifier"/>
              </xsl:attribute>
              <!-- bt-49 -->
              <xsl:value-of select="ir:buyer/ir:buyer-electronic-address/ir:content"/>
            </cbc:EndpointID>
          </xsl:if>
          <xsl:if test="exists(ir:buyer/ir:buyer-identifier)">
            <cac:PartyIdentification>
              <cbc:ID>
                <xsl:if test="exists(ir:buyer/ir:buyer-identifier/ir:scheme-identifier)">
                  <xsl:attribute name="schemeID">
                    <!-- bt-46-1 -->
                    <xsl:value-of select="ir:buyer/ir:buyer-identifier/ir:scheme-identifier"/>
                  </xsl:attribute>
                </xsl:if>
                <!-- bt-46 -->
                <xsl:value-of select="ir:buyer/ir:buyer-identifier/ir:content"/>
              </cbc:ID>
            </cac:PartyIdentification>
          </xsl:if>
          <xsl:if test="exists(ir:buyer/ir:buyer-trading-name)">
            <cac:PartyName>
              <cbc:Name>
                <!-- bt-45 -->
                <xsl:value-of select="ir:buyer/ir:buyer-trading-name"/>
              </cbc:Name>
            </cac:PartyName>
          </xsl:if>
          <cac:PostalAddress>
            <xsl:if test="exists(ir:buyer/ir:buyer-postal-address/ir:buyer-address-line-1)">
              <cbc:StreetName>
                <!-- bt-50 -->
                <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-address-line-1"/>
              </cbc:StreetName>
            </xsl:if>
            <xsl:if test="exists(ir:buyer/ir:buyer-postal-address/ir:buyer-address-line-2)">
              <cbc:AdditionalStreetName>
                <!-- bt-51 -->
                <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-address-line-2"/>
              </cbc:AdditionalStreetName>
            </xsl:if>
            <cbc:CityName>
              <!-- bt-52 -->
              <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-city"/>
            </cbc:CityName>
            <cbc:PostalZone>
              <!-- bt-53 -->
              <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-post-code"/>
            </cbc:PostalZone>
            <xsl:if test="exists(ir:buyer/ir:buyer-postal-address/ir:buyer-country-subdivision)">
              <cbc:CountrySubentity>
                <!-- bt-54 -->
                <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-country-subdivision"/>
              </cbc:CountrySubentity>
            </xsl:if>
            <xsl:if test="exists(ir:buyer/ir:buyer-postal-address/ir:buyer-address-line-3)">
              <cac:AddressLine>
                <cbc:Line>
                  <!-- bt-163 -->
                  <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-address-line-3"/>
                </cbc:Line>
              </cac:AddressLine>
            </xsl:if>
            <cac:Country>
              <cbc:IdentificationCode>
                <!-- bt-55 -->
                <xsl:value-of select="ir:buyer/ir:buyer-postal-address/ir:buyer-country-code"/>
              </cbc:IdentificationCode>
            </cac:Country>
          </cac:PostalAddress>
          <xsl:if test="exists(ir:buyer/ir:buyer-vat-identifier)">
            <cac:PartyTaxScheme>
              <cbc:CompanyID>
                <!-- bt-48 -->
                <xsl:value-of select="ir:buyer/ir:buyer-vat-identifier/ir:content"/>
              </cbc:CompanyID>
              <cac:TaxScheme>
                <cbc:ID>VAT</cbc:ID>
              </cac:TaxScheme>
            </cac:PartyTaxScheme>
          </xsl:if>
          <cac:PartyLegalEntity>
            <cbc:RegistrationName>
              <!-- bt-44 -->
              <xsl:value-of select="ir:buyer/ir:buyer-name"/>
            </cbc:RegistrationName>
            <xsl:if test="exists(ir:buyer/ir:buyer-legal-registration-identifier)">
              <cbc:CompanyID>
                <xsl:if test="exists(ir:buyer/ir:buyer-legal-registration-identifier/ir:scheme-identifier)">
                  <xsl:attribute name="schemeID">
                    <!-- bt-47-1 -->
                    <xsl:value-of select="ir:buyer/ir:buyer-legal-registration-identifier/ir:scheme-identifier"/>
                  </xsl:attribute>
                  <!-- bt-47 -->
                  <xsl:value-of select="ir:buyer/ir:buyer-legal-registration-identifier/ir:content"/>
                </xsl:if>
              </cbc:CompanyID>
            </xsl:if>
          </cac:PartyLegalEntity>
          <xsl:if test="exists(ir:buyer/ir:buyer-contact/ir:buyer-contact-point)
              or exists(ir:buyer/ir:buyer-contact/ir:buyer-contact-telephone-number)
              or exists(ir:buyer/ir:buyer-contact/ir:buyer-contact-email-address)">
            <cac:Contact>
              <xsl:if test="exists(ir:buyer/ir:buyer-contact/ir:buyer-contact-point)">
                <cbc:Name>
                  <!-- bt-56 -->
                  <xsl:value-of select="ir:buyer/ir:buyer-contact/ir:buyer-contact-point"/>
                </cbc:Name>
              </xsl:if>
              <xsl:if test="exists(ir:buyer/ir:buyer-contact/ir:buyer-contact-telephone-number)">
                <cbc:Telephone>
                  <!-- bt-57 -->
                  <xsl:value-of select="ir:buyer/ir:buyer-contact/ir:buyer-contact-telephone-number"/>
                </cbc:Telephone>
              </xsl:if>
              <xsl:if test="exists(ir:buyer/ir:buyer-contact/ir:buyer-contact-email-address)">
                <cbc:ElectronicMail>
                  <!-- bt-58 -->
                  <xsl:value-of select="ir:buyer/ir:buyer-contact/ir:buyer-contact-email-address"/>
                </cbc:ElectronicMail>
              </xsl:if>
            </cac:Contact>
          </xsl:if>
        </cac:Party>
      </cac:AccountingCustomerParty>
    </invoice:Invoice>
  </xsl:template>

  <xsl:template match="/ir:invoice" mode="credit-note">
    <credit-note:CreditNote xmlns:credit-note="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2">
    </credit-note:CreditNote>
  </xsl:template>
</xsl:stylesheet>
