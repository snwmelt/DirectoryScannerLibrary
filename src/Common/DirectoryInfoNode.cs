using System.IO;

namespace DirectoryScanner.Common
{
    /// <summary>
    /// Immutable struct to aid in sub-directory mapping.
    /// </summary>
    public struct DirectoryInfoNode
    {
        /// <summary>
        /// Indicates how many sub-directories between this node and the base directory
        /// </summary>
        public readonly long Depth;

        /// <summary>
        /// Directory information of this node.
        /// </summary>
        public readonly DirectoryInfo DirectoryInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DirectoryInfo">DirectoryInfo for a given directory.</param>
        /// <param name="Depth">The number of directories between this and the base directory.</param>
        public DirectoryInfoNode(DirectoryInfo DirectoryInfo, long Depth)
        {
            this.Depth         = Depth;
            this.DirectoryInfo = DirectoryInfo;
        }
    }
}

