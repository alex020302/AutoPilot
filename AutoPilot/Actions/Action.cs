using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilot
{
    public class Action
    {
        public string Comment { get; set; }
        public string ActionType;

        public virtual void Execute()
        {

        }

        //Wird automatisch von der Listbox verwendet
        public override string ToString()
        {
            return "";
        }
    }
}
