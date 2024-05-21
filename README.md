# Articula IK Solver

Articula is an IK (Inverse Kinematic) Solver compatible with Godot 4.

My main motivation was to have an easy-to-use way of adding different IK algorithms to a game engine - at least for the moment, only Godot 4.

## Current Solvers

- FabricSolver3D: an implementation of the Fabric algorithm
- LookerSolver (also 3D): the idea is to make a chain of bones look at a target, useful for aiming  

## Usage

1. Define a set of IKBone (see Skeleton3DIKBone for genering them from a Skeleton3D);
2. Choose a solver and specify a target to solve for.

## ToDos

- [ ] Right now, almost every file has a dependency to the Godot SDK; however, there should be a clear separation of the Godot convenience layer and the core functionality.
- [ ] Document APIs and research parts. For example, I provided a Fabric algorithm implementation; however, this is what I think Fabric is, it could be totally wrong (please let me know if this is the case).
- [ ] Improve code - most importantly, the public API. The code in src just works, it's not nearly as clean as I would like.

## Contributing

Feel free to raise issues, submit PRs, or start new discussions. I'm here to learn and meet new people, so your feedback is always welcome :)
