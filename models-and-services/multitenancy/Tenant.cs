using KpokPatagon.Data;
using System;

namespace KpokPatagon.Multitenancy
{
    /// <summary>
    /// <see cref="Tenant"/> implements a data model for tenants information.
    /// </summary>
    public class Tenant : DataModel, IEquatable<Tenant>, ITenant
    {
        /// <summary>
        /// Creates a new instance of <see cref="Tenant"/>.
        /// </summary>
        public Tenant()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Tenant"/>.
        /// </summary>
        /// <param name="id">Tenant Id.</param>
        public Tenant(int id)
        {
            Id = id;
        }


        int _id;
        /// <summary>
        /// Tenant Id.
        /// </summary>
        [IntegerDataProperty("Id", DbType.Integer, FieldName = "_id", PrimaryKey = 1, MinValue = 0, MaxValue = Int32.MaxValue)]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged(_id);
            }
        }

        int _ring;
        /// <summary>
        /// Ring number.
        /// </summary>
        [IntegerDataProperty("Ring", DbType.Integer, FieldName = "_ring", DefaultValue = 0)]
        public int Ring
        {
            get { return _ring; }
            set
            {
                _ring = value;
                NotifyPropertyChanged(_ring);
            }
        }

        string _name;
        /// <summary>
        /// The name or code of the tenant.
        /// </summary>
        [PrimaryKeyTextDataProperty("Name", DbType.Text, FieldName = "_name", Mandatory = true, MinLength = 0, MaxLength = 128)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(_name);
            }
        }

        string _displayName;
        /// <summary>
        /// Display name of this tenant.
        /// </summary>
        [TextDataProperty("DisplayName", DbType.NText, FieldName = "_displayName", Mandatory = true, MinLength = 0, MaxLength = 256)]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyPropertyChanged(_displayName);
            }
        }

        string _email;
        /// <summary>
        /// Primary tenant's e-mail.
        /// </summary>
        [EmailTextDataProperty("Email", DbType.NText, FieldName = "_email", Mandatory = true, MinLength = 0, MaxLength = 256)]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged(_email);
            }
        }

        string _culture;
        /// <summary>
        /// Culture name.
        /// </summary>
        [TextDataProperty("Culture", DbType.Text, FieldName = "_culture", Mandatory = true, MinLength = 0, MaxLength = 10)]
        public string Culture
        {
            get { return _culture; }
            set
            {
                _culture = value;
                NotifyPropertyChanged(_culture);
            }
        }

        string _mailServerId;
        /// <summary>
        /// Mail server ID.
        /// </summary>
        [TextDataProperty("MailServerId", DbType.Text, FieldName = "_mailServerId", MinLength = 0, MaxLength = 64)]
        public string MailServerId
        {
            get { return _mailServerId; }
            set
            {
                _mailServerId = value;
                NotifyPropertyChanged(_mailServerId);
            }
        }

        string _mailFromAddress;
        /// <summary>
        /// Address from which mails are sent.
        /// </summary>
        [TextDataProperty("MailFromAddress", DbType.NText, FieldName = "_mailFromAddress", MinLength = 0, MaxLength = 256)]
        public string MailFromAddress
        {
            get { return _mailFromAddress; }
            set
            {
                _mailFromAddress = value;
                NotifyPropertyChanged(_mailFromAddress);
            }
        }

        string _mailFromName;
        /// <summary>
        /// Display name for sending address.
        /// </summary>
        [TextDataProperty("MailFromName", DbType.NText, FieldName = "_mailFromName", MinLength = 0, MaxLength = 256)]
        public string MailFromName
        {
            get { return _mailFromName; }
            set
            {
                _mailFromName = value;
                NotifyPropertyChanged(_mailFromName);
            }
        }

        string _connectionString;
        /// <summary>
        /// Connection string to connect the tenant database (optional).
        /// </summary>
        [TextDataProperty("ConnectionString", DbType.NClob, FieldName = "_connectionString", Protection = ProtectionType.Encripted)]
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                NotifyPropertyChanged(_connectionString);
            }
        }

        string _provisionState;
        /// <summary>
        /// State of tenant provisioning.
        /// </summary>
        [TextDataProperty("ProvisionState", DbType.Text, FieldName = "_provisionState", 
            Mandatory = true, MinLength = 0, MaxLength = 24)]
        public ProvisionState ProvisionState
        {
            get
            {
                if (Enum.TryParse(_provisionState, out ProvisionState ps))
                {
                    return ps;
                }
                return ProvisionState.Pending;
            }
            set
            {
                _provisionState = Enum.GetName(typeof(ProvisionState), value);
                NotifyPropertyChanged(_provisionState);
            }
        }

        string _subscriptionState;
        /// <summary>
        /// State of tenant subscription
        /// </summary>
        [TextDataProperty("SubscriptionState", DbType.Text, FieldName = "_subscriptionState",
            Mandatory = true, MinLength = 0, MaxLength = 16)]
        public SubscriptionState SubscriptionState
        {
            get
            {
                if (Enum.TryParse(_subscriptionState, out SubscriptionState ps))
                {
                    return ps;
                }
                return SubscriptionState.Inactive;
            }
            set
            {
                _subscriptionState = Enum.GetName(typeof(SubscriptionState), value);
                NotifyPropertyChanged(_subscriptionState);
            }
        }

        string _subscriptionStateComment;
        /// <summary>
        /// A comment about the subscription state.
        /// </summary>
        [TextDataProperty("SubscriptionStateComment", DbType.NClob, FieldName = "_subscriptionStateComment")]
        public string SubscriptionStateComment
        {
            get { return _subscriptionStateComment; }
            set
            {
                _subscriptionStateComment = value;
                NotifyPropertyChanged(_subscriptionStateComment);
            }
        }

        int? _trialDurationInDays;
        /// <summary>
        /// Trial duration in days.
        /// </summary>
        [IntegerDataProperty("TrialDurationInDays", DbType.Integer, FieldName = "_trialDurationInDays", MinValue = 30, MaxValue = 180)]
        public int? TrialDurationInDays
        {
            get { return _trialDurationInDays; }
            set
            {
                _trialDurationInDays = value;
                NotifyPropertyChanged(_trialDurationInDays);
            }
        }

        DateTime? _trialStarted;
        /// <summary>
        /// Date and time that the trial period has started.
        /// </summary>
        [GenericDataProperty("TrialStarted", DbType.DateTime, FieldName = "_trialStarted")]
        public DateTime? TrialStarted
        {
            get { return _trialStarted; }
            set
            {
                _trialStarted = value;
                NotifyPropertyChanged(_trialStarted);
            }
        }

        DateTime? _trialEnd;
        /// <summary>
        /// Date and time that the trial period ends or ended.
        /// </summary>
        [GenericDataProperty("TrialEnd", DbType.DateTime, FieldName = "_trialEnd")]
        public DateTime? TrialEnd
        {
            get { return _trialEnd; }
            set
            {
                _trialEnd = value;
                NotifyPropertyChanged(_trialEnd);
            }
        }

        DateTime? _deletionTime;
        /// <summary>
        /// Date and time when the tenant was deleted (soft delete).
        /// </summary>
        [GenericDataProperty("DeletionTime", DbType.DateTime, FieldName = "_deletionTime")]
        public DateTime? DeletionTime
        {
            get { return _deletionTime; }
            set
            {
                _deletionTime = value;
                NotifyPropertyChanged(_deletionTime);
            }
        }


        /// <summary>
        /// Whether the tenant is deleted.
        /// </summary>
        public bool IsDeleted => DeletionTime.HasValue;


        /// <summary>
        /// Gets whether the model validation must be enforced.
        /// </summary>
        public override bool EnforceModelValidation => false;


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare this object to.</param>
        public bool Equals(Tenant other)
        {
            if (other == null) return false;
            else return Id == other.Id;
        }

        /// <summary>
        /// Indicates whether the current object is equal to the <paramref name="obj"/> object.
        /// </summary>
        /// <param name="obj">The object to compare this object to.</param>
        public override bool Equals(object obj) => Equals(obj as Tenant);

        /// <summary>
        /// Serves as the default hash function.
        /// <remarks>Not required, implemented to avoid compiler warning.</remarks>
        /// </summary>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Returns a textual representation.
        /// </summary>
        public override string ToString() => $"{Id}, {Name}";
    }
}