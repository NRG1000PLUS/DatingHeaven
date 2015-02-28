using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DatingHeaven.Core.Interfaces {
    public interface ICacheManager{

        object Get(string key);

        void Save(string key, object value);
    }
}
