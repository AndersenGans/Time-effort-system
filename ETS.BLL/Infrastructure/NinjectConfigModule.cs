using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using ETS.BLL.Infrastructure.Abstract;
using ETS.BLL.Infrastructure.Concrete;

namespace ETS.BLL.Infrastructure
{
    public class NinjectConfigModule : NinjectModule
    { 
        public override void Load()
        {
            Kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}
