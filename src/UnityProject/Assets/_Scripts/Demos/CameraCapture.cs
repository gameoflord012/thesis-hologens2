using Microsoft.MixedReality.Toolkit.UI;

using System.Linq;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Windows.WebCam;

public class CameraCapture : MonoBehaviour
{
    public Interactable button;

    private PhotoCapture photoCaptureObject = null;
    private bool photoModeStarted = false;
    private bool canTakePhoto = false;
    private bool isTakingPhoto = false;

    private bool CanTakePhoto
    {
        set
        {
            button.enabled = value;
            canTakePhoto = value;
        }
    }

    public bool PhotoModeStarted 
    { 
        get => photoModeStarted;
        set
        {
            photoModeStarted = value;
            UpdateCanTakePhoto();
        }
    }


    private PhotoCapture PhotoCaptureObject 
    { 
        get => photoCaptureObject;
        set
        {
            photoCaptureObject = value;
            UpdateCanTakePhoto();
        }
    }

    public bool IsTakingPhoto 
    { 
        get => isTakingPhoto; 
        set
        {
            isTakingPhoto = value;
            UpdateCanTakePhoto();
        }
    }

    private void UpdateCanTakePhoto()
    {
        CanTakePhoto = photoModeStarted && photoCaptureObject != null && !IsTakingPhoto;
    }

    private void Start()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureObjectCreated);
        button.OnClick.AddListener(TakePhoto);
    }

    private void OnPhotoCaptureObjectCreated(PhotoCapture captureObject)
    {
        PhotoCaptureObject = captureObject;

        // setup CameraParameters
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        CameraParameters c = new CameraParameters();
        c.hologramOpacity = 0.0f;
        c.cameraResolutionWidth = cameraResolution.width;
        c.cameraResolutionHeight = cameraResolution.height;
        c.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(c, OnPhotoModeStarted);
    }

    private void TakePhoto()
    {
        Assert.IsNotNull(PhotoCaptureObject);
        Assert.IsFalse(IsTakingPhoto);

        IsTakingPhoto = true;

        string filename = string.Format(@"CapturedImage{0}_n.jpg", Time.time);
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

        PhotoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        if(result.success)
        {
            Debug.Log("Saved Photo to disk!");
        }
        else
        {
            Assert.IsTrue(false, "Failed to save Photo to disk");
        }

        IsTakingPhoto = false;
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if(result.success)
        {
            PhotoModeStarted = true;
        }
        else
        {
            PhotoModeStarted = false;

            Assert.IsTrue(false, "Unable to start photo mode!");
        }
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        //PhotoCaptureObject.Dispose();
        //PhotoCaptureObject = null;
        PhotoModeStarted = false;
    }
}
