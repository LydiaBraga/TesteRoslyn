using System;
namespace TesteRoslyn {

    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct)]
    public class Attribute1 : System.Attribute {
        public Attribute1() {
        }
    }
}