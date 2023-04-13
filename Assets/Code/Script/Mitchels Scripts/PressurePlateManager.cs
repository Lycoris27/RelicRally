using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mitchel.traps
{
    public class PressurePlateManager : MonoBehaviour
    {
        [System.Serializable]
        public class Plate
        {
            [SerializeField] public GameObject plateGO;
            [SerializeField] public int id;
            [SerializeField] public bool triggered = false;
        }

        public List<Plate> plates;
        public List<int> targetSequence;
        public List<int> currentSequence;
        public GameObject wallGO;

        public void AddTriggeredPlate(int id)
        {
            if (plates[id - 1].triggered != true)
            {
                plates[id - 1].triggered = true;
                currentSequence.Add(id);
                plates[id - 1].plateGO.transform.Translate(0, -0.08f, 0);
            }
            CheckPlateTriggering();
        }

        private void CheckPlateTriggering()
        {
            if (currentSequence.Count != targetSequence.Count)
            {
                return;
            }

            Debug.Log("Checking triggering!");

            // compare the array of currentSequence against targetSequence
            bool inSequence = true;
            int n = 0;

            foreach (int i in currentSequence)
            {
                if (i != targetSequence[n])
                {
                    inSequence = false;
                }
                else
                {
                    n++;
                }
            }

            // if array is the same, call wall game object to trigger animation or disappear
            // TODO swap this code for triggering the animation on the WallGO
            if (inSequence)
            {
                Debug.Log("Plate sequence triggered!");
                Destroy(wallGO);
            }
            else
            {
                Debug.Log("Plate sequence not triggered!");
                currentSequence = new List<int>();
                foreach (Plate p in plates)
                {
                    p.triggered = false;
                    p.plateGO.transform.Translate(0, 0.08f, 0);
                }
            }
        }
    }



    //   public GameObject leftPlate;
    //   public GameObject middlePlate;
    //   public GameObject rightPlate;
    /*
    [Header ("Sequence Order")]
    [Range(1, 3)]
    public int firstPlateSequence;
    [Range(1, 3)]
    public int secondPlateSequence;
    [Range(1, 3)]
    public int thirdPlateSequence;
    */

    //   private GameObject plate1;
    //   private GameObject plate2;
    //   private GameObject plate3;


    /*
    switch (firstPlateSequence)
    {
        case 1:
        plate1 = leftPlate;
        //Debug.Log("Plate1 Option 1 Selected");
        break;
        case 2:
        plate1 = middlePlate;
        //Debug.Log("Plate1 Option 2 Selected");
        break;
        case 3:
        plate1 = rightPlate;
        //Debug.Log("Plate1 Option 3 Selected");
        break;
        default:
        Debug.Log("Plate1 Invalid Option Selected");
        break;
    }

    switch (secondPlateSequence)
    {
        case 1:
        plate2 = leftPlate;
        //Debug.Log("Plate2 Option 1 Selected");
        break;
        case 2:
        plate2 = middlePlate;
        //Debug.Log("Plate2 Option 2 Selected");
        break;
        case 3:
        plate2 = rightPlate;
        //Debug.Log("Plate2 Option 3 Selected");
        break;
        default:
        Debug.Log("Plate2 Invalid Option Selected");
        break;
    }

    switch (thirdPlateSequence)
    {
        case 1:
        plate3 = leftPlate;
        //Debug.Log("Plate3 Option 1 Selected");
        break;
        case 2:
        plate3 = middlePlate;
        //Debug.Log("Plate3 Option 2 Selected");
        break;
        case 3:
        plate3 = rightPlate;
        //Debug.Log("Plate3 Option 3 Selected");
        break;
        default:
        Debug.Log("Plate3 Invalid Option Selected");
        break;
    }

    Debug.Log("Plate1 = " + plate1 + ", Plate2 = " + plate2 + ", Plate3 = " + plate3);
    */
}