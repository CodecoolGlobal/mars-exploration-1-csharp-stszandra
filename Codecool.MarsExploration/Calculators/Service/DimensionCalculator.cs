
namespace Codecool.MarsExploration.Calculators.Service;

public class DimensionCalculator : IDimensionCalculator
{
    public int CalculateDimension(int size, int dimensionGrowth)
    {
        double squareRootOfSize = Math.Sqrt(size);
        int minDimension = (int)Math.Ceiling(squareRootOfSize);
        return minDimension + dimensionGrowth;
    }
}