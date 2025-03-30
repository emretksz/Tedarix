using Tedarix.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.EntityFrameworkCore;

namespace Tedarix.Areas.ManagementPanel.Helpers.ExcelHelper
{
    public class ExportExcel
    {

        public async Task<string> ExportExcelFunc(int id, IWebHostEnvironment _evn,bool arsivlendiMi) {


            using (TedarixContext db = new TedarixContext())
            {
               
                var foy = await db.Foys.Include(a => a.Tenant).Include(a => a.Atolye).AsNoTracking().Where(a => a.ArsivlendiMi == arsivlendiMi && a.Id == id).FirstOrDefaultAsync();
                var foyAndKesim = await db.FoyAndKesims.Include(a => a.Kesim).AsNoTracking().Where(a => a.FoyId == foy.Id).ToListAsync();
                var foyAndCins = await db.FoyAndCinsS.Include(a => a.Cins).AsNoTracking().Where(a => a.FoyId == foy.Id).ToListAsync();       
                
                var foyAndColor = await db.FoyAndColors.AsNoTracking().Where(a => a.FoyId == foy.Id).ToListAsync();


                //var rol = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role).Value;

                try
                {

                    string folder = _evn.WebRootPath;   // _evn değişkenini doğru bir şekilde tanımladığınızdan emin olun

                    string specificFolder = Path.Combine(folder, "ExcelTemplates");

                    if (!Directory.Exists(specificFolder))
                        Directory.CreateDirectory(specificFolder);

                    var path = Path.Combine(specificFolder, "Foy_" + foy.ModelCode + ".xlsx");

                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
                    {
                        document.AddWorkbookPart();
                        document.WorkbookPart.Workbook = new Workbook();
                        SheetFormatProperties sheetFormatProperties = new SheetFormatProperties() { DefaultColumnWidth = 12.75D, DefaultRowHeight = 5D, };
                        Worksheet worksheet = new Worksheet();

                        worksheet.Append(sheetFormatProperties);

                        #region stylesPart
                        var stylesPart = document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                        stylesPart.Stylesheet = new Stylesheet();

                        // Font list
                        // Create a bold font
                        stylesPart.Stylesheet.Fonts = new Fonts();
                        Font bold_font = new Font();         // Bold font
                        Bold bold = new Bold();
                        bold_font.Append(bold);

                        // Add fonts to list
                        stylesPart.Stylesheet.Fonts.AppendChild(new Font());
                        stylesPart.Stylesheet.Fonts.AppendChild(bold_font); // Bold gets fontid = 1
                        stylesPart.Stylesheet.Fonts.Count = 2;

                        // Create fills list
                        stylesPart.Stylesheet.Fills = new Fills();

                        var formatRed = new PatternFill() { PatternType = PatternValues.Solid };
                        formatRed.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("FF6600") }; // red fill
                        formatRed.BackgroundColor = new BackgroundColor { Indexed = 64 };

                        var formatGreen = new PatternFill() { PatternType = PatternValues.Solid };
                        formatGreen.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("45ff2f") }; // red fill
                        formatGreen.BackgroundColor = new BackgroundColor { Indexed = 64 };


                        var formatKirmizi = new PatternFill() { PatternType = PatternValues.Solid };
                        formatKirmizi.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("ff0000") }; // red fill
                        formatKirmizi.BackgroundColor = new BackgroundColor { Indexed = 64 };


                        // Append fills to list
                        stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
                        stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
                        stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = formatRed }); // Red gets fillid = 2

                        stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = formatGreen }); // required, reserved by Excel
                        stylesPart.Stylesheet.Fills.AppendChild(new Fill { PatternFill = formatKirmizi }); // required, reserved by Excel

                        stylesPart.Stylesheet.Fills.Count = 5;

                        // Create border list
                        stylesPart.Stylesheet.Borders = new Borders();
                        // Create thin borders for passed/failed tests and default cells
                        LeftBorder leftThin = new LeftBorder() { Style = BorderStyleValues.Thin };
                        RightBorder rightThin = new RightBorder() { Style = BorderStyleValues.Thin };
                        TopBorder topThin = new TopBorder() { Style = BorderStyleValues.Thin };
                        BottomBorder bottomThin = new BottomBorder() { Style = BorderStyleValues.Thin };

                        Border borderThin = new Border();
                        borderThin.Append(leftThin);
                        borderThin.Append(rightThin);
                        borderThin.Append(topThin);
                        borderThin.Append(bottomThin);

                        // Create thick borders for headings
                        LeftBorder leftThick = new LeftBorder() { Style = BorderStyleValues.Thick };
                        RightBorder rightThick = new RightBorder() { Style = BorderStyleValues.Thick };
                        TopBorder topThick = new TopBorder() { Style = BorderStyleValues.Thick };
                        BottomBorder bottomThick = new BottomBorder() { Style = BorderStyleValues.Thick };

                        Border borderThick = new Border();
                        borderThick.Append(leftThick);
                        borderThick.Append(rightThick);
                        borderThick.Append(topThick);
                        borderThick.Append(bottomThick);

                        // Add borders to list
                        stylesPart.Stylesheet.Borders.AppendChild(new Border());
                        stylesPart.Stylesheet.Borders.AppendChild(borderThin);
                        stylesPart.Stylesheet.Borders.AppendChild(borderThick);
                        stylesPart.Stylesheet.Borders.Count = 3;


                        // Create blank cell format list
                        stylesPart.Stylesheet.CellStyleFormats = new CellStyleFormats();
                        stylesPart.Stylesheet.CellStyleFormats.Count = 1;
                        stylesPart.Stylesheet.CellStyleFormats.AppendChild(new CellFormat());

                        // Create cell format list
                        stylesPart.Stylesheet.CellFormats = new CellFormats();
                        // empty one for index 0, seems to be required
                        stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat());

                        // Styleindex = 1, Normal
                        stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 1, FillId = 2, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });

                        // Styleindex = 2, Bold Header
                        stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 2, FillId = 0, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });

                        stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 2, FillId = 3, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });
                        stylesPart.Stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 1, BorderId = 2, FillId = 4, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });

                        stylesPart.Stylesheet.CellFormats.Count = 5;
                        stylesPart.Stylesheet.Save();

                        #endregion

                        var worksheetPart = document.WorkbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = worksheet;

                        #region Column
                        Columns columns = new Columns();

                        //columns.Append(new Column() { Min = 1, Max = 1, Width = 10, CustomWidth = true });
                        //for (int i = 0; i < 50; i++)
                        //{
                        //}
                        columns.Append(new Column() { Min = Convert.ToUInt32(1), Max = Convert.ToUInt32(2), Width = 15, CustomWidth = true });
                        columns.Append(new Column() { Min = Convert.ToUInt32(2), Max = Convert.ToUInt32(3), Width = 15, CustomWidth = true });
                        columns.Append(new Column() { Min = Convert.ToUInt32(3), Max = Convert.ToUInt32(4), Width = 15, CustomWidth = true });
                        columns.Append(new Column() { Min = Convert.ToUInt32(4), Max = Convert.ToUInt32(5), Width = 15, CustomWidth = true });
                        columns.Append(new Column() { Min = Convert.ToUInt32(5), Max = Convert.ToUInt32(6), Width = 20, CustomWidth = true });
                        columns.Append(new Column() { Min = Convert.ToUInt32(6), Max = Convert.ToUInt32(7), Width = 20, CustomWidth = true });

                        worksheetPart.Worksheet.Append(columns);
                        #endregion

                        var sheetData = new SheetData();
                        worksheet.AppendChild(sheetData);

                        Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());

                        Sheet sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Foy" };

                        sheets.Append(sheet);

                        #region Headers

                        // Add header
                        UInt32 rowIdex = 0;
                        var row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        var cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Model Kodu" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Yaş" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                             rowIdex, "Firma Adı" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Atolye" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Oluşturma Tarihi" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Sevk Tarihi" ?? string.Empty, 1));

                        #endregion

                        #region Add Data
                        string formula = "";
                        cellIdex = 0;
                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);

                        var cell = CreateTextCell(ColumnLetter(0),
                               rowIdex, foy.ModelCode, 0);
                        row.AppendChild(cell);
                        var cell2 = CreateTextCell(ColumnLetter(1),
                                rowIdex, foy.Age, 0);
                        row.AppendChild(cell2);
                        var cell3 = CreateTextCell(ColumnLetter(2),
                                rowIdex, foy.Tenant.Name, 0);
                        row.AppendChild(cell3);
                        var cell4 = CreateTextCell(ColumnLetter(3),
                                rowIdex, foy.Atolye != null ? foy.Atolye.Name : "", 0);
                        row.AppendChild(cell4);
                        var cell5 = CreateTextCell(ColumnLetter(4),
                                rowIdex, foy.RegisterDate.ToString(), 0);
                        row.AppendChild(cell5);
                        var cell6 = CreateTextCell(ColumnLetter(5),
                                rowIdex, foy.SevkTarihi.ToString() == "1.01.0001 00:00:00" ? "" : foy.SevkTarihi.ToString(), 0);
                        row.AppendChild(cell6);

                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                             rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));

                        #endregion

                        #region Add Data
                        formula = "";
                        cellIdex = 0;
                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);

                        cell = CreateTextCell(ColumnLetter(0),
                              rowIdex, "", 0);
                        row.AppendChild(cell);
                        cell2 = CreateTextCell(ColumnLetter(1),
                               rowIdex, "", 0);
                        row.AppendChild(cell2);
                        cell3 = CreateTextCell(ColumnLetter(2),
                                rowIdex, "", 0);
                        row.AppendChild(cell3);
                        cell4 = CreateTextCell(ColumnLetter(3),
                               rowIdex, "", 0);
                        row.AppendChild(cell4);
                        cell5 = CreateTextCell(ColumnLetter(4),
                                 rowIdex, "", 0);
                        row.AppendChild(cell5);





                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Kesim" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                      rowIdex, "Durum" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Tarih" ?? string.Empty, 1));

                        #endregion

                        #region Add Data
                        foreach (var item in foyAndKesim)
                        {
                            formula = "";
                            cellIdex = 0;
                            row = new Row { RowIndex = ++rowIdex };
                            sheetData.AppendChild(row);

                            cell = CreateTextCell(ColumnLetter(0),
                                  rowIdex, item.Kesim.Name, 0);
                            row.AppendChild(cell);
                            if (item.State)
                            {
                                cell2 = CreateTextCell(ColumnLetter(1),
                                      rowIdex, "Tamamlandı", 3);
                                row.AppendChild(cell2);
                            }
                            else
                            {
                                cell2 = CreateTextCell(ColumnLetter(1),
                                  rowIdex, "Tamamlanmadı", 4);
                                row.AppendChild(cell2);
                            }

                            cell3 = CreateTextCell(ColumnLetter(2),
                                   rowIdex, item.RegisterDate.ToString() == "1.01.0001 00:00:00" ? "" : item.RegisterDate.ToString(), 0);
                            row.AppendChild(cell3);

                        }


                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                             rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));

                        #endregion




                        ///////////////
                        ///

                        #region Add Data
                        formula = "";
                        cellIdex = 0;
                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);

                        cell = CreateTextCell(ColumnLetter(0),
                              rowIdex, "", 0);
                        row.AppendChild(cell);
                        cell2 = CreateTextCell(ColumnLetter(1),
                               rowIdex, "", 0);
                        row.AppendChild(cell2);

                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Renk Adı" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                         rowIdex, "Adet" ?? string.Empty, 1));

                        #endregion

                        #region Add Data
                        foreach (var item in foyAndColor)
                        {
                            formula = "";
                            cellIdex = 0;
                            row = new Row { RowIndex = ++rowIdex };
                            sheetData.AppendChild(row);

                            cell = CreateTextCell(ColumnLetter(0),
                                  rowIdex, item.Renk, 0);
                            row.AppendChild(cell);
                            cell = CreateTextCell(ColumnLetter(1),
                               rowIdex, item.Adet, 0);
                            row.AppendChild(cell);

                        }

                        #endregion


                        //////
                        #region Add Data



                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                             rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "" ?? string.Empty, 0));

                        #endregion

                        #region Add Data
                        formula = "";
                        cellIdex = 0;
                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);

                        cell = CreateTextCell(ColumnLetter(0),
                              rowIdex, "", 0);
                        row.AppendChild(cell);
                        cell2 = CreateTextCell(ColumnLetter(1),
                               rowIdex, "", 0);
                        row.AppendChild(cell2);
                        cell3 = CreateTextCell(ColumnLetter(2),
                                rowIdex, "", 0);
                        row.AppendChild(cell3);
                        cell4 = CreateTextCell(ColumnLetter(3),
                               rowIdex, "", 0);
                        row.AppendChild(cell4);
                        cell5 = CreateTextCell(ColumnLetter(4),
                                 rowIdex, "", 0);
                        row.AppendChild(cell5);


                        row = new Row { RowIndex = ++rowIdex };
                        sheetData.AppendChild(row);
                        cellIdex = 0;

                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Cins" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                         rowIdex, "Renk  Adı" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                       rowIdex, "Durum" ?? string.Empty, 1));
                        row.AppendChild(CreateTextCell(ColumnLetter(cellIdex++),
                            rowIdex, "Tarih" ?? string.Empty, 1));

                        #endregion

                        #region Add Data
                        foreach (var item in foyAndCins)
                        {
                            formula = "";
                            cellIdex = 0;
                            row = new Row { RowIndex = ++rowIdex };
                            sheetData.AppendChild(row);

                            cell = CreateTextCell(ColumnLetter(0),
                                  rowIdex, item.Cins.Name, 0);
                            row.AppendChild(cell);
                            cell = CreateTextCell(ColumnLetter(1),
                               rowIdex, item.RenkAdi, 0);
                            row.AppendChild(cell);
                            if (item.State)
                            {
                                cell = CreateTextCell(ColumnLetter(2),
                       rowIdex, "Tamamlandı", 3);
                                row.AppendChild(cell);
                            }
                            else
                            {
                                cell = CreateTextCell(ColumnLetter(2),
                       rowIdex, "Tamamlanmadı", 4);
                                row.AppendChild(cell);
                            }

                            cell2 = CreateTextCell(ColumnLetter(3),
                                   rowIdex, item.RegisterDate.ToString() == "1.01.0001 00:00:00" ? "" : item.RegisterDate.ToString(), 0);
                            row.AppendChild(cell2);

                        }
                        document.WorkbookPart.Workbook.Save();
                        document.Close();

                    }
                    #endregion
                    return path;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
         
        }

        private Cell CreateTextCell(string header, UInt32 index, string text, UInt32Value styleIndex)
        {
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index,
                StyleIndex = styleIndex
            };

            var istring = new InlineString();
            var t = new Text { Text = text };
            istring.AppendChild(t);
            cell.AppendChild(istring);
            return cell;
        }
        private string ColumnLetter(int intCol)
        {
            var intFirstLetter = ((intCol) / 676) + 64;
            var intSecondLetter = ((intCol % 676) / 26) + 64;
            var intThirdLetter = (intCol % 26) + 65;

            var firstLetter = (intFirstLetter > 64)
                ? (char)intFirstLetter : ' ';
            var secondLetter = (intSecondLetter > 64)
                ? (char)intSecondLetter : ' ';
            var thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter,
                thirdLetter).Trim();
        }

    }

}
