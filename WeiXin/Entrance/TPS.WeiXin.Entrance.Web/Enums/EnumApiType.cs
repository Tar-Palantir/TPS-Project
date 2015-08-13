namespace TPS.WeiXin.Entrance.Web.Enums
{
    /// <summary>
    /// Api类型枚举
    /// </summary>
    public enum EnumApiType
    {
        /// <summary>
        /// 发送消息-客服
        /// </summary>
        SendMsg_CustomerService = 12,

        /// <summary>
        /// 发送消息-群发
        /// </summary>
        SendMsg_GroupSend = 13,

        /// <summary>
        /// 发送消息-模板消息
        /// </summary>
        SendMsg_TemplateMsg = 14,
        
        /// <summary>
        /// 素材管理-新增临时素材
        /// </summary>
        MaterialManage_CreateTemp = 31,

        /// <summary>
        /// 素材管理-获取临时素材
        /// </summary>
        MaterialManage_GetTemp = 32,

        /// <summary>
        /// 素材管理-新增永久素材
        /// </summary>
        MaterialManage_CreatePermanent = 33,

        /// <summary>
        /// 素材管理-获取永久素材
        /// </summary>
        MaterialManage_GetPermanent = 34,

        /// <summary>
        /// 素材管理-删除永久素材
        /// </summary>
        MaterialManage_DeletePermanent = 35,

        /// <summary>
        /// 素材管理-修改永久图文素材
        /// </summary>
        MaterialManage_UpdatePermanentArticle = 36,

        /// <summary>
        /// 素材管理-获取素材总数
        /// </summary>
        MaterialManage_GetTotalCount = 37,

        /// <summary>
        /// 素材管理-获取素材列表
        /// </summary>
        MaterialManage_GetAll = 38,

        /// <summary>
        /// 用户管理-分组管理
        /// </summary>
        UserManage_Group = 41,

        /// <summary>
        /// 用户管理-设置用户备注名
        /// </summary>
        UserManage_SetRemark = 42,

        /// <summary>
        /// 用户管理-获取用户基本信息
        /// </summary>
        UserManage_GetUserBaseInfo = 43,

        /// <summary>
        /// 用户管理-获取用户列表
        /// </summary>
        UserManage_GetUserList = 44,

        /// <summary>
        /// 用户管理-获取用户地理位置
        /// </summary>
        UserManage_GetUserLocation = 45,

        /// <summary>
        /// 自定义菜单-菜单创建
        /// </summary>
        CustomMenu_Create = 51,

        /// <summary>
        /// 自定义菜单-菜单查询
        /// </summary>
        CustomMenu_GetAll = 52,

        /// <summary>
        /// 自定义菜单-菜单删除
        /// </summary>
        CustomMenu_Delete = 53,

        /// <summary>
        /// 自定义菜单-获取菜单配置信息
        /// </summary>
        CustomMenu_GetAllInfo = 54,

        /// <summary>
        /// 账号管理-生成二维码
        /// </summary>
        AccountManage_GenerateQrcode = 61,

        /// <summary>
        /// 账号管理-长链接转短链接
        /// </summary>
        AccountManage_Long2ShortUrl = 62,

        /// <summary>
        /// 数据统计-用户数据
        /// </summary>
        DataStatistics_UserData = 71,

        /// <summary>
        /// 数据统计-图文消息数据
        /// </summary>
        DataStatistics_ArticleData = 72,

        /// <summary>
        /// 数据统计-消息数据
        /// </summary>
        DataStatistics_MsgData = 73,

        /// <summary>
        /// 数据统计-接口数据
        /// </summary>
        DataStatistics_ApiData = 74
    }
}