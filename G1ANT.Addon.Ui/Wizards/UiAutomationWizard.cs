
using G1ANT.Language;
using G1ANT.Language.Wizards;

namespace G1ANT.Addon.Ui
{
    [Wizard(Name = "Ui Automation")]
    class UiAutomationWizard : IWizard
    {
        private IWizardManager wizardManager;
        public void Execute()
        {
            UiWizard uiWizardForm = UiWizard.Instance;
            if (uiWizardForm.Visible)
                uiWizardForm.Focus();
            uiWizardForm.Show();
        }

        public void Initialize(IWizardManager wizardManagerInput)
        {
            wizardManager = wizardManagerInput;
        }
    }
}
