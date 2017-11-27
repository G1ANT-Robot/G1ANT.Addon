using System;
using G1ANT.Language;

namespace G1ANT.Language.Mscrm
{
    [Command(Name = "mscrm.activate", Tooltip = "This command brings Microsoft Dynamics CRM Internet Explorer instance to the foreground.")]
    public class MsCrmActivate : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Arguments style defines the style of a window – maximized, minimized or restored (restore from minimized state)")]
            public TextStructure Style { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public MsCrmActivate(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var wrapper = MsCrmManager.CurrentCRM;
            if (wrapper == null)
                throw new ApplicationException("Could not activate Dynamics CRM instance. It has to be attached first.");
            IntPtr iHandle = wrapper.Ie.NativeBrowser.hWnd;
            //TODO AddLog("Window '" + wrapper.Title + "' has been found");
              Scripter.LastWindow = (iHandle);
            throw new NotImplementedException();
            RobotWin32.BringWindowToFront(iHandle);
            if (arguments.Style.Value.ToLower() == "maximize")
                RobotWin32.ShowWindow(iHandle, RobotWin32.ShowWindowEnum.Maximize);
            else if (arguments.Style.Value.ToLower() == "minimize")
                RobotWin32.ShowWindow(iHandle, RobotWin32.ShowWindowEnum.Minimize);
            else if (arguments.Style.Value.ToLower() == "normal")
                RobotWin32.ShowWindow(iHandle, RobotWin32.ShowWindowEnum.Restore);
        }
    }
}
