
using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace JCore.Tracking
{
    public class Breadcrumbs
    {
        private readonly int _maxBreadcrumbs;
        private List<Breadcrumb> _allBreadcrumbs;
        public List<Breadcrumb> AllBreadcrumbs => _allBreadcrumbs;

        public Breadcrumbs(int nr = 100)
        {
            _maxBreadcrumbs = nr;
            _allBreadcrumbs = new List<Breadcrumb>();
        }
        
        public void AddBreadCrumb(string message, BreadcrumbType type = BreadcrumbType.Manual, string context = null)
        {
            Breadcrumb newBC = new Breadcrumb(message, type, context);
            _allBreadcrumbs.Add(newBC);
            if (_allBreadcrumbs.Count > _maxBreadcrumbs) _allBreadcrumbs.RemoveAt(0);
            Debug.Log("Added bc:"+newBC+_allBreadcrumbs.Count);
        }

        public override string ToString()
        {
            string str = "";
            foreach (Breadcrumb bc in _allBreadcrumbs)
            {
                str = str + bc + "\n";
            }

            return str;
        }
    }
    public enum BreadcrumbType
    {
        Manual,
        Navigation,
        ClickEvent
    }

    public struct Breadcrumb
    {
        public long timeStamp;
        public string message;
        public BreadcrumbType type;
        public string context;
        

        public Breadcrumb(string message, BreadcrumbType type, string context)
        {
            this.timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.message = message;
            this.type = type;
            this.context = context;
            
        }
        public override string ToString()
        {
            return this.timeStamp + "::" + this.context + " - " + type;
        }  
    }
}