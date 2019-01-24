using System;
namespace TesteRoslyn {
    
    [Attribute1]
	public class TestClass {

        private int field1;
        private string field2;

        public void method1(){
            
        }

        public double method2(double x, double y){
            return x + y;
        }


    }
}
