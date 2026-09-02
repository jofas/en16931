# FAQ

## Does CII D22B support multiple BG-17 elements? (I.e. is it fully compliant with the EN16931 data model?)

No. Multiple `ram:PayeePartyCreditorFinancialAccount` elements
can be provided (contains fields `BT-84` and `BT-85` of `BG-17`), but
only one `ram:PayeeSpecifiedCreditorFinancialInstitution` (`BT-86`).

Factur-X 1.09 rule FX-SCH-A-000194 prohibits multiple 
`ram:PayeePartyCreditorFinancialAccount` elements.

IR conversion: only read / write first BG-17.
