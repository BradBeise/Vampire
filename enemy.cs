using Godot;
using System;

public partial class enemy : CharacterBody3D
{
    // Minimum speed of the enemy in meters per second.
    [Export]
    public int MinSpeed { get; set; } = 10;
    // Maximum speed of the enemy in meters per second.
    [Export]
    public int MaxSpeed { get; set; } = 18;

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    // This function will be called from the Main scene.
	// TODO: Better path finding. Maybe A*?
    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        
		//Position = startPosition;

        //RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));

        int randomSpeed = GD.RandRange(MinSpeed, MaxSpeed);
        
        //Velocity = Vector3.Forward * randomSpeed;
        
        //Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }

	private void OnVisibilityNotifierScreenExited()
	{
		QueueFree();
	}
}
