using Godot;
using System;

public partial class Node2D : Godot.Node2D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotate(GetLocalMousePosition().Angle());
	}
}
