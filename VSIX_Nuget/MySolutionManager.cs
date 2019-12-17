using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSIX_Nuget
{
    public class MySolutionManager: IVsSolutionLoadManager
    {
        #region Implementation of IVsSolutionLoadManager

        public int OnDisconnect()
        {
            Console.WriteLine("OnDisconnect");
            return 0;
        }

        public int OnBeforeOpenProject(ref Guid guidProjectID, ref Guid guidProjectType, string pszFileName,
            IVsSolutionLoadManagerSupport pSLMgrSupport)
        {
            Console.WriteLine($"guidProjectID:{guidProjectID}\nguidProjectType:{guidProjectType}\npszFileName:{pszFileName}");
            return 0;
        }

        #endregion
    }
}