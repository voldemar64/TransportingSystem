using Itmo.ObjectOrientedProgramming.Lab1.Models;
using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

namespace Itmo.ObjectOrientedProgramming.Lab1.Interfaces;

public interface IRouteSectionService
{
    public double Distance { get; }

    Result CalculateTravelTime(Train train);
}