using System.Runtime.Serialization;
using AutoMapper;
using Ships.Application.Common.Mappings;
using Ships.Application.Common.Models;
using Ships.Application.ShipsQR.Queries;
using Ships.Domain.Entities;

namespace Application.UnitTests;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    public void ShouldSupportMappingFromSourceToDestination()
    {
        var instance = GetInstanceOf(typeof(Ship));

        _mapper.Map(instance, typeof(Ship), typeof(ShipDto));
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        return FormatterServices.GetUninitializedObject(type);
    }
}
