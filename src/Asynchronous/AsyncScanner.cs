using DirectoryScanner.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DirectoryScanner.Asynchronous
{
    /// <summary>
    /// An asynchronous thread safe class implimenting the IDirectoryScanner interface.
    /// </summary>
    public sealed class AsyncScanner : IADirectoryScanner
    {
        private List<Batch> batchQueue;

        public AsyncScanner(String BaseDirectory, Boolean Recursive, ScanMode ScanMode, String[] SearchFor) : base(BaseDirectory, Recursive, ScanMode, SearchFor)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="BaseDirectory"></param>
        /// <param name="BatchSize"></param>
        public AsyncScanner(String BaseDirectory, int BatchSize) : base(BaseDirectory)
        {
            this.BatchSize = BatchSize;
        }

        public AsyncScanner(String BaseDirectory) : base(BaseDirectory)
        { }

        public int BatchSize { set; get; }
        
        public override void EndScan()
        {
            lock (base._threadLock)
            {

            }
        }

        public override void Scan()
        {
            lock (base._threadLock)
            {
                if (SearchFor == null)
                {
                    return;
                }

                if (SearchFor.Length < 1)
                {
                    return;
                }

                throw new NotImplementedException();
            }
        }
    }
}
