using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Helper.EventAggregator;
using SM.EventTypes;

namespace SM
{
    public class HelperIO
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static async Task CopyFileAsync(string sourceFile, string destinationFile, CancellationToken cancellationToken)
        {
            var fileOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
            var bufferSize = 4096;

            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, fileOptions))
            {
                {
                    using (FileStream destinationStream = new FileStream(destinationFile, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize, fileOptions))
                        await sourceStream.CopyToAsync(destinationStream, bufferSize, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                }
            }
        }
        public async void CopyFilesAsync(string sourceDirectory, string destinationDirectory)
        {
            foreach (string filename in Directory.EnumerateFiles(sourceDirectory))
            {
                using (FileStream sourceStream = File.Open(filename, FileMode.Open))
                {
                    FileInfo sourceFileInfo = new FileInfo(filename);

                    using (FileStream destinationStream = File.Create(Path.Combine(destinationDirectory, sourceFileInfo.Name)))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }
                }
            }
        }

        public static async void DirectoryCopy(string[] directoriesToCopy, string destDirNamexxx, bool copySubDirs, bool overwrite)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                foreach (string directoryToCopy in directoriesToCopy)
                {
                    DirectoryInfo directoryToCopyInfo = new DirectoryInfo(directoryToCopy);
                    if (!directoryToCopyInfo.Exists)
                    {
                        throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + directoriesToCopy);
                    }

                    string destDirName = Path.Combine(destDirNamexxx, directoryToCopyInfo.Name); // destination path combined with source directory name.

                    // If the destination directory doesn't exist, create it.
                    if (!Directory.Exists(destDirName))
                    {
                        Directory.CreateDirectory(destDirName);
                    }

                    // Get the files in the directory and copy them to the new location.
                    FileInfo[] files = directoryToCopyInfo.GetFiles();
                    foreach (FileInfo fileInfo in files)
                    {
                        string tempPath = Path.Combine(destDirName, fileInfo.Name);
                        //await Task.Run(() => fileInfo.CopyTo(tempPath, overwrite));

                        CopyProgressEventArgs copyProgressEventArgs = new CopyProgressEventArgs();
                        copyProgressEventArgs.CurrentItem.Progress = 0;
                        copyProgressEventArgs.CurrentItem.ItemSize = fileInfo.Length;
                        copyProgressEventArgs.CurrentItem.SourceItemName = tempPath;
                        copyProgressEventArgs.CurrentItem.DestinationItemName = destDirName;
                        copyProgressEventArgs.OperationStatus = OperationStatus.Inprogress;
                        Helper.EventAggregator.EventAggregator.Instance.Publish(copyProgressEventArgs);
                        fileInfo.CopyTo(tempPath, overwrite);

                        Logger.Info("Copy: " + tempPath);
                    }

                    // If copying subdirectories, copy them and their contents to new location.
                    if (copySubDirs)
                    {
                        DirectoryInfo[] subdirDirectories = directoryToCopyInfo.GetDirectories();
                        string temppath = Path.Combine(destDirName, directoryToCopy);
                        DirectoryCopy(subdirDirectories.Select(x => x.FullName).ToArray(), destDirName, copySubDirs, overwrite);
                    }
                }
            }
            catch (Exception ex)
            {
                //EventAggregator.EventAggregator.Instance.Publish<LogMessageArgs>(new LogMessageArgs(ex.Message));
                Logger.Error(ex.Message);
            }
            //EventAggregator.Instance.Publish(new CopyProgressEventArgs { OperationStatus = OperationStatus.Ended });
        }
    }
}
