using EzGame.Domain.Core.Interfaces;
using System;

namespace EzGame.Domain.Core.Services
{
    public class IdGenerator
    {
        public static string NewGuid(int letterCount= 0)
        {
            var id = letterCount == 0 ? Guid.NewGuid().ToString().Replace("-", "") : Guid.NewGuid().ToString().Replace("-", "").Substring(0, letterCount);
            return id;
        }
    }
}