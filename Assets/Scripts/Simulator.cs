using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class Simulator : MonoBehaviour
{
    [SerializeField]
    private bool bSimulate;
    [SerializeField]
    private float iteration = 1;
    void Update()
    {
        if (bSimulate)
        {
            //Time.deltaTime; time in between frames
            //Time.fixedDeltaTime; timestep that is set in project settings/time
            //Time.unscaledDeltaTime; is time that is independent of Timescale
            //Time.timeScale = 0; will freeze time
            Physics.simulationMode = SimulationMode.Script;
            for (int x = 0; x < iteration; x++)
            {
                Physics.Simulate(Time.fixedDeltaTime);
            }
            bSimulate = false;
        }
    }
}
