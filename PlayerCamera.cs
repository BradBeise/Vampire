using Godot;
using System;

public partial class PlayerCamera : Camera3D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var player = (CharacterBody3D) GetNode("../Player");

		Position = player.Position + new Vector3(0, 10, 2);
    }
}
