using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class PowerMagneticRouteSection : IRouteSectionService
{
    public double Distance { get; }

    public double RemainedDistance { get; private set; }

    public double Force { get; }

    public PowerMagneticRouteSection(double distance, double force)
    {
        if (distance <= 0)
        {
            throw new Exception("Distance can't be less than zero.");
        }

        Distance = distance;
        RemainedDistance = distance;
        Force = force;
    }

    public Result CalculateTravelTime(Train train)
    {
        if (Force > train.MaxForce)
        {
            return new Result.InvalidForceApplied(Force);
        }

        double time = 0;
        train.ApplyForce(Force);
        while (RemainedDistance > 0)
        {
            Result result = train.ChangeSpeed();
            if (result is Result.InvalidSpeed)
            {
                return result;
            }

            time += ChangeDistance(train);
        }

        return new Result.SuccessWithTime(time);
    }

    private double ChangeDistance(Train train)
    {
        if (RemainedDistance < train.Speed * train.Accuracy)
        {
            RemainedDistance = 0;
            return Distance / train.Speed;
        }

        RemainedDistance -= train.Speed * train.Accuracy;
        return train.Accuracy;
    }
}
