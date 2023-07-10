
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public static class Utility
{   
    public static class BetterRandom
    {
        public static int WeightedRandom(float[] Weightes)
        {
            int Output = -1;
            float totalweight = 0;
            for (int i = 0; i < Weightes.Length; i++) totalweight += Weightes[i];
            float rndweightvalue = UnityEngine.Random.Range(0, totalweight);
            float processedweights = 0;
            for(int i = 0; i < Weightes.Length; i++)
            {
                processedweights += Weightes[i];
                if (rndweightvalue <= processedweights)
                {
                    Output = i;
                    break;
                }
            }

            return Output;
        }   
    }

    public static void Timer(this MonoBehaviour Script,Action Method,float Timer)
    {
        Script.StartCoroutine(ITimer(Method, Timer));
    }

    private static IEnumerator ITimer(Action Method, float Timer)
    {
        yield return new WaitForSeconds(Timer);
        Method.Invoke();
    }

    public static string FormatTime(int time)
    {
        int minutes = time / 60;
        int seconds = time % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static class WaitFor
    {
        public static IEnumerator Frames(int frameCount)
        {
            if (frameCount <= 0)
            {
                throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
            }

            while (frameCount > 0)
            {
                frameCount--;
                yield return null;
            }
        }
    }


    public static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    public static Vector3[] GetBoundPoints(Vector3 Point, Vector3 Size)
    {
        Vector3[] ReturnPoints = new Vector3[8];
        //UpRightForward
        ReturnPoints[0] = Point + Vector3.up * Size.y / 2 + Vector3.right * Size.x + Vector3.forward * Size.z;
        //UpLeftForward
        ReturnPoints[1] = Point + Vector3.up * Size.y / 2 - Vector3.right * Size.x / 2 + Vector3.forward * Size.z / 2;
        //UpRightBack 
        ReturnPoints[2] = Point + Vector3.up * Size.y / 2 + Vector3.right * Size.x / 2 - Vector3.forward * Size.z / 2;
        //UpLeftBack     
        ReturnPoints[3] = Point + Vector3.up * Size.y / 2 - Vector3.right * Size.x / 2 - Vector3.forward * Size.z / 2;
        //DownRightForward 
        ReturnPoints[4] = Point - Vector3.up * Size.y / 2 + Vector3.right * Size.x / 2 + Vector3.forward * Size.z / 2;
        //DownLeftForward
        ReturnPoints[5] = Point - Vector3.up * Size.y / 2 - Vector3.right * Size.x / 2 + Vector3.forward * Size.z / 2;
        //DownRightBack
        ReturnPoints[6] = Point - Vector3.up * Size.y + Vector3.right * Size.x / 2 - Vector3.forward * Size.z / 2;
        //DownLeftBack
        ReturnPoints[7] = Point - Vector3.up * Size.y / 2 - Vector3.right * Size.x / 2 - Vector3.forward * Size.z / 2;

        return ReturnPoints;
    }

    public static bool CanSee(this Transform transform, Vector3 Point, Vector3 Size)
    {
        Vector3[] boundPoints = GetBoundPoints(Point, Size);
        foreach (Vector3 point in boundPoints)
        {
            //Must all of the rays dont touch an other object

            Vector3 directionBetween = point - transform.position;
            directionBetween = directionBetween.normalized;

            float distance = Vector3.Distance(transform.position, point);
            if (Physics.Raycast(transform.position, directionBetween, distance + 0.05f))
            {
                return false;
            }
        }

        return true;
    }



    [System.Serializable]
    public struct SerializeDictionary<K, V>
    {
        public K[] Keys;
        public V[] Values;

        public SerializeDictionary(Dictionary<K, V> d)
        {
            Keys = new K[d.Count];
            Values = new V[d.Count];

            d.Keys.CopyTo(Keys, 0);
            d.Values.CopyTo(Values, 0);
        }

        public Dictionary<K, V> DeserializeDictionary()
        {
            Dictionary<K, V> d = new Dictionary<K, V>();
            for(int i = 0; i < Keys.Length; i++)
            {
                d.Add(Keys[i], Values[i]);
            }

            return d;
        }
    }

    public static DateTime GetNetworkTime()
    {
        const string ntpServer = "pool.ntp.org";
        var ntpData = new byte[48];
        ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

        var addresses = Dns.GetHostEntry(ntpServer).AddressList;
        var ipEndPoint = new IPEndPoint(addresses[0], 123);
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        socket.Connect(ipEndPoint);
        socket.Send(ntpData);
        socket.Receive(ntpData);
        socket.Close();

        ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
        ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

        var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
        var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

        return networkDateTime;
    }


}