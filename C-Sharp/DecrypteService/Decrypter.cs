using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DecrypteService
{
    public class Decrypter
    {
        private string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string currentKey;
        Request request;
        private int nbrOfThread;
        private bool isDone;
        Thread[] threads;
        List<string[]> listResult; 

        public Decrypter(Request request)
        {
            nbrOfThread = 4;
            this.request = request;
            threads = new Thread[nbrOfThread];
            listResult = new List<string[]>();
            isDone = false;
            currentKey = "AAAA";
        }
       
        public List<string[]> startDecrypt()
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(ThreadStart));
                threads[i].Start();
            }
            while (!isDone) Thread.Sleep(100);
            return listResult; 
        }

        private void ThreadStart()
        {
            while(!isDone)
            {
                string outputString = "";
                int fileLen = 0;
                int KeyLen = currentKey.Length;
                fileLen = request.body.Length;
                char[] output = new char[fileLen];
                for (int i = 0; i < fileLen; ++i)
                {
                    outputString += output[i] = (char)(request.body[i] ^ currentKey[i % KeyLen]);
                }
                currentKey =  add(currentKey); 

                while (!Monitor.TryEnter(listResult, 100));

                listResult.Add(new string[] { currentKey, outputString });
                
                Console.WriteLine(output);
            }
        }

        private string add(string input)
        {
            if (input == "ZZZZ")
            {
                isDone = true;
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
            Console.WriteLine(output.ToString());
            return output.ToString();
        }
    }
}
