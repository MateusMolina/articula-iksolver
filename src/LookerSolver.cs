using System.Collections.Generic;
using Godot;

namespace Articula;

/// <summary>
/// My attempt of a IK Solver that makes a chain of bones look at a target defined by a LookerAimProvider.
/// </summary>
class LookerSolver : IKSolver
{
    public List<IKBone> Bones;

    private float tolerance;

    private float deltaMultiplier;

    public LookerAimProvider AimProvider { get; set; }

    public LookerSolver(
        IEnumerable<IKBone> bones,
        LookerAimProvider aimProvider,
        float deltaMultiplier = 5f,
        float tolerance = 0.1f
    )
    {
        Bones = new List<IKBone>(bones);
        this.deltaMultiplier = deltaMultiplier;
        this.tolerance = tolerance;
        AimProvider = aimProvider;
    }

    public void Solve(Vector3 target, float delta)
    {
        if (Bones.Count == 0)
            return;

        Quaternion requiredRotationToTarget = getRotationDiffTo(target);

        if (requiredRotationToTarget.LengthSquared() < tolerance)
            return;

        delta = delta * deltaMultiplier;

        ApplyForwardPass(requiredRotationToTarget, delta);
    }

    private void ApplyForwardPass(Quaternion requiredRotation, float delta)
    {
        foreach (IKBone bone in Bones)
        {
            if (bone.IsRigid)
                continue;
            bone.Rotation += requiredRotation * delta;
        }
    }

    private Quaternion getRotationDiffTo(Vector3 targetPos)
    {
        bool isModelOrientation = AimProvider.TargetPosition.Z > 0;

        var lookingBasis = AimProvider
            .GlobalTransform.LookingAt(targetPos, Vector3.Up, isModelOrientation)
            .Basis;

        return lookingBasis.GetRotationQuaternion().Normalized()
            - AimProvider.GlobalTransform.Basis.GetRotationQuaternion().Normalized();
    }
}
