using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseTests {
    class RandomDataGenerator{

        private const int MINIMAL_INT = 20000;
        private const int MAXIMAL_INT = 90000;
        private Random r = new Random();

        public string GenerateRandomString(int size){
            var data = new StringBuilder();

            for (var idx = 0; idx < size; idx++){
                data.Append((char) r.Next(97,122));
            }

            return data.ToString();
        }

        public int RandomInt(){
            return r.Next(MINIMAL_INT, MAXIMAL_INT);
        }
    }
}
