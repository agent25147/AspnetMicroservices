using System.Collections.Generic;

namespace Catalog.API.Models
{
    public class WebChatAvayaConfigs
    {
        public string Url { get; set; }
        public Dictionary<string, string> Mappings { get; set; } = new Dictionary<string, string>();
    }

}

