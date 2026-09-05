# FAQ

## Does CII D22B support multiple BG-17 elements? (I.e. is it fully compliant with the EN16931 data model?)

No. Multiple `ram:PayeePartyCreditorFinancialAccount` elements
can be provided (contains fields BT-84 and BT-85 of BG-17), but
only one `ram:PayeeSpecifiedCreditorFinancialInstitution` (BT-86).

Factur-X 1.09 rule FX-SCH-A-000194 prohibits multiple 
`ram:PayeePartyCreditorFinancialAccount` elements.

IR conversion: only read / write first BG-17.

## Are the XRechnung v3 Schematrons compatible with CII D22B?

No. Both the core EN16931 schematron and the XRechnung schematron for 
CII miss a rule akin to Factur-X's FX-SCH-A-000194 that prohibits multiple
`ram:PayeePartyCreditorFinancialAccount` elements.
Multiple `ram:PayeePartyCreditorFinancialAccount` elements are arguably resulting
in a malformed document that should not pass validation (something the CII D16B
schema does).

As of right now, the policy of this library is: as long as the standard 
doesn't explicitly state support for D22B, this library does not try to 
implement support for D22B for that standard, even though extrapolating support 
from D16B to D22B should be as easy as making sure the schematrons support 
multiple BG-3 elements and disallow multiple 
`ram:PayeePartyCreditorFinancialAccount` elements.
