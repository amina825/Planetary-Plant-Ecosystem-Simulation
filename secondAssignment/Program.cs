using Microsoft.VisualBasic;
using secondAssignment;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextFile;
using static System.Net.Mime.MediaTypeNames;
namespace secondAssignment
{
    public class Program
    {
        static void Main()
        {
            List<Plant> plants = ReadPlantsFromFile(out int days);
            Console.WriteLine("File contents are: ");
            for (int i = 0; i < plants.Count; i++)
            {
                string status = plants[i].IsAlive() ? "Alive" : "Dead";
                Console.WriteLine(plants[i].name + " " + plants[i].nutrientLevel + " " + status);
            }
            Console.WriteLine(days + "\n");
            Radiation no = NoRadiation.GetInstance();
            for (int i = 0; i < days; i++)
            {
                foreach (Plant plant in plants)
                {
                    plant.Traverse(no);
                }
                Console.WriteLine("Day:" + (i + 1) + "\nRadiation: " + no.GetTypeOfRadiation());
                for (int j = 0; j < plants.Count; j++)
                {
                    string status = plants[j].IsAlive() ? "Alive" : "Dead";
                    Console.WriteLine(plants[j].name + " "  + plants[j].nutrientLevel + " " + status);
                }
                Console.WriteLine();
                plants.RemoveAll(plant => !plant.IsAlive());
                no = DetermineNextRadiation(plants);
                if (plants.Count == 0)
                {                   
                    break;
                }
            }
            if (plants.Count != 0)
            {
                Console.WriteLine("The strongest plant is: " + FindStrongestPlant(plants));
            }                 
        }
        public static string FindStrongestPlant(List<Plant> plants)
        {
            string nameOfStrongestPlant = null;
            if (plants.Count != 0)
            {              
                int max = 0;               
                foreach (Plant plant in plants)
                {
                    if (plant.nutrientLevel > max)
                    {
                        max = plant.nutrientLevel;
                        nameOfStrongestPlant = plant.name;
                    }
                }
            }
            return nameOfStrongestPlant ;
        }
        public static Radiation DetermineNextRadiation(List<Plant> plants)
        {
            Radiation no = null;
            try
            {
                if (plants.Count != 0)
                {
                    int alpha = 0;
                    int delta = 0;
                    foreach (Plant plant in plants)
                    {
                        if (plant.IsAlive())
                        {
                            plant.DemandRadiation(ref alpha, ref delta);
                        }
                    }
                    if (alpha - delta >= 3)
                    {
                        no = Alpha.GetInstance();
                    }
                    else if (delta - alpha >= 3)
                    {
                        no = Delta.GetInstance();
                    }
                    else
                    {
                        no = NoRadiation.GetInstance();
                    }
                }               
                else
                {
                    throw new Plant.EmptyEcosystemException();
                }
               
            }                  
            catch (Plant.EmptyEcosystemException)
            {
                Console.WriteLine("Empty ecosystem all plants are dead");             
            }             
            return no;
        }
        static List<Plant> ReadPlantsFromFile(out int days)
        {
            try
            {
                Console.Write("Enter the file name : ");
                string filename = Console.ReadLine();
                Console.WriteLine();
                TextFileReader t = new TextFileReader(filename);
                t.ReadLine(out string line);
                int n = int.Parse(line);
                List<Plant> plants = new();
                for (int i = 0; i < n; i++)
                {
                    Plant plant = null;
                    if (t.ReadLine(out line))
                    {
                        string name = line.Split(' ')[0];
                        string species = line.Split(' ')[1];
                        int nutrientLevel = int.Parse(line.Split(' ')[2]);
                        switch (species)
                        {
                            case "wom":
                                plant = new Wombleroot(name,  nutrientLevel);
                                break;
                            case "wit":
                                plant = new Wittentoot(name, nutrientLevel);
                                break;
                            case "wor":
                                plant = new Woreroot(name, nutrientLevel);
                                break;
                        }
                    }                   
                    plants.Add(plant);
                }
                t.ReadLine(out string numdays);
                days = int.Parse(numdays);
                return plants;
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File not found");
                days = 0;
                return new List<Plant>();
            }                   
        }
    }
}   
