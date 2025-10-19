using System.ComponentModel;
using System.Data;
using System.Dynamic;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Toyota.Shared.Extensions;

namespace Toyota.Shared.Helpers
{
    public static class ImportExportHelper
    {
        public static FileContentResult ExportExcel(List<ExcelExportModel> sheets, string author = "", string title = "", string comment = "")
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.Properties.Author = author;
                workbook.Properties.Title = title;
                workbook.Properties.Comments = comment;

                foreach (var sheet in sheets)
                {
                    var worksheet = workbook.Worksheets.Add(sheet.SheetName);
                    CreateWorksheet(sheet, worksheet);
                }
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }

        }

        public static FileContentResult ExportExcel(ExcelExportModel sheet, string author = "", string title = "", string comment = "")
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.Properties.Author = author;
                workbook.Properties.Title = title;
                workbook.Properties.Comments = comment;

                var worksheet = workbook.Worksheets.Add(sheet.SheetName);
                CreateWorksheet(sheet, worksheet);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }

        }

        public static FileContentResult ExportExcel(string jsonStr, string sheetName, string author = "", string title = "", string comment = "")
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.Properties.Author = author;
                workbook.Properties.Title = title;
                workbook.Properties.Comments = comment;

                var headers = new Dictionary<string, string>();

                var worksheet = workbook.Worksheets.Add(sheetName);
                var deserializedModel = JsonConvert.DeserializeObject(jsonStr, (typeof(DataTable)));
                DataTable dt = new DataTable();
                if (deserializedModel != null)
                {
                    dt = (DataTable)deserializedModel;
                }

                foreach (DataColumn item in dt.Columns)
                {
                    headers.Add(item.ToStr(), item.ToStr());
                    worksheet.Cell(1, dt.Columns.IndexOf(item) + 1).SetValue(item.ToStr());
                }

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < headers.Count(); i++)
                    {
                        worksheet.Cell(dt.Rows.IndexOf(row) + 2, i + 1).SetValue(row[headers.ElementAt(i).Key]);
                    }
                }

                worksheet.Row(1).CellsUsed().Style.Font.SetBold();
                worksheet.Row(1).CellsUsed().Style.Font.SetFontSize(12);
                worksheet.Row(1).CellsUsed().Style.Fill.SetBackgroundColor(XLColor.LightGray);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }

        }

        private static void CreateWorksheet(ExcelExportModel model, IXLWorksheet worksheet)
        {
            var firstElement = model.Entities.FirstOrDefault();
            if (firstElement == null)
            {
                return;
            }
            Type curEntityType = firstElement.GetType();

            if (model.ReadProperties)
            {
                model.Headers = new Dictionary<string, string>();
                var properties = curEntityType.GetProperties();
                foreach (var item in properties)
                {
                    model.Headers.Add(item.Name, item.Name);
                }
            }
            else if (model.ReadDisplayName)
            {
                model.Headers = new Dictionary<string, string>();
                var properties = curEntityType.GetProperties();
                foreach (var item in properties)
                {
                    var attribute = item.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault();
                    if (attribute != null)
                        model.Headers.Add(item.Name, ((DisplayNameAttribute)attribute).DisplayName);
                    else
                        model.Headers.Add(item.Name, item.Name);
                }
            }

            for (int i = 0; i < model.Headers.Count(); i++)
            {
                worksheet.Cell(1, i + 1).SetValue(model.Headers.ElementAt(i).Value);
            }

            foreach (var entity in model.Entities)
            {
                for (int i = 0; i < model.Headers.Count(); i++)
                {
                    worksheet.Cell(model.Entities.IndexOf(entity) + 2, i + 1).SetValue(entity.GetType().GetProperty(model.Headers.ElementAt(i).Key).GetValue(entity, null));
                }
            }

            worksheet.Row(1).CellsUsed().Style.Font.SetBold();
            worksheet.Row(1).CellsUsed().Style.Font.SetFontSize(12);
            worksheet.Row(1).CellsUsed().Style.Fill.SetBackgroundColor(XLColor.LightGray);


        }


        /// <summary>
        /// Headers da ki key Exceldeki columnname, value ise T nin propert name i
        /// useExcelColumn true ise Headers yerine T nin propertylerini kullanir
        /// </summary>
        public static List<T> ImportExcel<T>(IFormFile file, bool useExcelColumn = false, Dictionary<string, string> Headers = null)
        {
            var responseList = new List<T>();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                using (var workbook = new XLWorkbook(ms))
                {
                    var props = typeof(T).GetProperties();
                    if (useExcelColumn)
                    {
                        Headers = new Dictionary<string, string>();
                        props.ToList().ForEach(x => Headers.Add(x.Name, x.Name));
                    }

                    var worksheet = workbook.Worksheet(1);
                    int columnCount = worksheet.Columns().Count();

                    for (int i = 2; i <= worksheet.Rows().Count(); i++)
                    {
                        T obj = (T)Activator.CreateInstance(typeof(T));
                        for (int j = 1; j <= columnCount; j++)
                        {
                            var propName = string.Empty;
                            if (Headers.TryGetValue(worksheet.Cell(1, j).Value?.ToString(), out propName))
                            {
                                var prop = props.FirstOrDefault(x => x.Name == propName);
                                if (prop != null)
                                {
                                    prop.SetValue(obj, Convert.ChangeType(worksheet.Cell(i, j).Value, prop.PropertyType), null);
                                }
                            }
                        }
                        responseList.Add(obj);
                    }
                }
            }
            return responseList;
        }


        /// <summary>
        /// cleanProperties = true => propertylerin adlarini temizler 
        /// </summary>
        public static List<dynamic> ImportExcel(IFormFile file, bool cleanProperties = false)
        {
            var responseList = new List<dynamic>();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                using (var workbook = new XLWorkbook(ms))
                {
                    var worksheet = workbook.Worksheet(1);
                    int columnCount = worksheet.Columns().Count();

                    for (int i = 2; i <= worksheet.Rows().Count(); i++)
                    {
                        dynamic obj = new System.Dynamic.ExpandoObject();

                        for (int j = 1; j <= columnCount; j++)
                        {
                            string propName = worksheet.Cell(1, j).Value.ToStr();
                            if (cleanProperties)
                            {
                                propName.ToStr().Trim().Replace(' ', '_');
                            }
                            AddProperty(obj, propName, worksheet.Cell(i, j).Value);
                        }
                        responseList.Add(obj);
                    }
                }
            }
            return responseList;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
        public static dynamic GetPropertyValue(dynamic expando, string propertyName)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                return expandoDict[propertyName];
            else
                return null;
        }

        #region 

        //public static FileContentResult DashboardGeneralTeamStatisticReportExcelExport(List<object> entities, int[,,] breakdowns, string author = "", string title = "", string comment = "")
        //{
        //    using (var workbook = new XLWorkbook())
        //    {
        //        workbook.Properties.Author = author;
        //        workbook.Properties.Title = title;
        //        workbook.Properties.Comments = comment;


        //        var worksheet = workbook.Worksheets.Add("TeamStatisticReportExcel");
        //        Type curEntityType = entities.FirstOrDefault().GetType();

        //        worksheet.Cell(1, 1).SetValue("Hafta");
        //        worksheet.Cell(2, 1).SetValue("Toplam Unique kullanıcı");
        //        worksheet.Cell(3, 1).SetValue("Toplam Unique takım");
        //        worksheet.Cell(4, 1).SetValue("Unique kullanıcı-takım kuran");
        //        worksheet.Cell(5, 1).SetValue("Unique Takım");
        //        worksheet.Cell(6, 1).SetValue("Abonelik Sahibi Güncellenen Takım");
        //        worksheet.Cell(7, 1).SetValue("Abonelik Sahibi Güncellenmeyen Takım");
        //        worksheet.Cell(8, 1).SetValue("Abonelik Sahibi Güncelleyen Kullanıcı");
        //        worksheet.Cell(9, 1).SetValue("Abonelik Sahibi Güncellemeyen Kullanıcı");
        //        foreach (var entity in entities)
        //        {
        //            worksheet.Cell(1, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("ContestId").GetValue(entity, null));
        //            worksheet.Cell(2, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("TotalUniqueUserCount").GetValue(entity, null));
        //            worksheet.Cell(3, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("TotalUniqueTeamCount").GetValue(entity, null));
        //            worksheet.Cell(4, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("UniqueUserCount").GetValue(entity, null));
        //            worksheet.Cell(5, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("UniqueTeamCount").GetValue(entity, null));
        //            worksheet.Cell(6, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("UpdatedTeamCountHaveBoosterSubscripiton").GetValue(entity, null));
        //            worksheet.Cell(7, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("NotUpdatedTeamCountHaveBoosterSubscripiton").GetValue(entity, null));
        //            worksheet.Cell(8, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("UpdatedUserCountHaveBoosterSubscripiton").GetValue(entity, null));
        //            worksheet.Cell(9, entities.IndexOf(entity) + 2).SetValue(entity.GetType().GetProperty("NotUpdatedUserCountHaveBoosterSubscripiton").GetValue(entity, null));
        //        }

        //        worksheet.Cell(10, 1).SetValue("Hafta");
        //        worksheet.Cell(11, 1).SetValue("Editlenen/Kaydedilen");
        //        worksheet.Cell(12, 1).SetValue("Hiç editlenmemiş kadro sayısı");


        //        for (int i = 0; i < breakdowns.GetLength(0); i++)
        //        {
        //            worksheet.Cell(10, i + 2).SetValue(i + 1);
        //            worksheet.Cell(i + 13, 1).SetValue(i + 1);

        //            int sumUpdated = 0;
        //            int sumUnUpdated = 0;

        //            for (int j = 0; j < breakdowns.GetLength(1); j++)
        //            {
        //                worksheet.Cell(i + 13, j + 2).SetValue(breakdowns[i, j, 0]);
        //                sumUpdated += breakdowns[j, i, 0];
        //                sumUnUpdated += breakdowns[j, i, 1];
        //            }
        //            worksheet.Cell(11, i + 2).SetValue(sumUpdated);
        //            worksheet.Cell(12, i + 2).SetValue(sumUnUpdated);
        //        }

        //        worksheet.Row(1).CellsUsed().Style.Font.SetBold();
        //        worksheet.Row(1).CellsUsed().Style.Font.SetFontSize(12);
        //        worksheet.Row(1).CellsUsed().Style.Fill.SetBackgroundColor(XLColor.LightGray);

        //        worksheet.Row(10).CellsUsed().Style.Font.SetBold();
        //        worksheet.Row(10).CellsUsed().Style.Font.SetFontSize(12);
        //        worksheet.Row(10).CellsUsed().Style.Fill.SetBackgroundColor(XLColor.LightGray);

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            workbook.SaveAs(memoryStream);
        //            return new FileContentResult(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //        }
        //    }

        //}
        #endregion



    }
    public class ExcelExportModel
    {
        public ExcelExportModel()
        {
            Headers = new Dictionary<string, string>();
            ReadDisplayName = false;
            ReadProperties = false;
        }
        public List<object> Entities { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string SheetName { get; set; }

        /// <summary>
        /// true ise sadece "DisplayName" attribute u olan propertyleri alir
        /// </summary>
        public bool ReadDisplayName { get; set; }

        /// <summary>
        /// true ise "headers" i kullanmaz class icindeki propertileri okur
        /// </summary>
        public bool ReadProperties { get; set; }
    }
}
