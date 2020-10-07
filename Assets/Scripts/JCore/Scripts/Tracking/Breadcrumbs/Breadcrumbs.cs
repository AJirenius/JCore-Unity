
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
        
        public Breadcrumbs(int nr = 100)
        {
            _maxBreadcrumbs = nr;
            _allBreadcrumbs = new List<Breadcrumb>();
        }
        
        public void AddBreadCrumb(string context, string longMessage = "", BreadcrumbType type = BreadcrumbType.Manual)
        {
            Breadcrumb newBC = new Breadcrumb(context, longMessage, type);
            _allBreadcrumbs.Add(newBC);
            if (_allBreadcrumbs.Count > _maxBreadcrumbs) _allBreadcrumbs.RemoveAt(0);
            Debug.Log("Added bc:"+newBC+_allBreadcrumbs.Count);
        }

        public List<Breadcrumb> Flush()
        {
            List<Breadcrumb> result = _allBreadcrumbs;
            _allBreadcrumbs = new List<Breadcrumb>();
            Debug.Log("Will flush nr bc:"+result.Count);
            return result;
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
        public BreadcrumbType type;
        public string context;
        public string longMessage;

        public Breadcrumb(string context, string longMessage, BreadcrumbType type)
        {
            this.timeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            this.type = type;
            this.context = context;
            this.longMessage = longMessage;
        }
        public override string ToString()
        {
            return this.timeStamp + "::" + this.context + " - " + type;
        }  
    }
}