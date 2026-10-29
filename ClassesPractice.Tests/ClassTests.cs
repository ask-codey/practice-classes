namespace ClassesPractice.Tests;

// One test class per class you write. Work on one at a time: the tests for
// a class you haven't started will fail until you create it.

[TestClass]
public class RectangleTests
{
    [TestMethod]
    public void Rectangle_HasItsProperties()
    {
        Check.Property("Rectangle", "Width", typeof(int), Access.Public);
        Check.Property("Rectangle", "Height", typeof(int), Access.Public);
        Check.Property("Rectangle", "Area", typeof(int), Access.None);
    }

    [TestMethod]
    public void Rectangle_ParameterlessConstructor_CanBeSetLater()
    {
        object rectangle = Check.New("Rectangle");
        Check.Set(rectangle, "Width", 5);
        Check.Set(rectangle, "Height", 2);

        Assert.AreEqual(10, Check.Get<int>(rectangle, "Area"), "A 5 x 2 rectangle has an area of 10.");
    }

    [TestMethod]
    [DataRow(3, 4, 12)]
    [DataRow(10, 10, 100)]
    [DataRow(7, 1, 7)]
    public void Rectangle_ConstructorSetsSize_AndAreaIsCalculated(int width, int height, int area)
    {
        object rectangle = Check.New("Rectangle", width, height);

        Assert.AreEqual(width, Check.Get<int>(rectangle, "Width"), "The constructor sets Width.");
        Assert.AreEqual(height, Check.Get<int>(rectangle, "Height"), "The constructor sets Height.");
        Assert.AreEqual(area, Check.Get<int>(rectangle, "Area"), $"A {width} x {height} rectangle has an area of {area}.");
    }
}

[TestClass]
public class CircleTests
{
    [TestMethod]
    public void Circle_HasItsProperties()
    {
        Check.Property("Circle", "Radius", typeof(int), Access.Public);
        Check.Property("Circle", "Diameter", typeof(int), Access.None);
        Check.Property("Circle", "Area", typeof(double), Access.None);
        Check.Property("Circle", "Circumference", typeof(double), Access.None);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(5)]
    [DataRow(12)]
    public void Circle_CalculatesFromItsRadius(int radius)
    {
        object circle = Check.New("Circle", radius);

        Assert.AreEqual(radius, Check.Get<int>(circle, "Radius"), "The constructor sets Radius.");
        Assert.AreEqual(radius * 2, Check.Get<int>(circle, "Diameter"), "The diameter is twice the radius.");
        Assert.AreEqual(Math.PI * radius * radius, Check.Get<double>(circle, "Area"), 0.0001, "The area is π times the radius squared.");
        Assert.AreEqual(2 * Math.PI * radius, Check.Get<double>(circle, "Circumference"), 0.0001, "The circumference is 2 times π times the radius.");
    }

    [TestMethod]
    public void Circle_ParameterlessConstructor_CanBeSetLater()
    {
        object circle = Check.New("Circle");
        Check.Set(circle, "Radius", 3);

        Assert.AreEqual(6, Check.Get<int>(circle, "Diameter"), "A circle with a radius of 3 has a diameter of 6.");
    }
}

[TestClass]
public class MathTestTests
{
    [TestMethod]
    public void MathTest_HasItsProperties()
    {
        Check.Property("MathTest", "Score", typeof(int), Access.Public);
        Check.Property("MathTest", "PossiblePoints", typeof(int), Access.Private);
        Check.Property("MathTest", "StudentName", typeof(string), Access.Private);
        Check.Property("MathTest", "Percent", typeof(int), Access.None);
        Check.Property("MathTest", "LetterGrade", typeof(string), Access.None);
    }

    [TestMethod]
    public void MathTest_ConstructorSetsPointsAndName()
    {
        object test = Check.New("MathTest", 50, "Ana");

        Assert.AreEqual(50, Check.Get<int>(test, "PossiblePoints"));
        Assert.AreEqual("Ana", Check.Get<string>(test, "StudentName"));
    }

    [TestMethod]
    [DataRow(100, 95, 95, "A")]
    [DataRow(100, 90, 90, "A")]
    [DataRow(50, 44, 88, "B")]
    [DataRow(40, 30, 75, "C")]
    [DataRow(20, 13, 65, "D")]
    [DataRow(20, 11, 55, "F")]
    public void MathTest_CalculatesPercentAndLetterGrade(int possible, int score, int percent, string grade)
    {
        object test = Check.New("MathTest", possible, "Ana");
        Check.Set(test, "Score", score);

        Assert.AreEqual(percent, Check.Get<int>(test, "Percent"), $"{score} out of {possible} is {percent}%.");
        Assert.AreEqual(grade, Check.Get<string>(test, "LetterGrade"), $"{percent}% is a {grade}.");
    }
}

[TestClass]
public class PrinterTests
{
    [TestMethod]
    public void Printer_HasItsPropertiesAndMethods()
    {
        Check.Property("Printer", "Paper", typeof(int), Access.Private);
        Check.Property("Printer", "Toner", typeof(int), Access.Private);
        Check.Returns("Printer", "Print", typeof(int), typeof(int));
        Check.Returns("Printer", "ReplaceToner", typeof(void));
        Check.Returns("Printer", "AddPaper", typeof(void), typeof(int));
    }

    [TestMethod]
    [DataRow(100, 100, 10, 10, 90, 90)]
    [DataRow(20, 100, 50, 20, 0, 80)]
    [DataRow(100, 5, 50, 5, 95, 0)]
    [DataRow(100, 100, -3, 0, 100, 100)]
    public void Print_UsesPaperAndToner_AndNeverGoesBelowZero(int paper, int toner, int copies, int printed, int paperLeft, int tonerLeft)
    {
        object printer = Check.New("Printer", paper, toner);

        Assert.AreEqual(printed, (int)Check.Call(printer, "Print", copies)!, $"With {paper} sheets and {toner} toner, asking for {copies} copies prints {printed}.");
        Assert.AreEqual(paperLeft, Check.Get<int>(printer, "Paper"), "Each copy uses one sheet of paper.");
        Assert.AreEqual(tonerLeft, Check.Get<int>(printer, "Toner"), "Each copy uses one unit of toner.");
    }

    [TestMethod]
    public void ReplaceToner_FillsItTo1000()
    {
        object printer = Check.New("Printer", 100, 12);
        Check.Call(printer, "ReplaceToner");

        Assert.AreEqual(1000, Check.Get<int>(printer, "Toner"), "New toner holds 1000.");
    }

    [TestMethod]
    [DataRow(100, 50, 150)]
    [DataRow(450, 100, 500)]
    [DataRow(100, -20, 100)]
    public void AddPaper_NeverGoesOver500(int paper, int sheets, int expected)
    {
        object printer = Check.New("Printer", paper, 100);
        Check.Call(printer, "AddPaper", sheets);

        Assert.AreEqual(expected, Check.Get<int>(printer, "Paper"), $"Adding {sheets} sheets to {paper} leaves {expected} (500 at most, and never fewer).");
    }
}

[TestClass]
public class ElevatorTests
{
    [TestMethod]
    public void Elevator_HasItsPropertiesAndMethods()
    {
        Check.Property("Elevator", "CurrentLevel", typeof(int), Access.Private);
        Check.Property("Elevator", "NumberOfLevels", typeof(int), Access.Private);
        Check.Property("Elevator", "DoorIsOpen", typeof(bool), Access.Private);
        Check.Returns("Elevator", "OpenDoor", typeof(void));
        Check.Returns("Elevator", "CloseDoor", typeof(void));
        Check.Returns("Elevator", "GoUp", typeof(void), typeof(int));
        Check.Returns("Elevator", "GoDown", typeof(void), typeof(int));
    }

    [TestMethod]
    public void NewElevator_StartsOnFloorOne_WithTheDoorClosed()
    {
        object elevator = Check.New("Elevator", 10);

        Assert.AreEqual(1, Check.Get<int>(elevator, "CurrentLevel"), "A new elevator starts on floor 1.");
        Assert.AreEqual(10, Check.Get<int>(elevator, "NumberOfLevels"));
        Assert.IsFalse(Check.Get<bool>(elevator, "DoorIsOpen"), "A new elevator's door is closed.");
    }

    [TestMethod]
    public void Door_OpensAndCloses()
    {
        object elevator = Check.New("Elevator", 10);

        Check.Call(elevator, "OpenDoor");
        Assert.IsTrue(Check.Get<bool>(elevator, "DoorIsOpen"), "OpenDoor opens the door.");
        Check.Call(elevator, "CloseDoor");
        Assert.IsFalse(Check.Get<bool>(elevator, "DoorIsOpen"), "CloseDoor closes it.");
    }

    [TestMethod]
    [DataRow(5, 5)]
    [DataRow(10, 10)]
    [DataRow(11, 1)]
    public void GoUp_MovesUpToTheTopFloor_ButNotPastIt(int desiredFloor, int expected)
    {
        object elevator = Check.New("Elevator", 10);
        Check.Call(elevator, "GoUp", desiredFloor);

        Assert.AreEqual(expected, Check.Get<int>(elevator, "CurrentLevel"), $"In a 10-floor building, going up to {desiredFloor} from floor 1 ends on floor {expected}.");
    }

    [TestMethod]
    public void Elevator_DoesNotMove_WhileTheDoorIsOpen()
    {
        object elevator = Check.New("Elevator", 10);
        Check.Call(elevator, "OpenDoor");
        Check.Call(elevator, "GoUp", 5);

        Assert.AreEqual(1, Check.Get<int>(elevator, "CurrentLevel"), "An elevator with its door open stays where it is.");
    }

    [TestMethod]
    [DataRow(3, 3)]
    [DataRow(1, 1)]
    [DataRow(0, 7)]
    [DataRow(9, 7)]
    public void GoDown_MovesDownToFloorOne_ButNotPastIt(int desiredFloor, int expected)
    {
        object elevator = Check.New("Elevator", 10);
        Check.Call(elevator, "GoUp", 7);
        Check.Call(elevator, "GoDown", desiredFloor);

        Assert.AreEqual(expected, Check.Get<int>(elevator, "CurrentLevel"), $"From floor 7, going down to {desiredFloor} ends on floor {expected}.");
    }
}

[TestClass]
public class AirplaneTests
{
    [TestMethod]
    public void Airplane_HasItsProperties()
    {
        Check.Property("Airplane", "PlaneNumber", typeof(string), Access.Private);
        Check.Property("Airplane", "TotalFirstClassSeats", typeof(int), Access.Private);
        Check.Property("Airplane", "BookedFirstClassSeats", typeof(int), Access.Private);
        Check.Property("Airplane", "AvailableFirstClassSeats", typeof(int), Access.None);
        Check.Property("Airplane", "TotalCoachSeats", typeof(int), Access.Private);
        Check.Property("Airplane", "BookedCoachSeats", typeof(int), Access.Private);
        Check.Property("Airplane", "AvailableCoachSeats", typeof(int), Access.None);
        Check.Returns("Airplane", "ReserveSeats", typeof(bool), typeof(bool), typeof(int));
    }

    [TestMethod]
    public void NewAirplane_HasEverySeatAvailable()
    {
        object plane = Check.New("Airplane", "FS1200", 12, 150);

        Assert.AreEqual("FS1200", Check.Get<string>(plane, "PlaneNumber"));
        Assert.AreEqual(12, Check.Get<int>(plane, "AvailableFirstClassSeats"));
        Assert.AreEqual(150, Check.Get<int>(plane, "AvailableCoachSeats"));
    }

    [TestMethod]
    public void ReserveSeats_BooksSeats_WhenThereIsRoom()
    {
        object plane = Check.New("Airplane", "FS1200", 12, 150);

        Assert.IsTrue((bool)Check.Call(plane, "ReserveSeats", true, 4)!, "4 of 12 first class seats can be booked.");
        Assert.IsTrue((bool)Check.Call(plane, "ReserveSeats", false, 150)!, "All 150 coach seats can be booked.");
        Assert.AreEqual(8, Check.Get<int>(plane, "AvailableFirstClassSeats"));
        Assert.AreEqual(0, Check.Get<int>(plane, "AvailableCoachSeats"));
    }

    [TestMethod]
    public void ReserveSeats_BooksNothing_WhenThereIsNotRoom()
    {
        object plane = Check.New("Airplane", "FS1200", 12, 150);
        Check.Call(plane, "ReserveSeats", true, 10);

        Assert.IsFalse((bool)Check.Call(plane, "ReserveSeats", true, 3)!, "Only 2 first class seats are left, so 3 can't be booked.");
        Assert.AreEqual(10, Check.Get<int>(plane, "BookedFirstClassSeats"), "A reservation that doesn't fit books nothing.");
    }
}
