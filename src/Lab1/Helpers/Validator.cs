namespace Itmo.ObjectOrientedProgramming.Lab1.Helpers;

public static class Validator
{
    public static void IsBigger(double number)
    {
        if (number <= 0)
        {
            throw new Exception("The argument can't be less than zero.");
        }
    }
}