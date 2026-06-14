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
      <rsm:ExchangedDocumentContext>
        <ram:BusinessProcessSpecifiedDocumentContextParameter>
          <ram:ID>
            <!-- bt-23 -->
            <xsl:value-of select="ir:process-control/ir:business-process-type"/>
          </ram:ID>
        </ram:BusinessProcessSpecifiedDocumentContextParameter>
        <ram:GuidelineSpecifiedDocumentContextParameter>
          <ram:ID>
            <!-- bt-24 -->
            <xsl:value-of select="ir:process-control/ir:specification-identifier/ir:content"/>
          </ram:ID>
        </ram:GuidelineSpecifiedDocumentContextParameter>
      </rsm:ExchangedDocumentContext>
      <rsm:ExchangedDocument>
        <ram:ID>
		  <!-- bt-1 -->
          <xsl:value-of select="ir:invoice-number"/>
        </ram:ID>
        <ram:TypeCode>
		  <!-- bt-3 -->
          <xsl:value-of select="ir:invoice-type-code"/>
        </ram:TypeCode>
        <ram:IssueDateTime>
          <udt:DateTimeString format="102">
		    <!-- bt-2 -->
            <xsl:call-template name="date">
              <xsl:with-param name="node" select="ir:invoice-issue-date"/>
            </xsl:call-template>
          </udt:DateTimeString>
        </ram:IssueDateTime>
        <xsl:for-each select="ir:invoice-notes/ir:invoice-note">
          <ram:IncludedNote>
            <ram:Content>
	          <!-- bt-22 -->
              <xsl:value-of select="./ir:invoice-note"/>
            </ram:Content>
            <xsl:if test="exists(./ir:invoice-note-subject-code)">
              <ram:SubjectCode>
		        <!-- bt-21 -->
                <xsl:value-of select="./ir:invoice-note-subject-code"/>
              </ram:SubjectCode>
            </xsl:if>
          </ram:IncludedNote>
        </xsl:for-each>
      </rsm:ExchangedDocument>
      <rsm:SupplyChainTradeTransaction>
        <xsl:for-each select="ir:invoice-lines/ir:invoice-line">
          <ram:IncludedSupplyChainTradeLineItem>
            <ram:AssociatedDocumentLineDocument>
              <ram:LineID>
                <!-- bt-126 -->
                <xsl:value-of select="./ir:invoice-line-identifier/ir:content"/>
              </ram:LineID>
              <xsl:if test="exists(./ir:invoice-line-note)">
                <ram:IncludedNote>
                  <ram:Content>
                    <!-- bt-127 -->
                    <xsl:value-of select="./ir:invoice-line-note"/>
                  </ram:Content>
                </ram:IncludedNote>
              </xsl:if>
            </ram:AssociatedDocumentLineDocument>
            <ram:SpecifiedTradeProduct>
              <xsl:if test="exists(./ir:item-information/ir:item-standard-identifier)">
                <ram:GlobalID>
                  <xsl:attribute name="schemeID">
                    <!-- bt-157-1 -->
                    <xsl:value-of select="./ir:item-information/ir:item-standard-identifier/ir:scheme-identifier"/>
                  </xsl:attribute>
                  <!-- bt-157 -->
                  <xsl:value-of select="./ir:item-information/ir:item-standard-identifier/ir:content"/>
                </ram:GlobalID>
              </xsl:if>
              <xsl:if test="exists(./ir:item-information/ir:item-sellers-identifier)">
                <ram:SellerAssignedID>
                    <!-- bt-155 -->
                  <xsl:value-of select="./ir:item-information/ir:item-sellers-identifier/ir:content"/>
                </ram:SellerAssignedID>
              </xsl:if>
              <xsl:if test="exists(./ir:item-information/ir:item-buyers-identifier)">
                <ram:BuyerAssignedID>
                    <!-- bt-156 -->
                  <xsl:value-of select="./ir:item-information/ir:item-buyers-identifier/ir:content"/>
                </ram:BuyerAssignedID>
              </xsl:if>
              <ram:Name>
                <!-- bt-153 -->
                <xsl:value-of select="./ir:item-information/ir:item-name"/>
              </ram:Name>
              <xsl:if test="exists(./ir:item-information/ir:item-description)">
                <ram:Description>
                    <!-- bt-154 -->
                  <xsl:value-of select="./ir:item-information/ir:item-decription"/>
                </ram:Description>
              </xsl:if>
              <xsl:for-each select="./ir:item-information/ir:item-attributes/ir:item-attribute">
                <ram:ApplicableProductCharacteristic>
                  <ram:Description>
                    <!-- bt-160 -->
                    <xsl:value-of select="./ir:item-attribute-name"/>
                  </ram:Description>
                  <ram:Value>
                    <!-- bt-161 -->
                    <xsl:value-of select="./ir:item-attribute-value"/>
                  </ram:Value>
                </ram:ApplicableProductCharacteristic>
              </xsl:for-each>
              <xsl:for-each select="./ir:item-information/ir:item-classification-identifiers/ir:item-classification-identifier">
                <ram:DesignatedProductClassification>
                  <ram:ClassCode>
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
                  </ram:ClassCode>
                </ram:DesignatedProductClassification>
              </xsl:for-each>
              <xsl:if test="exists(./ir:item-information/ir:item-country-of-origin)">
                <ram:OriginTradeCountry>
                  <ram:ID>
                    <!-- bt-159 -->
                    <xsl:value-of select="./ir:item-information/ir:item-country-of-origin"/>
                  </ram:ID>
                </ram:OriginTradeCountry>
              </xsl:if>
            </ram:SpecifiedTradeProduct>
          </ram:IncludedSupplyChainTradeLineItem>
        </xsl:for-each>
      </rsm:SupplyChainTradeTransaction>
    </rsm:CrossIndustryInvoice>
  </xsl:template>

  <!-- TODO: support for format codes 610 and 616. 102 already implemented. See https://github.com/itplr-kosit/validator-configuration-xrechnung/issues/56 -->
  <xsl:template name="date">
    <xsl:param name="node"/>
    <xsl:value-of select="substring($node, 1, 4)"/><xsl:value-of select="substring($node, 6, 2)"/><xsl:value-of select="substring($node, 9, 2)"/>
  </xsl:template>

  <!--
      BT-8 is mapped to a different code list (UNTDID 2475) in CII than in EN16931 (UNTDID 2005).

      This template provides the mapping for all allowed values.
      See also: https://github.com/phax/en16931-cii2ubl/issues/29
  -->
  <xsl:template name="bt-8">
    <xsl:variable name="bt-8" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:DueDateTypeCode"/>
    <value-added-tax-point-date-code id="bt-8">
      <xsl:choose>
        <xsl:when test="$bt-8 = 5">3</xsl:when>
        <xsl:when test="$bt-8 = 29">35</xsl:when>
        <xsl:when test="$bt-8 = 72">432</xsl:when>
        <xsl:otherwise>
          <xsl:message terminate="yes">
            Error: BT-8 is filled with unknown value: <xsl:value-of select="$bt-8"/>. Allowed values are: 5, 29, 72.
          </xsl:message>
        </xsl:otherwise>
      </xsl:choose>
    </value-added-tax-point-date-code>
  </xsl:template>
</xsl:stylesheet>
