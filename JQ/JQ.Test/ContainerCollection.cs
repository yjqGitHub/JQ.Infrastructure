using Xunit;

namespace JQ.Test
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ContainerCollection.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：ContainerCollection
    /// 创建标识：yjq 2017/5/14 15:37:48
    /// </summary>
    [CollectionDefinition("ContainerCollection")]
    public class ContainerCollection : ICollectionFixture<ContainerFixture>
    {
    }
}