using Soda.PineappleBunsWin.Shared;

namespace Soda.PineappleBunsWin.Core;

public interface IRefreshWindow
{
    void Update(ShortCutBox box);
    void Refresh();
}