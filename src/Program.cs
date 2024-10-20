using System.Reflection;

namespace TRXInjectionTool;

internal class Program
{
    static readonly string _topNS = "TRXInjectionTool.Types";

    static void Main()
    {
        Type[] types = Assembly.GetExecutingAssembly().GetTypes();
        List<string> namespaces = types
            .Where(t => t.Namespace != null && t.Namespace.StartsWith(_topNS) && t.Namespace.Length > _topNS.Length)
            .Select(t => t.Namespace[(_topNS.Length + 1)..])
            .Distinct()
            .OrderBy(t => t)
            .ToList();

        while (true)
        {
            Console.WriteLine("Select a namespace to run. All builders that match will be run.");
            Console.WriteLine("Type ALL to run everything.");
            Console.WriteLine();

            for (int i = 0; i < namespaces.Count; i++)
            {
                Console.WriteLine($"[{i}] - {namespaces[i]}");
            }
            Console.WriteLine();

            int result = -1;
            do
            {
                Console.Write("Selected namespace: ");
                string option = Console.ReadLine();
                if (option.ToLower() == "all")
                {
                    result = -2;
                    break;
                }
                else
                {
                    if (!int.TryParse(option, out result))
                    {
                        result = -1;
                    }
                }
            }
            while (result < 0 || result >= namespaces.Count);

            Console.WriteLine();
            List<string> selectedNS = result == -2 ? namespaces : namespaces.FindAll(n => n == namespaces[result]);
            foreach (string ns in selectedNS)
            {
                Console.WriteLine(ns);
                IEnumerable<Type> builders = types
                    .Where(t => t.IsSubclassOf(typeof(InjectionBuilder)) && t.Namespace == $"{_topNS}.{ns}");
                foreach (Type type in builders)
                {
                    Console.WriteLine($"\t{type.Name}");
                    InjectionBuilder builder = (InjectionBuilder)Activator.CreateInstance(type);
                    builder.Build();
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.WriteLine();
        }
    }
}
