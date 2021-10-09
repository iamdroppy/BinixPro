using System;

namespace Binix.Database
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Type type, string message) : base($"Not found {type.Name}: " + message)
        {}
    }
}