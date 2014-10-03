using DirectoryScanner.Common;
using System;
using System.IO;

namespace DirectoryScanner.Interface
{
    /// <summary>
    /// Interface outlining basic fuctions to be implimented by all directory scanners.
    /// </summary>
    public interface IDirectoryScanner
    {
        /// <summary>
        /// The base parent directory for scan.
        /// </summary>
        string BaseDirectory { set; get; }

        /// <summary>
        /// Event to be raised in the event of adding a DirectoryInfoNode to the DirectoryMap.
        /// </summary>
        event EventHandler<DirectoryInfoNode> DirectoryEnteredEvent;

        /// <summary>
        /// Indicates weather or not re-scans of the directory should occure when search critical properties are ultered.
        /// </summary>
        bool DynamicRescanning { set; get; }

        /// <summary>
        /// Event to be raised in the event of a file being found that matches the search criteria.
        /// </summary>
        event EventHandler<FileInfo> FileFoundEvent;

        /// <summary>
        /// Indicates if the search terms in SearchFor should be considered case sensitive.
        /// </summary>
        bool IgnoreCase { set; get; }

        /// <summary>
        /// Indicates if the search terms in SearchFor are literal comparisons or locale equvalent comparisons.
        /// </summary>
        bool IgnoreLocale { set; get; }

        /// <summary>
        /// Indicates how far down form the parent directory a recursive scan is allowed to continue scanning.
        /// </summary>
        long MaxScanDepth { set; get; }

        /// <summary>
        /// Indicates weather or not the scanner should also scan sub-directories.
        /// </summary>
        bool Recursive { set; get; }

        /// <summary>
        /// Indicates what portion of the file name should be matched against the search terms in SearchFor.
        /// </summary>
        ScanMode ScanMode { set; get; }

        /// <summary>
        /// An array containing the strings to match files against.
        /// </summary>
        String[] SearchFor { set; get; }

        /// <summary>
        /// Terminates any ongoing directory scanning.
        /// </summary>
        void EndScan();

        /// <summary>
        /// Initiates directory scan.
        /// </summary>
        void Scan();
    }
}
