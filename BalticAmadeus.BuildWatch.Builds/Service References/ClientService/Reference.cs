﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BalticAmadeus.BuildWatch.Builds.ClientService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PollBuildStatusReq", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class PollBuildStatusReq : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ConfigurationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long UpdateCounterField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ConfigurationId {
            get {
                return this.ConfigurationIdField;
            }
            set {
                if ((this.ConfigurationIdField.Equals(value) != true)) {
                    this.ConfigurationIdField = value;
                    this.RaisePropertyChanged("ConfigurationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long UpdateCounter {
            get {
                return this.UpdateCounterField;
            }
            set {
                if ((this.UpdateCounterField.Equals(value) != true)) {
                    this.UpdateCounterField = value;
                    this.RaisePropertyChanged("UpdateCounter");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PollBuildStatusResp", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class PollBuildStatusResp : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BalticAmadeus.BuildWatch.Builds.ClientService.ClientEvent[] ClientEventsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BalticAmadeus.BuildWatch.Builds.ClientService.FinishedBuild[] FinishedBuildsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime FinishedBuildsDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BalticAmadeus.BuildWatch.Builds.ClientService.PictureMap[] PictureMapsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BalticAmadeus.BuildWatch.Builds.ClientService.QueuedBuild[] QueuedBuildsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long UpdateCounterField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BalticAmadeus.BuildWatch.Builds.ClientService.ClientEvent[] ClientEvents {
            get {
                return this.ClientEventsField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientEventsField, value) != true)) {
                    this.ClientEventsField = value;
                    this.RaisePropertyChanged("ClientEvents");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BalticAmadeus.BuildWatch.Builds.ClientService.FinishedBuild[] FinishedBuilds {
            get {
                return this.FinishedBuildsField;
            }
            set {
                if ((object.ReferenceEquals(this.FinishedBuildsField, value) != true)) {
                    this.FinishedBuildsField = value;
                    this.RaisePropertyChanged("FinishedBuilds");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FinishedBuildsDate {
            get {
                return this.FinishedBuildsDateField;
            }
            set {
                if ((this.FinishedBuildsDateField.Equals(value) != true)) {
                    this.FinishedBuildsDateField = value;
                    this.RaisePropertyChanged("FinishedBuildsDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BalticAmadeus.BuildWatch.Builds.ClientService.PictureMap[] PictureMaps {
            get {
                return this.PictureMapsField;
            }
            set {
                if ((object.ReferenceEquals(this.PictureMapsField, value) != true)) {
                    this.PictureMapsField = value;
                    this.RaisePropertyChanged("PictureMaps");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BalticAmadeus.BuildWatch.Builds.ClientService.QueuedBuild[] QueuedBuilds {
            get {
                return this.QueuedBuildsField;
            }
            set {
                if ((object.ReferenceEquals(this.QueuedBuildsField, value) != true)) {
                    this.QueuedBuildsField = value;
                    this.RaisePropertyChanged("QueuedBuilds");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long UpdateCounter {
            get {
                return this.UpdateCounterField;
            }
            set {
                if ((this.UpdateCounterField.Equals(value) != true)) {
                    this.UpdateCounterField = value;
                    this.RaisePropertyChanged("UpdateCounter");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClientEvent", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class ClientEvent : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FinishedBuild", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class FinishedBuild : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BuildInstanceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BuildNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ResultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimeStampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BuildInstance {
            get {
                return this.BuildInstanceField;
            }
            set {
                if ((object.ReferenceEquals(this.BuildInstanceField, value) != true)) {
                    this.BuildInstanceField = value;
                    this.RaisePropertyChanged("BuildInstance");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BuildName {
            get {
                return this.BuildNameField;
            }
            set {
                if ((object.ReferenceEquals(this.BuildNameField, value) != true)) {
                    this.BuildNameField = value;
                    this.RaisePropertyChanged("BuildName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Result {
            get {
                return this.ResultField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultField, value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string State {
            get {
                return this.StateField;
            }
            set {
                if ((object.ReferenceEquals(this.StateField, value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TimeStamp {
            get {
                return this.TimeStampField;
            }
            set {
                if ((this.TimeStampField.Equals(value) != true)) {
                    this.TimeStampField = value;
                    this.RaisePropertyChanged("TimeStamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PictureMap", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class PictureMap : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PictureHashField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PictureHash {
            get {
                return this.PictureHashField;
            }
            set {
                if ((object.ReferenceEquals(this.PictureHashField, value) != true)) {
                    this.PictureHashField = value;
                    this.RaisePropertyChanged("PictureHash");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QueuedBuild", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class QueuedBuild : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BuildNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime QueueTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BuildName {
            get {
                return this.BuildNameField;
            }
            set {
                if ((object.ReferenceEquals(this.BuildNameField, value) != true)) {
                    this.BuildNameField = value;
                    this.RaisePropertyChanged("BuildName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime QueueTime {
            get {
                return this.QueueTimeField;
            }
            set {
                if ((this.QueueTimeField.Equals(value) != true)) {
                    this.QueueTimeField = value;
                    this.RaisePropertyChanged("QueueTime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetPicturesReq", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class GetPicturesReq : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] UserNamesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] UserNames {
            get {
                return this.UserNamesField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNamesField, value) != true)) {
                    this.UserNamesField = value;
                    this.RaisePropertyChanged("UserNames");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetPicturesResp", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class GetPicturesResp : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BalticAmadeus.BuildWatch.Builds.ClientService.PictureInfo[] PicturesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BalticAmadeus.BuildWatch.Builds.ClientService.PictureInfo[] Pictures {
            get {
                return this.PicturesField;
            }
            set {
                if ((object.ReferenceEquals(this.PicturesField, value) != true)) {
                    this.PicturesField = value;
                    this.RaisePropertyChanged("Pictures");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PictureInfo", Namespace="http://schemas.datacontract.org/2004/07/BuildWatch.ControlServer")]
    [System.SerializableAttribute()]
    public partial class PictureInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] PictureDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PictureHashField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] PictureData {
            get {
                return this.PictureDataField;
            }
            set {
                if ((object.ReferenceEquals(this.PictureDataField, value) != true)) {
                    this.PictureDataField = value;
                    this.RaisePropertyChanged("PictureData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PictureHash {
            get {
                return this.PictureHashField;
            }
            set {
                if ((object.ReferenceEquals(this.PictureHashField, value) != true)) {
                    this.PictureHashField = value;
                    this.RaisePropertyChanged("PictureHash");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ClientService.IClientService")]
    public interface IClientService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientService/GetProtocolVersion", ReplyAction="http://tempuri.org/IClientService/GetProtocolVersionResponse")]
        int GetProtocolVersion();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientService/GetProtocolVersion", ReplyAction="http://tempuri.org/IClientService/GetProtocolVersionResponse")]
        System.Threading.Tasks.Task<int> GetProtocolVersionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientService/PollBuildStatus", ReplyAction="http://tempuri.org/IClientService/PollBuildStatusResponse")]
        BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusResp PollBuildStatus(BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusReq req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientService/PollBuildStatus", ReplyAction="http://tempuri.org/IClientService/PollBuildStatusResponse")]
        System.Threading.Tasks.Task<BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusResp> PollBuildStatusAsync(BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusReq req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientService/GetPictures", ReplyAction="http://tempuri.org/IClientService/GetPicturesResponse")]
        BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesResp GetPictures(BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesReq req);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientService/GetPictures", ReplyAction="http://tempuri.org/IClientService/GetPicturesResponse")]
        System.Threading.Tasks.Task<BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesResp> GetPicturesAsync(BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesReq req);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IClientServiceChannel : BalticAmadeus.BuildWatch.Builds.ClientService.IClientService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ClientServiceClient : System.ServiceModel.ClientBase<BalticAmadeus.BuildWatch.Builds.ClientService.IClientService>, BalticAmadeus.BuildWatch.Builds.ClientService.IClientService {
        
        public ClientServiceClient() {
        }
        
        public ClientServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ClientServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ClientServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ClientServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int GetProtocolVersion() {
            return base.Channel.GetProtocolVersion();
        }
        
        public System.Threading.Tasks.Task<int> GetProtocolVersionAsync() {
            return base.Channel.GetProtocolVersionAsync();
        }
        
        public BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusResp PollBuildStatus(BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusReq req) {
            return base.Channel.PollBuildStatus(req);
        }
        
        public System.Threading.Tasks.Task<BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusResp> PollBuildStatusAsync(BalticAmadeus.BuildWatch.Builds.ClientService.PollBuildStatusReq req) {
            return base.Channel.PollBuildStatusAsync(req);
        }
        
        public BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesResp GetPictures(BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesReq req) {
            return base.Channel.GetPictures(req);
        }
        
        public System.Threading.Tasks.Task<BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesResp> GetPicturesAsync(BalticAmadeus.BuildWatch.Builds.ClientService.GetPicturesReq req) {
            return base.Channel.GetPicturesAsync(req);
        }
    }
}
