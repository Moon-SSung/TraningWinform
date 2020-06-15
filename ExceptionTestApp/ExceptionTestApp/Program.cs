using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 100, y = 5, value = 0;

            try
            {
                value = x / y;
                Console.WriteLine($"{x} / {y} = {value} ");
                //throw new Exception("사용자 에러");
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine("2. y의 값을 0보다 크게 입력하세요");
            }
            catch (Exception ex)
            {
                Console.WriteLine("3." + ex.Message);
            }
            finally //에러가 발생해도 메세지가 출력되고 발생하지 않아도 출력된다..
            {
                Console.WriteLine("4. 프로그램이 종료했습니다.");
            }
        }
    }
}
