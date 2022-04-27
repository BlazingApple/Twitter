using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingApple.Twitter.Models.Entities
{
    /// <summary>An image associated with a URL.</summary>
    public class UrlImage
    {
        /// <summary>Height, in pixels.</summary>
        public int Height { get; set; }

        /// <summary>Url to image.</summary>
        public string? Url { get; set; }

        /// <summary>Width, in pixels.</summary>
        public int Width { get; set; }
    }
}
