using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingHeaven.Core {
    public static class TypesHelper {

        public static bool TypeDerivesFrom<TFirst, TSecond>(){
            //
            //
            //
            return typeof (TSecond).IsAssignableFrom(typeof(TFirst));
        }
    }
}
