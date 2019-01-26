using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float CheckDistance(Vector3 point1, Vector3 point2)
    {
        Vector3 heading;
        float distance;
        Vector3 direction;
        float distanceSquared;

        heading.x = point1.x - point2.x;
        heading.y = point1.y - point2.y;
        heading.z = point1.z - point2.z;

        distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
        distance = Mathf.Sqrt(distanceSquared);

        direction.x = heading.x / distance;
        direction.y = heading.y / distance;
        direction.z = heading.z / distance;
        return distance;
    }

    public static float CheckDistance(Vector2 point1, Vector2 point2)
    {
        Vector2 heading;
        float distance;
        Vector2 direction;
        float distanceSquared;

        heading.x = point1.x - point2.x;
        heading.y = point1.y - point2.y;

        distanceSquared = heading.x * heading.x + heading.y * heading.y;
        distance = Mathf.Sqrt(distanceSquared);

        direction.x = heading.x / distance;
        direction.y = heading.y / distance;
        return distance;
    }

    public static Vector3 Clamp(Vector3 vector, float clampValue)
    {
        return new Vector3(Mathf.Clamp(vector.x, -clampValue, clampValue), Mathf.Clamp(vector.y, -clampValue, clampValue), Mathf.Clamp(vector.z, -clampValue, clampValue));
    }

    public static Vector3 Clamp(Vector3 vector, float minClampValue, float maxClampValue)
    {
        return new Vector3(Mathf.Clamp(vector.x, minClampValue, maxClampValue), Mathf.Clamp(vector.y, minClampValue, maxClampValue), Mathf.Clamp(vector.z, minClampValue, maxClampValue));
    }

    public static Transform GetClosestEnemy(Collider[] enemies, Vector3 currentPos)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Collider col in enemies)
        {
            float dist = CheckDistance(col.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = col.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    public static T GetRandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> GetRandomItems<T>(this List<T> list, int amount)
    {
        List<T> origList = new List<T>();
        foreach (T item in list)
            origList.Add(item);

        List<T> tempList = new List<T>();

        for (int i = amount; i > 0; i--)
        {
            T t = GetRandomItem(origList);
            tempList.Add(t);
            origList.Remove(t);
        }

        return tempList;
    }
}
