using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackImg : MonoBehaviour
{
    public float _timer;

    public ARTrackedImageManager arManager;
    public List<GameObject> objList = new List<GameObject>();
    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();

    /*
    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();

    private List<float> _trackedTimer = new List<float>();

    */
    private void Awake()
    {
        foreach(GameObject obj in objList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj);
        }
    }

    /*

    private void Update()
    {
        if(_trackedImg.Count > 0)
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();

            for (var i = 0; i < _trackedImg.Count; i++)
            {
                if(_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                    if(_trackedTimer[i] > _timer)
                    {
                        string name = _trackedImg[i].referenceImage.name;
                        GameObject tObj = _prefabDic[name];
                        tObj.GetComponent<Exercise>().isExercise = false;
                        tObj.GetComponent<Exercise>().isOn = false;
                        tObj.SetActive(false);

                        tNumList.Add(_trackedImg[i]);
                    }
                    else
                    {
                        _trackedTimer[i] += Time.deltaTime;
                    }
                }
            }

            if(tNumList.Count > 0)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    int num = _trackedImg.IndexOf(tNumList[i]);

                    _trackedImg.Remove(_trackedImg[num]);
                    _trackedTimer.Remove(_trackedTimer[num]);
                }
            }
        }
    }

    */

    private void OnEnable()
    {
        arManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        arManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            /*
            if(!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            */
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            /*
            if(!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else
            {
                int num = _trackedImg.IndexOf(trackedImage);
                _trackedTimer[num] = 0;
            }
            */

            UpdateImage(trackedImage);
        }

        foreach(ARTrackedImage trackedImage in eventArgs.removed)
        {

            _prefabDic[trackedImage.name].SetActive(false);

            

        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDic[name];

        if(trackedImage.trackingState == TrackingState.Tracking)
        {
            tObj.transform.position = trackedImage.transform.position;
            tObj.transform.rotation = trackedImage.transform.rotation;
            if (tObj.GetComponentInChildren<Exercise>() != null)
                tObj.GetComponentInChildren<Exercise>().isOn = true;

            tObj.SetActive(true);
            
        }
        else
        {
            if (tObj.GetComponentInChildren<Exercise>() != null)
            {
                tObj.GetComponentInChildren<Exercise>().isOn = false;
                tObj.GetComponentInChildren<Exercise>().isExercise = false;
            }
            tObj.SetActive(false);
        }

    }

}
