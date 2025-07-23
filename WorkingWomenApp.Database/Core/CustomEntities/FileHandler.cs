using System;
using System.Collections.Generic;
using System.Text;

namespace WorkingWomenApp.Database.Core.CustomEntities
{
    public class FileHandler
    {
        public byte[] File { get; set; }
        public string ContentType { get; set; }
        public string Extention { get; set; }
        public string Container { get; set; }
        public string Name { get; set; }
    }
}
