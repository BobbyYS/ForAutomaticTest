using ForAutomaticTest.Controllers;


namespace NUnit
{
    //https://medium.com/@agrawalvishesh9271/mastering-unit-testing-in-net-core-2-1b0f48075b61
    [TestFixture]
    public class Tests
    {

        [SetUp]
        public void SetUp()
        {
            Console.Out.WriteLine("SetUp");
        }
        
        [TearDown]
        public void TearDown()
        {
            Console.Out.WriteLine("TearDown");
        }

        [Test]
        public void Test1()
        {
            Console.Out.WriteLine("Test1");
        }

        [Test]
        public void Test2()
        {
            int[] array = { 1, 2, 3 };
            Assert.Contains(3, array.ToList());


            //Assert.That(array, Is.EqualTo(3));
            //Assert.That(array, Has.Exactly(2).GreaterThan(1));
            //Assert.That(array, Has.Exactly(3).LessThan(100));
            Console.Out.WriteLine("Test2");
        }



        //WeatherForecastController testController;

        //[SetUp]
        //public void Setup()
        //{

        //    testController = new WeatherForecastController();
        //}

        //[Test]
        //public void Test1()
        //{
        //    var count = testController.calculator(3,3);
        //    Assert.That(count == 3*3);
        //}
    }
}