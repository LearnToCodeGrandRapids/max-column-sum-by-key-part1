using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Ltcgr.IO;
using Ltcgr.MaxColumnSumByKey.Models;

namespace Ltcgr.MaxColumnSumByKey
{
    public interface IProcessor
    {
        /// <summary>
        /// Processes the given file by grouping the values associated
        /// with each key, summing them, and then returning the key
        /// and total for the key with the greatest total.
        /// </summary>
        /// <param name="filePath">Absolute path to the tab separated file containing the records</param>
        /// <param name="keyPosition">The column of the key in the record (first pos is 0)</param>
        /// <param name="valuePosition">The column of the value corresponding to the key in the record (first pos is 0)</param>
        /// <returns>A record representing the key with the height sum value</returns>
        Record ProcessFile(string filePath, int keyPosition, int valuePosition);

        /// <summary>
        /// Dynamically create a map for the Result class for CsvHelper
        /// </summary>
        /// <param name="keyPosition">The column of the key in the record (first pos is 0)</param>
        /// <param name="valuePosition">The column of the value corresponding to the key in the record (first pos is 0)</param>
        /// <returns>A CsvClassMap with mapping information for Record class</returns>
        DefaultCsvClassMap<Record> CreateRecordMap(int keyPosition, int valuePosition);
    }

    public class Processor : IProcessor
    {
        private IFile File { get; }

        public Processor(IFile file)
        {
            File = file;
        }

        public Record ProcessFile(string filePath, int keyPosition, int valuePosition)
        {
            List<Record> records;

            var map = CreateRecordMap(keyPosition, valuePosition);
            
            using (var reader = File.OpenText(filePath))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = "\t";
                csv.Configuration.RegisterClassMap(map);
                records = csv.GetRecords<Record>().ToList();
            }

            var result = records
                .GroupBy(r => r.Key)
                .Select(rg => new Record { Key = rg.Key, Value = rg.Sum(x => x.Value) })
                .OrderByDescending(x => x.Value)
                .First();

            return result;
        }

        public DefaultCsvClassMap<Record> CreateRecordMap(int keyPosition, int valuePosition)
        {
            var map = new DefaultCsvClassMap<Record>();

            var keyProperty = typeof(Record).GetProperty(nameof(Record.Key));
            var valueProperty = typeof(Record).GetProperty(nameof(Record.Value));

            var keyMap = new CsvPropertyMap(keyProperty);
            keyMap.Index(keyPosition);

            var valueMap = new CsvPropertyMap(valueProperty);
            valueMap.Index(valuePosition);

            map.PropertyMaps.Add(keyMap);
            map.PropertyMaps.Add(valueMap);

            return map;
        }
    }
}