using DirectoryScanner.Common;
using System;
using System.IO;

namespace DirectoryScanner.Synchronous
{
    // Note : Any Undoctumented methods or properties will have been documented in the IADirectoryScanner base class or the IDirectoryScanner interface.
    // TODO : Build in fault tollerance.

    /// <summary>
    /// A synchronous thread safe class implimenting the IDirectoryScanner interface.
    /// </summary>
    public sealed class Scanner : IADirectoryScanner
    {
        public Scanner(String BaseDirectory, bool Recursive, ScanMode ScanMode, String[] SearchFor) : base(BaseDirectory, Recursive, ScanMode, SearchFor)
        { }

        public Scanner(String BaseDirectory) : base(BaseDirectory)
        { }

        public override void EndScan()
        {
            lock (base._threadLock)
            {
                Active = false;
            }
        }

        /// <summary>
        /// Recursively enters sub-directories calling shallowScan method.
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="directoryInfoNodeDepth"></param>
        private void recursiveScan(DirectoryInfo directoryInfo, long directoryInfoNodeDepth)
        {
            if (!Active)
                return;

            if (directoryInfoNodeDepth > MaxScanDepth && !MaxScanDepth.Equals(-1))
            {
                return;
            }

            base.onDirectoryEntered(this, new DirectoryInfoNode(directoryInfo, directoryInfoNodeDepth));

            DirectoryInfo[] directoryDirectoryInfo = directoryInfo.GetDirectories();

            long nodeDepth = (directoryInfoNodeDepth + 1);

            for (int i = 0; i < directoryDirectoryInfo.Length; i++)
            {
                if (!Active)
                    break;

                shallowScan(directoryDirectoryInfo[i], nodeDepth);
                recursiveScan(directoryDirectoryInfo[i], nodeDepth);
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

                Active = true;

                if (Recursive)
                {
                    shallowScan(new DirectoryInfo(BaseDirectory), 0);
                    recursiveScan(new DirectoryInfo(BaseDirectory), 0);
                }
                else
                {
                    shallowScan(new DirectoryInfo(BaseDirectory), 0);
                }

                Active = false;
            }
        }

        /// <summary>
        /// Scans current directory for files matching search criteria.
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="directoryInfoNodeDepth"></param>
        private void shallowScan(DirectoryInfo directoryInfo, long directoryInfoNodeDepth)
        {
            FileInfo[] directoryFileInfo = directoryInfo.GetFiles();

            string matchString = null;

            // Iterates through the files in the directory building a string for comparison against SearchFor contents.
            for (int i = 0; i < directoryFileInfo.Length; i++)
            {
                switch(ScanMode)
                {
                    case ScanMode.MatchExtension:
                        matchString = directoryFileInfo[i].Extension;
                        break;

                    case ScanMode.MatchName:
                        matchString = directoryFileInfo[i].Name;
                        break;

                    case ScanMode.MatchNameAndExtension:
                        matchString = directoryFileInfo[i].Name + directoryFileInfo[i].Name;
                        break;
                }

                // Iterates through the SearchFor array of match strings comparing against the matchString in the current loop itteration.
                for (int x = 0; x < SearchFor.Length; x++)
                {
                    if (!IgnoreCase && IgnoreLocale)
                    {
                        if (matchString.Equals(SearchFor[x]))
                        {
                            onFileFound(this, directoryFileInfo[i]);
                        }
                    }
                    else if (IgnoreCase && IgnoreLocale)
                    {
                        throw new NotImplementedException();
                    }
                    else if (IgnoreCase && !IgnoreLocale)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
