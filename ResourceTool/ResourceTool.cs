using System.Collections.Generic;
using System.Linq;
using ResourceTool.core;
using KSP.Localization;
using System.IO;
using System;



namespace ResourceTool
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ResourceTool : PartModule
    {
        //fields
        protected Part CurentPart;
        protected ResourceManager ResMng;


        //GUI options
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Move resources up")]
        protected void MoveResourcesUp()
        {
            ResMng.Transfer(PartManager.GetParentParts(CurentPart, FlightGlobals.ActiveVessel.parts.Count(), "dockingPort", new List<Part>()), PartManager.GetChildParts(CurentPart, FlightGlobals.ActiveVessel.parts.Count(), "dockingPort", CurentPart, new List<Part>()));
        }
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Move resources down")]
        protected void MoveResourcesDown()
        {
            ResMng.Transfer(PartManager.GetChildParts(CurentPart, FlightGlobals.ActiveVessel.parts.Count(), "dockingPort", CurentPart, new List<Part>()), PartManager.GetParentParts(CurentPart, FlightGlobals.ActiveVessel.parts.Count(), "dockingPort", new List<Part>()));
        }

        //methods
        private void SetCurentPart(UIPartActionWindow win, Part part) {
            CurentPart = part;
        }

        //standart methods 
        public void Start()
        {
            //get resource names from file
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\GameData\ResourceTool\resourceList.txt";
            ResMng = new ResourceManager(File.ReadAllText(path).Split(' '));
            //add event listener
            GameEvents.onPartActionUIShown.Add(SetCurentPart);
            //localization
            Events["MoveResourcesUp"].guiName = Localizer.Format("#ResourceTool_MoveUpButton");
            Events["MoveResourcesDown"].guiName = Localizer.Format("#ResourceTool_MoveDownButton");
        }

    }
}
