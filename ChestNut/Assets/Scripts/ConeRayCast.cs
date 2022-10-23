using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConeRayCast
{
    public static RaycastHit[] ConeCastAll(this Physics physics, Vector3 origin, float radius, Vector3 direction, float range, float coneAngle)
    {
        RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin - new Vector3(0, 0, radius), radius, direction, range);
        List<RaycastHit> coneCastHitList = new List<RaycastHit>();
        foreach (RaycastHit h in sphereCastHits) {
            if (Vector3.Angle(direction, h.point - origin) < coneAngle) {
                coneCastHitList.Add(h);
            }
        }    
        return coneCastHitList.ToArray();
    }

    public static RaycastHit[] FollowSpotCastAll(this Physics physics, Vector3 origin, Vector3 target, float radius)
    {
        RaycastHit[] coneCastHits = physics.ConeCastAll(origin, 0.1f, target - origin, Vector3.Distance(target, origin), 180f * radius / Mathf.PI / Vector3.Distance(target, origin));
        List<RaycastHit> followSpotCastHitList = new List<RaycastHit>();
        foreach (RaycastHit h in coneCastHits) {
            if (h.point.y + 0.5f >= target.y) {
                followSpotCastHitList.Add(h);
            }
        }    
        return followSpotCastHitList.ToArray();
    }
}
