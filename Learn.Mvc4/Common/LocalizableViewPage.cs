using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.Mvc4
{
    public abstract class LocalizableViewPage<TModel> : WebViewPage<TModel>
    {
        [Inject]
        public ResourceReader ResourceReader { get; set; }
    }
}
