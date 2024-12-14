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

    private bool IsAttacking = false;
    private Timer AttackTimer;

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
        CurrentSpeed = GD.RandRange(MinSpeed, MaxSpeed);

        Player = GetNode<Player>("../Player");
        NavigationAgent = GetNode<NavigationAgent3D>("NavigationAgent");

        NavigationAgent.GetNextPathPosition();

        AttackTimer = GetNode<Timer>("AttackTimer");

        MakePath();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsInstanceValid(Player))
        {
            var dir = ToLocal(NavigationAgent.GetNextPathPosition()).Normalized();

            LookAt(Player.Position);

            Velocity = dir * CurrentSpeed;
            Velocity = Velocity.Rotated(Vector3.Up, Rotation.Y);

            MoveAndSlide();
        }
    }

    private void MakePath()
    {
        if (IsInstanceValid(Player))
        {
            NavigationAgent.TargetPosition = Player.GlobalPosition;
        }
    }

    private void AttackPlayer()
    {
        var playerStatsNode = Player.GetNode<Stats>("Stats");
        playerStatsNode.TakeDamage(5);

        GD.Print($"Player HP: {playerStatsNode.CurrentHp}");
    }

    private void OnVisibilityNotifierScreenExited()
	{
		QueueFree();
	}

    private void OnAttackRadiusBodyEntered(Node3D node)
    {
        if (node == Player)
        {
            AttackPlayer();
            AttackTimer.Start();
        }
    }

    private void OnAttackRadiusBodyExit(Node3D node)
    {
        if (node == Player)
        {
            AttackTimer.Stop();
            GD.Print("No more attack");
        }
    }

    private void OnTimerTimeout()
    {
        MakePath();
    }
    private void OnAttackTimerTimeout()
    {
        AttackPlayer();
    }

    private void OnCharacterDeath()
    {
        QueueFree();
    }
}
