using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Classes
{
    public class Request
    {
        public string model { get; set; }
        public Message[] messages { get; set;} 
    }
}
