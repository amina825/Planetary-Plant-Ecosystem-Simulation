using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace secondAssignment
{
    public interface Radiation
    {
        Radiation Changewombleroot(Wombleroot p);
        Radiation Changewittentoot(Wittentoot p);
        Radiation Changeworeroot(Woreroot p);
        public string GetTypeOfRadiation();
    }  
    public class Alpha : Radiation
    { 
        public Radiation Changewombleroot(Wombleroot p)
        {
            p.ModifyNutrientLevel(2);
            return this;
        }
        public Radiation Changewittentoot(Wittentoot p)
        {          
            p.ModifyNutrientLevel(-3);            
            return this;
        }
        public Radiation Changeworeroot(Woreroot p)
        {
            p.ModifyNutrientLevel(1);
            return this;
        }
        private Alpha() { }
        private static Alpha Instance = null;
        public static Alpha GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Alpha();
            }
            return Instance;
        }
        public  string GetTypeOfRadiation()
        {
            return "Alpha radiation";
        }
    }
    public class Delta: Radiation
    {
        public Radiation Changewombleroot(Wombleroot p)
        {
            p.ModifyNutrientLevel(-2);           
            return this;
        }     
        public Radiation Changewittentoot(Wittentoot p)
        {           
            p.ModifyNutrientLevel(4);                                 
            return this;                 
        }
        public Radiation Changeworeroot(Woreroot p)
        {      
            p.ModifyNutrientLevel(1);
            return this;        
        }
        private Delta() { }
        private static Delta Instance = null;
        public static Delta GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Delta();
            }
            return Instance;
        }
        public string GetTypeOfRadiation()
        {
            return "Delta radiation";
        }
    }
    public class NoRadiation : Radiation
    {
        public Radiation Changewombleroot(Wombleroot p)
        {
            p.ModifyNutrientLevel(-1);      
            return this;
        }
        public Radiation Changewittentoot(Wittentoot p)
        {          
            p.ModifyNutrientLevel(-1);          
            return this;       
        }
        public Radiation Changeworeroot(Woreroot p)
        {
            p.ModifyNutrientLevel(-1);
            return this;
        }
        private NoRadiation() { }
        private static NoRadiation Instance = null;
        public static NoRadiation GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NoRadiation();
            }
            return Instance;
        }
        public string GetTypeOfRadiation()
        {
            return "No radiation";
        }
    }
}
