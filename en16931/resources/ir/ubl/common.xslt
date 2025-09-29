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
</xsl:stylesheet>
