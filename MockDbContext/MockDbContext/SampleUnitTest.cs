using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Data.Entities;
using App.Data.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Testing.Common.Helpers;
using Xunit;

namespace TestingSample
{
    public class SampleUnitTest
    {
        private readonly IEnumerable<Person> _persons = new List<Person>
        {
            new Person
            {
                Identification = "123456-1234A",
                FirstName = "Peter",
                LastName = "Yutani"
            },
            new Person
            {
                Identification = "123456-1234B",
                FirstName = "Bishop",
                LastName = "Yutani"
            }
        };

        private readonly IEnumerable<Pet> _pets = new List<Pet>
        {
            new Pet
            {
                Identification = Guid.NewGuid(),
                FullName = "Spot"
            },
            new Pet
            {
                Identification = Guid.NewGuid(),
                FullName = "Bella"
            }
        };

        private readonly IEnumerable<Hobby> _hobbies = new List<Hobby>
        {
            new Hobby
            {
                Identifier = Guid.NewGuid(),
                Name = "Badminton"
            },
            new Hobby
            {
                Identifier = Guid.NewGuid(),
                Name = "Curling"
            }
        };

        private readonly Mock<ISampleDbContext> _mockDbContext;

        public SampleUnitTest()
        {
            _mockDbContext = MockDbContextBuilder
                .BuildMockDbContext<ISampleDbContext>()
                .AttachMockDbSetToSetMethodCall(MockDbContextBuilder.BuildMockDbSet(_persons))
                .AttachMockDbSetToPropertyCall(MockDbContextBuilder.BuildMockDbSet(_pets), context => context.Pets)
                .AttachMockDbSetToModelCall(MockDbContextBuilder.BuildMockDbSet(_hobbies), "Hobbies");
        }

        [Fact]
        public void SetMethod_ShouldBeMockedCorrectly()
        {
            // Act
            var result = _mockDbContext.Object
                .Set<Person>()
                .Where(p => p.Identification == "123456-1234A")
                .ToList();

            // Assert
            result.Should().BeEquivalentTo(_persons.First());
        }

        [Fact]
        public void EntityProperty_ShouldBeMockedCorrectly()
        {
            // Act
            var result = _mockDbContext.Object
                .Pets
                .Where(p => p.FullName == "Bella");

            // Assert
            result.Should().BeEquivalentTo(_pets.Skip(1).First());
        }

        [Fact]
        public void Model_ShouldBeMockedCorrectly()
        {
            // Act
            var result = _mockDbContext.Object
                .Model["Hobbies"];

            // Assert
            result.Should().BeEquivalentTo(_hobbies);
        }
    }
}
