using Godot;
using System;

public partial class Level : Node3D
{
	[Export]
    public PackedScene EnemyScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		enemy levelEnemy = EnemyScene.Instantiate<enemy>();

		var spawnLocation = GetNode<Marker3D>("EnemySpawn").Position;

		var playerPosition = GetNode<Player>("Player").Position;

		levelEnemy.Initialize(spawnLocation, playerPosition);

		AddChild(levelEnemy);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
