using Lesson1.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Model
{
    public abstract class Duck
    {
        public Duck() { }

        public FlyBehavior flyBehavior;
        public QuackBehavior quackBehavior;

        public void performQuack()
        {
            quackBehavior.quack();
        }

        public void performFly()
        {
            flyBehavior.fly();
        }

        public void setFlyBehavior(FlyBehavior fb)
        {
            flyBehavior = fb;
        }

        public void setQuackBehavior(QuackBehavior qb)
        {
            quackBehavior = qb;
        }
        
        public abstract void display();

        public void swim()
        {
            Console.WriteLine("All ducks float,even decoys");
        }



    }
}
