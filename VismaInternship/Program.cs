using CommandDotNet;
using System;

namespace VismaInternship
{
    class Program
    {
        static int Main(string[] args)
        {
            return new AppRunner<BookLibrary>().Run(args);
        }
    }
}
