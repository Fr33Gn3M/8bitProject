using Lesson1.Model;
using Lesson1.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            //向上转型
            //Duck mallard = new MallardDuck();
            //mallard.performQuack();
            //mallard.performFly();
            Duck model = new ModelDuck();
            model.performFly();
            model.setFlyBehavior(new FlyRocketPowered());
            model.performFly();
        }
    }
}
