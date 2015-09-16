using System;

namespace Rabbit.IWasThere.Common
{
    public class GlobalConstants
    {
        public static readonly Guid GlobalCategory = new Guid("7573A0E7-B94B-4752-AFE5-A426DD9B454A");
        public const string DatabaseSystem = "DatabaseSystem";
        public const string RavenDb = "RavenDB";
        public const string DocumentDb = "DocumentDB";
        public const string SqlServer = "SQLServer";

        public const string DocumentDbAppKey = "DocumentDbAppKey";
        public const string DocumentDbUri = "DocumentDbUri";
    }
}
