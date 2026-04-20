using System;
using System.Diagnostics;

namespace Lexio.App.DI;

public class TestInjection {
    public TestInjection() {
        Debug.WriteLine("Hello constructor");
        Console.WriteLine("test");
    }
    public void SayHello() {
        Console.WriteLine("Hello world");
    }
}