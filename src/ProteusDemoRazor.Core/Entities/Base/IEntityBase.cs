using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Core.Entities.Base
{
    //Every entity should have an id, so creating a generic helps guarantee this
    public interface IEntityBase<TId>
    {
        TId Id { get; }
    }
}
