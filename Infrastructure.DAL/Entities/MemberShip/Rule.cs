using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;

namespace Infrastructure.DAL
{
    public partial class Rule : IRule
    {

        private IEntityCollection<IMemberShip> _MemberShips12;

        IEntityCollection<IMemberShip> IRule.MemberShips
        {
            get
            {
                if (_MemberShips12 != null) { return _MemberShips12; }

                var x = new EntityCollection<IMemberShip, MemberShip>();
                x.setCollection(this.MemberShips);
                _MemberShips12 = (IEntityCollection<IMemberShip>)x;
                return _MemberShips12;
            }
            set
            {
                this.MemberShips = (TrackableCollection<MemberShip>)value;
            }
        }
    }
}
