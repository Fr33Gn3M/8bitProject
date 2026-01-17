using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.BlackBox.Main
{
    public class MainViewModel : Screen
    {
        public string Name { get; set; } = "waku";

        public void SayHello() => Name = "Hello " + Name;    // C#6的语法, 表达式方法
    }
}
