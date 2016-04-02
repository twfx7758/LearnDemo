using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AOP.Interface
{
    public interface IInterception
    {
        /// <summary>
        /// 方法执行前
        /// </summary>
        void PreInvoke();

        /// <summary>
        /// 方法执行后
        /// </summary>
        void PostInvoke();

        /// <summary>
        /// 方法执行过程中，发生异常处理
        /// </summary>
        void ExceptionHandle();
    }
}
