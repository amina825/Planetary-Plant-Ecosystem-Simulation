using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Channels;
using System.Threading;
using System.Xml.Linq;
using System;
using TextFile;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace secondAssignment
{
    public abstract class Plant
    {
        //empty ecosystem exception
        public class EmptyEcosystemException : Exception { }    
        public string name { get; }

        public int nutrientLevel;
        public void ModifyNutrientLevel(int value) {
            nutrientLevel += value;
        }
        public virtual bool IsAlive()
        {
            return nutrientLevel > 0;
        }
        protected Plant(string nameOFPlant, int NutrientLevel)
        {
            name = nameOFPlant;
            nutrientLevel = NutrientLevel;
            
        }
        public abstract Radiation Traverse(Radiation radiation);
        public abstract void DemandRadiation( ref int alphaDemand, ref int deltaDemand);
    }          
    public class Wombleroot : Plant
    {
        public Wombleroot(string str, int num) : base(str, num) { }
        public override Radiation Traverse(Radiation radiation)
        {
            return radiation.Changewombleroot(this);
        }
        public override bool IsAlive()
        {
            return nutrientLevel > 0 && nutrientLevel <= 10;
        }
        public override void DemandRadiation(ref int alphaDemand, ref int deltaDemand)
        {
            if (IsAlive())
            {
                alphaDemand += 10; // Wombleroot demands alpha radiation by a strength of 10
            }            
        }        
    }
    public class Wittentoot : Plant
    {
        public Wittentoot(string str, int num) : base(str , num) { }
        public override Radiation Traverse(Radiation radiation)
        {
            return radiation.Changewittentoot(this);
        }        
        public override void DemandRadiation(ref int alpha, ref int delta)
        {
            if (nutrientLevel < 5 && IsAlive())
            {
                delta += 4;
            }
            else if (nutrientLevel >= 5 && nutrientLevel <= 10 && IsAlive())
            {
                delta += 1;
            }
        }
    }
    public class Woreroot : Plant
    {
        public Woreroot(string str, int num) : base(str, num) { }
        public override Radiation Traverse(Radiation radiation)
        {
            return radiation.Changeworeroot(this);
        }       
        public override void DemandRadiation(ref int alphaDemand, ref int deltaDemand)
        {
            
        }
    }
}