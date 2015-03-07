using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseTests {
    class RandomDataGenerator{

        private const int MINIMAL_INT = 20000;
        private const int MAXIMAL_INT = 90000;

        public string GenerateRandomString(int size){
            var r = new Random();
            var data = new StringBuilder();

            for (var idx = 0; idx < size; idx++){
                data.Append((char) r.Next(2000,3000));
            }

            return data.ToString();
        }

        public int RandomInt(){
            var r = new Random();
            return r.Next(MINIMAL_INT, MAXIMAL_INT);
        }
    }
}
