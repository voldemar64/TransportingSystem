using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Route
{
    public double MaxSpeed { get; private set; }

    public IList<IRouteSectionService> Sections { get; }

    public Route(IList<IRouteSectionService> sections, double maxSpeed)
    {
        MaxSpeed = maxSpeed;
        Sections = sections;
    }

    public void AddSection(IRouteSectionService section)
    {
        Sections.Add(section);
    }

    public Result TravelRoute(Train train)
    {
        if (Sections == null)
        {
            throw new Exception("Route can't be null");
        }

        double totalTime = 0;

        foreach (IRouteSectionService section in Sections)
        {
            Result segmentResult = section.CalculateTravelTime(train);
            if (segmentResult is Result.SuccessWithTime success)
            {
                totalTime += success.Time;
            }
            else
            {
                return segmentResult;
            }
        }

        if (train.Speed > MaxSpeed)
        {
            return new Result.ExceededMaxSpeed(train.Speed);
        }

        return new Result.SuccessWithTime(totalTime);
    }
}