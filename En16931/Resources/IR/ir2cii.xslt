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
            <ram:SpecifiedLineTradeAgreement>
              <xsl:if test="exists(./ir:referenced-purchase-order-line-reference)">
                <ram:BuyerOrderReferencedDocument>
                  <ram:LineID>
                    <!-- bt-132 -->
                    <xsl:value-of select="./ir:referenced-purchase-order-line-reference"/>
                  </ram:LineID>
                </ram:BuyerOrderReferencedDocument>
              </xsl:if>
              <xsl:if test="exists(./ir:price-details/ir:item-price-discount)
                  or exists(./ir:price-details/ir:item-gross-price)">
                <ram:GrossPriceProductTradePrice>
                  <xsl:if test="exists(./ir:price-details/ir:item-gross-price)">
                    <ram:ChargeAmount>
                      <!-- bt-148 -->
                      <xsl:value-of select="./ir:price-details/ir:item-gross-price"/>
                    </ram:ChargeAmount>
                  </xsl:if>
                  <xsl:if test="exists(./ir:price-details/ir:item-price-discount)">
                    <ram:AppliedTradeAllowanceCharge>
                      <ram:ChargeIndicator>
                        <udt:Indicator>false</udt:Indicator>
                      </ram:ChargeIndicator>
                      <ram:ActualAmount>
                        <!-- bt-147 -->
                        <xsl:value-of select="./ir:price-details/ir:item-price-discount"/>
                      </ram:ActualAmount>
                    </ram:AppliedTradeAllowanceCharge>
                  </xsl:if>
                </ram:GrossPriceProductTradePrice>
              </xsl:if>
              <ram:NetPriceProductTradePrice>
                <ram:ChargeAmount>
                  <!-- bt-146 -->
                  <xsl:value-of select="./ir:price-details/ir:item-net-price"/>
                </ram:ChargeAmount>
                <xsl:if test="exists(./ir:price-details/ir:item-price-base-quantity)">
                  <ram:BasisQuantity>
                    <xsl:if test="exists(./ir:price-details/ir:item-price-base-quantity-unit-of-measure-code)">
                      <xsl:attribute name="unitCode">
                        <!-- bt-150 -->
                        <xsl:value-of select="./ir:price-details/ir:item-price-base-quantity-unit-of-measure-code"/>
                      </xsl:attribute>
                    </xsl:if>
                    <!-- bt-149 -->
                    <xsl:value-of select="./ir:price-details/ir:item-price-base-quantity"/>
                  </ram:BasisQuantity>
                </xsl:if>
              </ram:NetPriceProductTradePrice>
            </ram:SpecifiedLineTradeAgreement>
            <ram:SpecifiedLineTradeDelivery>
              <ram:BilledQuantity>
                <xsl:attribute name="unitCode">
                  <!-- bt-130 -->
                  <xsl:value-of select="./ir:invoiced-quantity-unit-of-measure-code"/>
                </xsl:attribute>
                <!-- bt-129 -->
                <xsl:value-of select="./ir:invoiced-quantity"/>
              </ram:BilledQuantity>
            </ram:SpecifiedLineTradeDelivery>
            <ram:SpecifiedLineTradeSettlement>
              <ram:ApplicableTradeTax>
                <ram:TypeCode>VAT</ram:TypeCode>
                <ram:CategoryCode>
                  <!-- bt-151 -->
                  <xsl:value-of select="./ir:line-vat-information/ir:invoiced-item-vat-category-code"/>
                </ram:CategoryCode>
                <xsl:if test="exists(./ir:line-vat-information/ir:invoiced-item-vat-rate)">
                  <ram:RateApplicablePercent>
                    <!-- bt-152 -->
                    <xsl:value-of select="./ir:line-vat-information/ir:invoiced-item-vat-rate"/>
                  </ram:RateApplicablePercent>
                </xsl:if>
              </ram:ApplicableTradeTax>
              <xsl:if test="exists(./ir:invoice-line-period/ir:invoice-line-period-start-date)
                  or exists(./ir:invoice-line-period/ir:invoice-line-period-end-date)">
                <ram:BillingSpecifiedPeriod>
                  <xsl:if test="exists(./ir:invoice-line-period/ir:invoice-line-period-start-date)">
                    <ram:StartDateTime>
                      <udt:DateTimeString format="102">
                        <!-- bt-134 -->
                        <xsl:call-template name="date">
                          <xsl:with-param name="node" select="./ir:invoice-line-period/ir:invoice-line-period-start-date"/>
                        </xsl:call-template>
                      </udt:DateTimeString>
                    </ram:StartDateTime>
                  </xsl:if>
                  <xsl:if test="exists(./ir:invoice-line-period/ir:invoice-line-period-end-date)">
                    <ram:EndDateTime>
                      <udt:DateTimeString format="102">
                        <!-- bt-135 -->
                        <xsl:call-template name="date">
                          <xsl:with-param name="node" select="./ir:invoice-line-period/ir:invoice-line-period-end-date"/>
                        </xsl:call-template>
                      </udt:DateTimeString>
                    </ram:EndDateTime>
                  </xsl:if>
                </ram:BillingSpecifiedPeriod>
              </xsl:if>
              <xsl:for-each select="./ir:invoice-line-charges/ir:invoice-line-charge">
                <ram:SpecifiedTradeAllowanceCharge>
                  <ram:ChargeIndicator>
                    <udt:Indicator>true</udt:Indicator>
                  </ram:ChargeIndicator>
                  <xsl:if test="exists(./ir:invoice-line-charge-percentage)">
                    <ram:CalculationPercent>
                      <!-- bt-143 -->
                      <xsl:value-of select="./ir:invoice-line-charge-percentage"/>
                    </ram:CalculationPercent>
                  </xsl:if>
                  <xsl:if test="exists(./ir:invoice-line-charge-base-amount)">
                    <ram:BasisAmount>
                      <!-- bt-142 -->
                      <xsl:value-of select="./ir:invoice-line-charge-base-amount"/>
                    </ram:BasisAmount>
                  </xsl:if>
                  <ram:ActualAmount>
                    <!-- bt-141 -->
                    <xsl:value-of select="./ir:invoice-line-charge-amount"/>
                  </ram:ActualAmount>
                  <xsl:if test="exists(./ir:invoice-line-charge-reason-code)">
                    <ram:ReasonCode>
                      <!-- bt-145 -->
                      <xsl:value-of select="./ir:invoice-line-charge-reason-code"/>
                    </ram:ReasonCode>
                  </xsl:if>
                  <xsl:if test="exists(./ir:invoice-line-charge-reason)">
                    <ram:Reason>
                      <!-- bt-144 -->
                      <xsl:value-of select="./ir:invoice-line-charge-reason"/>
                    </ram:Reason>
                  </xsl:if>
                </ram:SpecifiedTradeAllowanceCharge>
              </xsl:for-each>
              <xsl:for-each select="./ir:invoice-line-allowances/ir:invoice-line-allowance">
                <ram:SpecifiedTradeAllowanceCharge>
                  <ram:ChargeIndicator>
                    <udt:Indicator>false</udt:Indicator>
                  </ram:ChargeIndicator>
                  <xsl:if test="exists(./ir:invoice-line-allowance-percentage)">
                    <ram:CalculationPercent>
                      <!-- bt-138 -->
                      <xsl:value-of select="./ir:invoice-line-allowance-percentage"/>
                    </ram:CalculationPercent>
                  </xsl:if>
                  <xsl:if test="exists(./ir:invoice-line-allowance-base-amount)">
                    <ram:BasisAmount>
                      <!-- bt-137 -->
                      <xsl:value-of select="./ir:invoice-line-allowance-base-amount"/>
                    </ram:BasisAmount>
                  </xsl:if>
                  <ram:ActualAmount>
                    <!-- bt-136 -->
                    <xsl:value-of select="./ir:invoice-line-allowance-amount"/>
                  </ram:ActualAmount>
                  <xsl:if test="exists(./ir:invoice-line-allowance-reason-code)">
                    <ram:ReasonCode>
                      <!-- bt-140 -->
                      <xsl:value-of select="./ir:invoice-line-allowance-reason-code"/>
                    </ram:ReasonCode>
                  </xsl:if>
                  <xsl:if test="exists(./ir:invoice-line-allowance-reason)">
                    <ram:Reason>
                      <!-- bt-139 -->
                      <xsl:value-of select="./ir:invoice-line-allowance-reason"/>
                    </ram:Reason>
                  </xsl:if>
                </ram:SpecifiedTradeAllowanceCharge>
              </xsl:for-each>
            </ram:SpecifiedLineTradeSettlement>
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
