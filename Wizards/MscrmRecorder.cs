
using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G1ANT.Addon.Mscrm
{
    [Wizard(Name = "Dynamics CRM Recorder")]
    public class MscrmRecorder //: IWizard
    {
      //  private IWizardManager wizardManager;
        private bool initialWindowPosition = true;
        private bool initialInstance = true;
        public void Execute()
        {
            MscrmRecorderForm recorderWindow = MscrmRecorderForm.Instance;
            if (initialInstance)
            {
              //  recorderWindow.InsertTextToRobotHandler += wizardManager.InsertTextToRobot;
                initialInstance = false;
            }
            if (!recorderWindow.Visible)
            {
                recorderWindow.Show();
                if (initialWindowPosition)
                {
               //     recorderWindow.LocateInMiddleOfForm(wizardManager.Parent);
                    initialWindowPosition = false;
                }
            }            
        }
        //TODO
        //public void Initialize(IWizardManager wizardManager)
        //{
        //    this.wizardManager = wizardManager;
        //}
    }
}
