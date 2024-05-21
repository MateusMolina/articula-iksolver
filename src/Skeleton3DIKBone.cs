using System.Collections;
using Godot;

namespace Articula;

public class Skeleton3DIKBone : IKBone
{
    public bool IsRigid { get; set; }
    private int EndNodeId;
    private int StartNodeId;
    private Skeleton3D Skeleton;

    public Vector3 Start
    {
        get => Skeleton.GetBoneGlobalPose(StartNodeId).Origin;
        set => Skeleton.SetBonePosePosition(StartNodeId, value - Start);
    }

    public Vector3 End
    {
        get => Skeleton.GetBoneGlobalPose(EndNodeId).Origin;
        set => Skeleton.SetBonePosePosition(EndNodeId, value - End);
    }
    public Vector3 LocalEnd
    {
        get => Skeleton.GetBonePosePosition(EndNodeId);
        set => Skeleton.SetBonePosePosition(EndNodeId, value);
    }
    public float Length { get; private set; }

    public Quaternion Rotation
    {
        get => Skeleton.GetBonePoseRotation(StartNodeId);
        set => Skeleton.SetBonePoseRotation(StartNodeId, value);
    }
    public Transform3D GlobalTransform
    {
        get => Skeleton.GetBoneGlobalPose(StartNodeId);
        set => Skeleton.SetBoneGlobalPoseOverride(StartNodeId, value, 1f, true);
    }

    public Skeleton3DIKBone(
        Skeleton3D skeleton,
        int startBoneId,
        int endNodeId,
        bool isRigid = false
    )
    {
        Skeleton = skeleton;
        StartNodeId = startBoneId;
        EndNodeId = endNodeId;
        IsRigid = isRigid;
        Length = Start.DistanceTo(End);
    }

    public static Skeleton3DIKBone FromSkeleton3D(
        Skeleton3D skeleton,
        int startNodeId,
        int endNodeId
    ) => new Skeleton3DIKBone(skeleton, startNodeId, endNodeId);
}

public static class Skeleton3DExtensions
{
    public static List<Skeleton3DIKBone> GetIKBones(this Skeleton3D skeleton, string[] boneLabels)
    {
        var bones = new List<Skeleton3DIKBone>();
        IEnumerator it = boneLabels.GetEnumerator();

        bool hasMoreElements = it.MoveNext();

        while (hasMoreElements)
        {
            var startBoneId = skeleton.FindBone(it.Current as string);
            hasMoreElements = it.MoveNext();

            int? endBoneId = hasMoreElements ? skeleton.FindBone(it.Current as string) : null;

            if (endBoneId != null)
                bones.Add(Skeleton3DIKBone.FromSkeleton3D(skeleton, startBoneId, endBoneId.Value));
        }

        return bones;
    }
}
