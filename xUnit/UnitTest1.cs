using ForAutomaticTest.Controllers;
using static System.Reflection.Metadata.BlobBuilder;

namespace xUnit;

public class UnitTest1
{
    WeatherForecastController testController;

    public UnitTest1()
    {
        testController = new WeatherForecastController();
    }

    [Fact]
    public void success()
    {
        var count = testController.calculator(3, 3);
        Assert.Equal(9, count);
    }

    [Fact]
    public void unRun()
    {
        var count = testController.calculator(3, 3);
        Assert.Equal(3, count);
    }

}