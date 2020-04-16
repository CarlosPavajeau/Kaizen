using System.Collections.Generic;

namespace Kaizen.Core.DependencyResolver
{
    public interface IResolver
    {
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
    }
}
