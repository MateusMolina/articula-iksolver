using Godot;

namespace Articula;

public class LookerAimProvider
{
    public Transform3D GlobalTransform => getGlobalTransform();

    public Vector3 TargetPosition => getTargetPosition();
    private readonly Func<Vector3> getTargetPosition;
    private readonly Func<Transform3D> getGlobalTransform;

    public LookerAimProvider(Func<Transform3D> getGlobalTransform, Func<Vector3> getTargetPosition)
    {
        this.getTargetPosition = getTargetPosition;
        this.getGlobalTransform = getGlobalTransform;
    }
}
