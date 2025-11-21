using System;

namespace Likebook
{
    public sealed class SiteOption
    {
        public SiteOption(string name, string url, string userAgent, string glyph, string description, string colorHex)
        {
            Name = name;
            Url = url;
            UserAgent = userAgent;
            Glyph = glyph;
            Description = description;
            ColorHex = colorHex;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string UserAgent { get; set; }
        public string Glyph { get; set; }
        public string Description { get; set; }
        public string ColorHex { get; set; }
    }
}
