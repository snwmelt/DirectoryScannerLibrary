using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Common
{
    /// <summary>
    /// A static class holding common exceptions for use in IDirectoryScanner implimentations.
    /// </summary>
    internal static class Exceptions
    {
        public  static TypeAccessException ActiveScanException        = new TypeAccessException(activeScanExceptionMessage);
        private static string              activeScanExceptionMessage = @"Fields Or Properties Cannot Be Modified Whilst A Scan Is Occuring";
    }
}
