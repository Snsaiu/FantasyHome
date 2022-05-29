// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

namespace FantasyHomeCenter.Client.Base
{
    public interface IClientLocalizer
    {
        string this[string name]
        {
            get;
        }
        public string Combination(params string[] names);
    }
}
