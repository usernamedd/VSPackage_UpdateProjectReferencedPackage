using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSIX_Nuget
{
    public class OutputHelper
    {
        private static Guid currentPaneGuid;
        static OutputHelper()
        {
            currentPaneGuid = Guid.NewGuid();
        }
        private static IVsOutputWindowPane currentPane;
        public static IVsOutputWindow OutputWindow { get; set; }

        public static void CreatePane()
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            OutputWindow.GetPane(ref currentPaneGuid, out currentPane);
            if (currentPane==null)
            {
                CreatePaneInternal(currentPaneGuid, "更新Nuget包的信息", true, true);
            }
        }

        static void CreatePaneInternal(Guid paneGuid, string title, bool visible, bool clearWithSolution)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            IVsOutputWindow output = OutputWindow;
            IVsOutputWindowPane pane;

            // Create a new pane.
            output.CreatePane(ref paneGuid,title,Convert.ToInt32(visible),Convert.ToInt32(clearWithSolution));

            // Retrieve the new pane.
            output.GetPane(ref paneGuid, out pane);

            pane.OutputString("This is the Created Pane \n");
            currentPane = pane;
        }

        public static void OutputMessageToPane(string message)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            if (currentPane == null)
            {
                CreatePane();
            }

            currentPane?.OutputString(message+"\n");
        }
    }
}