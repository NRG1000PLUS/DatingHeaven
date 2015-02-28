﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("DbModel", "MemberBaseEntity", "Member", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(DatingHeaven.Entities.Domain.Member), "BaseEntity", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(DatingHeaven.Entities.Domain.BaseEntity))]

#endregion

namespace DatingHeaven.Entities.Domain
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class DbModelContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new DbModelContainer object using the connection string found in the 'DbModelContainer' section of the application configuration file.
        /// </summary>
        public DbModelContainer() : base("name=DbModelContainer", "DbModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DbModelContainer object.
        /// </summary>
        public DbModelContainer(string connectionString) : base(connectionString, "DbModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new DbModelContainer object.
        /// </summary>
        public DbModelContainer(EntityConnection connection) : base(connection, "DbModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<BaseEntity> BaseEntitySet
        {
            get
            {
                if ((_BaseEntitySet == null))
                {
                    _BaseEntitySet = base.CreateObjectSet<BaseEntity>("BaseEntitySet");
                }
                return _BaseEntitySet;
            }
        }
        private ObjectSet<BaseEntity> _BaseEntitySet;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Member> Members
        {
            get
            {
                if ((_Members == null))
                {
                    _Members = base.CreateObjectSet<Member>("Members");
                }
                return _Members;
            }
        }
        private ObjectSet<Member> _Members;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the BaseEntitySet EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToBaseEntitySet(BaseEntity baseEntity)
        {
            base.AddObject("BaseEntitySet", baseEntity);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Members EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMembers(Member member)
        {
            base.AddObject("Members", member);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DbModel", Name="BaseEntity")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    [KnownTypeAttribute(typeof(Message))]
    public abstract partial class BaseEntity : EntityObject
    {
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                OnCreatedOnChanging(value);
                ReportPropertyChanging("CreatedOn");
                _CreatedOn = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CreatedOn");
                OnCreatedOnChanged();
            }
        }
        private global::System.DateTime _CreatedOn;
        partial void OnCreatedOnChanging(global::System.DateTime value);
        partial void OnCreatedOnChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ModifiedOn
        {
            get
            {
                return _ModifiedOn;
            }
            set
            {
                OnModifiedOnChanging(value);
                ReportPropertyChanging("ModifiedOn");
                _ModifiedOn = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ModifiedOn");
                OnModifiedOnChanged();
            }
        }
        private global::System.Int32 _ModifiedOn;
        partial void OnModifiedOnChanging(global::System.Int32 value);
        partial void OnModifiedOnChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsDeleted
        {
            get
            {
                return _IsDeleted;
            }
            set
            {
                OnIsDeletedChanging(value);
                ReportPropertyChanging("IsDeleted");
                _IsDeleted = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsDeleted");
                OnIsDeletedChanged();
            }
        }
        private global::System.Boolean _IsDeleted;
        partial void OnIsDeletedChanging(global::System.Boolean value);
        partial void OnIsDeletedChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("DbModel", "MemberBaseEntity", "Member")]
        public Member Member
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Member>("DbModel.MemberBaseEntity", "Member").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Member>("DbModel.MemberBaseEntity", "Member").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Member> MemberReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Member>("DbModel.MemberBaseEntity", "Member");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Member>("DbModel.MemberBaseEntity", "Member", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DbModel", Name="Member")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Member : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Member object.
        /// </summary>
        /// <param name="login">Initial value of the Login property.</param>
        /// <param name="firstName">Initial value of the FirstName property.</param>
        /// <param name="lastName">Initial value of the LastName property.</param>
        /// <param name="id">Initial value of the Id property.</param>
        public static Member CreateMember(global::System.String login, global::System.String firstName, global::System.String lastName, global::System.Int32 id)
        {
            Member member = new Member();
            member.Login = login;
            member.FirstName = firstName;
            member.LastName = lastName;
            member.Id = id;
            return member;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Login
        {
            get
            {
                return _Login;
            }
            set
            {
                OnLoginChanging(value);
                ReportPropertyChanging("Login");
                _Login = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Login");
                OnLoginChanged();
            }
        }
        private global::System.String _Login;
        partial void OnLoginChanging(global::System.String value);
        partial void OnLoginChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                OnFirstNameChanging(value);
                ReportPropertyChanging("FirstName");
                _FirstName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("FirstName");
                OnFirstNameChanged();
            }
        }
        private global::System.String _FirstName;
        partial void OnFirstNameChanging(global::System.String value);
        partial void OnFirstNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                OnLastNameChanging(value);
                ReportPropertyChanging("LastName");
                _LastName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LastName");
                OnLastNameChanged();
            }
        }
        private global::System.String _LastName;
        partial void OnLastNameChanging(global::System.String value);
        partial void OnLastNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("DbModel", "MemberBaseEntity", "BaseEntity")]
        public BaseEntity BaseEntity
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BaseEntity>("DbModel.MemberBaseEntity", "BaseEntity").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BaseEntity>("DbModel.MemberBaseEntity", "BaseEntity").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BaseEntity> BaseEntityReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BaseEntity>("DbModel.MemberBaseEntity", "BaseEntity");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BaseEntity>("DbModel.MemberBaseEntity", "BaseEntity", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="DbModel", Name="Message")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Message : BaseEntity
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Message object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="createdOn">Initial value of the CreatedOn property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="isDeleted">Initial value of the IsDeleted property.</param>
        /// <param name="body">Initial value of the Body property.</param>
        /// <param name="senderId">Initial value of the SenderId property.</param>
        /// <param name="receiverId">Initial value of the ReceiverId property.</param>
        public static Message CreateMessage(global::System.Int32 id, global::System.DateTime createdOn, global::System.Int32 modifiedOn, global::System.Boolean isDeleted, global::System.String body, global::System.Int32 senderId, global::System.Int32 receiverId)
        {
            Message message = new Message();
            message.Id = id;
            message.CreatedOn = createdOn;
            message.ModifiedOn = modifiedOn;
            message.IsDeleted = isDeleted;
            message.Body = body;
            message.SenderId = senderId;
            message.ReceiverId = receiverId;
            return message;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Body
        {
            get
            {
                return _Body;
            }
            set
            {
                OnBodyChanging(value);
                ReportPropertyChanging("Body");
                _Body = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Body");
                OnBodyChanged();
            }
        }
        private global::System.String _Body;
        partial void OnBodyChanging(global::System.String value);
        partial void OnBodyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 SenderId
        {
            get
            {
                return _SenderId;
            }
            set
            {
                OnSenderIdChanging(value);
                ReportPropertyChanging("SenderId");
                _SenderId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SenderId");
                OnSenderIdChanged();
            }
        }
        private global::System.Int32 _SenderId;
        partial void OnSenderIdChanging(global::System.Int32 value);
        partial void OnSenderIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ReceiverId
        {
            get
            {
                return _ReceiverId;
            }
            set
            {
                OnReceiverIdChanging(value);
                ReportPropertyChanging("ReceiverId");
                _ReceiverId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ReceiverId");
                OnReceiverIdChanged();
            }
        }
        private global::System.Int32 _ReceiverId;
        partial void OnReceiverIdChanging(global::System.Int32 value);
        partial void OnReceiverIdChanged();

        #endregion

    
    }

    #endregion

    
}