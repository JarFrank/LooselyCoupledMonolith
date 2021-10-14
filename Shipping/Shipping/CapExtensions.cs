using DotNetCore.CAP;
using System;

namespace Shipping
{
    public static class CapExtensions
    {
        public static long GetMessageId(this CapHeader header)
        {
            return Convert.ToInt64(header["cap-msg-id"]);
        }
    }
}
