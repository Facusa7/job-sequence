using System;
using System.Collections.Generic;

namespace JobSequence
{
    public static class Mappers
    {
        public static Dictionary<string, string> MapJobsToDependencyTree(string dependencies)
        {
            var dependencyTree = new Dictionary<string, string>();

            foreach (var jobDependency in dependencies.Split(','))
            {
                var node = jobDependency.Split(':');
                var job = node[0];
                var dependency = string.Empty;

                if (node.Length > 1)
                {
                    if(node[0] == node[1]) throw new ArgumentException("Jobs cannot depend on themselves");
                    dependency = node[1];

                }

                if (!dependencyTree.TryAdd(job, dependency))
                {
                    throw new ArgumentException("Cannot add repeated jobs to the sequence");
                }
            }

            return dependencyTree;
        }
    }
}
