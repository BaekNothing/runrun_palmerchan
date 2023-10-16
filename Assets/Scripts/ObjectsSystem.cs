using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsSystem : MonoBehaviour
{
    public interface IBGObject
    {
        public void RePosition();
        public void OnSpawn();
        public void OnDespawn();
        public void OnCollide();
    }

    public class BGObject : IBGObject
    {
        public void RePosition()
        {
            throw new NotImplementedException();
        }

        public void OnSpawn()
        {
            throw new NotImplementedException();
        }

        public void OnDespawn()
        {
            throw new NotImplementedException();
        }

        public void OnCollide()
        {
            throw new NotImplementedException();
        }
    }

}
