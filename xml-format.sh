#!/bin/bash

for file in $(rg --files -g *.xslt -g *.xsl -g *.xml -g *.xsd); do
  xmllint --format $file --output $file
done
