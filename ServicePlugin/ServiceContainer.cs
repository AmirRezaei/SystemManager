using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using NLog;

namespace ServicePlugin
{
    public class ServiceContainer : Entity
    {
        public ServiceContainer(string path) : base(path)
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
                    IEnumerable<ServiceController> services = ServiceController.GetServices();
                    foreach (ServiceController service in services)
                    {
                        try
                        {
                            Entity itemContainer = new ServiceContainer(service.ServiceName);
                            itemContainer.Name = service.DisplayName;
                            itemContainer.IsDirectory = service.DependentServices.Length > 0;

                            itemContainer.Attributes.Add(nameof(service.CanPauseAndContinue),
                                service.CanPauseAndContinue.ToString());
                            itemContainer.Attributes.Add(nameof(service.CanShutdown), service.CanShutdown.ToString());
                            itemContainer.Attributes.Add(nameof(service.CanStop), service.CanStop.ToString());
                            entities.Add(itemContainer);
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
                                Entity itemContainer = new ServiceContainer(service.ServiceName);
                                itemContainer.Name = service.DisplayName;
                                itemContainer.IsDirectory = service.DependentServices.Length > 0;
                                itemContainer.Attributes.Add(nameof(service.CanPauseAndContinue),
                                    service.CanPauseAndContinue.ToString());
                                itemContainer.Attributes.Add(nameof(service.CanShutdown), service.CanShutdown.ToString());
                                itemContainer.Attributes.Add(nameof(service.CanStop), service.CanStop.ToString());
                                entities.Add(itemContainer);
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
            Parent = path;
            Entities = GetEntities();
            return Entities;
        }
    }
}
