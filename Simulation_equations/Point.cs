using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Point
    {
        private float x;
        /// <summary>
        /// return the x value
        /// </summary>
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        private float z;
        /// <summary>
        /// Return the z value
        /// </summary>
        public float Z
        {
            get { return z; }
            set { z = value; }
        }
        /// <summary>
        /// Point constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        public Point(float x, float z)
        {
            this.x = x;
            this.z = z;
        }
    }
}
