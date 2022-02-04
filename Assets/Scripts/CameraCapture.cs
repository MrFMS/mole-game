using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

public class CameraCapture : MonoBehaviour
{
    // Webcam
    private Emgu.CV.VideoCapture webcam;
    private Mat webcamFrame;

    public UnityEngine.UI.RawImage rawImage;
    public Texture2D tex;

    List<int> idList = new List<int> { 120, 121, 122, 123};
 
    public GameObject Hammer;
    HammerController HammerController;

    bool isMissing = false;

    MarkerDetection mkdetect = new MarkerDetection();




    private void HandleWebcamQueryFrame(object sender, System.EventArgs e)
    {

        if (webcam.IsOpened)
        {
            webcam.Retrieve(webcamFrame);
        }




        var markers = mkdetect.Detect(webcamFrame);

        List<int> markerID = new List<int>(markers.Item2.ToArray());


        if (markerID.Count < 4)
        {
            foreach (var id in idList)
            {
                if (!markerID.Contains(id) && isMissing == false)
                {
                    isMissing = true;
                    Debug.Log("icii " + id);
                    HammerController.MakeHammer(id);
                }
            }
        } else
        {
            isMissing = false;
        }

    }


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("starting webcam");

        HammerController = Hammer.GetComponent<HammerController>();
        // Manière Avec Webcam (flux de la webcam)
        webcam = new Emgu.CV.VideoCapture(0, VideoCapture.API.DShow);
        webcamFrame = new Mat();

        // Add event handler to the webcam
        webcam.ImageGrabbed += new System.EventHandler(HandleWebcamQueryFrame);
        // Demarage de la webcam
        webcam.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (webcam.IsOpened)
        {
            // send event that an image has been acquired
            bool grabbed = webcam.Grab();
        }

        DisplayFrameOnPlane();//manque dans le sujet
    }
    void OnDestroy()
    {
        Debug.Log("entering destroy");

        if (webcam != null)
        {

            Debug.Log("sleeping");
            //waiting for thread to finish before disposing the camera...(took a while to figure out)
            System.Threading.Thread.Sleep(60);
            // close camera
            webcam.Stop();
            webcam.Dispose();
        }

        Debug.Log("Destroying webcam");
    }

    private void DisplayFrameOnPlane()
    {
        if (webcamFrame.IsEmpty) return;

        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        // destroy existing texture
        if (tex != null)
        {
            Destroy(tex);
            tex = null;
        }

        // creating new texture to hold our frame
        tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        // Resize mat to the texture format
        CvInvoke.Resize(webcamFrame, webcamFrame, new System.Drawing.Size(width, height));
        // Convert to unity texture format ( RGBA )
        CvInvoke.CvtColor(webcamFrame, webcamFrame, ColorConversion.Bgr2Rgba);
        // Flipping because unity texture is inverted.
        CvInvoke.Flip(webcamFrame, webcamFrame, FlipType.Vertical);

        // loading texture in texture object
        tex.LoadRawTextureData(webcamFrame.ToImage<Rgba, byte>().Bytes);
        tex.Apply();

        // assigning texture to gameObject
        rawImage.texture = tex;
    }
}
