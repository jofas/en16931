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
        <xsl:element name="content">
          <xsl:value-of select="cbc:ID"/>
        </xsl:element>
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
      <xsl:if test="exists(cbc:TaxCurrencyCode)">
        <xsl:element name="vat-accounting-currency-code">
          <xsl:attribute name="id">bt-6</xsl:attribute>
          <xsl:value-of select="cbc:TaxCurrencyCode"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:TaxPointDate)">
        <xsl:element name="value-added-tax-point-date">
          <xsl:attribute name="id">bt-7</xsl:attribute>
          <xsl:value-of select="cbc:TaxPointDate"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:InvoicePeriod/cbc:DescriptionCode)">
        <xsl:element name="value-added-tax-point-date-code">
          <xsl:attribute name="id">bt-8</xsl:attribute>
          <xsl:value-of select="cac:InvoicePeriod/cbc:DescriptionCode"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:DueDate)">
        <xsl:element name="payment-due-date">
          <xsl:attribute name="id">bt-9</xsl:attribute>
          <xsl:value-of select="cbc:DueDate"/>
        </xsl:element>
      </xsl:if>
      <xsl:element name="buyer-reference">
        <xsl:attribute name="id">bt-10</xsl:attribute>
        <xsl:value-of select="cbc:BuyerReference"/>
      </xsl:element>
      <xsl:if test="exists(cac:ProjectReference/cbc:ID)">
        <xsl:element name="project-reference">
          <xsl:attribute name="id">bt-11</xsl:attribute>
          <xsl:value-of select="cac:ProjectReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:ContractDocumentReference/cbc:ID)">
        <xsl:element name="contract-reference">
          <xsl:attribute name="id">bt-12</xsl:attribute>
          <xsl:value-of select="cac:ContractDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:OrderReference/cbc:ID)">
        <xsl:element name="purchase-order-reference">
          <xsl:attribute name="id">bt-13</xsl:attribute>
          <xsl:value-of select="cac:OrderReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:OrderReference/cbc:SalesOrderID)">
        <xsl:element name="sales-order-reference">
          <xsl:attribute name="id">bt-14</xsl:attribute>
          <xsl:value-of select="cac:OrderReference/cbc:SalesOrderID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:ReceiptDocumentReference/cbc:ID)">
        <xsl:element name="receiving-advice-reference">
          <xsl:attribute name="id">bt-15</xsl:attribute>
          <xsl:value-of select="cac:ReceiptDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:DespatchDocumentReference/cbc:ID)">
        <xsl:element name="despatch-advice-reference">
          <xsl:attribute name="id">bt-16</xsl:attribute>
          <xsl:value-of select="cac:DespatchDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:OriginatorDocumentReference/cbc:ID)">
        <xsl:element name="tender-or-lot-reference">
          <xsl:attribute name="id">bt-17</xsl:attribute>
          <xsl:value-of select="cac:OriginatorDocumentReference/cbc:ID"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID)">
        <xsl:element name="invoiced-object-identifier">
          <xsl:attribute name="id">bt-18</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID"/>
          </xsl:element>
          <xsl:element name="scheme-identifier">
            <xsl:value-of select="cac:AdditionalDocumentReference[cbc:DocumentTypeCode = '130']/cbc:ID/@schemeID"/>
          </xsl:element>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:AccountingCost)">
        <xsl:element name="buyer-accounting-reference">
          <xsl:attribute name="id">bt-19</xsl:attribute>
          <xsl:value-of select="cbc:AccountingCost"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cac:PaymentTerms/cbc:Note)">
        <xsl:element name="payment-terms">
          <xsl:attribute name="id">bt-20</xsl:attribute>
          <xsl:value-of select="cac:PaymentTerms/cbc:Note"/>
        </xsl:element>
      </xsl:if>
      <xsl:if test="exists(cbc:Note)">
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
      </xsl:if>
      <xsl:element name="process-control">
        <xsl:attribute name="id">bg-2</xsl:attribute>
        <xsl:element name="business-process-type">
          <xsl:attribute name="id">bt-23</xsl:attribute>
          <xsl:value-of select="cbc:ProfileID"/>
        </xsl:element>
        <xsl:element name="specification-identifier">
          <xsl:attribute name="id">bt-24</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cbc:CustomizationID"/>
          </xsl:element>
        </xsl:element>
      </xsl:element>
      <xsl:if test="cac:BillingReference">
        <xsl:element name="preceding-invoice-references">
          <xsl:attribute name="id">bg-3</xsl:attribute>
          <xsl:for-each select="cac:BillingReference">
            <xsl:element name="preceding-invoice-reference">
              <xsl:attribute name="id">bg-3</xsl:attribute>
              <xsl:element name="preceding-invoice-reference">
                <xsl:attribute name="id">bt-25</xsl:attribute>
                <xsl:value-of select="./cac:InvoiceDocumentReference/cbc:ID"/>
              </xsl:element>
              <xsl:if test="exists(./cac:InvoiceDocumentReference/cbc:IssueDate)">
                <xsl:element name="preceding-invoice-issue-date">
                  <xsl:attribute name="id">bt-26</xsl:attribute>
                  <xsl:value-of select="./cac:InvoiceDocumentReference/cbc:IssueDate"/>
                </xsl:element>
              </xsl:if>
            </xsl:element>
          </xsl:for-each>
        </xsl:element>
      </xsl:if>
      <xsl:element name="seller">
        <xsl:attribute name="id">bg-4</xsl:attribute>
        <xsl:element name="seller-name">
          <xsl:attribute name="id">bt-27</xsl:attribute>
          <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName"/>
        </xsl:element>
        <xsl:if test="cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name">
          <xsl:element name="seller-trading-name">
            <xsl:attribute name="id">bt-28</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyName/cbc:Name"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[(normalize-space(upper-case(@schmeID)) != 'SEPA')])">
          <xsl:element name="seller-identifiers">
            <xsl:attribute name="id">bt-29</xsl:attribute>
            <xsl:for-each select="cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification[(normalize-space(upper-case(@schmeID)) != 'SEPA')]">
              <xsl:element name="seller-identifier">
                <xsl:attribute name="id">bt-29</xsl:attribute>
                <xsl:element name="content">
                  <xsl:value-of select="./cbc:ID"/>
                </xsl:element>
                <xsl:if test="exists(./cbc:ID[@schemeID])">
                  <xsl:element name="scheme-identifier">
                    <xsl:value-of select="./cbc:ID/@schemeID"/>
                  </xsl:element>
                </xsl:if>
              </xsl:element>
            </xsl:for-each>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID)">
          <xsl:element name="seller-legal-registration-identifier">
            <xsl:attribute name="id">bt-30</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID"/>
            </xsl:element>
            <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID[@schemeID])">
              <xsl:element name="scheme-identifier">
                <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID/@schemeID]"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID)">
          <xsl:element name="seller-vat-identifier">
            <xsl:attribute name="id">bt-31</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
            </xsl:element>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) != 'VAT')]/cbc:CompanyID)">
          <xsl:element name="seller-tax-registration-identifier">
            <xsl:attribute name="id">bt-32</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) != 'VAT')]/cbc:CompanyID"/>
            </xsl:element>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyLegalForm)">
          <xsl:element name="seller-additional-legal-information">
            <xsl:attribute name="id">bt-33</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyLegalForm"/>
          </xsl:element>
        </xsl:if>
        <xsl:element name="seller-electronic-address">
          <xsl:attribute name="id">bt-34</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cbc:EndpointID"/>
          </xsl:element>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cbc:EndpointID[@schemeID])">
            <xsl:element name="scheme-identifier">
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cbc:EndpointID/@schemeID]"/>
            </xsl:element>
          </xsl:if>
        </xsl:element>
        <xsl:element name="seller-postal-address">
          <xsl:attribute name="id">bg-5</xsl:attribute>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:StreetName)">
            <xsl:element name="seller-address-line-1">
              <xsl:attribute name="id">bt-35</xsl:attribute>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:StreetName"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName)">
            <xsl:element name="seller-address-line-2">
              <xsl:attribute name="id">bt-36</xsl:attribute>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line)">
            <xsl:element name="seller-address-line-3">
              <xsl:attribute name="id">bt-162</xsl:attribute>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line"/>
            </xsl:element>
          </xsl:if>
          <xsl:element name="seller-city">
            <xsl:attribute name="id">bt-37</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName"/>
          </xsl:element>
          <xsl:element name="seller-post-code">
            <xsl:attribute name="id">bt-38</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone"/>
          </xsl:element>
          <xsl:if test="exists(cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity)">
            <xsl:element name="seller-address-line-3">
              <xsl:attribute name="id">bt-39</xsl:attribute>
              <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity"/>
            </xsl:element>
          </xsl:if>
          <xsl:element name="seller-post-code">
            <xsl:attribute name="id">bt-40</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
          </xsl:element>
        </xsl:element>
        <xsl:element name="seller-contact">
          <xsl:attribute name="id">bg-6</xsl:attribute>
          <xsl:element name="seller-contact-point">
            <xsl:attribute name="id">bt-41</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:Name"/>
          </xsl:element>
          <xsl:element name="seller-contact-telephone-number">
            <xsl:attribute name="id">bt-42</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:Telephone"/>
          </xsl:element>
          <xsl:element name="seller-contact-email-address">
            <xsl:attribute name="id">bt-43</xsl:attribute>
            <xsl:value-of select="cac:AccountingSupplierParty/cac:Party/cac:Contact/cbc:ElectronicMail"/>
          </xsl:element>
        </xsl:element>
      </xsl:element>
      <xsl:element name="buyer">
        <xsl:attribute name="id">bg-7</xsl:attribute>
        <xsl:element name="buyer-name">
          <xsl:attribute name="id">bt-44</xsl:attribute>
          <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:RegistrationName"/>
        </xsl:element>
        <xsl:if test="cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name">
          <xsl:element name="buyer-trading-name">
            <xsl:attribute name="id">bt-45</xsl:attribute>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyName/cbc:Name"/>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID)">
          <xsl:element name="buyer-identifier">
            <xsl:attribute name="id">bt-46</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID"/>
            </xsl:element>
            <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification)/cbc:ID[@schemeID])">
              <xsl:element name="scheme-identifier">
                <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification/cbc:ID@schemeID"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID)">
          <xsl:element name="buyer-legal-registration-identifier">
            <xsl:attribute name="id">bt-47</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID"/>
            </xsl:element>
            <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID[@schemeID])">
              <xsl:element name="scheme-identifier">
                <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID/@schemeID]"/>
              </xsl:element>
            </xsl:if>
          </xsl:element>
        </xsl:if>
        <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID)">
          <xsl:element name="buyer-vat-identifier">
            <xsl:attribute name="id">bt-48</xsl:attribute>
            <xsl:element name="content">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme[cac:TaxScheme/(normalize-space(upper-case(cbc:ID)) = 'VAT')]/cbc:CompanyID"/>
            </xsl:element>
          </xsl:element>
        </xsl:if>
        <xsl:element name="buyer-electronic-address">
          <xsl:attribute name="id">bt-49</xsl:attribute>
          <xsl:element name="content">
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cbc:EndpointID"/>
          </xsl:element>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cbc:EndpointID[@schemeID])">
            <xsl:element name="scheme-identifier">
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cbc:EndpointID/@schemeID]"/>
            </xsl:element>
          </xsl:if>
        </xsl:element>
        <xsl:element name="buyer-postal-address">
          <xsl:attribute name="id">bg-8</xsl:attribute>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:StreetName)">
            <xsl:element name="buyer-address-line-1">
              <xsl:attribute name="id">bt-50</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:StreetName"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName)">
            <xsl:element name="buyer-address-line-2">
              <xsl:attribute name="id">bt-51</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:AdditionalStreetName"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line)">
            <xsl:element name="buyer-address-line-3">
              <xsl:attribute name="id">bt-163</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:AddressLine/cbc:Line"/>
            </xsl:element>
          </xsl:if>
          <xsl:element name="buyer-city">
            <xsl:attribute name="id">bt-52</xsl:attribute>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CityName"/>
          </xsl:element>
          <xsl:element name="buyer-post-code">
            <xsl:attribute name="id">bt-53</xsl:attribute>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:PostalZone"/>
          </xsl:element>
          <xsl:if test="exists(cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity)">
            <xsl:element name="buyer-address-line-3">
              <xsl:attribute name="id">bt-54</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cbc:CountrySubentity"/>
            </xsl:element>
          </xsl:if>
          <xsl:element name="buyer-post-code">
            <xsl:attribute name="id">bt-55</xsl:attribute>
            <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
          </xsl:element>
        </xsl:element>
        <xsl:element name="buyer-contact">
          <xsl:attribute name="id">bg-9</xsl:attribute>
          <xsl:if select="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Name)">
            <xsl:element name="buyer-contact-point">
              <xsl:attribute name="id">bt-56</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Name"/>
            </xsl:element>
          </xsl:if>
          <xsl:if select="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Telephone)">
            <xsl:element name="buyer-contact-telephone-number">
              <xsl:attribute name="id">bt-57</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:Telephone"/>
            </xsl:element>
          </xsl:if>
          <xsl:if select="exists(cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail)">
            <xsl:element name="buyer-contact-email-address">
              <xsl:attribute name="id">bt-58</xsl:attribute>
              <xsl:value-of select="cac:AccountingCustomerParty/cac:Party/cac:Contact/cbc:ElectronicMail"/>
            </xsl:element>
          </xsl:if>
        </xsl:element>
      </xsl:element>
      <xsl:if test="false()">
        <xsl:element name="payee">
          <xsl:attribute name="id">bg-10</xsl:attribute>
          <!--
            * payee-name bt-59 1
            * payee-identifier bt-60 ? (schemeID)
            * payee-legal-registration-identifier bt-61 ? (schemeID)
          -->
        </xsl:element>
      </xsl:if>
      <xsl:if test="false()">
        <xsl:element name="seller-tax-representative-party">
          <xsl:attribute name="id">bg-11</xsl:attribute>
          <!--
            * seller-tax-representative-name bt-62 1
            * seller-tax-representative-vat-identifier bt-63 1
            * seller-tax-representative-postal-address bg-12 1
              - tax-representative-address-line-1 bt-64 ?
              - tax-representative-address-line-2 bt-65 ?
              - tax-representative-address-line-3 bt-164 ?
              - tax-representative-city bt-66 ?
              - tax-representative-post-code bt-67 ?
              - tax-representative-country-subdivision bt-68 ?
              - tax-representative-country-code bt-69 1
          -->
        </xsl:element>
      </xsl:if>
      <xsl:if test="false()">
        <xsl:element name="delivery-information">
          <xsl:attribute name="id">bg-13</xsl:attribute>
          <!--
            * deliver-to-party-name bt-70 ?
            * deliver-to-location-identifier bt-71 ? (schemeID)
            * actual-delivery-date bt-72 ?
            * invoicing-period bg-14 ?
              - invoicing-period-start-date bt-73 ?
              - invoicing-period-end-date bt-74 ?
            * deliver-to-address bg-15 ?
              - deliver-to-address-line-1 bt-75 ?
              - deliver-to-address-line-2 bt-76 ?
              - deliver-to-address-line-3 bt-165 ?
              - deliver-to-city bt-77 1
              - deliver-to-post-code bt-78 1
              - deliver-to-country-subdivision bt-79 ?
              - deliver-to-country-code bt-80 1
          -->
        </xsl:element>
      </xsl:if>
      <xsl:element name="payment-instructions">
        <xsl:attribute name="id">bg-16</xsl:attribute>
        <!--
          * payment-means-type-code bt-81 1
          * payment-means-text bt-82 ?
          * remittance-information bt-83 ?
          * credit-transfer bg-17 *
            - payment-account-identifier bt-84 1
            - payment-account-name bt-85 ?
            - payment-service-provider-identifier bt-86 ?
          * payment-card-information bg-18 ?
            - payment-card-primary-account-number bt-87 1
            - payment-card-holder-name bt-88 ?
          * direct-debit bg-19 ?
            - mandate-reference-identifier bt-89 1
            - bank-assigned-creditor-identifier bt-90 1
            - debited-account-identifier bt-91 1
        -->
      </xsl:element>
      <xsl:if test="false()">
        <xsl:element name="document-level-allowances">
          <xsl:attribute name="id">bg-20</xsl:attribute>
          <!--
            * document-level-allowance-amount bt-92 1
            * document-level-allowance-base-amount bt-93 ?
            * document-level-allowance-percentage bt-94 ?
            * document-level-allowance-vat-category-code bt-95 1
            * document-level-allowance-vat-rate bt-96 ?
            * document-level-allowance-reason bt-97 ?
            * document-level-allowance-reason-code bt-98 ?
          -->
          <!-- list -->
        </xsl:element>
      </xsl:if>
      <xsl:if test="false()">
        <xsl:element name="document-level-charges">
          <xsl:attribute name="id">bg-21</xsl:attribute>
          <!--
            * document-level-charge-amount bt-99 1
            * document-level-charge-base-amount bt-100 ?
            * document-level-charge-percentage bt-101 ?
            * document-level-charge-vat-category-code bt-102 1
            * document-level-charge-vat-rate bt-103 ?
            * document-level-charge-reason bt-104 ?
            * document-level-charge-reason-code bt-105 ?
          -->
          <!-- list -->
        </xsl:element>
      </xsl:if>
      <xsl:element name="document-totals">
        <xsl:attribute name="id">bg-22</xsl:attribute>
        <!--
          * sum-of-invoice-line-net-amount bt-106 1
          * sum-of-allowances-on-document-level bt-107 ?
          * sum-of-charges-on-document-level bt-108 ?
          * invoice-total-amount-without-vat bt-109 1
          * invoice-total-vat-amount bt-110 ?
          * invoice-total-vat-amount-in-accounting-currency bt-111 ?
          * invoice-total-amount-with-vat bt-112 1
          * paid-amount bt-113 ?
          * rounding-amount bt-114 ?
          * amount-due-for-payment bt-115 1
        -->
      </xsl:element>
      <xsl:element name="vat-breakdown">
        <xsl:attribute name="id">bg-23</xsl:attribute>
        <!--
          * vat-category-taxable-amount bt-116 1
          * vat-category-tax-amount bt-117 1
          * vat-category-code bt-118 1
          * vat-category-rate bt-119 1
          * vat-exemption-reason-text bt-120 ?
          * vat-exemption-reason-code bt-121 ?
        -->
        <!-- list -->
      </xsl:element>
      <xsl:if test="false()">
        <xsl:element name="additional-supporting-documents">
          <xsl:attribute name="id">bg-24</xsl:attribute>
          <!--
            * supporting-document-reference bt-122 1
            * supporting-document-description bt-123 ?
            * external-document-location bt-124 ?
            * attached-document bt-125 ? (binary object)
          -->
          <!-- list -->
        </xsl:element>
        <xsl:element name="invoice-lines">
          <xsl:attribute name="id">bg-25</xsl:attribute>
          <!--
            * invoice-line-identifier bt-126 1
            * invoice-line-note bt-127 ?
            * invoice-line-object-identifier bt-128 ? (schemeID)
            * invoiced-quantity bt-129 1
            * invoiced-quantity-unit-of-measure-code bt-130 1
            * invoice-line-net-amount bt-131 1
            * referenced-purchase-order-line-reference bt-132 ?
            * invoice-line-buyer-accounting-reference bt-133 ?
            * invoice-line-period bg-26 ?
              - invoice-line-period-start-date bt-134 ?
              - invoice-line-period-end-date bt-135 ?
            * invoice-line-allowances bg-27 *
              - invoice-line-allowance-amount bt-136 1
              - invoice-line-allowance-base-amount bt-137 ?
              - invoice-line-allowance-percentage bt-138 ?
              - invoice-line-allowance-reason bt-139 ?
              - invoice-line-allowance-reason-code bt-140 ?
            * invoice-line-charges bg-28 *
              - invoice-line-charge-amount bt-141 1
              - invoice-line-charge-base-amount bt-142 ?
              - invoice-line-charge-percentage bt-143 ?
              - invoice-line-charge-reason bt-144 ?
              - invoice-line-charge-reason-code bt-145 ?
            * price-details bg-29 1
              - item-net-price bt-146 1
              - item-price-discount bt-147 ?
              - item-gross-price bt-148 ?
              - item-price-base-quantity bt-149 ?
              - item-price-base-quantity-unit-of-measure bt-150 ?
            * line-vat-information bg-30 1
              - invoiced-item-vat-category-code bt-151 1
              - invoiced-item-vat-rate bt-152 ?
            * item-information bg-31 1
              - item-name bt-153 1
              - item-description bt-154 ?
              - item-sellers-identifier bt-155 ?
              - item-buyers-identifier bt-156 ?
              - item-standard-identifier bt-157 ? (schemeID)
              - item-classification-identifier bt-158 * (schemeID)
              - item-country-of-origin bt-159 ?
              - item-attributes bg-32 *
                + item-attribute-name bt-160 1
                + item-attribute-value bt-161 1
          -->
          <!-- list -->
        </xsl:element>
      </xsl:if>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
