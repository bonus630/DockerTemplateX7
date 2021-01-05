<?xml version="1.0"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:frmwrk="Corel Framework Data">
  <xsl:output method="xml" encoding="UTF-8" indent="yes"/>
  <frmwrk:uiconfig>
    <frmwrk:applicationInfo userConfiguration="true" />
  </frmwrk:uiconfig>
  
  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>

  <!-- Each item is defined in the item section of the application uiConfig -->
  <xsl:template match="uiConfig/items">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>

      <!-- Define any new items here -->
      
      <!-- C# Test Line Tool -->
      <itemData guid="$GuidA$"
                type="groupedRadioButton"
                currentActiveOfGroup="*Bind(DataSource=WAppDataSource;Path=ActiveTool;BindType=TwoWay)"
                enable="*Bind(DataSource=WAppDataSource;Path=ToolboxEnable)"
                identifier="$GuidA$"/>

    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>