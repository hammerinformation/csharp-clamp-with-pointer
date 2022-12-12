using UnityEngine;


[ClampInt(millisecondsTimeout: 10)]
public class Example2 : MonoBehaviour, IClampInt
{
    [SerializeField] private int number;
    [SerializeField] private int number2;


    private void Start()
    {
        unsafe
        {
            fixed (int* p = &this.number)
            {
                ((IClampInt)this).Accept(typeof(Example2), p, 480);
            }

            fixed (int* p = &this.number2)
            {
                ((IClampInt)this).Accept(typeof(Example2), p, 980);
            }
        }
    }
}