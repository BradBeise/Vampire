using Godot;
using System;

public partial class enemy : CharacterBody3D
{
    // Minimum speed of the enemy in meters per second.
    [Export]
    public int MinSpeed = 2;
    // Maximum speed of the enemy in meters per second.
    [Export]
    public int MaxSpeed = 5;

    private int CurrentSpeed;
    private int Health;

    private Player Player;

    public override void _Ready()
    {
        Player = GetNode<Player>("../Player");
        Health = 100;
    }

    public override void _PhysicsProcess(double delta)
    {
        var playerLocation = Player.Position;

        var look = new Vector3(playerLocation.X, Position.Y, playerLocation.Z);

        LookAt(look, Vector3.Up);

        Velocity = Vector3.Forward * CurrentSpeed;

        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);

        MoveAndSlide();
    }

    // This function will be called from the Main scene.
	// TODO: Better path finding. Maybe A*?
    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);

        CurrentSpeed = GD.RandRange(MinSpeed, MaxSpeed);
        
        Velocity = Vector3.Forward * CurrentSpeed;
        
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }

	private void OnVisibilityNotifierScreenExited()
	{
		QueueFree();
	}
}
