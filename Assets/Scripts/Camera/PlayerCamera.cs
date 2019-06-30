using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private Camera cam; //the Camera component attached to the Camera GameObject
    private GameObject player;
    private List<CameraZone> insideZones = new List<CameraZone>(); //List of all the CameraZones the player is currently inside

    private float defaultSize; //how zoomed in the Camera should be when not inside a CameraZone
    public float defaultPlayerFocus;
    public float defaultFollowSpeed = .5f;

    private Vector2 lastCamPos = new Vector2(0, 0), targetCamPos = new Vector2(0, 0);
    private float lastCamSize = 0, targetCamSize = 0;

    private float blendedPlayerFocus;
    private float blendedFollowSpeed;

    public static PlayerCamera instance;

    void Awake()
    {
        instance = this;
        cam = gameObject.GetComponent<Camera>();
        defaultSize = cam.orthographicSize;
    }

    private void Start()
    {
        player = PlayerManager.instance.getPlayer();
    }

    //Public-facing functions for interacting with insideZones
    public void addZone(CameraZone zone) { insideZones.Add( zone ); }
    public void removeZone(CameraZone zone) { insideZones.Remove( zone ); }

    //Converts a 2D camera position to 3D
    private Vector3 vec2To3(Vector2 vec2) { return new Vector3(vec2.x, vec2.y, -1); }

    // Update is called once per frame
    void Update () {
        //If player is not inside a zone, camera should follow him/her/zer/zim/zoom/zibitty bob hey kids mmmmmm jell-o
		if(insideZones.Count > 0)
        {
            CameraSettings blendedCameraSettings = CameraSettings.blendSettings( insideZones, player );
            targetCamPos = new Vector2(blendedCameraSettings.x, blendedCameraSettings.y);
            targetCamSize = blendedCameraSettings.size;

            CameraZoneSettings blendedCameraZoneSettings = CameraZoneSettings.blendSettings( insideZones );
            blendedPlayerFocus = blendedCameraZoneSettings.playerFocus;
            blendedFollowSpeed = blendedCameraZoneSettings.followSpeed;
        }
        else
        {
            targetCamPos = new Vector2(player.transform.position.x, player.transform.position.y);
            targetCamSize = defaultSize;

            blendedPlayerFocus = defaultPlayerFocus;
            blendedFollowSpeed = defaultFollowSpeed;
        }

        //position
        transform.position = vec2To3( lastCamPos + ( targetCamPos - lastCamPos ) * blendedFollowSpeed );
        lastCamPos = new Vector2(transform.position.x, transform.position.y);
        //zoom
        cam.orthographicSize = ( lastCamSize + ( targetCamSize - lastCamSize ) * blendedFollowSpeed / 10 );
        lastCamSize = cam.orthographicSize;
    }
}
