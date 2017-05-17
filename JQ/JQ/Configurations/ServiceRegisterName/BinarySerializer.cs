namespace JQ.Configurations.ServiceRegisterName
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BinarySerializer.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：BinarySerializer注册的名字
    /// 创建标识：yjq 2017/5/13 16:44:14
    /// </summary>
    public static partial class Register
    {
        /// <summary>
        /// 默认字节序列化服务注册的名字
        /// </summary>
        public const string REGISTER_NAME_DEFAULTBINARY = "DefaultBinary";

        /// <summary>
        /// json字节序列化服务注册的名字
        /// </summary>
        public const string REGISTER_NAME_JSONBINARY = "JsonBinary";

        /// <summary>
        /// protobuf字节序列化名字
        /// </summary>
        public const string REGISTER_NAME_PROTOBUFBINARY = "ProtobufBinary";
    }
}