using System;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSIX_Nuget
{
    public class MySolutionEvents: IVsSolutionEvents, IVsSolutionLoadEvents
    {
        #region Implementation of IVsSolutionEvents

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            //IVsHierarchy[] hierarchy = new IVsHierarchy[1]{ pHierarchy };
            //Guid guid = Guid.Empty;
            //IEnumHierarchies en;
            //PackageHelper.VsSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_ALLINSOLUTION, ref guid, out en);
            //uint fetched;
            //while (en.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1)
            //{
            //    //if (hierarchy.Length > 0 && hierarchy[0] != null)
            //    {
            //        var proj = GetDTEProject(pHierarchy);
            //        var c = proj.Collection;
            //        var rator = c.GetEnumerator();
            //        while (rator.MoveNext())
            //        {
            //            var cur = rator.Current;
            //            var tmpP = cur as Project;
            //        }
            //        try
            //        {
            //            var allPackages = PackageHelper.GetInstalledPackages(proj);
            //            foreach (var vsPackageMetadata in allPackages)
            //            {
            //                PackageHelper.InstallPackageToProject(proj, vsPackageMetadata.Id);
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //        }
                   
            //    }
            //}

            //throw new System.NotImplementedException();
               

            return 0;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            throw new System.NotImplementedException();
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            throw new System.NotImplementedException();
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            throw new System.NotImplementedException();
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            throw new System.NotImplementedException();
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            throw new System.NotImplementedException();
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            ////throw new System.NotImplementedException();
            ////todo 读取所有项目的引用包，并更新到最新包
            //Guid guid = Guid.Empty;
            //Microsoft.VisualStudio.Shell.Interop.IEnumHierarchies en;
            //PackageHelper.VsSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_ALLINSOLUTION, ref guid, out en);
            //IVsHierarchy[] hierarchy = new IVsHierarchy[1];
            //uint fetched;
            //while (en.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1)
            //{
            //    if (hierarchy.Length > 0 && hierarchy[0] != null)
            //    {
            //        var proj = GetDTEProject(hierarchy[0]);
            //        var allPackages = PackageHelper.GetInstalledPackages(proj);
            //        foreach (var vsPackageMetadata in allPackages)
            //        {
            //            PackageHelper.InstallPackageToProject(proj, vsPackageMetadata.Id);
            //        }
            //    }

            //}
            
                
            return 0;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            throw new System.NotImplementedException();
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            throw new System.NotImplementedException();
        }

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Implementation of IVsSolutionLoadEvents

        public int OnBeforeOpenSolution(string pszSolutionFilename)
        {
            throw new NotImplementedException();
        }

        public int OnBeforeBackgroundSolutionLoadBegins()
        {
            throw new NotImplementedException();
        }

        public int OnQueryBackgroundLoadProjectBatch(out bool pfShouldDelayLoadToNextIdle)
        {
            throw new NotImplementedException();
        }

        public int OnBeforeLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            throw new NotImplementedException();
        }

        public int OnAfterLoadProjectBatch(bool fIsBackgroundIdleBatch)
        {
            throw new NotImplementedException();
        }

        public int OnAfterBackgroundSolutionLoadComplete()
        {
            //Guid guid = Guid.Empty;
            //Microsoft.VisualStudio.Shell.Interop.IEnumHierarchies en;
            //PackageHelper.VsSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_ALLINSOLUTION, ref guid, out en);
            //IVsHierarchy[] hierarchy = new IVsHierarchy[1];
            //uint fetched;
            //while (en.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1)
            //{
            //    if (hierarchy.Length > 0 && hierarchy[0] != null)
            //    {
            //        var proj = PackageHelper.GetDTEProject(hierarchy[0]);
            //        var allPackages = PackageHelper.GetInstalledPackages(proj);
            //        foreach (var vsPackageMetadata in allPackages)
            //        {
            //            PackageHelper.InstallPackageToProject(proj, vsPackageMetadata.Id);
            //        }
            //    }

            //}
            return VSConstants.S_OK;
        }

        #endregion
    }
}