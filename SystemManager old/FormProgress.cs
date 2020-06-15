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
    public partial class FormProgress : Form
    {
        private Subscription<Event.CopyProgressEventArgs> copyProgressSubscription => EventAggregator.EventAggregator.Instance.Subscribe<Event.CopyProgressEventArgs>(this.CopyProgress);
        

        private Event.CopyProgressEventArgs copyProgressEventArgs = new Event.CopyProgressEventArgs();
        public FormProgress()
        {
            InitializeComponent();
            //copyProgressSubscription = EventAggregator.Instance.Subscribe<CopyProgressEventArgs>(CopyProgress);
            //fileCountSubscription = EventAggregator.Instance.Subscribe<FileCountArgs>(UpdateFileCount);
            EventAggregator.EventAggregator.Instance.Subscribe<Event.FileCountArgs>(this.UpdateFileCount); //TODO: UnSubscribe
        }

        private void UpdateFileCount(Event.FileCountArgs fileCountArgs)
        {
            Invoke(new MethodInvoker(delegate
            {
                this.labelCountItems.Text = fileCountArgs.CountFiles.ToString();
                this.labelSizeItems.Text = fileCountArgs.CountSize.ToHumanReadable();
                this.Refresh();
            }));
        }

        private void CopyProgress(Event.CopyProgressEventArgs args)
        {
            switch (args.OperationStatus)
            {
                case Event.OperationStatus.Initiated:
                    {
                        copyProgressEventArgs = args;
                        break;
                    }
                case Event.OperationStatus.Inprogress:
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
                                EventAggregator.EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
                                //this.Close();
                                //this.Invoke(new MethodInvoker(delegate
                                //{
                                //    this.Close();
                                //}));
                            }
                        }));

                        break;
                    }
                case Event.OperationStatus.Canceled:
                    {
                        EventAggregator.EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.Close();
                        }));
                        break;
                    }
                case Event.OperationStatus.Ended:
                    {
                        EventAggregator.EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
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
            EventAggregator.EventAggregator.Instance.UnSubscribe(copyProgressSubscription);
            this.Close();
        }
    }
}
