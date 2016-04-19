using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.Mvc4
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public IKernel kernel { get; private set; }
        public NinjectDependencyResolver()
        {
            this.kernel = new StandardKernel();
        }

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            this.kernel.Bind<TFrom>().To<TTo>();
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }
    }
}
