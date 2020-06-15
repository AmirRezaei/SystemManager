using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using NLog;
using PluginInterface;

namespace ProcessPlugin
{
    public class ProcessContainer : Entity
    {
        public ProcessContainer(string path) : base(path)
        {
            Path = path;
        }

        public IEnumerable<Entity> Entities { get; set; }

        public override IEnumerable<Entity> GetEntities()
        {
            var entities = new List<Entity>();

            try
            {
                if (String.IsNullOrEmpty(Path))
                {
                    Process[] processes = Process.GetProcesses();
                    foreach (Process process in processes)
                    {
                        try
                        {
                            Entity entity = new ProcessContainer(process.Id.ToString());
                            entity.IsDirectory = process.Threads.Count > 0;
                            entity.Name = process.ProcessName;
                            entity.Attributes.Add(nameof(process.Id), process.Id.ToString());
                            entity.Attributes.Add(nameof(process.TotalProcessorTime), process.TotalProcessorTime.ToString());
                            entities.Add(entity);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e.Message);
                        }
                    }
                }
                else
                {
                    Process process = Process.GetProcesses().FirstOrDefault(x => x.Id.ToString() == Path);
                    if (process != null)
                    {
                        foreach (ProcessThread processThread in process.Threads)
                        {
                            try
                            {
                                Entity entity = new ProcessContainer(@"");
                                entity.IsDirectory = false;
                                entity.Name = process.ProcessName+ " Thread " + processThread.Id.ToString();
                                entity.Attributes.Add(nameof(processThread.Id), processThread.Id.ToString());
                                entity.Attributes.Add(nameof(processThread.TotalProcessorTime), process.TotalProcessorTime.ToString());
                                entities.Add(entity);
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return entities;
        }

        public override Entity Parent => new ProcessContainer("");
    }
}
