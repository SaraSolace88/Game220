using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //needed for editor specific functions and API
using Unity.VisualScripting;
using static System.Net.WebRequestMethods;

[ExecuteInEditMode] //allows scripts to run in the Editor
public class Sample : MonoBehaviour //all monobehavior scripts can be attached to gameObjects
{
                      //The name of the menu item, name of the function to execute
    [ContextMenuItem("CustomMenuItem", "CustomContextMenuFunction")]
    public int wholeNumber;
    public float decimalNumber;
    public string sentence;
    public bool bTrueFalse;
    public Color color;

    //colorUsage enablesHDR,enablesAlpha you can set either to true or false
    [ColorUsage(true, true)]
    public Color hdrColorWithAlpha;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Vector3 prefabSpawnLocation = Vector3.zero;
    [Range(1, 10f)]
    public float rayLength = 1;

    public int[] arrayOfWholeNumbers;
    public List<int> listOfWholeNumbers; //if this is private, the list needs to be declared as 
                                         // public List<int> listOfWholeNumbers = new List<int>();
    private int hiddenNumber;
    protected int classOnlyNumber; //visible only to class and its members

    private Transform localTransform; //transform variables gives access to position, rotation, scale and to any attached component

    [SerializeField]//makes private and protected variables visible in the inspector
    private string privateString;
    [SerializeField]
    private ComponentType componentType;
    [SerializeField]
    private bool bDrawRay;

    private Vector3 start = Vector3.zero;
    private Vector3 end = Vector3.forward;
    private float rate;
    private bool bMove;
    private void Awake()
    {
        localTransform = transform; //assigns transform to localTransform
        //first function to run on a script and runs only once
    }
    private void OnEnable()
    {
        //runs after Awake
    }
    private void Start()
    {
        //runs after OnEnable but only once
    }
    //directions
    /*
     * new Vector3(0,1,0)  = Vector3.up
     * new Vector3(1,0,0) = Vector3.right
     * new Vector3(0,0,1) = Vector3.forward
     */

    private void Update()
    {
        if (bMove)
        {        
            //called every frame
            rate += Time.deltaTime; //increments rate by frame time
            localTransform.position = Vector3.Lerp(start, end, rate); //linearly blends between start and end at a rate between 0-1 using rate
        }

        if(bDrawRay) 
        {
            Debug.DrawRay(localTransform.position, localTransform.up * rayLength); //draws a ray using the localTransfrom.up
                        //because localTransform.up magnitude is 1 by default, multiply the direction by any number sets the length
        }
    }
    private void FixedUpdate()
    {
        //called every fixed update that is set under Time in Project Settings
    }

    public void ButtonFunction() //this function is made public so it can be called by the button created below
    {
        Debug.Log("Sample button pressed.");
    }

    private void CustomContextMenuFunction()
    {
        Debug.Log("Custom context menu function used.");
    }
    public void SelectItems()
    {
        List<GameObject> listOfItemsToSelect = new List<GameObject>();

        if (componentType == ComponentType.Collider)
        {
            Collider[] listOfColliders = FindObjectsOfType<Collider>();
            foreach(Collider collider in listOfColliders)
                listOfItemsToSelect.Add(collider.gameObject); //adds the collider's gameObject to the list
        }
        else if (componentType == ComponentType.Collider)
        {
            Rigidbody[] listOfRigidBody = FindObjectsOfType<Rigidbody>();
            foreach (Rigidbody rBody in listOfRigidBody)
                listOfItemsToSelect.Add(rBody.gameObject); //adds the rigidBody's gameObject to the list
        }
        else if (componentType == ComponentType.MeshRenderer)
        {
            MeshRenderer[] listOfMeshRenderers = FindObjectsOfType<MeshRenderer>();
            foreach (MeshRenderer mRenderer in listOfMeshRenderers)
                listOfItemsToSelect.Add(mRenderer.gameObject); //adds the rigidBody's gameObject to the list
        }

        //selects items in listOfItemsToSelect list
        Selection.objects = listOfItemsToSelect.ToArray();
    }

    public void DrawRaycast()
    {
        bDrawRay = true;

        //Physics.Raycast can be used to check for colliders by casting a ray in a specific direction
        //There are several overloads for this function and more information can be found at https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        //To query the hit information use RaycastHit variable more information can be found at https://docs.unity3d.com/ScriptReference/RaycastHit.html
        //Physics.Raycast has many options. Use the resources to determine what is needed for your specific application
    }

    public void SpawnPrefab()
    {
        if(prefab != null) 
        {
            Debug.LogError("No prefab has been set. Please assign a prefab before spawning.");
            return; //if no prefab is assigned, the return exits the function skipping the rest of the code in the function
        }
        //use PrefabUtility to instantiate prefabs in the Editor. This will maintain prefab integrity
        GameObject spawnedItem = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        spawnedItem.transform.position = prefabSpawnLocation; //sets the location of the newly formed prefab
    }
}

// the following adds a button to the previous code in the Inspector. You may need to deselect and reselect gameobject to update Inspector
              //typeof requires the class that will be overriden in the inspector
[CustomEditor(typeof(Sample))]
public class SampleCI : Editor //because we are overriding the Inspector, the class is derived from Editor and not Monobehavior
{
    public override void OnInspectorGUI() //this function handles the rendering of elements in the inspector for the class
    {
        base.OnInspectorGUI(); // loads the default behavior. Removing this would make the component appear blank in the inspector

        Sample classReference = (Sample)target; //gets the reference to the class that we would like override the inspector

        if (GUILayout.Button("Sample Button")) //this creates a button named Sample Button. You can change the name to anything
            classReference.ButtonFunction();  //using the reference from the classReference variable, a direct call to the ButtonFunction is made. 
                                              //You can call  any public function that is declared in the reference class.

        if(GUILayout.Button("Select Components"))
            classReference.SelectItems();

        if(GUILayout.Button("Draw ray"))
            classReference.DrawRaycast();

        if(GUILayout.Button("Spawn Prefab"))
            classReference.SpawnPrefab();
    }
}
//enums are named list items in which you can used to create a drop down menu
//public enum ComponentType { RigidBody,MeshRenderer,Collider}