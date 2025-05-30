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
        <xsl:value-of select="cbc:ID"/>
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
      <xsl:element name="vat-accounting-currency-code">
        <xsl:attribute name="id">bt-6</xsl:attribute>
        <xsl:value-of select="cbc:TaxCurrencyCode"/>
      </xsl:element>
      <xsl:element name="value-added-tax-point-date">
        <xsl:attribute name="id">bt-7</xsl:attribute>
        <xsl:value-of select="cbc:TaxPointDate"/>
      </xsl:element>
      <xsl:element name="value-added-tax-point-date-code">
        <xsl:attribute name="id">bt-8</xsl:attribute>
        <xsl:value-of select="cac:InvoicePeriod/cbc:DescriptionCode"/>
      </xsl:element>
      <xsl:element name="payment-due-date">
        <xsl:attribute name="id">bt-9</xsl:attribute>
        <xsl:value-of select="cbc:DueDate"/>
      </xsl:element>
      <xsl:element name="buyer-reference">
        <xsl:attribute name="id">bt-10</xsl:attribute>
        <xsl:value-of select="cbc:BuyerReference"/>
      </xsl:element>
      <xsl:element name="project-reference">
        <xsl:attribute name="id">bt-11</xsl:attribute>
        <xsl:value-of select="cac:ProjectReference/cbc:ID"/>
      </xsl:element>
      <xsl:element name="contract-reference">
        <xsl:attribute name="id">bt-12</xsl:attribute>
        <xsl:value-of select="cac:ContractDocumentReference/cbc:ID"/>
      </xsl:element>
      <xsl:element name="purchase-order-reference">
        <xsl:attribute name="id">bt-13</xsl:attribute>
        <xsl:value-of select="cac:OrderReference/cbc:ID"/>
      </xsl:element>
      <xsl:element name="sales-order-reference">
        <xsl:attribute name="id">bt-14</xsl:attribute>
        <xsl:value-of select="cac:OrderReference/cbc:SalesOrderID"/>
      </xsl:element>
      <xsl:element name="receiving-advice-reference">
        <xsl:attribute name="id">bt-15</xsl:attribute>
        <xsl:value-of select="cac:ReceiptDocumentReference/cbc:ID"/>
      </xsl:element>
      <xsl:element name="despatch-advice-reference">
        <xsl:attribute name="id">bt-16</xsl:attribute>
        <xsl:value-of select="cac:DespatchDocumentReference/cbc:ID"/>
      </xsl:element>
      <xsl:element name="tender-or-lot-reference">
        <xsl:attribute name="id">bt-17</xsl:attribute>
        <xsl:value-of select="cac:OriginatorDocumentReference/cbc:ID"/>
      </xsl:element>
      <xsl:element name="invoiced-object-identifier">
        <xsl:attribute name="id">bt-18</xsl:attribute>
        <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID"/>
      </xsl:element>
      <xsl:element name="buyer-accounting-reference">
        <xsl:attribute name="id">bt-19</xsl:attribute>
        <xsl:value-of select="cbc:AccountingCost"/>
      </xsl:element>
      <xsl:element name="payment-terms">
        <xsl:attribute name="id">bt-20</xsl:attribute>
        <xsl:value-of select="cac:PaymentTerms/cbc:Note"/>
      </xsl:element>
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
    </xsl:element>
    <xsl:element name="process-control">
      <xsl:attribute name="id">bg-2</xsl:attribute>
      <!-- TODO: continue here -->
    </xsl:element>
    <xsl:element name="preceding-invoice-references">
      <xsl:attribute name="id">bg-3</xsl:attribute>
      <!-- list -->
    </xsl:element>
    <xsl:element name="seller">
      <xsl:attribute name="id">bg-4</xsl:attribute>
    </xsl:element>
    <xsl:element name="buyer">
      <xsl:attribute name="id">bg-7</xsl:attribute>
    </xsl:element>
    <xsl:element name="payee">
      <xsl:attribute name="id">bg-10</xsl:attribute>
    </xsl:element>
    <xsl:element name="seller-tax-representative-party">
      <xsl:attribute name="id">bg-11</xsl:attribute>
    </xsl:element>
    <xsl:element name="delivery-information">
      <xsl:attribute name="id">bg-13</xsl:attribute>
    </xsl:element>
    <xsl:element name="payment-instructions">
      <xsl:attribute name="id">bg-16</xsl:attribute>
    </xsl:element>
    <xsl:element name="document-level-allowances">
      <xsl:attribute name="id">bg-20</xsl:attribute>
      <!-- list -->
    </xsl:element>
    <xsl:element name="document-level-charges">
      <xsl:attribute name="id">bg-21</xsl:attribute>
      <!-- list -->
    </xsl:element>
    <xsl:element name="document-totals">
      <xsl:attribute name="id">bg-22</xsl:attribute>
    </xsl:element>
    <xsl:element name="vat-breakdown">
      <xsl:attribute name="id">bg-23</xsl:attribute>
      <!-- list -->
    </xsl:element>
    <xsl:element name="additional-supporting-documents">
      <xsl:attribute name="id">bg-24</xsl:attribute>
      <!-- list -->
    </xsl:element>
    <xsl:element name="invoice-lines">
      <xsl:attribute name="id">bg-25</xsl:attribute>
      <!-- list -->
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
