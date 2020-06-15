using System;
using System.Windows.Forms;
using Helper;
using Helper.EventAggregator;
using SM.EventTypes;

namespace SM
{
    public partial class FormProgress : Form
    {
        private Subscription<CopyProgressEventArgs> copyProgressSubscription => EventAggregator.Instance.Subscribe<CopyProgressEventArgs>(this.CopyProgress);


        private CopyProgressEventArgs copyProgressEventArgs = new CopyProgressEventArgs();
        public FormProgress()
        {
            InitializeComponent();
            //copyProgressSubscription = EventAggregator.Instance.Subscribe<CopyProgressEventArgs>(CopyProgress);
            //fileCountSubscription = EventAggregator.Instance.Subscribe<FileCountArgs>(UpdateFileCount);
            EventAggregator.Instance.Subscribe<FileCountArgs>(this.UpdateFileCount); //TODO: UnSubscribe
        }

        private void UpdateFileCount(FileCountArgs fileCountArgs)
        {
            Invoke(new MethodInvoker(delegate
            {
                this.labelCountItems.Text = fileCountArgs.CountFiles.ToString();
                this.labelSizeItems.Text = fileCountArgs.CountSize.ToHumanReadable();
                this.Refresh();
            }));
        }

        private void CopyProgress(CopyProgressEventArgs args)
        {
            switch (args.OperationStatus)
            {
                case OperationStatus.Initiated:
                    {
                        copyProgressEventArgs = args;
                        break;
                    }
                case OperationStatus.Inprogress:
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            copyProgressEventArgs.Aggregated.CountItems++;
                            copyProgressEventArgs.Aggregated.CountSize += args.Aggregated.CountSize;
                            this.progressBarCurrentOperation.Value = args.CurrentItem.Progress;
                            this.labelCountItems.Text = copyProgressEventArgs.Initiate.CountItems + "/" + copyProgressEventArgs.Aggregated.CountItems;
                            this.labelSizeItems.Text = args.Initiate.CountSizeItems.ToHumanReadable() + "/" + copyProgressEventArgs.Aggregated.CountSize.ToHumanReadable();
                            this.labelSource.Text = args.CurrentItem.SourceItemName.ShrinkPath(80);
                            this.labelDestination.Text = args.CurrentItem.DestinationItemName.ShrinkPath(80);
                            //this.Refresh();
                            if (copyProgressEventArgs.Aggregated.CountItems == copyProgressEventArgs.Initiate.CountItems)
                            {
                                EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
                                //this.Close();
                                //this.Invoke(new MethodInvoker(delegate
                                //{
                                //    this.Close();
                                //}));
                            }
                        }));

                        break;
                    }
                case OperationStatus.Canceled:
                    {
                        EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Close();
                        }));
                        break;
                    }
                case OperationStatus.Ended:
                    {
                        EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Close();
                        }));
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
            this.Close();
        }
    }
}
