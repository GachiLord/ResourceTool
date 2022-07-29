using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ResourceTool.debug;



namespace tests
{
    [TestClass]
    public class Ship
    {
        [TestMethod]
        public void ShipSaving()
        {
            JsonSerializer.Serialize(new FlightGlobals());
        }
    }
}


