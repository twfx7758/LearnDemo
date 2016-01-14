using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.DesignPattern.Abstract_Factory
{
    public abstract class Event
    {
        protected int numLanes;
        protected ArrayList swimmers;

        public Event(string filename, int lanes)
        {
            numLanes = lanes;
            swimmers = new ArrayList();
            
        }
    }
}
