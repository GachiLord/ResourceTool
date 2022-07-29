using System.Collections.Generic;

namespace ResourceTool.core
{
    public class ResourceManager
    {
        private static readonly IResourceBroker Broker = new ResourceBroker();

        protected readonly string[] Resources = new string[] {};

        protected readonly ResourceFlowMode Flow;

        public ResourceManager(string[] resourceNameList, ResourceFlowMode flow = ResourceFlowMode.NO_FLOW) { 
            Resources = resourceNameList;
            Flow = flow;
        }
        public void Transfer(List<Part> v1, List<Part> v2) {
            foreach (var r in Resources) {
                TransferRes(v1, v2, r);
            }
        }

        protected List<Part> GetResParts(string resName, List<Part> parts) {
            var matched = new List<Part>();


            foreach (Part part in parts) {
                var st = Broker.StorageAvailable(part, resName, 0, Flow, 1);
                if (st != 0) matched.Add(part);
            }


            return matched;
        }

        protected void TransferRes(List<Part> v1, List<Part> v2, string resName) {
            double am2 = GetResAmount(v2, resName);
            double usedRes = 0;
            double requiredAmount;


            //fill v1
            foreach (Part part in v1) {
                requiredAmount = Broker.StorageAvailable(part, resName, 0, Flow, 1);

                if (am2 - requiredAmount >= 0)
                {
                    Broker.StoreResource(part, resName, requiredAmount, 0, Flow);
                    am2 -= requiredAmount;
                    usedRes += requiredAmount;
                }
                else {
                    Broker.StoreResource(part, resName, am2, 0, Flow);
                    usedRes += am2;
                    break;
                }
            }


            //empty v2
            foreach (Part part in v2) {
                requiredAmount = Broker.AmountAvailable(part, resName, 0, Flow);

                if (usedRes - requiredAmount >= 0)
                {
                    Broker.RequestResource(part, resName, requiredAmount, 0, Flow);
                    usedRes -= requiredAmount;
                }
                else
                {
                    Broker.RequestResource(part, resName, usedRes, 0, Flow);
                    break;
                }
            }
        }

        protected double GetResAmount(List<Part> parts, string resName, string t = "a") {
            double amount = 0;


            foreach (var part in parts) {
                if (t == "s") amount += Broker.StorageAvailable(part, resName, 0, Flow, 1);
                else amount += Broker.AmountAvailable(part, resName, 0, Flow);
            }


            return amount;
        }
    }
}
