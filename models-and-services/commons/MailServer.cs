using KpokPatagon.Data;
using KpokPatagon.Messaging;
using System;

namespace KpokPatagon.Commons
{
    /// <summary>
    /// <see cref="MailServer"/> implements a data model for mail servers configuration.
    /// </summary>
    public class MailServer : DataModel, IEquatable<MailServer>, ISmtpServerConfiguration
    {
        /// <summary>
        /// Creates a new instance of <see cref="MailServer"/>.
        /// </summary>
        public MailServer()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MailServer"/>.
        /// </summary>
        /// <param name="id">Object Id.</param>
        public MailServer(string id)
        {
            Id = id;
        }


        string _id;
        /// <summary>
        /// The identification of this <see cref="MailServer"/>.
        /// </summary>
        [TextDataProperty("Id", DbType.Text, FieldName = "_id", PrimaryKey = 1, 
            MinLength = 0, MaxLength = 64, AutomaticType = AutomaticType.Identity)]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged(_id);
            }
        }

        string _name;
        /// <summary>
        /// Server name or IP address.
        /// </summary>
        [TextDataProperty("Name", DbType.NText, FieldName = "_name", Mandatory = true, MinLength = 0, MaxLength = 128)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(_name);
            }
        }

        string _from;
        /// <summary>
        /// Address from which mails are sent.
        /// </summary>
        [EmailTextDataProperty("From", DbType.NText, FieldName = "_from", Mandatory = true, MinLength = 0, MaxLength = 256)]
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                NotifyPropertyChanged(_from);
            }
        }

        string _fromName;
        /// <summary>
        /// Display name for the sending address.
        /// </summary>
        [TextDataProperty("FromName", DbType.NText, FieldName = "_fromName", Mandatory = true, MinLength = 0, MaxLength = 256)]
        public string FromName
        {
            get { return _fromName; }
            set
            {
                _fromName = value;
                NotifyPropertyChanged(_fromName);
            }
        }

        int _port;
        /// <summary>
        /// Mail sending port.
        /// </summary>
        [IntegerDataProperty("Port", DbType.Integer, FieldName = "_port", MinValue = 0, DefaultValue = 0)]
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                NotifyPropertyChanged(_port);
            }
        }

        bool _requiresSsl;
        /// <summary>
        /// Whether the server requires SSL.
        /// </summary>
        [GenericDataProperty("RequiresSsl", DbType.Boolean, FieldName = "_requiresSsl")]
        public bool RequiresSsl
        {
            get { return _requiresSsl; }
            set
            {
                _requiresSsl = value;
                NotifyPropertyChanged(_requiresSsl);
            }
        }

        string _userName;
        /// <summary>
        /// User name for use with authentication.
        /// </summary>
        [TextDataProperty("UserName", DbType.NText, FieldName = "_userName",
            MinLength = 0, MaxLength = 2048, Protection = ProtectionType.Encripted)]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged(_userName);
            }
        }

        string _password;
        /// <summary>
        /// Password of the user for use with authentication.
        /// </summary>
        [TextDataProperty("Password", DbType.NText, FieldName = "_password",
            MinLength = 0, MaxLength = 2048, Protection = ProtectionType.Encripted)]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged(_password);
            }
        }


        /// <summary>
        /// Gets whether the model validation must be enforced.
        /// </summary>
        public override bool EnforceModelValidation => false;


        /// <summary>
        /// Checks whether <paramref name="other"/> is equal to this object.
        /// </summary>
        /// <param name="other">The object to check equality to.</param>
        public bool Equals(MailServer other)
        {
            if (other == null) return false;
            else return Id == other.Id;
        }

        /// <summary>
        /// Checks whether <paramref name="obj"/> is equal to this object.
        /// </summary>
        /// <param name="obj">The object to compare this object to.</param>
        public override bool Equals(object obj) => Equals(obj as MailServer);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <remarks>Not required, implemented to avoid compiler warning.</remarks>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Returns a textual representation of a <see cref="MailServer"/>.
        /// </summary>
        public override string ToString() => $"{Name} ({From})";
    }
}