// Copyright (c) .NET Foundation and Contributors.  All Rights Reserved.  See LICENSE in the project root for license information.
using System;
using System.Runtime.InteropServices;

namespace TorchSharp
{
    public static partial class torch
    {
        public static partial class nn
        {
            public static partial class functional
            {
                [DllImport("LibTorchSharp")]
                extern static IntPtr THSNN_batch_norm(IntPtr input, IntPtr running_mean, IntPtr running_var, IntPtr weight, IntPtr bias, bool training, double momentum, double eps);

                /// <summary>
                /// Applies Batch Normalization for each channel across a batch of data.
                /// </summary>
                public static Tensor batch_norm(Tensor input, Tensor running_mean, Tensor running_var, Tensor weight = null, Tensor bias = null, bool training = false, double momentum = 0.1, double eps = 1e-5)
                {
                    var res = THSNN_batch_norm(
                        input.Handle,
                        running_mean.Handle,
                        running_var.Handle,
                        weight is not null ? weight.Handle : IntPtr.Zero,
                        bias is not null ? bias.Handle : IntPtr.Zero,
                        training,
                        momentum, eps);
                    if (res == IntPtr.Zero)
                        torch.CheckForErrors();
                    return new Tensor(res);
                }

                [DllImport("LibTorchSharp")]
                extern static IntPtr THSNN_group_norm(IntPtr input, long num_groups, IntPtr weight, IntPtr bias, double eps);

                /// <summary>
                /// Applies Group Normalization for last certain number of dimensions.
                /// </summary>
                public static Tensor group_norm(Tensor input, long num_groups, Tensor weight = null, Tensor bias = null, double eps = 1e-5)
                {
                    var res = THSNN_group_norm(
                        input.Handle,
                        num_groups,
                        weight is not null ? weight.Handle : IntPtr.Zero,
                        bias is not null ? bias.Handle : IntPtr.Zero,
                        eps);
                    if (res == IntPtr.Zero)
                        torch.CheckForErrors();
                    return new Tensor(res);
                }

                [DllImport("LibTorchSharp")]
                extern static IntPtr THSNN_instance_norm(IntPtr input, IntPtr running_mean, IntPtr running_var, IntPtr weight, IntPtr bias, bool use_input_stats, double momentum, double eps);

                /// <summary>
                /// Applies Instance Normalization for each channel in each data sample in a batch.
                /// </summary>
                public static Tensor instance_norm(Tensor input, Tensor running_mean = null, Tensor running_var = null, Tensor weight = null, Tensor bias = null, bool use_input_stats = true, double momentum = 0.1, double eps = 1e-5)
                {
                    var res = THSNN_instance_norm(
                        input.Handle,
                        running_mean is not null ? running_mean.Handle : IntPtr.Zero,
                        running_var is not null ? running_var.Handle : IntPtr.Zero,
                        weight is not null ? weight.Handle : IntPtr.Zero,
                        bias is not null ? bias.Handle : IntPtr.Zero,
                        use_input_stats,
                        momentum, eps);
                    if (res == IntPtr.Zero)
                        torch.CheckForErrors();
                    return new Tensor(res);
                }

                [DllImport("LibTorchSharp")]
                extern static unsafe IntPtr THSNN_layer_norm(IntPtr input, long* normalized_shape, long normalized_shape_len, IntPtr weight, IntPtr bias, double eps);

                /// <summary>
                /// Applies Layer Normalization for last certain number of dimensions.
                /// </summary>
                public static Tensor layer_norm(Tensor input, long[] normalized_shape, Tensor weight = null, Tensor bias = null, double eps = 1e-5)
                {
                    IntPtr res;
                    unsafe {
                        fixed (long* normalized_shape_ptr = normalized_shape) {
                            res = THSNN_layer_norm(
                                input.Handle,
                                normalized_shape_ptr,
                                normalized_shape.LongLength,
                                weight is not null ? weight.Handle : IntPtr.Zero,
                                bias is not null ? bias.Handle : IntPtr.Zero,
                                eps);
                        }
                    }
                    if (res == IntPtr.Zero)
                        torch.CheckForErrors();
                    return new Tensor(res);
                }

                [DllImport("LibTorchSharp")]
                extern static IntPtr THSNN_local_response_norm(IntPtr input, long size, double alpha, double beta, double k);

                /// <summary>
                /// Applies Local Normalization.
                /// </summary>
                public static Tensor local_response_norm(Tensor input, long size, double alpha = 0.0001, double beta = 0.75, double k = 1.0)
                {
                    var res = THSNN_local_response_norm(input.Handle, size, alpha, beta, k);
                    if (res == IntPtr.Zero)
                        torch.CheckForErrors();
                    return new Tensor(res);
                }
            }
        }
    }
}
