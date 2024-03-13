using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmeProject.Domain.Entities
{
    public class AboutApp:BaseEntity
    {
        public string AppName { get; set; }
        public string Site { get; set; }
        public string AppVersion { get; set; }
        public string Content { get; set; }
    }
}
