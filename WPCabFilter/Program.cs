using System;
using System.IO;


namespace WPCabFilter
{
    class Program
    {
        static string InstalledPackages;
        static string FilterPath;
        static string OutputPath;
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
            }
            else
            {
                if (!File.Exists(".\\InstalledPackages.csv"))
                {
                    Usage();
                }
                else
                {
                    if (args[0] != null)
                    {
                        Console.WriteLine();
                        
                        Console.WriteLine($"INPUT: {args[0]}");
                        if (args[1] != null)
                        {
                            if (!Directory.Exists(args[1]))
                            {
                                Directory.CreateDirectory(args[1]);
                            }
                            Console.WriteLine($"OUTPUT: {args[1]}");
                            string pkglist = File.ReadAllText(".\\InstalledPackages.csv").ToLower();
                            var cablist = Directory.EnumerateFiles(args[0]);
                            Console.WriteLine("Loaded \"InstalledPackages.csv\"");
                            Console.WriteLine("Starting Filtering, Please Wait this may take a while");
                            Console.WriteLine();
                            Console.WriteLine();
                            foreach (var item in cablist)
                            {
                                
                                string altfiletype = item.Replace(".cab", "");
                                string filename1 = System.IO.Path.GetFileNameWithoutExtension(altfiletype).ToLower();
                                if (pkglist.Contains(filename1))
                                {
                                    Console.Write($"[+] {Path.GetFileName(item)} ");
                                    File.Copy(item, $"{args[1]}\\{Path.GetFileName(item)}", true);
                                    Console.Write($"> {args[1]}\\{Path.GetFileName(item)}\n");
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Usage()
        {
            Console.WriteLine("WPCabFilter:\n" +
                "Filter W10M Update Cabs using \"InstalledPackages.csv\"\n" +
                "\nUsage:\n" +
                "Retrieve \"InstalledPackages.csv\" from device and place next to \"WPCabFilter.exe\"\n" +
                "   WPCabFilter.exe \"Path\\To\\Cab\\Files\" \"Path\\To\\Output\\Folder\" \n" +
                "\n");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
