using DecrypteService.JavaDecryptService;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace DecrypteService
{
    public class Decrypter
    {
        private string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public int currentKey = 0;
        Request request;
        private int nbrOfThread;
        string[] keys; 
        int count = -1;
        
        int pourcent; 

        public Decrypter(Request request)
        {
            keys = new string[456976];
            keys[0] = "AAAA";
            for(int i = 1; i < keys.Length; i++)
            {
                keys[i] = generateKey(keys[i - 1]);
            }
            pourcent = 0; 
            nbrOfThread = 4;
            this.request = request;
        }

        public bool startDecrypt()
        {
            Parallel.For(0, 456976, (i) => ThreadStart(i));
            return true;
        }

        private void ThreadStart(int i)
        {
            if ((i % 4_560) == 0)
            {
                count++;
                Console.WriteLine(count + " %");
                if (Monitor.TryEnter(BDDConnection.GetInstance))
                {
                    BDDConnection.GetInstance.UpdateProgress(request.token, count);
                    Monitor.Exit(BDDConnection.GetInstance);
                }
            }
            if ((i % 45_600) == 0)
            {
            }
            int fileLen = 0;
            
            int KeyLen = keys[i].Length;
            string key = "AWHI";//keys[i];
            fileLen = request.body.Length;
            byte[] outputString = new byte[fileLen];

            for (int j = 0; j < fileLen; ++j)
            {
                int ascii = (request.body[j] ^ key[j % KeyLen]);
                outputString[j] = ((byte)ascii);
            }
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            writer.WriteStartElement("file");
            writer.WriteAttributeString("token", request.token);
            writer.WriteAttributeString("name", request.name);
            //writer.WriteElementString("content", Convert.ToBase64String(Encoding.ASCII.GetBytes("Je suis un mot francais je me balade dans la rue")));
            //writer.WriteElementString("content", outputString.Replace("&", "&amp").Replace("\"", "&quot").Replace("<", "&lt").Replace(">", "&gt").Replace("\'", "&apos"));
            //writer.WriteElementString("content", "Je suis un message");
            //string s = Encoding.ASCII.GetString(outputString);
            writer.WriteElementString("content", Convert.ToBase64String(outputString));
            writer.WriteElementString("key", key);
            writer.WriteEndElement();

            JavaConnector.SendToJava(sw.ToString());
            sw.Dispose();
            writer.Dispose();
        }

        private string generateKey(string input)
        {
            if (input == "ZZZZ")
            {
                return "AAAA";
            }
            StringBuilder output = new StringBuilder(input);

            if (output[0] == 'Z')
            {
                output[0] = 'A';
                for (int i = 1; i < input.Length; i++)
                {
                    if (input[i] == 'Z')
                    {
                        output[i] = 'A';
                        pourcent++;
                        
                        continue;
                    }
                    else
                    {
                        char c = input[i];
                        int indexAlphabet = Alphabet.IndexOf(c) + 1;
                        c = Alphabet[indexAlphabet];
                        output[i] = c;
                        break;
                    }
                }
            }
            else
            {
                char c = input[0];
                int indexAlphabet = Alphabet.IndexOf(c) + 1;
                c = Alphabet[indexAlphabet];
                output[0] = c;
            }
            return output.ToString();
        }
    }
}
