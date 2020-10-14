using System.Collections.Generic;

namespace ContactForms.Models
{
    public class Database
    {
        // this is a memory-leak
        // don't do this in PRODUCTION
        // DEMO PURPOSES ONLY!
        private readonly List<object> records
            = new List<object>();

        public void Save(object record)
        {
            records.Add(record);
        }

        public IReadOnlyList<object> Results => records.AsReadOnly();
    }
}