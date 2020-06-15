using System;
using System.Windows.Forms;
using Helper.EventAggregator;
using SM.EventTypes;

namespace SM
{
    public partial class FormComfirmCopy : Form
    {
        public FormComfirmCopy()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CopyDialogEventArgs copyEventArgs = new CopyDialogEventArgs
            {
                ConfirmationDialog = ConfirmationDialog.Cancel
            };

            Helper.EventAggregator.EventAggregator.Instance.Publish(copyEventArgs);
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            CopyDialogEventArgs copyEventArgs = new CopyDialogEventArgs
            {
                CopyPermission = checkBoxCopyNTFSPermissions.Checked,
                Verify = checkBoxVerify.Checked,
                ConfirmationDialog = ConfirmationDialog.OK
            };

            Helper.EventAggregator.EventAggregator.Instance.Publish(copyEventArgs);
            this.Close();
        }
    }
}
