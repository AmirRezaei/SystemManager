using System;

namespace SM.EventTypes
{
    public class LogMessageArgs
    {
        public LogMessageArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
    public class FileCountArgs
    {
        public FileCountArgs(long countFiles, long countSize)
        {
            CountFiles = countFiles;
            CountSize = countSize;
        }

        public long CountFiles { get; set; }
        public long CountSize { get; set; }
    }

    public enum ConfirmationDialog
    {
        OK,
        Cancel
    }

    public enum OperationStatus
    {
        Initiated,
        Started,
        Canceled,
        Inprogress,
        Ended
    }

    public class CopyDialogEventArgs : EventArgs
    {
        public bool Verify { get; set; }
        public bool CopyPermission { get; set; }
        public ConfirmationDialog ConfirmationDialog { get; set; }
    }
    public class InitiateEventArgs
    {
        public long CountItems { get; set; }
        public long CountSizeItems { get; set; }
    }
    public class CurrentItemEventArgs
    {
        public int Progress { get; set; }
        public string SourceItemName { get; set; }
        public string DestinationItemName { get; set; }
        public long ItemSize { get; set; }
    }
    public class AggregatedEventArgs
    {
        public long TotalProgress { get; set; }
        public long CountItems { get; set; }
        public long CountSize { get; set; }
    }

    public sealed class CopyProgressEventArgs : EventArgs
    {
        public InitiateEventArgs Initiate { get; set; } = new InitiateEventArgs();
        public CurrentItemEventArgs CurrentItem { get; set; } = new CurrentItemEventArgs();
        public AggregatedEventArgs Aggregated { get; set; } = new AggregatedEventArgs();
        public OperationStatus OperationStatus { get; set; } = new OperationStatus();
    }

    public class MessageArgument<T> : EventArgs
    {
        public T Message { get; private set; }
        public MessageArgument(T message)
        {
            Message = message;
        }
    }
}
