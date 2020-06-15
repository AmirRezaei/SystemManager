using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemManager.EventAggregator;

namespace SystemManager
{
    public partial class FormComfirmCopy : Form
    {
        public FormComfirmCopy()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Event.CopyDialogEventArgs copyEventArgs = new Event.CopyDialogEventArgs
            {
                ConfirmationDialog = Event.ConfirmationDialog.Cancel
            };

            EventAggregator.EventAggregator.Instance.Publish(copyEventArgs);
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Event.CopyDialogEventArgs copyEventArgs = new Event.CopyDialogEventArgs
            {
                CopyPermission = checkBoxCopyNTFSPermissions.Checked,
                Verify = checkBoxVerify.Checked,
                ConfirmationDialog = Event.ConfirmationDialog.OK
            };

            EventAggregator.EventAggregator.Instance.Publish(copyEventArgs);
            this.Close();
        }
    }
}
