using DirectoryScanner.Interface;
using System;
using System.IO;

namespace DirectoryScanner.Common
{
    // Note : Any Undoctumented methods or properties will have been documented in the IDirectoryScanner interface.

    /// <summary>
    /// A threadsafe abstract base class implimenting the IDirectoryScanner interface.
    /// </summary>
    public abstract class IADirectoryScanner : IDirectoryScanner
    {
        protected object   _threadLock   = new object();
        private   String   baseDirectory;
        private   bool     ignoreCase;
        private   bool     ignoreLocale;
        private   long     maxScanDepth;
        private   bool     recursive;
        private   ScanMode scanMode;
        private   String[] searchFor;

        public event EventHandler<DirectoryInfoNode> DirectoryEnteredEvent;
        public event EventHandler<FileInfo>          FileFoundEvent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="BaseDirectory"></param>
        /// <param name="Recursive"></param>
        /// <param name="ScanMode"></param>
        /// <param name="SearchFor"></param>
        internal IADirectoryScanner(String BaseDirectory, bool Recursive, ScanMode ScanMode, String[] SearchFor) : this(BaseDirectory)
        {
            this.Recursive = Recursive;
            this.ScanMode  = ScanMode;
            this.SearchFor = SearchFor;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="BaseDirectory"></param>
        internal IADirectoryScanner(String BaseDirectory)
        {
            this.BaseDirectory = BaseDirectory;

            DynamicRescanning = false;
            MaxScanDepth      = -1;
            IgnoreLocale      = true;
            IgnoreCase        = false;
            Recursive         = false;
            searchFor         = new String[0];
        }

        protected IADirectoryScanner()
        { }

        public bool Active { protected set; get; }

        public String BaseDirectory
        {
            set
            {
                lock (_threadLock)
                {
                    if (Active)
                    {
                        throw Exceptions.ActiveScanException;
                    }

                    if (!Directory.Exists(value))
                    {
                        throw new DirectoryNotFoundException(value);
                    }

                    baseDirectory = value;

                    if (DynamicRescanning)
                    {
                        this.Scan();
                    }
                }
            }
            get
            {
                return baseDirectory;
            }
        }

        /// <summary>
        /// Rescans the directory in the event of a critical property being changed via this method if DynamicRemodelling is set to true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">Property</param>
        /// <param name="value">Value to assign to property</param>
        protected virtual void criticalPropertyChange<T>(ref T property, T value)
        {
            lock (_threadLock)
            {
                if (!property.Equals(value))
                {
                    if (Active)
                    {
                        throw Exceptions.ActiveScanException;
                    }

                    property = value;

                    if (DynamicRescanning)
                    {
                        this.Scan();
                    }
                }
            }
        }

        public bool DynamicRescanning { set; get; }

        public abstract void EndScan();

        public bool IgnoreCase
        {
            set
            {
                criticalPropertyChange(ref ignoreCase, value);
            }

            get
            {
                return ignoreCase;
            }
        }

        public bool IgnoreLocale
        {
            set
            {
                criticalPropertyChange(ref ignoreLocale, value);
            }

            get
            {
                return ignoreLocale;
            }
        }

        public long MaxScanDepth
        {
            set
            {
                criticalPropertyChange(ref maxScanDepth, value);
            }

            get
            {
                return maxScanDepth;
            }
        }

        /// <summary>
        /// Raises DirectoryEnteredEvent in derived classes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected virtual void onDirectoryEntered(IDirectoryScanner sender, DirectoryInfoNode eventArgs)
        {
            if (DirectoryEnteredEvent != null)
            {
                DirectoryEnteredEvent(sender, eventArgs);
            }
        }

        /// <summary>
        /// Raises FileFoundEvent in derived classes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        protected virtual void onFileFound(IDirectoryScanner sender, FileInfo eventArgs)
        {
            if (FileFoundEvent != null)
            {
                FileFoundEvent(sender, eventArgs);
            }
        }

        public bool Recursive
        {
            set
            {
                criticalPropertyChange(ref recursive, value);
            }

            get
            {
                return recursive;
            }
        }

        public abstract void Scan();

        public ScanMode ScanMode
        {
            set
            {
                criticalPropertyChange(ref scanMode, value);
            }

            get
            {
                return scanMode;
            }
        }

        public String[] SearchFor
        {
            set
            {
                criticalPropertyChange(ref searchFor, value);
            }

            get
            {
                return searchFor;
            }
        }
    }
}
