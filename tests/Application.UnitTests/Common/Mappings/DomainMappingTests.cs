using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using JobCandidate.Application.Candidates.Commands.UpdateCandidate;
using JobCandidate.Application.Common.Interfaces;
using JobCandidate.Domain.Entities;
using NUnit.Framework;

namespace JobCandidate.Application.UnitTests.Common.Mappings;
public class DomainMappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public DomainMappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(UpdateCandidateCommand), typeof(Candidate))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
