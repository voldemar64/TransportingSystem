using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;
using Xunit;

namespace Lab1.Tests;

public class TrainTravelTests
{
    private const double ShortDistance = 50;
    private const double Distance = 100;
    private const double HugeNegativeForce = -100;
    private const double NegativeForce = -80;
    private const double NormalForce = 60;
    private const double MediumForce = 100;
    private const double HugeForce = 160;
    private const double SmallMass = 5;
    private const double HugeMass = 100;
    private const double MaxLowSpeed = 2;
    private const double MaxNormalSpeed = 50;
    private const double MaxMediumSpeed = 100;
    private const double MaxHighSpeed = 1000;
    private const double InitialSpeed = 0;
    private const double MaxForce = 100;
    private const double Occupancy = 10;
    private const double Accuracy = 1;

    [Fact]
    public void PowerMagneticRouteSectionAndMagneticRouteSectionShouldReturnSuccess()
    {
        // Arrange
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(Distance, NormalForce),
            new MagneticRouteSection(Distance),
        };
        var train = new Train(SmallMass, InitialSpeed, MaxForce, Accuracy);
        var route = new Route(segments, MaxHighSpeed);

        // Act and assert
        Assert.IsType<Result.SuccessWithTime>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithHugeForceMagneticRouteSectionShouldReturnFailure()
    {
        // Arrange
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(Distance, HugeForce),
            new MagneticRouteSection(Distance),
        };
        var train = new Train(SmallMass, InitialSpeed, MaxForce, Accuracy);
        var route = new Route(segments, MaxHighSpeed);

        // Act and assert
        Assert.IsType<Result.InvalidForceApplied>(route.TravelRoute(train));
    }

    [Fact]
    public void
        PowerMagneticRouteSectionWithNormalForceMagneticRouteSectionStationMagneticRouteSectionShouldReturnSuccess()
    {
        // Arrange
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(Distance, NormalForce),
            new MagneticRouteSection(Distance),
            new Station(Distance, Occupancy, MaxMediumSpeed),
            new MagneticRouteSection(Distance),
        };
        var train = new Train(HugeMass, InitialSpeed, MaxForce, Accuracy);
        var route = new Route(segments, MaxHighSpeed);

        // Act and assert
        Assert.IsType<Result.SuccessWithTime>(route.TravelRoute(train));
    }

    [Fact]
    public void PowerMagneticRouteSectionWithHugeForceStationMagneticRouteSectionShouldReturnFailure()
    {
        // Arrange
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(Distance, MediumForce),
            new Station(Distance, Occupancy, MaxLowSpeed),
            new MagneticRouteSection(Distance),
        };
        var train = new Train(SmallMass, InitialSpeed, MaxForce, Accuracy);
        var route = new Route(segments, MaxHighSpeed);

        // Act and assert
        Assert.IsType<Result.ExceededMaxSpeed>(route.TravelRoute(train));
    }

    [Fact]
    public void
        PowerMagneticRouteSectionWithBiggerForceMagneticRouteSectionStationMagneticRouteSectionShouldReturnFailure()
    {
        // Arrange
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(Distance, MediumForce),
            new MagneticRouteSection(Distance),
            new Station(Distance, Occupancy, MaxMediumSpeed),
            new MagneticRouteSection(Distance),
        };
        var train = new Train(HugeMass, InitialSpeed, MaxForce, Accuracy);
        var route = new Route(segments, MaxLowSpeed);

        // Act and assert
        Assert.IsType<Result.ExceededMaxSpeed>(route.TravelRoute(train));
    }

    [Fact]
    public void
        PowerMagneticRouteSectionWithBiggerForceMagneticRouteSectionPowerMagneticRouteSectionWithLessForceStationMagneticRouteSectionPowerMagneticRouteSectionWithBiggerForceMagneticRouteSectionPowerMagneticRouteSectionWithLessForceShouldReturnSuccess()
    {
        // Arrange
        var segments = new List<IRouteSectionService>
        {
            new PowerMagneticRouteSection(Distance, MediumForce),
            new MagneticRouteSection(Distance),
            new PowerMagneticRouteSection(ShortDistance, HugeNegativeForce),
            new Station(Distance, Occupancy, MaxNormalSpeed),
            new MagneticRouteSection(Distance),
            new PowerMagneticRouteSection(Distance, MediumForce),
            new MagneticRouteSection(Distance),
            new PowerMagneticRouteSection(Distance, NegativeForce),
        };
        var train = new Train(HugeMass, InitialSpeed, MaxForce, Accuracy);
        var route = new Route(segments, MaxMediumSpeed);

        // Act and assert
        Assert.IsType<Result.SuccessWithTime>(route.TravelRoute(train));
    }
}