using Godot;

namespace Articula;

public interface IKBone
{
    /// <summary>
    ///     The start position of the bone in relation to the root origin.
    /// </summary>
    Vector3 Start { get; set; }

    /// <summary>
    ///    The end position of the bone in relation to the root origin.
    /// </summary>
    Vector3 End { get; set; }

    /// <summary>
    ///    The end position of the bone in the Bone Start Reference System.
    /// </summary>
    Vector3 LocalEnd { get; set; }
    Quaternion Rotation { get; set; }
    Transform3D GlobalTransform { get; set; }
    bool IsRigid { get; set; }
    public float Length { get; }
}

public static class IKBoneExtensions
{
    public static Vector3 Direction(this IKBone b) => (b.End - b.Start).Normalized();

    public static float DistanceTo(this IKBone b, Vector3 target) => b.End.DistanceTo(target);

    public static float DistanceTo(this IKBone b, IKBone bone) => b.End.DistanceTo(bone.Start);
}
