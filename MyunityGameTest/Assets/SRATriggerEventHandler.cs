using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Serialization;

namespace XR.EventHandler1
{
    public class SRATriggerEventHandler : MonoBehaviour
    {
        // Fields

        public GameObject targetGameObject = null;
        public float snapDuration;
        public bool shouldSnap = true;
        [FormerlySerializedAs("onSnapBegin")]
        public UnityEvent onTriggerEnter;
        public UnityEvent onSnapEnd;

        public GameObject currentTriggeredObject;

        // Public method

        private void OnTriggerEnter(Collider collision)
        {
            currentTriggeredObject = collision.gameObject;
            if (currentTriggeredObject == null) { currentTriggeredObject.name = "Trigger"; }
            Sequence s = DOTween.Sequence();
            if (collision.gameObject.Equals(targetGameObject))
            {
                onTriggerEnter.Invoke();
                if (shouldSnap)
                {
                    s.Append(targetGameObject.transform.DOMove(transform.position, snapDuration));
                    s.Join(targetGameObject.transform.DORotate(transform.eulerAngles, snapDuration));
                    s.OnComplete(() => onSnapEnd.Invoke());
                }
            }
        }
    }
}
