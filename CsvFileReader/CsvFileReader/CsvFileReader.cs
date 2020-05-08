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
        private CsvReader CsvReader { get; set; }
        private readonly List<string> headers;

        public CsvFileReader(StreamReader streamReader, bool headersRequired, List<string> headers = null)
        {
            CsvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            CsvReader.Configuration.HasHeaderRecord = true;

            if (headersRequired)
            {
                if (headers != null)
                {
                    this.headers = headers;
                    CsvReader.Read();
                    CsvReader.ReadHeader();
                }
                else
                {
                    CsvReader.Read();
                    if (!CsvReader.ReadHeader())
                    {
                        CsvReader.Configuration.HasHeaderRecord = false;
                    }
                    this.headers = CsvReader.Context.HeaderRecord.ToList();
                }
            }
            else
            {
                CsvReader.Read();
                CsvReader.ReadHeader();
                CsvReader.Configuration.HasHeaderRecord = false;
            }
        }

        public List<dynamic> ReadValues()
        {
            if (headers != null)
            {
                throw new Exception("This method can't be used if there are headers in file.");
            }

            CsvReader.Read();

            T record = CsvReader.GetRecord<T>();
            List<dynamic> values = new List<dynamic>();

            foreach (var field in typeof(T).GetProperties())
            {
                values.Add(field.GetValue(record).ToString());
            }

            return values;
        }

        public Dictionary<string, string> ReadRecord()
        {
            if (headers == null)
            {
                throw new Exception("This method can't be used if there are no headers in file.");
            }

            Dictionary<string, string> values = new Dictionary<string, string>();

            CsvReader.Read();

            foreach (var column in headers)
            {
                values.Add(column, CsvReader.GetField(column));
            }

            return values;
        }
    }
}
