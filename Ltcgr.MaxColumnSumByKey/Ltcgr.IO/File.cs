using System.IO;

namespace Ltcgr.IO
{
    public interface IFile
    {
        /// <summary>
        /// Opens an existing UTF-8 encoded text file for reading (this
        /// method wraps System.IO.File.OpenText).
        /// </summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>A StreamReader on the specified path.</returns>
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