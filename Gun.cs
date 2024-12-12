using Godot;
using System;

public partial class Gun : Node3D
{
	[Export]
	private PackedScene Bullet;

	
	private int MuzzleSpeed = 30;
	private double MilliBetweenShots = 70;

	private Timer RateOfFireTimer;
	private bool CanShoot = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RateOfFireTimer = (Timer)GetNode("Timer");

		RateOfFireTimer.WaitTime = MilliBetweenShots / 1000.0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void Shoot()
    {
		if (CanShoot)
		{
            var newBullet = Bullet.Instantiate<Bullet>();
            var muzzle = GetNode<Marker3D>("Muzzle");

            newBullet.GlobalTransform = muzzle.GlobalTransform;

            newBullet.Speed = MuzzleSpeed;

            GetTree().CurrentScene.AddChild(newBullet);

            CanShoot = false;
			RateOfFireTimer.Start();
        }
	}

	private void OnTimerTimeout()
	{
		CanShoot = true;
	}
}
