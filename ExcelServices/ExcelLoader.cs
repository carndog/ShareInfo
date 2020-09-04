using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DTO.Exceptions;
using NodaTime;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelServices
{
    public class ExcelLoader
    {
        public IEnumerable<object> Read(ExcelMapping mapping)
        {
            List<object> objects = new List<object>();
            
            string path = Path.Combine(Environment.CurrentDirectory, "file.xlsx");

            ISheet sheet;
            using (var stream = new FileStream(
                path,
                FileMode.Open))
            {
                stream.Position = 0;

                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);

                sheet = xssWorkbook.GetSheetAt(mapping.SheetIndex);

                IRow headerRow = sheet.GetRow(mapping.ExpectedHeaderRowIndex);

                int cellCount = headerRow.LastCellNum;

                if (cellCount != mapping.ExpectedColumnHeaders.Count)
                {
                    throw new IncorrectHeaderCountException();
                }

                for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                {
                    ICell cell = headerRow.GetCell(cellIndex);

                    string expectedHeaderString = mapping.ExpectedColumnHeaders[cellIndex];

                    if (cell.StringCellValue != expectedHeaderString)
                    {
                        throw new IncorrectHeaderException(expectedHeaderString);
                    }
                }

                for (int rowIndex = (sheet.FirstRowNum + 1); rowIndex <= sheet.LastRowNum; rowIndex++)
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

        private static void AssignProperties(ExcelMapping mapping, IRow row, int cellCount, object loadedObject)
        {
            for (int cellIndex = row.FirstCellNum; cellIndex < cellCount; cellIndex++)
            {
                string stringCellValue = row.GetCell(cellIndex)?.StringCellValue;

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