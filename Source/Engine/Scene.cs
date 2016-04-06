﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public class Scene
    {
        public SortedList<int, List<Entity>> layers;

        /// <summary>
        /// Scene constructor
        /// </summary>
        public Scene() 
        {
            layers = new SortedList<int,List<Entity>>();
        }

        /// <summary>
        /// This method is used to retreive all entities in the current scene
        /// </summary>
        /// <returns>A list of entities from the scene</returns>
        public List<Entity> GetAllEntities()
        {
            List<Entity> e = new List<Entity>();
            List<Entity> tmp = new List<Entity>();
            for (int i = 0; i < layers.Count; ++i)
            {
                tmp = layers[i].ToList();
                e = e.Concat(tmp).ToList();
                //tmp.Clear();
            }
            return e;
        }

        /// <summary>
        /// Returns a list of all the layers
        /// </summary>
        /// <returns>A sorted list with all the layers</returns>
        public List<List<Entity>> GetAllLayers()
        {
            return layers.Values.ToList();
        }

        /// <summary>
        /// Adds an entity to a specified layer
        /// </summary>
        /// <param name="layer"> The layer number </param>
        /// <param name="entity"> The entity to be added to layer </param>
        public void AddEntityToLayer(int layer, Entity entity)
        {
            if (layers.Count<=layer)
            {
                layers.Add(layer, new List<Entity>());
            }
            layers[layer].Add(entity);
        }

        /// <summary>
        /// Remove an entity from a layer
        /// </summary>
        /// <param name="layer"> The layer to be removed </param>
        /// <param name="?"> The entity to be removed </param>
        public void RemoveEntityFromLayer(int layer, Entity entity)
        {
            if (layers[layer] != null)
                if (layers[layer].Contains(entity))
                    layers[layer].Remove(entity);
        }

        /// <summary>
        /// Remove a layer from the scene
        /// </summary>
        /// <param name="layer"> The layer to be removed </param>
        public void RemoveLayer(int layer)
        {
            if (layers[layer] != null)
                layers.Remove(layer);
        }
    }
}
