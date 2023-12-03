using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameRTSController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;

    public GameObject Click_Arrow;

    private Vector3 start_position;
    private List<UnitRTS> selectedUnitRTSList;

    private void Awake()
    {
        selectedUnitRTSList = new List<UnitRTS>();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Left Button Pressed
            selectionAreaTransform.gameObject.SetActive(true);
            start_position = UtilsClass.GetMouseWorldPosition();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();

            Vector3 lowerLeft = new Vector3(Mathf.Min(start_position.x, currentMousePosition.x), Mathf.Min(start_position.y, currentMousePosition.y));
            Vector3 upperRight = new Vector3(Mathf.Max(start_position.x, currentMousePosition.x), Mathf.Max(start_position.y, currentMousePosition.y));

            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        if ( Input.GetMouseButtonUp(0)) 
        {
            selectionAreaTransform.gameObject.SetActive(false);

            // Left Button Released
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(start_position, UtilsClass.GetMouseWorldPosition());

            //Deselect all objects
            foreach(UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.SetSelectedVisible(false);
            }
            selectedUnitRTSList.Clear();

            //goes in the array of objects that got raycasted in and add them to the list
            foreach(Collider2D collider2d in collider2DArray) 
            {
                UnitRTS unitRTS = collider2d.GetComponent<UnitRTS>();
                if (unitRTS != null)
                {
                    unitRTS.SetSelectedVisible(true);
                    selectedUnitRTSList.Add(unitRTS);
                }
            }

            // Create a temporary list to store non-null units
            List<UnitRTS> nonNullUnits = new List<UnitRTS>();

            foreach (Collider2D collider2d in collider2DArray)
            {
                UnitRTS unitRTS = collider2d.GetComponent<UnitRTS>();
                if (unitRTS != null)
                {
                    unitRTS.SetSelectedVisible(true);
                    nonNullUnits.Add(unitRTS);
                }
            }

            // Replace the original list with the non-null list
            selectedUnitRTSList = nonNullUnits;

        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition();
            //retine unde vrei sa ajunga jucatorul la pozitia mouse ului de click dreapta

            GameObject arrow = Instantiate(Click_Arrow, moveToPosition, Quaternion.identity);
            Destroy(arrow, 0.4f);


            List<Vector3> targetPositionList = new List<Vector3>()
            {
                moveToPosition + new Vector3(0,0),
                moveToPosition + new Vector3(1, 0),
                moveToPosition + new Vector3(2, 0),
                moveToPosition + new Vector3(0, 1),
                moveToPosition + new Vector3(1, 1),
                moveToPosition + new Vector3(2, 1),
                moveToPosition + new Vector3(0, 2),
                moveToPosition + new Vector3(1, 2),
                moveToPosition + new Vector3(2, 2),
            };
            //genereaza o lista de pozitii offsetate in jurul targetului
            //problema curenta e faptul ca daca sunt 5 soldati al 5 lea sta pe pozitia 1

            int targetPositionListIndex = 0;
            //clar incepi lista de la index 0

            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                //Aici muta jucatorii in pozitiile din lista de pozitii ca sa nu dea overlap
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
                //muta pozitia de index pentru urmatorul
            }

            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                // Check if the unit is not null before accessing it
                if (unitRTS != null)
                {
                    unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                    // ... existing code ...
                }
            }
        }
    }
}
