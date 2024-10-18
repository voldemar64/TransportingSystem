using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Train : ITrain
{
    public double Speed { get; private set; }

    public double Acceleration { get; private set; }

    public double Accuracy { get; }

    public double MaxForce { get; }

    public double Mass { get; }

    public Train(
        double mass,
        double initialSpeed,
        double maxForce,
        double accuracy)
    {
        if (mass <= 0 || accuracy <= 0)
        {
            throw new ArgumentException("Mass or accuracy must be greater than zero.");
        }

        Mass = mass;
        Speed = initialSpeed;
        Accuracy = accuracy;
        MaxForce = maxForce;
        Acceleration = 0;
    }

    public void ApplyForce(double force)
    {
        Acceleration = force / Mass;
    }

    public Result ChangeSpeed()
    {
        Speed += Acceleration * Accuracy;
        if (Speed < 0)
        {
            return new Result.InvalidSpeed(Speed);
        }

        return new Result.Success();
    }
}