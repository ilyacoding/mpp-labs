using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Lab4.Library
{
    public class FileStreamResourceHandler : OSHandle<FileStream>
    {
        public FileStreamResourceHandler(FileStream fileStream)
        {
            Resource = fileStream;
            Handle = fileStream.Handle;
        }
        
        ~FileStreamResourceHandler()
        {
            Dispose(false);
        }
    }
}