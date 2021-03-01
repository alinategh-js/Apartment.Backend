using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
