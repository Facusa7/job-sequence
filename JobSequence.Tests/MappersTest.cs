using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace JobSequence.Tests
{
    public class MappersTest
    {

        [Theory]
        [InlineData("a,b:c,c")]
        public void Sequence_Is_Correctly_Parsed(string sequence)
        {
            var expected = new Dictionary<string, string>()
            {
                {"a", ""},
                {"b", "c"},
                {"c", ""}
            };

            var dependencies = Mappers.MapJobsToDependencyTree(sequence);

            dependencies.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("a,b:b,c")]
        public void Exception_Is_Thrown_When_Jobs_Depend_On_Themselves(string sequence)
        {
            Action act = () => { Mappers.MapJobsToDependencyTree(sequence); };

            act.Should().Throw<ArgumentException>("Jobs cannot depend on themselves");
        }

        [Theory]
        [InlineData("a,b:c,b")]
        public void Exception_Is_Thrown_When_Duplicate_Jobs_Are_Submitted(string sequence)
        {
            Action act = () => { Mappers.MapJobsToDependencyTree(sequence); };

            act.Should().Throw<ArgumentException>("Cannot add repeated jobs to the sequence");
        }
    }
}
