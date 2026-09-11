# FAQ

## Is CII D22B fully compliant with the EN16931:2017 data model?

Yes. It allows multiple BG-3, which CII D16B doesn't. 
Otherwise, D16B is able to fully represent the EN16931:2017 data model.
However, that does not mean that a standard automatically also 
supports D22B, if it is only meant to target D16B.
D22B is more expansive in what documents it allows, beyond multiple BG-3.
This makes it necessary for the standard to add more syntax rules to prevent
malformed documents.
See also the 
[Are the XRechnung v3 Schematrons compatible with CII D22B?](#are-the-xrechnung-v3-schematrons-compatible-with-cii-d22b) 
question.

## Are the XRechnung v3 schematrons compatible with CII D22B?

No. Both the core EN16931 schematron and the XRechnung schematron for 
CII miss a rule akin to Factur-X's FX-SCH-A-000194 that prohibits multiple
`ram:PayeePartyCreditorFinancialAccount` elements.
Multiple `ram:PayeePartyCreditorFinancialAccount` elements are arguably resulting
in a malformed document that should not pass validation (something the D16B
schema prevents).
There might be more syntax expansions in the D22B version that require additional 
rules enforced by the standard.
Therefore, the policy of this library is: as long as the standard 
doesn't explicitly state support for D22B, this library rejects D22B documents
for that standard.
