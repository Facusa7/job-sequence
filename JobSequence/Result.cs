using System;
using System.Collections.Generic;
using System.Linq;

namespace JobSequence
{
    public static class Result
    {
        public static string OrderJobs(IDictionary<string, string> dependencyTree)
        {
            var result = new List<string>();
            
            while (result.Count < dependencyTree.Count)
            {
                var keys = dependencyTree.Keys.Except(result);
                var added = false;

                foreach (var key in keys)
                {
                    var jobHasNoDependencies = string.IsNullOrWhiteSpace(dependencyTree[key]);
                    var jobDependencyWasAlreadyAdded = result.Contains(dependencyTree[key]);
                    var jobWasAlreadyAdded = result.Contains(key);

                    if (jobWasAlreadyAdded || (!jobHasNoDependencies && !jobDependencyWasAlreadyAdded)) continue;
                    
                    result.Add(key);
                    added = true;
                }

                if (!added) throw new Exception("Cannot have circular dependency");
            }

            return string.Join("", result);
        }
    }
}
