using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using PluginInterface;

namespace ProcessPlugin
{
    public class ProcessEntity : Entity
    {
        private enum TagNames
        {
            Name,
            Id,
            WorkingSet64,
            PagedSystemMemorySize64,
            PagedMemorySize64,
            TotalProcessorTime
        }
        public ProcessEntity(string path) : base(path)
        {
            Path = path;
        }
        public override IEnumerable<Entity> Seek(string path)
        {
            Path = path;
            Parent = "";

            Tags.Add(new Tag(nameof(TagNames.Name), typeof(string), 50));
            Tags.Add(new Tag(nameof(TagNames.Id), typeof(string), 10));
            Tags.Add(new Tag(nameof(TagNames.WorkingSet64), typeof(long), 10));
            Tags.Add(new Tag(nameof(TagNames.PagedSystemMemorySize64), typeof(long), 10));
            Tags.Add(new Tag(nameof(TagNames.PagedMemorySize64), typeof(long), 10));
            Tags.Add(new Tag(nameof(TagNames.TotalProcessorTime), typeof(DateTime), 10));

            Entities = GetEntities();
            return Entities;
        }

        public override IEnumerable<Entity> GetEntities()
        {
            var entities = new List<Entity>();
            if (!IsRoot)
                entities.Add(new ProcessEntity(Parent) { Name = "[..]", IsDirectory = true, IsParent = true });

            try
            {
                if (String.IsNullOrEmpty(Path))
                {
                    Process[] processes = Process.GetProcesses();
                    Parallel.ForEach(Process.GetProcesses(), process =>
                    {
                        try
                        {
                            Entity entity = new ProcessEntity(process.Id.ToString());
                            entity.IsDirectory = process.Threads.Count > 0;
                            entity.Name = process.ProcessName;

                            entity.Values.Add(nameof(TagNames.Id), process.Id.ToString());
                            entity.Values.Add(nameof(TagNames.WorkingSet64), process.WorkingSet64.ToString());
                            entity.Values.Add(nameof(TagNames.PagedSystemMemorySize64), process.PagedSystemMemorySize64.ToString());
                            entity.Values.Add(nameof(TagNames.PagedMemorySize64), process.PagedMemorySize64.ToString());
                            entity.Values.Add(nameof(TagNames.TotalProcessorTime), process.TotalProcessorTime.ToString());
                            entities.Add(entity);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e.Message);
                        }
                    });
                }
                else
                {
                    Process process = Process.GetProcesses().FirstOrDefault(x => x.Id.ToString() == Path);
                    if (process != null)
                    {
                        //foreach (ProcessThread processThread in process.Threads)
                        Parallel.ForEach(process.Threads.Cast<ProcessThread>(), processThread =>
                        {
                            try
                            {
                                Entity entity = new ProcessEntity(processThread.Id.ToString());
                                entity.Name = process.ProcessName + " Thread " + processThread.Id;
                                entity.IsDirectory = false;
                                entity.IsRoot = false;

                                entity.Values.Add(nameof(TagNames.Id), processThread.Id.ToString());
                                entity.Values.Add(nameof(TagNames.WorkingSet64), "");
                                entity.Values.Add(nameof(TagNames.PagedSystemMemorySize64), "");
                                entity.Values.Add(nameof(TagNames.PagedMemorySize64), "");
                                entity.Values.Add(nameof(TagNames.TotalProcessorTime), processThread.TotalProcessorTime.ToString());
                                entities.Add(entity);
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e.Message);
                            }
                        });
                    }
                }

                //TODO: fix null objects in Parallel.ForEach result
                entities = (List<Entity>)entities.Where(x => x != null);
                //var a = entities.Count(x => x == null);
                //if (a> 0)
                //    Debugger.Break();

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return entities;
        }
    }
}
