using System;
using System.Collections.Generic;
using System.Text;

namespace InternalServer.Infrastructure.API
{
    public static class BaseAPI
    {

        public static string GetByExternalID(string url, Guid externalId)
        {
            return $"{url}/{externalId.ToString()}";
        }

        //public static string GetByExternalID(string url, Guid externalId)
        //{
        //    return $"{baseUri}/{url}/{externalId.ToString()}";
        //}
    }
}
