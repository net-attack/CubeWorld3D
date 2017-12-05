using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class PlayerActions : MonoBehaviour {
    public GameObject axtPrefab;
    public GameObject schaufelPrefab;

    private ToolList tools;

    public float MinVibration = 1;//the minimum vibration power
    public float MaxVibration = 1;//the maximum vibration power


    // Use this for initialization
    void Start()
    {
        tools = new ToolList(2, axtPrefab, schaufelPrefab);
    }

    // Update is called once per frame
    void Update()
    {

        

        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Debug.Log("increase");
            tools.IncTool();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            Debug.Log("decrease");
            tools.DecTool();
        }
        else if (Input.GetMouseButtonDown(0)) // backwards
        {
           
            axtPrefab.transform.eulerAngles = new Vector3(Random.Range(MinVibration, MaxVibration), Random.Range(MinVibration, MaxVibration), Random.Range(MinVibration, MaxVibration)); 

            Debug.Log("CLick");
        }


    }
    

    private class ToolList
    {
        private Tool currentTool;
        private int toolPosition;
        private GameObject axtPrefab;
        private GameObject schaufelPrefab;

        private int toolCount = 2;
        private List<Tool> tools;
        public ToolList(int length, GameObject axPr, GameObject scPr)
        {
            if (axPr == null || scPr == null) {
                throw new System.ArgumentException("Prefabs of Tools not loaded", "Src: ToolList");
            }
            else {
                axtPrefab = axPr;
                schaufelPrefab = scPr;

                if (length > toolCount)
                {
                    throw new System.ArgumentException("Length of ToolList to big", "Src: ToolList");
                }
                else
                {
                    tools = new List<Tool>();

                    for (int i = 0; i < length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                Axt axt = new Axt(axtPrefab);
                                tools.Add(axt);
                                Debug.Log("Set Axt as Tool");
                                break;
                            case 1:
                                Schaufel schaufel = new Schaufel(schaufelPrefab);
                                tools.Add(schaufel);
                                Debug.Log("Set Schaufel as Tool");
                                break;
                            default:
                                throw new System.ArgumentException("Length of ToolList to big", "Src: ToolList");
                        }
                    }
                }
            }
        }

        public void DecTool()
        {
            SetTool(false);
        }
        public void IncTool()
        {
            SetTool(true);
        }

        public Tool GetTool()
        {
            return currentTool;
        } 

        private void SetTool(bool direc)
        {
            
            int i = tools.IndexOf(currentTool);
            if (direc)
            {
                if (i == tools.Count - 1)
                {
                    currentTool = tools[0];
                }
                else
                {
                    currentTool = tools[i + 1];
                }
            }
            else
            {
                if (i == 0)
                {
                    currentTool = tools[tools.Count-1];
                }
                else
                {
                    currentTool = tools[ i - 1];
                }
            }

        }
    }


    public class Tool
    {
        private int strength;
        private int id = -1;

        public int getId()

        {
            return id;
        }


    }

    private class Axt : Tool
    {
        private GameObject axtPrefab;
        private int strength;
        private int id;

        public Axt(GameObject aP)
        {
            axtPrefab = aP;
        }

    }

    private class Schaufel : Tool
    {
        private GameObject schaufelPrefab;
        private int strength;
        private int id = 0;

        public Schaufel(GameObject sP)
        {
            schaufelPrefab = sP;
        }
    }

    
}
