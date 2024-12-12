using Godot;

public partial class Player : Godot.CharacterBody3D
{
    private float CurrentSpeed = 15.0f;
    private const float ConstSpeed = 10.0f;
    private const float SprintSpeed = 15.0f;
    private const float JumpVelocity = 4.5f;

    private GunController GunController;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GunController = GetNode<GunController>("GunController");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Movement Logic
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y -= gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            velocity.Y = JumpVelocity;

        if (Input.IsActionPressed("sprint"))
        {
            CurrentSpeed = SprintSpeed;
        }
        else
        {
            CurrentSpeed = ConstSpeed;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("left", "right", "forward", "back");
        Vector3 direction = (new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * CurrentSpeed;
            velocity.Z = direction.Z * CurrentSpeed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, CurrentSpeed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, CurrentSpeed);
        }

        Velocity = velocity;
        MoveAndSlide();

        // Shoot Logic
        if (Input.IsActionPressed("LMB"))
        {
            GunController.Shoot();
        }
    }
}
