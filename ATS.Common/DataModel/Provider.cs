using System;
using System.Collections.Generic;
using System.Text;

namespace ATS.Common.DataModel
{
    public class Provider
    {
        public Provider(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"[{Id}, {Name}]";
        }
    }
}
