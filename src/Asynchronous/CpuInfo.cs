using System;
using System.Management;

namespace DirectoryScanner.Asynchronous
{
    public static class CpuInfo
    {
        /// <summary>
        /// Represents the number of logical processing units across all centeral processing units.
        /// </summary>
        public static readonly int LogicalCoreCount = Environment.ProcessorCount;

        /// <summary>
        /// Represents the number of physical processing cores across all centeral processing untis.
        /// </summary>
        public static int PhysicalCoreCount
        {
            get
            {
                int physicalCoreCount = 0;

                foreach (var item in new ManagementObjectSearcher("Select NumberOfCores from Win32_Processor").Get())
                {
                    physicalCoreCount += int.Parse(item["NumberOfCores"].ToString());
                }

                return physicalCoreCount;
            }
        }

        /// <summary>
        /// Returns the number of central processing units available.
        /// </summary>
        public static int ProcessorCount
        {
            get
            {
                int processorCount = 0;

                foreach (var item in new ManagementObjectSearcher("Select NumberOfProcessors from Win32_ComputerSystem").Get())
                {
                    processorCount += int.Parse(item["NumberOfProcessors"].ToString());
                }

                return processorCount;
            }
        }
    }
}
