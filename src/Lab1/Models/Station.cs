using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Station : IRouteSectionService
{
    public double Distance { get; }

    public double Occupancy { get; }

    public double MaxSpeed { get; }

    public Station(double distance, double occupancy, double maxSpeed)
    {
        if (distance <= 0)
        {
            throw new Exception("Distance can't be less than zero.");
        }

        if (maxSpeed <= 0)
        {
            throw new Exception("Maximum speed can't be less than zero.");
        }

        Distance = distance;
        MaxSpeed = maxSpeed;
        Occupancy = occupancy;
    }

    public Result CalculateTravelTime(Train train)
    {
        if (train.Speed <= MaxSpeed)
        {
            return new Result.SuccessWithTime(Occupancy + (Distance / train.Speed));
        }

        return new Result.ExceededMaxSpeed(train.Speed);
    }
}
