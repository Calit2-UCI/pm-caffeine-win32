# pm-caffeine-win32

Command line C# program which takes one argument, a number of minutes, and tells the OS to not go to standby or turn off the display for that long.

## Example Usage

`caffeine-win32.exe 5` -- the program will prevent sleep for 5 minutes.

## Notes

* You can use `powercfg -requests` to see if it has actually registered itself with the OS.
* Although the program will auto-close after the time has passed, if you force-close it, it will still "unregister" from the OS, due to the nature of the Windows power management API, which does not allow a program to prevent sleep unless it is actually running.
* Sleep is not prevented through the act of doing extra processing. This program will remain idle. It simply calls a function in Windows, sets a timer to auto-exit in the future, and then idles.
* This project is not used because it was made to go along with an electron app, but it was later discovered that the current stable version of [electron has this feature built-in](https://github.com/electron/electron/blob/master/docs/api/power-save-blocker.md)
