using System;
using System.Linq;
using System.Linq.Expressions;

namespace AtomicCore
{
    /// <summary>
    /// 表达式计算帮助类
    /// </summary>
    public static class ExpressionCalculater
    {
        /// <summary>
        /// 判断表达式中是否包含的有参数
        /// </summary>
        /// <param name="exp">需要被判断的表达式</param>
        /// <returns></returns>
        public static bool IsExistsParameters(Expression exp)
        {
            ExpressionParameterVisitor entity = new ExpressionParameterVisitor(exp);
            return entity.ParameterTypes.Count() > 0;
        }

        /// <summary>
        /// 判断表达式中是否包含的有参数
        /// </summary>
        /// <param name="exp">需要被判断的表达式</param>
        /// <param name="paramType">参数类型</param>
        /// <returns></returns>
        public static bool IsExistsParameters(Expression exp, out Type paramType)
        {
            ExpressionParameterVisitor entity = new ExpressionParameterVisitor(exp);
            if (entity.ParameterTypes.Count() > 0)
            {
                paramType = entity.ParameterTypes.First();
                return true;
            }
            else
            {
                paramType = null;
                return false;
            }
        }

        /// <summary>
        /// 计算表达式的值
        /// </summary>
        /// <param name="expression">需要被计算的表达式</param>
        /// <param name="args">参与表达式计算的参数列表</param>
        /// <returns></returns>
        public static object GetValue(Expression expression, params object[] args)
        {
            object expVal = null;
            LambdaExpression lambdaExp = Expression.Lambda(expression);
            Delegate currentDelegate = lambdaExp.Compile();
            expVal = currentDelegate.DynamicInvoke(args);
            return expVal;
        }
    }
}
