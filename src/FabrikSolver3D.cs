using System.Collections.Generic;
using Godot;

namespace Articula;

class FabrikSolver3D : IKSolver
{
    public List<IKBone> Bones;

    private float tolerance;

    private float deltaMultiplier;

    public FabrikSolver3D(
        IEnumerable<IKBone> bones,
        float deltaMultiplier = 5f,
        float tolerance = 0.1f
    )
    {
        Bones = new List<IKBone>(bones);
        this.deltaMultiplier = deltaMultiplier;
        this.tolerance = tolerance;
    }

    public void Solve(Vector3 target, float delta)
    {
        if (Bones.Count == 0)
            return;

        var basePosition = Bones[0].Start;

        var tipBone = Bones[^1];

        if (tipBone.DistanceTo(target) <= tolerance)
            return;

        ApplyForwardPass(target, delta * deltaMultiplier);
        ApplyBackwardPass(basePosition);
    }

    private void ApplyForwardPass(Vector3 targetPos, float delta)
    {
        for (int i = Bones.Count - 1; i > 0; i--)
        {
            var bone = Bones[i];

            if (bone.IsRigid)
                continue;

            Vector3 direction = computeLerpedDirection(bone, targetPos, delta);

            bone.LocalEnd = direction * bone.Length;
        }
    }

    private void ApplyBackwardPass(Vector3 basePosition)
    {
        Bones[0].Start = basePosition;

        for (int i = 1; i < Bones.Count; i++)
        {
            var bone = Bones[i];

            if (bone.IsRigid)
                continue;

            bone.LocalEnd = bone.Direction() * bone.Length;
        }
    }

    private Vector3 computeLerpedDirection(IKBone bone, Vector3 targetPos, float delta)
    {
        var targetDirection = (targetPos - bone.Start).Normalized();
        return bone.Direction().Lerp(targetDirection, delta).Normalized();
    }
}
