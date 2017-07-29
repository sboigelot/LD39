using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Serialization;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PrototypeManager : Singleton<PrototypeManager>
    {

        public IEnumerator LoadPrototypes()
        {
            //Rooms = new List<Room>();
            //var sub = Load<List<Room>, Room>(Rooms, "Rooms.xml");
            //foreach (var s in sub)
            //{
            //    yield return s;
            //}
            
            //PlayerTemplate = Load<PlayerProfile>("PlayerTemplate.xml");
            //Localizer.Instance.EnsureAllLocalKeyExist();
            yield break;
        }

        private IEnumerable Load<T,TI>(T store, string fileName) where T : class, IList<TI>, new() where TI: class, new()
        {
            var sub = DataSerializer.Instance.LoadFromStreamingAssets<T, TI>(store, "Data", fileName);
            foreach (var s in sub)
            {
                yield return s;
            }
        }

        //public static Room FindRoomPrototype(string roomName)
        //{
        //    var room = Instance.Rooms.SingleOrDefault(r => r.Name == roomName);
        //    if (room == null)
        //    {
        //        Debug.LogWarning("PrototypeManager coudn't find room with name: "+roomName);
        //    }
        //    return room;
        //}
    }
}