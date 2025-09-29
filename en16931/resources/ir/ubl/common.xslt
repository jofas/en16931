<xsl:stylesheet
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns="urn:todo"
    exclude-result-prefixes="cac cbc xsl"
    version="1.0">
  <xsl:template name="common-bt-1-2">
    <invoice-number id="bt-1">
      <content>
        <xsl:value-of select="cbc:ID"/>
      </content>
    </invoice-number>
    <invoice-issue-date id="bt-2">
      <xsl:value-of select="cbc:IssueDate"/>
    </invoice-issue-date>
  </xsl:template>

  <xsl:template name="common-bt-5-8">
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
</xsl:stylesheet>
