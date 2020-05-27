using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESEncryptor
{
    class Program
    {    
        static void Main(string[] args)
        {
            CommandExecutor commandExecutor = new CommandExecutor();
            commandExecutor.ChooseProcess();
            Console.ReadKey();
        }
    }
}
