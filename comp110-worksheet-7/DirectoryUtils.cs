using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
        //path is the file directory
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory)
		{
            //Creating an array of strings called Files, then assigning this path to the list
            string[] Files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            long totalSize = 0;

            //This foreach statement is iterating through the array Files and adding the size of the current file to totalSize
            foreach (string file in Files)
            {
                totalSize += GetFileSize(file);
            }
            return totalSize;
		}

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{

			string[] Files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            return Files.Length;
        }

		// Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
		public static int GetDepth(string directory)
		{
			string[] directories = Directory.GetDirectories(directory);
            int FolderDepth = 0;
            //This foreach statement is iterating through the array directories and adding the 1 to the folder depth
            foreach(string i in directories)
            {
                FolderDepth += 1;
            }
            return FolderDepth;
		}

        // Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory)
        {
            var listOfFiles = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            string smallestFilePath;
            long smallestFile;
            
            smallestFilePath = listOfFiles[0];
            smallestFile = GetFileSize(smallestFilePath);

            foreach (var i in listOfFiles)
            {
                if (GetFileSize(i) < smallestFile)
                {
                    smallestFile = GetFileSize(i);
                    smallestFilePath = i;
                }
            }
            return new Tuple<string, long>(smallestFilePath, smallestFile);
        }

        // Get the path and size (in bytes) of the largest file below the given directory
        public static Tuple<string, long> GetLargestFile(string directory)
        {
            var listOfFiles = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            string LargestFilePath;
            long largestFile;

            LargestFilePath = listOfFiles[0];
            largestFile = GetFileSize(LargestFilePath);

            foreach (var i in listOfFiles)
            {
                if (GetFileSize(i) > largestFile)
                {
                    largestFile = GetFileSize(i);
                    LargestFilePath = i;
                }
            }
            return new Tuple<string, long>(LargestFilePath, largestFile);
        }

        // Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
            List<string> sameValueList = new List<string>();
            var listOfFiles = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (var i in listOfFiles)
            {
                if (GetFileSize(i) == size)
                {
                    sameValueList.Add(i);
                }
            }
            return sameValueList;

		}
	}
}
