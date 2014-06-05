using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheContainer
{
    public interface IContainerKey<K> where K : IEquatable<K>
    {
        K GetKey();
    }
}
