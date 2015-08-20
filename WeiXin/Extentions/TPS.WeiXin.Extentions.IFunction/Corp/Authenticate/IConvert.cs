namespace TPS.WeiXin.Extentions.IFunction.Corp.Authenticate
{
    public interface IConvert
    {
        string ConvertToOpenID(string userID);

        string ConvertToUserID(string openID);
    }
}