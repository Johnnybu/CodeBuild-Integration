using Newtonsoft.Json;
using System;

namespace InputModel
{
    [JsonObject]
    public class Event
    {
        public string Id { get; set; }

        public string Source { get; set; }

        public DateTime Time { get; set; }

        public Detail Detail { get; set; }
    }
}
