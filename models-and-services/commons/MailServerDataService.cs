using KpokPatagon.Data;
using System.Collections.Generic;

namespace KpokPatagon.Commons
{
    /// <summary>
    /// <see cref="MailServerDataService"/> implements the data services for the Commons.MailServer table.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This service does not have any configuration for the required services for it to work.
    ///     </para>
    ///     <para>
    ///         All required services like the <b>CommandBuilder</b> and <b>DataModelCommandBuilder</b>
    ///         must be configured by a <b>DataServiceOptions&lt;MailServerDataService&gt;</b> 
    ///         configuration within the dependency injection service collection.
    ///     </para>
    /// </remarks>
    public class MailServerDataService : DataService
    {
        /// <summary>
        /// Creates a new instance of <see cref="MailServerDataService"/>.
        /// </summary>
        public MailServerDataService()
        {
            Schema = CommonsConsts.DefaultSchema;
            SequenceName = null;
        }


        string _alias;
        /// <summary>
        /// The alias for the underlaying table.
        /// </summary>
        public override string Alias
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_alias))
                    _alias = "MailServer";
                return _alias;
            }
            set { _alias = value; }
        }

        string _table;
        /// <summary>
        /// The name of the underlaying table.
        /// </summary>
        public override string Table
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_table))
                    _table = "MailServer";
                return _table;
            }
            set { _table = value; }
        }

        IDictionary<string, Field> _fields;
        /// <summary>
        /// A dictionary of field configuration for the underlaying table.
        /// </summary>
        public override IDictionary<string, Field> Fields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new Dictionary<string, Field>()
                    {
                        ["Id"] = new Field("Id", DbType.Text, 1, AutomaticType.Identity),
                        ["Name"] = new Field("Name", DbType.NText, 0, AutomaticType.None),
                        ["From"] = new Field("From", DbType.NText, 0, AutomaticType.None),
                        ["FromName"] = new Field("FromName", DbType.NText, 0, AutomaticType.None),
                        ["Port"] = new Field("Port", DbType.Integer, 0, AutomaticType.None),
                        ["RequiresSsl"] = new Field("RequiresSsl", DbType.Boolean, 0, AutomaticType.None),
                        ["UserName"] = new Field("UserName", DbType.Text, 0, AutomaticType.None),
                        ["Password"] = new Field("Password", DbType.Text, 0, AutomaticType.None)
                    };
                }
                return _fields;
            }
            set { _fields = value; }
        }
    }
}