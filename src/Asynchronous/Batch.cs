using System;
using System.IO;

namespace DirectoryScanner.Asynchronous
{
    /// <summary>
    /// A thread safe struct for batch FileInfo Reads.
    /// </summary>
    internal struct Batch
    {
        private readonly object     _ThreadLock;
        private readonly FileInfo[] fileInfoArray;
        
        private int next;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="FileInfoArray">Array of FileInfo objects to be held in this batch.</param>
        public Batch(FileInfo[] FileInfoArray)
        {
            _ThreadLock   = new object();
            fileInfoArray = FileInfoArray;
            next          = 0;
            Size          = FileInfoArray.Length;
        }

        /// <summary>
        /// Progress of thread through batch;
        /// </summary>
        public float progress
        {
            get
            {
                return (next / Size);
            }
        }

        /// <summary>
        /// Returns next FileInfo object in batch.
        /// </summary>
        public FileInfo Next
        {
            get
            {
                lock(_ThreadLock)
                {
                    if (progress.Equals(1))
                    {
                        return null;
                    }
                    else
                    {
                        next++;
                    }

                    return fileInfoArray[next];
                }
            }
        }

        /// <summary>
        /// Returns FileInfo object at Index.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public FileInfo this[int i]
        {
            get
            {
                if (i < 0 || i > Size)
                {
                    throw new IndexOutOfRangeException();
                }

                return fileInfoArray[i];
            }
        }

        /// <summary>
        /// Indicates the size of this batch object instance.
        /// </summary>
        public readonly int Size;
    }
}
