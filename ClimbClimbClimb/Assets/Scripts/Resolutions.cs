/* Resolutions.cs
 * Author: Wesley Crowe
 * A script that allows the player to change the resolution of the game
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resolutions : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown dropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int currentRefreshRate;
    private int currentResolutionIndex;


    void Awake()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        //dropdown = gameObject.GetComponent<TMP_Dropdown>();
        //clear the options
        dropdown.ClearOptions();
        currentRefreshRate = (int)Screen.currentResolution.refreshRateRatio.value;

        //Debug.Log("RefreshRate: " + currentRefreshRate);

        for (int i = 0; i < resolutions.Length; i++){
            //Debug.Log("Resolution: " + resolutions[i]);
            if ((int)resolutions[i].refreshRateRatio.value == currentRefreshRate){
                filteredResolutions.Add(resolutions[i]);
            }
        }

        //create list of options and populate it
        List<string> options = new List<string>{};
        for (int j = 0; j<filteredResolutions.Count; j++){
            string resolutionOption = filteredResolutions[j].width+"x"+filteredResolutions[j].height+" "+Math.Ceiling(filteredResolutions[j].refreshRateRatio.value)+" Hz";
            options.Add(resolutionOption);

            //get the current resolution
            if (filteredResolutions[j].width == Screen.width && filteredResolutions[j].height == Screen.height){
                currentResolutionIndex = j;
            }
        }

        //add options to the dropdown, set current value
        dropdown.AddOptions(options);
        dropdown.value = currentResolutionIndex;
        dropdown.RefreshShownValue();

        //add a listener for when the dropdown value is changed
        /*dropdown.onValueChanged.AddListener(delegate {
            OnDropdownValueChanged(dropdown);
        });*/

        Debug.Log("Resolutions awake");
    }

    public void SetResolution(int resIndex){
        Resolution resolution = filteredResolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void OnDropdownValueChanged(TMP_Dropdown dropdown)
    {
        Debug.Log("OnDropdownValueChanged invoked");
        string selected = dropdown.captionText.text;
        Debug.Log(selected);
    }
}
