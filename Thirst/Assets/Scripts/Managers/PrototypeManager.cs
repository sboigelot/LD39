using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Localization;
using Assets.Scripts.Models;
using Assets.Scripts.Serialization;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PrototypeManager : Singleton<PrototypeManager>
    {
        public List<AnimationPrototype> AnimationPrototypes;

        public List<TilePrototype> TilePrototypes;

        public List<MonsterPrototype> MonsterPrototypes;

        public List<ItemPrototype> ItemPrototypes;

        public List<WeaponPrototype> WeaponPrototypes;

        public List<MermaidPrototype> MermaidPrototypes;

        public MermaidPrototype MermaidPrototype
        {
            get { return MermaidPrototypes != null ? MermaidPrototypes.FirstOrDefault() : null; }
        }

        public IEnumerator LoadPrototypes(Action onLoaded)
        {
            AnimationPrototypes = new List<AnimationPrototype>();
            var sub = Load<List<AnimationPrototype>, AnimationPrototype>(AnimationPrototypes, "Animations.xml");
            foreach (var s in sub)
            {
                yield return s;
            }

            TilePrototypes = new List<TilePrototype>();
            sub = Load<List<TilePrototype>, TilePrototype>(TilePrototypes, "Tiles.xml");
            foreach (var s in sub)
            {
                yield return s;
            }

            MonsterPrototypes = new List<MonsterPrototype>();
            sub = Load<List<MonsterPrototype>, MonsterPrototype>(MonsterPrototypes, "Monsters.xml");
            foreach (var s in sub)
            {
                yield return s;
            }

            ItemPrototypes = new List<ItemPrototype>();
            sub = Load<List<ItemPrototype>, ItemPrototype>(ItemPrototypes, "Items.xml");
            foreach (var s in sub)
            {
                yield return s;
            }

            WeaponPrototypes = new List<WeaponPrototype>();
            sub = Load<List<WeaponPrototype>, WeaponPrototype>(WeaponPrototypes, "Weapons.xml");
            foreach (var s in sub)
            {
                yield return s;
            }

            MermaidPrototypes = new List<MermaidPrototype>();
            sub = Load<List<MermaidPrototype>, MermaidPrototype>(MermaidPrototypes, "Mermaid.xml");
            foreach (var s in sub)
            {
                yield return s;
            }

            //Localizer.Instance.EnsureAllLocalKeyExist();

            if (onLoaded != null)
            {
                onLoaded();
            }
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

        public static AnimationPrototype FindAnimationPrototype(string name)
        {
            var proto = Instance.AnimationPrototypes.SingleOrDefault(r => r.Name == name);
            if (proto == null)
            {
                Debug.LogWarning("PrototypeManager coudn't find prototype with name: " + name);
            }
            return proto;
        }

        public static TilePrototype FindTilePrototype(string name)
        {
            var proto = Instance.TilePrototypes.SingleOrDefault(r => r.Name == name);
            if (proto == null)
            {
                Debug.LogWarning("PrototypeManager coudn't find prototype with name: " + name);
            }
            return proto;
        }

        public static MonsterPrototype FindMonsterPrototype(string name)
        {
            var proto = Instance.MonsterPrototypes.SingleOrDefault(r => r.Name == name);
            if (proto == null)
            {
                Debug.LogWarning("PrototypeManager coudn't find prototype with name: " + name);
            }
            return proto;
        }

        public static ItemPrototype FindItemPrototype(string name)
        {
            var proto = Instance.ItemPrototypes.SingleOrDefault(r => r.Name == name);
            if (proto == null)
            {
                Debug.LogWarning("PrototypeManager coudn't find prototype with name: " + name);
            }
            return proto;
        }

        public static WeaponPrototype FindWeaponPrototype(string name)
        {
            var proto = Instance.WeaponPrototypes.SingleOrDefault(r => r.Name == name);
            if (proto == null)
            {
                Debug.LogWarning("PrototypeManager coudn't find prototype with name: " + name);
            }
            return proto;
        }
    }
}