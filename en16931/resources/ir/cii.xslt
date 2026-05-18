<xsl:stylesheet
    xmlns:rsm="urn:un:unece:uncefact:data:standard:CrossIndustryInvoice:100"
    xmlns:ram="urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:100"
    xmlns:qdt="urn:un:unece:uncefact:data:standard:QualifiedDataType:100"
    xmlns:udt="urn:un:unece:uncefact:data:standard:UnqualifiedDataType:100"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns="urn:todo"
    exclude-result-prefixes="rsm ram qdt udt xsl"
    version="1.0">
  <xsl:template match="rsm:CrossIndustryInvoice">
    <invoice>
      <invoice-number id="bt-1">
        <content>
          <xsl:value-of select="rsm:ExchangedDocument/ram:ID"/>
        </content>
      </invoice-number>
      <invoice-issue-date id="bt-2">
        <xsl:call-template name="date">
          <xsl:with-param name="node" select="rsm:ExchangedDocument/ram:IssueDateTime/udt:DateTimeString[@format = '102']"/>
        </xsl:call-template>
      </invoice-issue-date>
      <invoice-type-code id="bt-3">
        <xsl:value-of select="rsm:ExchangedDocument/ram:TypeCode"/>
      </invoice-type-code>
      <invoice-currency-code id="bt-5">
        <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:InvoiceCurrencyCode"/>
      </invoice-currency-code>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:TaxCurrencyCode)">
        <vat-accounting-currency-code id="bt-6">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:TaxCurrencyCode"/>
        </vat-accounting-currency-code>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:TaxPointDate/udt:DateString[@format = '102'])">
        <value-added-tax-point-date id="bt-7">
          <xsl:call-template name="date">
            <xsl:with-param name="node" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:TaxPointDate/udt:DateString[@format = '102']"/>
          </xsl:call-template>
        </value-added-tax-point-date>
      </xsl:if>
      <!-- TODO: bt-8 -->
      <!-- TODO: bt-9 -->
      <buyer-reference id="bt-10">
        <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerReference"/>
      </buyer-reference>
      <!-- TODO: bt-11 -->
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:ContractReferencedDocument/ram:IssuerAssignedID)">
        <contract-reference id="bt-12">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:ContractReferencedDocument/ram:IssuerAssignedID"/>
        </contract-reference>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerOrderReferencedDocument/ram:IssuerAssignedID)">
        <purchase-order-reference id="bt-13">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerOrderReferencedDocument/ram:IssuerAssignedID"/>
        </purchase-order-reference>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerOrderReferencedDocument/ram:IssuerAssignedID)">
        <sales-order-reference id="bt-14">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerOrderReferencedDocument/ram:IssuerAssignedID"/>
        </sales-order-reference>
      </xsl:if>
      <process-control id="bg-2">
        <business-process-type id="bt-23">
          <xsl:value-of select="rsm:ExchangedDocumentContext/ram:BusinessProcessSpecifiedDocumentContextParameter/ram:ID"/>
        </business-process-type>
        <specification-identifier id="bt-24">
          <content>
            <xsl:value-of select="rsm:ExchangedDocumentContext/ram:GuidelineSpecifiedDocumentContextParameter/ram:ID"/>
          </content>
        </specification-identifier>
      </process-control>
      <seller id="bg-4">
        <seller-name id="bt-27">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:Name"/>
        </seller-name>
        <seller-electronic-address id="bt-34">
          <content>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:URIUniversalCommunication/ram:URIID"/>
          </content>
          <scheme-identifier>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:URIUniversalCommunication/ram:URIID/@schemeID"/>
          </scheme-identifier>
        </seller-electronic-address>
        <seller-postal-address id="bg-5">
          <seller-city id="bt-37">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:CityName"/>
          </seller-city>
          <seller-post-code id="bt-38">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:PostcodeCode"/>
          </seller-post-code>
          <seller-country-code id="bt-40">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:CountryID"/>
          </seller-country-code>
        </seller-postal-address>
        <seller-contact id="bg-6">
          <seller-contact-point id="bt-41">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:DefinedTradeContact/ram:PersonName"/>
          </seller-contact-point>
          <seller-contact-telephone-number id="bt-42">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:DefinedTradeContact/ram:TelephoneUniversalCommunication/ram:CompleteNumber"/>
          </seller-contact-telephone-number>
          <seller-contact-email-address id="bt-43">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:DefinedTradeContact/ram:EmailURIUniversalCommunication/ram:URIID"/>
          </seller-contact-email-address>
        </seller-contact>
      </seller>
      <!-- TODO: bg-7 -->
      <buyer>
        <buyer-name id="bt-44">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:Name"/>
        </buyer-name>
        <buyer-electronic-address id="bt-49">
          <content>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:URIUniversalCommunication/ram:URIID"/>
          </content>
          <scheme-identifier>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:URIUniversalCommunication/ram:URIID/@schemeID"/>
          </scheme-identifier>
        </buyer-electronic-address>
        <buyer-postal-address id="bg-8">
          <buyer-city id="bt-52">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:CityName"/>
          </buyer-city>
          <buyer-post-code id="bt-53">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:PostcodeCode"/>
          </buyer-post-code>
          <buyer-country-code id="bt-55">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:CountryID"/>
          </buyer-country-code>
        </buyer-postal-address>
      </buyer>
      <payment-instructions id="bg-16">
        <payment-means-type-code id="bt-81">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:TypeCode"/>
        </payment-means-type-code>
      </payment-instructions>
      <document-totals id="bg-22">
      </document-totals>
      <vat-breakdown id="bg-23">
        <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax">
          <vat-breakdown id="bg-23">
            <vat-category-taxable-amount id="bt-116">
              <xsl:value-of select="./ram:BasisAmount"/>
            </vat-category-taxable-amount>
            <vat-category-tax-amount id="bt-117">
              <xsl:value-of select="./ram:CalculatedAmount"/>
            </vat-category-tax-amount>
            <!-- TODO: what's up with ./ram:TypeCode = 'VAT'? Do I need to consider it maybe in for-loop? Ignore it? -->
            <vat-category-code id="bt-118">
              <xsl:value-of select="./ram:CategoryCode"/>
            </vat-category-code>
            <vat-category-rate id="bt-119">
              <xsl:value-of select="./ram:RateApplicablePercent"/>
            </vat-category-rate>
          </vat-breakdown>
        </xsl:for-each>
      </vat-breakdown>
      <invoice-lines id="bg-25">
        <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:IncludedSupplyChainTradeLineItem">
          <invoice-line id="bg-25">
            <invoice-line-identifier id="bt-126">
              <content>
                <xsl:value-of select="./ram:AssociatedDocumentLineDocument/ram:LineID"/>
              </content>
            </invoice-line-identifier>
            <invoiced-quantity id="bt-129">
              <xsl:value-of select="./ram:SpecifiedLineTradeDelivery/ram:BilledQuantity"/>
            </invoiced-quantity>
            <invoiced-quantity-unit-of-measure-code id="bt-130">
              <xsl:value-of select="./ram:SpecifiedLineTradeDelivery/ram:BilledQuantity/@unitCode"/>
            </invoiced-quantity-unit-of-measure-code>
            <invoice-line-net-amount id="bt-131">
              <xsl:value-of select="./ram:SpecifiedLineTradeSettlement/ram:SpecifiedTradeSettlementLineMonetarySummation/ram:LineTotalAmount"/>
            </invoice-line-net-amount>
            <price-details id="bg-29">
              <item-net-price id="bt-146">
                <xsl:value-of select="./ram:SpecifiedLineTradeAgreement/ram:NetPriceProductTradePrice/ram:ChargeAmount"/>
              </item-net-price>
            </price-details>
            <line-vat-information id="bg-30">
              <!-- TODO: what's up with ram:TypeCode = 'VAT'? Do I need to consider it maybe in for-loop? Ignore it? -->
              <invoiced-item-vat-category-code id="bt-151">
                <xsl:value-of select="./ram:SpecifiedLineTradeSettlement/ram:ApplicableTradeTax/ram:CategoryCode"/>
              </invoiced-item-vat-category-code>
            </line-vat-information>
            <item-information id="bg-31">
              <item-name id="bt-153">
                <xsl:value-of select="./ram:SpecifiedTradeProduct/ram:Name"/>
              </item-name>
            </item-information>
          </invoice-line>
        </xsl:for-each>
      </invoice-lines>
    </invoice>
  </xsl:template>

  <!-- TODO: support for format codes 610 and 616. 102 already implemented. See https://github.com/itplr-kosit/validator-configuration-xrechnung/issues/56 -->
  <xsl:template name="date">
    <xsl:param name="node"/>
    <xsl:value-of select="substring($node, 1, 4)"/>-<xsl:value-of select="substring($node, 5, 2)"/>-<xsl:value-of select="substring($node, 7, 2)"/>
  </xsl:template>
</xsl:stylesheet>
