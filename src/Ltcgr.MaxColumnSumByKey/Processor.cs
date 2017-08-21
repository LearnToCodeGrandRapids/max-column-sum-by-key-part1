using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ltcgr.MaxColumnSumByKey.Models;
using Ltcgr.IO;

namespace Ltcgr.MaxColumnSumByKey
{
    public interface IProcessor
    {
        /// <summary>
        /// Processes the given file by grouping the value associated
        /// with each key, summing them, and then returning the key
        /// and total for the key wit the greatest total.
        /// </summary>
        /// <param name="filePath">Absolute path to the tab separated file</param>
        /// <param name="keyPosition">The column of the field in the record containing
        /// the key</param>
        /// <param name="valuePosition">The column of the field in the record 
        /// containing the value</param>
        /// <returns>A record representing the key, and aggregate total of values</returns>
        Record ProcessFile(string filePath, int keyPosition, int valuePosition);
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
            return null;
        }
    }
}
