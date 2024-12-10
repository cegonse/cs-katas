using src;

namespace test;

public class Tests
{
    [Test]
    public void TestRoverKnowsItsLandingZone()
    {
        Position landingZone = new Position(new Coordinates(5, 5), Orientation.North);
        
        MarsRover rover = new MarsRover(landingZone);

        Assert.That(rover.Position, Is.EqualTo(landingZone));
    }

    [Test]
    public void TestRoverCanMoveForward()
    {
        Position landingZone = new Position(new Coordinates(1, 2), Orientation.North);
        Position finalPosition = new Position(new Coordinates(1, 5), Orientation.North);
        MarsRover rover = new MarsRover(landingZone);
        
        rover.Move("M");
        rover.Move("M");
        rover.Move("M");

        Assert.That(rover.Position, Is.EqualTo(finalPosition));
    }
    
    [Test]
    public void TestRoverCanMoveAndRotateRight()
    {
        Position landingZone = new Position(new Coordinates(1, 2), Orientation.North);
        Position finalPosition = new Position(new Coordinates(1, 2), Orientation.South);
        MarsRover rover = new MarsRover(landingZone);
        
        rover.Move("M");
        rover.Move("R");
        rover.Move("R");
        rover.Move("M");

        Assert.That(rover.Position, Is.EqualTo(finalPosition));
    }
    
    [Test]
    public void TestRoverCanMoveAndRotateLeft()
    {
        Position landingZone = new Position(new Coordinates(1, 2), Orientation.East);
        Position finalPosition = new Position(new Coordinates(1, 4), Orientation.West);
        MarsRover rover = new MarsRover(landingZone);
        
        rover.Move("M");
        rover.Move("L");
        rover.Move("M");
        rover.Move("M");
        rover.Move("L");
        rover.Move("M");

        Assert.That(rover.Position, Is.EqualTo(finalPosition));
    }

    [Test]
    public void TestRoverWrapsWhenPassingTheWorldEdge()
    {
        Position landingZone = new Position(new Coordinates(1, 1), Orientation.North);
        Coordinates worldDimensions = new Coordinates(5, 5);
        MarsRover rover = new MarsRover(landingZone, worldDimensions);
        Coordinates expectedCoordinates = new Coordinates(1, -worldDimensions.Longitude);
        
        rover.Move("M");
        rover.Move("M");
        rover.Move("M");
        rover.Move("M");
        rover.Move("M");
        
        Assert.That(rover.Position.Coordinates, Is.EqualTo(expectedCoordinates));
    }
}