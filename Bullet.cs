using Godot;
using System;
using System.Diagnostics;

public partial class Bullet : Node3D
{
	public int Speed = 70;

	private double Timer;
	private const double TTL = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var forwardDirection = GlobalTransform.Basis.Z.Normalized();

		GlobalTranslate(forwardDirection * Speed * (float)delta);

		Timer += delta;

		if (Timer >= TTL) QueueFree();
	}
}
