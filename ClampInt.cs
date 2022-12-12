using System;
using System.Threading;
using UnityEngine;
using ThreadPriority = System.Threading.ThreadPriority;

public unsafe interface IClampInt
{
    public void Accept(Type type, int* ptr, int value)
    {
        ClampInt MyAttribute = (ClampInt)Attribute.GetCustomAttribute(type, typeof(ClampInt));

        if (MyAttribute == null)
        {
            Debug.Log("The attribute was not found.");
        }
        else
        {
            MyAttribute.SetIntegerPointerAndValue(ptr, ref value);
        }
    }
}

[System.AttributeUsage(AttributeTargets.Class)]
public unsafe class ClampInt : System.Attribute
{
    private int* iPtr;
    private int value;
    private int millisecondsTimeout = 50;

    private void ThreadProc()
    {
        while (true)
        {
            Thread.Sleep(millisecondsTimeout);
            *iPtr = value;
        }
    }

    

    public ClampInt(int millisecondsTimeout)
    {
        this.millisecondsTimeout = millisecondsTimeout;
    }


    public void SetIntegerPointerAndValue(int* iPtr, ref int value)
    {
        this.iPtr = iPtr;
        this.value = value;
        Thread thread = new Thread(new ThreadStart(ThreadProc));
        thread.IsBackground = true;
        thread.Priority = ThreadPriority.Lowest;
        thread.Start();
    }
}