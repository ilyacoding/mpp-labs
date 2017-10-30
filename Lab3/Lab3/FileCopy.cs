using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadPoolLib;
using SearchOption = System.IO.SearchOption;

namespace Lab3
{
    public static class FileCopy
    {
        public static int Copy(string sourcePath, string destinationPath)
        {
            var filesCopied = 0;
            
            using (var pool = new ThreadPoolParametrized(10))
            {
                if (!Directory.Exists(sourcePath))
                {
                    throw new DirectoryNotFoundException("Source directory not found: " + sourcePath);
                }

                if (Directory.Exists(destinationPath))
                {
                    throw new DirectoryException("Destination directory already exists: " + destinationPath);
                }

                Directory.CreateDirectory(destinationPath);

                foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    var parameters = new FileCopyParams { DestinationPath = dirPath.Replace(sourcePath, destinationPath) };
                    pool.EnqueueTask(CreateDirectory, parameters);
                }
                pool.WaitAll();

                foreach (var newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    var parameters = new FileCopyParams { SourcePath = newPath, DestinationPath = newPath.Replace(sourcePath, destinationPath) };
                    pool.EnqueueTask(CopyFile, parameters);
                }
                filesCopied = pool.WaitAll();
            }

            return filesCopied;
        }

        private static void CopyFile(Object fileCopyParams)
        {
            var copyParams = (FileCopyParams) fileCopyParams;
            File.Copy(copyParams.SourcePath, copyParams.DestinationPath, true);
        }

        private static void CreateDirectory(Object fileCopyParams)
        {
            var copyParams = fileCopyParams as FileCopyParams;
            Directory.CreateDirectory(copyParams.DestinationPath);
        }
        
        public static long GetDirectorySize(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.EnumerateFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);
        }
    }
}
