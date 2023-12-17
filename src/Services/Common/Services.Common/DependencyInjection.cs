using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SingletonInjectionAttribute(string key = default) : Attribute
{
    public string Key { get; init; } = key;
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ScopedInjectionAttribute(string key = default) : Attribute
{
    public string Key { get; init; } = key;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TransientInjectionAttribute(string key = default) : Attribute
{
    public string Key { get; init; } = key;
}
