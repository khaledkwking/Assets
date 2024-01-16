using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;

namespace Infrastructure.DAL.PartialClasses
{
    public class KeyListItem : IKeyListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
