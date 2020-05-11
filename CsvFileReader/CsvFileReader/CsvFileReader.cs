using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvFileReader
{
    public class CsvFileReader<T>
    {
        private readonly CsvReader csvReader;
        private readonly List<string> headers;

        public CsvFileReader(StreamReader streamReader, bool headersRequired, List<string> passedHeaders = null)
        {
            csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            headers = ReadHeaders(headersRequired, passedHeaders);
        }
            
        public List<string> ReadValues()
        {
            if (headers != null)
            {
                throw new Exception("This method can't be used if there are headers in file.");
            }

            csvReader.Read();

            var record = csvReader.GetRecord<T>();

            return typeof(T)
                .GetProperties()
                .Select(field => field.GetValue(record).ToString())
                .ToList();
        }

        public Dictionary<string, string> ReadRecord()
        {
            if (headers == null)
            {
                throw new Exception("This method can't be used if there are no headers in file.");
            }

            csvReader.Read();

            return headers.ToDictionary(column => column, column => csvReader.GetField(column));
        }

        private List<string> ReadHeaders(bool headersRequired, List<string> headers = null)
        {
            csvReader.Read();
            if (!csvReader.ReadHeader())
            {
                csvReader.Configuration.HasHeaderRecord = false;
            }

            if (headersRequired)
            {
                return headers ?? csvReader.Context.HeaderRecord.ToList();
            }
            else
            {
                csvReader.Configuration.HasHeaderRecord = false;
                return null;
            }
        }
    }
}
