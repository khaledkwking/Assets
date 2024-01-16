using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;

namespace Infrastructure.DAL
{
    public partial class MemberShip : IMemberShip
    {

        IRule IMemberShip.Rule
        {
            get
            {
                return this.Rule;
            }
            set
            {
                this.Rule = (Rule)value;
            }
        }
    }
}
