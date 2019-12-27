using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NuGet.VisualStudio;
using Task = System.Threading.Tasks.Task;

namespace VSIX_Nuget
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(VSIX_NugetPackage.PackageGuidString)]
    //[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    
    public sealed class VSIX_NugetPackage : AsyncPackage
    //public sealed class VSIX_NugetPackage : Package
    {
        //public const string UiContextGuidString = VSConstants.UICONTEXT_SolutionExists.ToString();
        /// <summary>
        /// VSIX_NugetPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "ea358f86-d491-427f-9cea-67f36a68ca9e";

        #region Package Members

        #region Overrides of Package

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            Console.WriteLine("111111111111111111111111111111111111111111111111111111111111");

            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            PackageHelper.JoinableTaskFactory = this.JoinableTaskFactory;
            IVsSolution pSolution = GetService(typeof(SVsSolution)) as IVsSolution;
            object objLoadMgr = new MySolutionManager();   //the class that implements IVsSolutionManager
            //看看是否已经有solution load manager了
            object loadManager;
            int result = pSolution.GetProperty((int)__VSPROPID4.VSPROPID_ActiveSolutionLoadManager, out loadManager);
            pSolution.SetProperty((int)__VSPROPID4.VSPROPID_ActiveSolutionLoadManager, objLoadMgr);
            uint pdwCookie;
            pSolution.AdviseSolutionEvents(new MySolutionEvents(), out pdwCookie);
            PackageHelper.VsSolution = pSolution;

            InitPackageHelper();
            InitOutputHelper();
            
        }

        private void InitOutputHelper()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IVsOutputWindow output = (IVsOutputWindow)GetService(typeof(SVsOutputWindow));
            OutputHelper.OutputWindow = output;
            OutputHelper.OutputMessageToPane("创建了一个Pane，这个Pane用于放入关于包更新的信息的，也是本扩展的信息\n");
        }

        #endregion

        #endregion

        private void InitPackageHelper()
        {
            var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
            IVsPackageInstallerServices installerServices = componentModel.GetService<IVsPackageInstallerServices>();
            PackageHelper.PackageInstallerServices = installerServices;
            PackageHelper.PackageInstaller = componentModel.GetService<IVsPackageInstaller2>();
            var nugetEvents = componentModel.GetService<IVsPackageInstallerEvents>();
            nugetEvents.PackageInstalled += NugetEvents_PackageInstalled;
        }

        private void NugetEvents_PackageInstalled(IVsPackageMetadata metadata)
        {
            //有可能出现这种情况：解决方案已经加载完成了，还没加载此包，
            //导致在解决方案加载完整事件时，不能更新所有项目的引用包
            //所以在这里更新包一次
            PackageHelper.UpdateAllPackagesOfAllProjects();
        }
    }
}
