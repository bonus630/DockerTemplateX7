<?xml version="1.0"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:frmwrk="Corel Framework Data"
                exclude-result-prefixes="frmwrk"
                extension-element-prefixes="msxsl">
  <xsl:output method="xml" encoding="UTF-8" indent="yes"/>

  <frmwrk:uiconfig>
    <frmwrk:compositeNode xPath="/uiConfig/commandBars/commandBarData[@guid='7c905e2a-cb64-4ba1-aff0-c306f392680c']"/>
    <frmwrk:compositeNode xPath="/uiConfig/commandBars/commandBarData[@guid='74e03d83-404c-49f5-824a-fe0fd02ab29a']"/>
    <frmwrk:compositeNode xPath="/uiConfig/frame"/>
  </frmwrk:uiconfig>

  <!-- Copy everything by default -->
  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>

  <!-- Helper to insert a new item into a menu/toolbar -->
  <xsl:template match="node()|@*" mode="insert-item">
    <xsl:param name="after"></xsl:param>
    <xsl:param name="before"></xsl:param>
    <xsl:param name="content"></xsl:param>
    <xsl:copy>
      <xsl:apply-templates select="@*"/>
      <xsl:for-each select="node()">
        <xsl:if test="name()='item' and @guidRef=$before">
          <xsl:copy-of select="$content"/>
        </xsl:if>
        <xsl:copy>
          <xsl:apply-templates select="node()|@*"/>
        </xsl:copy>
        <xsl:if test="name()='item' and @guidRef=$after">
          <xsl:copy-of select="$content"/>
        </xsl:if>
      </xsl:for-each>
      <xsl:if test="not(./item[@guidRef=$after]) and not(./item[@guidRef=$before])">
        <xsl:copy-of select="$content"/>
      </xsl:if>
    </xsl:copy>
  </xsl:template>


  <!-- Add tool item to the Toolbox -->
  <xsl:template match="//commandBarData[@guid='7c905e2a-cb64-4ba1-aff0-c306f392680c']/toolbar">
    <xsl:apply-templates mode="insert-item" select=".">
      <xsl:with-param name="content">
        <xsl:if test="not(./item[@guidRef='$GuidA$'])">
          <!-- Only insert if its not already there -->
          <item guidRef="$guid1$"/>
        </xsl:if>
      </xsl:with-param>
    </xsl:apply-templates>
  </xsl:template>


  <xsl:template match="uiConfig/commandBars/commandBarData[@guid='74e03d83-404c-49f5-824a-fe0fd02ab29a']/toolbar">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
      <modeData guid="$GuidB$" captionRef="01965a34-ae38-01a0-4271-cd903b48e518">
        <item guidRef="3148e122-4f73-6e8a-4dfb-b2f6a84090d5"/>
        <item guidRef="a41164ff-c9e9-4b2c-954a-095f4204538e"/>
        <item guidRef="266435b4-6e53-460f-9fa7-f45be187d400"/>
        <item guidRef="325d7c86-da3a-4610-bd25-5cf98cf66a41"/>
        <item guidRef="42bef211-16e5-4b4f-b9ee-52b7b5476232"/>
        <item guidRef="d289f32b-3808-4510-b892-fd2cb0820209"/>
        <item guidRef="266435b4-6e53-460f-9fa7-f45be187d400"/>
        <item guidRef="8723ad52-3e31-473c-8756-7ae85abcc483"/>
        <item guidRef="266435b4-6e53-460f-9fa7-f45be187d400"/>
        <item guidRef="e6644135-9dab-4935-8ab9-fc85527810ca"/>
        <item guidRef="6ae897fd-2eab-4dad-b172-f4fb768c273e"/>
        <item guidRef="266435b4-6e53-460f-9fa7-f45be187d400"/>
        <item guidRef="97e5c8b9-f7e2-453c-92a2-af7fb61e67b2"/>
        <item guidRef="266435b4-6e53-460f-9fa7-f45be187d400"/>
        <item guidRef="76dd9e0c-e9c2-42b6-b8bb-f7717482164e"/>
        <item guidRef="6bd00383-d132-4686-8a21-8d7052b6a81b"/>
        <item guidRef="40cc3adc-498a-4256-a98a-d1210a4019f7"/>
      </modeData>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>