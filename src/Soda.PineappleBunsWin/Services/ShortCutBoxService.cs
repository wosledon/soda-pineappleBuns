using System.Collections.Concurrent;
using System.Windows;
using IWshRuntimeLibrary;
using Soda.PineappleBunsWin.Core;
using Soda.PineappleBunsWin.Helpers;
using Soda.PineappleBunsWin.Pages;
using Soda.PineappleBunsWin.Shared;

namespace Soda.PineappleBunsWin.Services;

public class ShortCutBoxService
{
    private readonly ConcurrentDictionary<string, Window> _shortCutBoxes = new();

    public void ShowShortCutBox(string path)
    {
        if (_shortCutBoxes.TryGetValue(path, out var window))
        {
            window.Activate();
            return;
        }

        var box = new ShortCutBox(path);
        var shortCutBoxWindow = new ShortCutBoxWindow(box);
        shortCutBoxWindow.Show();
        _shortCutBoxes.TryAdd(path, shortCutBoxWindow);
    }

    public void ShowAllShortCutBoxes()
    {
        var paths = FileHelper.GetDirectories();

        foreach (var path in paths)
        {
            ShowShortCutBox(path);
        }
    }

    public void CloseShortCutBox(string path)
    {
        if (_shortCutBoxes.TryRemove(path, out var window))
        {
            window.Close();
        }
    }

    public void CloseAllShortCutBox()
    {
        foreach (var window in _shortCutBoxes.Values)
        {
            window.Close();
        }
    }

    public void RefreshShortCutBox(string path)
    {
        if (!_shortCutBoxes.TryGetValue(path, out var window)) return;
        if (window is IRefreshWindow refreshWindow)
        {
            refreshWindow.Refresh();
        }
    }

    public void RefreshAllShortCutBox()
    {
        foreach (var window in _shortCutBoxes.Values)
        {
            if (window is IRefreshWindow refreshWindow)
            {
                refreshWindow.Refresh();
            }
        }
    }

    public void RemoveShortCutBox(string path)
    {
        if (_shortCutBoxes.TryRemove(path, out var window))
        {
            window.Close();
        }
    }

    public void RemoveAllShortCutBox()
    {
        foreach (var window in _shortCutBoxes.Values)
        {
            window.Close();
        }
        _shortCutBoxes.Clear();
    }

    public void UpdateShortCutBox(ShortCutBox box)
    {
        if (!_shortCutBoxes.TryGetValue(box.Name, out var window)) return;
        if (window is ShortCutBoxWindow shortCutBoxWindow)
        {
            shortCutBoxWindow.Update(box);
        }
    }

    public Window? GetOtherWindow(Window window)
    {
        return _shortCutBoxes.Values.FirstOrDefault(w => w != window);
    }

    public List<Window> GetWindows()
    {
        return _shortCutBoxes.Values.ToList();
    }

    public void CheckForSnap(ShortCutBoxWindow window)
    {
        foreach (var value in _shortCutBoxes.Values)
        {
            if (value is not ShortCutBoxWindow box) continue;

            if (box.Id == window.Id) continue;

            if (Math.Abs(box.Top - window.Top) < AppSettings.WindowSnap)
            {
                window.Top = value.Top;
            }

            if (Math.Abs(box.Left - window.Left) < AppSettings.WindowSnap)
            {
                window.Left = value.Left;
            }

            if (Math.Abs(box.Top + box.Height - window.Top - window.Height) < AppSettings.WindowSnap)
            {
                window.Top = value.Top + value.Height - window.Height;
            }

            if (Math.Abs(box.Left + box.Width - window.Left - window.Width) < AppSettings.WindowSnap)
            {
                window.Left = value.Left + value.Width - window.Width;
            }
        }
    }

    //添加一个新的ShortCutBox
    public void CreateShortCutBox(ShortCutBox box)
    {
        var shortCutBoxWindow = new ShortCutBoxWindow(box);
        shortCutBoxWindow.Show();
        _shortCutBoxes.TryAdd(box.Name, shortCutBoxWindow);
    }

    public IWshShortcut CreateShortCut(string shortcutPath)
    {
        WshShell shell = new WshShell();
        IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
        return shortcut;
    }

    public void CreateShortCut(string shortcutPath, string targetPath, string description, string iconLocation)
    {
        var shortcut = CreateShortCut(shortcutPath);
        shortcut.TargetPath = targetPath;
        shortcut.Description = description;
        shortcut.IconLocation = iconLocation;
        shortcut.Save();
    }
}