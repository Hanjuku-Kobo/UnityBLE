using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlePlugin.Data {
    public class DiscoveredService {
        private Dictionary<string, List<DiscoveredCharacterisitc>> discoveredServices = new Dictionary<string, List<DiscoveredCharacterisitc>>();

        // Dictionary生成時にデータをセットする
        public void setValue(string service, DiscoveredCharacterisitc characteristic) 
        {
            if (discoveredServices.ContainsKey(service)) {
                // 既にkeyが存在している場合
                List<DiscoveredCharacterisitc> charas = discoveredServices[service];
                charas.Add(characteristic);
            } else {
                // まだない場合はついかする
                discoveredServices.Add(
                    service,
                    new List<DiscoveredCharacterisitc>{characteristic}
                );
            }
        }

        // getter
        public Dictionary<string, List<DiscoveredCharacterisitc>> GetDiscoveredServices() 
        {
            return new Dictionary<string, List<DiscoveredCharacterisitc>>(discoveredServices);
        }
        public List<string> GetServices() 
        {
            return new List<string>(discoveredServices.Keys);
        }
        public List<DiscoveredCharacterisitc> GetCharacteristics(string service)
        {
            return new List<DiscoveredCharacterisitc>(discoveredServices[service]);
        }
    }
}