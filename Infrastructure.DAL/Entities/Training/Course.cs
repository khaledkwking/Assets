using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainInterface;

namespace Infrastructure.DAL
{
    public partial class Course : ICourse
    {

        ICategory ICourse.Category
        {
            get
            {
                return this.Category;
            }
            set
            {
                this.Category = (Category)value;
            }
        }
    }
}
