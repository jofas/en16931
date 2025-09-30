<xsl:stylesheet
    xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns="urn:todo"
    exclude-result-prefixes="ubl cac cbc xsl"
    version="1.0">
  <xsl:include href="common.xslt"/>
  <xsl:template match="ubl:Invoice">
    <invoice>
      <xsl:call-template name="common-invoice-bt-1-2"/>
      <!-- UBL CreditNote: cbc:CreditNoteTypeCode -->
      <invoice-type-code id="bt-3">
        <xsl:value-of select="cbc:InvoiceTypeCode"/>
      </invoice-type-code>
      <xsl:call-template name="common-invoice-bt-5-8"/>
      <!-- UBL CreditNote: cac:PaymentTerms/cbc:PaymentDueDate -->
      <xsl:if test="exists(cbc:DueDate)">
        <payment-due-date id="bt-9">
          <xsl:value-of select="cbc:DueDate"/>
        </payment-due-date>
      </xsl:if>
      <xsl:call-template name="common-invoice-bt-10"/>
      <!-- UBL CreditNote: cac:AdditionalDocumentReference[cbc:DocumentTypeCode='50']/cbc:ID -->
      <xsl:if test="exists(cac:ProjectReference/cbc:ID)">
        <project-reference id="bt-11">
          <xsl:value-of select="cac:ProjectReference/cbc:ID"/>
        </project-reference>
      </xsl:if>
      <xsl:call-template name="common-invoice-bt-12-20"/>
      <xsl:call-template name="common-invoice-bg-1-23"/>
      <!-- UBL CreditNote DocumentType can't be 50 either -->
      <xsl:if test="exists(cac:AdditionalDocumentReference[not(cbc:DocumentTypeCode = '130')]/cbc:ID)">
        <additional-supporting-documents id="bg-24">
          <xsl:for-each select="cac:AdditionalDocumentReference[not(cbc:DocumentTypeCode = '130')]">
            <xsl:call-template name="common-additional-supporting-document"/>
          </xsl:for-each>
        </additional-supporting-documents>
      </xsl:if>
      <invoice-lines id="bg-25">
        <!-- CreditNote: cac:CreditNoteLine -->
        <xsl:for-each select="cac:InvoiceLine">
          <invoice-line id="bg-25">
            <xsl:call-template name="common-invoice-line-bt-126-128"/>
            <!-- UBL CreditNote: cbc:CreditedQuantity -->
            <invoiced-quantity id="bt-129">
              <xsl:value-of select="./cbc:InvoicedQuantity"/>
            </invoiced-quantity>
            <!-- UBL CreditNote: cbc:CreditedQuantity -->
            <invoiced-quantity-unit-of-measure-code id="bt-130">
              <xsl:value-of select="./cbc:InvoicedQuantity/@unitCode"/>
            </invoiced-quantity-unit-of-measure-code>
            <xsl:call-template name="common-invoice-line-bt-131-133"/>
            <xsl:call-template name="common-invoice-line-bg-26-31"/>
          </invoice-line>
        </xsl:for-each>
      </invoice-lines>
    </invoice>
  </xsl:template>
</xsl:stylesheet>
