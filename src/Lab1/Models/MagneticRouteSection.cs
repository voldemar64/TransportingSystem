using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class MagneticRouteSection : IRouteSectionService
{
    public double Distance { get; }

    public MagneticRouteSection(double distance)
    {
        if (distance <= 0)
        {
            throw new Exception("Distance can't be less than zero.");
        }

        Distance = distance;
    }

    public Result CalculateTravelTime(Train train)
    {
        if (train.Speed > 0)
        {
            return new Result.SuccessWithTime(Distance / train.Speed);
        }

        return new Result.ProcessingSectionFailure();
    }
}
