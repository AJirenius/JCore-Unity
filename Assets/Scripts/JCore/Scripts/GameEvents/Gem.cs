using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// System for registering for and triggering global events.
// Supports two kinds of events: int ids (without params, use GetId to get unique string ids) and Objects (extend GameEvent and define params)

namespace JCore
{
    public class Gem : MonoBehaviour// SingletonMonoBehaviour<EventManager>
    {
        public delegate void EventDelegate<T>(T e) where T : AGameEvent;
        private delegate void InternalEventDelegate(AGameEvent e);

        static private int eventIdCounter = 0;
        static private IDictionary<string, int> eventIds = new Dictionary<string, int>();

        // lookup for int events
        static private Dictionary<int, UnityEvent> eventsById = new Dictionary<int, UnityEvent>();

        // lookup for IGameEvent events
        static private Dictionary<System.Type, InternalEventDelegate> delegatesByType = new Dictionary<System.Type, InternalEventDelegate>();
        static private Dictionary<System.Delegate, InternalEventDelegate> delegatesByExternal = new Dictionary<System.Delegate, InternalEventDelegate>();

        static public int GetId(string eventName)
        {
            int eventId;
            if (eventIds.TryGetValue(eventName, out eventId))
            {
                return eventId;
            }
            eventIds.Add(eventName, ++eventIdCounter);
            return eventIdCounter;
        }

        // slow - Use only for debugging
        static public string GetEventName(int eventId)
        {
            foreach(KeyValuePair<string, int> entry in eventIds)
            {
                if (entry.Value == eventId) return entry.Key;
            }
            Debug.LogWarning("Tried get eventname for id: "+eventId+" which does not exist");
            return "EventId does not exist";
        }
       
        static public void Register<T>(EventDelegate<T> del) where T : AGameEvent
        {
            if (delegatesByExternal.ContainsKey(del)) return;

            // Create a new non-generic internal delegate which calls the external one
            InternalEventDelegate internalDelegate = (e) => del((T)e);
            delegatesByExternal[del] = internalDelegate;

            InternalEventDelegate tempDel;
            if (delegatesByType.TryGetValue(typeof(T), out tempDel))
            {
                delegatesByType[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                delegatesByType[typeof(T)] = internalDelegate;
            }
        }

        static public void Register(int eventId, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (eventsById.TryGetValue(eventId, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                eventsById.Add(eventId, thisEvent);
            }
        }

        static public void Unregister(int eventId, UnityAction listener)
        {
            //if (instance == null) return;
            UnityEvent thisEvent = null;
            if (eventsById.TryGetValue(eventId, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
                return;
            }
            Debug.LogWarning("Tried to stop listening to events but event wasnt registered. EventName: " + GetEventName(eventId));
        }

        static public void Unregister<T>(EventDelegate<T> del) where T : AGameEvent
        {
            //if (instance == null) return;
            InternalEventDelegate internalDelegate;
            if (delegatesByExternal.TryGetValue(del, out internalDelegate))
            {
                InternalEventDelegate tempDel;
                if (delegatesByType.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        delegatesByType.Remove(typeof(T));
                    }
                    else
                    {
                        delegatesByType[typeof(T)] = tempDel;
                    }
                }

                delegatesByExternal.Remove(del);
            }
        }

        static public void Post(int eventId)
        {
            UnityEvent thisEvent = null;
            if (eventsById.TryGetValue(eventId, out thisEvent))
            {
                thisEvent.Invoke();
                return;
            }
            Debug.Log("Noone listening to event: "+ GetEventName(eventId)+ "Consider removing the trigger?");
        }

        static public void Post(AGameEvent e)
        {
            InternalEventDelegate del;
            if (delegatesByType.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);
            }
        }
    }
}
