<xsl:stylesheet
    xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2"
    xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
    xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    exclude-result-prefixes="ubl cac cbc xsl"
    version="1.0">
  <xsl:template match="ubl:CreditNote">
    <invoice xmlns="urn:todo">
    </invoice>
  </xsl:template>
</xsl:stylesheet>
