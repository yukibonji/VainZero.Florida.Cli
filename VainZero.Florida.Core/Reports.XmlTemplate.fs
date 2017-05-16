[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module VainZero.Florida.Reports.XmlTemplate

let dayByDay =
  """
  <Row ss:AutoFitHeight="0" ss:StyleID="s64">
  <Cell ss:StyleID="s76"><PhoneticText
    xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
    ss:Type="String">{{日付}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
  <Cell ss:StyleID="s73"><PhoneticText
    xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
    ss:Type="String">{{曜日}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
  <Cell ss:MergeAcross="1" ss:StyleID="m75990336"><PhoneticText
    xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
    ss:Type="String">{{案件}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
  <Cell ss:MergeAcross="5" ss:StyleID="m75990476"><PhoneticText
    xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
    ss:Type="String">{{内容}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
  <Cell ss:StyleID="s86"><PhoneticText
    xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
    ss:Type="Number">{{工数}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
  </Row>
"""

let weeklyReport =
  """
<?xml version="1.0"?>
<?mso-application progid="Excel.Sheet"?>
<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:o="urn:schemas-microsoft-com:office:office"
 xmlns:x="urn:schemas-microsoft-com:office:excel"
 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:html="http://www.w3.org/TR/REC-html40">
 <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
  <Author>anonymous</Author>
  <LastAuthor>anonymous</LastAuthor>
  <LastPrinted>2008-10-28T03:18:04Z</LastPrinted>
  <Created>2008-10-12T15:00:00Z</Created>
  <LastSaved>2008-10-12T15:00:00Z</LastSaved>
  <Version>14.00</Version>
 </DocumentProperties>
 <OfficeDocumentSettings xmlns="urn:schemas-microsoft-com:office:office">
  <AllowPNG/>
 </OfficeDocumentSettings>
 <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
  <WindowHeight>9285</WindowHeight>
  <WindowWidth>15480</WindowWidth>
  <WindowTopX>360</WindowTopX>
  <WindowTopY>330</WindowTopY>
  <ActiveSheet>1</ActiveSheet>
  <ProtectStructure>False</ProtectStructure>
  <ProtectWindows>False</ProtectWindows>
 </ExcelWorkbook>
 <Styles>
  <Style ss:ID="Default" ss:Name="Normal">
   <Alignment ss:Vertical="Center"/>
   <Borders/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
   <NumberFormat/>
   <Protection/>
  </Style>
  <Style ss:ID="m75989216">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Size="14"
    ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75989276">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75989296">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989316">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75988992">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75989012">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75989032">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75989072">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989112">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030836">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030876">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030916">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030592">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#99CCFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m74030632">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030672">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030712">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030368">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m74030388">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#99CCFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m74030408">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030428">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#99CCFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m74030448">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030488">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m74030528">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990560">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990580">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990640">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990660">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Size="14"
    ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990680">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#F2F2F2"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990336">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990356">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990376">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#99CCFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990396">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990416">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990476">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990112">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75990132">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#99CCFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="m75989888">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989908">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989928">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989948">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989968">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75989988">
   <Alignment ss:Horizontal="Left" ss:Vertical="Top" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="m75990008">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#99CCFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s62">
   <Borders/>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s63">
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s64">
   <Borders/>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s65">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders/>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s66">
   <Alignment ss:Vertical="Center"/>
   <Borders/>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="Short Time"/>
  </Style>
  <Style ss:ID="s68">
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s69">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s70">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s71">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s72">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s73">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="m&quot;月&quot;d&quot;日&quot;"/>
  </Style>
  <Style ss:ID="s74">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="m&quot;月&quot;d&quot;日&quot;"/>
  </Style>
  <Style ss:ID="s75">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="m&quot;月&quot;d&quot;日&quot;"/>
  </Style>
  <Style ss:ID="s76">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="m/d;@"/>
  </Style>
  <Style ss:ID="s77">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="m/d;@"/>
  </Style>
  <Style ss:ID="s78">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="m/d;@"/>
  </Style>
  <Style ss:ID="s79">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s80">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s81">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s82">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s83">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s84">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s85">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#FFFFFF"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s86">
   <Alignment ss:Horizontal="Right" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="0.0_ "/>
  </Style>
  <Style ss:ID="s87">
   <Alignment ss:Horizontal="Right" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="0.0_ "/>
  </Style>
  <Style ss:ID="s88">
   <Alignment ss:Horizontal="Right" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
   <NumberFormat ss:Format="0.0_ "/>
  </Style>
  <Style ss:ID="s166">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s171">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s176">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior ss:Color="#FFFFFF" ss:Pattern="Solid"/>
  </Style>
  <Style ss:ID="s187">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s198">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern"/>
   <Interior/>
  </Style>
  <Style ss:ID="s199">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ ゴシック" x:CharSet="128" x:Family="Modern" ss:Color="#FFFFFF"/>
   <Interior ss:Color="#003366" ss:Pattern="Solid"/>
  </Style>
 </Styles>
 <Worksheet ss:Name="原紙">
  <Names>
   <NamedRange ss:Name="Print_Area" ss:RefersTo="=原紙!R1C1:R45C11"/>
  </Names>
  <Table ss:ExpandedColumnCount="11" ss:ExpandedRowCount="44" x:FullColumns="1"
   x:FullRows="1" ss:StyleID="s63" ss:DefaultColumnWidth="54"
   ss:DefaultRowHeight="19.5">
   <Column ss:StyleID="s63" ss:AutoFitWidth="0" ss:Width="51.75" ss:Span="10"/>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="5" ss:MergeDown="1" ss:StyleID="m75989216"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">シュウホウ</PhoneticText><Data
      ss:Type="String">週報</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s62"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s69"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ショゾクブショ</PhoneticText><Data
      ss:Type="String">所属部署</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="2" ss:StyleID="s198"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:Index="8" ss:StyleID="s70"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">タントウシャメイ</PhoneticText><Data
      ss:Type="String">担当者名</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="2" ss:StyleID="s198"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s65"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s66"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s66"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s66"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:MergeAcross="10" ss:StyleID="m75989276"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">コンシュウオモカツドウ</PhoneticText><Data
      ss:Type="String">今週の主な活動</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:MergeAcross="10" ss:MergeDown="2" ss:StyleID="m75989296"><NamedCell
      ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64" ss:Span="1"/>
   <Row ss:Index="8" ss:AutoFitHeight="0" ss:Height="7.5" ss:StyleID="s64">
    <Cell ss:MergeAcross="10" ss:StyleID="m75989316"><NamedCell
      ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:MergeAcross="10" ss:StyleID="m75988992"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ヒベツナイヨウ</PhoneticText><Data
      ss:Type="String">日別の内容</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s71"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ニチジ</PhoneticText><Data
      ss:Type="String">日時</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s72"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ヨウビ</PhoneticText><Data
      ss:Type="String">曜日</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="m75989012"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">アンケン</PhoneticText><Data
      ss:Type="String">案件</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m75989032"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ナイヨウ</PhoneticText><Data
      ss:Type="String">内容</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s76"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s73"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s187"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m75989072"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m75989112"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030836"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s62">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030876"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030916"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030632"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s62">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030672"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s62">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030712"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:StyleID="s77"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s74"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s166"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030488"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:StyleID="s78"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s75"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s171"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m74030528"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Height="7.5">
    <Cell ss:MergeAcross="10" ss:StyleID="s176"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:StyleID="m74030592"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">コンシュウジッセキ</PhoneticText><Data
      ss:Type="String">今週実績</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s68">
    <Cell ss:MergeAcross="10" ss:MergeDown="8" ss:StyleID="m74030368"><NamedCell
      ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s68" ss:Span="6"/>
   <Row ss:Index="31" ss:AutoFitHeight="0"/>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:StyleID="m74030388"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ライシュウヨテイ</PhoneticText><Data
      ss:Type="String">来週予定</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:MergeDown="3" ss:StyleID="m74030408"><NamedCell
      ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Span="2"/>
   <Row ss:Index="37" ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:StyleID="m74030428"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">タレンラクジコウ</PhoneticText><Data
      ss:Type="String">その他、連絡事項</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:MergeDown="6" ss:StyleID="m74030448"><NamedCell
      ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Span="5"/>
  </Table>
  <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
   <PageSetup>
    <Layout x:CenterHorizontal="1" x:CenterVertical="1"/>
    <Header x:Margin="0"/>
    <Footer x:Margin="0"/>
    <PageMargins x:Bottom="0.39370078740157483" x:Left="0.39370078740157483"
     x:Right="0.39370078740157483" x:Top="0.39370078740157483"/>
   </PageSetup>
   <Unsynced/>
   <Print>
    <ValidPrinterInfo/>
    <PaperSizeIndex>9</PaperSizeIndex>
    <HorizontalResolution>600</HorizontalResolution>
    <VerticalResolution>600</VerticalResolution>
   </Print>
   <ShowPageBreakZoom/>
   <DoNotDisplayGridlines/>
   <GridlineColorIndex>56</GridlineColorIndex>
   <GridlineColor>#003366</GridlineColor>
   <Panes>
    <Pane>
     <Number>3</Number>
     <ActiveRow>37</ActiveRow>
     <RangeSelection>R38C1:R44C11</RangeSelection>
    </Pane>
   </Panes>
   <ProtectObjects>False</ProtectObjects>
   <ProtectScenarios>False</ProtectScenarios>
  </WorksheetOptions>
 </Worksheet>
 <Worksheet ss:Name="見本">
  <Names>
   <NamedRange ss:Name="Print_Area" ss:RefersTo="=見本!R1C1:R45C11"/>
  </Names>
  <Table ss:ExpandedColumnCount="11" ss:ExpandedRowCount="44" x:FullColumns="1"
   x:FullRows="1" ss:StyleID="s63" ss:DefaultColumnWidth="54"
   ss:DefaultRowHeight="19.5">
   <Column ss:StyleID="s63" ss:AutoFitWidth="0" ss:Width="51.75" ss:Span="10"/>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="5" ss:MergeDown="1" ss:StyleID="m75990660"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">シュウホウ</PhoneticText><Data
      ss:Type="String">週報</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s62"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s69"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ショゾクブショ</PhoneticText><Data
      ss:Type="String">所属部署</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="2" ss:StyleID="s198"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{所属部署}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:Index="8" ss:StyleID="s70"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">タントウシャメイ</PhoneticText><Data
      ss:Type="String">担当者名</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="2" ss:StyleID="s198"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{担当者名}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s65"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s66"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s66"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s66"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:MergeAcross="6" ss:StyleID="s199"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">コンシュウオモカツドウ</PhoneticText><Data
      ss:Type="String">今週の主な活動</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="3" ss:StyleID="m75990680"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">シンチョク</PhoneticText><Data
      ss:Type="String">進捗</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:MergeAcross="6" ss:MergeDown="2" ss:StyleID="m75990560"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{今週活動}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="3" ss:MergeDown="2" ss:StyleID="m75990580"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{進捗}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64" ss:Span="1"/>
   <Row ss:Index="8" ss:AutoFitHeight="0" ss:Height="7.5" ss:StyleID="s64">
    <Cell ss:MergeAcross="10" ss:StyleID="m75990640"><NamedCell
      ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:MergeAcross="10" ss:StyleID="m75990112"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ヒベツナイヨウ</PhoneticText><Data
      ss:Type="String">日別の内容</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s64">
    <Cell ss:StyleID="s71"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ニチジ</PhoneticText><Data
      ss:Type="String">日時</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s72"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ヨウビ</PhoneticText><Data
      ss:Type="String">曜日</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="m75990356"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">アンケン</PhoneticText><Data
      ss:Type="String">案件</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s83"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ナイヨウ</PhoneticText><Data
      ss:Type="String">内容</Data><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s84"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s84"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s84"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s84"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s84"><NamedCell ss:Name="Print_Area"/></Cell>
    <Cell ss:StyleID="s85"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">コウスウ</PhoneticText><Data
      ss:Type="String">工数(H)</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   {{日別の内容}}
   <Row ss:AutoFitHeight="0" ss:Height="7.5">
    <Cell ss:MergeAcross="10" ss:StyleID="s176"><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:StyleID="m75990376"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">コンシュウジッセキ</PhoneticText><Data
      ss:Type="String">今週実績</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s68">
    <Cell ss:MergeAcross="10" ss:MergeDown="8" ss:StyleID="m75990396"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{今週実績}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:StyleID="s68" ss:Span="6"/>
   <Row ss:Index="31" ss:AutoFitHeight="0"/>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:StyleID="m75990132"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ライシュウヨテイ</PhoneticText><Data
      ss:Type="String">来週予定</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:MergeDown="3" ss:StyleID="m75990416"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{来週予定}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Span="2"/>
   <Row ss:Index="37" ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:StyleID="m75990008"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">カダイ</PhoneticText><Data
      ss:Type="String">課題など</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0">
    <Cell ss:MergeAcross="10" ss:MergeDown="6" ss:StyleID="m75989988"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel"></PhoneticText><Data
      ss:Type="String">{{その他}}</Data><NamedCell ss:Name="Print_Area"/></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Span="5"/>
  </Table>
  <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
   <PageSetup>
    <Layout x:CenterHorizontal="1" x:CenterVertical="1"/>
    <Header x:Margin="0"/>
    <Footer x:Margin="0"/>
    <PageMargins x:Bottom="0.39370078740157483" x:Left="0.39370078740157483"
     x:Right="0.39370078740157483" x:Top="0.39370078740157483"/>
   </PageSetup>
   <Unsynced/>
   <Print>
    <ValidPrinterInfo/>
    <PaperSizeIndex>9</PaperSizeIndex>
    <HorizontalResolution>600</HorizontalResolution>
    <VerticalResolution>600</VerticalResolution>
   </Print>
   <ShowPageBreakZoom/>
   <PageBreakZoom>100</PageBreakZoom>
   <Selected/>
   <DoNotDisplayGridlines/>
   <GridlineColorIndex>56</GridlineColorIndex>
   <TopRowVisible>31</TopRowVisible>
   <GridlineColor>#003366</GridlineColor>
   <Panes>
    <Pane>
     <Number>3</Number>
     <ActiveRow>44</ActiveRow>
    </Pane>
   </Panes>
   <ProtectObjects>False</ProtectObjects>
   <ProtectScenarios>False</ProtectScenarios>
  </WorksheetOptions>
 </Worksheet>
</Workbook>
"""

let timeSheetEmptyRow =
  """
   <Row ss:AutoFitHeight="0" ss:Height="19.5">
    <Cell ss:StyleID="s432" ss:Formula="=IF(DAY(R2C1+{{日}}-1)&lt;&gt;{{日}},&quot;&quot;,R2C1+{{日}}-1)"><Data
      ss:Type="DateTime">{{日付}}T00:00:00.000</Data></Cell>
    <Cell ss:StyleID="s438" ss:Formula="=IF(DAY(R2C1+{{日}}-1)&lt;&gt;{{日}},&quot;&quot;,R2C1+{{日}}-1)"><Data
      ss:Type="DateTime">{{日付}}T00:00:00.000</Data></Cell>
    <Cell ss:StyleID="s434"/>
    <Cell ss:StyleID="s434"/>
    <Cell ss:StyleID="s434"/>
    <Cell ss:StyleID="s434" ss:Formula="=RC[-2]-(RC[-3]+RC[-1])"><Data
      ss:Type="DateTime">1899-12-31T00:00:00.000</Data></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m174954428"/>
   </Row>
  """

let timeSheetWorkingRow =
  """
   <Row ss:AutoFitHeight="0" ss:Height="19.5">
    <Cell ss:StyleID="s432" ss:Formula="=IF(DAY(R2C1+{{日}}-1)&lt;&gt;{{日}},&quot;&quot;,R2C1+{{日}}-1)"><Data
      ss:Type="DateTime">{{日付}}T00:00:00.000</Data></Cell>
    <Cell ss:StyleID="s438" ss:Formula="=IF(DAY(R2C1+{{日}}-1)&lt;&gt;{{日}},&quot;&quot;,R2C1+{{日}}-1)"><Data
      ss:Type="DateTime">{{日付}}T00:00:00.000</Data></Cell>
    <Cell ss:StyleID="s434"><Data ss:Type="DateTime">1899-12-31T{{開始時刻}}.000</Data></Cell>
    <Cell ss:StyleID="s434"><Data ss:Type="DateTime">1899-12-31T{{終了時刻}}.000</Data></Cell>
    <Cell ss:StyleID="s434"><Data ss:Type="DateTime">1899-12-31T{{休憩時間}}.000</Data></Cell>
    <Cell ss:StyleID="s434" ss:Formula="=RC[-2]-(RC[-3]+RC[-1])"><Data
      ss:Type="DateTime">1899-12-31T{{勤務時間}}.000</Data></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m174954428"/>
   </Row>
  """

let timeSheet =
  """
<?xml version="1.0"?>
<?mso-application progid="Excel.Sheet"?>
<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:o="urn:schemas-microsoft-com:office:office"
 xmlns:x="urn:schemas-microsoft-com:office:excel"
 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:html="http://www.w3.org/TR/REC-html40">
 <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
  <Author>guest</Author>
  <LastAuthor>guest</LastAuthor>
  <LastPrinted>2014-08-25T01:24:27Z</LastPrinted>
  <Created>2014-08-20T07:10:40Z</Created>
  <LastSaved>2017-05-16T00:53:26Z</LastSaved>
  <Version>14.00</Version>
 </DocumentProperties>
 <OfficeDocumentSettings xmlns="urn:schemas-microsoft-com:office:office">
  <AllowPNG/>
 </OfficeDocumentSettings>
 <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
  <SupBook>
   <Path>WINDOWS\TEMP\外注委１.XLS</Path>
   <SheetName>資格</SheetName>
  </SupBook>
  <WindowHeight>8175</WindowHeight>
  <WindowWidth>16380</WindowWidth>
  <WindowTopX>0</WindowTopX>
  <WindowTopY>15</WindowTopY>
  <TabRatio>243</TabRatio>
  <ProtectStructure>False</ProtectStructure>
  <ProtectWindows>False</ProtectWindows>
 </ExcelWorkbook>
 <Styles>
  <Style ss:ID="Default" ss:Name="Normal">
   <Alignment ss:Vertical="Bottom"/>
   <Borders/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
   <NumberFormat/>
   <Protection/>
  </Style>
  <Style ss:ID="m174956008">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="18"/>
   <NumberFormat ss:Format="[$-409]mmmmm;@"/>
  </Style>
  <Style ss:ID="m174956028">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174956048">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955648">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955668">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955688">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955708">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955728">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955328">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955348">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955368">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955388">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955408">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955428">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955448">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955468">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955488">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955508">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955008">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955028">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955048">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955068">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955088">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955108">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955128">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955148">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955168">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174955188">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174954688">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <NumberFormat ss:Format="d"/>
  </Style>
  <Style ss:ID="m174954708">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <NumberFormat ss:Format="d"/>
  </Style>
  <Style ss:ID="m174954728">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <NumberFormat ss:Format="d"/>
  </Style>
  <Style ss:ID="m174954748">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
  </Style>
  <Style ss:ID="m174954768">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <NumberFormat ss:Format="d"/>
  </Style>
  <Style ss:ID="m174954368">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174954388">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174954408">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174954428">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="m174954448">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="s419">
   <Alignment ss:Vertical="Center"/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
  </Style>
  <Style ss:ID="s420">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="18"/>
   <NumberFormat ss:Format="[JPN][$-411]gggy&quot;年&quot;m&quot;月&quot;"/>
  </Style>
  <Style ss:ID="s421">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="18"/>
   <NumberFormat ss:Format="[$-409]mmmmm;@"/>
  </Style>
  <Style ss:ID="s425">
   <Alignment ss:Horizontal="Right" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="6"/>
  </Style>
  <Style ss:ID="s426">
   <Alignment ss:Vertical="Center"/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="20"/>
  </Style>
  <Style ss:ID="s427">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="s428">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center" ss:WrapText="1"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
  </Style>
  <Style ss:ID="s432">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
   <NumberFormat ss:Format="d"/>
  </Style>
  <Style ss:ID="s433">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"
    ss:Color="#FF0000"/>
   <Interior/>
   <NumberFormat ss:Format="ddd"/>
  </Style>
  <Style ss:ID="s434">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
   <NumberFormat ss:Format="Short Time"/>
  </Style>
  <Style ss:ID="s438">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <Interior/>
   <NumberFormat ss:Format="ddd"/>
  </Style>
  <Style ss:ID="s441">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
  </Style>
  <Style ss:ID="s442">
   <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
  </Style>
  <Style ss:ID="s443">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Borders>
    <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
    <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"
     ss:Color="#000000"/>
   </Borders>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <NumberFormat/>
   <Protection/>
  </Style>
  <Style ss:ID="s444">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
  </Style>
  <Style ss:ID="s445">
   <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"
    ss:Color="#FFFFFF"/>
   <NumberFormat/>
   <Protection x:HideFormula="1"/>
  </Style>
  <Style ss:ID="s446">
   <Alignment ss:Vertical="Center"/>
   <Font ss:FontName="ＭＳ Ｐゴシック" x:CharSet="128" x:Family="Modern" ss:Size="11"/>
   <NumberFormat ss:Format="d"/>
  </Style>
 </Styles>
 <Names/>
 <Worksheet ss:Name="TimeSheet">
  <Table ss:ExpandedColumnCount="13" ss:ExpandedRowCount="39" x:FullColumns="1"
   x:FullRows="1" ss:StyleID="s419" ss:DefaultColumnWidth="27"
   ss:DefaultRowHeight="13.5">
   <Column ss:StyleID="s419" ss:AutoFitWidth="0" ss:Width="30.75" ss:Span="1"/>
   <Column ss:Index="3" ss:StyleID="s419" ss:AutoFitWidth="0" ss:Width="85.5"
    ss:Span="3"/>
   <Row ss:AutoFitHeight="0" ss:Height="3.75"/>
   <Row ss:AutoFitHeight="0" ss:Height="25.5" ss:StyleID="s426">
    <Cell ss:MergeAcross="2" ss:StyleID="s420"><Data ss:Type="DateTime">2017-04-01T00:00:00.000</Data></Cell>
    <Cell ss:MergeAcross="1" ss:StyleID="s420"><Data ss:Type="String">勤務報告書</Data></Cell>
    <Cell ss:StyleID="s421"><Data ss:Type="String">氏名</Data></Cell>
    <Cell ss:MergeAcross="5" ss:StyleID="m174956008"/>
    <Cell ss:StyleID="s425"><Data ss:Type="String">印</Data></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Height="3"/>
   <Row ss:AutoFitHeight="0" ss:Height="19.5">
    <Cell ss:StyleID="s427"><Data ss:Type="String">日</Data></Cell>
    <Cell ss:StyleID="s427"><Data ss:Type="String">曜日</Data></Cell>
    <Cell ss:StyleID="s427"><Data ss:Type="String">開始時間</Data></Cell>
    <Cell ss:StyleID="s427"><Data ss:Type="String">終了時間</Data></Cell>
    <Cell ss:StyleID="s427"><Data ss:Type="String">休憩時間</Data></Cell>
    <Cell ss:StyleID="s428"><Data ss:Type="String">通常勤務時間</Data></Cell>
    <Cell ss:MergeAcross="6" ss:StyleID="m174956028"><Data ss:Type="String">備考</Data></Cell>
   </Row>
   {{行}}
   <Row ss:AutoFitHeight="0" ss:Height="22.5">
    <Cell ss:MergeAcross="1" ss:StyleID="m174954768"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ゴウケイ</PhoneticText><Data
      ss:Type="String">合計</Data></Cell>
    <Cell ss:StyleID="s441" ss:Formula="=COUNT(R[-31]C:R[-1]C)"><Data
      ss:Type="Number">0</Data></Cell>
    <Cell ss:StyleID="s442"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ニチ</PhoneticText><Data
      ss:Type="String">日</Data></Cell>
    <Cell ss:StyleID="s443" ss:Formula="=RC[3]"><Data ss:Type="Number">0</Data></Cell>
    <Cell ss:StyleID="s442"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ジカン</PhoneticText><Data
      ss:Type="String">時間</Data></Cell>
    <Cell ss:StyleID="s444"/>
    <Cell ss:StyleID="s445"
     ss:Formula="=(DAY(SUM(R[-31]C[-2]:R[-1]C[-2]))*24+HOUR(SUM(R[-31]C[-2]:R[-1]C[-2])))+(MINUTE(SUM(R[-31]C[-2]:R[-1]C[-2]))/60)"><Data
      ss:Type="Number">0</Data></Cell>
    <Cell ss:StyleID="s444"/>
    <Cell ss:StyleID="s444"/>
    <Cell ss:StyleID="s444"/>
    <Cell ss:StyleID="s444"/>
    <Cell ss:StyleID="s444"/>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Height="3.75">
    <Cell ss:StyleID="s446"/>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Height="18.75">
    <Cell ss:MergeAcross="5" ss:StyleID="m174954688"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">ソナエコウ</PhoneticText><Data
      ss:Type="String">備　考</Data></Cell>
    <Cell ss:Index="9" ss:MergeAcross="2" ss:StyleID="m174954728"><PhoneticText
      xmlns="urn:schemas-microsoft-com:office:excel">セキニンシャ</PhoneticText><Data
      ss:Type="String">責任者</Data></Cell>
   </Row>
   <Row ss:AutoFitHeight="0" ss:Height="56.25">
    <Cell ss:MergeAcross="5" ss:StyleID="m174954708"/>
    <Cell ss:Index="9" ss:MergeAcross="2" ss:StyleID="m174954748"/>
   </Row>
  </Table>
  <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
   <PageSetup>
    <Header x:Margin="0.3"/>
    <Footer x:Margin="0.3"/>
    <PageMargins x:Bottom="0.75" x:Left="0.25" x:Right="0.25" x:Top="0.75"/>
   </PageSetup>
   <Print>
    <ValidPrinterInfo/>
    <PaperSizeIndex>9</PaperSizeIndex>
    <HorizontalResolution>600</HorizontalResolution>
    <VerticalResolution>600</VerticalResolution>
   </Print>
   <Selected/>
   <DoNotDisplayZeros/>
   <Panes>
    <Pane>
     <Number>3</Number>
     <ActiveRow>4</ActiveRow>
     <ActiveCol>3</ActiveCol>
    </Pane>
   </Panes>
   <ProtectObjects>False</ProtectObjects>
   <ProtectScenarios>False</ProtectScenarios>
  </WorksheetOptions>
  <ConditionalFormatting xmlns="urn:schemas-microsoft-com:office:excel">
   <Range>R5C2:R35C2</Range>
   <Condition>
    <Value1>DATE(#REF!,1,1)</Value1>
    <Format Style='color:#DA9694'/>
   </Condition>
   <Condition>
    <Value1>WEEKDAY(R[-1]C)=7</Value1>
    <Format Style='color:#DA9694'/>
   </Condition>
   <Condition>
    <Value1>WEEKDAY(R[-1]C)=6</Value1>
    <Format Style='color:#538DD5'/>
   </Condition>
  </ConditionalFormatting>
 </Worksheet>
</Workbook>
  """
