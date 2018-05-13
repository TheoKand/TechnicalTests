using System;

namespace Test
{

    public class Egg
    {
        private Func<IBird> hatchBirdFunctionPointer;

        public Egg(Func<IBird> createBird)
        {
            hatchBirdFunctionPointer = createBird;
        }

        public IBird Hatch()
        {
            if (hatchBirdFunctionPointer!=null)
            {
                IBird bird = hatchBirdFunctionPointer.Invoke();
                hatchBirdFunctionPointer = null;
                return bird;
            } else
            {
                throw new System.InvalidCastException();
            }

        }
    }	
	
    
	public interface IBird
    {
        Egg Lay();

    }
	

    // Should implement IBird
    public class Chicken : IBird
    {
        public Chicken()
        {
        }

        public Egg Lay()
        {
            Func<IBird> hatchFunc = () => new Chicken();
            Egg egg = new Egg(hatchFunc);
            return egg;
        }
    }

    public class Crow : IBird
    {
        public Crow()
        {
        }

        public Egg Lay()
        {
            Func<IBird> hatchFunc = () => new Crow();
            Egg egg = new Egg(hatchFunc);
            return egg;
        }
    }

	
    public class Program
    {
        public static void Main(string[] args)
        {
            var chicken1 = new Chicken();
            var chickenEgg = chicken1.Lay();
            var childChicken = chickenEgg.Hatch();


            var crow = new Crow();
            var crowEgg = crow.Lay();
            var childCrow = crowEgg.Hatch();
        }
    }
}