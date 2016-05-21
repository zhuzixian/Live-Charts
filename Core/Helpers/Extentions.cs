﻿//The MIT License(MIT)

//copyright(c) 2016 Alberto Rodriguez

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace LiveCharts.Helpers
{
    public class CrossNet
    {
        public Type Type { get; set; }
    }

    public static class Extentions
    {

        public static CrossNet AsCrossNet(this Type type)
        {
            return new CrossNet {Type = type};
        }

        public static bool IsAssignableFrom(this CrossNet cn, Type from)
        {
#if RUNNING_ON_4
            return cn.Type.IsAssignableFrom(from);
#endif
#if NOT_RUNNING_ON_4
            return cn.Type.GetTypeInfo().IsAssignableFrom(from.GetTypeInfo());
#endif
        }

        public static bool IsClass(this CrossNet cn)
        {
#if RUNNING_ON_4
            return cn.Type.IsClass;
#endif
#if NOT_RUNNING_ON_4
            return cn.Type.GetTypeInfo().IsClass;
#endif
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> predicate)
        {
            foreach (var item in source) predicate(item);
        }

        public static IEnumerable<List<ChartPoint>> SplitEachNaN(this List<ChartPoint> toSplit)
        {
            var l = new List<ChartPoint>(toSplit.Count);
            var acum = -1;

            foreach (var point in toSplit)
            {
                if (double.IsNaN(point.X) || double.IsNaN(point.Y))
                {
                    yield return l;
                    acum += l.Count;
                    l = new List<ChartPoint>(toSplit.Count - acum);
                }
                else
                {
                    l.Add(point);
                }
            }

            yield return l;
        }
    }
}
