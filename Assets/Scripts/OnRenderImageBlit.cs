using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OnRenderImageBlit : MonoBehaviour
{
    public Camera environmentCam;
    private CommandBuffer cmd;

    public Material CopyMaterial;

    public RenderTexture EnvironmentCameraRenderTexture;
    public RenderTexture CharacterCameraRenderTexture;
    
    public void Start()
    {
        cmd = new CommandBuffer();
        cmd.name = "CommandBuffer MainCamera X";
      
        environmentCam.AddCommandBuffer(CameraEvent.AfterImageEffects, cmd);
        cmd.SetGlobalTexture("_CharacterTex", CharacterCameraRenderTexture);
    }
    
    private void OnPreRender()
    {
        environmentCam.targetTexture = EnvironmentCameraRenderTexture;
    }
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        environmentCam.targetTexture = null;
        Graphics.Blit(EnvironmentCameraRenderTexture, null, CopyMaterial);
    }



 

}
