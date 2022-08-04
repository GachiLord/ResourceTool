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
        protected PartManager PartMng;


        //GUI options
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Move resources up")]
        protected void MoveResourcesUp()
        {
            ResMng.Transfer(PartMng.GetParentParts(CurentPart), PartMng.GetChildParts(CurentPart));
        }
        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiName = "Move resources down")]
        protected void MoveResourcesDown()
        {
            ResMng.Transfer(PartMng.GetChildParts(CurentPart), PartMng.GetParentParts(CurentPart));
        }

        //methods
        private void SetCurentPart(UIPartActionWindow win, Part part) {
            CurentPart = part;
        }

        //standart methods 
        public void Start()
        {
            //setup
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\GameData\ResourceTool\resourceList.txt";
            ResMng = new ResourceManager(File.ReadAllText(path).Split(' '));
            PartMng = new PartManager(FlightGlobals.ActiveVessel.parts.Count());
            //add event listener
            GameEvents.onPartActionUIShown.Add(SetCurentPart);
            //localization
            Events["MoveResourcesUp"].guiName = Localizer.Format("#ResourceTool_MoveUpButton");
            Events["MoveResourcesDown"].guiName = Localizer.Format("#ResourceTool_MoveDownButton");
        }

    }
}
