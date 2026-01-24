﻿namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants;

public static class NetworkingDscConstants
{
    public static class Firewall
    {
        public const string ResourceId = "Firewall";
        
        public static class Parameters
        {
            public const string Enabled = "Enabled";
            
            public const string Name = "Name";
            
            public const string Group = "Group";
            
            public const string DisplayName = "DisplayName";
            
            public const string Action = "Action";
            
            public const string Direction = "Direction";
            
            public const string Protocol = "Protocol";
            
            public const string RemoteMachine = "RemoteMachine";
            
            public const string Authentication = "Authentication";
            
            public const string Encryption = "Encryption";
            
            public const string LocalPort = "LocalPort";
            
            public const string RemotePort = "RemotePort";
            
            public const string LocalAddress = "LocalAddress";
            
            public const string RemoteAddress = "RemoteAddress";
        }
    }
    
    public static class DnsClientGlobalSetting
    {
        public const string ResourceId = "DnsClientGlobalSetting";
        
        public static class Properties
        {
            public const string IsSingleInstance = "IsSingleInstance";
            
            public const string SuffixSearchList = "SuffixSearchList";
            
            public const string UseDevolution = "UseDevolution";
            
            public const string DevolutionLevel = "DevolutionLevel";
        }
    }
}