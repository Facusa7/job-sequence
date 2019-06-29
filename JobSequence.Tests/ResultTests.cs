using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace JobSequence.Tests
{
    public class ResultTests
    {   
        [Theory]
        [InlineData("a,b:c,c")]
        public void Sequence_Is_Returned_Correctly(string sequence)
        {
            var expected = "acb";

            var dependencies = Mappers.MapJobsToDependencyTree(sequence);
            var result = Result.OrderJobs(dependencies);

            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("a,b:c,c:f,d:a,e,f:b")]
        public void Exception_Is_Thrown_When_Circular_Dependency_Is_Detected(string sequence)
        {

            Action act = () =>
            {
                var dependencies = Mappers.MapJobsToDependencyTree(sequence);
                Result.OrderJobs(dependencies);
            };

            act.Should().Throw<Exception>("Cannot have circular dependency");
        }
    }
}
