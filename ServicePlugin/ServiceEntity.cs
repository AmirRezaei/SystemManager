using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using NLog;

namespace ServicePlugin
{
    public class ServiceEntity : Entity
    {
        public ServiceEntity(string path) : base(path)
        {
            Path = path;
        }

        private enum TagNames
        {
            Name,
            CanPauseAndContinue,
            CanShutdown,
            CanStop
        }

        public override IEnumerable<Entity> GetEntities()
        {
            var entities = new List<Entity>();
            if (!IsRoot)
                entities.Add(new ServiceEntity(Parent) { Name = "[..]", IsDirectory = true, IsParent = true });

            try
            {
                if (String.IsNullOrEmpty(Path))
                {
                    IEnumerable<ServiceController> services = ServiceController.GetServices();
                    foreach (ServiceController service in services)
                    {
                        try
                        {
                            Entity serviceEntity = new ServiceEntity(service.ServiceName);
                            serviceEntity.Name = service.DisplayName;
                            serviceEntity.IsDirectory = service.DependentServices.Length > 0;

                            serviceEntity.Values.Add(nameof(TagNames.CanPauseAndContinue), service.CanPauseAndContinue.ToString());
                            serviceEntity.Values.Add(nameof(TagNames.CanShutdown), service.CanShutdown.ToString());
                            serviceEntity.Values.Add(nameof(TagNames.CanStop), service.CanStop.ToString());
                            entities.Add(serviceEntity);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e.Message);
                        }
                    }
                }
                else
                {
                    var theService = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == Path);
                    if (theService != null)
                    {
                        foreach (ServiceController service in theService.DependentServices)
                        {
                            try
                            {
                                Entity serviceEntity = new ServiceEntity(service.ServiceName);
                                serviceEntity.Name = service.DisplayName;
                                serviceEntity.IsDirectory = service.DependentServices.Length > 0;
                                serviceEntity.Values.Add(nameof(TagNames.CanPauseAndContinue), service.CanPauseAndContinue.ToString());
                                serviceEntity.Values.Add(nameof(TagNames.CanShutdown), service.CanShutdown.ToString());
                                serviceEntity.Values.Add(nameof(TagNames.CanStop), service.CanStop.ToString());
                                entities.Add(serviceEntity);
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

        public override IEnumerable<Entity> Seek(string path)
        {
            Path = path;
            Parent = "";

            Tags.Add(new Tag(nameof(TagNames.Name), typeof(string), 70));
            Tags.Add(new Tag(nameof(TagNames.CanPauseAndContinue), typeof(string), 10));
            Tags.Add(new Tag(nameof(TagNames.CanShutdown), typeof(string), 10));
            Tags.Add(new Tag(nameof(TagNames.CanStop), typeof(string), 10));

            Entities = GetEntities();
            return Entities;
        }
    }
}
