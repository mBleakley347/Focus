using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RetargettingAddons
{
    public static bool SphereCastLimit(Vector3 origin, float radius, Vector3 direction, float maxAngle, out RaycastHit hitInfo,
        float maxDistance = Mathf.Infinity)
    {
        RaycastHit hit;
        bool temp = Physics.SphereCast(origin, radius, direction.normalized, out hit, maxDistance);
        if (!temp)
        {
            // did not hit anything
            hitInfo = hit;
            return false;
        }

        // if the hit normal is less than the max angle, it's the ground
        if (Vector3.Angle(direction,hit.normal) <= maxAngle)
        {
            //hit point is considered the ground
            hitInfo = hit;
            return true;
        }
        else //otherwise it is considered a wall
        {
            
            // tan is opposite over adjacent, opposite is the distance to cast for and adjacent is the radius
            // the angle to put in the tan should be the opposite of the angle between the hit normal and the max
            float maxdist = radius * Mathf.Tan(90 - (maxAngle + Vector3.Angle(direction, hit.normal))/2);

            // the direction along the plane normal towards the cast direction (using max angle, needs changing)
            Vector3 normaltowardsdirection = hit.normal + (direction.normalized/Mathf.Cos(Mathf.Deg2Rad*Vector3.Angle(-hit.normal,direction.normalized)));
            
            //make a raycast from the spherecast point down the normal to check if there is a point considered the ground contacted instead
            RaycastHit hit2;
            Physics.Raycast(hit.point, normaltowardsdirection, out hit2, maxdist);
            
            // now that we have a hit point, do something to find the actual ground

            Vector3 spheretopoint = -hit2.normal * radius;

            float hitdistance = (Vector3.Dot((hit2.point + (hit2.normal * radius)) - origin, hit2.normal)
                                 / Vector3.Dot(direction, hit2.normal));

            // 
            if (hitdistance <= maxDistance)
            {
                
            }
            // origin plus the direction multiplied by the distance along the direction that intersects the plane (the centre of the sphere)
            Vector3 spherehitcenter = origin+direction*hitdistance;

            //Mathf.Tan(Vector3.Angle(direction, -hit2.normal)) * radius;
            
            // this currently uses the old center of when it hit the wall which actually gives the old value, find out how much of the cast is left and use the blog method
            Vector3 spherecentre = hit.point + (hit.normal * radius);
            // in the case that the 
            Vector3 pointonnormal = Vector3.ProjectOnPlane(spherecentre-hit2.point, hit2.normal);
            if (Vector3.Distance(pointonnormal, spherecentre) <= radius)
            {
                hitInfo = hit2;
                hitInfo.point = pointonnormal;
                hitInfo.distance = hit.distance;
                return true;
            }
        }

        hitInfo = hit;

        return hit.collider;
    }

    public static void RetargetRaycastToSphere(this ref RaycastHit hit, Vector3 raycastOrigin, Vector3 spherecastDir, float newR, float originalR = 0, float tolerance = 0.01f)
    {
        float groundAngle = Vector3.Angle(hit.normal, -spherecastDir) * Mathf.Deg2Rad;
        float cos = 1/Mathf.Cos(groundAngle);
		
        // get the distance
        float hypOriginal = originalR*cos;
        float hypNew = newR*cos;
		
        // get the center of the original sphere from the hit normal and radius
        Vector3 originalSphereCenter = hit.point + hit.normal*originalR;
        
        // get an intermediate point down the line of the hit normal not in contact
        Vector3 pointOnSurface = originalSphereCenter + hypOriginal*spherecastDir;
        
        // go from the intermediate point to the new center
        Vector3 newSphereCentre = pointOnSurface - hypNew*spherecastDir;
		
        // set the distance to something that can be used to reconstruct other values since we know we wanted to use this function
        hit.distance = Vector3.Distance(raycastOrigin, newSphereCentre);
		
        // if the center is too close to the raycast origin, just say it's there
        if(hit.distance < Mathf.Pow(tolerance,2))
            hit.distance = 0;
		
        // set the new hit point, normal is not changed
        hit.point = newSphereCentre - hit.normal*newR;
    }
    
    public static Vector3 normalFromTriangle(this Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Vector3 Oneto2 = point1 - point2;
        Vector3 Twoto3 = point2 - point3;
        return Vector3.Cross(Oneto2,Twoto3);
    }
}