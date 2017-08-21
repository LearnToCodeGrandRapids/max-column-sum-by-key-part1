using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Ltcgr.IO
{
    public interface IFile
    {
        /// <summary>
        /// Opens an existing UTF-8 encoded text file for reading (this
        /// method wraps System.IO.File.OpenText)
        /// </summary>
        /// <param name="path">The file path to be open for reading</param>
        /// <returns>A StreamReader for the specified file.</returns>
        StreamReader OpenText(string path);
    }

    public class File : IFile
    {
        public StreamReader OpenText(string path)
        {
            return System.IO.File.OpenText(path);
        }
    }
}
