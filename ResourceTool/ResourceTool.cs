using System.Collections.Generic;
using System.Linq;
using ResourceTool.core;
using KSP.Localization;
using System.IO;
using System;
using UnityEngine;


namespace ResourceTool
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    internal class ResourceTool : PartModule
    {
        //fields
        private Part CurentPart;
        private ResourceManager ResMng;
        private PartManager PartMng;


        //GUI options
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Move resources up")]
        private void MoveResourcesUp()
        {
            var parent = PartMng.GetParentParts(CurentPart);
            var child = PartMng.GetChildParts(CurentPart);

            ShowTransferInfoMsg(ResMng.Transfer(child, parent));
        }
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Move resources down")]
        private void MoveResourcesDown()
        {
            var parent = PartMng.GetParentParts(CurentPart);
            var child = PartMng.GetChildParts(CurentPart);

            ShowTransferInfoMsg(ResMng.Transfer(parent, child));
        }

        //methods
        private void SetCurentPart(UIPartActionWindow win, Part part) {
            CurentPart = part;
        }
        private void ShowTransferInfoMsg(string[] resources) {
            string msg = Localizer.Format("#ResourceTool_InfoMsg");
            foreach (var r in resources) msg += " " + r;
            if ( resources.Length == 0 ) ScreenMessages.PostScreenMessage(Localizer.Format("#ResourceTool_InfoNoResources"));
            else ScreenMessages.PostScreenMessage(msg);
        }

        //standart methods 
        private void Start()
        {
            //setup
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\GameData\ResourceTool\resourceList.txt";
            ResMng = new ResourceManager(File.ReadAllText(path).Split(' '));
            PartMng = new PartManager(FlightGlobals.ActiveVessel);
            //add event listener
            GameEvents.onPartActionUIShown.Add(SetCurentPart);
            //localization
            Events["MoveResourcesUp"].guiName = Localizer.Format("#ResourceTool_MoveUpButton");
            Events["MoveResourcesDown"].guiName = Localizer.Format("#ResourceTool_MoveDownButton");
        }

    }
}
