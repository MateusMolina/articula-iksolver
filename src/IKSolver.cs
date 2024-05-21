using Godot;

namespace Articula;

public interface IKSolver
{
    void Solve(Vector3 target, float delta);
}
