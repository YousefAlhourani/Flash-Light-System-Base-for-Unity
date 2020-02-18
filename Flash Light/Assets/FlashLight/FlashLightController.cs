using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GabieStudio
{
    [ExecuteInEditMode]
    public class FlashLightController : MonoBehaviour
    {
        [Header("General Flash Light Settings\n")]
        public FlashLightType flashLightType;
        public float batteryPower;
        public float batteryConsumptionRate;
        private float _DefaultBatteryPower;
        public enum FlashLightType { Infinite, BatteryPower }


        //In Inspector Properties
        [Header("Light Settings\n")]
        [Range(1, 360)]
        public float lightCircleAngle = 45f;
        public float lightCastRange = 5f;
        public float lightStrength = 1f;
        public LightShadows shadowType = LightShadows.None;
        public Color lightColor = Color.yellow;
        public LightmapBakeType lightMode = LightmapBakeType.Realtime;
        public Texture Cookie;
        public Flare flare;
        public LightRenderMode renderMode = LightRenderMode.Auto;
        public bool drawHalo;



        [Header("Light Referencing")]
        public Light LightObject;

        //Logic Related Data
        private bool _ToggleLight { get; set; }
        [HideInInspector] public bool GetLightStatus() { return _ToggleLight; }
        [HideInInspector] public void SetLightStatus(bool X) { _ToggleLight = X; }

        //singelton for referencing.
        public static FlashLightController Instance;
        private static readonly string _MouseLeft = "Fire1"; //Set This To name in Input Manager (Left Mouse Click)
        private static readonly string _MouseRight = "Fire2"; //Set this to name in Input Manager (Right Mouse Click)

        private void Awake() { Instance = this; }

        private void Start()
        {
            if (!Application.isPlaying)
            {
                _ToggleLight = true;
                LightObject.enabled = true;
                LightObject.range = lightCastRange;
                LightObject.shadows = shadowType;
                LightObject.color = lightColor;
                LightObject.lightmapBakeType = lightMode;
                LightObject.intensity = lightStrength;
                LightObject.spotAngle = lightCircleAngle;
            }
            else if (Application.isPlaying)
            {
                _DefaultBatteryPower = batteryPower;
                _ToggleLight = false;
                LightObject.enabled = false;
                LightObject.range = lightCastRange;
                LightObject.shadows = shadowType;
                LightObject.color = lightColor;
                LightObject.lightmapBakeType = lightMode;
                LightObject.intensity = lightStrength;
                LightObject.spotAngle = lightCircleAngle;
            }
        }
       

        private void Update()
        {
            if (!Application.isPlaying)
            {

                LightObject.enabled = true;
                LightObject.range = lightCastRange;
                LightObject.shadows = shadowType;
                LightObject.color = lightColor;
                LightObject.lightmapBakeType = lightMode;
                LightObject.intensity = lightStrength;
                LightObject.spotAngle = lightCircleAngle;
            }

            if (Application.isPlaying)
            {

                if(Input.GetButtonDown(_MouseLeft))
                {
                    if(GetLightStatus())
                    {
                        LightObject.enabled = false;
                        SetLightStatus(false);
                    }
                    else
                    if(!GetLightStatus())
                    {
                        LightObject.enabled = true;
                        SetLightStatus(true);
                    }
                }

                if(flashLightType==FlashLightType.BatteryPower)
                {
                    if(GetLightStatus())
                    {
                        batteryPower -= batteryConsumptionRate;
                        if(batteryPower<=0)
                        {
                            SetLightStatus(false);
                            LightObject.enabled = false;
                        }
                    }
                    
                    if(Input.GetButtonDown(_MouseRight))
                    {
                        batteryPower = _DefaultBatteryPower;
                        //Reload Logic Place Here 
                        //Todo:Connecting UI element to Display battery percentage.
                    }
                }

            }
        }
    }
}