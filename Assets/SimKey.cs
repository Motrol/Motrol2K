using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;



public class SimKey : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    public static extern void keybd_event(
            byte bVk,    //virtual key value
            byte bScan,// scan code 
            int dwFlags,  //action typw
            int dwExtraInfo  // extra info
    );

    [DllImport("user32.dll")]
    static extern byte MapVirtualKey(byte wCode, int wMap);
    
    private bool isBusy;

    // Use this for initialization
    void Start()
    {
        isBusy = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReceiveControl(string s)

    {
        switch(s)
        {
            case "Shoot": Shoot(); break;
            case "Fake": Fake(); break;
            case "Pass": Pass(); break;
            case "Block": Block(); break;
            case "Fancy": Fancy(); break;
        }
    }
       

    void Shoot()  
    {
        if (!isBusy)
        {
            //keybd_event(32, MapVirtualKey(32, 0), 0, 0);
            //System.Threading.Thread.Sleep(100);
            //keybd_event(32, MapVirtualKey(32, 0), 0x0002, 0);
            InvokeRepeating("PressShoot",0.0f, 10.0f);  // Invoke immediately and repeat every 2 seconds
            //  InvokeRepeating("ReleaseShoot", 0, 4.0f);  // Invoke immediately and repeat every 2 seconds
        }
    }

    void Fake()
    {
        if (!isBusy)
        {
            InvokeRepeating("PressFake", 0.0f, 10.0f);
        }
    }

    void Pass()
    {
        keybd_event((byte)'K', MapVirtualKey((byte)'K', 0), 0, 0);
        keybd_event((byte)'K', MapVirtualKey((byte)'K', 0), 0x0002, 0);
    }

    void Block()
    {
        keybd_event((byte)'B', MapVirtualKey((byte)'B', 0), 0, 0);
        keybd_event((byte)'B', MapVirtualKey((byte)'B', 0), 0x0002, 0);
    }

    void Fancy()
    {
        keybd_event((byte)'Q', MapVirtualKey((byte)'Q', 0), 0, 0);
        keybd_event((byte)'Q', MapVirtualKey((byte)'Q', 0), 0x0002, 0);
    }

    void PressShoot()
    {
        isBusy = true;
        keybd_event(32, MapVirtualKey(32, 0), 0, 0);
        CancelInvoke();
        InvokeRepeating("ReleaseShoot", 3.0f, 10.0f);
    }

    void ReleaseShoot()
    {
        keybd_event(32, MapVirtualKey(32, 0), 0x0002, 0);
        CancelInvoke();
        isBusy = false;
    }

    void PressFake()
    {
        isBusy = true;
        keybd_event(32, MapVirtualKey(32, 0), 0, 0);
        CancelInvoke();
        InvokeRepeating("ReleaseFake", 1.8f, 10.0f);
    }

    void ReleaseFake()
    {
        keybd_event(32, MapVirtualKey(32, 0), 0x0002, 0);
        CancelInvoke();
        isBusy = false;
    }

    /*
    void LaunchProjectile()
    {
        if (counting == 0)
        {
            SimKey.SendMessage("ReceiveControl", "push", SendMessageOptions.DontRequireReceiver);
            counting = 1;
        }
        else if (counting == 1)
        {
            SimKey.SendMessage("ReceiveControl", "release", SendMessageOptions.DontRequireReceiver);
            counting = 0;
            //CancelInvoke();
        }
    }
    */

}