using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;
using Microsoft.CSharp;
using System.Runtime;

namespace StreamTunnel
{
    class RegOperation
    {
        public RegOperation()
        {

        }
        public void SetAutoStartUpReg(string path, string keyName)
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey subkey = key.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            subkey.SetValue(keyName, path);
        }
        public object GetAutoStartUpRegKeyValue(string keyName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            return key.GetValue(keyName);
        }
        public void DeleteAutoStartUpReg(string keyName)
        {
            /*
            RegistryKey key = Registry.LocalMachine;
            key.DeleteSubKey()
            */
        }
    }
}
