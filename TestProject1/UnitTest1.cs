using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using secondAssignment;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {       
        private List<Plant> OneInput()
        {
            List<Plant> plants = new List<Plant>();
            plants.Add(new Wombleroot("Hungry", 5));
            return plants;
        }
        private List<Plant> MulInput()
        {
            List<Plant> plants = new List<Plant>
            {
                new Wombleroot("Hungry", 5),
                new Wittentoot("Thirsty", 4),
                new Wittentoot("Lany", 4),
                new Wittentoot("Sooo", 4),
                new Wittentoot("Smallo", 6)
            };
            return plants;
        }
        private List<Plant> TestInput()
        {
            List<Plant> plants = new List<Plant>
            {
                new Wombleroot("Hungry", 7),
                new Wittentoot("Thirsty", 5),
                new Woreroot("Lany", 4),
                new Wittentoot("Sooo", 3),
            };
            return plants;
        }        
        [TestMethod]

        public void TestMethodOne()
        {
            List<Plant> plants = OneInput();
            Radiation nextRadiation = Program.DetermineNextRadiation(plants);
            Assert.IsInstanceOfType(nextRadiation, typeof(Alpha));
        }
        [TestMethod]
        public void TestMethodMul()
        {
            List<Plant> plants = MulInput();
            Radiation nextRadiation = Program.DetermineNextRadiation(plants);
            Assert.IsInstanceOfType(nextRadiation, typeof(Delta));
        }
        [TestMethod]
        public void TestMax()
        {
            List<Plant> plants = MulInput();
            Radiation no = NoRadiation.GetInstance();
            int days = 3;
            for (int i = 0; i < days; i++)
            {
                foreach (Plant plant in plants)
                {
                    plant.Traverse(no);
                }
                plants.RemoveAll(plant => !plant.IsAlive());
                no = Program.DetermineNextRadiation(plants);
            }
            Assert.AreEqual("Smallo", Program.FindStrongestPlant(plants));
        }
        [TestMethod]
        // test for alpha
        public void TestAlpha()
        {
            List<Plant> plants = TestInput();
            Radiation alpha = Alpha.GetInstance();
            foreach (Plant plant in plants)
            {
                plant.Traverse(alpha);
            }
            // assert
            Assert.AreEqual(9, plants[0].nutrientLevel);//wom 7
            Assert.AreEqual(2, plants[1].nutrientLevel);//wit 5
            Assert.AreEqual(5, plants[2].nutrientLevel);//wor 4
            Assert.AreEqual(0, plants[3].nutrientLevel);//wit 3
        }
        [TestMethod]
        // test for delta
        public void TestDelta()
        {
            List<Plant> plants = TestInput();
            Radiation delta = Delta.GetInstance();
            foreach (Plant plant in plants)
            {
                plant.Traverse(delta);
            }
            // assert
            Assert.AreEqual(5, plants[0].nutrientLevel);//wom 7
            Assert.AreEqual(9, plants[1].nutrientLevel);//wit 5
            Assert.AreEqual(5, plants[2].nutrientLevel);//wor 4
            Assert.AreEqual(7, plants[3].nutrientLevel);//wit 3
        }
        [TestMethod]
        public void TestNoRadiation()
        {
            List<Plant> plants = TestInput();
            Radiation no = NoRadiation.GetInstance();
            foreach (Plant plant in plants)
            {
                plant.Traverse(no);
            }
            // assert
            Assert.AreEqual(6, plants[0].nutrientLevel);//wom 7
            Assert.AreEqual(4, plants[1].nutrientLevel);//wit 5
            Assert.AreEqual(3, plants[2].nutrientLevel);//wor 4
            Assert.AreEqual(2, plants[3].nutrientLevel);//wit 3
        }
        [TestMethod]
        // test womble root death
        public void TestWomblerootDeath()
        {
            Radiation a = Alpha.GetInstance();
            Plant w = new Wombleroot("Huy", 10);
            // alpha will increase its nutrient level by 2 so at >10 it will die
            w.Traverse(a);
            Assert.IsFalse(w.IsAlive());
        }
        [TestMethod]
        // test wittentoot death
        public void TestWittentootDeath()
        {
            Radiation a = Alpha.GetInstance();
            Plant w = new Wittentoot("lam", 3);
            // alpha will increase its nutrient level by 2 so at >10 it will die
            w.Traverse(a);          
            Assert.IsFalse(w.IsAlive());
        }
        [TestMethod]
        // test woreroot death
        public void TestWorerootDeath()
        {
            Radiation a = NoRadiation.GetInstance();
            Plant w = new Woreroot("Big", 1);
            w.Traverse(a);
            Assert.IsFalse(w.IsAlive());

        }
        [TestMethod]
        public void TestOthersDeath()
        {
            Radiation a = Alpha.GetInstance();
            Radiation noRad = NoRadiation.GetInstance();
            Plant wit = new Wittentoot("Lanky", 2); //-1
            Plant wor = new Woreroot("Big", 1); //0
            wit.Traverse(a);
            wor.Traverse(noRad);
            Assert.IsFalse(wit.IsAlive());
            Assert.IsFalse(wor.IsAlive());
        }
        // test for the strongest plant
        [TestMethod]
        public void TestStrongestPlant()
        {
            List<Plant> plants = TestInput();
            Assert.AreEqual("Hungry", Program.FindStrongestPlant(plants));
        }        
    }
}