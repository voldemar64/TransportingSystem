using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;
using Xunit;

namespace Lab1.Tests;

public class TrainTravelTests
{
    [Fact]
    public void PowerMagneticRouteSectionAndMagneticRouteSectionShouldReturnSuccess()
    {
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(239, 60),
            new MagneticRouteSection(52),
        };

        var train = new Train(5, 0, 100, 1);
        var route = new Route(segments, 1000);
        Assert.IsType<Result.SuccessWithTime>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithHugeForceMagneticRouteSectionShouldReturnFailure()
    {
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(239, 160),
            new MagneticRouteSection(52),
        };

        var train = new Train(5, 0, 100, 1);
        var route = new Route(segments, 1000);
        Assert.IsType<Result.InvalidForceApplied>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithNormalForceMagneticRouteSectionStationMagneticRouteSectionShouldReturnSuccess()
    {
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(100, 100),
            new MagneticRouteSection(52),
            new Station(5, 10, 100),
            new MagneticRouteSection(2007),
        };

        var train = new Train(100, 0, 100, 1);
        var route = new Route(segments, 1000);
        Assert.IsType<Result.SuccessWithTime>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithHugeForceStationMagneticRouteSectionShouldReturnFailure()
    {
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(239, 100),
            new Station(5, 10, 2),
            new MagneticRouteSection(52),
        };

        var train = new Train(5, 0, 100, 1);
        var route = new Route(segments, 1000);
        Assert.IsType<Result.ExceededMaxSpeed>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithBiggerForceMagneticRouteSectionStationMagneticRouteSectionShouldReturnFailure()
    {
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(100, 100),
            new MagneticRouteSection(52),
            new Station(5, 10, 100),
            new MagneticRouteSection(2007),
        };

        var train = new Train(100, 0, 100, 1);
        var route = new Route(segments, 2);
        Assert.IsType<Result.ExceededMaxSpeed>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithBiggerForceMagneticRouteSectionPowerMagneticRouteSectionWithLessForceStationMagneticRouteSectionPowerMagneticRouteSectionWithBiggerForceMagneticRouteSectionPowerMagneticRouteSectionWithLessForceShouldReturnSuccess()
    {
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(100, 100),
            new MagneticRouteSection(52),
            new PowerMagneticRouteSection(50, -100),
            new Station(5, 10, 50),
            new MagneticRouteSection(2007),
            new PowerMagneticRouteSection(100, 100),
            new MagneticRouteSection(52),
            new PowerMagneticRouteSection(100, -80),
        };

        var train = new Train(100, 0, 100, 1);
        var route = new Route(segments, 150);
        Assert.IsType<Result.SuccessWithTime>(route.TravelRoute(train));
    }
}