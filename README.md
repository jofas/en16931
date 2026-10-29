# En16931

.NET implementation of the EN16931:2017 data model and the two mandatory syntaxes. 

## Supported Standards

This library comes with a set of already implemented CIUS and extensions:

|Standard|Type|Version|
|--------|----|-------|
|XRechnung|CIUS|3.0.2/2.5.0|
|XRechnung Extension|Extension|3.0.2/2.5.0|
|XRechnung CVD|CIUS|0.9/2.5.0|
|Factur-X Basic|CIUS|1.09.2|
|Factur-X EN16931|CIUS|1.09.2|

The library is extensible through the interfaces of the `En16931.Spec` namespace
and supporting more standards out of the box is one of the development goals of
this project.

## CEN Copyright Notice

Derivative applications of the EN16931:2017 standard, such as this library, are
granted with the permission of CEN and the relevant member national standardisation
bodies as copyright owners, with no additional licensing requirement.
CEN and the relevant national standardisation bodies bear no liability from the
content and implementation of such derivative applications.
In case of doubt, users shall always refer to the official authoritative content
of the semantic data model and two mandatory syntaxes as provided by each CEN
member national standardisation bodies at national level.
CEN and the relevant member national standardisation bodies bear no liability 
from the use of the content and implementation of such derivative applications
and give no warranties expressed or implied for any purpose of such derivative
applications.
In case of doubt, users shall always refer to the content of the publication
provided by the relevant member national standardisation bodies, which make
available the official authoritative text.
