using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSIX_Nuget
{
    public class MyVsSolutionLoadEvents: IVsSolutionLoadEvents
    {
        #region Implementation of IVsSolutionLoadEvents

        public int OnBeforeOpenSolution(string pszSolutionFilename)
        {
            throw new System.NotImplementedException();
        }

        public int OnBeforeBackgroundSolutionLoadBegins()
        {
            throw new System.NotImplementedException();
        }

        public int OnQueryBackgroundLoadProjectBatch(out bool pfShouldDelayLoadToNextIdle)
        {
            throw new System.NotImplementedException();
        }

        public int OnBeforeLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            throw new System.NotImplementedException();
        }

        public int OnAfterLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            throw new System.NotImplementedException();
        }

        public int OnAfterBackgroundSolutionLoadComplete()
        {
            //throw new System.NotImplementedException();
            Guid guid = Guid.Empty;
            Microsoft.VisualStudio.Shell.Interop.IEnumHierarchies en;
            PackageHelper.VsSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_ALLINSOLUTION, ref guid, out en);
            IVsHierarchy[] hierarchy = new IVsHierarchy[1];
            uint fetched;
            while (en.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1)
            {
                if (hierarchy.Length > 0 && hierarchy[0] != null)
                {
                    var proj = PackageHelper.GetDTEProject(hierarchy[0]);
                    var allPackages = PackageHelper.GetInstalledPackages(proj);
                    foreach (var vsPackageMetadata in allPackages)
                    {
                        PackageHelper.InstallPackageToProject(proj, vsPackageMetadata.Id);
                    }
                }

            }
            return 0;
        }

        #endregion
    }
}