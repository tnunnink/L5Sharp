namespace L5Sharp.Core;

/// <summary>
/// 
/// </summary>
public interface IBlockWireBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    IBlockWireBuilder From(TagName source);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Block To(uint id, TagName param);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="taget"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Block To(Block taget, TagName param);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="taget"></param>
    /// <returns></returns>
    Block To(TagName taget);
}