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
      <!-- CII-SR-461 fails if more than one ram:TaxPointDate node is present (there can be multiple ram:ApplicableTradeTax elements)  -->
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:TaxPointDate/udt:DateString[@format = '102'])">
        <value-added-tax-point-date id="bt-7">
          <xsl:call-template name="date">
            <xsl:with-param name="node" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:TaxPointDate/udt:DateString[@format = '102']"/>
          </xsl:call-template>
        </value-added-tax-point-date>
      </xsl:if>
      <!-- CII-SR-462 fails if more than one ram:DueDateTypeCode node is present (there can be multiple ram:ApplicableTradeTax elements) -->
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:DueDateTypeCode)">
        <value-added-tax-point-date-code id="bt-8">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax/ram:DueDateTypeCode"/>
        </value-added-tax-point-date-code>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradePaymentTerms/ram:DueDateDateTime/udt:DateTimeString[@format = '102'])">
        <payment-due-date id="bt-9">
          <xsl:call-template name="date">
            <xsl:with-param name="node" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradePaymentTerms/ram:DueDateDateTime/udt:DateTimeString[@format = '102']"/>
          </xsl:call-template>
        </payment-due-date>
      </xsl:if>
      <buyer-reference id="bt-10">
        <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerReference"/>
      </buyer-reference>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SpecifiedProcuringProject/ram:ID)">
        <project-reference id="bt-11">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SpecifiedProcuringProject/ram:ID"/>
        </project-reference>
      </xsl:if>
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
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ReceivingAdviceReferencedDocument/ram:IssuerAssignedID)">
        <receiving-advice-reference id="bt-15">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ReceivingAdviceReferencedDocument/ram:IssuerAssignedID"/>
        </receiving-advice-reference>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:DespatchAdviceReferencedDocument/ram:IssuerAssignedID)">
        <despatch-advice-reference id="bt-16">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:DespatchAdviceReferencedDocument/ram:IssuerAssignedID"/>
        </despatch-advice-reference>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument[ram:TypeCode = '50'])">
        <tender-or-lot-reference id="bt-17">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument[ram:TypeCode = '50']/ram:IssuerAssignedID"/>
        </tender-or-lot-reference>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument[ram:TypeCode = '130'])">
        <invoiced-object-identifier id="bt-18">
          <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument[ram:TypeCode = '130']/ram:IssuerAssignedID"/>
          </content>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument[ram:TypeCode = '130']/ram:ReferenceTypeCode)">
            <scheme-identifier>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:AdditionalReferencedDocument[ram:TypeCode = '130']/ram:ReferenceTypeCode"/>
            </scheme-identifier>
          </xsl:if>
        </invoiced-object-identifier>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ReceivableSpecifiedTradeAccountingAccount/ram:ID)">
        <buyer-accounting-reference id="bt-19">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ReceivableSpecifiedTradeAccountingAccount/ram:ID"/>
        </buyer-accounting-reference>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradePaymentTerms/ram:Description)">
        <payment-terms id="bt-20">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradePaymentTerms/ram:Description"/>
        </payment-terms>
      </xsl:if>
      <xsl:if test="exists(rsm:ExchangedDocument/ram:IncludedNote)">
        <invoice-notes id="bg-1">
          <xsl:for-each select="rsm:ExchangedDocument/ram:IncludedNote">
            <invoice-note id="bg-1">
              <xsl:if test="exists(./ram:SubjectCode)">
                <invoice-note-subject-code id="bt-21">
                  <xsl:value-of select="./ram:SubjectCode"/>
                </invoice-note-subject-code>
              </xsl:if>
              <invoice-note id="bt-22">
                <xsl:value-of select="ram:Content"/>
              </invoice-note>
            </invoice-note>
          </xsl:for-each>
        </invoice-notes>
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
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:InvoiceReferencedDocument)">
        <preceding-invoice-references id="bg-3">
          <!-- Note that CII does not actually support multiple bg-3 instances -->
          <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:InvoiceReferencedDocument">
            <preceding-invoice-reference id="bg-3">
              <preceding-invoice-reference id="bt-25">
                <xsl:value-of select="./ram:IssuerAssignedID"/>
              </preceding-invoice-reference>
              <xsl:if test="exists(./ram:FormattedIssueDateTime/qdt:DateTimeString[@format = '102'])">
                <preceding-invoice-issue-date id="bt-26">
                  <xsl:call-template name="date">
                    <xsl:with-param name="node" select="./ram:FormattedIssueDateTime/qdt:DateTimeString[@format = '102']"/>
                  </xsl:call-template>
                </preceding-invoice-issue-date>
              </xsl:if>
            </preceding-invoice-reference>
          </xsl:for-each>
        </preceding-invoice-references>
      </xsl:if>
      <seller id="bg-4">
        <seller-name id="bt-27">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:Name"/>
        </seller-name>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedLegalOrganization/ram:TradingBusinessName)">
          <seller-trading-name id="bt-28">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedLegalOrganization/ram:TradingBusinessName"/>
          </seller-trading-name>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:ID)
            or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:GlobalID)">
          <seller-identifiers id="bt-29">
            <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:ID">
              <seller-identifier id="bt-29">
                <content>
                  <xsl:value-of select="."/>
                </content>
              </seller-identifier>
            </xsl:for-each>
            <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:GlobalID">
              <seller-identifier id="bt-29">
                <content>
                  <xsl:value-of select="."/>
                </content>
                <scheme-identifier>
                  <xsl:value-of select="./@schemeID"/>
                </scheme-identifier>
              </seller-identifier>
            </xsl:for-each>
          </seller-identifiers>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedLegalOrganization/ram:ID)">
          <seller-legal-registration-identifier id="bt-30">
            <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedLegalOrganization/ram:ID"/>
            </content>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedLegalOrganization/ram:ID[@schemeID])">
              <scheme-identifier>
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedLegalOrganization/ram:ID/@schemeID"/>
              </scheme-identifier>
            </xsl:if>
          </seller-legal-registration-identifier>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'VA']])">
          <seller-vat-identifier id="bt-31">
            <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'VA']]/ram:ID"/>
            </content>
          </seller-vat-identifier>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'FC']])">
          <seller-tax-registration-identifier id="bt-32">
            <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'FC']]/ram:ID"/>
            </content>
          </seller-tax-registration-identifier>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:Description)">
          <seller-additional-legal-information id="bt-33">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:Description"/>
          </seller-additional-legal-information>
        </xsl:if>
        <seller-electronic-address id="bt-34">
          <content>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:URIUniversalCommunication/ram:URIID"/>
          </content>
          <scheme-identifier>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:URIUniversalCommunication/ram:URIID/@schemeID"/>
          </scheme-identifier>
        </seller-electronic-address>
        <seller-postal-address id="bg-5">
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:LineOne)">
            <seller-address-line-1 id="bt-35">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:LineOne"/>
            </seller-address-line-1>
          </xsl:if>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:LineTwo)">
            <seller-address-line-2 id="bt-36">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:LineTwo"/>
            </seller-address-line-2>
          </xsl:if>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:LineThree)">
            <seller-address-line-3 id="bt-162">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:LineThree"/>
            </seller-address-line-3>
          </xsl:if>
          <seller-city id="bt-37">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:CityName"/>
          </seller-city>
          <seller-post-code id="bt-38">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:PostcodeCode"/>
          </seller-post-code>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName)">
            <seller-country-subdivision id="bt-39">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName"/>
            </seller-country-subdivision>
          </xsl:if>
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
      <buyer id="bg-7">
        <buyer-name id="bt-44">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:Name"/>
        </buyer-name>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedLegalOrganization/ram:TradingBusinessName)">
          <buyer-trading-name id="bt-45">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedLegalOrganization/ram:TradingBusinessName"/>
          </buyer-trading-name>
        </xsl:if>
        <xsl:choose>
          <xsl:when test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:GlobalID)">
            <buyer-identifier id="bt-46">
              <content>
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:GlobalID"/>
              </content>
              <scheme-identifier>
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:GlobalID/@schemeID"/>
              </scheme-identifier>
            </buyer-identifier>
          </xsl:when>
          <xsl:when test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:ID)">
            <buyer-identifier id="bt-46">
              <content>
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:ID"/>
              </content>
            </buyer-identifier>
          </xsl:when>
        </xsl:choose>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedLegalOrganization/ram:ID)">
          <buyer-legal-registration-identifier id="bt-47">
            <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedLegalOrganization/ram:ID"/>
            </content>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedLegalOrganization/ram:ID[@schemeID])">
              <scheme-identifier>
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedLegalOrganization/ram:ID/@schemeID"/>
              </scheme-identifier>
            </xsl:if>
          </buyer-legal-registration-identifier>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'VA']])">
          <buyer-vat-identifier id="bt-48">
            <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'VA']]/ram:ID"/>
            </content>
          </buyer-vat-identifier>
        </xsl:if>
        <buyer-electronic-address id="bt-49">
          <content>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:URIUniversalCommunication/ram:URIID"/>
          </content>
          <scheme-identifier>
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:URIUniversalCommunication/ram:URIID/@schemeID"/>
          </scheme-identifier>
        </buyer-electronic-address>
        <buyer-postal-address id="bg-8">
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:LineOne)">
            <buyer-address-line-1 id="bt-50">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:LineOne"/>
            </buyer-address-line-1>
          </xsl:if>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:LineTwo)">
            <buyer-address-line-2 id="bt-51">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:LineTwo"/>
            </buyer-address-line-2>
          </xsl:if>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:LineThree)">
            <buyer-address-line-3 id="bt-163">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:LineThree"/>
            </buyer-address-line-3>
          </xsl:if>
          <buyer-city id="bt-52">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:CityName"/>
          </buyer-city>
          <buyer-post-code id="bt-53">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:PostcodeCode"/>
          </buyer-post-code>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName)">
            <buyer-country-subdivision id="bt-54">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName"/>
            </buyer-country-subdivision>
          </xsl:if>
          <buyer-country-code id="bt-55">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:PostalTradeAddress/ram:CountryID"/>
          </buyer-country-code>
        </buyer-postal-address>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:PersonName)
            or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:TelephoneUniversalCommunication/ram:CompleteNumber)
            or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:EmailURIUniversalCommunication/ram:URIID)">
          <buyer-contact id="bg-9">
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:PersonName)">
              <buyer-contact-point id="bt-56">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:PersonName"/>
              </buyer-contact-point>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:TelephoneUniversalCommunication/ram:CompleteNumber)">
              <buyer-contact-telephone-number id="bt-57">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:TelephoneUniversalCommunication/ram:CompleteNumber"/>
              </buyer-contact-telephone-number>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:EmailURIUniversalCommunication/ram:URIID)">
              <buyer-contact-email-address id="bt-58">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:BuyerTradeParty/ram:DefinedTradeContact/ram:EmailURIUniversalCommunication/ram:URIID"/>
              </buyer-contact-email-address>
            </xsl:if>
          </buyer-contact>
        </xsl:if>
      </buyer>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:Name)">
        <payee id="bg-10">
          <payee-name id="bt-59">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:Name"/>
          </payee-name>
          <xsl:choose>
            <xsl:when test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:GlobalID)">
              <payee-identifier id="bt-60">
                <content>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:GlobalID"/>
                </content>
                <scheme-identifier>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:GlobalID/@schemeID"/>
                </scheme-identifier>
              </payee-identifier>
            </xsl:when>
            <xsl:when test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:ID)">
              <payee-identifier id="bt-60">
                <content>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:ID"/>
                </content>
              </payee-identifier>
            </xsl:when>
          </xsl:choose>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:SpecifiedLegalOrganization/ram:ID)">
            <payee-legal-registration-identifier id="bt-61">
              <content>
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:SpecifiedLegalOrganization/ram:ID"/>
              </content>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:SpecifiedLegalOrganization/ram:ID[@schemeID])">
                <scheme-identifier>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PayeeTradeParty/ram:SpecifiedLegalOrganization/ram:ID/@schemeID"/>
                </scheme-identifier>
              </xsl:if>
            </payee-legal-registration-identifier>
          </xsl:if>
        </payee>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:Name)">
        <seller-tax-representative-party id="bg-11">
          <seller-tax-representative-name id="bt-62">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:Name"/>
          </seller-tax-representative-name>
          <seller-tax-representative-vat-identifier id="bt-63">
            <content>
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:SpecifiedTaxRegistration[ram:ID[upper-case(@schemeID) = 'VA']]/ram:ID"/>
            </content>
          </seller-tax-representative-vat-identifier>
          <seller-tax-representative-postal-address id="bg-12">
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:LineOne)">
              <tax-representative-address-line-1 id="bt-64">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:LineOne"/>
              </tax-representative-address-line-1>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:LineTwo)">
              <tax-representative-address-line-2 id="bt-65">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:LineTwo"/>
              </tax-representative-address-line-2>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:LineThree)">
              <tax-representative-address-line-3 id="bt-164">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:LineThree"/>
              </tax-representative-address-line-3>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:CityName)">
              <tax-representative-city id="bt-66">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:CityName"/>
              </tax-representative-city>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:PostcodeCode)">
              <tax-representative-post-code id="bt-67">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:PostcodeCode"/>
              </tax-representative-post-code>
            </xsl:if>
            <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName)">
              <tax-representative-country-subdivision id="bt-68">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName"/>
              </tax-representative-country-subdivision>
            </xsl:if>
            <tax-representative-country-code id="bt-69">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeAgreement/ram:SellerTaxRepresentativeTradeParty/ram:PostalTradeAddress/ram:CountryID"/>
            </tax-representative-country-code>
          </seller-tax-representative-postal-address>
        </seller-tax-representative-party>
      </xsl:if>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:Name)
          or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:GlobalID)
          or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:ID)
          or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ActualDeliverySupplyChainEvent/ram:OccurrenceDateTime/udt:DateTimeString[@format = '102'])
          or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:StartDateTime/udt:DateTimeString[@format = '102'])
          or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:EndDateTime/udt:DateTimeString[@format = '102'])
          or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:CityName)">
        <delivery-information id="bg-13">
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:Name)">
            <deliver-to-party-name id="bt-70">
              <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:Name"/>
            </deliver-to-party-name>
          </xsl:if>
          <xsl:choose>
            <xsl:when test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:GlobalID)">
              <deliver-to-location-identifier id="bt-71">
                <content>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:GlobalID"/>
                </content>
                <scheme-identifier>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:GlobalID/@schemeID"/>
                </scheme-identifier>
              </deliver-to-location-identifier>
            </xsl:when>
            <xsl:when test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:ID)">
              <deliver-to-location-identifier id="bt-71">
                <content>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:ID"/>
                </content>
              </deliver-to-location-identifier>
            </xsl:when>
          </xsl:choose>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ActualDeliverySupplyChainEvent/ram:OccurrenceDateTime/udt:DateTimeString[@format = '102'])">
            <actual-delivery-date id="bt-72">
              <xsl:call-template name="date">
                <xsl:with-param name="node" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ActualDeliverySupplyChainEvent/ram:OccurrenceDateTime/udt:DateTimeString[@format = '102']"/>
              </xsl:call-template>
            </actual-delivery-date>
          </xsl:if>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:StartDateTime/udt:DateTimeString[@format = '102'])
              or exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:EndDateTime/udt:DateTimeString[@format = '102'])">
            <invoicing-period id="bg-14">
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:StartDateTime/udt:DateTimeString[@format = '102'])">
                <invoicing-period-start-date id="bt-73">
                  <xsl:call-template name="date">
                    <xsl:with-param name="node" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:StartDateTime/udt:DateTimeString[@format = '102']"/>
                  </xsl:call-template>
                </invoicing-period-start-date>
              </xsl:if>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:EndDateTime/udt:DateTimeString[@format = '102'])">
                <invoicing-period-end-date id="bt-74">
                  <xsl:call-template name="date">
                    <xsl:with-param name="node" select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:BillingSpecifiedPeriod/ram:EndDateTime/udt:DateTimeString[@format = '102']"/>
                  </xsl:call-template>
                </invoicing-period-end-date>
              </xsl:if>
            </invoicing-period>
          </xsl:if>
          <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:CityName)">
            <deliver-to-address id="bg-15">
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:LineOne)">
                <deliver-to-address-line-1 id="bt-75">
                  <xsl:value-of select="/rsm:CrossIndustryInvoice/rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:LineOne"/>
                </deliver-to-address-line-1>
              </xsl:if>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:LineTwo)">
                <deliver-to-address-line-2 id="bt-76">
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:LineTwo"/>
                </deliver-to-address-line-2>
              </xsl:if>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:LineThree)">
                <deliver-to-address-line-3 id="bt-165">
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:LineThree"/>
                </deliver-to-address-line-3>
              </xsl:if>
              <deliver-to-city id="bt-77">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:CityName"/>
              </deliver-to-city>
              <deliver-to-post-code id="bt-78">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:PostcodeCode"/>
              </deliver-to-post-code>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName)">
                <deliver-to-country-subdivision id="bt-79">
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:CountrySubDivisionName"/>
                </deliver-to-country-subdivision>
              </xsl:if>
              <deliver-to-country-code id="bt-80">
                <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeDelivery/ram:ShipToTradeParty/ram:PostalTradeAddress/ram:CountryID"/>
              </deliver-to-country-code>
            </deliver-to-address>
          </xsl:if>
        </delivery-information>
      </xsl:if>
      <payment-instructions id="bg-16">
        <payment-means-type-code id="bt-81">
          <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:TypeCode"/>
        </payment-means-type-code>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:Information)">
          <payment-means-text id="bt-82">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:Information"/>
          </payment-means-text>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PaymentReference)">
          <remittance-information id="bt-83">
            <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:PaymentReference"/>
          </remittance-information>
        </xsl:if>
        <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:PayeePartyCreditorFinancialAccount/ram:IBANID)">
          <!-- Note that CII does not actually support multiple bg-17 instances -->
          <credit-transfers id="bg-17">
            <credit-transfer id="bg-17">
              <payment-account-identifier id="bt-84">
                <content>
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:PayeePartyCreditorFinancialAccount/ram:IBANID"/>
                </content>
              </payment-account-identifier>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:PayeePartyCreditorFinancialAccount/ram:AccountName)">
                <payment-account-name id="bt-85">
                  <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:PayeePartyCreditorFinancialAccount/ram:AccountName"/>
                </payment-account-name>
              </xsl:if>
              <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:PayeeSpecifiedCreditorFinancialInstitution/ram:BICID)">
                <payment-service-provider-identifier id="bt-86">
                  <content>
                    <xsl:value-of select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeSettlementPaymentMeans/ram:PayeeSpecifiedCreditorFinancialInstitution/ram:BICID"/>
                  </content>
                </payment-service-provider-identifier>
              </xsl:if>
            </credit-transfer>
          </credit-transfers>
        </xsl:if>
        <!-- TODO
        <xsl:if test="exists()">
          <payment-card-information id="bg-18">
            <payment-card-primary-account-number id="bt-87">
              <xsl:value-of select=""/>
            </payment-card-primary-account-number>
            <xsl:if test="exists()">
              <payment-card-holder-name id="bt-88">
                <xsl:value-of select=""/>
              </payment-card-holder-name>
            </xsl:if>
          </payment-card-information>
        </xsl:if>
        <xsl:if test="exists()">
          <direct-debit id="bg-19">
            <mandate-reference-identifier id="bt-89">
              <content>
                <xsl:value-of select=""/>
              </content>
            </mandate-reference-identifier>
            <bank-assigned-creditor-identifier id="bt-90">
              <content>
                <xsl:value-of select=""/>
              </content>
            </bank-assigned-creditor-identifier>
            <debited-account-identifier id="bt-91">
              <content>
                <xsl:value-of select=""/>
              </content>
            </debited-account-identifier>
          </direct-debit>
        </xsl:if>
        -->
      </payment-instructions>
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeAllowanceCharge[ram:ChargeIndicator/udt:Indicator = 'false']/ram:ActualAmount)">
        <document-level-allowances id="bg-20">
          <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeAllowanceCharge[ram:ChargeIndicator/udt:Indicator = 'false']">
            <document-level-allowance id="bg-20">
              <document-level-allowance-amount id="bt-92">
                <xsl:value-of select="./ram:ActualAmount"/>
              </document-level-allowance-amount>
              <xsl:if test="exists(./ram:BasisAmount)">
                <document-level-allowance-base-amount id="bt-93">
                  <xsl:value-of select="./ram:BasisAmount"/>
                </document-level-allowance-base-amount>
              </xsl:if>
              <xsl:if test="exists(./ram:CalculationPercent)">
                <document-level-allowance-percentage id="bt-94">
                  <xsl:value-of select="./ram:CalculationPercent"/>
                </document-level-allowance-percentage>
              </xsl:if>
              <document-level-allowance-vat-category-code id="bt-95">
                <xsl:value-of select="./ram:CategoryTradeTax[normalize-space(upper-case(ram:TypeCode)) = 'VAT']/ram:CategoryCode"/>
              </document-level-allowance-vat-category-code>
              <xsl:if test="exists(./ram:CategoryTradeTax[normalize-space(upper-case(ram:TypeCode)) = 'VAT']/ram:RateApplicablePercent)">
                <document-level-allowance-vat-rate id="bt-96">
                  <xsl:value-of select="./ram:CategoryTradeTax[normalize-space(upper-case(ram:TypeCode)) = 'VAT']/ram:RateApplicablePercent"/>
                </document-level-allowance-vat-rate>
              </xsl:if>
              <xsl:if test="exists(./ram:Reason)">
                <document-level-allowance-reason id="bt-97">
                  <xsl:value-of select="./ram:Reason"/>
                </document-level-allowance-reason>
              </xsl:if>
              <xsl:if test="exists(./ram:ReasonCode)">
                <document-level-allowance-reason-code id="bt-98">
                  <xsl:value-of select="./ram:ReasonCode"/>
                </document-level-allowance-reason-code>
              </xsl:if>
            </document-level-allowance>
          </xsl:for-each>
        </document-level-allowances>
      </xsl:if>
      <!-- TODO: bg-21 -->
      <xsl:if test="exists(rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeAllowanceCharge[ram:ChargeIndicator/udt:Indicator = 'true']/ram:ActualAmount)">
        <document-level-charges id="bg-21">
          <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:SpecifiedTradeAllowanceCharge[ram:ChargeIndicator/udt:Indicator = 'true']">
            <document-level-charge id="bg-21">
              <document-level-charge-amount id="bt-99">
                <xsl:value-of select="./ram:ActualAmount"/>
              </document-level-charge-amount>
              <xsl:if test="exists(./ram:BasisAmount)">
                <document-level-charge-base-amount id="bt-100">
                  <xsl:value-of select="./ram:BasisAmount"/>
                </document-level-charge-base-amount>
              </xsl:if>
              <xsl:if test="exists(./ram:CalculationPercent)">
                <document-level-charge-percentage id="bt-101">
                  <xsl:value-of select="./ram:CalculationPercent"/>
                </document-level-charge-percentage>
              </xsl:if>
              <document-level-charge-vat-category-code id="bt-102">
                <xsl:value-of select="./ram:CategoryTradeTax[normalize-space(upper-case(ram:TypeCode)) = 'VAT']/ram:CategoryCode"/>
              </document-level-charge-vat-category-code>
              <xsl:if test="exists(./ram:CategoryTradeTax[normalize-space(upper-case(ram:TypeCode)) = 'VAT']/ram:RateApplicablePercent)">
                <document-level-charge-vat-rate id="bt-103">
                  <xsl:value-of select="./ram:CategoryTradeTax[normalize-space(upper-case(ram:TypeCode)) = 'VAT']/ram:RateApplicablePercent"/>
                </document-level-charge-vat-rate>
              </xsl:if>
              <xsl:if test="exists(./ram:Reason)">
                <document-level-charge-reason id="bt-104">
                  <xsl:value-of select="./ram:Reason"/>
                </document-level-charge-reason>
              </xsl:if>
              <xsl:if test="exists(./ram:ReasonCode)">
                <document-level-charge-reason-code id="bt-105">
                  <xsl:value-of select="./ram:ReasonCode"/>
                </document-level-charge-reason-code>
              </xsl:if>
            </document-level-charge>
          </xsl:for-each>
        </document-level-charges>
      </xsl:if>
      <!-- TODO: bg-22 -->
      <document-totals id="bg-22">
      </document-totals>
      <vat-breakdown id="bg-23">
        <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:ApplicableHeaderTradeSettlement/ram:ApplicableTradeTax[upper-case(ram:TypeCode) = 'VAT']">
          <vat-breakdown id="bg-23">
            <vat-category-taxable-amount id="bt-116">
              <xsl:value-of select="./ram:BasisAmount"/>
            </vat-category-taxable-amount>
            <vat-category-tax-amount id="bt-117">
              <xsl:value-of select="./ram:CalculatedAmount"/>
            </vat-category-tax-amount>
            <vat-category-code id="bt-118">
              <xsl:value-of select="./ram:CategoryCode"/>
            </vat-category-code>
            <vat-category-rate id="bt-119">
              <xsl:value-of select="./ram:RateApplicablePercent"/>
            </vat-category-rate>
          </vat-breakdown>
        </xsl:for-each>
      </vat-breakdown>
      <!-- TODO: bg-24 -->
      <!-- TypeCode = '916' -->
      <invoice-lines id="bg-25">
        <xsl:for-each select="rsm:SupplyChainTradeTransaction/ram:IncludedSupplyChainTradeLineItem">
          <invoice-line id="bg-25">
            <invoice-line-identifier id="bt-126">
              <content>
                <xsl:value-of select="./ram:AssociatedDocumentLineDocument/ram:LineID"/>
              </content>
            </invoice-line-identifier>
            <!-- TODO: bt-128 -->
            <!-- TypeCode = '130' -->
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
              <invoiced-item-vat-category-code id="bt-151">
                <xsl:value-of select="./ram:SpecifiedLineTradeSettlement/ram:ApplicableTradeTax[upper-case(ram:TypeCode) = 'VAT']/ram:CategoryCode"/>
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
