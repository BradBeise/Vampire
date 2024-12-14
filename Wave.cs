using Godot;
using System;

public partial class Wave : Node
{
    [Export]
    public int NumEnemies = 3;
    [Export]
	public double SecBetweenSpawn = 2;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
