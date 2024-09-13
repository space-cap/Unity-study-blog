using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLib
{
    public static bool DetectCharacter(Camera sight, CharacterController cc)
    {
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }
    
    public static void RotateToTarget(Transform transform, Vector3 destination, CharacterStat stat)
    {
        // B-A는 A에서 B로 향하는 벡터.
        Vector3 dir = destination - transform.position;
        dir.y = 0.0f;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(dir),
                    stat.turnSpeed * Time.deltaTime
                );
        }
    }

    public static void MoveToPosition(CharacterController cc, Vector3 destination, CharacterStat stat)
    {
        Transform transform = cc.transform;

        RotateToTarget(transform, destination, stat);

        Vector3 deltaMove = Vector3.MoveTowards(
            transform.position,
            destination,
            stat.moveSpeed * Time.deltaTime) - transform.position;

        deltaMove.y = -stat.fallSpeed * Time.deltaTime;

        cc.Move(deltaMove);
    }

    public static void MoveToTransform(CharacterController cc, Transform target, CharacterStat stat)
    {
        Transform transform = cc.transform;
        MoveToPosition(cc, target.position, stat);
    }
}
