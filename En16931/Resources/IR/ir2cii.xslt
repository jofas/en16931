<xsl:stylesheet
    xmlns:rsm="urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"
    xmlns:ram="urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"
    xmlns:qdt="urn:un:unece:uncefact:data:standard:QualifiedDataType:100"
    xmlns:udt="urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:ir="urn:todo"
    exclude-result-prefixes="xsl ir"
    version="1.0">
  <xsl:template match="/ir:invoice">
    <rsm:CrossIndustryInvoice>
      <!-- TODO: implement -->
    </rsm:CrossIndustryInvoice>
  </xsl:template>
</xsl:stylesheet>
