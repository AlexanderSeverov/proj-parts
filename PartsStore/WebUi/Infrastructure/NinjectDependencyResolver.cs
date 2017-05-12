using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Web.Mvc;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concreate;
using System.Configuration;

namespace WebUi.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();

        }

        private void AddBindings()
        {
        
            kernel.Bind<IPartsRepository>().To<EFPartRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcess>()
                .WithConstructorArgument("settings", emailSettings);

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
 
    }
}