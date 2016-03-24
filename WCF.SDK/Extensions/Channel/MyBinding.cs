using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace WCF.SDK.Extensions.Channel
{
    public class MyBinding:Binding
    {
        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection elemens = new BindingElementCollection();
            elemens.Add(new TextMessageEncodingBindingElement());
            elemens.Add(new MyBindingElement());
            elemens.Add(new HttpTransportBindingElement());
            return elemens.Clone();
        }

        public override string Scheme
        {
            get
            {
                return "http";
            }
        }
    }
}
