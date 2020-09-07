
using System;
using eShop.Common.Enums;

namespace eShop.InternalServer.Interfaces
{
    public interface IAudience
    {
        Guid ExternalId();
        long EntityId();

        UserType? UserType();
    }
}
