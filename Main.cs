using Godot;
using System;
using System.Linq;

public partial class Main : Node3D
{
    public override void _PhysicsProcess(double delta)
    {
        var mousePosition = GetViewport().GetMousePosition();
        var camera = (Camera3D) GetNode("PlayerCamera");

        var rayOrigin = camera.ProjectRayOrigin(mousePosition);
        var rayTarget = rayOrigin + camera.ProjectRayNormal(mousePosition) * 2000;

        var spaceState = GetWorld3D().DirectSpaceState;
        var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayTarget);
        query.CollideWithAreas = true;
        var intersection = spaceState.IntersectRay(query);
        

        if (intersection.Count != 0)
        {
            var pos = (Vector3)intersection["position"];
            var player = (CharacterBody3D)GetNode("Player");

            var playerLookPos = new Vector3(pos.X, player.Position.Y, pos.Z);

            player.LookAt(playerLookPos, Vector3.Up);
        }
    }
}
