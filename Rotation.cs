using Godot;
using System;

public partial class Rotation : Marker3D
{
    Camera3D camera;
    Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        camera = (Camera3D)GetNode("PlayerCamera");
        player = (Player)GetNode("../Player");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        var mousePosition = GetViewport().GetMousePosition();

        var rayOrigin = camera.ProjectRayOrigin(mousePosition);
        var rayTarget = rayOrigin + camera.ProjectRayNormal(mousePosition) * 2000;

        var spaceState = GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayTarget);
        query.CollideWithAreas = true;
        var intersection = spaceState.IntersectRay(query);


        if (intersection.Count != 0)
        {
            var pos = (Vector3)intersection["position"];

            var playerLookPos = new Vector3(pos.X, player.Position.Y, pos.Z);

            player.LookAt(playerLookPos, Vector3.Up);
        }

        camera.Position = player.Position + new Vector3(0, 10, 2);
    }
}
