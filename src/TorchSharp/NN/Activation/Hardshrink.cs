// Copyright (c) .NET Foundation and Contributors.  All Rights Reserved.  See LICENSE in the project root for license information.
using System;
using System.Runtime.InteropServices;
using static TorchSharp.torch;

namespace TorchSharp
{
    using Modules;

    namespace Modules
    {
        /// <summary>
        /// This class is used to represent a Hardshrink module.
        /// </summary>
        public class Hardshrink : torch.nn.Module
        {
            internal Hardshrink(IntPtr handle, IntPtr boxedHandle) : base(handle, boxedHandle) { }

            [DllImport("LibTorchSharp")]
            private static extern IntPtr THSNN_Hardshrink_forward(torch.nn.Module.HType module, IntPtr tensor);

            public override Tensor forward(Tensor tensor)
            {
                var res = THSNN_Hardshrink_forward(handle, tensor.Handle);
                if (res == IntPtr.Zero) { torch.CheckForErrors(); }
                return new Tensor(res);
            }

            public override string GetName()
            {
                return typeof(Hardshrink).Name;
            }
        }
    }

    public static partial class torch
    {
        public static partial class nn
        {
            [DllImport("LibTorchSharp")]
            extern static IntPtr THSNN_Hardshrink_ctor(double lambd, out IntPtr pBoxedModule);

            /// <summary>
            /// Hardshrink
            /// </summary>
            /// <param name="lambda"> the λ value for the Hardshrink formulation. Default: 0.5</param>
            /// <returns></returns>
            static public Hardshrink Hardshrink(double lambda = 0.5)
            {
                var handle = THSNN_Hardshrink_ctor(lambda, out var boxedHandle);
                if (handle == IntPtr.Zero) { torch.CheckForErrors(); }
                return new Hardshrink(handle, boxedHandle);
            }

            public static partial class functional
            {
                /// <summary>
                /// Hardshrink
                /// </summary>
                /// <param name="x">The input tensor</param>
                /// <param name="lambda">The λ value for the Hardshrink formulation. Default: 0.5</param>
                /// <returns></returns>
                static public Tensor Hardshrink(Tensor x, double lambda = 0.5)
                {
                    using (var m = nn.Hardshrink(lambda)) {
                        return m.forward(x);
                    }
                }
            }
        }
    }
}
