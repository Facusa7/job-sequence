using System;

namespace JobSequence
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dependencies = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dependencies))
            {
                Console.WriteLine("Empty sequence was sent");
                Console.ReadLine();
                return;
            }

            try
            {
                var dependencyTree = Mappers.MapJobsToDependencyTree(dependencies);
                var result = Result.OrderJobs(dependencyTree);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
