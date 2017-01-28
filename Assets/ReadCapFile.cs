using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Reflection;
using libsvm;

public class ReadCapFile : MonoBehaviour
{
    private string m_path;
    private string m_filename;
    private long index;

    FileInfo t_info;
    StreamReader t_reader = null;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;
    public GameObject s5;
    public GameObject s6;
    public GameObject s7;
    public GameObject SimKey;

    //svm method
    //private svm_model m_model1;
    //private svm_model m_model2;
    //private svm_model m_model3;
    //private svm_model m_model4;

    bool m_initialed;
    private int counting;
    private int countdown;
    private string state;
    private string state2;
    private string state3;
    private string state4;
    private string last_state;
    private string last_state2;
    private string last_state3;
    private string last_state4;
    private ArrayList window1_last;
    private ArrayList window2_last;
    private ArrayList window3_last;
    private ArrayList window4_last;
    private int keep1;
    private int keep2;
    private int keep3;
    private int keep4;
    private bool state4_reset;

    Queue q1 = new Queue();
    Queue q2 = new Queue();
    Queue q3 = new Queue();
    Queue q4 = new Queue();

    // Use this for initialization
    void Start()
    {
        m_path = "E:\\My Projects\\MotionControl\\Motrol\\Assets";
        m_filename = "Action1.txt";
        m_initialed = false;

        //svm method
        //m_model1 = readModel("E:\\My Projects\\MotionControl\\Motrol\\Assets\\myModel1.mdl");
        //m_model2 = readModel("E:\\My Projects\\MotionControl\\Motrol\\Assets\\myModel2.mdl");
        //m_model3 = readModel("E:\\My Projects\\MotionControl\\Motrol\\Assets\\myModel3.mdl");
        //m_model4 = readModel("E:\\My Projects\\MotionControl\\Motrol\\Assets\\myModel4.mdl");

        counting = 0;
        countdown = 0;

        last_state = "";
        last_state2 = "";
        last_state3 = "";
        last_state4 = "";

        keep1 = 0;
        keep2 = 0;
        keep3 = 0;
        keep4 = 0;

        state4_reset = true;

        for (int a = 0; a < 6; a++)
        {
            q1.Enqueue("");
            q2.Enqueue("");
            q3.Enqueue("");
            q4.Enqueue("");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (t_reader == null) return;
        else if (t_reader.EndOfStream) return;


        string t_line;

        if ((t_line = t_reader.ReadLine()) != null)
        {
            string[] sArray = t_line.Split('	');

            if (!m_initialed)
            {
                s1.transform.position = new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2]));
                s2.transform.position = new Vector3(float.Parse(sArray[5]), float.Parse(sArray[6]), float.Parse(sArray[7]));
                s3.transform.position = new Vector3(float.Parse(sArray[10]), float.Parse(sArray[11]), float.Parse(sArray[12]));
                s4.transform.position = new Vector3(float.Parse(sArray[15]), float.Parse(sArray[16]), float.Parse(sArray[17]));
                s5.transform.position = new Vector3(float.Parse(sArray[20]), float.Parse(sArray[21]), float.Parse(sArray[22]));
                s6.transform.position = new Vector3(float.Parse(sArray[25]), float.Parse(sArray[26]), float.Parse(sArray[27]));
                s7.transform.position = new Vector3(float.Parse(sArray[30]), float.Parse(sArray[31]), float.Parse(sArray[32]));
                m_initialed = true;
            }
            else
            {
                List<Vector3> points = new List<Vector3>();
                points.Add(new Vector3(float.Parse(sArray[0]), float.Parse(sArray[1]), float.Parse(sArray[2])));
                points.Add(new Vector3(float.Parse(sArray[5]), float.Parse(sArray[6]), float.Parse(sArray[7])));
                points.Add(new Vector3(float.Parse(sArray[10]), float.Parse(sArray[11]), float.Parse(sArray[12])));
                points.Add(new Vector3(float.Parse(sArray[15]), float.Parse(sArray[16]), float.Parse(sArray[17])));
                points.Add(new Vector3(float.Parse(sArray[20]), float.Parse(sArray[21]), float.Parse(sArray[22])));
                points.Add(new Vector3(float.Parse(sArray[25]), float.Parse(sArray[26]), float.Parse(sArray[27])));
                points.Add(new Vector3(float.Parse(sArray[30]), float.Parse(sArray[31]), float.Parse(sArray[32])));

                double d = 10000;
                Vector3 min = Vector3.zero;
                foreach (Vector3 vec in points)
                {
                    double temp = (s1.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s1.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s1.transform.position = min;

                d = 10000;
                foreach (Vector3 vec in points)
                {
                    double temp = (s2.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s2.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s2.transform.position = min;


                d = 10000;
                foreach (Vector3 vec in points)
                {
                    double temp = (s3.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s3.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s3.transform.position = min;


                d = 10000;
                foreach (Vector3 vec in points)
                {
                    double temp = (s4.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s4.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s4.transform.position = min;

                d = 10000;
                foreach (Vector3 vec in points)
                {
                    double temp = (s5.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s5.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s5.transform.position = min;

                d = 10000;
                foreach (Vector3 vec in points)
                {
                    double temp = (s6.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s6.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s6.transform.position = min;

                d = 10000;
                foreach (Vector3 vec in points)
                {
                    double temp = (s7.transform.position - vec).magnitude;
                    if (temp < d)
                    {
                        d = (s7.transform.position - vec).magnitude;
                        min = vec;
                    }
                }
                s7.transform.position = min;

                double d1 = double.Parse((s5.transform.position - s7.transform.position).sqrMagnitude.ToString());
                double d2 = double.Parse((s6.transform.position - s3.transform.position).sqrMagnitude.ToString());
                double d3 = double.Parse((s5.transform.position - s6.transform.position).sqrMagnitude.ToString());

                s2.GetComponent<Renderer>().material.color = new Color((float)d1 / 0.5f, 0.0f,0.0f);
                s4.GetComponent<Renderer>().material.color = new Color((float)d2 / 0.5f, 0.0f, 0.0f);
                s5.GetComponent<Renderer>().material.color = new Color( 0.0f , (float)s5.transform.position.y / 2.0f, 0.0f);
                s6.GetComponent<Renderer>().material.color = new Color(0.0f, (float)s6.transform.position.y / 2.0f, 0.0f);

                state = "";
                state2 = "";
                state3 = "";
                state4 = "";
                if ((s5.transform.position.y - s7.transform.position.y) > -0.1 && (s6.transform.position.y - s3.transform.position.y) > -0.1)
                {
                    if (d1 < 0.15 && d2 < 0.15 && d3<0.15)
                        state = "ready";//stretch
                    else if (d1 > 0.25 && d2 > 0.25 && (s5.transform.position.y - s7.transform.position.y) > 0.2 && (s5.transform.position.y - s7.transform.position.y) > 0.2 && d3 < 0.15)
                        state = "shoot";

                }
                else if (s5.transform.position.y < 1.4 && s5.transform.position.y > 1 && s6.transform.position.y < 1.4 && s6.transform.position.y > 1)
                {
                    if (d1 > 0.3 && d2 > 0.3 && d3<0.15)
                        state2 = "catch";
                    else if (d1 < 0.2 && d2 < 0.2 && d3 < 0.15)
                        state2 = "hold";
                }

                if ((s5.transform.position.y - s7.transform.position.y) > 0.1 && (s6.transform.position.y - s3.transform.position.y) < 0)
                {
                    state3 = "HandUp";
                }
                else if((s5.transform.position.y - s7.transform.position.y) < -0.1 && (s6.transform.position.y - s3.transform.position.y) <0)
                {
                    state3 = "HandDown";
                }
                if((s5.transform.position.y - s7.transform.position.y) < -0.2 && s1.transform.position.x < 0.1)
                {
                    if (d1 < 0.15)
                        state4 = "Fancy1";
                    if (d1 > 0.25)
                        state4 = "Fancy2";
                }


                //svm method

                /*
                string result1 = svm.svm_predict(m_model1, (s5.transform.position.y - s7.transform.position.y).ToString()+" "+ (s6.transform.position.y - s3.transform.position.y).ToString() + " " + d1.ToString() + " " + d2.ToString()+ " " + d3.ToString());
                switch (int.Parse(result1))
                {
                    case 0: state = "ready"; break;
                    case 1: state = "shoot"; break;
                    default: state = ""; break;
                }

                string result2 = svm.svm_predict(m_model2, (s5.transform.position.y.ToString() + " " + s6.transform.position.y.ToString() + " " + d1.ToString() + " " + d2.ToString() + " " + d3.ToString());
                switch (int.Parse(result2))
                {
                    case 0: state2 = "catch"; break;
                    case 1: state2 = "hold"; break;
                    default: state2 = ""; break;
                }

                string result3 = svm.svm_predict(m_model3, (s5.transform.position.y - s7.transform.position.y).ToString() + " " + (s6.transform.position.y - s3.transform.position.y).ToString());
                switch (int.Parse(result3))
                {
                    case 0: state3 = "HandUp"; break;
                    case 1: state3 = "HandDown"; break;
                    default: state3 = ""; break;
                }

                string result4 = svm.svm_predict(m_model4, ((s5.transform.position.y - s7.transform.position.y).ToString() + " " + s1.transform.position.x.ToString());
                switch (int.Parse(result4))
                {
                    case 0: state4 = "Fancy1"; break;
                    case 1: state4 = "Fancy2"; break;
                    default: state4 = ""; break;
                }
                */


                if (last_state != state || keep1 >50)
                {
                    keep1 = 0;
                    q1.Enqueue(state);
                    q1.Dequeue();
                    last_state = state;
                }
                else
                {
                    keep1++;
                }

                if (last_state2 != state2 || keep2 > 50)
                {
                    keep2 = 0;
                    q2.Enqueue(state2);
                    q2.Dequeue();
                    last_state2 = state2;
                }
                else
                {
                    keep2++;
                }

                if (last_state3 != state3 || keep3 > 50)
                {
                    keep3 = 0;
                    q3.Enqueue(state3);
                    q3.Dequeue();
                    last_state3 = state3;
                }
                else
                {
                    keep3++;
                }

                if (last_state4 != state4 || keep4 > 50)
                {
                    keep4 = 0;
                    q4.Enqueue(state4);
                    q4.Dequeue();
                    last_state4 = state4;
                }
                else
                {
                    keep4++;
                }
            }


        }
        else
        {
            t_reader.Close();
            t_reader.Dispose();
        }
    }

    //svm method
    /*
    private void readModel(String filename)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        m_model = (svm_model)formatter.Deserialize(stream);
        stream.Close();

    }
    */

    float angle_360(Vector3 from_, Vector3 to_)
    {
        Vector3 v3 = Vector3.Cross(from_, to_);
        if (v3.z > 0)
            return Vector3.Angle(from_, to_);
        else
            return 360 - Vector3.Angle(from_, to_);
    }

    float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }

    private void OnGUI()
    {
        GUI.skin.label.normal.textColor = new Color(0, 0, 0);

        // Find file path and open file
        if (GUI.Button(new Rect(20, 20, 160, 20), "Open Cap File"))
        {
            OpenFileName ofn = new OpenFileName();

            ofn.structSize = Marshal.SizeOf(ofn);

            ofn.filter = "All Files\0*.*\0\0";

            ofn.file = new string(new char[256]);

            ofn.maxFile = ofn.file.Length;

            ofn.fileTitle = new string(new char[64]);

            ofn.maxFileTitle = ofn.fileTitle.Length;
            string path = Application.streamingAssetsPath;
            path = path.Replace('/', '\\');
            ofn.initialDir = m_path;
            ofn.title = "Open Project";
            ofn.defExt = "txt";
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  

            if (WindowDll.GetOpenFileName(ofn))
            {
                if (t_reader != null)
                {
                    t_reader.Close();
                    t_reader.Dispose();
                }
                index = 0;

                t_info = new FileInfo(ofn.file);
                if (!t_info.Exists)
                {
                    print("File not found");
                }
                else
                {

                    t_reader = null;
                    try
                    {
                        t_reader = File.OpenText(ofn.file);
                        m_initialed = false;
                    }
                    catch
                    {
                        print("Read error!");
                        return;
                    }


                }
            }
        }

        GUI.Label(new Rect(20, 0, 160, 20),state);
        GUI.Label(new Rect(50, 0, 160, 20), state2);
        GUI.Label(new Rect(80, 0, 160, 20), state3);
        GUI.Label(new Rect(150, 0, 160, 20), state4);

        int pos = 160;
        int count = 0;
        ArrayList window1 = new ArrayList();
        
        foreach (string str in q1)
        {
            //GUI.Label(new Rect(20, pos + count * 20, 160, 20), str);
            window1.Add(str);
            count++;
        }
        if(window1[5].Equals("shoot"))
        {
            if(window1.Contains("ready"))
            {
                GUI.Label(new Rect(20, 100, 160, 20), "Press shoot");
                if(!window1.Equals(window1_last))
                    SimKey.SendMessage("ReceiveControl", "Shoot", SendMessageOptions.DontRequireReceiver);
            }
        }
        if (window1[3].Equals("ready"))
        {
            if (!window1.Contains("shoot"))
            {
                GUI.Label(new Rect(20, 100, 160, 20), "Press fake");
                if (!window1.Equals(window1_last))
                    SimKey.SendMessage("ReceiveControl", "Fake", SendMessageOptions.DontRequireReceiver);
            }

        }
        window1_last = window1;


        pos = 160;
        count = 0;
        ArrayList window2 = new ArrayList();
        foreach (string str in q2)
        {
            //GUI.Label(new Rect(50, pos + count * 20, 160, 20), str);
            window2.Add(str);
            count++;
        }
        if (window2[3].Equals("catch"))
        {
            if (window2[4].Equals("hold")|| window2[5].Equals("hold"))
            {
                GUI.Label(new Rect(20, 120, 160, 20), "Press pass");
                if (!window2.Equals(window2_last))
                    SimKey.SendMessage("ReceiveControl", "Pass", SendMessageOptions.DontRequireReceiver);
            }

        }
        window2_last = window2;


        pos = 160;
        count = 0;
        ArrayList window3 = new ArrayList();
        foreach (string str in q3)
        {
            //GUI.Label(new Rect(80, pos + count * 20, 160, 20), str);
            window3.Add(str);
            count++;
        }
        if (window3[3].Equals("HandUp"))
        {
            if (window3[4].Equals("HandDown") || window3[5].Equals("HandDown") && (!window1.Contains("shoot")))
            {
                GUI.Label(new Rect(20, 150, 160, 20), "Press block");
                if(!window3.Equals(window3_last))
                SimKey.SendMessage("ReceiveControl", "Block", SendMessageOptions.DontRequireReceiver);
            }

        }
        window3_last = window3;


        pos = 160;
        count = 0;
        ArrayList window4 = new ArrayList();
        foreach (string str in q4)
        {
            //GUI.Label(new Rect(150, pos + count * 20, 160, 20), str);
            window4.Add(str);
            count++;
        }
        if (window4[3].Equals("Fancy1"))
        {
            if (window4.Contains("Fancy2"))
            {
                GUI.Label(new Rect(20, 180, 160, 20), "Press fancy");
                if (state4_reset)
                {
                    state4_reset = false;
                    if(!window4.Equals(window4_last))
                    SimKey.SendMessage("ReceiveControl", "Fancy", SendMessageOptions.DontRequireReceiver);
                }
            }

        }
        else if (!(window4.Contains("Fancy1") || window4.Contains("Fancy2")))
        {
            state4_reset = true;
        }
        window4_last = window4;


    }

    bool ArrayEquals(ArrayList array1, ArrayList array2)
    {
        if (array1.Count != array2.Count) return false;
        for (int i = 0; i < array1.Count; i++)
            if (array1[i] != array2[i])
                return false;
        return true;
    }



}
