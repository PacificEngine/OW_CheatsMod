using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    class Items
    {
        public static ItemTool getItemTool()
        {
            return GameObject.FindObjectOfType<ItemTool>();
        }

        public static void Start()
        {
        }

        public static void Destroy()
        {
        }

        public static void pickUpWarpCore(WarpCoreType type)
        {
            var tool = getItemTool();
            if (tool)
            {
                foreach (WarpCoreItem core in GameObject.FindObjectsOfType<WarpCoreItem>())
                {
                    if (core.GetWarpCoreType().Equals(type))
                    {
                        var newCore = GameObject.Instantiate(core, (Locator.GetAstroObject(AstroObject.Name.Sun)?.transform ?? Locator.GetAstroObject(AstroObject.Name.Eye)?.transform ?? Locator.GetPlayerBody()?.transform));
                        SetVisible(newCore, true);
                        tool.PickUpItemInstantly(newCore);
                        break;
                    }
                }
            }
        }

        private static void SetVisible(OWItem item, bool visible)
        {
            item.SetValue("_visible", visible);
            foreach (OWRenderer render in item.GetValue<OWRenderer[]>("_renderers"))
            {
                if (render)
                {
                    render.enabled = true;
                    render.SetActivation(true);
                    render.SetLODActivation(visible);
                    if (render.GetRenderer())
                    {
                        render.GetRenderer().enabled = true;
                    }
                }
            }
            foreach (ParticleSystem particleSystem in item.GetValue<ParticleSystem[]>("_particleSystems"))
            {
                if (particleSystem)
                {
                    if (visible)
                        particleSystem.Play(true);
                    else
                        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
            foreach (OWLight2 light in item.GetValue<OWLight2[]>("_lights"))
            {
                if (light)
                {
                    light.enabled = true;
                    light.SetActivation(true);
                    light.SetLODActivation(visible);
                    if (light.GetLight())
                    {
                        light.GetLight().enabled = true;
                    }
                }
            }
        }

    }
}
