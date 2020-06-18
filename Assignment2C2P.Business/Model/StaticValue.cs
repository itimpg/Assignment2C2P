using System.Collections.Generic;

namespace Assignment2C2P.Business.Model
{
    public static class StaticValue
    {
        public static Dictionary<string, string> XmlStatusList = new Dictionary<string, string>
        {
            { "Approved", "A" },
            { "Rejected", "R" },
            { "Done", "D" },
        };

        public static Dictionary<string, string> CsvStatusList = new Dictionary<string, string>
        {
            { "Approved", "A" },
            { "Failed", "R" },
            { "Finished", "D" },
        };
    }
}
