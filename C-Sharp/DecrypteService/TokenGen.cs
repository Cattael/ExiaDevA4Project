using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecrypteService
{
    public class TokenGen
    {
        private static TokenGen instance;

        public static TokenGen GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new TokenGen();
                return instance;
            }
        }

        private TokenGen() { }

        public string GenerateToken(int length)
        {
            byte[] buffer = new byte[length];
            Random random = new Random();
            for(int i = 0; i < length; i++)
            {
                buffer[i] = (byte)random.Next(65, 90);
            }
            return Encoding.ASCII.GetString(buffer);
        }

    }
}
