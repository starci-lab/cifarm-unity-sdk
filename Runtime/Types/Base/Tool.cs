using System;

namespace CiFarm.Types.Base
{
    public class Tool
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public string AvailableIn { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
