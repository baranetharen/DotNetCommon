using System;

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