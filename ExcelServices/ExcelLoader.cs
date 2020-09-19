using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using DTO.Exceptions;
using NodaTime;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelServices
{
    public class ExcelLoader : IExcelLoader
    {
        public IEnumerable<object> Read(ExcelMapping mapping, string filename)
        {
            List<object> objects = new List<object>();

            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (directoryName == null)
            {
                throw new NullReferenceException();
            }
            
            string path = Path.Combine(directoryName, filename);

            ISheet sheet;
            using (FileStream stream = new FileStream(
                path,
                FileMode.Open))
            {
                stream.Position = 0;

                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);

                sheet = xssWorkbook.GetSheetAt(mapping.SheetIndex);

                IRow headerRow = sheet.GetRow(mapping.ExpectedHeaderRowIndex);
                
                int cellCount = headerRow.LastCellNum;
                
                ValidateHeaders(mapping, cellCount, headerRow);

                for (int rowIndex = sheet.FirstRowNum + 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);

                    if (row == null || row.Cells.All(d => d.CellType == CellType.Blank))
                    {
                        continue;
                    }

                    object loadedObject = Activator.CreateInstance(mapping.TargetType);

                    AssignProperties(mapping, row, cellCount, loadedObject);
                    
                    objects.Add(loadedObject);
                }
            }

            return objects;
        }

        private static int ValidateHeaders(ExcelMapping mapping, int cellCount, IRow headerRow)
        {
            for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
            {
                if (mapping.ExpectedColumnHeaders.ContainsKey(cellIndex))
                {
                    ICell cell = headerRow.GetCell(cellIndex);

                    string expectedHeaderString = mapping.ExpectedColumnHeaders[cellIndex];

                    if (!cell.StringCellValue.Equals(expectedHeaderString, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IncorrectHeaderException(expectedHeaderString);
                    }
                }
            }

            return cellCount;
        }

        private static void AssignProperties(ExcelMapping mapping, IRow row, int cellCount, object loadedObject)
        {
            for (int cellIndex = row.FirstCellNum; cellIndex < cellCount; cellIndex++)
            {
                if (mapping.ExpectedColumnHeaders.ContainsKey(cellIndex))
                {
                    ICell cell = row.GetCell(cellIndex);

                    if (cell != null)
                    {
                        string stringCellValue = null;

                        if (cell.CellType == CellType.String)
                        {
                            stringCellValue = cell?.StringCellValue;
                        }
                        else if (cell.CellType == CellType.Numeric)
                        {
                            stringCellValue = cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
                        }

                        if (!string.IsNullOrWhiteSpace(stringCellValue))
                        {
                            PropertyInfo propertyInfo = loadedObject.GetType()
                                .GetProperty(mapping.TargetProperties[cellIndex],
                                    BindingFlags.Public | BindingFlags.Instance);

                            if (propertyInfo == null)
                            {
                                throw new MissingMappedPropertyException();
                            }

                            if (!propertyInfo.CanWrite)
                            {
                                throw new CannotWritePropertyException();
                            }

                            SetPropertyOnObject(propertyInfo, loadedObject, stringCellValue);
                        }
                    }
                }
            }
        }

        private static void SetPropertyOnObject(PropertyInfo propertyInfo, object loadedObject, string stringCellValue)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(loadedObject, stringCellValue, null);
            }
            else if (propertyInfo.PropertyType == typeof(LocalDateTime))
            {
                bool isDateTime = DateTime.TryParse(stringCellValue, out DateTime result);

                if (!isDateTime)
                {
                    throw new ExcelParseCellStringValueException(stringCellValue);
                }

                propertyInfo.SetValue(loadedObject, LocalDateTime.FromDateTime(result), null);
            }
            else if (propertyInfo.PropertyType == typeof(ulong))
            {
                bool isUlong = ulong.TryParse(stringCellValue, out ulong result);

                if (!isUlong)
                {
                    throw new ExcelParseCellStringValueException(stringCellValue);
                }

                propertyInfo.SetValue(loadedObject, result, null);
            }
            else if (propertyInfo.PropertyType == typeof(int))
            {
                bool isInt = int.TryParse(stringCellValue, out int result);

                if (!isInt)
                {
                    throw new ExcelParseCellStringValueException(stringCellValue);
                }

                propertyInfo.SetValue(loadedObject, result, null);
            }
            else if (propertyInfo.PropertyType == typeof(Int64))
            {
                bool isInt64 = Int64.TryParse(stringCellValue, out Int64 result);

                if (!isInt64)
                {
                    throw new ExcelParseCellStringValueException(stringCellValue);
                }

                propertyInfo.SetValue(loadedObject, result, null);
            }
            else if (propertyInfo.PropertyType == typeof(decimal))
            {
                bool isDecimal = decimal.TryParse(stringCellValue, out decimal result);

                if (!isDecimal)
                {
                    throw new ExcelParseCellStringValueException(stringCellValue);
                }

                propertyInfo.SetValue(loadedObject, result, null);
            }
            else
            {
                throw new UnsupportedPropertyTypeException(stringCellValue);
            }
        }
    }
}