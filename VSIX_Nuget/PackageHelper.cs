using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using NuGet.VisualStudio;

namespace VSIX_Nuget
{
    public class PackageHelper
    {
        public static JoinableTaskFactory JoinableTaskFactory;
        public static IVsSolution VsSolution { get; set; }
        public static IVsPackageInstallerServices PackageInstallerServices { get; set; }

        private static string source = "http://192.168.0.40/nuget";

        //Using the Import attribute
        [Import(typeof(IVsPackageInstaller2))]
        public static IVsPackageInstaller2 PackageInstaller;
        //packageInstaller.InstallLatestPackage(null, currentProject,"Newtonsoft.Json", false, false);

        public static void InstallPackageToProject(Project project,string packageId)
        {
            //PackageInstallerServices.in
            PackageInstaller.InstallLatestPackage(source, project, packageId, false, false);
        }

        public static IEnumerable<IVsPackageMetadata> GetInstalledPackages(Project project)
        {
            return PackageInstallerServices.GetInstalledPackages(project);
        }

        #region 项目与解决方案
        public static EnvDTE.Project GetDTEProject(IVsHierarchy hierarchy)
        {
            if (hierarchy == null)
                throw new ArgumentNullException("hierarchy");
            object obj;
            hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out obj);
            return obj as EnvDTE.Project;
        }
        #endregion
    }
}