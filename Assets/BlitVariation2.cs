using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BlitVariation2 : MonoBehaviour
{
    public Camera environmentCam;
    private CommandBuffer cmd;

    public Material CopyMaterial;

    public RenderTexture EnvironmentCameraRenderTexture;
    public RenderTexture CharacterCameraRenderTexture;

    
    void Awake()
    {
        cmd = new CommandBuffer();
        cmd.name = "Env Command Buffer";
        
        cmd.Blit( BuiltinRenderTextureType.CameraTarget, EnvironmentCameraRenderTexture );
        cmd.SetGlobalTexture("_CharacterTex", CharacterCameraRenderTexture);
        cmd.Blit(EnvironmentCameraRenderTexture, BuiltinRenderTextureType.CameraTarget, CopyMaterial);
        
        environmentCam.AddCommandBuffer( CameraEvent.AfterImageEffects, cmd );
    }
}

