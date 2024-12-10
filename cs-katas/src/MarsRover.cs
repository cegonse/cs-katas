namespace src;

public readonly record struct Position(Coordinates Coordinates, Orientation Orientation)
{
    public Position WithOrientation(Orientation newOrientation)
    {
        return new Position(Coordinates, newOrientation);
    }

    public Position WithCoordinates(Coordinates newCoordinates)
    {
        return new Position(newCoordinates, Orientation);
    }
}

public readonly record struct Coordinates(int Latitude, int Longitude)
{
    public static Coordinates operator +(Coordinates a, Coordinates b) =>
        new Coordinates(a.Latitude + b.Latitude, a.Longitude + b.Longitude);

    public static Coordinates operator -(Coordinates a, Coordinates b) => a + (-b);
    public static Coordinates operator -(Coordinates a) => new Coordinates(-a.Latitude, -a.Longitude);
}

public enum Orientation
{
    North,
    South,
    East,
    West
}

public class MarsRover(Position position, Coordinates worldDimensions)
{
    public MarsRover(Position position) : this(position, new Coordinates(10, 10))
    {
    }

    private Position _position = position;
    public Position Position => _position;

    public void Move(string command)
    {
        var parsedCommand = FromString(command);
        _position = parsedCommand switch
        {
            Command.Move => _position.WithCoordinates(MoveCoordinates()),
            _ => _position.WithOrientation(OrientTowards(parsedCommand))
        };
    }

    private Coordinates MoveCoordinates()
    {
        var newCoordinates = _position.Orientation switch
        {
            Orientation.North => new Coordinates(0, 1),
            Orientation.South => new Coordinates(0, -1),
            Orientation.East => new Coordinates(1, 0),
            Orientation.West => new Coordinates(-1, 0),
            _ => new Coordinates(0, 0)
        } + _position.Coordinates;
        
        var wrapsLatitude = newCoordinates.Latitude > worldDimensions.Latitude ||
                            newCoordinates.Latitude < -worldDimensions.Latitude;
        var wrapsLongitude = newCoordinates.Longitude > worldDimensions.Longitude ||
                            newCoordinates.Longitude < -worldDimensions.Longitude;
        var latitudeWrapMultiplier = -Math.Sign(newCoordinates.Latitude);
        var longitudeWrapMultiplier = -Math.Sign(newCoordinates.Longitude);

        if (wrapsLatitude)
            return new Coordinates(worldDimensions.Latitude * latitudeWrapMultiplier, newCoordinates.Longitude);
        
        if (wrapsLongitude)
            return new Coordinates(newCoordinates.Latitude, worldDimensions.Longitude * longitudeWrapMultiplier);
            
        return newCoordinates;
    }

    private Orientation OrientTowards(Command command)
    {
        return _position.Orientation switch
        {
            Orientation.North => command == Command.Right ? Orientation.East : Orientation.West,
            Orientation.South => command == Command.Right ? Orientation.West : Orientation.East,
            Orientation.East => command == Command.Right ? Orientation.South : Orientation.North,
            Orientation.West => command == Command.Right ? Orientation.North : Orientation.South,
            _ => _position.Orientation
        };
    }

    private enum Command
    {
        Move,
        Right,
        Left
    }

    private static Command FromString(string command)
    {
        return command switch
        {
            "M" => Command.Move,
            "L" => Command.Left,
            "R" => Command.Right,
            _ => Command.Move
        };
    }
}