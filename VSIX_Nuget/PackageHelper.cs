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
            try
            {
                OutputHelper.OutputMessageToPane($"准备{packageId}安装包到项目");
                string versionString = GetVersionOfPackage(project, packageId);
                OutputHelper.OutputMessageToPane($"更新包：{packageId}到项目{project.Name},原始版本是{versionString}");
                PackageInstaller.InstallLatestPackage(source, project, packageId, false, false);
                OutputHelper.OutputMessageToPane($"安装包{packageId}到项目成功");
            }
            catch (Exception e)
            {
                OutputHelper.OutputMessageToPane($"安装包{packageId}到项目失败，原因是：{e.Message}");
            }
        }

        public static IEnumerable<IVsPackageMetadata> GetInstalledPackages(Project project)
        {
            return PackageInstallerServices.GetInstalledPackages(project);
        }

        private static string GetVersionOfPackage(Project project, string packageId)
        {
            foreach (var vsPackageMetadata in GetInstalledPackages(project))
            {
                if (vsPackageMetadata.Id.Equals(packageId))
                {
                    return vsPackageMetadata.VersionString;
                }
            }

            return "没有得到版本信息";
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

        public static void UpdateAllPackagesOfAllProjects()
        {
            Guid guid = Guid.Empty;
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            try
            {
                PackageHelper.VsSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_ALLINSOLUTION, ref guid, out var en);
                IVsHierarchy[] hierarchy = new IVsHierarchy[1];
                uint fetched;
                while (en.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1)
                {
                    if (hierarchy.Length > 0 && hierarchy[0] != null)
                    {
                        var proj = PackageHelper.GetDTEProject(hierarchy[0]);
                        try
                        {
                            var allPackages = PackageHelper.GetInstalledPackages(proj);
                            foreach (var vsPackageMetadata in allPackages)
                            {
                                PackageHelper.InstallPackageToProject(proj, vsPackageMetadata.Id);
                            }
                        }
                        catch (Exception e)
                        {//getting packages of dotnet core targeted project  is thrown exception here
                            OutputHelper.OutputMessageToPane(e.Message);
                        }

                    }

                }
            }
            catch (Exception e)
            {
                OutputHelper.OutputMessageToPane(e.Message);
            }
        }
        #endregion
    }
}