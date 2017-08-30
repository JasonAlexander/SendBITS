using System;
using MSBITS = Microsoft.BackgroundIntelligentTransfer.Management.Interop;

namespace SendBITS.UI
{
    internal class BITSEnum
    {
        public Array AuthenticationScheme
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.AuthenticationScheme));
            }
        }
        public Array AuthenticationTarget
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.AuthenticationTarget));
            }
        }
        public Array ErrorCondition
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.ErrorCondition));
            }
        }
        public Array ErrorContext
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.ErrorContext));
            }
        }
        public Array JobCostState
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.JobCostState));
            }
        }
        public Array JobPropertyId
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.JobPropertyId));
            }
        }
        public Array JobProxyUsage
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.JobProxyUsage));
            }
        }
        public Array JobState
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.JobState));
            }
        }
        public Array JobType
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.JobType));
            }
        }
        public Array LogTarget
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.LogTarget));
            }
        }
        public Array NotifyOptions
        {
            get
            {
                return Enum.GetValues(typeof(MSBITS.NotifyOptions));
            }
        }
    }
}
