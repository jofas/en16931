<xsl:stylesheet
    xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    version="1.0">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="ubl:Invoice">
    <xsl:element name="invoice">
      <xsl:element name="invoice-number">
        <xsl:attribute name="id">bt-1</xsl:attribute>
        <xsl:element name="content">
          <xsl:value-of select="cbc:ID"/>
        </xsl:element>
      </xsl:element>
      <xsl:element name="invoice-issue-date">
        <xsl:attribute name="id">bt-2</xsl:attribute>
        <xsl:value-of select="cbc:IssueDate"/>
      </xsl:element>
      <xsl:element name="invoice-type-code">
        <xsl:attribute name="id">bt-3</xsl:attribute>
        <xsl:value-of select="cbc:InvoiceTypeCode"/>
      </xsl:element>
      <xsl:element name="invoice-currency-code">
        <xsl:attribute name="id">bt-5</xsl:attribute>
        <xsl:value-of select="cbc:DocumentCurrencyCode"/>
      </xsl:element>
      <xsl:if test="exists(cbc:TaxCurrencyCode)">
        <xsl:element name="vat-accounting-currency-code">
          <xsl:attribute name="id">bt-6</xsl:attribute>
          <xsl:value-of select="cbc:TaxCurrencyCode"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:TaxPointDate)">
        <xsl:element name="value-added-tax-point-date">
          <xsl:attribute name="id">bt-7</xsl:attribute>
          <xsl:value-of select="cbc:TaxPointDate"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:InvoicePeriod/cbc:DescriptionCode)">
        <xsl:element name="value-added-tax-point-date-code">
          <xsl:attribute name="id">bt-8</xsl:attribute>
          <xsl:value-of select="cac:InvoicePeriod/cbc:DescriptionCode"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:DueDate)">
        <xsl:element name="payment-due-date">
          <xsl:attribute name="id">bt-9</xsl:attribute>
          <xsl:value-of select="cbc:DueDate"/>
        </xsl:element>
      </xsl:if>
      <xsl:element name="buyer-reference">
        <xsl:attribute name="id">bt-10</xsl:attribute>
        <xsl:value-of select="cbc:BuyerReference"/>
      </xsl:element>
      <xsl:if test="exists(cac:ProjectReference/cbc:ID)">
        <xsl:element name="project-reference">
          <xsl:attribute name="id">bt-11</xsl:attribute>
          <xsl:value-of select="cac:ProjectReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:ContractDocumentReference/cbc:ID)">
        <xsl:element name="contract-reference">
          <xsl:attribute name="id">bt-12</xsl:attribute>
          <xsl:value-of select="cac:ContractDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:OrderReference/cbc:ID)">
        <xsl:element name="purchase-order-reference">
          <xsl:attribute name="id">bt-13</xsl:attribute>
          <xsl:value-of select="cac:OrderReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:OrderReference/cbc:SalesOrderID)">
        <xsl:element name="sales-order-reference">
          <xsl:attribute name="id">bt-14</xsl:attribute>
          <xsl:value-of select="cac:OrderReference/cbc:SalesOrderID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:ReceiptDocumentReference/cbc:ID)">
        <xsl:element name="receiving-advice-reference">
          <xsl:attribute name="id">bt-15</xsl:attribute>
          <xsl:value-of select="cac:ReceiptDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:DespatchDocumentReference/cbc:ID)">
        <xsl:element name="despatch-advice-reference">
          <xsl:attribute name="id">bt-16</xsl:attribute>
          <xsl:value-of select="cac:DespatchDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:OriginatorDocumentReference/cbc:ID)">
        <xsl:element name="tender-or-lot-reference">
          <xsl:attribute name="id">bt-17</xsl:attribute>
          <xsl:value-of select="cac:OriginatorDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID)">
        <xsl:element name="invoiced-object-identifier">
          <xsl:attribute name="id">bt-18</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID"/>
          </xsl:element>
          <xsl:element name="scheme-identifier">
            <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID/@schemeID"/>
          </xsl:element>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:AccountingCost)">
        <xsl:element name="buyer-accounting-reference">
          <xsl:attribute name="id">bt-19</xsl:attribute>
          <xsl:value-of select="cbc:AccountingCost"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:PaymentTerms/cbc:Note)">
        <xsl:element name="payment-terms">
          <xsl:attribute name="id">bt-20</xsl:attribute>
          <xsl:value-of select="cac:PaymentTerms/cbc:Note"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:Note)">
        <xsl:element name="invoice-notes">
          <xsl:attribute name="id">bg-1</xsl:attribute>
          <xsl:for-each select="cbc:Note">
            <xsl:element name="invoice-note">
              <xsl:attribute name="id">bg-1</xsl:attribute>
              <xsl:choose>
                <xsl:when test="contains(., '#') and string-length(substring-before(substring-after(., '#'), '#')) = 3">
                  <xsl:element name="invoice-note-subject-code">
                    <xsl:attribute name="id">bt-21</xsl:attribute>
                    <xsl:value-of select="substring-before(substring-after(., '#'), '#')"/>
                  </xsl:element>
                  <xsl:element name="invoice-note">
                    <xsl:attribute name="id">bt-22</xsl:attribute>
                    <xsl:value-of select="substring-after(substring-after(., '#'), '#')"/>
                  </xsl:element>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:element name="invoice-note">
                    <xsl:attribute name="id">bt-21</xsl:attribute>
                    <xsl:value-of select="."/>
                  </xsl:element>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
      </xsl:if>
      <xsl:element name="process-control">
        <xsl:attribute name="id">bg-2</xsl:attribute>
        <xsl:element name="business-process-type">
          <xsl:attribute name="id">bt-23</xsl:attribute>
          <xsl:value-of select="cbc:ProfileID"/>
        </xsl:element>
        <xsl:element name="specification-identifier">
          <xsl:attribute name="id">bt-24</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cbc:CustomizationID"/>
          </xsl:element>
        </xsl:element>
      </xsl:element>
      <xsl:if test="cac:BillingReference">
        <xsl:element name="preceding-invoice-references">
          <xsl:attribute name="id">bg-3</xsl:attribute>
          <xsl:for-each select="cac:BillingReference">
            <xsl:element name="preceding-invoice-reference">
              <xsl:attribute name="id">bg-3</xsl:attribute>
              <xsl:element name="preceding-invoice-reference">
                <xsl:attribute name="id">bt-25</xsl:attribute>
                <xsl:value-of select="./cac:InvoiceDocumentReference/cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(./cac:InvoiceDocumentReference/cbc:IssueDate)">
                <xsl:element name="preceding-invoice-issue-date">
                  <xsl:attribute name="id">bt-26</xsl:attribute>
                  <xsl:value-of select="./cac:InvoiceDocumentReference/cbc:IssueDate"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
      </xsl:if>
      <xsl:element name="seller">
        <xsl:attribute name="id">bg-4</xsl:attribute>
        <xsl:element name="seller-name">
          <xsl:attribute name="id">bt-27</xsl:attribute>
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName"/>
        </xsl:element>
        <xsl:if test="cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name">
          <xsl:element name="seller-trading-name">
            <xsl:attribute name="id">bt-28</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[(normalize-space(upper-case(@schmeID)) != 'SEPA')])">
          <xsl:element name="seller-identifiers">
            <xsl:attribute name="id">bt-29</xsl:attribute>
            <xsl:for-each select="cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[(normalize-space(upper-case(@schmeID)) != 'SEPA')]">
              <xsl:element name="seller-identifier">
                <xsl:attribute name="id">bt-29</xsl:attribute>
                <xsl:element name="content">
                  <xsl:value-of select="./cbc:ID"/>
                </xsl:element>
                <xsl:if test="exists(./cbc:ID[@schemeID])">
                  <xsl:element name="scheme-identifier">
                    <xsl:value-of select="./cbc:ID/@schemeID"/>
                  </xsl:element>
                </xsl:if>
              </xsl:element>
            </xsl:for-each>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID)">
          <xsl:element name="seller-legal-registration-identifier">
            <xsl:attribute name="id">bt-30</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID"/>
            </xsl:element>
            <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID[@schemeID])">
              <xsl:element name="scheme-identifier">
                <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID/@schemeID]"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID)">
          <xsl:element name="seller-vat-identifier">
            <xsl:attribute name="id">bt-31</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
            </xsl:element>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) != 'VAT')]/cbc:CompanyID)">
          <xsl:element name="seller-tax-registration-identifier">
            <xsl:attribute name="id">bt-32</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) != 'VAT')]/cbc:CompanyID"/>
            </xsl:element>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyLegalForm)">
          <xsl:element name="seller-additional-legal-information">
            <xsl:attribute name="id">bt-33</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyLegalForm"/>
          </xsl:element>
        </xsl:if>
        <xsl:element name="seller-electronic-address">
          <xsl:attribute name="id">bt-34</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cbc:EndpointID"/>
          </xsl:element>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cbc:EndpointID[@schemeID])">
            <xsl:element name="scheme-identifier">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cbc:EndpointID/@schemeID]"/>
            </xsl:element>
          </xsl:if>
        </xsl:element>
        <!-- TODO:
        * seller-postal-address bg-5
          - seller-address-line-1 bt-35
          - seller-address-line-2 bt-36
          - seller-address-line-3 bt-162
          - seller-city bt-37
          - seller-post-code bt-38
          - seller-country-subdivision bt-39
          - seller-country-code bt-40
        -->
        <!-- TODO:
        * seller-contact bg-6
          - seller-contact-point bt-41
          - seller-contact-telephone-number bt-42
          - seller-contact-email-address bt-43
        -->
      </xsl:element>
      <xsl:element name="buyer">
        <xsl:attribute name="id">bg-7</xsl:attribute>
      </xsl:element>
      <xsl:if test="false()">
        <xsl:element name="payee">
          <xsl:attribute name="id">bg-10</xsl:attribute>
        </xsl:element>
      </xsl:if>
      <xsl:if test="false()">
        <xsl:element name="seller-tax-representative-party">
          <xsl:attribute name="id">bg-11</xsl:attribute>
        </xsl:element>
      </xsl:if>
      <xsl:if test="false()">
        <xsl:element name="delivery-information">
          <xsl:attribute name="id">bg-13</xsl:attribute>
        </xsl:element>
      </xsl:if>
      <xsl:element name="payment-instructions">
        <xsl:attribute name="id">bg-16</xsl:attribute>
      </xsl:element>
      <xsl:if test="false()">
        <xsl:element name="document-level-allowances">
          <xsl:attribute name="id">bg-20</xsl:attribute>
          <!-- list -->
        </xsl:element>
      </xsl:if>
      <xsl:if test="false()">
        <xsl:element name="document-level-charges">
          <xsl:attribute name="id">bg-21</xsl:attribute>
          <!-- list -->
        </xsl:element>
      </xsl:if>
      <xsl:element name="document-totals">
        <xsl:attribute name="id">bg-22</xsl:attribute>
      </xsl:element>
      <xsl:element name="vat-breakdown">
        <xsl:attribute name="id">bg-23</xsl:attribute>
        <!-- list -->
      </xsl:element>
      <xsl:if test="false()">
        <xsl:element name="additional-supporting-documents">
          <xsl:attribute name="id">bg-24</xsl:attribute>
          <!-- list -->
        </xsl:element>
        <xsl:element name="invoice-lines">
          <xsl:attribute name="id">bg-25</xsl:attribute>
          <!-- list -->
        </xsl:element>
      </xsl:if>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
