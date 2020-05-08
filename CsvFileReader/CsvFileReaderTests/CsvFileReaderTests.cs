using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvFileReader;
using CsvFileReader.Test;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CsvFileReaderTests
{
    [TestClass]
    public class CsvFileReaderTests
    {
        [TestMethod]
        public void ReadFromFileWithHeaders_CallHeadersMethod_NoHeadersPassed()
        {
            var reader = new StreamReader("personsWithHeaders.csv");
            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, true);

            Dictionary<string, string> expectedResult = new Dictionary<string, string>();
            expectedResult.Add("FirstName", "John");
            expectedResult.Add("LastName", "Doe");
            expectedResult.Add("Age", "12");

            var actualResult = csvFileReader.ReadRecord();

            foreach (var key in expectedResult.Keys)
            {
                Assert.IsTrue(expectedResult[key] == actualResult[key]);
            }
        }

        [TestMethod]
        public void ReadFromFileWithHeaders_CallHeadersMethod_ValidHeadersPassed()
        {
            var reader = new StreamReader("personsWithHeaders.csv");

            List<string> headers = new List<string>()
            {
                "FirstName",
                "LastName",
                "Age"
            };

            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, true, headers);

            Dictionary<string, string> expectedResult = new Dictionary<string, string>();
            expectedResult.Add("FirstName", "John");
            expectedResult.Add("LastName", "Doe");
            expectedResult.Add("Age", "12");

            var actualResult = csvFileReader.ReadRecord();

            foreach (var key in expectedResult.Keys)
            {
                Assert.IsTrue(expectedResult[key] == actualResult[key]);
            }
        }

        [TestMethod]
        public void ReadFromFileWithHeaders_CallNoHeadersMethod_HeadersRequired()
        {
            var reader = new StreamReader("personsWithHeaders.csv");
            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, true);

            string expectedResult = "This method can't be used if there are headers in file.";

            try
            {
                var answer = csvFileReader.ReadValues();
            }
            catch (Exception e)
            {
                Assert.IsTrue(expectedResult.CompareTo(e.Message) == 0);
            }
        }

        [TestMethod]
        public void ReadFromFileWithHeaders_CallNoHeadersMethod_HeadersNotRequired()
        {
            var reader = new StreamReader("personsWithHeaders.csv");
            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, false);

            string expectedResult = "This method can't be used if there are headers in file.";

            try
            {
                var answer = csvFileReader.ReadValues();
            }
            catch (Exception e)
            {
                Assert.IsTrue(expectedResult.CompareTo(e.Message) == 0);
            }
        }

        [TestMethod]
        public void ReadFromFileWithoutHeaders_CallHeadersMethod_HeadersRequired()
        {
            var reader = new StreamReader("persons.csv");
            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, true);

            string expectedResult = "This method can't be used if there headers in file.";

            try
            {
                var answer = csvFileReader.ReadRecord();
            }
            catch (Exception e)
            {
                Assert.IsTrue(expectedResult.CompareTo(e.Message) == 0);
            }
        }

        [TestMethod]
        public void ReadFromFileWithoutHeaders_CallHeadersMethod_HeadersNotRequired()
        {
            var reader = new StreamReader("persons.csv");
            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, false);

            string expectedResult = "This method can't be used if there are no headers in file.";

            try
            {
                var answer = csvFileReader.ReadRecord();
            }
            catch (Exception e)
            {
                Assert.IsTrue(expectedResult.CompareTo(e.Message) == 0);
            }
        }

        [TestMethod]
        public void ReadFromFileWithoutHeaders_CallNoHeadersMethod_HeadersRequired()
        {
            var reader = new StreamReader("persons.csv");
            CsvFileReader<Person> csvFileReader = new CsvFileReader<Person>(reader, true);
            string expectedResult = "This method can't be used if there are headers in file.";

            try
            {
                var answer = csvFileReader.ReadRecord();
            }
            catch (Exception e)
            {
                Assert.IsTrue(expectedResult.CompareTo(e.Message) == 0);
            }
        }
    }
}
