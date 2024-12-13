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
    private NavigationAgent3D NavigationAgent;

    // This function will be called from the Main scene.
    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);

        CurrentSpeed = GD.RandRange(MinSpeed, MaxSpeed);

        //Velocity = Vector3.Forward * CurrentSpeed;

        //Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);
    }

    public override void _Ready()
    {
        Player = GetNode<Player>("../Player");
        NavigationAgent = GetNode<NavigationAgent3D>("NavigationAgent");

        NavigationAgent.GetNextPathPosition();

        MakePath();

        Health = 100;
    }

    public override void _PhysicsProcess(double delta)
    {
        var dir = ToLocal(NavigationAgent.GetNextPathPosition()).Normalized();

        LookAt(Player.Position);

        Velocity = dir * CurrentSpeed;
        Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);

        MoveAndSlide();
    }

    private void MakePath()
    {
        NavigationAgent.TargetPosition = Player.GlobalPosition;
    }

	private void OnVisibilityNotifierScreenExited()
	{
		QueueFree();
	}

    private void OnTimerTimeout()
    {
        MakePath();
    }

    private void OnCharacterDeath()
    {
        QueueFree();
    }
}
