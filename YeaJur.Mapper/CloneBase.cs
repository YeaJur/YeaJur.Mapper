#region YeaJur.Mapper 4.0.30319.42000

/***
 *
 *	本代码版权归  侯兴鼎（YeaJur） 所有，All Rights Reserved (C) 2017
 * 	CLR版本：4.0.30319.42000
 *	唯一标识：e569b0b5-7de8-41b2-8fe0-e46c5ba366dc
 **
 *	所属域：DESKTOP-Q9MAAK4
 *	机器名称：DESKTOP-Q9MAAK4
 *	登录用户：houxi
 *	创建时间：2017/1/17 20:49:02
 *	作者：侯兴鼎（YeaJur）
 *	E_mail：houxingding@hotmail.com
 **
 *	命名空间：YeaJur.Mapper
 *	类名称：CloneBase
 *	文件名：CloneBase
 *	文件描述：
 *
 ***/

#endregion

namespace YeaJur.Mapper
{
    /// <summary>
    /// 克隆实体 抽象类
    /// </summary>
    public abstract class CloneBase<T>
    {
        /// <summary>
        /// 浅克隆 实体对象
        /// </summary>
        /// <returns></returns>
        public T Clone()
        {
            return (T)MemberwiseClone();
        }
    }
}