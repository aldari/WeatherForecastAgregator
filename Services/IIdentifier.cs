using System;

namespace Services
{
    public interface IIdentifier
    {
        int IdentifierFor(Type type);
    }
}