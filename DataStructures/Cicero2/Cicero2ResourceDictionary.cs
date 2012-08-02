﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cicero.DataStructures2
{
    public class Cicero2ResourceDictionary : Dictionary<ResourceID, Cicero2DataObject>
    {
        public IEnumerable<KeyValuePair<ResourceID<FilterType>, FilterType>> KeyValuePairsOfType<FilterType>() where FilterType : Cicero2DataObject
        {
            return this.Where(
                    x => { return (x is FilterType); }
                ).Cast<KeyValuePair<ResourceID<FilterType>, FilterType>>();
        }

        public IEnumerable<FilterType> ResourcesOfType<FilterType>() where FilterType : Cicero2DataObject
        {
          
            return Values.Where(
                x => { return x is FilterType; }
                ).Cast<FilterType>();
        }

        /// <summary>
        /// Adds given resource to the dictionary and returns true if successful. 
        /// 
        /// Returns false and fails if resource was null,
        /// if key was already taken, or if key was null valued.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public bool TryAddResource(Cicero2DataObject resource)
        {
            if (resource == null)
                return false;
            if (resource.ResourceID == ResourceID.Null)
                return false;
            Add(resource.ResourceID, resource);
            return true;
        }

        public ResourceType Get<ResourceType>(ResourceID<ResourceType> resourceID) where ResourceType : Cicero2DataObject
        {
            if (ContainsKey(resourceID))
            {
                Cicero2DataObject resource = this[resourceID];
                if (resource is ResourceType)
                    return (ResourceType)resource;
                throw new ResourceException("Attempted to fetch resource of incorrect type from dictionary. Expected type was "
                    + typeof(ResourceType).ToString() + ", encountered type was " + resource.GetType().ToString() +
                    ". ResourceID was " + resourceID.ToString());
            }
            return null;
        }

        /// <summary>
        /// Intended to be used only when first creating a resource. Example use:
        /// DimensionedParameter param = resourceDictionary.AddNew(new DimensionedParameter(...));
        /// </summary>
        /// <typeparam name="ResourceType"></typeparam>
        /// <param name="newResource"></param>
        /// <returns></returns>
        public ResourceID<ResourceType> AddNew<ResourceType>(ResourceType newResource) where ResourceType : Cicero2DataObject {
            Add(newResource.ResourceID, newResource);
            return newResource.ResourceID;
        }
    }
}