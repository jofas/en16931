<xsl:stylesheet
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:ir="urn:todo"
    exclude-result-prefixes="xsl ir"
    version="1.0">
  <xsl:template match="/ir:invoice" mode="invoice">
    <invoice:Invoice xmlns:invoice="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2">
      <xsl:call-template name="common-invoice-1"/>
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
      <xsl:call-template name="common-invoice-notes"/>
      <xsl:call-template name="common-invoice-tax-point-date"/>
      <xsl:call-template name="common-invoice-2"/>
      <xsl:call-template name="common-invoice-originator-document-reference"/>
      <xsl:call-template name="common-invoice-contract-document-reference"/>
      <xsl:call-template name="common-invoice-additional-supporting-documents"/>
      <xsl:call-template name="common-invoice-invoiced-object-identifier"/>
      <xsl:if test="exists(ir:project-reference)">
        <cac:ProjectReference>
          <cbc:ID>
            <!-- bt-11 -->
            <xsl:value-of select="ir:project-reference"/>
          </cbc:ID>
        </cac:ProjectReference>
      </xsl:if>
      <xsl:call-template name="common-invoice-3"/>
      <xsl:if test="exists(ir:payment-terms)">
        <cac:PaymentTerms>
          <cbc:Note>
            <!-- bt-20 -->
            <xsl:value-of select="ir:payment-terms"/>
          </cbc:Note>
        </cac:PaymentTerms>
      </xsl:if>
      <xsl:call-template name="common-invoice-4"/>
      <xsl:for-each select="ir:invoice-lines/ir:invoice-line">
        <cac:InvoiceLine>
          <xsl:call-template name="common-invoice-line-1"/>
          <cbc:InvoicedQuantity>
            <xsl:attribute name="unitCode">
              <!-- bt-130 -->
              <xsl:value-of select="./ir:invoiced-quantity-unit-of-measure-code"/>
            </xsl:attribute>
            <!-- bt-129 -->
            <xsl:value-of select="./ir:invoiced-quantity"/>
          </cbc:InvoicedQuantity>
          <xsl:call-template name="common-invoice-line-2"/>
        </cac:InvoiceLine>
      </xsl:for-each>
    </invoice:Invoice>
  </xsl:template>

  <xsl:template match="/ir:invoice" mode="credit-note">
    <credit-note:CreditNote xmlns:credit-note="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2">
      <xsl:call-template name="common-invoice-1"/>
      <xsl:call-template name="common-invoice-tax-point-date"/>
      <cbc:CreditNoteTypeCode>
		<!-- bt-3 -->
        <xsl:value-of select="ir:invoice-type-code"/>
      </cbc:CreditNoteTypeCode>
      <xsl:call-template name="common-invoice-notes"/>
      <xsl:call-template name="common-invoice-2"/>
      <xsl:call-template name="common-invoice-contract-document-reference"/>
      <xsl:call-template name="common-invoice-additional-supporting-documents"/>
      <xsl:call-template name="common-invoice-invoiced-object-identifier"/>
      <xsl:if test="exists(ir:project-reference)">
        <cac:AdditionalDocumentReference>
          <cbc:ID>
            <!-- bt-11 -->
            <xsl:value-of select="ir:project-reference"/>
          </cbc:ID>
          <cbc:DocumentTypeCode>50</cbc:DocumentTypeCode>
        </cac:AdditionalDocumentReference>
      </xsl:if>
      <xsl:call-template name="common-invoice-originator-document-reference"/>
      <xsl:call-template name="common-invoice-3"/>
      <xsl:if test="exists(ir:payment-terms)
          or exists(ir:payment-due-date)">
        <cac:PaymentTerms>
          <xsl:if test="exists(ir:payment-terms)">
            <cbc:Note>
              <!-- bt-20 -->
              <xsl:value-of select="ir:payment-terms"/>
            </cbc:Note>
          </xsl:if>
          <xsl:if test="exists(ir:payment-due-date)">
            <cbc:PaymentDueDate>
              <!-- bt-9 -->
              <xsl:value-of select="ir:payment-due-date"/>
            </cbc:PaymentDueDate>
          </xsl:if>
        </cac:PaymentTerms>
      </xsl:if>
      <xsl:call-template name="common-invoice-4"/>
      <xsl:for-each select="ir:invoice-lines/ir:invoice-line">
        <cac:CreditNoteLine>
          <xsl:call-template name="common-invoice-line-1"/>
          <cbc:CreditedQuantity>
            <xsl:attribute name="unitCode">
              <!-- bt-130 -->
              <xsl:value-of select="./ir:invoiced-quantity-unit-of-measure-code"/>
            </xsl:attribute>
            <!-- bt-129 -->
            <xsl:value-of select="./ir:invoiced-quantity"/>
          </cbc:CreditedQuantity>
          <xsl:call-template name="common-invoice-line-2"/>
        </cac:CreditNoteLine>
      </xsl:for-each>
    </credit-note:CreditNote>
  </xsl:template>

  <xsl:template name="common-invoice-1">
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
  </xsl:template>

  <xsl:template name="common-invoice-notes">
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
  </xsl:template>

  <xsl:template name="common-invoice-tax-point-date">
    <xsl:if test="exists(ir:value-added-tax-point-date)">
      <cbc:TaxPointDate>
        <!-- bt-7 -->
        <xsl:value-of select="ir:value-added-tax-point-date"/>
      </cbc:TaxPointDate>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-2">
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
        or exists(ir:delivery-information/ir:invoicing-period/ir:invoicing-period-end-date)
        or exists(ir:value-added-tax-point-date-code)">
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
        <xsl:if test="exists(ir:value-added-tax-point-date-code)">
          <cbc:DescriptionCode>
            <!-- bt-8 -->
            <xsl:value-of select="ir:value-added-tax-point-date-code"/>
          </cbc:DescriptionCode>
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
  </xsl:template>

  <xsl:template name="common-invoice-originator-document-reference">
    <xsl:if test="exists(ir:tender-or-lot-reference)">
      <cac:OriginatorDocumentReference>
        <cbc:ID>
          <!-- bt-17 -->
          <xsl:value-of select="ir:tender-or-lot-reference"/>
        </cbc:ID>
      </cac:OriginatorDocumentReference>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-contract-document-reference">
    <xsl:if test="exists(ir:contract-reference)">
      <cac:ContractDocumentReference>
        <cbc:ID>
          <!-- bt-12 -->
          <xsl:value-of select="ir:contract-reference"/>
        </cbc:ID>
      </cac:ContractDocumentReference>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-additional-supporting-documents">
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
                <xsl:value-of select="./ir:attached-document/ir:content"/>
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
  </xsl:template>

  <xsl:template name="common-invoice-invoiced-object-identifier">
    <xsl:if test="exists(ir:invoiced-object-identifier)">
      <cac:AdditionalDocumentReference>
        <cbc:ID>
          <!-- bt-18 -->
          <xsl:value-of select="ir:invoiced-object-identifier"/>
        </cbc:ID>
        <cbc:DocumentTypeCode>130</cbc:DocumentTypeCode>
      </cac:AdditionalDocumentReference>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-3">
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
        <xsl:if test="exists(ir:payment-instructions/ir:direct-debit)">
          <cac:PartyIdentification>
            <cbc:ID schemeID="SEPA">
              <!-- bt-90 -->
              <xsl:value-of select="ir:payment-instructions/ir:direct-debit/ir:bank-assigned-creditor-identifier/ir:content"/>
            </cbc:ID>
          </cac:PartyIdentification>
        </xsl:if>
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
    <xsl:if test="exists(ir:payee)">
      <cac:PayeeParty>
        <xsl:if test="exists(ir:payee/ir:payee-identifier)">
          <cac:PartyIdentification>
            <cbc:ID>
              <xsl:if test="exists(ir:payee/ir:payee-identifier/ir:scheme-identifier)">
                <xsl:attribute name="schemeID">
                  <!-- bt-60-1 -->
                  <xsl:value-of select="ir:payee/ir:payee-identifier/ir:scheme-identifier"/>
                </xsl:attribute>
              </xsl:if>
              <!-- bt-60 -->
              <xsl:value-of select="ir:payee/ir:payee-identifier/ir:content"/>
            </cbc:ID>
          </cac:PartyIdentification>
        </xsl:if>
        <cac:PartyName>
          <cbc:Name>
            <!-- bt-59 -->
            <xsl:value-of select="ir:payee/ir:payee-name"/>
          </cbc:Name>
        </cac:PartyName>
        <xsl:if test="exists(ir:payee/ir:payee-legal-registration-identifier)">
          <cac:PartyLegalEntity>
            <cbc:CompanyID>
              <xsl:if test="exists(ir:payee/ir:payee-legal-registration-identifier/ir:scheme-identifier)">
                <xsl:attribute name="schemeID">
                  <!-- bt-61-1 -->
                  <xsl:value-of select="ir:payee/ir:payee-legal-registration-identifier/ir:scheme-identifier"/>
                </xsl:attribute>
              </xsl:if>
              <!-- bt-61 -->
              <xsl:value-of select="ir:payee/ir:payee-legal-registration-identifier/ir:content"/>
            </cbc:CompanyID>
          </cac:PartyLegalEntity>
        </xsl:if>
      </cac:PayeeParty>
    </xsl:if>
    <xsl:if test="exists(ir:seller-tax-representative-party)">
      <cac:TaxRepresentativeParty>
        <cac:PartyName>
          <cbc:Name>
            <!-- bt-62 -->
            <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-name"/>
          </cbc:Name>
        </cac:PartyName>
        <cac:PostalAddress>
          <xsl:if test="exists(ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-address-line-1)">
            <cbc:StreetName>
              <!-- bt-64 -->
              <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-address-line-1"/>
            </cbc:StreetName>
          </xsl:if>
          <xsl:if test="exists(ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-address-line-2)">
            <cbc:AdditionalStreetName>
              <!-- bt-65 -->
              <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-address-line-2"/>
            </cbc:AdditionalStreetName>
          </xsl:if>
          <xsl:if test="exists(ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-city)">
            <cbc:CityName>
              <!-- bt-66 -->
              <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-city"/>
            </cbc:CityName>
          </xsl:if>
          <xsl:if test="exists(ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-post-code)">
            <cbc:PostalZone>
              <!-- bt-67 -->
              <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-post-code"/>
            </cbc:PostalZone>
          </xsl:if>
          <xsl:if test="exists(ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-country-subdivision)">
            <cbc:CountrySubentity>
              <!-- bt-68 -->
              <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-country-subdivision"/>
            </cbc:CountrySubentity>
          </xsl:if>
          <xsl:if test="exists(ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-address-line-3)">
            <cac:AddressLine>
              <cbc:Line>
                <!-- bt-164 -->
                <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-address-line-3"/>
              </cbc:Line>
            </cac:AddressLine>
          </xsl:if>
          <cac:Country>
            <cbc:IdentificationCode>
              <!-- bt-69 -->
              <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-postal-address/ir:tax-representative-country-code"/>
            </cbc:IdentificationCode>
          </cac:Country>
        </cac:PostalAddress>
        <cac:PartyTaxScheme>
          <cbc:CompanyID>
            <!-- bt-63 -->
            <xsl:value-of select="ir:seller-tax-representative-party/ir:seller-tax-representative-vat-identifier/ir:content"/>
          </cbc:CompanyID>
          <cac:TaxScheme>
            <cbc:ID>VAT</cbc:ID>
          </cac:TaxScheme>
        </cac:PartyTaxScheme>
      </cac:TaxRepresentativeParty>
    </xsl:if>
    <xsl:if test="exists(ir:delivery-information/ir:deliver-to-party-name)
        or exists(ir:delivery-information/ir:deliver-to-location-identifier)
        or exists(ir:delivery-information/ir:actual-delivery-date)
        or exists(ir:deliver-information/ir:deliver-to-address)">
      <cac:Delivery>
        <xsl:if test="exists(ir:delivery-information/ir:actual-delivery-date)">
          <cbc:ActualDeliveryDate>
            <!-- bt-72 -->
            <xsl:value-of select="ir:delivery-information/ir:actual-delivery-date"/>
          </cbc:ActualDeliveryDate>
        </xsl:if>
        <xsl:if test="exists(ir:delivery-information/ir:deliver-to-location-identifier)
            or exists(ir:deliver-information/ir:deliver-to-address)">
          <cac:DeliveryLocation>
            <xsl:if test="exists(ir:delivery-information/ir:deliver-to-location-identifier)">
              <cbc:ID>
                <xsl:if test="exists(ir:delivery-information/ir:deliver-to-location-identifier/ir:scheme-identifier)">
                  <xsl:attribute name="schemeID">
                    <!-- bt-71-1 -->
                    <xsl:value-of select="ir:delivery-information/ir:deliver-to-location-identifier/ir:scheme-identifier"/>
                  </xsl:attribute>
                </xsl:if>
                <!-- bt-71 -->
                <xsl:value-of select="ir:delivery-information/ir:deliver-to-location-identifier/ir:content"/>
              </cbc:ID>
            </xsl:if>
            <xsl:if test="exists(ir:delivery-information/ir:deliver-to-address)">
              <cac:Address>
                <xsl:if test="exists(ir:delivery-information/ir:deliver-to-address/ir:deliver-to-address-line-1)">
                  <cbc:StreetName>
                    <!-- bt-75 -->
                    <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-address-line-1"/>
                  </cbc:StreetName>
                </xsl:if>
                <xsl:if test="exists(ir:delivery-information/ir:deliver-to-address/ir:deliver-to-address-line-2)">
                  <cbc:AdditionalStreetName>
                    <!-- bt-76 -->
                    <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-address-line-2"/>
                  </cbc:AdditionalStreetName>
                </xsl:if>
                <cbc:CityName>
                  <!-- bt-77 -->
                  <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-city"/>
                </cbc:CityName>
                <cbc:PostalZone>
                  <!-- bt-78 -->
                  <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-post-code"/>
                </cbc:PostalZone>
                <xsl:if test="exists(ir:delivery-information/ir:deliver-to-address/ir:deliver-to-country-subdivision)">
                  <cbc:CountrySubentity>
                    <!-- bt-79 -->
                    <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-country-subdivision"/>
                  </cbc:CountrySubentity>
                </xsl:if>
                <xsl:if test="exists(ir:delivery-information/ir:deliver-to-address/ir:deliver-to-address-line-3)">
                  <cac:AddressLine>
                    <cbc:Line>
                      <!-- bt-165 -->
                      <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-address-line-3"/>
                    </cbc:Line>
                  </cac:AddressLine>
                </xsl:if>
                <cac:Country>
                  <cbc:IdentificationCode>
                    <!-- bt-80 -->
                    <xsl:value-of select="ir:delivery-information/ir:deliver-to-address/ir:deliver-to-country-code"/>
                  </cbc:IdentificationCode>
                </cac:Country>
              </cac:Address>
            </xsl:if>
          </cac:DeliveryLocation>
        </xsl:if>
        <xsl:if test="exists(ir:delivery-information/ir:deliver-to-party-name)">
          <cac:DeliveryParty>
            <cac:PartyName>
              <cbc:Name>
                <!-- bt-70 -->
                <xsl:value-of select="ir:delivery-information/ir:deliver-to-party-name"/>
              </cbc:Name>
            </cac:PartyName>
          </cac:DeliveryParty>
        </xsl:if>
      </cac:Delivery>
    </xsl:if>
    <cac:PaymentMeans>
      <cbc:PaymentMeansCode>
        <xsl:if test="exists(ir:payment-instructions/ir:payment-means-text)">
          <xsl:attribute name="name">
            <!-- bt-82 -->
            <xsl:value-of select="ir:payment-instructions/ir:payment-means-text"/>
          </xsl:attribute>
        </xsl:if>
        <!-- bt-81 -->
        <xsl:value-of select="ir:payment-instructions/ir:payment-means-type-code"/>
      </cbc:PaymentMeansCode>
      <xsl:if test="exists(ir:payment-instructions/ir:remittance-information)">
        <cbc:PaymentID>
          <!-- bt-83 -->
          <xsl:value-of select="ir:payment-instructions/ir:remittance-information"/>
        </cbc:PaymentID>
      </xsl:if>
      <xsl:if test="exists(ir:payment-instructions/ir:payment-card-information)">
        <cac:CardAccount>
          <cbc:PrimaryAccountNumberID>
            <!-- bt-87 -->
            <xsl:value-of select="ir:payment-instructions/ir:payment-card-information/ir:payment-card-primary-account-number"/>
          </cbc:PrimaryAccountNumberID>
          <cbc:NetworkID>required-but-unmapped-field</cbc:NetworkID>
          <xsl:if test="exists(ir:payment-instructions/ir:payment-card-information/ir:payment-card-holder-name)">
            <cbc:HolderName>
              <!-- bt-88 -->
              <xsl:value-of select="ir:payment-instructions/ir:payment-card-information/ir:payment-card-holder-name"/>
            </cbc:HolderName>
          </xsl:if>
        </cac:CardAccount>
      </xsl:if>
      <xsl:if test="exists(ir:payment-instructions/ir:credit-transfers/ir:credit-transfer)">
        <cac:PayeeFinancialAccount>
          <cbc:ID>
            <!-- bt-84 -->
            <xsl:value-of select="ir:payment-instructions/ir:credit-transfers/ir:credit-transfer[1]/ir:payment-account-identifier"/>
          </cbc:ID>
          <xsl:if test="exists(ir:payment-instructions/ir:credit-transfers/ir:credit-transfer[1]/ir:payment-account-name)">
            <cbc:Name>
              <!-- bt-85 -->
              <xsl:value-of select="ir:payment-instructions/ir:credit-transfers/ir:credit-transfer[1]/ir:payment-account-name"/>
            </cbc:Name>
          </xsl:if>
          <xsl:if test="exists(ir:payment-instructions/ir:credit-transfers/ir:credit-transfer[1]/ir:payment-service-provider-identifier)">
            <cac:FinancialInstitutionBranch>
              <cbc:ID>
                <!-- bt-86 -->
                <xsl:value-of select="ir:payment-instructions/ir:credit-transfers/ir:credit-transfer[1]/ir:payment-service-provider-identifier"/>
              </cbc:ID>
            </cac:FinancialInstitutionBranch>
          </xsl:if>
        </cac:PayeeFinancialAccount>
      </xsl:if>
      <xsl:if test="exists(ir:payment-instructions/ir:direct-debit)">
        <cac:PaymentMandate>
          <cbc:ID>
            <!-- bt-89 -->
            <xsl:value-of select="ir:payment-instructions/ir:direct-debit/ir:mandate-reference-identifier/ir:content"/>
          </cbc:ID>
          <cac:PayerFinancialAccount>
            <cbc:ID>
              <!-- bt-91 -->
              <xsl:value-of select="ir:payment-instructions/ir:direct-debit/ir:debited-account-identifier/ir:content"/>
            </cbc:ID>
          </cac:PayerFinancialAccount>
        </cac:PaymentMandate>
      </xsl:if>
    </cac:PaymentMeans>
  </xsl:template>

  <xsl:template name="common-invoice-4">
    <xsl:for-each select="ir:document-level-charges/ir:document-level-charge">
      <cac:AllowanceCharge>
        <cbc:ChargeIndicator>true</cbc:ChargeIndicator>
        <xsl:if test="exists(./ir:document-level-charge-reason-code)">
          <cbc:AllowanceChargeReasonCode>
            <!-- bt-105 -->
            <xsl:value-of select="./ir:document-level-charge-reason-code"/>
          </cbc:AllowanceChargeReasonCode>
        </xsl:if>
        <xsl:if test="exists(./ir:document-level-charge-reason)">
          <cbc:AllowanceChargeReason>
            <!-- bt-104 -->
            <xsl:value-of select="./ir:document-level-charge-reason"/>
          </cbc:AllowanceChargeReason>
        </xsl:if>
        <xsl:if test="exists(./ir:document-level-charge-percentage)">
          <cbc:MultiplierFactorNumeric>
            <!-- bt-101 -->
            <xsl:value-of select="./ir:document-level-charge-percentage"/>
          </cbc:MultiplierFactorNumeric>
        </xsl:if>
        <cbc:Amount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-99 -->
          <xsl:value-of select="./ir:document-level-charge-amount"/>
        </cbc:Amount>
        <xsl:if test="exists(./ir:document-level-charge-base-amount)">
          <cbc:BaseAmount>
            <xsl:attribute name="currencyID">
              <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
            </xsl:attribute>
            <!-- bt-100 -->
            <xsl:value-of select="./ir:document-level-charge-base-amount"/>
          </cbc:BaseAmount>
        </xsl:if>
        <cac:TaxCategory>
          <cbc:ID>
            <!-- bt-102 -->
            <xsl:value-of select="./ir:document-level-charge-vat-category-code"/>
          </cbc:ID>
          <xsl:if test="exists(./ir:document-level-charge-vat-rate)">
            <cbc:Percent>
              <!-- bt-103 -->
              <xsl:value-of select="./ir:document-level-charge-vat-rate"/>
            </cbc:Percent>
          </xsl:if>
          <cac:TaxScheme>
            <cbc:ID>VAT</cbc:ID>
          </cac:TaxScheme>
        </cac:TaxCategory>
      </cac:AllowanceCharge>
    </xsl:for-each>
    <xsl:for-each select="ir:document-level-allowances/ir:document-level-allowance">
      <cac:AllowanceCharge>
        <cbc:ChargeIndicator>false</cbc:ChargeIndicator>
        <xsl:if test="exists(./ir:document-level-allowance-reason-code)">
          <cbc:AllowanceChargeReasonCode>
            <!-- bt-94 -->
            <xsl:value-of select="./ir:document-level-allowance-reason-code"/>
          </cbc:AllowanceChargeReasonCode>
        </xsl:if>
        <xsl:if test="exists(./ir:document-level-allowance-reason)">
          <cbc:AllowanceChargeReason>
            <!-- bt-93 -->
            <xsl:value-of select="./ir:document-level-allowance-reason"/>
          </cbc:AllowanceChargeReason>
        </xsl:if>
        <xsl:if test="exists(./ir:document-level-allowance-percentage)">
          <cbc:MultiplierFactorNumeric>
            <!-- bt-92 -->
            <xsl:value-of select="./ir:document-level-allowance-percentage"/>
          </cbc:MultiplierFactorNumeric>
        </xsl:if>
        <cbc:Amount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-98 -->
          <xsl:value-of select="./ir:document-level-allowance-amount"/>
        </cbc:Amount>
        <xsl:if test="exists(./ir:document-level-allowance-base-amount)">
          <cbc:BaseAmount>
            <xsl:attribute name="currencyID">
              <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
            </xsl:attribute>
            <!-- bt-97 -->
            <xsl:value-of select="./ir:document-level-allowance-base-amount"/>
          </cbc:BaseAmount>
        </xsl:if>
        <cac:TaxCategory>
          <cbc:ID>
            <!-- bt-95 -->
            <xsl:value-of select="./ir:document-level-allowance-vat-category-code"/>
          </cbc:ID>
          <xsl:if test="exists(./ir:document-level-allowance-vat-rate)">
            <cbc:Percent>
              <!-- bt-96 -->
              <xsl:value-of select="./ir:document-level-allowance-vat-rate"/>
            </cbc:Percent>
          </xsl:if>
          <cac:TaxScheme>
            <cbc:ID>VAT</cbc:ID>
          </cac:TaxScheme>
        </cac:TaxCategory>
      </cac:AllowanceCharge>
    </xsl:for-each>
    <cac:TaxTotal>
      <cbc:TaxAmount>
        <xsl:attribute name="currencyID">
          <xsl:value-of select="ir:invoice-currency-code"/>
        </xsl:attribute>
        <xsl:choose>
          <xsl:when test="exists(ir:document-totals/ir:invoice-total-vat-amount)">
            <!-- bt-110 -->
            <xsl:value-of select="ir:document-totals/ir:invoice-total-vat-amount"/>
          </xsl:when>
          <!-- If bt-110 is not present, we still need to put a value here to satisfy the UBL schema -->
          <xsl:otherwise>
            <xsl:text>0.00</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </cbc:TaxAmount>
      <xsl:for-each select="ir:vat-breakdown/ir:vat-breakdown">
        <cac:TaxSubtotal>
          <cbc:TaxableAmount>
            <xsl:attribute name="currencyID">
              <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
            </xsl:attribute>
            <!-- bt-116 -->
            <xsl:value-of select="./ir:vat-category-taxable-amount"/>
          </cbc:TaxableAmount>
          <cbc:TaxAmount>
            <xsl:attribute name="currencyID">
              <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
            </xsl:attribute>
            <!-- bt-117 -->
            <xsl:value-of select="./ir:vat-category-tax-amount"/>
          </cbc:TaxAmount>
          <cac:TaxCategory>
            <cbc:ID>
              <!-- bt-118 -->
              <xsl:value-of select="./ir:vat-category-code"/>
            </cbc:ID>
            <cbc:Percent>
              <!-- bt-119 -->
              <xsl:value-of select="./ir:vat-category-rate"/>
            </cbc:Percent>
            <xsl:if test="exists(./ir:vat-exemption-reason-code)">
              <cbc:TaxExemptionReasonCode>
                <!-- bt-121 -->
                <xsl:value-of select="./ir:vat-exemption-reason-code"/>
              </cbc:TaxExemptionReasonCode>
            </xsl:if>
            <xsl:if test="exists(./ir:vat-exemption-reason-text)">
              <cbc:TaxExemptionReason>
                <!-- bt-120 -->
                <xsl:value-of select="./ir:vat-exemption-reason-text"/>
              </cbc:TaxExemptionReason>
            </xsl:if>
            <cac:TaxScheme>
              <cbc:ID>VAT</cbc:ID>
            </cac:TaxScheme>
          </cac:TaxCategory>
        </cac:TaxSubtotal>
      </xsl:for-each>
    </cac:TaxTotal>
    <xsl:if test="exists(ir:document-totals/ir:invoice-total-vat-amount-in-accounting-currency)
        and exists(ir:vat-accounting-currency-code)">
      <cac:TaxTotal>
        <cbc:TaxAmount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="ir:vat-accounting-currency-code"/>
          </xsl:attribute>
          <!-- bt-111 -->
          <xsl:value-of select="ir:document-totals/ir:invoice-total-vat-amount-in-accounting-currency"/>
        </cbc:TaxAmount>
      </cac:TaxTotal>
    </xsl:if>
    <cac:LegalMonetaryTotal>
      <cbc:LineExtensionAmount>
        <xsl:attribute name="currencyID">
          <xsl:value-of select="ir:invoice-currency-code"/>
        </xsl:attribute>
        <!-- bt-106 -->
        <xsl:value-of select="ir:document-totals/ir:sum-of-invoice-line-net-amount"/>
      </cbc:LineExtensionAmount>
      <cbc:TaxExclusiveAmount>
        <xsl:attribute name="currencyID">
          <xsl:value-of select="ir:invoice-currency-code"/>
        </xsl:attribute>
        <!-- bt-109 -->
        <xsl:value-of select="ir:document-totals/ir:invoice-total-amount-without-vat"/>
      </cbc:TaxExclusiveAmount>
      <cbc:TaxInclusiveAmount>
        <xsl:attribute name="currencyID">
          <xsl:value-of select="ir:invoice-currency-code"/>
        </xsl:attribute>
        <!-- bt-112 -->
        <xsl:value-of select="ir:document-totals/ir:invoice-total-amount-with-vat"/>
      </cbc:TaxInclusiveAmount>
      <xsl:if test="exists(ir:document-totals/ir:sum-of-allowances-on-document-level)">
        <cbc:AllowanceTotalAmount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-107 -->
          <xsl:value-of select="ir:document-totals/ir:sum-of-allowances-on-document-level"/>
        </cbc:AllowanceTotalAmount>
      </xsl:if>
      <xsl:if test="exists(ir:document-totals/ir:sum-of-charges-on-document-level)">
        <cbc:ChargeTotalAmount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-108 -->
          <xsl:value-of select="ir:document-totals/ir:sum-of-charges-on-document-level"/>
        </cbc:ChargeTotalAmount>
      </xsl:if>
      <xsl:if test="exists(ir:document-totals/ir:paid-amount)">
        <cbc:PrepaidAmount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-113 -->
          <xsl:value-of select="ir:document-totals/ir:paid-amount"/>
        </cbc:PrepaidAmount>
      </xsl:if>
      <xsl:if test="exists(ir:document-totals/ir:rounding-amount)">
        <cbc:PayableRoundingAmount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-114 -->
          <xsl:value-of select="ir:document-totals/ir:rounding-amount"/>
        </cbc:PayableRoundingAmount>
      </xsl:if>
      <cbc:PayableAmount>
        <xsl:attribute name="currencyID">
          <xsl:value-of select="ir:invoice-currency-code"/>
        </xsl:attribute>
        <!-- bt-115 -->
        <xsl:value-of select="ir:document-totals/ir:amount-due-for-payment"/>
      </cbc:PayableAmount>
    </cac:LegalMonetaryTotal>
  </xsl:template>

  <xsl:template name="common-invoice-line-1">
    <cbc:ID>
      <!-- bt-126 -->
      <xsl:value-of select="./ir:invoice-line-identifier/ir:content"/>
    </cbc:ID>
    <xsl:if test="exists(./ir:invoice-line-note)">
      <cbc:Note>
        <!-- bt-127 -->
        <xsl:value-of select="./ir:invoice-line-note"/>
      </cbc:Note>
    </xsl:if>
  </xsl:template>

  <xsl:template name="common-invoice-line-2">
    <cbc:LineExtensionAmount>
      <xsl:attribute name="currencyID">
        <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
      </xsl:attribute>
      <!-- bt-131 -->
      <xsl:value-of select="./ir:invoice-line-net-amount"/>
    </cbc:LineExtensionAmount>
    <xsl:if test="exists(./ir:invoice-line-buyer-accounting-reference)">
      <cbc:AccountingCost>
        <!-- bt-133 -->
        <xsl:value-of select="./ir:invoice-line-buyer-accounting-reference"/>
      </cbc:AccountingCost>
    </xsl:if>
    <xsl:if test="exists(./ir:invoice-line-period/ir:invoice-line-period-start-date)
        or exists(./ir:invoice-line-period/ir:invoice-line-period-end-date)">
      <cac:InvoicePeriod>
        <xsl:if test="exists(./ir:invoice-line-period/ir:invoice-line-period-start-date)">
          <cbc:StartDate>
            <!-- bt-134 -->
            <xsl:value-of select="./ir:invoice-line-period/ir:invoice-line-period-start-date"/>
          </cbc:StartDate>
        </xsl:if>
        <xsl:if test="exists(./ir:invoice-line-period/ir:invoice-line-period-end-date)">
          <cbc:EndDate>
            <!-- bt-135 -->
            <xsl:value-of select="./ir:invoice-line-period/ir:invoice-line-period-end-date"/>
          </cbc:EndDate>
        </xsl:if>
      </cac:InvoicePeriod>
    </xsl:if>
    <xsl:if test="exists(./ir:referenced-purchase-order-line-reference)">
      <cac:OrderLineReference>
        <cbc:LineID>
          <!-- bt-132 -->
          <xsl:value-of select="./ir:referenced-purchase-order-line-reference"/>
        </cbc:LineID>
      </cac:OrderLineReference>
    </xsl:if>
    <xsl:if test="exists(./ir:invoice-line-object-identifier)">
      <cac:DocumentReference>
        <cbc:ID>
          <xsl:if test="exists(./ir:invoice-line-object-identifier/ir:scheme-identifier)">
            <xsl:attribute name="schemeID">
              <!-- bt-128-1 -->
              <xsl:value-of select="./ir:invoice-line-object-identifier/ir:scheme-identifier"/>
            </xsl:attribute>
          </xsl:if>
          <!-- bt-128 -->
          <xsl:value-of select="./ir:invoice-line-object-identifier/ir:content"/>
        </cbc:ID>
        <cbc:DocumentTypeCode>130</cbc:DocumentTypeCode>
      </cac:DocumentReference>
    </xsl:if>
    <xsl:for-each select="./ir:invoice-line-charges/ir:invoice-line-charge">
      <cac:AllowanceCharge>
        <cbc:ChargeIndicator>true</cbc:ChargeIndicator>
        <xsl:if test="exists(./ir:invoice-line-charge-reason-code)">
          <cbc:AllowanceChargeReasonCode>
            <!-- bt-145 -->
            <xsl:value-of select="./ir:invoice-line-charge-reason-code"/>
          </cbc:AllowanceChargeReasonCode>
        </xsl:if>
        <xsl:if test="exists(./ir:invoice-line-charge-reason)">
          <cbc:AllowanceChargeReason>
            <!-- bt-144 -->
            <xsl:value-of select="./ir:invoice-line-charge-reason"/>
          </cbc:AllowanceChargeReason>
        </xsl:if>
        <xsl:if test="exists(./ir:invoice-line-charge-percentage)">
          <cbc:MultiplierFactorNumeric>
            <!-- bt-143 -->
            <xsl:value-of select="./ir:invoice-line-charge-percentage"/>
          </cbc:MultiplierFactorNumeric>
        </xsl:if>
        <cbc:Amount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-141 -->
          <xsl:value-of select="./ir:invoice-line-charge-amount"/>
        </cbc:Amount>
        <xsl:if test="exists(./ir:invoice-line-charge-base-amount)">
          <cbc:BaseAmount>
            <xsl:attribute name="currencyID">
              <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
            </xsl:attribute>
            <!-- bt-142 -->
            <xsl:value-of select="./ir:invoice-line-charge-base-amount"/>
          </cbc:BaseAmount>
        </xsl:if>
      </cac:AllowanceCharge>
    </xsl:for-each>
    <xsl:for-each select="./ir:invoice-line-allowances/ir:invoice-line-allowance">
      <cac:AllowanceCharge>
        <cbc:ChargeIndicator>false</cbc:ChargeIndicator>
        <xsl:if test="exists(./ir:invoice-line-allowance-reason-code)">
          <cbc:AllowanceChargeReasonCode>
            <!-- bt-140 -->
            <xsl:value-of select="./ir:invoice-line-allowance-reason-code"/>
          </cbc:AllowanceChargeReasonCode>
        </xsl:if>
        <xsl:if test="exists(./ir:invoice-line-allowance-reason)">
          <cbc:AllowanceChargeReason>
            <!-- bt-139 -->
            <xsl:value-of select="./ir:invoice-line-allowance-reason"/>
          </cbc:AllowanceChargeReason>
        </xsl:if>
        <xsl:if test="exists(./ir:invoice-line-allowance-percentage)">
          <cbc:MultiplierFactorNumeric>
            <!-- bt-138 -->
            <xsl:value-of select="./ir:invoice-line-allowance-percentage"/>
          </cbc:MultiplierFactorNumeric>
        </xsl:if>
        <cbc:Amount>
          <xsl:attribute name="currencyID">
            <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
          </xsl:attribute>
          <!-- bt-136 -->
          <xsl:value-of select="./ir:invoice-line-allowance-amount"/>
        </cbc:Amount>
        <xsl:if test="exists(./ir:invoice-line-allowance-base-amount)">
          <cbc:BaseAmount>
            <xsl:attribute name="currencyID">
              <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
            </xsl:attribute>
            <!-- bt-137 -->
            <xsl:value-of select="./ir:invoice-line-allowance-base-amount"/>
          </cbc:BaseAmount>
        </xsl:if>
      </cac:AllowanceCharge>
    </xsl:for-each>
    <cac:Item>
      <xsl:if test="exists(./ir:item-information/ir:item-description)">
        <cbc:Description>
          <!-- bt-154 -->
          <xsl:value-of select="./ir:item-information/ir:item-description"/>
        </cbc:Description>
      </xsl:if>
      <cbc:Name>
        <!-- bt-153 -->
        <xsl:value-of select="./ir:item-information/ir:item-name"/>
      </cbc:Name>
      <xsl:if test="exists(./ir:item-information/ir:item-buyers-identifier)">
        <cac:BuyersItemIdentification>
          <cbc:ID>
            <!-- bt-156 -->
            <xsl:value-of select="./ir:item-information/ir:item-buyers-identifier/ir:content"/>
          </cbc:ID>
        </cac:BuyersItemIdentification>
      </xsl:if>
      <xsl:if test="exists(./ir:item-information/ir:item-sellers-identifier)">
        <cac:SellersItemIdentification>
          <cbc:ID>
            <!-- bt-155 -->
            <xsl:value-of select="./ir:item-information/ir:item-sellers-identifier/ir:content"/>
          </cbc:ID>
        </cac:SellersItemIdentification>
      </xsl:if>
      <xsl:if test="exists(./ir:item-information/ir:item-standard-identifier)">
        <cac:StandardItemIdentification>
          <cbc:ID>
            <xsl:attribute name="schemeID">
              <!-- bt-157-1 -->
              <xsl:value-of select="./ir:item-information/ir:item-standard-identifier/ir:scheme-identifier"/>
            </xsl:attribute>
            <!-- bt-157 -->
            <xsl:value-of select="./ir:item-information/ir:item-standard-identifier/ir:content"/>
          </cbc:ID>
        </cac:StandardItemIdentification>
      </xsl:if>
      <xsl:if test="exists(./ir:item-information/ir:item-country-of-origin)">
        <cac:OriginCountry>
          <cbc:IdentificationCode>
            <!-- bt-159 -->
            <xsl:value-of select="./ir:item-information/ir:item-country-of-origin"/>
          </cbc:IdentificationCode>
        </cac:OriginCountry>
      </xsl:if>
      <xsl:for-each select="./ir:item-information/ir:item-classification-identifiers/ir:item-classification-identifier">
        <cac:CommodityClassification>
          <cbc:ItemClassificationCode>
            <xsl:attribute name="listID">
              <!-- bt-158-1 -->
              <xsl:value-of select="./ir:scheme-identifier"/>
            </xsl:attribute>
            <xsl:if test="exists(./ir:scheme-version-identifier)">
              <xsl:attribute name="listVersionID">
                <!-- bt-158-2 -->
                <xsl:value-of select="./ir:scheme-version-identifier"/>
              </xsl:attribute>
            </xsl:if>
            <!-- bt-158 -->
            <xsl:value-of select="./ir:content"/>
          </cbc:ItemClassificationCode>
        </cac:CommodityClassification>
      </xsl:for-each>
      <cac:ClassifiedTaxCategory>
        <cbc:ID>
          <!-- bt-151 -->
          <xsl:value-of select="./ir:line-vat-information/ir:invoiced-item-vat-category-code"/>
        </cbc:ID>
        <xsl:if test="exists(./ir:line-vat-information/ir:invoiced-item-vat-rate)">
          <cbc:Percent>
            <!-- bt-152 -->
            <xsl:value-of select="./ir:line-vat-information/ir:invoiced-item-vat-rate"/>
          </cbc:Percent>
        </xsl:if>
        <cac:TaxScheme>
          <cbc:ID>VAT</cbc:ID>
        </cac:TaxScheme>
      </cac:ClassifiedTaxCategory>
      <xsl:for-each select="./ir:item-information/ir:item-attributes/ir:item-attribute">
        <cac:AdditionalItemProperty>
          <cbc:Name>
            <!-- bt-160 -->
            <xsl:value-of select="./ir:item-attribute-name"/>
          </cbc:Name>
          <cbc:Value>
            <!-- bt-161 -->
            <xsl:value-of select="./ir:item-attribute-value"/>
          </cbc:Value>
        </cac:AdditionalItemProperty>
      </xsl:for-each>
    </cac:Item>
    <cac:Price>
      <cbc:PriceAmount>
        <xsl:attribute name="currencyID">
          <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
        </xsl:attribute>
        <!-- bt-146 -->
        <xsl:value-of select="./ir:price-details/ir:item-net-price"/>
      </cbc:PriceAmount>
      <xsl:if test="exists(./ir:price-details/ir:item-price-base-quantity)">
        <cbc:BaseQuantity>
          <xsl:if test="exists(./ir:price-details/ir:item-price-base-quantity-unit-of-measure-code)">
            <xsl:attribute name="unitCode">
              <!-- bt-150 -->
              <xsl:value-of select="./ir:price-details/ir:item-price-base-quantity-unit-of-measure-code"/>
            </xsl:attribute>
          </xsl:if>
          <!-- bt-149 -->
          <xsl:value-of select="./ir:price-details/ir:item-price-base-quantity"/>
        </cbc:BaseQuantity>
      </xsl:if>
      <xsl:if test="exists(./ir:price-details/ir:item-price-discount)
          or exists(./ir:price-details/ir:item-gross-price)">
        <cac:AllowanceCharge>
          <cbc:ChargeIndicator>false</cbc:ChargeIndicator>
          <xsl:if test="exists(./ir:price-details/ir:item-price-discount)">
            <cbc:Amount>
              <xsl:attribute name="currencyID">
                <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
              </xsl:attribute>
              <!-- bt-147 -->
              <xsl:value-of select="./ir:price-details/ir:item-price-discount"/>
            </cbc:Amount>
          </xsl:if>
          <xsl:if test="exists(./ir:price-details/ir:item-gross-price)">
            <cbc:BaseAmount>
              <xsl:attribute name="currencyID">
                <xsl:value-of select="/ir:invoice/ir:invoice-currency-code"/>
              </xsl:attribute>
              <!-- bt-148 -->
              <xsl:value-of select="./ir:price-details/ir:item-gross-price"/>
            </cbc:BaseAmount>
          </xsl:if>
        </cac:AllowanceCharge>
      </xsl:if>
    </cac:Price>
  </xsl:template>
</xsl:stylesheet>
